/*
 * 
 * 用户： lv
 * 日期: 2012/5/20
 * 时间: 16:51
 * 功能：监测IE URL的方法
 * 测试说明：已测试IE6，IE9的使用，IE7、IE8和IE9的应该相同
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ElecFeePoj
{
	/// <summary>
	/// Description of MonitorIEURL.
	/// </summary>
	/// 
	
	public class MonitorIEURL
	{
		[DllImport("User32.dll")] //User32.dll是Windows操作系统的核心动态库之一
     	static extern int FindWindow(string lpClassName, string lpWindowName);
		[DllImport("User32.dll")]
         static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string lpszClass, string lpszWindow);
    	[DllImport("User32.dll")]
         static extern int GetWindowText(int hwnd,StringBuilder buf, int nMaxCount);
		[DllImport("User32.dll")]
    	static extern int SendMessage(int hWnd, int Msg, int wParam, StringBuilder lParam);
   		const int WM_GETTEXT = 0x000D; 
		//获得文本消息的16进制表示
		
		private MonitorIEURL(){}//构造方法
		
		//获得URL的方法
		public static string GetURL() 
        {
			string ieVesion = GetIEVersion();
			char [] versionChar = ieVesion.ToCharArray();
            int parent = FindWindow("IEFrame", null);
            int child = FindWindowEx(parent, 0, "WorkerW", null);
            child = FindWindowEx(child, 0, "ReBarWindow32", null);
            if(versionChar[0]=='6'){
            	//为IE6时
            	child = FindWindowEx(child, 0, "ComboBoxEx32", null);
            }else if(versionChar[0]=='7' || versionChar[0]=='8' || versionChar[0]=='9'){
            	child = FindWindowEx(child, 0, "Address Band Root", null);
            	child = FindWindowEx(child, 0, "Edit", null);
            }
            StringBuilder buffer = new StringBuilder(1024);
            //child表示要操作窗体的句柄号
            //WM_GETTEXT表示一个消息，怎么样来驱动窗体
            //1024表示要获得text的大小
            //buffer表示获得text的值存放在内存缓存中
            int num = SendMessage(child, WM_GETTEXT, 1024, buffer);
            string URL = buffer.ToString();
            return URL;
        }
		//获得浏览器版本
		public static string GetIEVersion()
		{
			string version;
            using (Microsoft.Win32.RegistryKey versionKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer"))
            {
                 version = versionKey.GetValue("Version").ToString();
            }
           	return version;
		}
	}
}
