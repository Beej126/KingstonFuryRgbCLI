using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace KingstonFuryRgbCLI
{
	// Token: 0x02000008 RID: 8
	public class KeepData
	{
		// Token: 0x06000051 RID: 81
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		// Token: 0x06000052 RID: 82
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		// Token: 0x06000053 RID: 83 RVA: 0x00002A00 File Offset: 0x00000C00
		public static void KeepDataWriter(string sOption, string sData)
		{
			KeepData.KeepData_FilePath = string.Format("{0}{1}\\{2}.txt", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "\\Kingston Fury\\FuryCTRL\\", "FuryCTRL");
			if (!File.Exists(KeepData.KeepData_FilePath))
			{
				Directory.CreateDirectory(string.Format("{0}{1}\\", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "\\Kingston Fury\\FuryCTRL\\"));
				using (File.Create(KeepData.KeepData_FilePath))
				{
				}
			}
			KeepData.WritePrivateProfileString("KeepData", sOption, sData, KeepData.KeepData_FilePath);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A90 File Offset: 0x00000C90
		public static string KeepDataReader(string sOption)
		{
			KeepData.KeepData_FilePath = string.Format("{0}{1}\\{2}.txt", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "\\Kingston Fury\\FuryCTRL\\", "FuryCTRL");
			string text = "";
			if (File.Exists(KeepData.KeepData_FilePath))
			{
				StringBuilder stringBuilder = new StringBuilder(10240);
				KeepData.GetPrivateProfileString("KeepData", sOption, "", stringBuilder, 10240, KeepData.KeepData_FilePath);
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0400000F RID: 15
		public static string KeepData_FilePath = string.Format("{0}{1}\\{2}.txt", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "\\Kingston Fury\\FuryCTRL\\", "FuryCTRL");
	}
}
