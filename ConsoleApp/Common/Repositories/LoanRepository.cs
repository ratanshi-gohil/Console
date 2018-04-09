using EllieMae.Encompass.BusinessObjects.ExternalOrganization;
using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.BusinessObjects.Loans.Logging;
using EllieMae.Encompass.BusinessObjects.Loans.Templates;
using EllieMae.Encompass.BusinessObjects.Users;
using EllieMae.Encompass.Collections;
using EllieMae.Encompass.Query;
using EllieMae.Encompass.Reporting;
using ConsoleApp.Common.Model;
using ConsoleApp.Common.Wrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ConsoleApp.Common.Repositories
{
    public class LoanRepository : EncompassRepository, ILoanRepository
    {
        public CreateLoanResponse CreateEncompassLoan(CreateLoanRequest request, EncompassSession encompassSession, DuplicateCheckResult duplicateCheckResult, Dictionary<string, long> timings)
        {
            var loanResponse = new CreateLoanResponse();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            try
            {
                using (var loanWrapper = new LoanWrapper(encompassSession.Session))
                {
                    var loan = loanWrapper.CreateLoan();

                    loan.CalculationsEnabled = false; //toggle off calcs;

                    loan.LoanFolder = "Wholesale - Registered";

                    sw.Stop();
                    timings.Add("Create Loan", sw.ElapsedMilliseconds);

                    sw.Restart();
                    SetValue(loan, "CX.LENDER.LOAN.NUMBER", request.LenderLoanNumber);
                    SetValue(loan, "CX.MANDATORY.BID.ID", request.BidIdNumber);
                    SetValue(loan, "CX.MANDATORY.LOAN.TYPE", request.MandatoryLoanType);
                    SetValue(loan, "14", request.SubjectProperty.State);

                    SetValue(loan, "1401", GetLoanProgram(request.LoanProgram));

                    SetValue(loan, "1041", GetPropertyType(request.PropertyType));
                    SetValue(loan, "1811", GetPropertyWillBe(request.PropertyWillBe));
                    SetValue(loan, "16", request.NumberOfUnits);
                    SetValue(loan, "19", GetLoanPurpose(request.LoanPurpose, request.PurposeOfRefinance));
                    SetValue(loan, "299", GetPurposeOfRefinance(request.PurposeOfRefinance));
                    SetValue(loan, "315", request.Company);
                    SetValue(loan, "3237", request.CompanyNmls);
                    
                    try
                    {
                        if (!string.IsNullOrEmpty(request.CompanyNmls))
                        {
                            ExternalOrganization[] orgCollection = encompassSession.Session.Organizations.GetAllExternalOrganizations().ToArray();
                            ExternalOrganization tpoCompany = orgCollection.Single(c => c.NmlsId == request.CompanyNmls);
                            if (tpoCompany != null)
                            {
                                SetValue(loan, "TPO.X14", tpoCompany.OrganizationName);
                                SetValue(loan, "315", tpoCompany.OrganizationName);
                                SetValue(loan, "TPO.X15", tpoCompany.ExternalID);
                                SetValue(loan, "TPO.X16", tpoCompany.OrganizationID);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string logMsg = $"Exception in External Organization search. CompanyNmls = {request.CompanyNmls}, Error Message: {e.Message}.";
                        //this.Log().Error(logMsg, e);
                        SetValue(loan, "TPO.X14", request.Company);
                        SetValue(loan, "TPO.X16", request.CompanyNmls);
                    }

                    SetValue(loan, "601", GetBuildingStatus(request.BuildingStatus));

                    sw.Stop();
                    timings.Add("Assign Loan Values", sw.ElapsedMilliseconds);

                    bool isFirstTime = true;
                    EllieMae.Encompass.BusinessObjects.Loans.BorrowerPair newPair;

                    int count = 1;
                    foreach (var borrowerPair in request.BorrowerPairs)
                    {
                        if (isFirstTime)
                        {
                            newPair = loan.BorrowerPairs.Current;
                            isFirstTime = false;
                        }
                        else
                        {
                            newPair = loan.BorrowerPairs.Add();
                        }

                        sw.Restart();
                        if (borrowerPair.PrimaryBorrower != null)
                        {
                            loan.Fields["4000"].SetValueForBorrowerPair(newPair, borrowerPair.PrimaryBorrower.FirstName);
                            loan.Fields["4002"].SetValueForBorrowerPair(newPair, borrowerPair.PrimaryBorrower.LastName);
                            loan.Fields["36"].SetValueForBorrowerPair(newPair, borrowerPair.PrimaryBorrower.FirstName);
                            loan.Fields["37"].SetValueForBorrowerPair(newPair, borrowerPair.PrimaryBorrower.LastName);
                            //loan.Fields["67"].SetValueForBorrowerPair(newPair, borrowerPair.PrimaryBorrower.Experianfico);
                        }

                        if (borrowerPair.CoBorrower != null && !string.IsNullOrEmpty(borrowerPair.CoBorrower.LastName))
                        {
                            loan.Fields["4004"].SetValueForBorrowerPair(newPair, borrowerPair.CoBorrower.FirstName);
                            loan.Fields["4006"].SetValueForBorrowerPair(newPair, borrowerPair.CoBorrower.LastName);
                            loan.Fields["68"].SetValueForBorrowerPair(newPair, borrowerPair.CoBorrower.FirstName);
                            loan.Fields["69"].SetValueForBorrowerPair(newPair, borrowerPair.CoBorrower.LastName);
                            //loan.Fields["60"].SetValueForBorrowerPair(newPair, borrowerPair.CoBorrower.Experianfico);
                        }

                        sw.Stop();
                        timings.Add("Populate Borrower Pair " + count, sw.ElapsedMilliseconds);

                        count++;
                    }

                    SetValue(loan, "1109", request.LoanAmount);
                    SetValue(loan, "2626", request.Channel);
                    //SetValue(loan, "CX.CORR.DELEGATED", "Y");
                    SetValue(loan, "CX.CORR.TYPE", request.CorrespondentDelegated);

                    sw.Restart();

                    if (request.Locks.Count > 0)
                    {
                        LockRequest Lockrequest = loan.Log.LockRequests.Add();
                        Lockrequest.Fields["3965"].Value = request.Locks[0].DeliveryOption;
                        Lockrequest.Fields["3406"].Value = "Total Margin";
                        Lockrequest.Fields["3407"].Value = request.Locks[0].TotalMargin;
                        //Lockrequest.Fields["2218"].Value = request.Locks[0].FinalPrice;
                        Lockrequest.Fields["2161"].Value = request.Locks[0].FinalPrice - request.Locks[0].TotalMargin;
                        Lockrequest.Fields["2149"].Value = request.Locks[0].LockDate;
                        Lockrequest.Fields["2089"].Value = request.Locks[0].LockDate;
                        Lockrequest.Fields["761"].Value = request.Locks[0].LockDate;
                        Lockrequest.Fields["2091"].Value = request.Locks[0].ExpirationDate;
                        Lockrequest.Fields["2151"].Value = request.Locks[0].ExpirationDate;
                        Lockrequest.Fields["762"].Value = request.Locks[0].ExpirationDate;

                        LockConfirmation conf = Lockrequest.Confirm();
                    }

                    SetValue(loan, "3", request.InterestRate);
                    SetValue(loan, "VASUMM.X23", request.CreditScoreforDecisionMaking);
                    SetValue(loan, "4105", "Mandatory");

                    if (request.LoanPurpose.ToUpper().Equals("PURCHASE"))
                        SetValue(loan, "136", GetPurchasePrice(request.LoanAmount, request.LTV)); //Purchase Price 
                    else if (request.LoanPurpose.ToUpper().Equals("REFINANCE"))
                        SetValue(loan, "356", GetPurchasePrice(request.LoanAmount, request.LTV)); //Appraised Value 

                    if (request.LTV.HasValue && request.LTV.Value > 0 && request.CLTV.HasValue && request.CLTV.Value > 0)
                    {   //1st Mortgage and 2nd Mortgage amounts
                        SetValue(loan, "427", request.LoanAmount); //(request.CLTV * GetPurchasePrice(request.LoanAmount, request.LTV)) / 100);
                        SetValue(loan, "428", (request.CLTV * GetPurchasePrice(request.LoanAmount, request.LTV)) / 100 - request.LoanAmount);
                    }

                    sw.Stop();
                    timings.Add("Locking Loan", sw.ElapsedMilliseconds);

                    //commit the loan - need to do this before we can assign user rights :(
                    sw.Restart();

                    loan.CalculationsEnabled = true; // turn calcs back on.

                    loan.MSLock = false; //this flag is required to put the milestone list in auto mode

                    loan.Commit();

                    sw.Stop();
                    timings.Add("Commit", sw.ElapsedMilliseconds);

                    var loanNumber = loan.Fields["364"].Value?.ToString();
                    //this.Log().Debug("Created loan {0}.", loanNumber);
                    loanResponse.LoanNumber = loanNumber;
                    loanResponse.LoanGUID = loan.Fields["GUID"].Value?.ToString();
                    loanWrapper.LoanNumber = loanNumber;

                    LogTimings(timings, loanNumber);

                    loanResponse.IsSuccess = true;
                    loanResponse.IsException = false;

                    return loanResponse;
                }
            }
            catch (Exception e)
            {
                string logMsg = string.Format("Exception in CreateEncompassLoan. Bid ID Number = {0}, Error Message: {1};", request.BidIdNumber, e.Message);
                //this.Log().Error(logMsg, e);

                return new CreateLoanResponse()
                {
                    IsSuccess = false,
                    IsException = true,
                    ErrorMessage = e.Message
                };
            }
        }

        //private ResponseBidLoan InitializeResponseBidLoan(CreateLoanRequest request)
        //{
        //    if (request == null) return new ResponseBidLoan();
        //    return new ResponseBidLoan()
        //    {
        //        BidId = request.BidIdNumber,
        //        LenderLoanNumber = request.LenderLoanNumber,
        //        BorrowerFirstName = request.BorrowerPairs[0].PrimaryBorrower.FirstName,
        //        BorrowerLastName = request.BorrowerPairs[0].PrimaryBorrower.LastName,
        //        CoborrowerFirstName = request.BorrowerPairs[0].CoBorrower.FirstName,
        //        CoborrowerLastName = request.BorrowerPairs[0].CoBorrower.LastName,
        //        Company = request.Company,
        //        CompanyNmlsId = request.CompanyNmls
        //    };
        //}
        public void CreateLoans(List<CreateLoanRequest> bidLoans)
        {
            //var responseBidTape = new ResponseBidTape()
            //{
            //    BidId = bidLoans[0].BidIdNumber,
            //    BidLoans = new List<ResponseBidLoan>()
            //};
            //ResponseBidLoan responseBidLoan = new ResponseBidLoan { };
            try
            {
                var timings = new Dictionary<string, long>();
                Stopwatch sw = new Stopwatch();

                using (var encompassSession = new EncompassSession())
                {
                    // Create the Encompass session
                    sw.Start();
                    encompassSession.Open();
                    sw.Stop();

                    timings.Add("Session Create", sw.ElapsedMilliseconds);
                    foreach (var bidLoan in bidLoans)
                    {
                        timings = new Dictionary<string, long>();
                        try
                        {
                            //responseBidLoan = InitializeResponseBidLoan(bidLoan);

                            // Duplicate loan check
                            var loanPurpose = GetLoanPurpose(bidLoan.LoanPurpose, bidLoan.PurposeOfRefinance);
                            var duplicateCheckPoints = new DuplicateCheckPoints()
                            {
                                BorrowerLastName = bidLoan.BorrowerPairs[0].PrimaryBorrower.LastName,
                                CompanyNMLS = bidLoan.CompanyNmls,
                                LenderLoanNumber = bidLoan.LenderLoanNumber,
                                LoanPurpose = loanPurpose,
                                PropertyState = bidLoan.SubjectProperty.State,
                            };

                            var duplicateCheckResult = CompassDuplicateLoanCheck(encompassSession, duplicateCheckPoints);
                            sw.Stop();
                            timings.Add("Duplicate Loan Check", sw.ElapsedMilliseconds);

                            if (duplicateCheckResult.Result == DuplicateCheckStatus.Success.ToString())
                            {
                                var loanCreateResult = CreateEncompassLoan(bidLoan, encompassSession, duplicateCheckResult, timings);

                                if (loanCreateResult.IsSuccess)
                                {
                                    //responseBidLoan.Status = Common.Constants.AlphaBidLoan.Success;
                                    //responseBidLoan.EncompassLoanNumber = loanCreateResult.LoanNumber;
                                }
                                else
                                {
                                    //responseBidLoan.Status = Common.Constants.AlphaBidLoan.Failed;
                                    //responseBidLoan.StatusReason = loanCreateResult.ErrorMessage;
                                }
                            }
                            else if (duplicateCheckResult.Result == DuplicateCheckStatus.Validate.ToString())
                            {
                                var loanCreateResult = CreateEncompassLoan(bidLoan, encompassSession, duplicateCheckResult, timings);
                                if (loanCreateResult.IsSuccess)
                                {
                                    //responseBidLoan.Status = Common.Constants.AlphaBidLoan.Success;
                                    //responseBidLoan.StatusReason = string.Format("Failed Validation: {0}.", duplicateCheckResult.MatchResults);
                                    //responseBidLoan.EncompassLoanNumber = loanCreateResult.LoanNumber;
                                }
                                else
                                {
                                    //responseBidLoan.Status = Common.Constants.AlphaBidLoan.Failed;
                                    //responseBidLoan.StatusReason = loanCreateResult.ErrorMessage;
                                }
                            }
                            else
                            {
                                //responseBidLoan.Status = Common.Constants.AlphaBidLoan.Failed;
                                //responseBidLoan.StatusReason = string.Format("Duplicate Check Failed: {0}.", duplicateCheckResult.MatchResults);
                            }

                            //responseBidTape.BidLoans.Add(responseBidLoan);
                        }
                        catch (Exception e)
                        {
                            //string logMsg = string.Format("Exception in CreateLoans. Bid ID Number = {0}, Lender LoanNumber: {1}, Error Message: {2};", bidLoan.BidIdNumber, bidLoan.LenderLoanNumber, e.Message);
                            //this.Log().Error(logMsg, e);

                            //responseBidLoan.Status = Common.Constants.AlphaBidLoan.Failed;
                            //responseBidLoan.StatusReason = logMsg;

                            //responseBidTape.BidLoans.Add(responseBidLoan);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                //string logMsg = string.Format("Exception in CreateLoans. Bid ID Number = {0}, Error Message: {1};", bidLoans[0].BidIdNumber, e.Message);
                //this.Log().Error(logMsg, e);

                //bidLoans.ForEach(x => responseBidTape.BidLoans.Add(InitializeResponseBidLoan(x)));
                //responseBidTape.BidLoans.ForEach((x) => { x.Status = Common.Constants.AlphaBidLoan.Failed; x.StatusReason = logMsg; });
            }

            //return responseBidTape;
        }

        public DuplicateCheckResult CompassDuplicateLoanCheck(EncompassSession encompassSession, DuplicateCheckPoints duplicateCheckPoints)
        {
            DuplicateCheckResult result = new DuplicateCheckResult();
            StringList searchFields = new StringList();
            searchFields.Add("Loan.LoanNumber"); // LoanNumber
            searchFields.Add("Fields.4002"); // BorrowerLastName
            //searchFields.Add("Fields.65"); // BorrowerLastSSN
            //searchFields.Add("Fields.11"); // Property Address
            //searchFields.Add("Fields.12"); // Property City
            searchFields.Add("Fields.14"); // Property State
            //searchFields.Add("Fields.15"); // Property ZipCode
            searchFields.Add("Fields.19"); // Loan Purpose
            searchFields.Add("Fields.CX.LENDER.LOAN.NUMBER"); // Lender Loan Number

            var cnmlsCriterion = new StringFieldCriterion("Fields.3237", duplicateCheckPoints.CompanyNMLS, StringFieldMatchType.Exact, true);
            var activeCriterion = new StringFieldCriterion("Fields.1393", "Active Loan", StringFieldMatchType.Exact, true);

            var searchCriteria = cnmlsCriterion.And(activeCriterion);

            LoanReportDataList results;
            using (var cursor = encompassSession.Session.Reports.OpenReportCursor(searchFields, searchCriteria))
            {
                results = cursor.GetItems(0, cursor.Count); //Pull the result set in one shot
                cursor.Close(); //Release the server resources
            }

            List<DuplicateCheckPoints> duplicateLoans = new List<DuplicateCheckPoints>();

            if (results != null)
            {
                foreach (LoanReportData data in results)
                {
                    // It is possible for the loan number to be blank.  Ignore those.
                    var loanNumber = data["Loan.LoanNumber"].ToString();
                    if (string.IsNullOrWhiteSpace(loanNumber))
                    {
                        continue;
                    }

                    duplicateLoans.Add(new DuplicateCheckPoints()
                    {
                        LoanNumber = data["Loan.LoanNumber"].ToString(),
                        BorrowerLastName = data["Fields.4002"].ToString(),
                        //PropertyAddress = data["Fields.11"].ToString(),
                        //PropertyCity = data["Fields.12"].ToString(),
                        PropertyState = data["Fields.14"].ToString(),
                        //PropertyZipCode = data["Fields.15"].ToString(),
                        LoanPurpose = data["Fields.19"].ToString(),
                        LenderLoanNumber = data["Fields.CX.LENDER.LOAN.NUMBER"].ToString()
                    });
                }
            }

            string matchString = "No match found";
            if (duplicateLoans.Count != 0)
            {
                var possibleDups = new List<DuplicateCheckPoints>();

                if (!string.IsNullOrWhiteSpace(duplicateCheckPoints.BorrowerLastName))
                {
                    var lastNameDups = duplicateLoans.Where(x => x.BorrowerLastName.Equals(duplicateCheckPoints.BorrowerLastName));
                    possibleDups.AddRange(lastNameDups);
                }

                if (!string.IsNullOrWhiteSpace(duplicateCheckPoints.PropertyState))
                {
                    var addressDups = duplicateLoans.Where(x => x.PropertyState.Equals(duplicateCheckPoints.PropertyState));
                    possibleDups.AddRange(addressDups);
                }

                if (!string.IsNullOrWhiteSpace(duplicateCheckPoints.LenderLoanNumber))
                {
                    var lenderLoanNumberDups = duplicateLoans.Where(x => x.LenderLoanNumber.Equals(duplicateCheckPoints.LenderLoanNumber));
                    possibleDups.AddRange(lenderLoanNumberDups);
                }

                if (!string.IsNullOrWhiteSpace(duplicateCheckPoints.LoanPurpose))
                {
                    var loanPurposeDups = duplicateLoans.Where(x => x.LoanPurpose.Equals(duplicateCheckPoints.LoanPurpose));
                    possibleDups.AddRange(loanPurposeDups);
                }

                var matchResult = new List<string>();

                if (possibleDups.Count > 0)
                {
                    string mostMatchesLoanId = possibleDups.GroupBy(x => x.LoanNumber).OrderByDescending(y => y.Count()).First().Key;

                    DuplicateCheckPoints mostMatchesLoan = duplicateLoans.Where(x => x.LoanNumber.Equals(mostMatchesLoanId)).FirstOrDefault();
                    if (mostMatchesLoan == null)
                    {
                        throw new Exception("Unexpected error: duplicateLoan is null.");
                    }

                    if (!string.IsNullOrWhiteSpace(mostMatchesLoan.BorrowerLastName) && mostMatchesLoan.BorrowerLastName.Equals(duplicateCheckPoints.BorrowerLastName))
                    {
                        matchResult.Add("Last Name");
                    }

                    if (!string.IsNullOrWhiteSpace(mostMatchesLoan.PropertyState) && mostMatchesLoan.PropertyState.Equals(duplicateCheckPoints.PropertyState))
                    {
                        matchResult.Add("Property State");
                    }

                    if (!string.IsNullOrWhiteSpace(mostMatchesLoan.LoanPurpose) && mostMatchesLoan.LoanPurpose.Equals(duplicateCheckPoints.LoanPurpose))
                    {
                        matchResult.Add("Loan Purpose");
                    }

                    if (!string.IsNullOrWhiteSpace(mostMatchesLoan.LenderLoanNumber) && mostMatchesLoan.LenderLoanNumber.Equals(duplicateCheckPoints.LenderLoanNumber))
                    {
                        matchResult.Add("Lender Loan Number");
                    }

                    matchString = string.Format("Loan ID: {0} - matches {1}", mostMatchesLoanId, string.Join(",", matchResult));
                }

                switch (matchResult.Count)
                {
                    case 0:
                    case 1:
                    case 2:
                        result.Result = DuplicateCheckStatus.Success.ToString();
                        break;
                    case 3:
                        result.Result = DuplicateCheckStatus.Validate.ToString();
                        break;
                    default:
                        result.Result = DuplicateCheckStatus.Fail.ToString();
                        break;
                }
            }
            else
            {
                result.Result = DuplicateCheckStatus.Success.ToString();
            }

            result.MatchResults = matchString;
            return result;
        }

        public LoanData GetLoan(string loanNumber)
        {

            int maxBorrowerPairsAllowed = 2;
            string fieldSuffix = "";
            using (var encompassSession = new EncompassSession())
            {
                encompassSession.Open();

                StringList searchFields = new StringList();
                searchFields.Add("Fields.2626");
                searchFields.Add("Fields.1172");
                searchFields.Add("Fields.1401");
                searchFields.Add("Fields.1811");
                searchFields.Add("Fields.608");
                searchFields.Add("Fields.4");
                searchFields.Add("Fields.MORNET.X67");
                searchFields.Add("Fields.19");
                searchFields.Add("Fields.420");
                searchFields.Add("Fields.2");
                searchFields.Add("Fields.1109");
                searchFields.Add("Fields.136");
                searchFields.Add("Fields.356");
                searchFields.Add("Fields.1821");
                searchFields.Add("Fields.1085");
                searchFields.Add("Fields.745");
                searchFields.Add("Fields.1996");
                searchFields.Add("Fields.2553");
                searchFields.Add("Fields.3197");
                searchFields.Add("Fields.3152");
                searchFields.Add("Fields.3153");
                searchFields.Add("Fields.3154");
                searchFields.Add("Fields.3979");
                searchFields.Add("Fields.3980");
                searchFields.Add("Fields.3147");
                searchFields.Add("Fields.748");
                searchFields.Add("Fields.1997");
                searchFields.Add("Fields.353");
                searchFields.Add("Fields.976");
                searchFields.Add("Fields.1540");
                searchFields.Add("Fields.1051");
                searchFields.Add("Fields.740");
                searchFields.Add("Fields.742");
                searchFields.Add("Fields.MORNET.X15");
                searchFields.Add("Fields.CASASRN.x158");
                searchFields.Add("Fields.VASUMM.X23");
                searchFields.Add("Fields.LCP.X1");
                searchFields.Add("Fields.11");
                searchFields.Add("Fields.12");
                searchFields.Add("Fields.14");
                searchFields.Add("Fields.15");
                searchFields.Add("Fields.13");
                searchFields.Add("Fields.1066");
                searchFields.Add("Fields.1041");
                searchFields.Add("Fields.18");
                searchFields.Add("Fields.601");
                searchFields.Add("Fields.16");
                searchFields.Add("Fields.3237");
                searchFields.Add("Fields.364");
                searchFields.Add("Fields.1393");
                searchFields.Add("Fields.1881");
                searchFields.Add("Fields.299");
                searchFields.Add("Fields.3");
                searchFields.Add("Fields.ULDD.RefinanceCashOutAmount");
                searchFields.Add("Fields.934");
                searchFields.Add("Fields.324");
                searchFields.Add("Fields.319");
                searchFields.Add("Fields.313");
                searchFields.Add("Fields.321");
                searchFields.Add("Fields.323");
                searchFields.Add("Fields.VEND.X150");
                searchFields.Add("Fields.714");
                searchFields.Add("Fields.VEND.X139");
                searchFields.Add("Fields.416");
                searchFields.Add("Fields.984");
                searchFields.Add("Fields.707");
                searchFields.Add("Fields.VEND.X162");
                searchFields.Add("Fields.VEND.X13");
                searchFields.Add("Fields.625");
                searchFields.Add("Fields.VEND.X271");
                searchFields.Add("Fields.VEND.X317");
                searchFields.Add("Fields.TPO.X90");
                searchFields.Add("Fields.CX.DV.FOLDERID");
                searchFields.Add("Fields.2400");
                searchFields.Add("Fields.762");
                searchFields.Add("Fields.2287");
                searchFields.Add("Fields.2160");
                searchFields.Add("Fields.2203");
                searchFields.Add("Fields.1040");
                searchFields.Add("Fields.315");
                searchFields.Add("Fields.1612");
                searchFields.Add("Fields.362");
                searchFields.Add("Fields.1409");
                searchFields.Add("Fields.1408");
                searchFields.Add("Fields.618");
                searchFields.Add("Fields.2301");
                searchFields.Add("Fields.2303");
                searchFields.Add("Fields.2987");
                searchFields.Add("Fields.2302");
                searchFields.Add("Fields.TPO.X88");
                //searchFields.Add("Fields.CX.CORR.DELEGATED");
                searchFields.Add("Fields.CX.4THPARTY");
                searchFields.Add("Fields.CX.CLOSINGPKG.RCVD.DATE");

                for (int i = 0; i < maxBorrowerPairsAllowed; i++)
                {
                    fieldSuffix = (i == 0) ? "" : "#" + (i + 1).ToString();
                    searchFields.Add("Fields.4000" + fieldSuffix);
                    searchFields.Add("Fields.4001" + fieldSuffix);
                    searchFields.Add("Fields.4002" + fieldSuffix);
                    searchFields.Add("Fields.1240" + fieldSuffix);
                    searchFields.Add("Fields.65" + fieldSuffix);
                    searchFields.Add("Fields.1402" + fieldSuffix);
                    searchFields.Add("Fields.66" + fieldSuffix);
                    searchFields.Add("Fields.FR0104" + fieldSuffix);
                    searchFields.Add("Fields.FR0106" + fieldSuffix);
                    searchFields.Add("Fields.FR0107" + fieldSuffix);
                    searchFields.Add("Fields.FR0108" + fieldSuffix);
                    searchFields.Add("Fields.1416" + fieldSuffix);
                    searchFields.Add("Fields.1417" + fieldSuffix);
                    searchFields.Add("Fields.1418" + fieldSuffix);
                    searchFields.Add("Fields.1419" + fieldSuffix);
                    searchFields.Add("Fields.67" + fieldSuffix);
                    searchFields.Add("Fields.FE0115" + fieldSuffix);
                    searchFields.Add("Fields.FE0115" + fieldSuffix);
                    searchFields.Add("Fields.FE0102" + fieldSuffix);
                    searchFields.Add("Fields.FE0104" + fieldSuffix);
                    searchFields.Add("Fields.FE0105" + fieldSuffix);
                    searchFields.Add("Fields.FE0106" + fieldSuffix);
                    searchFields.Add("Fields.FE0107" + fieldSuffix);
                    searchFields.Add("Fields.FE0117" + fieldSuffix);
                    searchFields.Add("Fields.FE0113" + fieldSuffix);
                    searchFields.Add("Fields.FE0110" + fieldSuffix);
                    searchFields.Add("Fields.FE0116" + fieldSuffix);
                    searchFields.Add("Fields.910" + fieldSuffix);
                    searchFields.Add("Fields.52" + fieldSuffix);
                    searchFields.Add("Fields.4004" + fieldSuffix);
                    searchFields.Add("Fields.4005" + fieldSuffix);
                    searchFields.Add("Fields.4006" + fieldSuffix);
                    searchFields.Add("Fields.1268" + fieldSuffix);
                    searchFields.Add("Fields.97" + fieldSuffix);
                    searchFields.Add("Fields.1403" + fieldSuffix);
                    searchFields.Add("Fields.98" + fieldSuffix);
                    searchFields.Add("Fields.FR0204" + fieldSuffix);
                    searchFields.Add("Fields.FR0206" + fieldSuffix);
                    searchFields.Add("Fields.FR0207" + fieldSuffix);
                    searchFields.Add("Fields.FR0208" + fieldSuffix);
                    searchFields.Add("Fields.1519" + fieldSuffix);
                    searchFields.Add("Fields.1520" + fieldSuffix);
                    searchFields.Add("Fields.1521" + fieldSuffix);
                    searchFields.Add("Fields.1522" + fieldSuffix);
                    searchFields.Add("Fields.60" + fieldSuffix);
                    searchFields.Add("Fields.FE0215" + fieldSuffix);
                    searchFields.Add("Fields.FE0215" + fieldSuffix);
                    searchFields.Add("Fields.FE0202" + fieldSuffix);
                    searchFields.Add("Fields.FE0204" + fieldSuffix);
                    searchFields.Add("Fields.FE0205" + fieldSuffix);
                    searchFields.Add("Fields.FE0206" + fieldSuffix);
                    searchFields.Add("Fields.FE0207" + fieldSuffix);
                    searchFields.Add("Fields.FE0217" + fieldSuffix);
                    searchFields.Add("Fields.FE0213" + fieldSuffix);
                    searchFields.Add("Fields.FE0210" + fieldSuffix);
                    searchFields.Add("Fields.FE0216" + fieldSuffix);
                    searchFields.Add("Fields.911" + fieldSuffix);
                    searchFields.Add("Fields.84" + fieldSuffix);
                }

                var loanNumberCriterion = new StringFieldCriterion("Loan.LoanNumber", loanNumber, StringFieldMatchType.CaseInsensitive, true);

                using (var cursor = encompassSession.Session.Reports.OpenReportCursor(searchFields, loanNumberCriterion))
                {
                    if (cursor.Count == 0)
                    {
                        cursor.Close();
                        return null;
                    }
                    var data = cursor.GetItem(0);
                    var loanResponse = new LoanData();

                    for (int i = 0; i < maxBorrowerPairsAllowed; i++)
                    {
                        fieldSuffix = (i == 0) ? "" : "#" + (i + 1).ToString();
                        var bp = new Model.BorrowerPair();
                        bp.PrimaryBorrower.FirstName = GetStringValue(data, "Fields.4000" + fieldSuffix);
                        bp.PrimaryBorrower.MiddleName = GetStringValue(data, "Fields.4001" + fieldSuffix);
                        bp.PrimaryBorrower.LastName = GetStringValue(data, "Fields.4002" + fieldSuffix);
                        bp.PrimaryBorrower.Ssn = GetStringValue(data, "Fields.65" + fieldSuffix);


                        bp.CoBorrower.FirstName = GetStringValue(data, "Fields.4004" + fieldSuffix);
                        bp.CoBorrower.MiddleName = GetStringValue(data, "Fields.4005" + fieldSuffix);
                        bp.CoBorrower.LastName = GetStringValue(data, "Fields.4006" + fieldSuffix);
                        bp.CoBorrower.Ssn = GetStringValue(data, "Fields.97" + fieldSuffix);

                        if (bp.HasData())
                        {
                            loanResponse.BorrowerPairs.Add(bp);
                        }
                    }

                    return loanResponse;
                }
            }
        }

        private void LogTimings(Dictionary<string, long> timings, string loanNumber)
        {
            StringBuilder sb = new StringBuilder("Timings for creation of loan ");
            sb.Append(loanNumber);
            sb.Append(": ");
            foreach (var key in timings.Keys)
            {
                sb.Append(key);
                sb.Append(" = ");
                sb.Append(timings[key].ToString());
                sb.Append(" ms, ");
            }

            //this.Log().Debug(sb.ToString());
        }
    }
}
