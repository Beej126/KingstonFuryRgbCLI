using System;

namespace FuryControllerService
{
	// Token: 0x0200000A RID: 10
	public class ddr5_dynamic_color_mode
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002E48 File Offset: 0x00001048
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
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.dynamic_color)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 4);
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, 0);
							Class_DLL.SetKingstonFuryDDR5Speed(b, 0);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.dynamic_color || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_dynamic_color_mode._direction)
						{
							Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_dynamic_color_mode._direction);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.dynamic_color || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_dynamic_color_mode._speed)
							{
								Class_DLL.SetKingstonFuryDDR5ColorHoldTime(b, ddr5_dynamic_color_mode._speed);
								Class_DLL.SetKingstonFuryDDR5ColorChangeTime(b, ddr5_dynamic_color_mode._speed * 5);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_dynamic_color_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_dynamic_color_mode._brightness, _obj.power_saving);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.dynamic_color || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_dynamic_color_mode._number_colors)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_dynamic_color_mode._number_colors);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.dynamic_color || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5ColorHoldTime(b, Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_dynamic_color_mode._speed, 100.0, 1000.0));
								Class_DLL.SetKingstonFuryDDR5ColorChangeTime(b, Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_dynamic_color_mode._speed, 100.0, 1000.0) * 5);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_dynamic_color_mode._brightness, 0, 100), _obj.power_saving);
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.dynamic_color || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
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
				Log.LibCmdLogWriter(string.Format("Dynamic Color mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000014 RID: 20
		public static readonly int _direction = 1;

		// Token: 0x04000015 RID: 21
		public static readonly int _speed = 300;

		// Token: 0x04000016 RID: 22
		public static readonly int _brightness = 80;

		// Token: 0x04000017 RID: 23
		public static readonly int _number_colors = 10;
	}
}
