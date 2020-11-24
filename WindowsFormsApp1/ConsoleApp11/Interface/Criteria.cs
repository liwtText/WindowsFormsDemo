using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp11.Interface
{
    public interface Criteria
    {
        public List<Person> meetCriteria(List<Person> persons);
    }
}
