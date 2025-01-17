using System;
using System.Threading;

namespace FuryControllerService
{
	// Token: 0x02000026 RID: 38
	public class double_blink_mode
	{
		// Token: 0x06000152 RID: 338 RVA: 0x0000A958 File Offset: 0x00008B58
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
						Class_DLL.ParameterSet(226, 7);
						break;
					case CTRLmode.ctrl:
					{
						Thread.Sleep(10);
						Class_DLL.ParameterSet(227, 7);
						int num = Miscellaneous.PercentageToValueRange(_obj.ctrl_settings.speed, double_blink_mode._speed, 50.0, 200.0);
						int num2 = num * 10;
						Thread.Sleep(10);
						Class_DLL.ParameterSetBlinkCycleSpeed(num, num, num2);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBlinkBrightness(Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, double_blink_mode._brightness, 0, 100));
						break;
					}
					case CTRLmode.ctrl_color:
					{
						Class_DLL.ParameterSet(228, 7);
						int num3 = Miscellaneous.PercentageToValueRange(_obj.ctrl_settings.speed, double_blink_mode._speed, 50.0, 200.0);
						int num4 = num3 * 10;
						Thread.Sleep(10);
						Class_DLL.ParameterSetBlinkCycleSpeed(num3, num3, num4);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBlinkBrightness(Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, double_blink_mode._brightness, 0, 100));
						Miscellaneous.setCtrlColor(_obj);
						break;
					}
					case CTRLmode.independence:
					{
						Thread.Sleep(10);
						Class_DLL.ParameterSet(229, 38);
						int num5 = Miscellaneous.PercentageToValueRange(_obj.ctrl_settings.speed, double_blink_mode._speed, 50.0, 200.0);
						int num6 = num5 * 10;
						Thread.Sleep(10);
						Class_DLL.ParameterSetBlinkCycleSpeed(num5, num5, num6);
						Miscellaneous.setMultiBrightness(_obj, Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, double_blink_mode._brightness, 0, 100));
						Miscellaneous.setMultiColor(_obj);
						break;
					}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Double Blink mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000117 RID: 279
		public static readonly int _speed = 125;

		// Token: 0x04000118 RID: 280
		public static readonly int _brightness = 100;
	}
}
