using System;
using System.Runtime.InteropServices;

namespace FuryControllerService
{
	// Token: 0x02000003 RID: 3
	internal sealed class Native
	{
		// Token: 0x0600003A RID: 58
		[DllImport("kernel32.dll")]
		public static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);

		// Token: 0x0600003B RID: 59
		[DllImport("kernel32.dll")]
		public static extern bool ReleaseMutex(IntPtr hMutex);

		// Token: 0x0600003C RID: 60
		[DllImport("kernel32.dll")]
		public static extern bool CloseHandle(IntPtr handle);

		// Token: 0x0600003D RID: 61
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenMutex(uint dwDesiredAccess, bool bInheritHandle, string lpName);

		// Token: 0x0600003E RID: 62
		[DllImport("kernel32.dll")]
		public static extern uint WaitForSingleObject(IntPtr hMutex, uint dwMilliseconds);

		// Token: 0x0600003F RID: 63
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool SetSecurityDescriptorDacl(ref Native.SECURITY_DESCRIPTOR securityDescriptor, bool daclPresent, IntPtr dacl, bool daclDefaulted);

		// Token: 0x06000040 RID: 64
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool InitializeSecurityDescriptor(out Native.SECURITY_DESCRIPTOR securityDescriptor, uint dwRevision);

		// Token: 0x06000041 RID: 65
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool SetKernelObjectSecurity(IntPtr hObject, uint SecurityInformation, IntPtr pSecurityDescriptor);

		// Token: 0x04000002 RID: 2
		public const int ERROR_ALREADY_EXISTS = 183;

		// Token: 0x04000003 RID: 3
		public const uint SYNCHRONIZE = 1048576U;

		// Token: 0x04000004 RID: 4
		public const uint DACL_SECURITY_INFORMATION = 4U;

		// Token: 0x02000044 RID: 68
		public struct SECURITY_DESCRIPTOR
		{
			// Token: 0x04000177 RID: 375
			public byte revision;

			// Token: 0x04000178 RID: 376
			public byte size;

			// Token: 0x04000179 RID: 377
			public short control;

			// Token: 0x0400017A RID: 378
			public IntPtr owner;

			// Token: 0x0400017B RID: 379
			public IntPtr group;

			// Token: 0x0400017C RID: 380
			public IntPtr sacl;

			// Token: 0x0400017D RID: 381
			public IntPtr dacl;
		}

		// Token: 0x02000045 RID: 69
		public struct SECURITY_ATTRIBUTES
		{
			// Token: 0x0400017E RID: 382
			public int nLength;

			// Token: 0x0400017F RID: 383
			public IntPtr lpSecurityDescriptor;

			// Token: 0x04000180 RID: 384
			public int bInheritHandle;
		}

		// Token: 0x02000046 RID: 70
		[Flags]
		public enum SyncObjectAccess : uint
		{
			// Token: 0x04000182 RID: 386
			DELETE = 65536U,
			// Token: 0x04000183 RID: 387
			READ_CONTROL = 131072U,
			// Token: 0x04000184 RID: 388
			WRITE_DAC = 262144U,
			// Token: 0x04000185 RID: 389
			WRITE_OWNER = 524288U,
			// Token: 0x04000186 RID: 390
			SYNCHRONIZE = 1048576U,
			// Token: 0x04000187 RID: 391
			EVENT_ALL_ACCESS = 2031619U,
			// Token: 0x04000188 RID: 392
			EVENT_MODIFY_STATE = 2U,
			// Token: 0x04000189 RID: 393
			MUTEX_ALL_ACCESS = 2031617U,
			// Token: 0x0400018A RID: 394
			MUTEX_MODIFY_STATE = 1U,
			// Token: 0x0400018B RID: 395
			SEMAPHORE_ALL_ACCESS = 2031619U,
			// Token: 0x0400018C RID: 396
			SEMAPHORE_MODIFY_STATE = 2U,
			// Token: 0x0400018D RID: 397
			TIMER_ALL_ACCESS = 2031619U,
			// Token: 0x0400018E RID: 398
			TIMER_MODIFY_STATE = 2U,
			// Token: 0x0400018F RID: 399
			TIMER_QUERY_STATE = 1U
		}
	}
}
