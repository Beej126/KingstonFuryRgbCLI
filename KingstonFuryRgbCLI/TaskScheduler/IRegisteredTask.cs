using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x02000035 RID: 53
	[CompilerGenerated]
	[Guid("9C86F320-DEE3-4DD1-B972-A303F26B061E")]
	[DefaultMember("Path")]
	[TypeIdentifier]
	[ComImport]
	public interface IRegisteredTask
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001B9 RID: 441
		[DispId(1)]
		string Name
		{
			[DispId(1)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001BA RID: 442
		[DispId(0)]
		//[IndexerName("Path")]
		string Path
		{
			[DispId(0)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001BB RID: 443
		[DispId(2)]
		_TASK_STATE State
		{
			[DispId(2)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001BC RID: 444
		// (set) Token: 0x060001BD RID: 445
		[DispId(3)]
		bool Enabled
		{
			[DispId(3)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(3)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			set;
		}

		// Token: 0x060001BE RID: 446
		[DispId(5)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IRunningTask Run([MarshalAs(UnmanagedType.Struct)] [In] object @params);
	}
}
