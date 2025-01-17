using System;

namespace FuryControllerService
{
	// Token: 0x0200000B RID: 11
	public class ddr5_breath_mode
	{
		// Token: 0x0600005D RID: 93 RVA: 0x000031D8 File Offset: 0x000013D8
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
						if (ctrlmode != CTRLmode.independence)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 3);
						}
						else
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 19);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.breath || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_breath_mode._direction)
						{
							Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_breath_mode._direction);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.breath)
						{
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, 0);
							Class_DLL.SetKingstonFuryDDR5Speed(b, 0);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.breath || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_breath_mode._speed)
							{
								Class_DLL.SetKingstonFuryDDR5BreathModeParameter(b, (byte)(ddr5_breath_mode._speed * 3), (byte)ddr5_breath_mode._speed, (byte)ddr5_breath_mode._speed, (byte)(ddr5_breath_mode._speed * 3), (byte)ddr5_breath_mode._speed_hold, (byte)ddr5_breath_mode._brightness_max, (byte)ddr5_breath_mode._brightness_mid, (byte)ddr5_breath_mode._brightness_min);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_breath_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_breath_mode._brightness, _obj.power_saving);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.breath || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_breath_mode._number_colors)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_breath_mode._number_colors);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.breath || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								int num = Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_breath_mode._speed, 1.0, 10.0);
								Class_DLL.SetKingstonFuryDDR5BreathModeParameter(b, (byte)(num * 3), (byte)num, (byte)num, (byte)(num * 3), (byte)ddr5_breath_mode._speed_hold, (byte)ddr5_breath_mode._brightness_max, (byte)ddr5_breath_mode._brightness_mid, (byte)ddr5_breath_mode._brightness_min);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_breath_mode._brightness, 0, 100), _obj.power_saving);
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.breath || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
								{
									Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)_obj.number_colors);
								}
								if (!bResume && !_obj.reset_color_table && _obj.color_table != null && _obj.color_table.Count >= _obj.number_colors)
								{
									Miscellaneous.setDDR5ColorsTable(_obj.index, _obj.number_colors, _obj.color_table);
								}
							}
							break;
						case CTRLmode.independence:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.breath || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								int num2 = Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_breath_mode._speed, 1.0, 10.0);
								Class_DLL.SetKingstonFuryDDR5BreathModeParameter(b, (byte)(num2 * 3), (byte)num2, (byte)num2, (byte)(num2 * 3), (byte)ddr5_breath_mode._speed_hold, (byte)ddr5_breath_mode._brightness_max, (byte)ddr5_breath_mode._brightness_mid, (byte)ddr5_breath_mode._brightness_min);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_breath_mode._brightness, 0, 100), _obj.power_saving);
							}
							if (_obj.ctrl_color != null && _obj.ctrl_color.Count >= 10)
							{
								Miscellaneous.setDDR5CtrlColors(_obj.index, _obj.ctrl_color, bResume);
							}
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Breath mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000018 RID: 24
		public static readonly int _direction = 1;

		// Token: 0x04000019 RID: 25
		public static readonly int _ir_delay = 0;

		// Token: 0x0400001A RID: 26
		public static readonly int _speed = 5;

		// Token: 0x0400001B RID: 27
		public static readonly int _speed_hold = 1;

		// Token: 0x0400001C RID: 28
		public static readonly int _brightness_max = 100;

		// Token: 0x0400001D RID: 29
		public static readonly int _brightness_mid = 64;

		// Token: 0x0400001E RID: 30
		public static readonly int _brightness_min = 1;

		// Token: 0x0400001F RID: 31
		public static readonly int _brightness = 80;

		// Token: 0x04000020 RID: 32
		public static readonly int _number_colors = 10;
	}
}
