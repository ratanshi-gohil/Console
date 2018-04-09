using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.Reporting;
using System;
using System.Collections.Generic;
using EllieMae.Encompass.BusinessObjects.ExternalOrganization;
using EllieMae.Encompass.Collections;
using ConsoleApp.Common.Model;
using ConsoleApp.Common.Wrappers;
//using ConsoleApp.Common.Config;

namespace ConsoleApp.Common.Repositories
{
    public abstract class EncompassRepository
    {
        protected void SetValue(Loan loan, string fieldId, string value)
        {
            loan.Fields[fieldId].Value = value;
        }

        protected void SetValue(Loan loan, string fieldId, DateTime? value)
        {
            if (value.HasValue)
            {
                loan.Fields[fieldId].Value = value.Value.ToString("MM/dd/yyyy");
            }
        }

        protected void SetValue(Loan loan, string fieldId, YesNo? value)
        {
            if (value.HasValue)
            {
                loan.Fields[fieldId].Value = value.ToString();
            }
        }

        protected void SetValue(Loan loan, string fieldId, decimal? value)
        {
            if (value.HasValue)
            {
                loan.Fields[fieldId].Value = value.Value;
            }
        }

        protected void SetValue(Loan loan, string fieldId, bool? value)
        {
            if (value.HasValue)
            {
                if (value.Value)
                {
                    loan.Fields[fieldId].Value = "Y";
                }
                else
                {
                    loan.Fields[fieldId].Value = "N";
                }
            }
            else
            {
                loan.Fields[fieldId].Value = string.Empty;
            }
        }

        protected void SetValue(Loan loan, EllieMae.Encompass.BusinessObjects.Loans.BorrowerPair borrowerPair, string fieldId, string value)
        {
            loan.Fields[fieldId].SetValueForBorrowerPair(borrowerPair, value);
        }

        protected string GetStringValue(Loan loan, string fieldId)
        {
            var field = loan.Fields[fieldId];
            if (field == null)
            {
                return null;
            }

            var formatted = field.FormattedValue;
            if (string.IsNullOrWhiteSpace(formatted))
            {
                return null;
            }

            return formatted;
        }

        protected string GetStringValue(LoanReportData data, string fieldId)
        {
            var field = data[fieldId];
            if (field == null)
            {
                return null;
            }

            var formatted = field.ToString();
            if (string.IsNullOrWhiteSpace(formatted))
            {
                return null;
            }

            return formatted;
        }

        protected string GetStringValue(Loan loan, string fieldId, EllieMae.Encompass.BusinessObjects.Loans.BorrowerPair borrowerPair)
        {
            var field = loan.Fields[fieldId];
            if (field == null)
            {
                return null;
            }

            var value = field.GetValueForBorrowerPair(borrowerPair);
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            return value;
        }

        protected int? GetIntValue(Loan loan, string fieldId)
        {
            int? toReturn = new int?();

            var value = loan.Fields[fieldId].Value;
            if (value == null)
            {
                return toReturn;
            }

            int temp;
            bool attempt = int.TryParse(value.ToString(), out temp);
            if (attempt)
            {
                return temp;
            }
            else
            {
                return toReturn;
            }
        }

        protected int? GetIntValue(Loan loan, string fieldId, EllieMae.Encompass.BusinessObjects.Loans.BorrowerPair borrowerPair)
        {
            int? toReturn = new int?();

            var field = loan.Fields[fieldId];
            if (field == null)
            {
                return null;
            }

            var value = field.GetValueForBorrowerPair(borrowerPair);
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            int temp;
            bool attempt = int.TryParse(value.ToString(), out temp);
            if (attempt)
            {
                return temp;
            }
            else
            {
                return toReturn;
            }
        }

        protected decimal? GetDecimalValue(Loan loan, string fieldId)
        {
            decimal? toReturn = new decimal?();

            var value = loan.Fields[fieldId].Value;
            if (value == null)
            {
                return toReturn;
            }

            decimal temp;
            bool attempt = decimal.TryParse(value.ToString(), out temp);
            if (attempt)
            {
                return temp;
            }
            else
            {
                return toReturn;
            }
        }

        protected decimal? GetDecimalValue(Loan loan, string fieldId, EllieMae.Encompass.BusinessObjects.Loans.BorrowerPair borrowerPair)
        {
            decimal? toReturn = new decimal?();

            var field = loan.Fields[fieldId];
            if (field == null)
            {
                return null;
            }

            var value = field.GetValueForBorrowerPair(borrowerPair);
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            decimal temp;
            bool attempt = decimal.TryParse(value.ToString(), out temp);
            if (attempt)
            {
                return temp;
            }
            else
            {
                return toReturn;
            }
        }

