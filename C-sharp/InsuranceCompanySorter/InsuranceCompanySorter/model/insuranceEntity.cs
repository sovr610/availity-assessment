using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCompanySorter.model
{
    internal class insuranceEntity
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int version { get; set; }
        public string insuranceCompany { get; set; }
    }
}
