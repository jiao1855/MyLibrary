using HSBC.INS.Utils.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSBC.INS.Utils.Pcomm
{
    public class PcommAccessor : IDisposable
    {
        private dynamic autECLConnListObj;
        private dynamic autECLPSObj;
        private dynamic autECLOIAObj;

        private string sessionName;
        public string SessionName
        {
            get { return sessionName; }
        }

        public PcommAccessor(string sessionName)
        {
            this.sessionName = sessionName;
            string pcommWindowTitle = string.Format("Session {0} - [24 x 80]", sessionName);
            User32.BringWindowToFront(pcommWindowTitle);

            autECLConnListObj = Activator.CreateInstance(Type.GetTypeFromProgID("PCOMM.autECLConnList"));
            autECLPSObj = Activator.CreateInstance(Type.GetTypeFromProgID("PCOMM.autECLPS"));
            autECLOIAObj = Activator.CreateInstance(Type.GetTypeFromProgID("PCOMM.autECLOIA"));
            autECLConnListObj.Refresh();
            autECLPSObj.SetConnectionByName(sessionName);
            autECLOIAObj.SetConnectionByName(sessionName);
        }

        /// <summary>
        /// 判断当前Pcomm屏幕是否可用。
        /// WaitForAppAvailable：在timeOut时间内等待，直到连接的Pcomm程序可以使用
        /// WaitForInputReady：在timeOut时间内等待，直到连接的Pcomm屏幕可以接收键盘输入
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public bool WaitForScreenAvailable(int timeOut = 10000)
        {
            return autECLOIAObj.WaitForAppAvailable(timeOut) && autECLOIAObj.WaitForInputReady(timeOut);
        }

        /// <summary>
        /// 连续按 F3，跳回到主屏，之后执行相关操作
        /// </summary>
        /// <param name="action"></param>
        /// <param name="scrNo">屏号，主屏默认是 S0017</param>
        /// <returns></returns>
        public PcommAccessor SkipToHomeScreen(Action action, string scrNo = "S0017")
        {
            return SkipToHomeScreen(new ScreenDesc(scrNo), action);
        }

        /// <summary>
        /// 连续按 F3，跳回到主屏，之后执行相关操作
        /// </summary>
        /// <param name="homeScrDesc"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public PcommAccessor SkipToHomeScreen(ScreenDesc homeScrDesc, Action action)
        {
            return SkipToScreen(homeScrDesc, action, 1000, Keys.PF3);
        }

        /// <summary>
        /// 为进入某个屏，连接按 key 键
        /// </summary>
        /// <param name="scrNo"></param>
        /// <param name="key">默认：Enter</param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public PcommAccessor SkipToScreen(string scrNo, Action action, int timeOut = 2000, string key = Keys.Enter, int retryCount = 10)
        {
            return SkipToScreen(new ScreenDesc(scrNo), action, timeOut, key, retryCount);
        }

        /// <summary>
        /// 为进入某个屏，连接按 key 键
        /// </summary>
        /// <param name="scrDesc"></param>
        /// <param name="key"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public PcommAccessor SkipToScreen(ScreenDesc scrDesc, Action action, int timeOut = 2000, string key = Keys.Enter, int retryCount = 10)
        {
            Wait(500);
            while (retryCount > 0 && !WaitForScreen(scrDesc, timeOut))
            {
                SendKeys(key);
                //按完键等1秒
                Wait(500);

                
                string errorTxt = GetText(24, 2, 50).Trim();
                if (!string.IsNullOrWhiteSpace(errorTxt))
                {
                    foreach (string err in errorMsgs)
                    {
                        if(errorTxt.StartsWith(err))
                            throw new Exception(errorTxt);
                    }                    
                }

                retryCount--;
                if (retryCount == 0)
                    throw new Exception("can not find the " + scrDesc.ScrNo + " screen");
            }
            WaitForScreenAvailable();
            action.Invoke();
            return this;
        }

        /// <summary>
        /// 为进入某个屏，连接按 key 键
        /// </summary>
        /// <param name="scrDesc"></param>
        /// <param name="key"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public PcommAccessor SkipToHomeScreenByTitle(string homeScrTitle, Action action, int timeOut = 2000, int retryCount = 10)
        {
            return SkipToScreenByTitle(homeScrTitle, action, timeOut, Keys.PF3, retryCount);
        }

        /// <summary>
        /// 为进入某个屏，连接按 key 键
        /// </summary>
        /// <param name="scrDesc"></param>
        /// <param name="key"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public PcommAccessor SkipToScreenByTitle(string scrTitle, Action action, int timeOut = 2000, string key = Keys.Enter, int retryCount = 10)
        {
            Wait(500);
            string title = GetTextRect(1, 1, 1, 80).Trim();
            while (retryCount > 0 && !scrTitle.Equals(title))
            {
                SendKeys(key);
                Wait(500);

                //string errorTxt = GetText(24, 2, 50).Trim();
                //if (!string.IsNullOrWhiteSpace(errorTxt))
                //{
                //    foreach (string err in errorMsgs)
                //    {
                //        if (errorTxt.StartsWith(err))
                //            throw new Exception(errorTxt);
                //    }
                //}

                retryCount--;
                if (retryCount == 0)
                    throw new Exception("can not find the " + scrTitle + " screen");
            }
            WaitForScreenAvailable();
            action.Invoke();
            return this;
        }

        public static List<string> errorMsgs = new List<string>
        {
            "No details found",
            "Err in Operating System",
            "Same groupmbr client",
            "Warn: FCCRM country"
        };

        public Position GetCursorPosition()
        {
            return new Position() { row = autECLPSObj.CursorPosRow, col = autECLPSObj.CursorPosCol };
        }

        /// <summary>
        /// 在指定位置设置焦点
        /// </summary>
        /// <param name="targetRow"></param>
        /// <param name="targetCol"></param>
        public void SetCursorOnPos(int targetRow, int targetCol)
        {
            Position currentPosition = GetCursorPosition();

            int startRow = currentPosition.row;
            int startCol = currentPosition.col;
            int endRow = targetRow;
            int endCol = targetCol;
            int row = endRow - startRow;
            int col = endCol - startCol;

            for (int i = 0; i < Math.Abs(row); i++)
            {
                if (row > 0)
                    SendKeys(Keys.Down);
                else if (row < 0)
                    SendKeys(Keys.Up);
            }
            for (int j = 0; j < Math.Abs(col); j++)
            {
                if (col > 0)
                    SendKeys(Keys.Right);
                else if (col < 0)
                    SendKeys(Keys.Left);
            }
        }

        /// <summary>
        /// 从指定位置(row,col)开始向后查找相应文本，如果找到，在此文本的位置设置焦点，且返回 True,否则，返回False
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public bool SetCursorOnText(string text, int row = 1, int col = 1)
        {
            Position? position = GetPositionOfText(text,row,col);
            if (position.HasValue)
            {
                SetCursorOnPos(position.Value.row, position.Value.col);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 在整个屏幕中查找是否有相应的文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool SearchText(String text)
        {
            return autECLPSObj.SearchText(text);
        }

        /// <summary>
        ///  从指定位置(row,col)向后查找屏幕中是否有相应的文本，找到返回True，且返回文本对应在位置，找不到返回False。
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public bool SearchText(String text, ref int row, ref int col)
        {
            return autECLPSObj.SearchText(text, 1, ref row, ref col);
        }

        /// <summary>
        /// 从指定位置(row,col)开始向后查找指定文本的位置，找不到返回 null
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public Position GetPositionOfText(string text, int row = 1, int col = 1)
        {
            if (SearchText(text, ref row, ref col))
                return new Position() { row = row, col = col };
            else
                throw new Exception("can not find " + text + " in screen");
        }

        /// <summary>
        /// 发送 Enter键
        /// </summary>
        public void SendEnterKey()
        {
            //Wait(3000);
            SendKeys(Keys.Enter);
        }

        /// <summary>
        /// 默认向当前焦点位置发送 key,如果指定了(row,col)，就向此位置发送 key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void SendKeys(String key, int row = -1, int col = -1)
        {
            if (row == -1 || col == -1)
                autECLPSObj.SendKeys(key);
            else
                autECLPSObj.SendKeys(key, row, col);
        }
        
        /// <summary>
        /// 获取指定位置指定长度的文本
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public string GetText(int row, int col, int len)
        {
            return autECLPSObj.GetText(row, col, len);
        }

        /// <summary>
        /// 获取指定矩形区域内的文本
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="endRow"></param>
        /// <param name="endCol"></param>
        /// <returns></returns>
        public string GetTextRect(int startRow, int startCol, int endRow, int endCol)
        {
            return autECLPSObj.GetTextRect(startRow, startCol, endRow, endCol);
        }

        /// <summary>
        /// 在指定位置(row,col)设置文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void SetText(string text, int row, int col)
        {
            autECLPSObj.SetText(text, row, col);
        }

        /// <summary>
        /// 在指定矩形区域内设置文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="endRow"></param>
        /// <param name="endCol"></param>
        /// <returns></returns>
        public string SetTextRect(string text, int startRow, int startCol, int endRow, int endCol)
        {
            return autECLPSObj.SetTextRect(text, startRow, startCol, endRow, endCol);
        }

        /// <summary>
        /// 删除指定位置指定长度的文本
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="length"></param>
        public void DeleteText(int startRow, int startCol, int length)
        {
            SetText(string.Format("{0,-" + length + "}", " "), startRow, startCol);
        }

        /// <summary>
        /// 等待多少毫秒
        /// </summary>
        /// <param name="milliseconds"></param>
        public void Wait(int milliseconds)
        {
            autECLPSObj.Wait(milliseconds);
        }

        /// <summary>
        /// 在指定超时值内等待屏幕直到可用
        /// </summary>
        /// <param name="screenDesc"></param>
        /// <param name="timeOut">单位：毫秒</param>
        /// <returns></returns>
        public bool WaitForScreen(ScreenDesc screenDesc, int timeOut = 10000)
        {
            return autECLPSObj.WaitForScreen(screenDesc.AutECLScreenDescObj, timeOut);
        }

        public void Dispose()
        {
            autECLOIAObj = null;
            autECLPSObj = null;
            autECLConnListObj = null;
        }
    }

    public struct Position
    {
        public int row;
        public int col;
    }
}
