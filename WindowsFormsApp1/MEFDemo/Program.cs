using BankInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace MEFDemo
{
    class Program
    {
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<ICard, IMetaData>> cards { get; set; }
        //[ImportMany(typeof(ICard))]
        //public IEnumerable<ICard> cards { get; set; }
        // [Import]
        //public Interface1 Service { get; set; }
        static void Main(string[] args)
        {
            Program pro = new Program();
            pro.Compose();
            foreach (var c in pro.cards)
            {
                if (c.Metadata.CardType == "BankOfChina")
                {
                    Console.WriteLine("这是中国银行卡");
                    Console.WriteLine(c.Value.GetCountInfo());
                }
                else if (c.Metadata.CardType == "NongHang")
                {
                    Console.WriteLine("这是农行卡");
                    Console.WriteLine(c.Value.GetCountInfo());
                }
            }
            //foreach (var c in pro.cards)
            //{
            //    Console.WriteLine(c.GetCountInfo());
            //}
            //if (pro.Service != null)
            //{
            //    Console.WriteLine(pro.Service.GetBookName());
            //}
            Console.Read();
        }
        private void Compose()
        {
            string url = System.IO.Directory.GetCurrentDirectory();
            var catalog = new DirectoryCatalog(url+"/Cards");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        //private void Compose()
        //{
        //    var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());//反射
        //    CompositionContainer container = new CompositionContainer(catalog);
        //    container.ComposeParts(this);
        //}
    }
}
