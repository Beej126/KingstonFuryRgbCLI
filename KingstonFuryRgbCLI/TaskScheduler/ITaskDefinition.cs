using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x02000039 RID: 57
	[CompilerGenerated]
	[Guid("F5BC8FC5-536D-4F77-B852-FBC1356FDEB6")]
	[TypeIdentifier]
	[ComImport]
	public interface ITaskDefinition
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001C7 RID: 455
		// (set) Token: 0x060001C8 RID: 456
		[DispId(1)]
		IRegistrationInfo RegistrationInfo
		{
			[DispId(1)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
			[DispId(1)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Interface)]
			[param: In]
			set;
		}

		// Token: 0x060001C9 RID: 457
		void _VtblGap1_2();

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001CA RID: 458
		// (set) Token: 0x060001CB RID: 459
		[DispId(7)]
		ITaskSettings Settings
		{
			[DispId(7)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
			[DispId(7)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Interface)]
			[param: In]
			set;
		}

		// Token: 0x060001CC RID: 460
		void _VtblGap2_2();

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001CD RID: 461
		// (set) Token: 0x060001CE RID: 462
		[DispId(12)]
		IPrincipal Principal
		{
			[DispId(12)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
			[DispId(12)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Interface)]
			[param: In]
			set;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001CF RID: 463
		// (set) Token: 0x060001D0 RID: 464
		[DispId(13)]
		IActionCollection Actions
		{
			[DispId(13)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.Interface)]
			get;
			[DispId(13)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: MarshalAs(UnmanagedType.Interface)]
			[param: In]
			set;
		}
	}
}
