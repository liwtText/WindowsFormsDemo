using ConsoleApp11.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    public class CriteriaSingle : Criteria
    {
        public List<Person> meetCriteria(List<Person> persons)
        {
            List<Person> singlePersons = new List<Person>();
            foreach (Person item in persons)
            {
                if (item.getMaritalStatus().Equals("Single"))
                {
                    singlePersons.Add(item);
                }
            }
            return singlePersons;
        }
    }
}
