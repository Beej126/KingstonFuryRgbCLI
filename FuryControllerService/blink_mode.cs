using System;
using System.Threading;

namespace FuryControllerService
{
	// Token: 0x02000023 RID: 35
	public class blink_mode
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000A31C File Offset: 0x0000851C
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
						Class_DLL.ParameterSet(226, 6);
						break;
					case CTRLmode.ctrl:
					{
						Thread.Sleep(10);
						Class_DLL.ParameterSet(227, 6);
						int num = Miscellaneous.PercentageToValueRangeSmoothVersion(_obj.ctrl_settings.speed, blink_mode._speed, 50.0, 2000.0);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBlinkSpeed(num, num);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBlinkBrightness(Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, blink_mode._brightness, 0, 100));
						break;
					}
					case CTRLmode.ctrl_color:
					{
						Thread.Sleep(10);
						Class_DLL.ParameterSet(228, 6);
						int num2 = Miscellaneous.PercentageToValueRangeSmoothVersion(_obj.ctrl_settings.speed, blink_mode._speed, 50.0, 2000.0);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBlinkSpeed(num2, num2);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBlinkBrightness(Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, blink_mode._brightness, 0, 100));
						Miscellaneous.setCtrlColor(_obj);
						break;
					}
					case CTRLmode.independence:
					{
						Thread.Sleep(10);
						Class_DLL.ParameterSet(229, 37);
						int num3 = Miscellaneous.PercentageToValueRangeSmoothVersion(_obj.ctrl_settings.speed, blink_mode._speed, 50.0, 2000.0);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBlinkSpeed(num3, num3);
						Miscellaneous.setMultiBrightness(_obj, Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, blink_mode._brightness, 0, 100));
						Miscellaneous.setMultiColor(_obj);
						break;
					}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Blink mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x0400010E RID: 270
		public static readonly int _speed = 1025;

		// Token: 0x0400010F RID: 271
		public static readonly int _brightness = 100;
	}
}
