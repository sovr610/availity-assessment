using InsuranceCompanySorter.model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCompanySorter
{
    internal class FileScanner
    {
        private string directory;

        public FileScanner(string directory)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("FileScannerLog.txt",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true)
                .CreateLogger();
            this.directory = directory;
        }

        public List<insuranceEntity> scanFiles()
        {
            var entities = new List<insuranceEntity>();
            try
            {
                var fileCollection = Directory.GetFiles(directory);


                foreach (var file in fileCollection)
                {


                    try
                    {
                        var fileContentLines = File.ReadAllLines(file);
                        int index = 0;
                        foreach (var line in fileContentLines)
                        {
                            if (index > 0)
                            {
                                var entity = new insuranceEntity();
                                string[] splitLine = line.Split(',');
                                entity.UserId = splitLine[0];

                                //var splitter = splitLine[1].Split(' ');

                                entity.FirstName = splitLine[1].Split(' ')[1];
                                entity.LastName = splitLine[1].Split(' ')[2];
                                entity.version = Convert.ToInt32(splitLine[2]);
                                entity.insuranceCompany = splitLine[3];
                                entities.Add(entity);
                            }
                            index++;

                        }
                    }
                    catch (Exception i)
                    {
                        Log.Error(i, "There was an issue with reading the file: " + file);
                    }
                }
            } catch (DirectoryNotFoundException x)
            {
                Log.Error(x, "Could not find directory");
            }

            return entities;
        }

        public void generateInsuranceFiles(Dictionary<string,List<insuranceEntity>> data)
        {
            if (!Directory.Exists(directory + "\\organizedDocuments"))
            {
                Directory.CreateDirectory(directory + "\\organizedDocuments");
            }
            
            foreach (var entity in data)
            {
                var insurance = entity.Key;
                var individuals = entity.Value;

                string cleanFileName = insurance.Trim().Replace(" ", "-").Replace(",", "");
                string file = directory + "\\organizedDocuments\\" + cleanFileName + ".csv";

                if (File.Exists(file))
                {
                    Log.Warning("the insurance: " + insurance + " file already exists. Tried to generate csv file: " + file);
                }
                else
                {

                    try
                    {
                        File.WriteAllLines(file, new string[] { "User Id, FullName, version" });

                        foreach (var dataLine in individuals)
                        {
                            File.AppendAllLines(file, new string[] { dataLine.UserId.Trim() + "," + dataLine.FirstName + " " + dataLine.LastName + "," + dataLine.version });
                        }
                    }
                    catch (Exception i)
                    {
                        Log.Error(i, "Had an issue with generating file with insurance: " + insurance);
                    }
                }
            }
        }
    }
}
