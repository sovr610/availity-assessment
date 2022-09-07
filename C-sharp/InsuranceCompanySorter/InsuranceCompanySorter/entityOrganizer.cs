using InsuranceCompanySorter.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCompanySorter
{
    internal class entityOrganizer
    {
        public Dictionary<string, List<insuranceEntity>> sortEntitiesByInsurance(List<insuranceEntity> entities)
        {
            List<insuranceEntity> distinctInsurance = entities
                .GroupBy(p => p.insuranceCompany)
                .Select(g => g.First())
                .ToList();
            List<string> insurances = new List<string>();

            foreach(var insurance in distinctInsurance)
            {
                insurances.Add(insurance.insuranceCompany);
            }

            Dictionary<string, List<insuranceEntity>> orderedEntities = new Dictionary<string, List<insuranceEntity>>();

            foreach (var insurance in insurances)
            {
                var insuranceBasedList = from a in entities
                                         where a.insuranceCompany == insurance
                                         select a;
                if (insuranceBasedList.Any())
                {
                    var groupedInsuranceList = insuranceBasedList.ToList();
                    orderedEntities.Add(insurance, groupedInsuranceList);
                }
            }

            Dictionary<string, List<insuranceEntity>> antiDuplicateEntities = new Dictionary<string, List<insuranceEntity>>();

            foreach (var insurance in insurances)
            {
                List<insuranceEntity> newEntities = new List<insuranceEntity>();
                var element = orderedEntities[insurance];
                var duplicates = element.GroupBy(s => s.UserId)
                    .Where(a => a.Count() > 1)
                    .Select(i => i.Key);
                if(duplicates.Any())
                {
                    List<int> removeVersions = new List<int>();


                    foreach(var dupe in duplicates)
                    {
                        var duplicateIds = from a in element
                                           where a.UserId == dupe
                                           select a;
                        int highestVersion = 0;
                        foreach(var duplicateId in duplicateIds)
                        {
                            if(highestVersion == 0)
                            {
                                highestVersion = duplicateId.version;
                            }
                            else
                            {
                                if(duplicateId.version > highestVersion)
                                {
                                    highestVersion = duplicateId.version;
                                }
                            }
                        }

                        var removeDuplicates = from a in duplicateIds
                                               where a.version != highestVersion && a.UserId == dupe
                                               select a;

                        if(removeDuplicates.Any())
                        {
                            foreach(var duplicate in removeDuplicates)
                            {
                                newEntities.Add(duplicate);
                            }
                        }

                        Console.WriteLine("highest version: " + highestVersion);
                    }
                } 
                else
                {
                    antiDuplicateEntities.Add(insurance, element);
                }

                if (newEntities.Any())
                {
                    foreach(var removeEntity in newEntities)
                    {
                        element.Remove(removeEntity);
                    }

                    antiDuplicateEntities.Add(insurance, element);
                
                }
            }



            return orderedEntities;
        }

        /// <summary>
        /// order the collection of insurances with people ordered by lastname and firstname
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Dictionary<string, List<insuranceEntity>> orderEnroleesByName(Dictionary<string,List<insuranceEntity>> entities)
        {
            Dictionary<string, List<insuranceEntity>> orderedEntities = new Dictionary<string, List<insuranceEntity>>();
            foreach(var entity in entities)
            {
                string key = entity.Key;
                var orderedList = entity.Value.OrderBy(i=>i.LastName).ThenBy(i=>i.FirstName);
                orderedEntities.Add(key, orderedList.ToList());
            }

            return orderedEntities;
        }
    }
}
