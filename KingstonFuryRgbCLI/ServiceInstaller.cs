using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace KingstonFuryRgbCLI
{
	// Token: 0x02000030 RID: 48
	public static class ServiceInstaller
	{
		// Token: 0x06000197 RID: 407
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "OpenSCManagerW", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr OpenSCManager(string machineName, string databaseName, ServiceInstaller.ScmAccessRights dwDesiredAccess);

		// Token: 0x06000198 RID: 408
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceInstaller.ServiceAccessRights dwDesiredAccess);

		// Token: 0x06000199 RID: 409
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, ServiceInstaller.ServiceAccessRights dwDesiredAccess, int dwServiceType, ServiceInstaller.ServiceBootFlag dwStartType, ServiceInstaller.ServiceError dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup, IntPtr lpdwTagId, string lpDependencies, string lp, string lpPassword);

		// Token: 0x0600019A RID: 410
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool ChangeServiceConfig2(IntPtr hService, ServiceInstaller.ServiceInfoLevel dwInfoLevel, ref ServiceInstaller.SERVICE_DESCRIPTION lpInfo);

		// Token: 0x0600019B RID: 411
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseServiceHandle(IntPtr hSCObject);

		// Token: 0x0600019C RID: 412
		[DllImport("advapi32.dll")]
		private static extern int QueryServiceStatus(IntPtr hService, ServiceInstaller.SERVICE_STATUS lpServiceStatus);

		// Token: 0x0600019D RID: 413
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool DeleteService(IntPtr hService);

		// Token: 0x0600019E RID: 414
		[DllImport("advapi32.dll")]
		private static extern int ControlService(IntPtr hService, ServiceInstaller.ServiceControl dwControl, ServiceInstaller.SERVICE_STATUS lpServiceStatus);

		// Token: 0x0600019F RID: 415
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int StartService(IntPtr hService, int dwNumServiceArgs, int lpServiceArgVectors);

		// Token: 0x060001A0 RID: 416 RVA: 0x00014A64 File Offset: 0x00012C64
		public static void Uninstall(string serviceName)
		{
			IntPtr intPtr = ServiceInstaller.OpenSCManager(ServiceInstaller.ScmAccessRights.AllAccess);
			try
			{
				IntPtr intPtr2 = ServiceInstaller.OpenService(intPtr, serviceName, ServiceInstaller.ServiceAccessRights.AllAccess);
				if (intPtr2 == IntPtr.Zero)
				{
					throw new ApplicationException("Service not installed.");
				}
				try
				{
					ServiceInstaller.StopService(intPtr2);
					if (!ServiceInstaller.DeleteService(intPtr2))
					{
						throw new ApplicationException("Could not delete service " + Marshal.GetLastWin32Error().ToString());
					}
				}
				catch (Exception ex)
				{
					Log.LibCmdLogWriter(ex.Message, false);
				}
				finally
				{
					ServiceInstaller.CloseServiceHandle(intPtr2);
				}
			}
			finally
			{
				ServiceInstaller.CloseServiceHandle(intPtr);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00014B14 File Offset: 0x00012D14
		public static bool ServiceIsInstalled(string serviceName)
		{
			IntPtr intPtr = ServiceInstaller.OpenSCManager(ServiceInstaller.ScmAccessRights.Connect);
			bool flag;
			try
			{
				IntPtr intPtr2 = ServiceInstaller.OpenService(intPtr, serviceName, ServiceInstaller.ServiceAccessRights.QueryStatus);
				if (intPtr2 == IntPtr.Zero)
				{
					flag = false;
				}
				else
				{
					ServiceInstaller.CloseServiceHandle(intPtr2);
					flag = true;
				}
			}
			finally
			{
				ServiceInstaller.CloseServiceHandle(intPtr);
			}
			return flag;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00014B68 File Offset: 0x00012D68
		public static void InstallAndStart(string serviceName, string displayName, string fileName)
		{
			IntPtr intPtr = ServiceInstaller.OpenSCManager(ServiceInstaller.ScmAccessRights.AllAccess);
			try
			{
				IntPtr intPtr2 = ServiceInstaller.OpenService(intPtr, serviceName, ServiceInstaller.ServiceAccessRights.AllAccess);
				if (intPtr2 == IntPtr.Zero)
				{
					intPtr2 = ServiceInstaller.CreateService(intPtr, serviceName, displayName, ServiceInstaller.ServiceAccessRights.AllAccess, 16, ServiceInstaller.ServiceBootFlag.AutoStart, ServiceInstaller.ServiceError.Normal, fileName, null, IntPtr.Zero, null, null, null);
				}
				if (intPtr2 == IntPtr.Zero)
				{
					throw new ApplicationException("Failed to install service.");
				}
				ServiceInstaller.SERVICE_DESCRIPTION service_DESCRIPTION = default;
				service_DESCRIPTION.lpDescription = serviceName;
				ServiceInstaller.ChangeServiceConfig2(intPtr2, ServiceInstaller.ServiceInfoLevel.SERVICE_CONFIG_DESCRIPTION, ref service_DESCRIPTION);
				try
				{
					ServiceInstaller.StartService(intPtr2);
				}
				finally
				{
					ServiceInstaller.CloseServiceHandle(intPtr2);
				}
			}
			finally
			{
				ServiceInstaller.CloseServiceHandle(intPtr);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00014C1C File Offset: 0x00012E1C
		public static void StartService(string serviceName)
		{
			IntPtr intPtr = ServiceInstaller.OpenSCManager(ServiceInstaller.ScmAccessRights.Connect);
			try
			{
				IntPtr intPtr2 = ServiceInstaller.OpenService(intPtr, serviceName, ServiceInstaller.ServiceAccessRights.QueryStatus | ServiceInstaller.ServiceAccessRights.Start);
				if (intPtr2 == IntPtr.Zero)
				{
					throw new ApplicationException("Could not open service.");
				}
				try
				{
					ServiceInstaller.StartService(intPtr2);
				}
				finally
				{
					ServiceInstaller.CloseServiceHandle(intPtr2);
				}
			}
			finally
			{
				ServiceInstaller.CloseServiceHandle(intPtr);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00014C88 File Offset: 0x00012E88
		public static void StopService(string serviceName)
		{
			IntPtr intPtr = ServiceInstaller.OpenSCManager(ServiceInstaller.ScmAccessRights.Connect);
			try
			{
				IntPtr intPtr2 = ServiceInstaller.OpenService(intPtr, serviceName, ServiceInstaller.ServiceAccessRights.QueryStatus | ServiceInstaller.ServiceAccessRights.Stop);
				if (intPtr2 == IntPtr.Zero)
				{
					throw new ApplicationException("Could not open service.");
				}
				try
				{
					ServiceInstaller.StopService(intPtr2);
				}
				finally
				{
					ServiceInstaller.CloseServiceHandle(intPtr2);
				}
			}
			finally
			{
				ServiceInstaller.CloseServiceHandle(intPtr);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00014CF4 File Offset: 0x00012EF4
		private static void StartService(IntPtr service)
		{
			new ServiceInstaller.SERVICE_STATUS();
			ServiceInstaller.StartService(service, 0, 0);
			if (!ServiceInstaller.WaitForServiceStatus(service, ServiceInstaller.ServiceState.StartPending, ServiceInstaller.ServiceState.Running))
			{
				throw new ApplicationException("Unable to start service");
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00014D1C File Offset: 0x00012F1C
		private static void StopService(IntPtr service)
		{
			ServiceInstaller.SERVICE_STATUS service_STATUS = new ServiceInstaller.SERVICE_STATUS();
			ServiceInstaller.ControlService(service, ServiceInstaller.ServiceControl.Stop, service_STATUS);
			if (!ServiceInstaller.WaitForServiceStatus(service, ServiceInstaller.ServiceState.StopPending, ServiceInstaller.ServiceState.Stopped))
			{
				throw new ApplicationException("Unable to stop service");
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00014D50 File Offset: 0x00012F50
		public static ServiceInstaller.ServiceState GetServiceStatus(string serviceName)
		{
			IntPtr intPtr = ServiceInstaller.OpenSCManager(ServiceInstaller.ScmAccessRights.Connect);
			ServiceInstaller.ServiceState serviceState;
			try
			{
				IntPtr intPtr2 = ServiceInstaller.OpenService(intPtr, serviceName, ServiceInstaller.ServiceAccessRights.QueryStatus);
				if (intPtr2 == IntPtr.Zero)
				{
					serviceState = ServiceInstaller.ServiceState.NotFound;
				}
				else
				{
					try
					{
						serviceState = ServiceInstaller.GetServiceStatus(intPtr2);
					}
					finally
					{
						ServiceInstaller.CloseServiceHandle(intPtr2);
					}
				}
			}
			finally
			{
				ServiceInstaller.CloseServiceHandle(intPtr);
			}
			return serviceState;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00014DB8 File Offset: 0x00012FB8
		private static ServiceInstaller.ServiceState GetServiceStatus(IntPtr service)
		{
			ServiceInstaller.SERVICE_STATUS service_STATUS = new ServiceInstaller.SERVICE_STATUS();
			if (ServiceInstaller.QueryServiceStatus(service, service_STATUS) == 0)
			{
				throw new ApplicationException("Failed to query service status.");
			}
			return service_STATUS.dwCurrentState;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00014DE8 File Offset: 0x00012FE8
		private static bool WaitForServiceStatus(IntPtr service, ServiceInstaller.ServiceState waitStatus, ServiceInstaller.ServiceState desiredStatus)
		{
			ServiceInstaller.SERVICE_STATUS service_STATUS = new ServiceInstaller.SERVICE_STATUS();
			ServiceInstaller.QueryServiceStatus(service, service_STATUS);
			if (service_STATUS.dwCurrentState == desiredStatus)
			{
				return true;
			}
			int num = Environment.TickCount;
			int num2 = service_STATUS.dwCheckPoint;
			while (service_STATUS.dwCurrentState == waitStatus)
			{
				int num3 = service_STATUS.dwWaitHint / 10;
				if (num3 < 1000)
				{
					num3 = 1000;
				}
				else if (num3 > 10000)
				{
					num3 = 10000;
				}
				Thread.Sleep(num3);
				if (ServiceInstaller.QueryServiceStatus(service, service_STATUS) == 0)
				{
					break;
				}
				if (service_STATUS.dwCheckPoint > num2)
				{
					num = Environment.TickCount;
					num2 = service_STATUS.dwCheckPoint;
				}
				else if (Environment.TickCount - num > 10000)
				{
					break;
				}
			}
			return service_STATUS.dwCurrentState == desiredStatus;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00014E8C File Offset: 0x0001308C
		private static IntPtr OpenSCManager(ServiceInstaller.ScmAccessRights rights)
		{
			IntPtr intPtr = ServiceInstaller.OpenSCManager(null, null, rights);
			if (intPtr == IntPtr.Zero)
			{
				throw new ApplicationException("Could not connect to service control manager.");
			}
			return intPtr;
		}

		// Token: 0x04000150 RID: 336
		//private const int STANDARD_RIGHTS_REQUIRED = 983040;

		// Token: 0x04000151 RID: 337
		//private const int SERVICE_WIN32_OWN_PROCESS = 16;

		// Token: 0x0200004A RID: 74
		[StructLayout(LayoutKind.Sequential)]
		private class SERVICE_STATUS
		{
			// Token: 0x04000194 RID: 404
			public int dwServiceType;

			// Token: 0x04000195 RID: 405
			public ServiceInstaller.ServiceState dwCurrentState;

			// Token: 0x04000196 RID: 406
			public int dwControlsAccepted;

			// Token: 0x04000197 RID: 407
			public int dwWin32ExitCode;

			// Token: 0x04000198 RID: 408
			public int dwServiceSpecificExitCode;

			// Token: 0x04000199 RID: 409
			public int dwCheckPoint;

			// Token: 0x0400019A RID: 410
			public int dwWaitHint = 10000;
		}

		// Token: 0x0200004B RID: 75
		public enum ServiceState
		{
			// Token: 0x0400019C RID: 412
			Unknown = -1,
			// Token: 0x0400019D RID: 413
			NotFound,
			// Token: 0x0400019E RID: 414
			Stopped,
			// Token: 0x0400019F RID: 415
			StartPending,
			// Token: 0x040001A0 RID: 416
			StopPending,
			// Token: 0x040001A1 RID: 417
			Running,
			// Token: 0x040001A2 RID: 418
			ContinuePending,
			// Token: 0x040001A3 RID: 419
			PausePending,
			// Token: 0x040001A4 RID: 420
			Paused
		}

		// Token: 0x0200004C RID: 76
		[Flags]
		public enum ScmAccessRights
		{
			// Token: 0x040001A6 RID: 422
			Connect = 1,
			// Token: 0x040001A7 RID: 423
			CreateService = 2,
			// Token: 0x040001A8 RID: 424
			EnumerateService = 4,
			// Token: 0x040001A9 RID: 425
			Lock = 8,
			// Token: 0x040001AA RID: 426
			QueryLockStatus = 16,
			// Token: 0x040001AB RID: 427
			ModifyBootConfig = 32,
			// Token: 0x040001AC RID: 428
			StandardRightsRequired = 983040,
			// Token: 0x040001AD RID: 429
			AllAccess = 983103
		}

		// Token: 0x0200004D RID: 77
		[Flags]
		public enum ServiceAccessRights
		{
			// Token: 0x040001AF RID: 431
			QueryConfig = 1,
			// Token: 0x040001B0 RID: 432
			ChangeConfig = 2,
			// Token: 0x040001B1 RID: 433
			QueryStatus = 4,
			// Token: 0x040001B2 RID: 434
			EnumerateDependants = 8,
			// Token: 0x040001B3 RID: 435
			Start = 16,
			// Token: 0x040001B4 RID: 436
			Stop = 32,
			// Token: 0x040001B5 RID: 437
			PauseContinue = 64,
			// Token: 0x040001B6 RID: 438
			Interrogate = 128,
			// Token: 0x040001B7 RID: 439
			UserDefinedControl = 256,
			// Token: 0x040001B8 RID: 440
			Delete = 65536,
			// Token: 0x040001B9 RID: 441
			StandardRightsRequired = 983040,
			// Token: 0x040001BA RID: 442
			AllAccess = 983551
		}

		// Token: 0x0200004E RID: 78
		public enum ServiceBootFlag
		{
			// Token: 0x040001BC RID: 444
			Start,
			// Token: 0x040001BD RID: 445
			SystemStart,
			// Token: 0x040001BE RID: 446
			AutoStart,
			// Token: 0x040001BF RID: 447
			DemandStart,
			// Token: 0x040001C0 RID: 448
			Disabled
		}

		// Token: 0x0200004F RID: 79
		public enum ServiceControl
		{
			// Token: 0x040001C2 RID: 450
			Stop = 1,
			// Token: 0x040001C3 RID: 451
			Pause,
			// Token: 0x040001C4 RID: 452
			Continue,
			// Token: 0x040001C5 RID: 453
			Interrogate,
			// Token: 0x040001C6 RID: 454
			Shutdown,
			// Token: 0x040001C7 RID: 455
			ParamChange,
			// Token: 0x040001C8 RID: 456
			NetBindAdd,
			// Token: 0x040001C9 RID: 457
			NetBindRemove,
			// Token: 0x040001CA RID: 458
			NetBindEnable,
			// Token: 0x040001CB RID: 459
			NetBindDisable
		}

		// Token: 0x02000050 RID: 80
		public enum ServiceError
		{
			// Token: 0x040001CD RID: 461
			Ignore,
			// Token: 0x040001CE RID: 462
			Normal,
			// Token: 0x040001CF RID: 463
			Severe,
			// Token: 0x040001D0 RID: 464
			Critical
		}

		// Token: 0x02000051 RID: 81
		public enum ServiceInfoLevel
		{
			// Token: 0x040001D2 RID: 466
			SERVICE_CONFIG_DESCRIPTION = 1,
			// Token: 0x040001D3 RID: 467
			SERVICE_CONFIG_FAILURE_ACTIONS,
			// Token: 0x040001D4 RID: 468
			SERVICE_CONFIG_DELAYED_AUTO_START_INFO,
			// Token: 0x040001D5 RID: 469
			SERVICE_CONFIG_FAILURE_ACTIONS_FLAG,
			// Token: 0x040001D6 RID: 470
			SERVICE_CONFIG_SERVICE_SID_INFO,
			// Token: 0x040001D7 RID: 471
			SERVICE_CONFIG_REQUIRED_PRIVILEGES_INFO,
			// Token: 0x040001D8 RID: 472
			SERVICE_CONFIG_PRESHUTDOWN_INFO,
			// Token: 0x040001D9 RID: 473
			SERVICE_CONFIG_TRIGGER_INFO,
			// Token: 0x040001DA RID: 474
			SERVICE_CONFIG_PREFERRED_NODE,
			// Token: 0x040001DB RID: 475
			SERVICE_CONFIG_LAUNCH_PROTECTED = 12
		}

		// Token: 0x02000052 RID: 82
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct SERVICE_DESCRIPTION
		{
			// Token: 0x040001DC RID: 476
			public string lpDescription;
		}
	}
}
