using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x02000037 RID: 55
	[CompilerGenerated]
	[Guid("416D8B73-CB41-4EA1-805C-9BE9A5AC4A74")]
	[TypeIdentifier]
	[ComImport]
	public interface IRegistrationInfo
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001C1 RID: 449
		// (set) Token: 0x060001C2 RID: 450
		[DispId(1)]
		string Description
		{
			[DispId(1)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(1)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001C3 RID: 451
		// (set) Token: 0x060001C4 RID: 452
		[DispId(2)]
		string Author
		{
			[DispId(2)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
			[DispId(2)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.BStr)]
			[param: In]
			set;
		}
	}
}
