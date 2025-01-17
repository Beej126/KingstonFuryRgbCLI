using System;
using System.Reflection;
using System.ServiceProcess;

namespace FuryControllerService
{
	// Token: 0x0200002E RID: 46
	internal static class Program
	{
		// Token: 0x06000181 RID: 385 RVA: 0x000101F4 File Offset: 0x0000E3F4
		private static void Main(string[] args)
		{
			try
			{
				if (Environment.UserInteractive)
				{
					string text = string.Concat(args);
					if (text == "-ksinstall")
					{
						Log.LibCmdLogWriter("install start", false);
						ServiceInstaller.InstallAndStart("FuryContorller_Service", "FuryContorller_Service", "\"" + Assembly.GetExecutingAssembly().Location + "\"");
						goto IL_0098;
					}
					if (!(text == "-ksuninstall"))
					{
						goto IL_0098;
					}
					Log.LibCmdLogWriter("uninstall start", false);
					ServiceInstaller.Uninstall("FuryContorller_Service");
					try
					{
						Class_DLL.InitialSMBusDriver();
						Class_DLL.ReleaseDll();
						goto IL_0098;
					}
					catch (Exception)
					{
						goto IL_0098;
					}
				}
				ServiceBase.Run(new ServiceBase[]
				{
					new FuryContorller_Service()
				});
				IL_0098:;
			}
			catch (Exception)
			{
			}
		}
	}
}
