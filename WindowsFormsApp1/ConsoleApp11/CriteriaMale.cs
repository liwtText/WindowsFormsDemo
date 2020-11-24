using ConsoleApp11.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11
{
    public class CriteriaMale : Criteria
    {
        public List<Person> meetCriteria(List<Person> persons)
        {
            List<Person> malePersons = new List<Person>();
            foreach (Person item in persons)
            {
                if (item.getGender().Equals("Male"))
                {
                    malePersons.Add(item);
                }
            }
            return malePersons;
        }
    }
}
