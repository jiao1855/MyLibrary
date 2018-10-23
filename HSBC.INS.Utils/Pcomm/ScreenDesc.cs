using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSBC.INS.Utils.Pcomm
{
    public class ScreenDesc
    {
        private dynamic autECLScreenDescObj;
        public dynamic AutECLScreenDescObj
        {
            get { return autECLScreenDescObj; }
        }
        public ScreenDesc()
        {
            autECLScreenDescObj = Activator.CreateInstance(Type.GetTypeFromProgID("PCOMM.autECLScreenDesc"));
        }

        private string scrNo;
        public string ScrNo
        {
            get { return scrNo; }
        }

        public ScreenDesc(string scrNo, int sRow = 1, int sCol = 72, int eRow = 1, int eCol = 76)
            : this()
        {
            this.scrNo = scrNo;
            AddStringInRect(scrNo, sRow, sCol, eRow, eCol);
        }

        public void AddString(string str, int row, int col)
        {
            autECLScreenDescObj.AddString(str, row, col);
        }

        public void AddStringInRect(string str, int sRow, int sCol, int eRow, int eCol)
        {
            autECLScreenDescObj.AddStringInRect(str, sRow, sCol, eRow, eCol);
        }
    }
}
