using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HSBC.INS.Utils.Common
{
    public class User32
    {
        /// <summary>
        /// 该函数获得一个顶层窗口的句柄，该窗口的类名和窗口名与给定的字符串相匹配。这个函数不查找子窗口。在查找时不区分大小写。
        /// </summary>
        /// <param name="lpClassName">要找的窗口的类名</param>
        /// <param name="lpWindowName">要找的窗口的标题</param>
        /// <returns></returns>
        [DllImport("user32.dll")]  
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName); 

        /// <summary>
        /// 该函数改变一个子窗口，弹出式窗口式顶层窗口的尺寸，位置和Z序。子窗口，弹出式窗口，及顶层窗口根据它们在屏幕上出现的顺序排序、顶层窗口设置的级别最高，并且被设置为Z序的第一个窗口。
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        //hWndInsertAfter:
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        //uFlags
        private static readonly uint SWP_NOSIZE = 0x0001;
        private static readonly uint SWP_NOMOVE = 0x0002;
        private static readonly uint SWP_SHOWWINDOW = 0x0040;

        /// <summary>
        /// 将程序窗口设置到最前
        /// </summary>
        public static void BringWindowToFront(string windowTitle)
        {
            IntPtr hWnd = FindWindow(null, windowTitle);
            if (hWnd.ToInt32() > 0)
            {
                SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
                SetWindowPos(hWnd, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
            }            
        }
    }
}
