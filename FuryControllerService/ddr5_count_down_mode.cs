using System;
using System.Collections.Generic;

namespace FuryControllerService
{
	// Token: 0x0200000D RID: 13
	public class ddr5_count_down_mode
	{
		// Token: 0x06000066 RID: 102 RVA: 0x0000473C File Offset: 0x0000293C
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
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.count_down)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 8);
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, 0);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.count_down || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_count_down_mode._direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_count_down_mode._direction);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.count_down || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_count_down_mode._speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_count_down_mode._speed);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_count_down_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_count_down_mode._brightness, _obj.power_saving);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.count_down || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_count_down_mode._number_colors)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_count_down_mode._number_colors);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.count_down || KSDRAMSMBusCtrlObj._direction[_obj.index] != _obj.direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)Miscellaneous.CheckValueRange(_obj.direction, ddr5_count_down_mode._direction, 1, 2));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.count_down || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_count_down_mode._speed, 20.0, 76.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_count_down_mode._brightness, 0, 100), _obj.power_saving);
							}
							if (_obj.background_color != null && _obj.background_color.Count >= 3)
							{
								Miscellaneous.setDDR5BackgroundColor(_obj.index, _obj.background_color);
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.count_down || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
								{
									Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)_obj.number_colors);
								}
								if (!bResume && !_obj.reset_color_table && _obj.color_table != null && _obj.color_table.Count >= _obj.number_colors)
								{
									Miscellaneous.setDDR5ColorsTable(_obj.index, _obj.number_colors, _obj.color_table);
								}
							}
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Count Down mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x0400003E RID: 62
		public static readonly int _direction = 1;

		// Token: 0x0400003F RID: 63
		public static readonly int _speed = 76;

		// Token: 0x04000040 RID: 64
		public static readonly int _brightness = 80;

		// Token: 0x04000041 RID: 65
		public static readonly List<int> _bg_color = new List<int> { 16, 16, 16 };

		// Token: 0x04000042 RID: 66
		public static readonly int _number_colors = 1;
	}
}
