using System;
using System.Threading;

namespace FuryControllerService
{
	// Token: 0x02000022 RID: 34
	public class all_off_mode
	{
		// Token: 0x06000147 RID: 327 RVA: 0x0000A2FF File Offset: 0x000084FF
		public static void set(DRAMCmdObj _obj)
		{
			Thread.Sleep(10);
			Class_DLL.ParameterSet(226, 1);
		}
	}
}
