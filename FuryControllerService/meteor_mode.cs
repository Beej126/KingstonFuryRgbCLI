using System;
using System.Threading;

namespace FuryControllerService
{
	// Token: 0x02000027 RID: 39
	public class meteor_mode
	{
		// Token: 0x06000155 RID: 341 RVA: 0x0000AB8C File Offset: 0x00008D8C
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
						Class_DLL.ParameterSet(226, 8);
						break;
					case CTRLmode.ctrl:
						Thread.Sleep(10);
						Class_DLL.ParameterSet(227, 8);
						Thread.Sleep(10);
						Class_DLL.ParameterSetMeteorSpeed(Miscellaneous.PercentageToValueRangeSmoothVersion(_obj.ctrl_settings.speed, meteor_mode._speed, 50.0, 2000.0));
						Thread.Sleep(10);
						Class_DLL.ParameterSet(221, 100);
						Class_DLL.ParameterSet(222, 40);
						Class_DLL.ParameterSet(223, 10);
						break;
					case CTRLmode.ctrl_color:
						Thread.Sleep(10);
						Class_DLL.ParameterSet(228, 8);
						Thread.Sleep(10);
						Class_DLL.ParameterSetMeteorSpeed(Miscellaneous.PercentageToValueRangeSmoothVersion(_obj.ctrl_settings.speed, meteor_mode._speed, 50.0, 2000.0));
						Thread.Sleep(10);
						Class_DLL.ParameterSet(221, 100);
						Class_DLL.ParameterSet(222, 40);
						Class_DLL.ParameterSet(223, 10);
						Miscellaneous.setCtrlColor(_obj);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Meteor mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000119 RID: 281
		public static readonly int _speed = 100;

		// Token: 0x0400011A RID: 282
		public static readonly int _brightness = 100;
	}
}
