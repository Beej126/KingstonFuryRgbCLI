using System;
using System.Threading;

namespace KingstonFuryRgbCLI
{
	// Token: 0x02000028 RID: 40
	public class rainbow_mode
	{
		// Token: 0x06000158 RID: 344 RVA: 0x0000AD4C File Offset: 0x00008F4C
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
							Class_DLL.ParameterSet(227, 5);
							Thread.Sleep(10);
							Class_DLL.ParameterSetRainbowSpeed(Miscellaneous.PercentageToValueRangeSmoothVersion(_obj.ctrl_settings.speed, rainbow_mode.speed, 50.0, 2000.0));
							Thread.Sleep(10);
							Class_DLL.ParameterSetRainbowBrightness(Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, rainbow_mode.brightness, 0, 100));
						}
					}
					else
					{
						Thread.Sleep(10);
						Class_DLL.ParameterSet(226, 5);
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Rainbow mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x0400011B RID: 283
		public static readonly int speed = 105;

		// Token: 0x0400011C RID: 284
		public static readonly int brightness = 100;
	}
}