        protected decimal? GetDecimalValue(LoanReportData data, string fieldId)
        {
            decimal? toReturn = new decimal?();

            var value = data[fieldId];
            if (value == null)
            {
                return toReturn;
            }

            decimal temp;
            bool attempt = decimal.TryParse(value.ToString(), out temp);
            if (attempt)
            {
                return temp;
            }
            else
            {
                return toReturn;
            }
        }

        protected int? GetIntValue(LoanReportData data, string fieldId)
        {
            int? toReturn = new int?();

            var value = data[fieldId];
            if (value == null)
            {
                return toReturn;
            }

            int temp;
            bool attempt = int.TryParse(value.ToString(), out temp);
            if (attempt)
            {
                return temp;
            }
            else
            {
                return toReturn;
            }
        }

        protected bool? GetBooleanValue(LoanReportData data, string fieldId)
        {
            var value = data[fieldId];

            return ParseBool(value);
        }

        protected bool? GetBooleanValue(Loan loan, string fieldId)
        {
            var value = loan.Fields[fieldId].Value;

            return ParseBool(value);
        }

        private bool? ParseBool(object value)
        {
            bool? defaultValue = new bool?();
            if (value == null)
            {
                return defaultValue;
            }

            if (value.ToString().Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            else if (value.ToString().Equals("N", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            bool temp;
            bool attempt = bool.TryParse(value.ToString(), out temp);
            if (attempt)
            {
                return temp;
            }
            else
            {
                return defaultValue;
            }
        }

        protected DateTime? GetDateTimeValue(Loan loan, string fieldId)
        {
            DateTime? toReturn = new DateTime?();

            var value = loan.Fields[fieldId].Value;
            if (value == null)
            {
                return toReturn;
            }

            DateTime temp;
            bool attempt = DateTime.TryParse(value.ToString(), out temp);
            if (attempt)
            {
                return temp;
            }
            else
            {
                return toReturn;
            }
        }

        protected DateTime? GetDateTimeValue(LoanReportData data, string fieldId)
        {
            DateTime? toReturn = new DateTime?();

            var value = data[fieldId];
            if (value == null)
            {
                return toReturn;
            }

            DateTime temp;
            bool attempt = DateTime.TryParse(value.ToString(), out temp);
            if (attempt)
            {
                return temp;
            }
            else
            {
                return toReturn;
            }
        }

        protected DateTime? GetDateTimeValue(Loan loan, string fieldId, EllieMae.Encompass.BusinessObjects.Loans.BorrowerPair borrowerPair)
        {
            DateTime? toReturn = new DateTime?();

            var value = loan.Fields[fieldId].GetValueForBorrowerPair(borrowerPair);
            if (value == null)
            {
                return toReturn;
            }

            DateTime temp;
            bool attempt = DateTime.TryParse(value.ToString(), out temp);
            if (attempt)
            {
                return temp;
            }
            else
            {
                return toReturn;
            }
        }

        protected string GetPropertyType(string compassPropertyType)
        {
            switch (compassPropertyType.ToUpper())
            {
                case "SINGLE FAMILY DWELLING":
                    return "Detached";
                case "2-4 FAMILY DWELLING":
                    return "Attached";
                case "MULTI-FAMILY":
                    return string.Empty; // We don't do
                case "CONDOMINIUM":
                    return "Condominium";
                case "TOWNHOUSE":
                    return "Attached";
                case "PUD":
                    return "PUD";
                case "OTHER":
                    return string.Empty;// We don't do
                case "COOPERATIVE":
                    return "Cooperative";
                case "HOME AND BUSINESS COMBINED":
                    return string.Empty;// We don't do
                case "COMMERCIAL - NON-RESIDENTIAL":
                    return string.Empty;// We don't do
                case "MANUFACTURED/MOBILE HOME":
                    return "ManufacturedHousing";
                default:
                    return string.Empty;
            }
        }

        protected string GetPropertyWillBe(string compassPropertyWillBe)
        {
            switch (compassPropertyWillBe.ToUpper())
            {
                case "OWNER OCCUPIED":
                    return "PrimaryResidence";
                case "2ND":
                    return "SecondHome";
                case "NON-OWNER OCCUPIED/INVESTMENT":
                    return "Investor";
                default:
                    return string.Empty;
            }
        }

        protected decimal GetPurchasePrice(decimal? loanAmount, decimal? lTV)
        {
            if (lTV.HasValue && lTV.Value > 0)
                return 100 * (loanAmount.Value / lTV.Value);
            else return 0;
        }

        protected string GetLoanPurpose(string compassLoanPurpose, string compassPurposeOfRefinance = "")
        {
            switch (compassLoanPurpose.ToUpper())
            {
                case "PURCHASE":
                    return "Purchase";
                case "REFINANCE":
                    return compassPurposeOfRefinance.ToUpper().Equals("NO CASH-OUT") ? "NoCash-Out Refinance" : "Cash-Out Refinance";
                case "CONSTRUCTION":
                    return "ConstructionOnly";
                case "CONSTRUCTION/PERMANENT":
                    return "ConstructionToPermanent";
                case "HOME IMPROVEMENT":
                    return ""; //TODO
                case "OTHER":
                    return "Other";
                default:
                    return string.Empty;
            }
        }

        protected string GetPurposeOfRefinance(string compassPurposeOfRefinance)
        {
            switch (compassPurposeOfRefinance.ToUpper())
            {
                case "LIMITED CASH-OUT":
                    return "CashOutLimited";
                case "NO CASH-OUT":
                    return "NoCashOutOther";
                case "CASH-OUT/OTHER":
                    return "CashOutOther";
                case "CASH-OUT/HOME IMPROVEMENT":
                    return "CashOutHomeImprovement";
                case "CASH-OUT/DEBT CONSOLIDATION":
                    return "CashOutDebtConsolidation";
                case "OTHER":
                default:
                    return string.Empty;
            }
        }

        protected string GetBuildingStatus(string compassBuildingStatus)
        {
            switch (compassBuildingStatus)
            {
                case "1":
                    return "UnderConstruction";
                case "4":
                    return "Existing";
                default:
                    return string.Empty;
            }
        }

        protected string GetLoanProgram(string loanProgram)
        {
            switch (loanProgram)
            {

                case "CF10": return "CONF FIXED 10";
                case "CF15": return "CONF FIXED 15";
                case "CF15HB": return "CONF HIGH BALANCE FIXED 15";
                case "CF20": return "CONF FIXED 20";
                case "CF25": return "CONF FIXED 25";
                case "CF30": return "CONF FIXED 30";
                case "CF30HB": return "CONF HIGH BALANCE FIXED 30";
                case "FHA15": 
                case "FHA15:2": return "FHA FIXED 15";
                case "FHA15SL:2": return "FHA STREAMLINE FIXED 15";
                case "FHA20": 
                case "FHA20:2": return "FHA FIXED 20";
                case "FHA25": 
                case "FHA25:2": return "FHA FIXED 25";
                case "FHA25SL": return "FHA STREAMLINE FIXED 25";
                case "FHA30": 
                case "FHA30:2": return "FHA FIXED 30";
                case "FHA30HB": 
                case "FHA30HB:2": return "FHA HIGH BALANCE FIXED 30";
                case "FHA30SL": 
                case "FHA30SL:2": return "FHA STREAMLINE FIXED 30";
                case "FHA30SLHB": 
                case "FHA30SLHB:2": return "FHA STREAMLINE HIGH BALANCE FIXED 30";
                case "USDA30": 
                case "USDA30:2": return "USDA FIXED 30";
                case "VA15": 
                case "VA15:2": return "VA FIXED 15";
                case "VA15IRRRL:2": return "VA IRRRL FIXED 15";
                case "VA20": 
                case "VA20:2": return "VA FIXED 20";
                case "VA20IRRRL:2": return "VA IRRRL FIXED 20";
                case "VA25": 
                case "VA25:2": return "VA FIXED 25";
                case "VA25IRRRL": 
                case "VA25IRRRL:2": return "VA IRRRL FIXED 25";
                case "VA30": 
                case "VA30:2": return "VA FIXED 30";
                case "VA30HB":
                case "VA30HB:2": return "VA HIGH BALANCE FIXED 30";
                case "VA30IRRRL": 
                case "VA30IRRRL:2": return "VA IRRRL FIXED 30";
                case "VA30IRRRLHB": 
                case "VA30IRRRLHB:2": return "VA IRRRL HIGH BALANCE FIXED 30";
                default: return string.Empty;
            }
        }
        
    }
}
