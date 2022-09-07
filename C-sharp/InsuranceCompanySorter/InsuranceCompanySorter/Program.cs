using System;

namespace InsuranceCompanySorter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("please enter in the directory of where the csv files are located: ");
            string dir = Console.ReadLine();
            FileScanner scanner = new FileScanner(dir);

            var data = scanner.scanFiles();

            entityOrganizer organize = new entityOrganizer();
            var collectedData = organize.sortEntitiesByInsurance(data);
            var orderedData = organize.orderEnroleesByName(collectedData);
            scanner.generateInsuranceFiles(orderedData);
            Console.ReadLine();
        }
    }
}   
