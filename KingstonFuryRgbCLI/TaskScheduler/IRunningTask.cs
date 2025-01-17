using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x02000038 RID: 56
	[CompilerGenerated]
	[DefaultMember("InstanceGuid")]
	[Guid("653758FB-7B9A-4F1E-A471-BEEB8E9B834E")]
	[TypeIdentifier]
	[ComImport]
	public interface IRunningTask
	{
		// Token: 0x060001C5 RID: 453
		void _VtblGap1_1();

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001C6 RID: 454
		[DispId(0)]
		//[IndexerName("InstanceGuid")]
		string InstanceGuid
		{
			[DispId(0)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[return: MarshalAs(UnmanagedType.BStr)]
			get;
		}
	}
}
