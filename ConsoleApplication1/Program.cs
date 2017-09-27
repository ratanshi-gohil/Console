using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Azure; // Namespace for Azure Configuration Manager
using Microsoft.WindowsAzure.Storage; // Namespace for Storage Client Library
using Microsoft.WindowsAzure.Storage.File; // Namespace for Azure File storage
using System.IO;
using Excel;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Action3();
        }

        private static void Action2()
        {
            using (var db = new SQLDBContext())
            {
                try
                {
                    Console.WriteLine("Enter Company NMLSID");
                    var id = Console.ReadLine();

                    var company = db.Companies.Where(c => c.NmlsCompanyId == id).FirstOrDefault();
                    Console.WriteLine("NMLSID | CompanyName \n {0}  |   {1}", company.NmlsCompanyId,
                            company.CompanyName);

                    Console.ReadLine();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }

            }
        }
        private static void Action1()
        {
            SqlConnection con;
            SqlDataReader reader;
            try
            {
                int id;
                con = new SqlConnection(@"Data Source=(LocalDb)\v11.0;Initial Catalog=OLIE2Context;Integrated Security=True; MultipleActiveResultSets=True");
                con.Open();
                Console.WriteLine("Enter Company NMLSID");
                id = int.Parse(Console.ReadLine());
                reader = new SqlCommand("select * from dbo.Company where nmlscompanyid='" + id + "'", con).ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("NMLSID | CompanyName \n {0}  |   {1}", reader.GetInt32(0),
                        reader.GetString(1));
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Action3()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("dev-file-share");
            try
            {
                if (share.Exists())
                {
                    CloudFileDirectory rootDir = share.GetRootDirectoryReference();
                    CloudFileDirectory reportDir = rootDir.GetDirectoryReference("Report");
                    CloudFile reportFile = reportDir.GetFileReference("abc.txt");
                    Stream stream = new MemoryStream();

                    //Write to stream
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < 50; j++)
                        {
                            stream.WriteByte(65);
                        }
                    }
                    stream.Position = 0;

                    reportFile.UploadFromStream(stream);

                    //// Get a reference to the root directory for the share.
                    //CloudFileDirectory rootDir = share.GetRootDirectoryReference();

                    //// Get a reference to the directory we created previously.
                    //CloudFileDirectory sampleDir = rootDir.GetDirectoryReference("Inbound");

                    //// Ensure that the directory exists.
                    //if (sampleDir.Exists())
                    //{
                    //    // Get a reference to the file we created previously.
                    //    CloudFile srcfile = sampleDir.GetFileReference("MILend WS Template 8.30.17.xlsx");

                    //    // Ensure that the file exists.
                    //    if (srcfile.Exists())
                    //    {

                    //        CloudFileDirectory archiveDir = rootDir.GetDirectoryReference("Archive");
                    //        CloudFile destFile = archiveDir.GetFileReference("MILend WS Template 8.30.17.xlsx");
                    //        destFile.StartCopy(srcfile);


                    //        ////Download cloud file onto memory stream
                    //        //Stream memoryStream = new MemoryStream();
                    //        //file.DownloadToStream(memoryStream);

                    //        //using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(memoryStream))
                    //        //{
                    //        //    excelReader.IsFirstRowAsColumnNames = true;
                    //        //    DataSet dataset = excelReader.AsDataSet();
                    //        //    if (dataset.Tables.Count == 0)
                    //        //        throw new InvalidDataException(string.Format("No WorkSheet available."));
                    //        //}
                    //        // Write the contents of the file to the console window.
                    //        //Console.WriteLine(memoryStream.ToString());
                    //        Console.ReadLine();
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}

