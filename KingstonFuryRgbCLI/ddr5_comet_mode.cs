using System;
using System.Collections.Generic;

namespace KingstonFuryRgbCLI
{
	// Token: 0x0200000C RID: 12
	public class ddr5_comet_mode
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000036BC File Offset: 0x000018BC
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
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.comet)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 6);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.comet || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_comet_mode._direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_comet_mode._direction);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.comet || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_comet_mode._ir_delay)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_comet_mode._ir_delay);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.comet || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_comet_mode._speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_comet_mode._speed);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_comet_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_comet_mode._brightness, _obj.power_saving);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.comet || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != ddr5_comet_mode._number_LED)
							{
								Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)ddr5_comet_mode._number_LED);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.comet || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_comet_mode._number_colors)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_comet_mode._number_colors);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.comet || KSDRAMSMBusCtrlObj._direction[_obj.index] != _obj.direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)Miscellaneous.CheckValueRange(_obj.direction, ddr5_comet_mode._direction, 1, 2));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.comet || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != _obj.ir_delay)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)Miscellaneous.CheckValueRange(_obj.ir_delay, ddr5_comet_mode._ir_delay, 0, 20));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.comet || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_comet_mode._speed, 0.0, 255.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_comet_mode._brightness, 0, 100), _obj.power_saving);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.comet || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != _obj.led_number)
							{
								Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)Miscellaneous.CheckValueRange(_obj.led_number, ddr5_comet_mode._number_LED, 1, 18));
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.comet || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
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
				Log.LibCmdLogWriter(string.Format("Comet mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003B20 File Offset: 0x00001D20
		public static void set_rain(DRAMDDR5CtrlObj _obj, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_mode, true);
					if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
					{
						byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]);
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rain)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 6);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rain || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_comet_mode._ir_delay_rain)
						{
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_comet_mode._ir_delay_rain);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rain || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != ddr5_comet_mode._number_LED_rain)
						{
							Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)ddr5_comet_mode._number_LED_rain);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rain || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_comet_mode._direction_rain)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_comet_mode._direction_rain);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rain || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_comet_mode._speed_rain)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)(ddr5_comet_mode._speed_rain + ddr5_comet_mode._speed_rain_offset[_obj.index % 4]));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_comet_mode._brightness_rain || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_comet_mode._brightness_rain, _obj.power_saving);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rain || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_comet_mode._number_colors_rain)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_comet_mode._number_colors_rain);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rain || KSDRAMSMBusCtrlObj._direction[_obj.index] != _obj.direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)Miscellaneous.CheckValueRange(_obj.direction, ddr5_comet_mode._direction_rain, 1, 2));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rain || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)(Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_comet_mode._speed_rain, 8.0, 28.0) + ddr5_comet_mode._speed_rain_offset[_obj.index % 4]));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_comet_mode._brightness_rain, 0, 100), _obj.power_saving);
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rain || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
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
				Log.LibCmdLogWriter(string.Format("Rain mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003F1C File Offset: 0x0000211C
		public static void set_firework(DRAMDDR5CtrlObj _obj, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_mode, true);
					if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
					{
						byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]);
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.firework)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 6);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.firework || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_comet_mode._ir_delay_firework)
						{
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_comet_mode._ir_delay_firework);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.firework || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != ddr5_comet_mode._number_LED_firework)
						{
							Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)ddr5_comet_mode._number_LED_firework);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.firework || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_comet_mode._direction_firework)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_comet_mode._direction_firework);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.firework || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_comet_mode._speed_firework)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)(ddr5_comet_mode._speed_firework + ddr5_comet_mode._speed_firework_offset[_obj.index % 4]));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_comet_mode._brightness_firework || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_comet_mode._brightness_firework, _obj.power_saving);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.firework || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_comet_mode._number_colors_firework)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_comet_mode._number_colors_firework);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.firework || KSDRAMSMBusCtrlObj._direction[_obj.index] != _obj.direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)Miscellaneous.CheckValueRange(_obj.direction, ddr5_comet_mode._direction_firework, 1, 2));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.firework || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)(Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_comet_mode._speed_firework, 33.0, 83.0) + ddr5_comet_mode._speed_firework_offset[_obj.index % 4]));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_comet_mode._brightness_firework, 0, 100), _obj.power_saving);
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.firework || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
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
				Log.LibCmdLogWriter(string.Format("Firework mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004318 File Offset: 0x00002518
		public static void set_racing(DRAMDDR5CtrlObj _obj, int iAPIVer, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_mode, true);
					if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
					{
						byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]);
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.racing)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 13);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.racing || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_comet_mode._ir_delay_racing)
						{
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_comet_mode._ir_delay_racing);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.racing || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != ddr5_comet_mode._number_LED_racing)
						{
							Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)ddr5_comet_mode._number_LED_racing);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.racing || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_comet_mode._direction_racing)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_comet_mode._direction_racing);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.racing || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_comet_mode._speed_racing)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)(ddr5_comet_mode._speed_racing + ddr5_comet_mode._speed_racing_offset[_obj.index % 4]));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_comet_mode._brightness_racing || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_comet_mode._brightness_racing, _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.racing || KSDRAMSMBusCtrlObj._width[_obj.index] != ddr5_comet_mode._width_racing))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)ddr5_comet_mode._width_racing);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.racing || KSDRAMSMBusCtrlObj._hue[_obj.index] != ddr5_comet_mode._hue_racing))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)ddr5_comet_mode._hue_racing);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.racing || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_comet_mode._number_colors_racing)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_comet_mode._number_colors_racing);
							}
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Racing mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000021 RID: 33
		public static readonly int _direction = 1;

		// Token: 0x04000022 RID: 34
		public static readonly int _direction_rain = 2;

		// Token: 0x04000023 RID: 35
		public static readonly int _direction_firework = 1;

		// Token: 0x04000024 RID: 36
		public static readonly int _direction_racing = 1;

		// Token: 0x04000025 RID: 37
		public static readonly int _ir_delay = 0;

		// Token: 0x04000026 RID: 38
		public static readonly int _ir_delay_rain = 0;

		// Token: 0x04000027 RID: 39
		public static readonly int _ir_delay_firework = 0;

		// Token: 0x04000028 RID: 40
		public static readonly int _ir_delay_racing = 0;

		// Token: 0x04000029 RID: 41
		public static readonly int _speed = 50;

		// Token: 0x0400002A RID: 42
		public static readonly int _speed_rain = 8;

		// Token: 0x0400002B RID: 43
		public static readonly List<int> _speed_rain_offset = new List<int> { 11, 0, 15, 9 };

		// Token: 0x0400002C RID: 44
		public static readonly int _speed_firework = 33;

		// Token: 0x0400002D RID: 45
		public static readonly List<int> _speed_firework_offset = new List<int> { 15, 0, 19, 4 };

		// Token: 0x0400002E RID: 46
		public static readonly int _speed_racing = 8;

		// Token: 0x0400002F RID: 47
		public static readonly List<int> _speed_racing_offset = new List<int> { 11, 0, 15, 9 };

		// Token: 0x04000030 RID: 48
		public static readonly int _brightness = 80;

		// Token: 0x04000031 RID: 49
		public static readonly int _brightness_rain = 80;

		// Token: 0x04000032 RID: 50
		public static readonly int _brightness_firework = 80;

		// Token: 0x04000033 RID: 51
		public static readonly int _brightness_racing = 80;

		// Token: 0x04000034 RID: 52
		public static readonly int _number_LED = 7;

		// Token: 0x04000035 RID: 53
		public static readonly int _number_LED_rain = 3;

		// Token: 0x04000036 RID: 54
		public static readonly int _number_LED_firework = 7;

		// Token: 0x04000037 RID: 55
		public static readonly int _number_LED_racing = 3;

		// Token: 0x04000038 RID: 56
		public static readonly int _number_colors = 10;

		// Token: 0x04000039 RID: 57
		public static readonly int _number_colors_rain = 10;

		// Token: 0x0400003A RID: 58
		public static readonly int _number_colors_firework = 10;

		// Token: 0x0400003B RID: 59
		public static readonly int _number_colors_racing = 10;

		// Token: 0x0400003C RID: 60
		public static readonly int _width_racing = 5;

		// Token: 0x0400003D RID: 61
		public static readonly int _hue_racing = 0;
	}
}
