using System;
using System.Threading;

namespace KingstonFuryRgbCLI
{
	// Token: 0x0200002A RID: 42
	public class static_color_mode
	{
		// Token: 0x0600015E RID: 350 RVA: 0x0000B0D0 File Offset: 0x000092D0
		public static void set(DRAMCmdObj _obj)
		{
			try
			{
				CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_settings.ctrl_mode, true);
				if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
				{
					switch (ctrlmode)
					{
					case CTRLmode.def:
						Thread.Sleep(10);
						Class_DLL.ParameterSet(226, 9);
						break;
					case CTRLmode.ctrl_color:
						Thread.Sleep(10);
						Class_DLL.ParameterSet(228, 9);
						Thread.Sleep(10);
						Class_DLL.ParameterSetStaticBrightness(Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, static_color_mode._brightness, 0, 100));
						Miscellaneous.setCtrlColor(_obj);
						break;
					case CTRLmode.independence:
						Thread.Sleep(10);
						Class_DLL.ParameterSet(229, 33);
						Thread.Sleep(10);
						Miscellaneous.setMultiBrightness(_obj, Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, static_color_mode._brightness, 0, 100));
						Miscellaneous.setMultiColor(_obj);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Static Color mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x0400011F RID: 287
		public static readonly int _speed = 0;

		// Token: 0x04000120 RID: 288
		public static readonly int _brightness = 100;
	}
}
