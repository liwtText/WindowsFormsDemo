using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp5.Abstract
{
    public abstract class Shape : ICloneable
    {
        private String id;
        protected String type;


        public String getType()
        {
            return type;
        }

        public String getId()
        {
            return id;
        }

        public void setId(String id)
        {
            this.id = id;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }


        //public Object Clone()
        //{
        //    Object clone  Shape;

        //    return clone;
        //}

    }

}
