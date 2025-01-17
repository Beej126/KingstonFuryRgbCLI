using System;
using System.IO;

namespace FuryControllerService
{
	// Token: 0x0200002D RID: 45
	public class Log
	{
		// Token: 0x06000177 RID: 375 RVA: 0x0000F9A4 File Offset: 0x0000DBA4
		public static void LibLogWriter(string sMessage, bool bFailed = false)
		{
			Log.sLibLogPath = string.Format("{0}\\Log\\Server_{1}.log", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("MM_dd_yyyy"));
			if (!File.Exists(Log.sLibLogPath))
			{
				Directory.CreateDirectory(string.Format("{0}\\Log\\", AppDomain.CurrentDomain.BaseDirectory));
				using (File.Create(Log.sLibLogPath))
				{
					goto IL_00A6;
				}
			}
			FileInfo fileInfo = new FileInfo(Log.sLibLogPath);
			if (fileInfo.Exists && fileInfo.Length > (long)Log.Log_MaxSize)
			{
				fileInfo.Delete();
				using (File.Create(Log.sLibLogPath))
				{
				}
			}
			IL_00A6:
			object liblocker = Log.Liblocker;
			lock (liblocker)
			{
				TextWriter textWriter = TextWriter.Synchronized(File.AppendText(Log.sLibLogPath));
				if (bFailed)
				{
					Console.WriteLine(string.Format("{0} | [ERROR] {1}", DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss.fff"), sMessage));
					textWriter.WriteLine(string.Format("{0} | [ERROR] {1}", DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss.fff"), sMessage));
				}
				else
				{
					Console.WriteLine(string.Format("{0} | {1}", DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss.fff"), sMessage));
					textWriter.WriteLine(string.Format("{0} | {1}", DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss.fff"), sMessage));
				}
				textWriter.Flush();
				textWriter.Dispose();
				textWriter.Close();
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000FB54 File Offset: 0x0000DD54
		public static void LibCmdLogWriter(string sMessage, bool bFailed = false)
		{
			Log.sLibCmdLogPath = string.Format("{0}\\Log\\FuryCtrl_{1}.log", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("MM_dd_yyyy"));
			if (!File.Exists(Log.sLibCmdLogPath))
			{
				Directory.CreateDirectory(string.Format("{0}\\Log\\", AppDomain.CurrentDomain.BaseDirectory));
				using (File.Create(Log.sLibCmdLogPath))
				{
					goto IL_00A6;
				}
			}
			FileInfo fileInfo = new FileInfo(Log.sLibCmdLogPath);
			if (fileInfo.Exists && fileInfo.Length > (long)Log.Log_MaxSize)
			{
				fileInfo.Delete();
				using (File.Create(Log.sLibCmdLogPath))
				{
				}
			}
			IL_00A6:
			object liblocker = Log.Liblocker;
			lock (liblocker)
			{
				TextWriter textWriter = TextWriter.Synchronized(File.AppendText(Log.sLibCmdLogPath));
				if (bFailed)
				{
					Console.WriteLine(string.Format("{0} | [ERROR] {1}", DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss.fff"), sMessage));
					textWriter.WriteLine(string.Format("{0} | [ERROR] {1}", DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss.fff"), sMessage));
				}
				else
				{
					Console.WriteLine(string.Format("{0} | {1}", DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss.fff"), sMessage));
					textWriter.WriteLine(string.Format("{0} | {1}", DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss.fff"), sMessage));
				}
				textWriter.Flush();
				textWriter.Dispose();
				textWriter.Close();
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000FD04 File Offset: 0x0000DF04
		public static void WriteLEDProfile(string sContent)
		{
			if (!File.Exists(Log.sSaveLEDProfilePath))
			{
				Directory.CreateDirectory(string.Format("{0}\\", AppDomain.CurrentDomain.BaseDirectory));
				using (File.Create(Log.sSaveLEDProfilePath))
				{
				}
			}
			try
			{
				object liblocker = Log.Liblocker;
				lock (liblocker)
				{
					File.WriteAllText(Log.sSaveLEDProfilePath, StringEncryptDecrypt.Encrypt(sContent, "3m23s45i599"));
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Write LED profile : {0}", ex.Message), true);
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000FDC4 File Offset: 0x0000DFC4
		public static string ReadLEDProfile()
		{
			string text = "";
			if (File.Exists(Log.sSaveLEDProfilePath))
			{
				try
				{
					object liblocker = Log.Liblocker;
					lock (liblocker)
					{
						text = StringEncryptDecrypt.Decrypt(File.ReadAllText(Log.sSaveLEDProfilePath), "3m23s45i599");
					}
				}
				catch (Exception ex)
				{
					Log.LibCmdLogWriter(string.Format("Read LED profile : {0}", ex.Message), true);
				}
			}
			return text;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000FE4C File Offset: 0x0000E04C
		public static void WriteLEDColorTableCache(string sContent)
		{
			if (!File.Exists(Log.sSaveLEDColorTableCachePath))
			{
				Directory.CreateDirectory(string.Format("{0}\\", AppDomain.CurrentDomain.BaseDirectory));
				using (File.Create(Log.sSaveLEDColorTableCachePath))
				{
				}
			}
			try
			{
				object liblocker = Log.Liblocker;
				lock (liblocker)
				{
					if (Log.sSaveLEDColorTableCache_temp != sContent)
					{
						File.WriteAllText(Log.sSaveLEDColorTableCachePath, StringEncryptDecrypt.Encrypt(sContent, "3m23s45i599"));
						Log.sSaveLEDColorTableCache_temp = sContent;
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Write LED color table cache : {0}", ex.Message), true);
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000FF20 File Offset: 0x0000E120
		public static string ReadLEDColorTableCache()
		{
			string text = "";
			if (File.Exists(Log.sSaveLEDColorTableCachePath))
			{
				try
				{
					object liblocker = Log.Liblocker;
					lock (liblocker)
					{
						text = StringEncryptDecrypt.Decrypt(File.ReadAllText(Log.sSaveLEDColorTableCachePath), "3m23s45i599");
						Log.sSaveLEDColorTableCache_temp = text;
					}
				}
				catch (Exception ex)
				{
					Log.LibCmdLogWriter(string.Format("Read LED color table cache : {0}", ex.Message), true);
				}
			}
			return text;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		public static void WriteLEDBGColorCache(string sContent)
		{
			if (!File.Exists(Log.sSaveLEDBGColorCachePath))
			{
				Directory.CreateDirectory(string.Format("{0}\\", AppDomain.CurrentDomain.BaseDirectory));
				using (File.Create(Log.sSaveLEDBGColorCachePath))
				{
				}
			}
			try
			{
				object liblocker = Log.Liblocker;
				lock (liblocker)
				{
					if (Log.sSaveLEDBGColorCache_temp != sContent)
					{
						File.WriteAllText(Log.sSaveLEDBGColorCachePath, StringEncryptDecrypt.Encrypt(sContent, "3m23s45i599"));
						Log.sSaveLEDBGColorCache_temp = sContent;
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Write LED background color cache : {0}", ex.Message), true);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00010084 File Offset: 0x0000E284
		public static string ReadLEDBGColorCache()
		{
			string text = "";
			if (File.Exists(Log.sSaveLEDBGColorCachePath))
			{
				try
				{
					object liblocker = Log.Liblocker;
					lock (liblocker)
					{
						text = StringEncryptDecrypt.Decrypt(File.ReadAllText(Log.sSaveLEDBGColorCachePath), "3m23s45i599");
						Log.sSaveLEDBGColorCache_temp = text;
					}
				}
				catch (Exception ex)
				{
					Log.LibCmdLogWriter(string.Format("Read LED background color cache : {0}", ex.Message), true);
				}
			}
			return text;
		}

		// Token: 0x0400012C RID: 300
		private static object Liblocker = new object();

		// Token: 0x0400012D RID: 301
		private static string sLibLogPath = string.Format("{0}\\Log\\Server_{1}.log", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("MM_dd_yyyy"));

		// Token: 0x0400012E RID: 302
		private static string sLibCmdLogPath = string.Format("{0}\\Log\\FuryCtrl_{1}.log", AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("MM_dd_yyyy"));

		// Token: 0x0400012F RID: 303
		private static string sSaveLEDProfilePath = string.Format("{0}\\FuryController_profile.log", AppDomain.CurrentDomain.BaseDirectory);

		// Token: 0x04000130 RID: 304
		private static string sSaveLEDColorTableCachePath = string.Format("{0}\\FuryController_colortable_cache.log", AppDomain.CurrentDomain.BaseDirectory);

		// Token: 0x04000131 RID: 305
		private static string sSaveLEDBGColorCachePath = string.Format("{0}\\FuryController_bgcolor_cache.log", AppDomain.CurrentDomain.BaseDirectory);

		// Token: 0x04000132 RID: 306
		private static string sSaveLEDColorTableCache_temp = "";

		// Token: 0x04000133 RID: 307
		private static string sSaveLEDBGColorCache_temp = "";

		// Token: 0x04000134 RID: 308
		private static int Log_MaxSize = 5242880;
	}
}
