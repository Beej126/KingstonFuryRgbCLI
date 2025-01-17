using System;
using System.Threading;

namespace FuryControllerService
{
	// Token: 0x02000025 RID: 37
	public class color_cycle_mode
	{
		// Token: 0x0600014F RID: 335 RVA: 0x0000A80C File Offset: 0x00008A0C
		public static void set(DRAMCmdObj _obj)
		{
			try
			{
				CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_settings.ctrl_mode, true);
				if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
				{
					if (ctrlmode != CTRLmode.def)
					{
						if (ctrlmode == CTRLmode.ctrl)
						{
							Thread.Sleep(10);
							Class_DLL.ParameterSet(227, 4);
							int num = (100 - Miscellaneous.CheckValueRange(_obj.ctrl_settings.speed, 50, 0, 100)) * 19 + 50;
							int num2 = (100 - Miscellaneous.CheckValueRange(_obj.ctrl_settings.speed, 50, 0, 100)) * 9 + 100;
							Thread.Sleep(10);
							Class_DLL.ParameterSetColorCycleSpeed(num, num2);
							Thread.Sleep(10);
							Class_DLL.ParameterSetColorCycleBrightness(Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, color_cycle_mode._brightness, 0, 100));
						}
					}
					else
					{
						Thread.Sleep(10);
						Class_DLL.ParameterSet(226, 4);
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Color Cycle mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000114 RID: 276
		public static readonly int _speedHold = 1000;

		// Token: 0x04000115 RID: 277
		public static readonly int _speedChange = 500;

		// Token: 0x04000116 RID: 278
		public static readonly int _brightness = 100;
	}
}
