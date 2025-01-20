using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x02000036 RID: 54
	[CompilerGenerated]
	[DefaultMember("Item")]
	[Guid("86627EB4-42A7-41E4-A4D9-AC33A72F2D52")]
	[TypeIdentifier]
	[ComImport]
	public interface IRegisteredTaskCollection : IEnumerable
	{
		// Token: 0x060001BF RID: 447
		void _VtblGap1_2();

		// Token: 0x060001C0 RID: 448
		[DispId(-4)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler))]
        new IEnumerator GetEnumerator();
	}
}
