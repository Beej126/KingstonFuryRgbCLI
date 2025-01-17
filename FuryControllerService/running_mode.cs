using System;
using System.Threading;

namespace FuryControllerService
{
	// Token: 0x02000029 RID: 41
	public class running_mode
	{
		// Token: 0x0600015B RID: 347 RVA: 0x0000AE70 File Offset: 0x00009070
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
						Class_DLL.ParameterSet(226, 2);
						break;
					case CTRLmode.ctrl:
						Thread.Sleep(10);
						Class_DLL.ParameterSet(227, 2);
						Thread.Sleep(10);
						Class_DLL.ParameterSetRunningSpeed(Miscellaneous.PercentageToValueRangeSmoothVersion(_obj.ctrl_settings.speed, running_mode._speed, 50.0, 2000.0));
						Thread.Sleep(10);
						Class_DLL.ParameterSetRunningBrightness(Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, running_mode._brightness, 0, 100));
						break;
					case CTRLmode.ctrl_color:
						Thread.Sleep(10);
						Class_DLL.ParameterSet(228, 2);
						Thread.Sleep(10);
						Class_DLL.ParameterSetRunningSpeed(Miscellaneous.PercentageToValueRangeSmoothVersion(_obj.ctrl_settings.speed, running_mode._speed, 50.0, 2000.0));
						Thread.Sleep(10);
						Class_DLL.ParameterSetRunningBrightness(Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, running_mode._brightness, 0, 100));
						Miscellaneous.setCtrlColor(_obj);
						break;
					case CTRLmode.independence:
						if (_obj.ctrl_settings.other_option != null && _obj.ctrl_settings.other_option == "right")
						{
							Thread.Sleep(10);
							Class_DLL.ParameterSet(229, 35);
						}
						else
						{
							Thread.Sleep(10);
							Class_DLL.ParameterSet(229, 34);
						}
						Thread.Sleep(10);
						Class_DLL.ParameterSetRunningSpeed(Miscellaneous.PercentageToValueRangeSmoothVersion(_obj.ctrl_settings.speed, running_mode._speed, 50.0, 2000.0));
						Miscellaneous.setMultiBrightness(_obj, Miscellaneous.CheckValueRange(_obj.ctrl_settings.brightness, running_mode._brightness, 0, 100));
						Miscellaneous.setMultiColor(_obj);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Running mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x0400011D RID: 285
		public static readonly int _speed = 100;

		// Token: 0x0400011E RID: 286
		public static readonly int _brightness = 100;
	}
}
