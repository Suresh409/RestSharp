using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Model.Response
{
    class Employee
    {
      

        public string status { get; set; }
        public string message { get; set; }
  
        public Data data { get; set; }

        public class Data
        {
            public int id { get; set; }

            public string employee_name { get; set; }

            public int employee_salary { get; set; }

            public int employee_age { get; set; }

            public string profile_image { get; set; }
        }
        }

    
}
