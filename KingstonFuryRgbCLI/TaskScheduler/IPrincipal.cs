using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x02000034 RID: 52
	[CompilerGenerated]
	[Guid("D98D51E5-C9B4-496A-A9C1-18980261CF0F")]
	[TypeIdentifier]
	[ComImport]
	public interface IPrincipal
	{
		// Token: 0x060001B2 RID: 434
		void _VtblGap1_6();

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001B3 RID: 435
		// (set) Token: 0x060001B4 RID: 436
		[DispId(4)]
		_TASK_LOGON_TYPE LogonType
		{
			[DispId(4)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(4)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001B5 RID: 437
		// (set) Token: 0x060001B6 RID: 438
		[DispId(5)]
		string GroupId
		{
			[DispId(5)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(5)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001B7 RID: 439
		// (set) Token: 0x060001B8 RID: 440
		[DispId(6)]
		_TASK_RUNLEVEL RunLevel
		{
			[DispId(6)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(6)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}
	}
}
