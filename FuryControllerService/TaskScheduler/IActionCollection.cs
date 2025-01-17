using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x02000032 RID: 50
	[CompilerGenerated]
	[DefaultMember("Item")]
	[Guid("02820E19-7B98-4ED2-B2E8-FDCCCEFF619B")]
	[TypeIdentifier]
	[ComImport]
	public interface IActionCollection : IEnumerable
	{
		// Token: 0x060001AB RID: 427
		void _VtblGap1_5();

		// Token: 0x060001AC RID: 428
		[DispId(3)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.Interface)]
		IAction Create([In] _TASK_ACTION_TYPE Type);
	}
}
