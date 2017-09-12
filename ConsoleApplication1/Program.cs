using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Azure; // Namespace for Azure Configuration Manager
using Microsoft.WindowsAzure.Storage; // Namespace for Storage Client Library
using Microsoft.WindowsAzure.Storage.File; // Namespace for Azure File storage

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

            if (share.Exists())
            {
                // Get a reference to the root directory for the share.
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();

                // Get a reference to the directory we created previously.
                CloudFileDirectory sampleDir = rootDir.GetDirectoryReference("Inbound");

                // Ensure that the directory exists.
                if (sampleDir.Exists())
                {
                    // Get a reference to the file we created previously.
                    CloudFile file = sampleDir.GetFileReference("File1.txt");

                    // Ensure that the file exists.
                    if (file.Exists())
                    {
                        // Write the contents of the file to the console window.
                        Console.WriteLine(file.DownloadTextAsync().Result);
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}

