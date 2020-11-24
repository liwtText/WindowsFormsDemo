using BankInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfChina
{
    //[Export(typeof(ICard))]
    [ExportCard(CardType = "BankOfChina")]
    public class ZHCard : ICard
    {
        public string GetCountInfo()
        {
            return "中国银行";
        }

        public void SaveMoney(double money)
        {
            this.Money += money;
        }

        public void CheckOutMoney(double money)
        {
            this.Money -= money;
        }

        public double Money { get; set; }
    }
}
