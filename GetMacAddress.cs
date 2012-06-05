/*
 * 
 * 用户： lv
 * 日期: 2012/5/24
 * 时间: 8:49
 * 
 */
using System;
using System.Management;
namespace ElecFeePoj
{
	/// <summary>
	/// Description of GetMacAddress.
	/// </summary>
	public class GetMacAddress
	{
		private GetMacAddress()
		{
		}
		public static string getMacAddress(){
			 ManagementClass mc;     
			 ManagementObjectCollection moc;
			 mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
			 moc = mc.GetInstances();     
			 string str = "";
			 foreach (ManagementObject mo in moc)     
			 {         
			 	if ((bool)mo["IPEnabled"] == true)         
			 	{            
			 		str = mo["MacAddress"].ToString();
			 		break;
			 	}    
			 }
			 return str;
		}
	}
}
