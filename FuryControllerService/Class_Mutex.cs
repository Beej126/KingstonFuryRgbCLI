using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace FuryControllerService
{
	// Token: 0x02000004 RID: 4
	public class Class_Mutex
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002060 File Offset: 0x00000260
		public IntPtr CreateMutex(string name, bool initiallyOwned = false)
		{
			new IntPtr(0);
			return Class_Mutex.InternalCreateMutex(initiallyOwned, name);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002070 File Offset: 0x00000270
		private static IntPtr InternalCreateMutex(bool initiallyOwned, string name)
		{
			IntPtr intPtr = new IntPtr(0);
			IntPtr intPtr2 = new IntPtr(0);
			IntPtr intPtr4;
			try
			{
				IntPtr intPtr3 = Native.OpenMutex(2031617U, false, name);
				if (intPtr3 == IntPtr.Zero)
				{
					Native.SECURITY_DESCRIPTOR security_DESCRIPTOR;
					int num;
					if (!Native.InitializeSecurityDescriptor(out security_DESCRIPTOR, 1U))
					{
						num = Marshal.GetLastWin32Error();
						throw new Class_Mutex.MutexCreationException(string.Format("Failed to initialize security descriptor. Win32 error num: '{0}'", num));
					}
					if (!Native.SetSecurityDescriptorDacl(ref security_DESCRIPTOR, true, IntPtr.Zero, false))
					{
						num = Marshal.GetLastWin32Error();
						throw new Class_Mutex.MutexCreationException(string.Format("Failed to set security descriptor DACL. Win32 error num: '{0}'", num));
					}
					Native.SECURITY_ATTRIBUTES security_ATTRIBUTES = default(Native.SECURITY_ATTRIBUTES);
					security_ATTRIBUTES.nLength = Marshal.SizeOf<Native.SECURITY_ATTRIBUTES>(security_ATTRIBUTES);
					security_ATTRIBUTES.bInheritHandle = 1;
					intPtr2 = Marshal.AllocCoTaskMem(Marshal.SizeOf<Native.SECURITY_DESCRIPTOR>(security_DESCRIPTOR));
					Marshal.StructureToPtr<Native.SECURITY_DESCRIPTOR>(security_DESCRIPTOR, intPtr2, false);
					security_ATTRIBUTES.lpSecurityDescriptor = intPtr2;
					intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<Native.SECURITY_ATTRIBUTES>(security_ATTRIBUTES));
					Marshal.StructureToPtr<Native.SECURITY_ATTRIBUTES>(security_ATTRIBUTES, intPtr, false);
					intPtr3 = Native.CreateMutex(intPtr, initiallyOwned > false, name);
					num = Marshal.GetLastWin32Error();
					if (intPtr3 == IntPtr.Zero)
					{
						intPtr3 = Native.OpenMutex(2031617U, false, name);
						if (intPtr3 == IntPtr.Zero)
						{
							num = Marshal.GetLastWin32Error();
							throw new Class_Mutex.MutexCreationException(string.Format("Unable to create or open mutex. Win32 error num: '{0}'", num));
						}
					}
				}
				else
				{
					Native.SECURITY_DESCRIPTOR security_DESCRIPTOR2;
					if (!Native.InitializeSecurityDescriptor(out security_DESCRIPTOR2, 1U))
					{
						int num = Marshal.GetLastWin32Error();
						throw new Class_Mutex.MutexCreationException(string.Format("Failed to initialize security descriptor. Win32 error num: '{0}'", num));
					}
					if (!Native.SetSecurityDescriptorDacl(ref security_DESCRIPTOR2, true, IntPtr.Zero, false))
					{
						int num = Marshal.GetLastWin32Error();
						throw new Class_Mutex.MutexCreationException(string.Format("Failed to set security descriptor DACL. Win32 error num: '{0}'", num));
					}
					Native.SECURITY_ATTRIBUTES security_ATTRIBUTES2 = default(Native.SECURITY_ATTRIBUTES);
					security_ATTRIBUTES2.nLength = Marshal.SizeOf<Native.SECURITY_ATTRIBUTES>(security_ATTRIBUTES2);
					security_ATTRIBUTES2.bInheritHandle = 0;
					intPtr2 = Marshal.AllocCoTaskMem(Marshal.SizeOf<Native.SECURITY_DESCRIPTOR>(security_DESCRIPTOR2));
					Marshal.StructureToPtr<Native.SECURITY_DESCRIPTOR>(security_DESCRIPTOR2, intPtr2, false);
					security_ATTRIBUTES2.lpSecurityDescriptor = intPtr2;
					intPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf<Native.SECURITY_ATTRIBUTES>(security_ATTRIBUTES2));
					Marshal.StructureToPtr<Native.SECURITY_ATTRIBUTES>(security_ATTRIBUTES2, intPtr, false);
					if (!Native.SetKernelObjectSecurity(intPtr3, 4U, security_ATTRIBUTES2.lpSecurityDescriptor))
					{
						Console.WriteLine("Failed to set security info for mutex.");
					}
				}
				intPtr4 = intPtr3;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr2);
				}
			}
			return intPtr4;
		}

		// Token: 0x02000047 RID: 71
		[Serializable]
		public class MutexCreationException : ApplicationException
		{
			// Token: 0x060001EA RID: 490 RVA: 0x00014EAE File Offset: 0x000130AE
			public MutexCreationException(string msg)
				: base(msg)
			{
			}

			// Token: 0x060001EB RID: 491 RVA: 0x00014EB7 File Offset: 0x000130B7
			public MutexCreationException(string msg, Exception ex)
				: base(msg, ex)
			{
			}

			// Token: 0x060001EC RID: 492 RVA: 0x00014EC1 File Offset: 0x000130C1
			public MutexCreationException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}
	}
}
