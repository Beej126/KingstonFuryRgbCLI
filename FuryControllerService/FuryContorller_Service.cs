using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskScheduler;
using WebSocketSharp.Server;

namespace FuryControllerService
{
	// Token: 0x0200002F RID: 47
	public class FuryContorller_Service : ServiceBase
	{
		// Token: 0x06000182 RID: 386 RVA: 0x000102BC File Offset: 0x0000E4BC
		public FuryContorller_Service()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000102D5 File Offset: 0x0000E4D5
		protected override void OnStart(string[] args)
		{
			Log.LibCmdLogWriter("OnStart", false);
			Task.Factory.StartNew(delegate
			{
				try
				{
					Log.LibCmdLogWriter("Initialize start.", false);
					FuryContorller_Service.IntPtr_DRAM = this.class_mutex.CreateMutex("Global\\Access_DRAM", false);
					Class_DLL.InitialSMBusDriver();
					FuryContorller_Service.bIs8Dimm = Class_DLL.Is8Dimm();
					Miscellaneous.bIs8Dimm = FuryContorller_Service.bIs8Dimm;
					Log.LibCmdLogWriter(string.Format("Special DIMM : {0}", FuryContorller_Service.bIs8Dimm ? "true" : "false"), false);
					FuryContorller_Service.bIsAM4 = Class_DLL.IsAM4();
					Log.LibCmdLogWriter(string.Format("AM4 CPU : {0}", FuryContorller_Service.bIsAM4 ? "true" : "false"), false);
					this.bIsX299 = Class_DLL.IsX299();
					Log.LibCmdLogWriter(string.Format("X299 series : {0}", this.bIsX299 ? "true" : "false"), false);
					bool flag = this.CheckExpireTime();
					if (flag)
					{
						this.CheckDramSupport();
					}
					Log.LibCmdLogWriter("Create dram led control object start.", false);
					FuryContorller_Service.KSDramSMBusCtrl = new KSDRAMSMBusCtrlObj(FuryContorller_Service.dDRAMInfos, flag);
					string text = KeepData.KeepDataReader("bKeep");
					if (!string.IsNullOrEmpty(text))
					{
						FuryContorller_Service.bKeep = text.ToLowerInvariant() == "true";
					}
					this.KeepDramLEDEffectFeature();
					Log.LibCmdLogWriter("WebSocket server start.", false);
					this.ws = new WebSocketServer("ws://127.0.0.1:55599");
					this.ws.AddWebSocketService<NotifyBehavior>("/");
					this.ws.Start();
					this.AppxPackageAvailableEvent();
				}
				catch (Exception)
				{
				}
			});
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000102FC File Offset: 0x0000E4FC
		protected override void OnStop()
		{
			Log.LibCmdLogWriter("OnStop", false);
			object onPowerEvent_locker = FuryContorller_Service.OnPowerEvent_locker;
			lock (onPowerEvent_locker)
			{
				Log.LibCmdLogWriter("*********** OnStop Feature START ***********", false);
				try
				{
					if (!this.bSuspend)
					{
						this.bSuspend = true;
						if (FuryContorller_Service.iAPIVer == 2 || FuryContorller_Service.iAPIVer == 4)
						{
							FuryContorller_Service.KSDramSMBusCtrl.setDRAMDDR5_DefaultRainbow();
						}
						else if (FuryContorller_Service.iAPIVer == 3)
						{
							FuryContorller_Service.KSDramSMBusCtrl.setDRAMDDR5_DefaultRacing();
						}
					}
				}
				catch (Exception ex)
				{
					Log.LibCmdLogWriter(string.Format("OnShutdown Feature. ({0})", ex.Message), true);
				}
				Log.LibCmdLogWriter("*********** OnStop Feature END ***********", false);
			}
			try
			{
				if (FuryContorller_Service._mutex != null)
				{
					FuryContorller_Service._mutex.ReleaseMutex();
					FuryContorller_Service._mutex.Close();
					FuryContorller_Service._mutex.Dispose();
				}
			}
			catch (Exception ex2)
			{
				Log.LibCmdLogWriter(string.Format("Release mutex {0}", ex2.Message), true);
			}
			Task.Factory.StartNew(delegate
			{
				try
				{
					try
					{
						Log.LibCmdLogWriter("Stop appxpackage", false);
						FileSystemWatcher appxPackageProgramDataFile_Watcher = this.AppxPackageProgramDataFile_Watcher;
						if (appxPackageProgramDataFile_Watcher != null)
						{
							appxPackageProgramDataFile_Watcher.Dispose();
						}
					}
					catch (Exception ex4)
					{
						Log.LibCmdLogWriter(string.Format("Stop appxpackage {0}", ex4.Message), true);
					}
					try
					{
						Log.LibCmdLogWriter("WebSocket server stop.", false);
						WebSocketServer webSocketServer = this.ws;
						if (webSocketServer != null)
						{
							webSocketServer.Stop();
						}
					}
					catch (Exception ex5)
					{
						Log.LibCmdLogWriter(string.Format("Stop WebSocket {0}", ex5.Message), true);
					}
					Log.LibCmdLogWriter("Release.", false);
					object lib_locker = FuryContorller_Service.Lib_locker;
					lock (lib_locker)
					{
						Class_DLL.ReleaseDll();
					}
				}
				catch (Exception ex6)
				{
					Log.LibCmdLogWriter(string.Format("Stop service {0}", ex6.Message), true);
				}
			}).Wait();
			try
			{
				if (FuryContorller_Service.IntPtr_DRAM != IntPtr.Zero)
				{
					Native.CloseHandle(FuryContorller_Service.IntPtr_DRAM);
				}
			}
			catch (Exception ex3)
			{
				Log.LibCmdLogWriter(string.Format("Release bus mutex {0}", ex3.Message), true);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0001046C File Offset: 0x0000E66C
		protected override void OnShutdown()
		{
			object onPowerEvent_locker = FuryContorller_Service.OnPowerEvent_locker;
			lock (onPowerEvent_locker)
			{
				Log.LibCmdLogWriter("*********** OnShutdown Feature START ***********", false);
				try
				{
					if (!this.bSuspend)
					{
						this.bSuspend = true;
						FuryContorller_Service.KSDramSMBusCtrl.setDRAMDDR5_AllOff();
					}
				}
				catch (Exception ex)
				{
					Log.LibCmdLogWriter(string.Format("OnShutdown Feature. ({0})", ex.Message), true);
				}
				Log.LibCmdLogWriter("*********** OnShutdown Feature END ***********", false);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000104FC File Offset: 0x0000E6FC
		private void AppxPackageAvailableEvent()
		{
			Log.LibCmdLogWriter("Start appxpackage", false);
			string currentUserName = this.GetCurrentUserName();
			if (!string.IsNullOrEmpty(currentUserName) && Directory.Exists("C:\\Users\\" + currentUserName + "\\AppData\\Local\\Packages\\"))
			{
				try
				{
					this.AppxPackageProgramDataFile_Watcher = new FileSystemWatcher("C:\\Users\\" + currentUserName + "\\AppData\\Local\\Packages\\");
					this.AppxPackageProgramDataFile_Watcher.Deleted += this.AppxPackageProgramDataFile_Watcher_Deleted;
					this.AppxPackageProgramDataFile_Watcher.EnableRaisingEvents = true;
					this.AppxPackageProgramDataFile_Watcher.IncludeSubdirectories = false;
					return;
				}
				catch (Exception ex)
				{
					Log.LibCmdLogWriter(string.Format("GetCurrentUserName {0}", ex.Message), true);
					return;
				}
			}
			Log.LibCmdLogWriter(string.Format("Current name {0}.", currentUserName), false);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000105C0 File Offset: 0x0000E7C0
		private string GetCurrentUserName()
		{
			string text = "";
			int num = 0;
			ManagementObjectSearcher managementObjectSearcher = null;
			while (managementObjectSearcher == null && num <= 15)
			{
				try
				{
					managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT PrimaryOwnerName FROM Win32_ComputerSystem ");
					if (managementObjectSearcher != null)
					{
						try
						{
							using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
							{
								if (enumerator.MoveNext())
								{
									text = ((ManagementObject)enumerator.Current)["PrimaryOwnerName"].ToString();
									return text;
								}
							}
						}
						catch (Exception ex)
						{
							Log.LibCmdLogWriter(string.Format("GetCurrentName {0}", ex.Message), true);
							Thread.Sleep(500);
						}
					}
					break;
				}
				catch (Exception ex2)
				{
					Log.LibCmdLogWriter(string.Format("GetCurrentName {0}", ex2.Message), true);
					Thread.Sleep(500);
				}
				num++;
			}
			return text;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000106BC File Offset: 0x0000E8BC
		private void AppxPackageProgramDataFile_Watcher_Deleted(object sender, FileSystemEventArgs e)
		{
			if (e.FullPath.Contains(FuryContorller_Service.sAppxPackageName))
			{
				Log.LibCmdLogWriter("Check KeepData file exists", false);
				if (File.Exists(KeepData.KeepData_FilePath))
				{
					Log.LibCmdLogWriter("KeepData file remove", false);
					File.Delete(KeepData.KeepData_FilePath);
				}
				Log.LibCmdLogWriter("appxpackage remove", false);
				string text = string.Format("{0}\\unins000.exe", AppDomain.CurrentDomain.BaseDirectory);
				if (!File.Exists(text))
				{
					text = string.Format("{0}\\unins001.exe", AppDomain.CurrentDomain.BaseDirectory);
				}
				if (File.Exists(text))
				{
					Log.LibCmdLogWriter("kernel remove", false);
					FuryContorller_Service.CreateTask(_TASK_RUNLEVEL.TASK_RUNLEVEL_HIGHEST, "Uninstall", text, "/verysilent /norestart");
					return;
				}
				Log.LibCmdLogWriter("kernel remove location not found.", false);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00010778 File Offset: 0x0000E978
		private bool CheckExpireTime()
		{
			if (Version.Parse(FuryContorller_Service.sAppVersion).CompareTo(Version.Parse("1.0.0.01")) >= 0)
			{
				Log.LibCmdLogWriter("Release version.", false);
				return true;
			}
			DateTime dateTime = new DateTime(2024, 4, 31);
			Log.LibCmdLogWriter(string.Format("Beta version. ({0})", dateTime.ToString("MMdd.yyyy")), false);
			if (DateTime.Now - dateTime < TimeSpan.FromDays(90.0))
			{
				return true;
			}
			Log.LibCmdLogWriter("Access denied.", false);
			return false;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00010808 File Offset: 0x0000EA08
		private void CheckDramSupport()
		{
			Log.LibCmdLogWriter("Check platform start.", false);
			FuryContorller_Service.dDRAMInfos.Clear();
			FuryContorller_Service.dDRAMInfos_cache.Clear();
			FuryContorller_Service.iDDRType = 0;
			FuryContorller_Service.iAPIVer = 0;
			this.iDDRDeviceCount = this.QueryDDRDeviceCount();
			Log.LibCmdLogWriter(string.Format("This platform have {0} memory device.", this.iDDRDeviceCount), false);
			if (this.bIsX299)
			{
				int num = 7;
				Log.LibCmdLogWriter("Check dram info start. (X99, X299)", false);
				Log.LibCmdLogWriter("***** DRAM INFORMATION BEGIN *****", false);
				for (int i = 0; i < 5; i++)
				{
					FuryContorller_Service.dDRAMInfos.Clear();
					FuryContorller_Service.dDRAMInfos_cache.Clear();
					bool flag = true;
					int num2 = 0;
					for (int j = 0; j <= num; j++)
					{
						Log.LibCmdLogWriter("slotCount=" + j.ToString(), false);
						if (this.QueryDDRInformation(j, j))
						{
							flag = false;
							num2++;
						}
					}
					if (!flag && num2 >= this.iDDRDeviceCount)
					{
						break;
					}
					Thread.Sleep(1000);
					Log.LibCmdLogWriter("***** DRAM INFORMATION RETRY *****", false);
				}
				Log.LibCmdLogWriter("***** DRAM INFORMATION END *****", false);
			}
			else
			{
				int num3 = 174;
				Log.LibCmdLogWriter("Check dram info start.", false);
				Log.LibCmdLogWriter("***** DRAM INFORMATION BEGIN *****", false);
				for (int k = 0; k < 10; k++)
				{
					FuryContorller_Service.dDRAMInfos.Clear();
					FuryContorller_Service.dDRAMInfos_cache.Clear();
					bool flag = true;
					int num4 = 0;
					int l = 160;
					int num5 = 0;
					while (l <= num3)
					{
						int num6 = num5;
						if (this.QueryDDRInformation(l, num6))
						{
							flag = false;
							num4++;
						}
						l += 2;
						num5++;
					}
					if (!flag && num4 >= this.iDDRDeviceCount)
					{
						break;
					}
					Thread.Sleep(500);
					Log.LibCmdLogWriter("***** DRAM INFORMATION RETRY *****", false);
				}
				Log.LibCmdLogWriter("***** DRAM INFORMATION END *****", false);
			}
			if (FuryContorller_Service.dDRAMInfos_cache.Count > 0 && FuryContorller_Service.dDRAMInfos_cache.ElementAt(0).Value.api_ver == 3)
			{
				int num7 = 0;
				for (int m = 0; m < FuryContorller_Service.dDRAMInfos_cache.Count; m++)
				{
					if (FuryContorller_Service.dDRAMInfos_cache.ElementAt(m).Value.mode == 13)
					{
						num7 += FuryContorller_Service.dDRAMInfos_cache.ElementAt(m).Value.master_slave;
					}
				}
				if (num7 == 0)
				{
					for (int n = 0; n < FuryContorller_Service.dDRAMInfos_cache.Count; n++)
					{
						if (FuryContorller_Service.dDRAMInfos_cache.ElementAt(n).Value.mode == 13 && FuryContorller_Service.dDRAMInfos_cache.ElementAt(n).Value.master_slave == 0)
						{
							if (FuryContorller_Service.dDRAMInfos_cache.ElementAt(n).Value.speed - 9 == 8)
							{
								FuryContorller_Service.dDRAMInfos_cache.ElementAt(n).Value.master_slave = 0;
							}
							else if (FuryContorller_Service.dDRAMInfos_cache.ElementAt(n).Value.speed - 15 == 8)
							{
								FuryContorller_Service.dDRAMInfos_cache.ElementAt(n).Value.master_slave = 1;
							}
							else if (FuryContorller_Service.dDRAMInfos_cache.ElementAt(n).Value.speed == 8)
							{
								FuryContorller_Service.dDRAMInfos_cache.ElementAt(n).Value.master_slave = 2;
							}
							else if (FuryContorller_Service.dDRAMInfos_cache.ElementAt(n).Value.speed - 11 == 8)
							{
								FuryContorller_Service.dDRAMInfos_cache.ElementAt(n).Value.master_slave = 3;
							}
						}
					}
				}
			}
			if (FuryContorller_Service.dDRAMInfos_cache.Count > 0)
			{
				if (FuryContorller_Service.dDRAMInfos.Count == 0)
				{
					FuryContorller_Service.IsCR_0B_Error = false;
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
					int num8 = 0;
					int num9 = 0;
					List<int> list = new List<int> { 0, 1, 3, 6 };
					for (int num10 = 0; num10 < FuryContorller_Service.dDRAMInfos_cache.Count; num10++)
					{
						if (FuryContorller_Service.dDRAMInfos_cache.ElementAt(num10).Value.index < 4)
						{
							if (!dictionary.ContainsKey(FuryContorller_Service.dDRAMInfos_cache.ElementAt(num10).Value.master_slave.ToString()))
							{
								dictionary.Add(FuryContorller_Service.dDRAMInfos_cache.ElementAt(num10).Value.master_slave.ToString(), FuryContorller_Service.dDRAMInfos_cache.ElementAt(num10).Key);
								num8 += FuryContorller_Service.dDRAMInfos_cache.ElementAt(num10).Value.master_slave;
							}
							else
							{
								FuryContorller_Service.IsCR_0B_Error = true;
								Log.LibCmdLogWriter("Section 1 > 0B error detected, duplicate value.", false);
							}
						}
						else if (FuryContorller_Service.dDRAMInfos_cache.ElementAt(num10).Value.index >= 4)
						{
							if (!dictionary2.ContainsKey(FuryContorller_Service.dDRAMInfos_cache.ElementAt(num10).Value.master_slave.ToString()))
							{
								dictionary2.Add(FuryContorller_Service.dDRAMInfos_cache.ElementAt(num10).Value.master_slave.ToString(), FuryContorller_Service.dDRAMInfos_cache.ElementAt(num10).Key);
								num9 += FuryContorller_Service.dDRAMInfos_cache.ElementAt(num10).Value.master_slave;
							}
							else
							{
								FuryContorller_Service.IsCR_0B_Error = true;
								Log.LibCmdLogWriter("Section 2 > 0B error detected, duplicate value.", false);
							}
						}
					}
					if (dictionary.Count > 0 && num8 != list[dictionary.Count - 1])
					{
						FuryContorller_Service.IsCR_0B_Error = true;
						Log.LibCmdLogWriter(string.Format("Section 1 > 0B error detected, non-sequential value. ({0})", num8), false);
					}
					if (dictionary2.Count > 0 && num9 != list[dictionary2.Count - 1])
					{
						FuryContorller_Service.IsCR_0B_Error = true;
						Log.LibCmdLogWriter(string.Format("Section 2 > 0B error detected, non-sequential value. ({0})", num9), false);
					}
					if (FuryContorller_Service.IsCR_0B_Error)
					{
						int num11 = 0;
						int num12 = 0;
						for (int num13 = FuryContorller_Service.dDRAMInfos_cache.Count - 1; num13 >= 0; num13--)
						{
							try
							{
								if (FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value.index < 4)
								{
									FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value.master_slave = num11;
									FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value.index = num11;
									FuryContorller_Service.dDRAMInfos.Add("slot_" + FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value.index.ToString(), FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value);
									num11++;
								}
								else if (FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value.index >= 4)
								{
									FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value.master_slave = num12;
									FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value.index = num12 + 4;
									FuryContorller_Service.dDRAMInfos.Add("slot_" + FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value.index.ToString(), FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value);
									num12++;
								}
							}
							catch (Exception)
							{
								Log.LibCmdLogWriter(string.Format("master_slave = {0} address_offset = {1}", FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value.master_slave.ToString(), FuryContorller_Service.dDRAMInfos_cache.ElementAt(num13).Value.address_offset.ToString()), false);
							}
						}
					}
					else
					{
						for (int num14 = 0; num14 < FuryContorller_Service.dDRAMInfos_cache.Count; num14++)
						{
							try
							{
								if (FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value.type == 1)
								{
									FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value.index = FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value.master_slave + FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value.address_offset / 4 * 4;
									FuryContorller_Service.dDRAMInfos.Add("slot_" + FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value.index.ToString(), FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value);
								}
								else
								{
									FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value.index = FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value.master_slave;
									FuryContorller_Service.dDRAMInfos.Add("slot_" + FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value.index.ToString(), FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value);
								}
							}
							catch (Exception)
							{
								Log.LibCmdLogWriter(string.Format("master_slave = {0} address_offset = {1}", FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value.master_slave.ToString(), FuryContorller_Service.dDRAMInfos_cache.ElementAt(num14).Value.address_offset.ToString()), false);
							}
						}
					}
				}
				else
				{
					FuryContorller_Service.iDDRType = 1;
					FuryContorller_Service.iAPIVer = 255;
					Log.LibCmdLogWriter("API Ver status 255", false);
				}
				FuryContorller_Service.dDRAMInfos_cache.Clear();
			}
			if (FuryContorller_Service.dDRAMInfos != null && FuryContorller_Service.dDRAMInfos.Count<KeyValuePair<string, DRAMInfoObj>>() > 0)
			{
				for (int num15 = 0; num15 < FuryContorller_Service.dDRAMInfos.Count; num15++)
				{
					if (FuryContorller_Service.iDDRType < 1 && FuryContorller_Service.dDRAMInfos.ElementAt(num15).Value.type == 1)
					{
						FuryContorller_Service.iDDRType = 1;
					}
					else if (FuryContorller_Service.iDDRType < 2 && FuryContorller_Service.dDRAMInfos.ElementAt(num15).Value.type == 2)
					{
						FuryContorller_Service.iDDRType = 2;
					}
					if (FuryContorller_Service.iAPIVer == 0)
					{
						if (FuryContorller_Service.dDRAMInfos.ElementAt(num15).Value.api_ver == 1)
						{
							FuryContorller_Service.iAPIVer = 1;
						}
						else if (FuryContorller_Service.dDRAMInfos.ElementAt(num15).Value.api_ver == 2)
						{
							FuryContorller_Service.iAPIVer = 2;
						}
						else if (FuryContorller_Service.dDRAMInfos.ElementAt(num15).Value.api_ver == 3)
						{
							FuryContorller_Service.iAPIVer = 3;
						}
						else if (FuryContorller_Service.dDRAMInfos.ElementAt(num15).Value.api_ver == 4)
						{
							FuryContorller_Service.iAPIVer = 4;
						}
					}
					else if (FuryContorller_Service.iAPIVer != 255 && FuryContorller_Service.iAPIVer != FuryContorller_Service.dDRAMInfos.ElementAt(num15).Value.api_ver)
					{
						FuryContorller_Service.iAPIVer = 255;
						Log.LibCmdLogWriter("API Ver status 255", false);
					}
				}
			}
			if (FuryContorller_Service.iAPIVer == 2 || FuryContorller_Service.iAPIVer == 3 || FuryContorller_Service.iAPIVer == 4)
			{
				Miscellaneous.getBaseAddress(FuryContorller_Service.iDDRType);
				Dictionary<string, DRAMInfoObj> dictionary3 = FuryContorller_Service.dDRAMInfos.OrderBy((KeyValuePair<string, DRAMInfoObj> e) => e.Value.index).Take(FuryContorller_Service.dDRAMInfos.Count).ToDictionary((KeyValuePair<string, DRAMInfoObj> e) => e.Key, (KeyValuePair<string, DRAMInfoObj> e) => e.Value);
				FuryContorller_Service.dDRAMInfos = new Dictionary<string, DRAMInfoObj>();
				int num16 = 0;
				int num17 = 0;
				for (int num18 = dictionary3.Count - 1; num18 >= 0; num18--)
				{
					if (dictionary3.ElementAt(num18).Value.index < 4)
					{
						num16++;
					}
					else
					{
						num17++;
					}
				}
				for (int num19 = dictionary3.Count - 1; num19 >= 0; num19--)
				{
					if (dictionary3.ElementAt(num19).Value.index < 4)
					{
						dictionary3.ElementAt(num19).Value.index = num16 - 1 - dictionary3.ElementAt(num19).Value.index;
					}
					else
					{
						dictionary3.ElementAt(num19).Value.index = num17 - 1 - (dictionary3.ElementAt(num19).Value.index - 4) + 4;
					}
					string text = "slot_" + dictionary3.ElementAt(num19).Value.index.ToString();
					FuryContorller_Service.dDRAMInfos.Add(text, dictionary3.ElementAt(num19).Value);
				}
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00011578 File Offset: 0x0000F778
		private int QueryDDRDeviceCount()
		{
			int num = 0;
			int num2 = 0;
			ManagementObjectSearcher managementObjectSearcher = null;
			while (managementObjectSearcher == null && num2 <= 15)
			{
				try
				{
					managementObjectSearcher = new ManagementObjectSearcher("\\\\.\\ROOT\\WMI", "SELECT * FROM MSSmBios_RawSMBiosTables");
					if (managementObjectSearcher != null)
					{
						foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
						{
							byte[] array = (byte[])((ManagementObject)managementBaseObject).Properties["SMBiosData"].Value;
							if (array.Length != 0)
							{
								for (int i = 8; i < array.Length; i++)
								{
									if (array[i] == 17 && array[i + 18] != 2)
									{
										num++;
									}
									i += (int)array[i + 1];
									while (array[i] != 0 || array[i + 1] != 0)
									{
										i++;
									}
									i++;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					Log.LibCmdLogWriter(string.Format("QueryDDRNumber {0}", ex.Message), true);
					Thread.Sleep(500);
				}
				num2++;
			}
			return num;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0001169C File Offset: 0x0000F89C
		private bool QueryDDRInformation(int index, int j_index)
		{
			string text = "";
			string text2 = "";
			int num = 0;
			bool flag = false;
			byte b = 0;
			Class_DLL.CheckDRAMSupport(index, out text, out text2, out flag, out b);
			if (string.IsNullOrEmpty(text))
			{
				Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac :", j_index.ToString(), index.ToString("X")), false);
				return false;
			}
			for (int i = 0; i < 3; i++)
			{
				if (text == "HyperX_Predator")
				{
					Class_DLL.GetKingstonFuryDDR5ModuleID((byte)(88 + j_index), out num);
					DRAMInfoObj draminfoObj = new DRAMInfoObj();
					draminfoObj.index = j_index;
					draminfoObj.manufac = text;
					draminfoObj.brand = "HyperX";
					draminfoObj.product = "Predator";
					draminfoObj.part_number = text2;
					draminfoObj.support_xmp = false;
					draminfoObj.type = 1;
					draminfoObj.api_ver = 1;
					draminfoObj.model_id = num;
					FuryContorller_Service.dDRAMInfos.Add("slot_" + j_index.ToString(), draminfoObj);
					Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac : {2} id : {3}", new object[]
					{
						j_index.ToString(),
						index.ToString("X"),
						text,
						num.ToString("X")
					}), false);
					return true;
				}
				if (text == "HyperX_Fury")
				{
					Class_DLL.GetKingstonFuryDDR5ModuleID((byte)(88 + j_index), out num);
					DRAMInfoObj draminfoObj2 = new DRAMInfoObj();
					draminfoObj2.index = j_index;
					draminfoObj2.manufac = text;
					draminfoObj2.brand = "HyperX";
					draminfoObj2.product = "Fury";
					draminfoObj2.part_number = text2;
					draminfoObj2.support_xmp = false;
					draminfoObj2.type = 1;
					draminfoObj2.api_ver = 1;
					draminfoObj2.model_id = num;
					FuryContorller_Service.dDRAMInfos.Add("slot_" + j_index.ToString(), draminfoObj2);
					Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac : {2} id : {3}", new object[]
					{
						j_index.ToString(),
						index.ToString("X"),
						text,
						num.ToString("X")
					}), false);
					return true;
				}
				if (text == "KingstonFury_Renegade")
				{
					Class_DLL.GetKingstonFuryDDR5ModuleID((byte)(88 + j_index), out num);
					DRAMInfoObj draminfoObj3 = new DRAMInfoObj();
					draminfoObj3.index = j_index;
					draminfoObj3.manufac = text;
					draminfoObj3.brand = "Kingston Fury";
					draminfoObj3.product = "Renegade";
					draminfoObj3.part_number = text2;
					draminfoObj3.support_xmp = false;
					draminfoObj3.type = 1;
					draminfoObj3.api_ver = 1;
					draminfoObj3.model_id = num;
					FuryContorller_Service.dDRAMInfos.Add("slot_" + j_index.ToString(), draminfoObj3);
					Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac : {2} id : {3}", new object[]
					{
						j_index.ToString(),
						index.ToString("X"),
						text,
						num.ToString("X")
					}), false);
					return true;
				}
				if (text == "KingstonFury_Beast")
				{
					Class_DLL.GetKingstonFuryDDR5ModuleID((byte)(88 + j_index), out num);
					DRAMInfoObj draminfoObj4 = new DRAMInfoObj();
					draminfoObj4.index = j_index;
					draminfoObj4.manufac = text;
					draminfoObj4.brand = "Kingston Fury";
					draminfoObj4.product = "Beast";
					draminfoObj4.part_number = text2;
					draminfoObj4.support_xmp = false;
					draminfoObj4.type = 1;
					draminfoObj4.api_ver = 1;
					draminfoObj4.model_id = num;
					FuryContorller_Service.dDRAMInfos.Add("slot_" + j_index.ToString(), draminfoObj4);
					Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac : {2} id : {3}", new object[]
					{
						j_index.ToString(),
						index.ToString("X"),
						text,
						num.ToString("X")
					}), false);
					return true;
				}
				if (text == "KingstonFury_Beast_SE" || text == "KingstonFury_Beast_Refresh")
				{
					byte b2 = 0;
					Class_DLL.GetKingstonFuryDDR5MasterSlaveRole((byte)(88 + j_index), out b2);
					Class_DLL.GetKingstonFuryDDR5ModuleID((byte)(88 + j_index), out num);
					DRAMInfoObj draminfoObj5 = new DRAMInfoObj();
					draminfoObj5.index = j_index;
					draminfoObj5.address_offset = j_index;
					draminfoObj5.master_slave = (int)b2;
					draminfoObj5.manufac = text;
					draminfoObj5.brand = "Kingston Fury";
					draminfoObj5.product = "Beast";
					draminfoObj5.part_number = text2;
					draminfoObj5.support_xmp = false;
					draminfoObj5.type = 1;
					draminfoObj5.api_ver = 2;
					draminfoObj5.model_id = num;
					if (!FuryContorller_Service.dDRAMInfos_cache.ContainsKey("slot_" + draminfoObj5.index.ToString()))
					{
						FuryContorller_Service.dDRAMInfos_cache.Add("slot_" + draminfoObj5.index.ToString(), draminfoObj5);
					}
					Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac : {2} id : {3} data : {4}", new object[]
					{
						j_index.ToString(),
						index.ToString("X"),
						text,
						num.ToString("X"),
						b2.ToString()
					}), false);
					return true;
				}
				if (text == "KingstonFury_Renegade_Refresh")
				{
					byte b3 = 0;
					Class_DLL.GetKingstonFuryDDR5MasterSlaveRole((byte)(88 + j_index), out b3);
					Class_DLL.GetKingstonFuryDDR5ModuleID((byte)(88 + j_index), out num);
					DRAMInfoObj draminfoObj6 = new DRAMInfoObj();
					draminfoObj6.index = j_index;
					draminfoObj6.address_offset = j_index;
					draminfoObj6.master_slave = (int)b3;
					draminfoObj6.manufac = text;
					draminfoObj6.brand = "Kingston Fury";
					draminfoObj6.product = "Renegade";
					draminfoObj6.part_number = text2;
					draminfoObj6.support_xmp = false;
					draminfoObj6.type = 1;
					draminfoObj6.api_ver = 2;
					draminfoObj6.model_id = num;
					if (!FuryContorller_Service.dDRAMInfos_cache.ContainsKey("slot_" + draminfoObj6.index.ToString()))
					{
						FuryContorller_Service.dDRAMInfos_cache.Add("slot_" + draminfoObj6.index.ToString(), draminfoObj6);
					}
					Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac : {2} id : {3} data : {4}", new object[]
					{
						j_index.ToString(),
						index.ToString("X"),
						text,
						num.ToString("X"),
						b3.ToString()
					}), false);
					return true;
				}
				if (text == "KingstonFury_Beast_DDR5" || text == "KingstonFury_Beast_DDR5_White")
				{
					byte b4 = 0;
					Class_DLL.GetKingstonFuryDDR5MasterSlaveRole((byte)(96 + j_index), out b4);
					Class_DLL.GetKingstonFuryDDR5ModuleID((byte)(96 + j_index), out num);
					DRAMInfoObj draminfoObj7 = new DRAMInfoObj();
					draminfoObj7.index = j_index;
					draminfoObj7.address_offset = j_index;
					draminfoObj7.master_slave = (int)b4;
					draminfoObj7.manufac = text;
					draminfoObj7.brand = "Kingston Fury";
					draminfoObj7.product = "Beast";
					draminfoObj7.part_number = text2;
					draminfoObj7.support_xmp = flag;
					draminfoObj7.type = 2;
					draminfoObj7.api_ver = 2;
					draminfoObj7.model_id = num;
					if (!FuryContorller_Service.dDRAMInfos_cache.ContainsKey("slot_" + draminfoObj7.index.ToString()))
					{
						FuryContorller_Service.dDRAMInfos_cache.Add("slot_" + draminfoObj7.index.ToString(), draminfoObj7);
					}
					Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac : {2} id : {3} data : {4}", new object[]
					{
						j_index.ToString(),
						index.ToString("X"),
						text,
						num.ToString("X"),
						b4.ToString()
					}), false);
					return true;
				}
				if (text == "KingstonFury_Renegade_DDR5" || text == "KingstonFury_Renegade_DDR5_White")
				{
					byte b5 = 0;
					Class_DLL.GetKingstonFuryDDR5MasterSlaveRole((byte)(96 + j_index), out b5);
					Class_DLL.GetKingstonFuryDDR5ModuleID((byte)(96 + j_index), out num);
					DRAMInfoObj draminfoObj8 = new DRAMInfoObj();
					draminfoObj8.index = j_index;
					draminfoObj8.address_offset = j_index;
					draminfoObj8.master_slave = (int)b5;
					draminfoObj8.manufac = text;
					draminfoObj8.brand = "Kingston Fury";
					draminfoObj8.product = "Renegade";
					draminfoObj8.part_number = text2;
					draminfoObj8.support_xmp = flag;
					draminfoObj8.type = 2;
					draminfoObj8.api_ver = 2;
					draminfoObj8.model_id = num;
					if (!FuryContorller_Service.dDRAMInfos_cache.ContainsKey("slot_" + draminfoObj8.index.ToString()))
					{
						FuryContorller_Service.dDRAMInfos_cache.Add("slot_" + draminfoObj8.index.ToString(), draminfoObj8);
					}
					Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac : {2} id : {3} data : {4}", new object[]
					{
						j_index.ToString(),
						index.ToString("X"),
						text,
						num.ToString("X"),
						b5.ToString()
					}), false);
					return true;
				}
				if (text == "KingstonFury_Renegade_DDR5_LE")
				{
					byte b6 = 0;
					byte b7 = 0;
					byte b8 = 0;
					Class_DLL.GetKingstonFuryDDR5MasterSlaveRole((byte)(96 + j_index), out b6);
					Class_DLL.GetKingstonFuryDDR5ModuleID((byte)(96 + j_index), out num);
					Class_DLL.GetKingstonFuryDDR5Speed((byte)(96 + j_index), out b7);
					Class_DLL.GetKingstonFuryDDR5Style((byte)(96 + j_index), out b8);
					DRAMInfoObj draminfoObj9 = new DRAMInfoObj();
					draminfoObj9.index = j_index;
					draminfoObj9.address_offset = j_index;
					draminfoObj9.master_slave = (int)b6;
					draminfoObj9.manufac = text;
					draminfoObj9.brand = "Kingston Fury";
					draminfoObj9.product = "Renegade";
					draminfoObj9.part_number = text2;
					draminfoObj9.support_xmp = flag;
					draminfoObj9.type = 2;
					draminfoObj9.api_ver = 3;
					draminfoObj9.model_id = num;
					draminfoObj9.speed = (int)b7;
					draminfoObj9.mode = (int)b8;
					if (!FuryContorller_Service.dDRAMInfos_cache.ContainsKey("slot_" + draminfoObj9.index.ToString()))
					{
						FuryContorller_Service.dDRAMInfos_cache.Add("slot_" + draminfoObj9.index.ToString(), draminfoObj9);
					}
					Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac : {2} id : {3} data : {4} mode : {5} speed : {6}", new object[]
					{
						j_index.ToString(),
						index.ToString("X"),
						text,
						num.ToString("X"),
						b6.ToString(),
						b8.ToString("X"),
						b7.ToString()
					}), false);
					return true;
				}
				if (text == "KingstonFury_Renegade_DDR5_v2" || text == "KingstonFury_Renegade_DDR5_White_v2")
				{
					byte b9 = 0;
					byte b10 = 0;
					byte b11 = 0;
					Class_DLL.GetKingstonFuryDDR5MasterSlaveRole((byte)(96 + j_index), out b9);
					Class_DLL.GetKingstonFuryDDR5ModuleID((byte)(96 + j_index), out num);
					Class_DLL.GetKingstonFuryDDR5Speed((byte)(96 + j_index), out b10);
					Class_DLL.GetKingstonFuryDDR5Style((byte)(96 + j_index), out b11);
					DRAMInfoObj draminfoObj10 = new DRAMInfoObj();
					draminfoObj10.index = j_index;
					draminfoObj10.address_offset = j_index;
					draminfoObj10.master_slave = (int)b9;
					draminfoObj10.manufac = text;
					draminfoObj10.brand = "Kingston Fury";
					draminfoObj10.product = "Renegade";
					draminfoObj10.part_number = text2;
					draminfoObj10.support_xmp = flag;
					draminfoObj10.type = 2;
					draminfoObj10.api_ver = 4;
					draminfoObj10.model_id = num;
					draminfoObj10.speed = (int)b10;
					draminfoObj10.mode = (int)b11;
					if (!FuryContorller_Service.dDRAMInfos_cache.ContainsKey("slot_" + draminfoObj10.index.ToString()))
					{
						FuryContorller_Service.dDRAMInfos_cache.Add("slot_" + draminfoObj10.index.ToString(), draminfoObj10);
					}
					Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac : {2} id : {3} data : {4} mode : {5} speed : {6}", new object[]
					{
						j_index.ToString(),
						index.ToString("X"),
						text,
						num.ToString("X"),
						b9.ToString(),
						b11.ToString("X"),
						b10.ToString()
					}), false);
					return true;
				}
				Thread.Sleep(100);
				Class_DLL.CheckDRAMSupport(index, out text, out text2, out flag, out b);
				Log.LibCmdLogWriter(string.Format("***** DRAM Slot : {0} ({1}) | manufac : {2}", j_index.ToString(), index.ToString("X"), text), false);
			}
			return true;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00012284 File Offset: 0x00010484
		public static string MainWindow_OnRequestFromClient(string sData)
		{
			if (!string.IsNullOrEmpty(sData))
			{
				try
				{
					if (sData.IndexOf("\"get_version\"", StringComparison.InvariantCultureIgnoreCase) > 0)
					{
						return "{" + "\"root\":{" + "\"api\":\"get_version\"," + "\"status\":\"0\"," + "\"version\":\"" + FuryContorller_Service.sAppVersion + "\"" + "}}";
					}
					if (sData.IndexOf("\"set_kernel_update\"", StringComparison.InvariantCultureIgnoreCase) > 0)
					{
						DRAMJsonRoot dramjsonRoot = null;
						try
						{
							dramjsonRoot = JsonClass.JavaScriptDeserialize<DRAMJsonRoot>(sData);
						}
						catch (Exception ex)
						{
							Log.LibLogWriter(string.Format("Set kernel deserialize. ({0})", ex.Message), true);
						}
						string text;
						if (dramjsonRoot != null && dramjsonRoot.root != null && dramjsonRoot.root.kernel_info != null)
						{
							if (!string.IsNullOrEmpty(dramjsonRoot.root.kernel_info.version) && Version.Parse(dramjsonRoot.root.kernel_info.version).CompareTo(Version.Parse(FuryContorller_Service.sAppVersion)) >= 0)
							{
								if (!string.IsNullOrEmpty(dramjsonRoot.root.kernel_info.setup_path) && File.Exists(dramjsonRoot.root.kernel_info.setup_path))
								{
									try
									{
										try
										{
											FuryContorller_Service._mutex = Mutex.OpenExisting(FuryContorller_Service.sServiceMutexGUID);
											text = "{";
											text += "\"root\":{";
											text += "\"api\":\"set_kernel_update\",";
											text += "\"status\":\"1\"";
											return text + "}}";
										}
										catch (WaitHandleCannotBeOpenedException)
										{
											FuryContorller_Service._mutex = new Mutex(true, FuryContorller_Service.sServiceMutexGUID);
										}
										DRAMJsonRoot dramjsonRoot2 = dramjsonRoot;
										string text2 = string.Format("{0}\\update\\{1}", AppDomain.CurrentDomain.BaseDirectory, dramjsonRoot2.root.kernel_info.setup_path.Substring(dramjsonRoot2.root.kernel_info.setup_path.LastIndexOf("\\") + 1));
										Directory.CreateDirectory(string.Format("{0}\\update\\", AppDomain.CurrentDomain.BaseDirectory));
										File.Copy(dramjsonRoot2.root.kernel_info.setup_path, text2, true);
										if (File.Exists(text2))
										{
											FuryContorller_Service.CreateTask(_TASK_RUNLEVEL.TASK_RUNLEVEL_HIGHEST, "Update", text2, dramjsonRoot2.root.kernel_info.setup_args);
										}
										else
										{
											Log.LibLogWriter(string.Format("Set kernel start update. (Copy file failure.)", Array.Empty<object>()), true);
										}
									}
									catch (Exception ex2)
									{
										FuryContorller_Service._mutex.ReleaseMutex();
										FuryContorller_Service._mutex.Close();
										FuryContorller_Service._mutex.Dispose();
										Log.LibLogWriter(string.Format("Set kernel start update. ({0})", ex2.Message), true);
									}
									text = "{";
									text += "\"root\":{";
									text += "\"api\":\"set_kernel_update\",";
									text += "\"status\":\"0\"";
									text += "}}";
								}
								else
								{
									text = "{";
									text += "\"root\":{";
									text += "\"api\":\"set_kernel_update\",";
									text += "\"status\":\"4\"";
									text += "}}";
								}
							}
							else
							{
								text = "{";
								text += "\"root\":{";
								text += "\"api\":\"set_kernel_update\",";
								text += "\"status\":\"3\"";
								text += "}}";
							}
						}
						else
						{
							text = "{";
							text += "\"root\":{";
							text += "\"api\":\"set_kernel_update\",";
							text += "\"status\":\"2\"";
							text += "}}";
						}
						return text;
					}
					if (sData.IndexOf("\"get_keep_status\"", StringComparison.InvariantCultureIgnoreCase) > 0)
					{
						return "{" + "\"root\":{" + "\"api\":\"get_keep_status\"," + "\"status\":\"0\"," + "\"keep\":\"" + (FuryContorller_Service.bKeep ? "1" : "0") + "\"" + "}}";
					}
					if (sData.IndexOf("\"set_keep_status\"", StringComparison.InvariantCultureIgnoreCase) > 0)
					{
						if (sData.IndexOf("\"keep\":\"1\"", StringComparison.InvariantCultureIgnoreCase) > 0)
						{
							FuryContorller_Service.bKeep = true;
						}
						else if (sData.IndexOf("\"keep\":\"", StringComparison.InvariantCultureIgnoreCase) > 0)
						{
							FuryContorller_Service.bKeep = false;
						}
						KeepData.KeepDataWriter("bKeep", FuryContorller_Service.bKeep.ToString());
						return "{" + "\"root\":{" + "\"api\":\"set_keep_status\"," + "\"status\":\"0\"" + "}}";
					}
					if (sData.IndexOf("\"get_dram_type\"", StringComparison.InvariantCultureIgnoreCase) > 0)
					{
						return "{" + "\"root\":{" + "\"api\":\"get_dram_type\"," + "\"status\":\"0\"," + "\"dram_type\":" + FuryContorller_Service.iDDRType.ToString() + "}}";
					}
					if (sData.IndexOf("\"get_dram_api\"", StringComparison.InvariantCultureIgnoreCase) > 0)
					{
						string text3 = "{";
						text3 += "\"root\":{";
						text3 += "\"api\":\"get_dram_api\",";
						text3 += "\"status\":\"0\",";
						if (FuryContorller_Service.iAPIVer != 255 && FuryContorller_Service.IsCR_0B_Error)
						{
							text3 += "\"dram_api\":254";
						}
						else
						{
							text3 = text3 + "\"dram_api\":" + FuryContorller_Service.iAPIVer.ToString();
						}
						return text3 + "}}";
					}
					if (sData.IndexOf("\"get_dram_info\"", StringComparison.InvariantCultureIgnoreCase) > 0)
					{
						string text4 = "null";
						object obj = FuryContorller_Service.OnPowerEvent_locker;
						lock (obj)
						{
							if (FuryContorller_Service.dDRAMInfos != null && FuryContorller_Service.dDRAMInfos.Count > 0)
							{
								Log.LibLogWriter(string.Format("Response DRAM amount =  ({0})", FuryContorller_Service.dDRAMInfos.Count.ToString()), false);
								try
								{
									if (FuryContorller_Service.bIsAM4 && FuryContorller_Service.iAPIVer == 1)
									{
										Dictionary<string, DRAMInfoObj> dictionary = new Dictionary<string, DRAMInfoObj>();
										for (int i = 0; i < FuryContorller_Service.dDRAMInfos.Count; i++)
										{
											if (FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.index == 1)
											{
												dictionary.Add("slot_2", new DRAMInfoObj
												{
													index = 2,
													manufac = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.manufac,
													brand = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.brand,
													product = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.product,
													part_number = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.part_number,
													type = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.type,
													api_ver = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.api_ver
												});
											}
											else if (FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.index == 2)
											{
												dictionary.Add("slot_1", new DRAMInfoObj
												{
													index = 1,
													manufac = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.manufac,
													brand = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.brand,
													product = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.product,
													part_number = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.part_number,
													type = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.type,
													api_ver = FuryContorller_Service.dDRAMInfos.ElementAt(i).Value.api_ver
												});
											}
											else
											{
												dictionary.Add(FuryContorller_Service.dDRAMInfos.ElementAt(i).Key, FuryContorller_Service.dDRAMInfos.ElementAt(i).Value);
											}
										}
										text4 = JsonClass.JavaScriptSerialize<Dictionary<string, DRAMInfoObj>>(dictionary);
									}
									else
									{
										text4 = JsonClass.JavaScriptSerialize<Dictionary<string, DRAMInfoObj>>(FuryContorller_Service.dDRAMInfos);
									}
									goto IL_08E1;
								}
								catch (Exception ex3)
								{
									Log.LibLogWriter(string.Format("Get info serialize. ({0})", ex3.Message), true);
									return "{" + "\"root\":{" + "\"api\":\"get_dram_info\"," + "\"status\":\"1\"," + "\"dram\":null" + "}}";
								}
							}
							if (!FuryContorller_Service.bExpire)
							{
								return "{" + "\"root\":{" + "\"api\":\"get_dram_info\"," + "\"status\":\"1\"," + "\"dram\":null" + "}}";
							}
							IL_08E1:;
						}
						return "{" + "\"root\":{" + "\"api\":\"get_dram_info\"," + "\"status\":\"0\"," + "\"dram\":" + text4 + "}}";
					}
					if (sData.IndexOf("\"get_dram_timings\"", StringComparison.InvariantCultureIgnoreCase) > 0)
					{
						string text5 = "null";
						object obj = FuryContorller_Service.OnPowerEvent_locker;
						lock (obj)
						{
							if (FuryContorller_Service.dDRAMInfos != null && FuryContorller_Service.dDRAMInfos.Count > 0 && FuryContorller_Service.iDDRType == 2)
							{
								Log.LibLogWriter(string.Format("Response DRAM timing", Array.Empty<object>()), false);
								try
								{
									if (FuryContorller_Service.dDRAMTimings.Count == 0)
									{
										for (int j = 0; j < FuryContorller_Service.dDRAMInfos.Count; j++)
										{
											if (FuryContorller_Service.dDRAMInfos.ElementAt(j).Value.type == 2 && FuryContorller_Service.dDRAMInfos.ElementAt(j).Value.support_xmp)
											{
												byte b = (byte)(Miscellaneous.iDDRBaseAddress + FuryContorller_Service.dDRAMInfos.ElementAt(j).Value.address_offset);
												Class_DLL.IntelXMP intelXMP = default(Class_DLL.IntelXMP);
												if (Class_DLL.GetKingstonFuryDDR5XmpProfile(b, ref intelXMP))
												{
													DRAMTimingTableObj dramtimingTableObj = new DRAMTimingTableObj();
													dramtimingTableObj.index = FuryContorller_Service.dDRAMInfos.ElementAt(j).Value.index;
													dramtimingTableObj.address_offset = FuryContorller_Service.dDRAMInfos.ElementAt(j).Value.address_offset;
													dramtimingTableObj.part_number = FuryContorller_Service.dDRAMInfos.ElementAt(j).Value.part_number;
													dramtimingTableObj.version = (double)intelXMP.Version / 16.0;
													dramtimingTableObj.timing_table = new List<DRAMTimingsObj>();
													DRAMTimingsObj dramtimingsObj = new DRAMTimingsObj();
													dramtimingsObj.enable = intelXMP.ProfileData1.Enable;
													dramtimingsObj.name = "";
													if (intelXMP.ProfileData1.Enable)
													{
														dramtimingsObj.name = Encoding.ASCII.GetString(intelXMP.ProfileData1.ProfileName);
														dramtimingsObj.frequency = intelXMP.ProfileData1.Frequency;
														dramtimingsObj.vdd = intelXMP.ProfileData1.VDD;
														dramtimingsObj.vddq = intelXMP.ProfileData1.VDDQ;
														dramtimingsObj.vpp = intelXMP.ProfileData1.VPP;
														dramtimingsObj.t_cl = intelXMP.ProfileData1.tCL;
														dramtimingsObj.t_rcd = intelXMP.ProfileData1.tRCD;
														dramtimingsObj.t_rp = intelXMP.ProfileData1.tRP;
														dramtimingsObj.t_ras = intelXMP.ProfileData1.tRAS;
														dramtimingsObj.t_rc = intelXMP.ProfileData1.tRC;
													}
													dramtimingTableObj.timing_table.Add(dramtimingsObj);
													DRAMTimingsObj dramtimingsObj2 = new DRAMTimingsObj();
													dramtimingsObj2.enable = intelXMP.ProfileData2.Enable;
													dramtimingsObj2.name = "";
													if (intelXMP.ProfileData2.Enable)
													{
														dramtimingsObj2.name = Encoding.ASCII.GetString(intelXMP.ProfileData2.ProfileName);
														dramtimingsObj2.frequency = intelXMP.ProfileData2.Frequency;
														dramtimingsObj2.vdd = intelXMP.ProfileData2.VDD;
														dramtimingsObj2.vddq = intelXMP.ProfileData2.VDDQ;
														dramtimingsObj2.vpp = intelXMP.ProfileData2.VPP;
														dramtimingsObj2.t_cl = intelXMP.ProfileData2.tCL;
														dramtimingsObj2.t_rcd = intelXMP.ProfileData2.tRCD;
														dramtimingsObj2.t_rp = intelXMP.ProfileData2.tRP;
														dramtimingsObj2.t_ras = intelXMP.ProfileData2.tRAS;
														dramtimingsObj2.t_rc = intelXMP.ProfileData2.tRC;
													}
													dramtimingTableObj.timing_table.Add(dramtimingsObj2);
													DRAMTimingsObj dramtimingsObj3 = new DRAMTimingsObj();
													dramtimingsObj3.enable = intelXMP.ProfileData3.Enable;
													dramtimingsObj3.name = "";
													if (intelXMP.ProfileData3.Enable)
													{
														dramtimingsObj3.name = Encoding.ASCII.GetString(intelXMP.ProfileData3.ProfileName);
														dramtimingsObj3.frequency = intelXMP.ProfileData3.Frequency;
														dramtimingsObj3.vdd = intelXMP.ProfileData3.VDD;
														dramtimingsObj3.vddq = intelXMP.ProfileData3.VDDQ;
														dramtimingsObj3.vpp = intelXMP.ProfileData3.VPP;
														dramtimingsObj3.t_cl = intelXMP.ProfileData3.tCL;
														dramtimingsObj3.t_rcd = intelXMP.ProfileData3.tRCD;
														dramtimingsObj3.t_rp = intelXMP.ProfileData3.tRP;
														dramtimingsObj3.t_ras = intelXMP.ProfileData3.tRAS;
														dramtimingsObj3.t_rc = intelXMP.ProfileData3.tRC;
													}
													dramtimingTableObj.timing_table.Add(dramtimingsObj3);
													FuryContorller_Service.dDRAMTimings.Add(FuryContorller_Service.dDRAMInfos.ElementAt(j).Key, dramtimingTableObj);
												}
											}
										}
									}
									if (FuryContorller_Service.dDRAMTimings.Count > 0)
									{
										for (int k = 0; k < FuryContorller_Service.dDRAMTimings.Count; k++)
										{
											byte b2 = (byte)(Miscellaneous.iDDRBaseAddress + FuryContorller_Service.dDRAMTimings.ElementAt(k).Value.address_offset);
											int num = 0;
											int num2 = 0;
											int num3 = 0;
											int num4 = 0;
											Class_DLL.GetKingstonFuryDDR5Temperature(b2, ref num4);
											Class_DLL.GetKingstonFuryDDR5VolSetting(b2, ref num, ref num2, ref num3);
											FuryContorller_Service.dDRAMTimings.ElementAt(k).Value.temperature = num4;
											FuryContorller_Service.dDRAMTimings.ElementAt(k).Value.vdd = (double)num / 1000.0;
											FuryContorller_Service.dDRAMTimings.ElementAt(k).Value.vddq = (double)num2 / 1000.0;
											FuryContorller_Service.dDRAMTimings.ElementAt(k).Value.vpp = (double)num3 / 1000.0;
										}
										text5 = JsonClass.JavaScriptSerialize<Dictionary<string, DRAMTimingTableObj>>(FuryContorller_Service.dDRAMTimings);
									}
									goto IL_0FBA;
								}
								catch (Exception ex4)
								{
									Log.LibLogWriter(string.Format("Get timings. ({0})", ex4.Message), true);
									return "{" + "\"root\":{" + "\"api\":\"get_dram_timings\"," + "\"status\":\"1\"," + "\"dram_timings\":null" + "}}";
								}
							}
							if (!FuryContorller_Service.bExpire)
							{
								return "{" + "\"root\":{" + "\"api\":\"get_dram_timings\"," + "\"status\":\"1\"," + "\"dram_timings\":null" + "}}";
							}
							IL_0FBA:;
						}
						return "{" + "\"root\":{" + "\"api\":\"get_dram_timings\"," + "\"status\":\"0\"," + "\"dram_timings\":" + text5 + "}}";
					}
					if (sData.IndexOf("\"get_dram_color_cache\"", StringComparison.InvariantCultureIgnoreCase) > 0)
					{
						string text6 = "null";
						string text7 = "null";
						object obj = FuryContorller_Service.OnPowerEvent_locker;
						lock (obj)
						{
							if (FuryContorller_Service.dDRAMInfos != null && FuryContorller_Service.dDRAMInfos.Count > 0 && (FuryContorller_Service.iAPIVer == 2 || FuryContorller_Service.iAPIVer == 3 || FuryContorller_Service.iAPIVer == 4))
							{
								Log.LibLogWriter(string.Format("Response DRAM color cache", Array.Empty<object>()), false);
								try
								{
									if (Miscellaneous.ddr5_bg_color_cache != null)
									{
										text6 = JsonClass.JavaScriptSerialize<Dictionary<string, List<int>>>(Miscellaneous.ddr5_bg_color_cache_out);
									}
									if (Miscellaneous.ddr5_color_table_cache != null)
									{
										text7 = JsonClass.JavaScriptSerialize<Dictionary<string, List<List<int>>>>(Miscellaneous.ddr5_color_table_cache_out);
									}
									goto IL_115D;
								}
								catch (Exception ex5)
								{
									Log.LibLogWriter(string.Format("Get color cache. ({0})", ex5.Message), true);
									return "{" + "\"root\":{" + "\"api\":\"get_dram_color_cache\"," + "\"status\":\"1\"," + "\"dram_background_color\":null," + "\"dram_color_table\":null" + "}}";
								}
							}
							if (!FuryContorller_Service.bExpire)
							{
								return "{" + "\"root\":{" + "\"api\":\"get_dram_color_cache\"," + "\"status\":\"1\"," + "\"dram_background_color\":null," + "\"dram_color_table\":null" + "}}";
							}
							IL_115D:;
						}
						return "{" + "\"root\":{" + "\"api\":\"get_dram_color_cache\"," + "\"status\":\"0\"," + "\"dram_background_color\":" + text6 + "," + "\"dram_color_table\":" + text7 + "}}";
					}
					if (sData.IndexOf("\"get_dram_led\"", StringComparison.InvariantCultureIgnoreCase) > 0)
					{
						if (FuryContorller_Service.KSDramSMBusCtrl.getDRAMCmdObj() == null)
						{
							return "{" + "\"root\":{" + "\"api\":\"get_dram_led\"," + "\"status\":\"1\"" + "}}";
						}
						string text8 = "";
						try
						{
							if (FuryContorller_Service.bIsAM4 && FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1 != null && FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings != null && FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.ctrl_color != null)
							{
								Dictionary<string, DRAMCtrlColorObj> dictionary2 = new Dictionary<string, DRAMCtrlColorObj>();
								for (int l = 0; l < FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.ctrl_color.Count; l++)
								{
									if (FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.ctrl_color.ElementAt(l).Value.index == 1)
									{
										DRAMCtrlColorObj dramctrlColorObj = new DRAMCtrlColorObj(2, FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.ctrl_color.ElementAt(l).Value.colors);
										dictionary2.Add("slot_2", dramctrlColorObj);
									}
									else if (FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.ctrl_color.ElementAt(l).Value.index == 2)
									{
										DRAMCtrlColorObj dramctrlColorObj2 = new DRAMCtrlColorObj(1, FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.ctrl_color.ElementAt(l).Value.colors);
										dictionary2.Add("slot_1", dramctrlColorObj2);
									}
									else
									{
										dictionary2.Add(FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.ctrl_color.ElementAt(l).Key, FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.ctrl_color.ElementAt(l).Value);
									}
								}
								text8 = JsonClass.JavaScriptSerialize<DRAMJsonRoot>(new DRAMJsonRoot("get_dram_led", "0", new DRAMCmdObj(FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.api, new DRAMCtrlObj(FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.mode, FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.ctrl_mode)))
								{
									root = 
									{
										ctrl_settings = 
										{
											speed = FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.speed,
											brightness = FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.brightness,
											darkness = FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1.ctrl_settings.darkness,
											ctrl_color = dictionary2
										}
									}
								});
							}
							else if ((FuryContorller_Service.iAPIVer == 2 || FuryContorller_Service.iAPIVer == 3 || FuryContorller_Service.iAPIVer == 4) && FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v2 != null)
							{
								text8 = JsonClass.JavaScriptSerialize<DRAMJsonRoot>(new DRAMJsonRoot("get_dram_led", "0", FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v2));
							}
							else if (FuryContorller_Service.iAPIVer == 1 && FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1 != null)
							{
								text8 = JsonClass.JavaScriptSerialize<DRAMJsonRoot>(new DRAMJsonRoot("get_dram_led", "0", FuryContorller_Service.KSDramSMBusCtrl._DRAMCmdObj_v1));
							}
						}
						catch (Exception ex6)
						{
							Log.LibLogWriter(string.Format("Get led serialize. ({0})", ex6.StackTrace), true);
						}
						if (text8 == "")
						{
							text8 = "{";
							text8 += "\"root\":{";
							text8 += "\"api\":\"get_dram_led\",";
							text8 += "\"status\":\"1\"";
							text8 += "}}";
							return text8;
						}
						return text8;
					}
					else if (sData.IndexOf("\"set_dram_led\"", StringComparison.InvariantCultureIgnoreCase) > 0)
					{
						DRAMJsonRoot dramjsonRoot3 = null;
						try
						{
							dramjsonRoot3 = JsonClass.JavaScriptDeserialize<DRAMJsonRoot>(sData);
							if (FuryContorller_Service.bIsAM4 && dramjsonRoot3.root != null && dramjsonRoot3.root.ctrl_settings != null && dramjsonRoot3.root.ctrl_settings.ctrl_color != null)
							{
								Dictionary<string, DRAMCtrlColorObj> dictionary3 = new Dictionary<string, DRAMCtrlColorObj>();
								for (int m = 0; m < dramjsonRoot3.root.ctrl_settings.ctrl_color.Count; m++)
								{
									if (dramjsonRoot3.root.ctrl_settings.ctrl_color.ElementAt(m).Value.index == 1)
									{
										DRAMCtrlColorObj dramctrlColorObj3 = new DRAMCtrlColorObj(2, dramjsonRoot3.root.ctrl_settings.ctrl_color.ElementAt(m).Value.colors);
										dictionary3.Add("slot_2", dramctrlColorObj3);
									}
									else if (dramjsonRoot3.root.ctrl_settings.ctrl_color.ElementAt(m).Value.index == 2)
									{
										DRAMCtrlColorObj dramctrlColorObj4 = new DRAMCtrlColorObj(1, dramjsonRoot3.root.ctrl_settings.ctrl_color.ElementAt(m).Value.colors);
										dictionary3.Add("slot_1", dramctrlColorObj4);
									}
									else
									{
										dictionary3.Add(dramjsonRoot3.root.ctrl_settings.ctrl_color.ElementAt(m).Key, dramjsonRoot3.root.ctrl_settings.ctrl_color.ElementAt(m).Value);
									}
								}
								dramjsonRoot3.root.ctrl_settings.ctrl_color = dictionary3;
							}
						}
						catch (Exception ex7)
						{
							Log.LibLogWriter(string.Format("Set led deserialize. ({0})", ex7.Message), true);
						}
						int num5 = 1;
						if (dramjsonRoot3 != null)
						{
							try
							{
								if (FuryContorller_Service.KSDramSMBusCtrl != null)
								{
									num5 = FuryContorller_Service.KSDramSMBusCtrl.setDRAMCmdObj(dramjsonRoot3.root);
									if (num5 == 0)
									{
										if (FuryContorller_Service.iAPIVer == 1 || FuryContorller_Service.iAPIVer == 2 || FuryContorller_Service.iAPIVer == 3 || FuryContorller_Service.iAPIVer == 4)
										{
											if (FuryContorller_Service.KSDramSMBusCtrl.applyDRAMLEDmode(FuryContorller_Service.iAPIVer, false))
											{
												try
												{
													Log.WriteLEDProfile(JsonClass.JavaScriptSerialize<DRAMJsonRoot>(dramjsonRoot3));
													Miscellaneous.SaveDDR5ColorCache();
												}
												catch (Exception)
												{
												}
												num5 = 0;
											}
											else
											{
												num5 = 5;
											}
										}
										else
										{
											num5 = 5;
										}
									}
								}
								else
								{
									num5 = 4;
								}
							}
							catch (Exception)
							{
							}
							return "{" + "\"root\":{" + "\"api\":\"set_dram_led\"," + "\"status\":\"" + num5.ToString() + "\"" + "}}";
						}
						return "{" + "\"root\":{" + "\"api\":\"set_dram_led\"," + "\"status\":\"3\"" + "}}";
					}
					else
					{
						if (sData.IndexOf("\"get_is8dimm_status\"", StringComparison.InvariantCultureIgnoreCase) > 0)
						{
							return "{" + "\"root\":{" + "\"api\":\"get_is8dimm_status\"," + "\"status\":\"0\"," + "\"is8dimm\":\"" + (FuryContorller_Service.bIs8Dimm ? "1" : "0") + "\"" + "}}";
						}
						Log.LibLogWriter(string.Format("Some strange request. ({0})", sData), true);
					}
				}
				catch (Exception ex8)
				{
					Log.LibLogWriter(string.Format("On request throw. ({0})", ex8.Message), true);
				}
				return "";
			}
			return "";
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00013CD8 File Offset: 0x00011ED8
		private bool CheckFURYCTRLrunning()
		{
			return Process.GetProcessesByName("FURYCTRL").Length != 0;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00013CEC File Offset: 0x00011EEC
		private void KeepDramLEDEffectFeature()
		{
			Log.LibCmdLogWriter("***** Keep LED effect Feature START *****", false);
			if (FuryContorller_Service.bKeep || this.CheckFURYCTRLrunning())
			{
				Log.LibCmdLogWriter("Load led profile start.", false);
				string text = Log.ReadLEDProfile();
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						DRAMJsonRoot dramjsonRoot = JsonClass.JavaScriptDeserialize<DRAMJsonRoot>(text);
						Log.LibCmdLogWriter("Set led after load led profile.", false);
						if (dramjsonRoot != null)
						{
							if (FuryContorller_Service.iAPIVer > 0 && FuryContorller_Service.iAPIVer != 255)
							{
								if (FuryContorller_Service.iAPIVer == 2 || FuryContorller_Service.iAPIVer == 3 || FuryContorller_Service.iAPIVer == 4)
								{
									Log.LibCmdLogWriter("Load led color cache start.", false);
									string text2 = Log.ReadLEDColorTableCache();
									if (!string.IsNullOrEmpty(text2))
									{
										Dictionary<string, List<List<int>>> dictionary = JsonClass.JavaScriptDeserialize<Dictionary<string, List<List<int>>>>(text2);
										if (dictionary != null)
										{
											Miscellaneous.ddr5_color_table_cache = dictionary;
											if (dictionary.Count > 0 && Miscellaneous.ddr5_color_table_cache_out.Count > 0)
											{
												for (int i = 0; i < Miscellaneous.ddr5_color_table_cache_out.ElementAt(0).Value.Count; i++)
												{
													if (dictionary.ElementAt(0).Value.Count > i && Miscellaneous.ddr5_color_table_cache_out.ElementAt(0).Value.ElementAt(i).Count >= 3 && dictionary.ElementAt(0).Value.ElementAt(i).Count >= 3)
													{
														Miscellaneous.ddr5_color_table_cache_out.ElementAt(0).Value.ElementAt(i)[0] = dictionary.ElementAt(0).Value.ElementAt(i)[0];
														Miscellaneous.ddr5_color_table_cache_out.ElementAt(0).Value.ElementAt(i)[1] = dictionary.ElementAt(0).Value.ElementAt(i)[1];
														Miscellaneous.ddr5_color_table_cache_out.ElementAt(0).Value.ElementAt(i)[2] = dictionary.ElementAt(0).Value.ElementAt(i)[2];
													}
												}
											}
										}
									}
									string text3 = Log.ReadLEDBGColorCache();
									if (!string.IsNullOrEmpty(text3))
									{
										Dictionary<string, List<int>> dictionary2 = JsonClass.JavaScriptDeserialize<Dictionary<string, List<int>>>(text3);
										if (dictionary2 != null)
										{
											Miscellaneous.ddr5_bg_color_cache = dictionary2;
											if (dictionary2.Count > 0 && Miscellaneous.ddr5_bg_color_cache_out.Count > 0)
											{
												for (int j = 0; j < Miscellaneous.ddr5_bg_color_cache_out.ElementAt(0).Value.Count; j++)
												{
													if (dictionary2.ElementAt(0).Value.Count > j && Miscellaneous.ddr5_bg_color_cache_out.ElementAt(0).Value.Count >= 3 && dictionary2.ElementAt(0).Value.Count >= 3)
													{
														Miscellaneous.ddr5_bg_color_cache_out.ElementAt(0).Value[0] = dictionary2.ElementAt(0).Value[0];
														Miscellaneous.ddr5_bg_color_cache_out.ElementAt(0).Value[1] = dictionary2.ElementAt(0).Value[1];
														Miscellaneous.ddr5_bg_color_cache_out.ElementAt(0).Value[2] = dictionary2.ElementAt(0).Value[2];
													}
												}
											}
										}
									}
									Log.LibCmdLogWriter("Load led color cache successful.", false);
								}
								if (FuryContorller_Service.KSDramSMBusCtrl.setDRAMCmdObj(dramjsonRoot.root) == 0 && (FuryContorller_Service.iAPIVer == 1 || FuryContorller_Service.iAPIVer == 2 || FuryContorller_Service.iAPIVer == 3 || FuryContorller_Service.iAPIVer == 4) && FuryContorller_Service.KSDramSMBusCtrl.applyDRAMLEDmode(FuryContorller_Service.iAPIVer, true))
								{
									Log.LibCmdLogWriter("Set led after load led profile successful.", false);
								}
							}
							else
							{
								Log.LibCmdLogWriter("Two different RGB API in the same time.", false);
							}
						}
						goto IL_0491;
					}
					catch (Exception ex)
					{
						Log.LibCmdLogWriter(string.Format("Read led profile deserialize. ({0})", ex.Message), true);
						goto IL_0491;
					}
				}
				Log.LibCmdLogWriter("Load default effect.", false);
				if (FuryContorller_Service.iAPIVer == 2 || FuryContorller_Service.iAPIVer == 4)
				{
					FuryContorller_Service.KSDramSMBusCtrl.setDRAMDDR5_DefaultRainbow();
				}
				else if (FuryContorller_Service.iAPIVer == 3)
				{
					FuryContorller_Service.KSDramSMBusCtrl.setDRAMDDR5_DefaultRacing();
				}
			}
			else
			{
				Log.LibCmdLogWriter("keep feature disable.", false);
				Log.LibCmdLogWriter("Load default effect.", false);
				if (FuryContorller_Service.iAPIVer == 2 || FuryContorller_Service.iAPIVer == 4)
				{
					FuryContorller_Service.KSDramSMBusCtrl.setDRAMDDR5_DefaultRainbow();
				}
				else if (FuryContorller_Service.iAPIVer == 3)
				{
					FuryContorller_Service.KSDramSMBusCtrl.setDRAMDDR5_DefaultRacing();
				}
			}
			IL_0491:
			Log.LibCmdLogWriter("***** Keep LED effect Feature END *****", false);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000141B4 File Offset: 0x000123B4
		protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
		{
			if (powerStatus == PowerBroadcastStatus.Suspend || powerStatus == PowerBroadcastStatus.QuerySuspend)
			{
				object obj = FuryContorller_Service.OnPowerEvent_locker;
				lock (obj)
				{
					Log.LibCmdLogWriter("*********** Suspend and QuerySuspend Feature START ***********", false);
					try
					{
						if (!this.bSuspend)
						{
							this.bSuspend = true;
							FuryContorller_Service.KSDramSMBusCtrl.setDRAMDDR5_AllOff();
						}
					}
					catch (Exception ex)
					{
						Log.LibCmdLogWriter(string.Format("Suspend and QuerySuspend Feature. ({0})", ex.Message), true);
					}
					Log.LibCmdLogWriter("*********** Suspend and QuerySuspend Feature END ***********", false);
					goto IL_0075;
				}
			}
			this.bSuspend = false;
			IL_0075:
			if (powerStatus == PowerBroadcastStatus.ResumeSuspend || powerStatus == PowerBroadcastStatus.ResumeAutomatic)
			{
				object obj = FuryContorller_Service.OnPowerEvent_locker;
				lock (obj)
				{
					Log.LibCmdLogWriter("*********** Resume Suspend and Automatic Feature START ***********", false);
					try
					{
						if (!this.bResumed)
						{
							this.bResumed = true;
							Log.LibCmdLogWriter("Initialize start.", false);
							Class_DLL.InitialSMBusDriver();
							FuryContorller_Service.bIs8Dimm = Class_DLL.Is8Dimm();
							Miscellaneous.bIs8Dimm = FuryContorller_Service.bIs8Dimm;
							Log.LibCmdLogWriter(string.Format("Special DIMM : {0}", FuryContorller_Service.bIs8Dimm ? "true" : "false"), false);
							FuryContorller_Service.bIsAM4 = Class_DLL.IsAM4();
							Log.LibCmdLogWriter(string.Format("AM4 CPU : {0}", FuryContorller_Service.bIsAM4 ? "true" : "false"), false);
							this.bIsX299 = Class_DLL.IsX299();
							Log.LibCmdLogWriter(string.Format("X299 series : {0}", this.bIsX299 ? "true" : "false"), false);
							bool flag2 = this.CheckExpireTime();
							if (flag2)
							{
								this.CheckDramSupport();
							}
							Log.LibCmdLogWriter("Create dram led control object start.", false);
							FuryContorller_Service.KSDramSMBusCtrl = new KSDRAMSMBusCtrlObj(FuryContorller_Service.dDRAMInfos, flag2);
							this.KeepDramLEDEffectFeature();
						}
					}
					catch (Exception ex2)
					{
						Log.LibCmdLogWriter(string.Format("Resume Suspend and Automatic Feature. ({0})", ex2.Message), true);
					}
					Log.LibCmdLogWriter("*********** Resume Suspend and Automatic Feature END ***********", false);
					goto IL_01BE;
				}
			}
			this.bResumed = false;
			IL_01BE:
			return base.OnPowerEvent(powerStatus);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000143EC File Offset: 0x000125EC
		private static void CreateTask(_TASK_RUNLEVEL taskRunLevel, string sName, string sPath, string sArg)
		{
			ITaskService taskService = (global::TaskScheduler.TaskScheduler)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("0F87369F-A4E5-4CFC-BD3E-73E6154572DD")));
			taskService.Connect(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
			ITaskFolder folder = taskService.GetFolder("\\");
			IRegisteredTaskCollection tasks = folder.GetTasks(1);
			bool flag = false;
			foreach (object obj in tasks)
			{
				IRegisteredTask registeredTask = (IRegisteredTask)obj;
				if (registeredTask.Name == sName)
				{
					if (!registeredTask.Enabled)
					{
						registeredTask.Enabled = true;
					}
					if (registeredTask.State == _TASK_STATE.TASK_STATE_RUNNING)
					{
						flag = true;
						break;
					}
					folder.DeleteTask(sName, 0);
					break;
				}
			}
			if (!flag)
			{
				string text = WindowsBuiltInRole.Administrator.ToString();
				text = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null).Translate(typeof(NTAccount)).ToString();
				text = text.Substring(text.IndexOf("\\") + 1);
				ITaskDefinition taskDefinition = taskService.NewTask(0U);
				taskDefinition.RegistrationInfo.Author = "Kingston";
				taskDefinition.RegistrationInfo.Description = sName;
				taskDefinition.Principal.RunLevel = taskRunLevel;
				taskDefinition.Principal.GroupId = string.Concat(new string[] { text });
				taskDefinition.Principal.LogonType = _TASK_LOGON_TYPE.TASK_LOGON_GROUP;
				taskDefinition.Settings.Enabled = true;
				taskDefinition.Settings.Hidden = true;
				taskDefinition.Settings.StopIfGoingOnBatteries = false;
				taskDefinition.Settings.DisallowStartIfOnBatteries = false;
				IActionCollection actions = taskDefinition.Actions;
				IExecAction execAction = actions.Create(_TASK_ACTION_TYPE.TASK_ACTION_EXEC) as IExecAction;
				execAction.Path = sPath;
				execAction.Arguments = sArg;
				taskDefinition.Actions = actions;
				try
				{
					Console.WriteLine("Task run!");
					folder.RegisterTaskDefinition(sName, taskDefinition, 6, null, null, _TASK_LOGON_TYPE.TASK_LOGON_S4U, null);
					Console.WriteLine("RegisterTaskDefinition finish!");
					folder.GetTask(sName).Run(null);
					Console.WriteLine("Task run finish!");
				}
				catch (Exception ex)
				{
					Log.LibLogWriter(ex.Message, false);
					Console.WriteLine(ex.Message.ToString());
				}
				Thread.Sleep(50);
				folder.DeleteTask(sName, 0);
				Marshal.ReleaseComObject(taskDefinition);
				Marshal.ReleaseComObject(folder);
				Marshal.ReleaseComObject(actions);
				Marshal.ReleaseComObject(taskService);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00014660 File Offset: 0x00012860
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0001467F File Offset: 0x0001287F
		private void InitializeComponent()
		{
			base.CanHandlePowerEvent = true;
			base.CanShutdown = true;
			base.ServiceName = "FuryContorller_Service";
		}

		// Token: 0x04000135 RID: 309
		public static readonly string sAppVersion = string.Concat(new string[]
		{
			Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(),
			".",
			Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString(),
			".",
			Assembly.GetExecutingAssembly().GetName().Version.Build.ToString(),
			".",
			Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString().PadLeft(2, '0')
		});

		// Token: 0x04000136 RID: 310
		private static KSDRAMSMBusCtrlObj KSDramSMBusCtrl = null;

		// Token: 0x04000137 RID: 311
		private static Dictionary<string, DRAMInfoObj> dDRAMInfos = new Dictionary<string, DRAMInfoObj>();

		// Token: 0x04000138 RID: 312
		private static Dictionary<string, DRAMInfoObj> dDRAMInfos_cache = new Dictionary<string, DRAMInfoObj>();

		// Token: 0x04000139 RID: 313
		private static Dictionary<string, DRAMTimingTableObj> dDRAMTimings = new Dictionary<string, DRAMTimingTableObj>();

		// Token: 0x0400013A RID: 314
		private WebSocketServer ws;

		// Token: 0x0400013B RID: 315
		public static object request_locker = new object();

		// Token: 0x0400013C RID: 316
		private static bool bKeep = true;

		// Token: 0x0400013D RID: 317
		private static bool bIs8Dimm = true;

		// Token: 0x0400013E RID: 318
		private static bool bIsAM4 = false;

		// Token: 0x0400013F RID: 319
		private bool bIsX299;

		// Token: 0x04000140 RID: 320
		private static bool bExpire = false;

		// Token: 0x04000141 RID: 321
		private int iDDRDeviceCount;

		// Token: 0x04000142 RID: 322
		private static int iDDRType = 0;

		// Token: 0x04000143 RID: 323
		private static int iAPIVer = 0;

		// Token: 0x04000144 RID: 324
		private bool bSuspend;

		// Token: 0x04000145 RID: 325
		private bool bResumed;

		// Token: 0x04000146 RID: 326
		private static bool IsCR_0B_Error = false;

		// Token: 0x04000147 RID: 327
		public static object Lib_locker = new object();

		// Token: 0x04000148 RID: 328
		public static object OnPowerEvent_locker = new object();

		// Token: 0x04000149 RID: 329
		private static Mutex _mutex = null;

		// Token: 0x0400014A RID: 330
		private static string sServiceMutexGUID = "895B1B8D-0461-4D22-9000-AC922630DF03";

		// Token: 0x0400014B RID: 331
		private static string sAppxPackageName = "KingstonTechnologyCompany.FURYCTRL_5myjd26we8sq4";

		// Token: 0x0400014C RID: 332
		private FileSystemWatcher AppxPackageProgramDataFile_Watcher;

		// Token: 0x0400014D RID: 333
		private Class_Mutex class_mutex = new Class_Mutex();

		// Token: 0x0400014E RID: 334
		public static IntPtr IntPtr_DRAM = IntPtr.Zero;

		// Token: 0x0400014F RID: 335
		private IContainer components;
	}
}
