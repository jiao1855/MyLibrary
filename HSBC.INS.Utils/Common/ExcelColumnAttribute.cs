using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSBC.INS.Utils.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ExcelColumnAttribute : Attribute
    {
        public string Column { get; set; }
        public ExcelColumnAttribute(string column)
        {
            this.Column = column;
        }
    }
}
