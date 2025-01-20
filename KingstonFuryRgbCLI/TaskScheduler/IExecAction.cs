using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x02000033 RID: 51
	[CompilerGenerated]
	[Guid("4C3D624D-FD6B-49A3-B9B7-09CB3CD3F047")]
	[TypeIdentifier]
	[ComImport]
	public interface IExecAction : IAction
	{
		// Token: 0x060001AD RID: 429
		void _VtblGap1_3();

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001AE RID: 430
		// (set) Token: 0x060001AF RID: 431
		[DispId(10)]
		string Path
		{
			[DispId(10)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(10)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001B0 RID: 432
		// (set) Token: 0x060001B1 RID: 433
		[DispId(11)]
		string Arguments
		{
			[DispId(11)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(11)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}
	}
}
