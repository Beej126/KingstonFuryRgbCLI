using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x0200003A RID: 58
	[CompilerGenerated]
	[DefaultMember("Path")]
	[Guid("8CFAC062-A080-4C15-9A88-AA7C2AF80DFC")]
	[TypeIdentifier]
	[ComImport]
	public interface ITaskFolder
	{
		// Token: 0x060001D1 RID: 465
		void _VtblGap1_1();

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001D2 RID: 466
		[DispId(0)]
		//[IndexerName("Path")]
		string Path
		{
			[DispId(0)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
		}

		// Token: 0x060001D3 RID: 467
		void _VtblGap2_4();

		// Token: 0x060001D4 RID: 468
		[DispId(7)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IRegisteredTask GetTask([MarshalAs(UnmanagedType.BStr)] [In] string Path);

		// Token: 0x060001D5 RID: 469
		[DispId(8)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IRegisteredTaskCollection GetTasks([In] int flags);

		// Token: 0x060001D6 RID: 470
		[DispId(9)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		void DeleteTask([MarshalAs(UnmanagedType.BStr)] [In] string Name, [In] int flags);

		// Token: 0x060001D7 RID: 471
		void _VtblGap3_1();

		// Token: 0x060001D8 RID: 472
		[DispId(11)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IRegisteredTask RegisterTaskDefinition([MarshalAs(UnmanagedType.BStr)] [In] string Path, [MarshalAs(UnmanagedType.Interface)] [In] ITaskDefinition pDefinition, [In] int flags, [MarshalAs(UnmanagedType.Struct)] [In] object UserId, [MarshalAs(UnmanagedType.Struct)] [In] object password, [In] _TASK_LOGON_TYPE LogonType, [MarshalAs(UnmanagedType.Struct)] [In] [Optional] object sddl);
	}
}
