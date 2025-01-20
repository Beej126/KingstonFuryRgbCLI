using System;
using System.Collections.Generic;

namespace KingstonFuryRgbCLI
{
	// Token: 0x0200000E RID: 14
	public class ddr5_electric_current_mode
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00004B30 File Offset: 0x00002D30
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
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.electric_current)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 7);
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, 0);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.electric_current || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_electric_current_mode._direction)
						{
							Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_electric_current_mode._direction);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.electric_current || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_electric_current_mode._speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_electric_current_mode._speed);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_electric_current_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_electric_current_mode._brightness, _obj.power_saving);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.electric_current || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_electric_current_mode._number_colors)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_electric_current_mode._number_colors);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.electric_current || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_electric_current_mode._speed, 5.0, 18.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_electric_current_mode._brightness, 0, 100), _obj.power_saving);
							}
							if (_obj.background_color != null && _obj.background_color.Count >= 3)
							{
								Miscellaneous.setDDR5BackgroundColor(_obj.index, _obj.background_color);
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.electric_current || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
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
				Log.LibCmdLogWriter(string.Format("Electric Current mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000043 RID: 67
		public static readonly int _direction = 1;

		// Token: 0x04000044 RID: 68
		public static readonly int _ir_delay = 0;

		// Token: 0x04000045 RID: 69
		public static readonly int _speed = 16;

		// Token: 0x04000046 RID: 70
		public static readonly int _brightness = 80;

		// Token: 0x04000047 RID: 71
		public static readonly List<int> _bg_color = new List<int> { 16, 16, 16 };

		// Token: 0x04000048 RID: 72
		public static readonly int _number_colors = 1;
	}
}
