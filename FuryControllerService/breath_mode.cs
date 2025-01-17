using System;
using System.Threading;

namespace FuryControllerService
{
	// Token: 0x02000024 RID: 36
	public class breath_mode
	{
		// Token: 0x0600014C RID: 332 RVA: 0x0000A544 File Offset: 0x00008744
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
						Class_DLL.ParameterSet(226, 3);
						break;
					case CTRLmode.ctrl:
					{
						Thread.Sleep(10);
						Class_DLL.ParameterSet(227, 3);
						int num = Miscellaneous.PercentageToValueRange(_obj.ctrl_settings.speed, breath_mode._speedRiseAndDown, 150.0, 2000.0);
						int num2 = Miscellaneous.CheckValueRange(num / 3 * 2, breath_mode._speedRiseAndDown, 100, 1333);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBreathSpeed(num, num, num2);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBreathBrightness(Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, breath_mode._brightness, 0, 100), Miscellaneous.CheckValueRange(_obj.ctrl_settings.darkness, breath_mode._darkness, 0, 100));
						break;
					}
					case CTRLmode.ctrl_color:
					{
						Thread.Sleep(10);
						Class_DLL.ParameterSet(228, 3);
						int num3 = Miscellaneous.PercentageToValueRange(_obj.ctrl_settings.speed, breath_mode._speedRiseAndDown, 150.0, 2000.0);
						int num4 = Miscellaneous.CheckValueRange(num3 / 3 * 2, breath_mode._speedRiseAndDown, 100, 1333);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBreathSpeed(num3, num3, num4);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBreathBrightness(Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, breath_mode._brightness, 0, 100), Miscellaneous.CheckValueRange(_obj.ctrl_settings.darkness, breath_mode._darkness, 0, 100));
						Miscellaneous.setCtrlColor(_obj);
						break;
					}
					case CTRLmode.independence:
					{
						int num5 = Miscellaneous.PercentageToValueRange(_obj.ctrl_settings.speed, breath_mode._speedRiseAndDown, 150.0, 2000.0);
						int num6 = Miscellaneous.CheckValueRange(num5 / 3 * 2, breath_mode._speedRiseAndDown, 100, 1333);
						Class_DLL.ParameterSet(225, 1);
						Thread.Sleep(10);
						Class_DLL.ParameterSetBreathSpeed(num5, num5, num6);
						Miscellaneous.setMultiBrightnessForBreath(_obj, Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, breath_mode._brightness, 0, 100), Miscellaneous.CheckValueRange(_obj.ctrl_settings.darkness, breath_mode._darkness, 0, 100));
						Miscellaneous.setMultiColor(_obj);
						break;
					}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Breath mode : ", ex.Message), true);
			}
		}

		// Token: 0x04000110 RID: 272
		public static readonly int _speedRiseAndDown = 1500;

		// Token: 0x04000111 RID: 273
		public static readonly int _speedPause = 1000;

		// Token: 0x04000112 RID: 274
		public static readonly int _brightness = 100;

		// Token: 0x04000113 RID: 275
		public static readonly int _darkness = 0;
	}
}
