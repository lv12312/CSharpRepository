/*
 * 
 * 用户： lv
 * 日期: 2012/5/21
 * 时间: 9:48
 * 
 */
using System;
using MSHTML;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.InteropServices;
using SHDocVw;
namespace ElecFeePoj
{
	/// <summary>
	/// 功能：监控IE浏览器里面的内容
	/// </summary>
	public class MonitorIEContent
	{
		private MonitorIEContent(){}
		
		//获得IE里面的URL和需要的信息
		public static string getIEContent(){
			Hashtable hashtable = new Hashtable();
			string userNum="blank";
			SHDocVw.ShellWindows shellWindow = new SHDocVw.ShellWindowsClass();
 			string filename;
 			foreach (SHDocVw.InternetExplorer ie in shellWindow){
 				filename= Path.GetFileNameWithoutExtension(ie.FullName).ToLower();
 				if (filename.Equals("iexplore")){
 					try{
 						MSHTML.IHTMLDocument2 htmlDoc = ie.Document as MSHTML.IHTMLDocument2;
 						IHTMLInputElement input =null;
 						if(htmlDoc!=null){
							input = (IHTMLInputElement)htmlDoc.all.item("CONS_NO",0); // 获取指定名称的对象
 						}
            			if(input != null ){
							string temp = input.value + "0";
							if(temp.Length>=5){
								userNum = input.value;
							}
 						}
 					}
 					catch(COMException e){
						//不做处理
						Console.WriteLine(e.Message);
 					}
 				}
			}
 			if(userNum.Equals("blank")){
// 				NotificationIcon.userNumCheck="initialStr";
 			}
 			return userNum;
		}
		//获得IE里面的URL和需要的信息
		public static string getIEContentDetail(){
			Hashtable hashtable = new Hashtable();
			string userNum="blank";
			SHDocVw.ShellWindows shellWindow = new SHDocVw.ShellWindowsClass();
 			string filename;
 			foreach (SHDocVw.InternetExplorer ie in shellWindow){
 				filename= Path.GetFileNameWithoutExtension(ie.FullName).ToLower();
 				if (filename.Equals("iexplore")){
 					try{
 						MSHTML.IHTMLDocument2 htmlDoc = ie.Document as MSHTML.IHTMLDocument2;
 						//写入参数
						FileStream fileStream = new FileStream(Application.StartupPath +"\\html.ini", FileMode.OpenOrCreate, FileAccess.ReadWrite);
						StreamWriter oputfile = new StreamWriter(fileStream);
						oputfile.WriteLine(htmlDoc.body.outerHTML);
						oputfile.Close();
						fileStream.Close();
 						IHTMLInputElement input =null;
 						if(htmlDoc!=null){
							input = (IHTMLInputElement)htmlDoc.all.item("CONS_NO",0); // 获取指定名称的对象
 						}
            			if(input != null ){
							string temp = input.value + "0";
							if(temp.Length>=5){
								userNum = input.value;
							}
 						}
 					}
 					catch(COMException e){
						//不做处理
						Console.WriteLine(e.Message);
 					}
 				}
			}
 			if(userNum.Equals("blank")){
// 				NotificationIcon.userNumCheck="initialStr";
 			}
 			return userNum;
		}		
	}				
	
}
