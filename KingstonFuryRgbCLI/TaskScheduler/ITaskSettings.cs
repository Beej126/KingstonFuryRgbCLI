using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x0200003C RID: 60
	[CompilerGenerated]
	[Guid("8FD4711D-2D02-4C8C-87E3-EFF699DE127E")]
	[TypeIdentifier]
	[ComImport]
	public interface ITaskSettings
	{
		// Token: 0x060001DF RID: 479
		void _VtblGap1_8();

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001E0 RID: 480
		// (set) Token: 0x060001E1 RID: 481
		[DispId(7)]
		bool StopIfGoingOnBatteries
		{
			[DispId(7)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(7)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001E2 RID: 482
		// (set) Token: 0x060001E3 RID: 483
		[DispId(8)]
		bool DisallowStartIfOnBatteries
		{
			[DispId(8)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(8)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}

		// Token: 0x060001E4 RID: 484
		void _VtblGap2_10();

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001E5 RID: 485
		// (set) Token: 0x060001E6 RID: 486
		[DispId(14)]
		bool Enabled
		{
			[DispId(14)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(14)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}

		// Token: 0x060001E7 RID: 487
		void _VtblGap3_6();

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001E8 RID: 488
		// (set) Token: 0x060001E9 RID: 489
		[DispId(18)]
		bool Hidden
		{
			[DispId(18)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			get;
			[DispId(18)]
			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[param: In]
			set;
		}
	}
}
