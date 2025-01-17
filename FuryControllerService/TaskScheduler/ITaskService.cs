using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x0200003B RID: 59
	[CompilerGenerated]
	[DefaultMember("TargetServer")]
	[Guid("2FABA4C7-4DA9-4013-9697-20CC3FD40F85")]
	[TypeIdentifier]
	[ComImport]
	public interface ITaskService
	{
		// Token: 0x060001D9 RID: 473
		[DispId(1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		ITaskFolder GetFolder([MarshalAs(UnmanagedType.BStr)] [In] string Path);

		// Token: 0x060001DA RID: 474
		void _VtblGap1_1();

		// Token: 0x060001DB RID: 475
		[DispId(3)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		ITaskDefinition NewTask([In] uint flags);

		// Token: 0x060001DC RID: 476
		[DispId(4)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void Connect([MarshalAs(UnmanagedType.Struct)] [In] [Optional] object serverName, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object user, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object domain, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object password);

		// Token: 0x060001DD RID: 477
		void _VtblGap2_1();

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001DE RID: 478
		[DispId(0)]
		[IndexerName("TargetServer")]
		string TargetServer
		{
			[DispId(0)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
		}
	}
}
