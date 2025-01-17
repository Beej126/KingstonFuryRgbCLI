using System;
using System.Collections.Generic;

namespace FuryControllerService
{
	// Token: 0x02000010 RID: 16
	public class ddr5_ecg_mode
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000066E8 File Offset: 0x000048E8
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
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.ecg)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 2);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.ecg || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_ecg_mode._direction)
						{
							Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_ecg_mode._direction);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.ecg || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_ecg_mode._ir_delay)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_ecg_mode._ir_delay);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.ecg || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_ecg_mode._speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_ecg_mode._speed);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_ecg_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_ecg_mode._brightness, _obj.power_saving);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.ecg || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_ecg_mode._number_colors)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_ecg_mode._number_colors);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.ecg || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != _obj.ir_delay)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)Miscellaneous.CheckValueRange(_obj.ir_delay, ddr5_ecg_mode._ir_delay, 2, 5));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.ecg || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_ecg_mode._speed, 0.0, 10.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_ecg_mode._brightness, 0, 100), _obj.power_saving);
							}
							if (_obj.background_color != null && _obj.background_color.Count >= 3)
							{
								Miscellaneous.setDDR5BackgroundColor(_obj.index, _obj.background_color);
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.ecg || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
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
				Log.LibCmdLogWriter(string.Format("ECG mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x0400006D RID: 109
		public static readonly int _direction = 1;

		// Token: 0x0400006E RID: 110
		public static readonly int _ir_delay = 3;

		// Token: 0x0400006F RID: 111
		public static readonly int _speed = 0;

		// Token: 0x04000070 RID: 112
		public static readonly int _brightness = 80;

		// Token: 0x04000071 RID: 113
		public static readonly List<int> _bg_color = new List<int> { 16, 16, 16 };

		// Token: 0x04000072 RID: 114
		public static readonly int _number_colors = 1;
	}
}
