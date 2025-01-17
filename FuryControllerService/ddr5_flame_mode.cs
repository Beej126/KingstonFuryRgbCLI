using System;
using System.Collections.Generic;

namespace FuryControllerService
{
	// Token: 0x02000009 RID: 9
	public class ddr5_flame_mode
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00002B28 File Offset: 0x00000D28
		public static void set(DRAMDDR5CtrlObj _obj, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_mode, true);
					if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
					{
						byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]);
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.flame)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 9);
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, 0);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.flame || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_flame_mode._direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_flame_mode._direction);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.flame || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_flame_mode._speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_flame_mode._speed);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_flame_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_flame_mode._brightness, _obj.power_saving);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.flame || KSDRAMSMBusCtrlObj._direction[_obj.index] != _obj.direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)Miscellaneous.CheckValueRange(_obj.direction, ddr5_flame_mode._direction, 1, 2));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.flame || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_flame_mode._speed, 40.0, 64.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_flame_mode._brightness, 0, 100), _obj.power_saving);
							}
							if (_obj.background_color != null && _obj.background_color.Count >= 3)
							{
								Miscellaneous.setDDR5BackgroundColor(_obj.index, _obj.background_color);
							}
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Flame mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000010 RID: 16
		public static readonly int _direction = 1;

		// Token: 0x04000011 RID: 17
		public static readonly int _speed = 64;

		// Token: 0x04000012 RID: 18
		public static readonly int _brightness = 80;

		// Token: 0x04000013 RID: 19
		public static readonly List<int> _bg_color = new List<int> { 16, 16, 16 };
	}
}
