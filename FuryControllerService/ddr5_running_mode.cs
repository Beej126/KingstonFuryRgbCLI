using System;
using System.Collections.Generic;

namespace FuryControllerService
{
	// Token: 0x0200000F RID: 15
	public class ddr5_running_mode
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00004ED8 File Offset: 0x000030D8
		public static void set(DRAMDDR5CtrlObj _obj, int iAPIVer, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_mode, true);
					if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
					{
						byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]);
						if ((iAPIVer == 3 || iAPIVer == 4) && _obj.multicolor)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 13);
						}
						else
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 5);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_running_mode._direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_running_mode._direction);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_running_mode._ir_delay)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_running_mode._ir_delay);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_running_mode._speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_running_mode._speed);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_running_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_running_mode._brightness, _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._width[_obj.index] != ddr5_running_mode._width))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)ddr5_running_mode._width);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._hue[_obj.index] != ddr5_running_mode._hue))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)ddr5_running_mode._hue);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != ddr5_running_mode._number_LED)
							{
								Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)ddr5_running_mode._number_LED);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_running_mode._number_colors)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_running_mode._number_colors);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._direction[_obj.index] != _obj.direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)Miscellaneous.CheckValueRange(_obj.direction, ddr5_running_mode._direction, 1, 2));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != _obj.ir_delay)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)Miscellaneous.CheckValueRange(_obj.ir_delay, ddr5_running_mode._ir_delay, 0, 32));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_running_mode._speed, 0.0, 255.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_running_mode._brightness, 0, 100), _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._width[_obj.index] != _obj.width))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)Miscellaneous.CheckValueRange(_obj.width, ddr5_running_mode._width, 0, 4));
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._hue[_obj.index] != _obj.hue))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)Miscellaneous.CheckValueRange(_obj.hue, ddr5_running_mode._hue, 0, 80));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != _obj.led_number)
							{
								Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)Miscellaneous.CheckValueRange(_obj.led_number, ddr5_running_mode._number_LED, 1, 32));
							}
							if (_obj.background_color != null && _obj.background_color.Count >= 3)
							{
								Miscellaneous.setDDR5BackgroundColor(_obj.index, _obj.background_color);
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.running || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
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
				Log.LibCmdLogWriter(string.Format("Running (Wind) mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00005488 File Offset: 0x00003688
		public static void set_slide(DRAMDDR5CtrlObj _obj, int iAPIVer, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_mode, true);
					if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
					{
						byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]);
						if ((iAPIVer == 3 || iAPIVer == 4) && _obj.multicolor)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 13);
						}
						else
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 5);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_running_mode._direction_slide)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_running_mode._direction_slide);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_running_mode._ir_delay_slide)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_running_mode._ir_delay_slide);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_running_mode._speed_slide)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_running_mode._speed_slide);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_running_mode._brightness_slide || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_running_mode._brightness_slide, _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._width[_obj.index] != ddr5_running_mode._width_slide))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)ddr5_running_mode._width_slide);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._hue[_obj.index] != ddr5_running_mode._hue_slide))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)ddr5_running_mode._hue_slide);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != ddr5_running_mode._number_LED_slide)
							{
								Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)ddr5_running_mode._number_LED_slide);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_running_mode._number_colors_slide)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_running_mode._number_colors_slide);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._direction[_obj.index] != _obj.direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)Miscellaneous.CheckValueRange(_obj.direction, ddr5_running_mode._direction_slide, 1, 2));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != _obj.ir_delay)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)Miscellaneous.CheckValueRange(_obj.ir_delay, ddr5_running_mode._ir_delay_slide, 1, 4));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_running_mode._speed_slide, 0.0, 255.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_running_mode._brightness_slide, 0, 100), _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._width[_obj.index] != _obj.width))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)Miscellaneous.CheckValueRange(_obj.width, ddr5_running_mode._width_slide, 0, 4));
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._hue[_obj.index] != _obj.hue))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)Miscellaneous.CheckValueRange(_obj.hue, ddr5_running_mode._hue_slide, 0, 80));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != _obj.led_number)
							{
								Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)Miscellaneous.CheckValueRange(_obj.led_number, ddr5_running_mode._number_LED_slide, 1, 12));
							}
							if (_obj.background_color != null && _obj.background_color.Count >= 3)
							{
								Miscellaneous.setDDR5BackgroundColor(_obj.index, _obj.background_color);
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.slide || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
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
				Log.LibCmdLogWriter(string.Format("Slide mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005A38 File Offset: 0x00003C38
		public static void set_cross(DRAMDDR5CtrlObj _obj, int iAPIVer, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_mode, true);
					if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
					{
						byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]);
						if ((iAPIVer == 3 || iAPIVer == 4) && _obj.multicolor)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 13);
						}
						else
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 5);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_running_mode._ir_delay_cross)
						{
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_running_mode._ir_delay_cross);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_running_mode._direction_cross[_obj.index % 2])
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_running_mode._direction_cross[_obj.index % 2]);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_running_mode._speed_cross)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_running_mode._speed_cross);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_running_mode._brightness_cross || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_running_mode._brightness_cross, _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._width[_obj.index] != ddr5_running_mode._width_cross))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)ddr5_running_mode._width_cross);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._hue[_obj.index] != ddr5_running_mode._hue_cross))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)ddr5_running_mode._hue_cross);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != ddr5_running_mode._number_LED_cross)
							{
								Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)ddr5_running_mode._number_LED_cross);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_running_mode._number_colors_cross)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_running_mode._number_colors_cross);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_running_mode._direction_cross[_obj.index % 2])
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_running_mode._direction_cross[_obj.index % 2]);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_running_mode._speed_cross, 0.0, 255.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_running_mode._brightness_cross, 0, 100), _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._width[_obj.index] != _obj.width))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)Miscellaneous.CheckValueRange(_obj.width, ddr5_running_mode._width_cross, 0, 4));
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._hue[_obj.index] != _obj.hue))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)Miscellaneous.CheckValueRange(_obj.hue, ddr5_running_mode._hue_cross, 0, 80));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != _obj.led_number)
							{
								Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)Miscellaneous.CheckValueRange(_obj.led_number, ddr5_running_mode._number_LED_cross, 1, 12));
							}
							if (_obj.background_color != null && _obj.background_color.Count >= 3)
							{
								Miscellaneous.setDDR5BackgroundColor(_obj.index, _obj.background_color);
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.cross || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
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
				Log.LibCmdLogWriter(string.Format("Cross (Teleport) mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005FC8 File Offset: 0x000041C8
		public static void set_snake(DRAMDDR5CtrlObj _obj, int iAPIVer, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_mode, true);
					if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
					{
						byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]);
						if ((iAPIVer == 3 || iAPIVer == 4) && _obj.multicolor)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 13);
						}
						else
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 5);
						}
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_running_mode._ir_delay_snake)
						{
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_running_mode._ir_delay_snake);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_running_mode._direction_snake[_obj.index % 2])
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_running_mode._direction_snake[_obj.index % 2]);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_running_mode._speed_snake)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_running_mode._speed_snake);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_running_mode._brightness_snake || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_running_mode._brightness_snake, _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._width[_obj.index] != ddr5_running_mode._width_snake))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)ddr5_running_mode._width_snake);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._hue[_obj.index] != ddr5_running_mode._hue_snake))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)ddr5_running_mode._hue_snake);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != ddr5_running_mode._number_LED_snake)
							{
								Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)ddr5_running_mode._number_LED_snake);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_running_mode._number_colors_snake)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_running_mode._number_colors_snake);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_running_mode._direction_snake[_obj.index % 2])
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_running_mode._direction_snake[_obj.index % 2]);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_running_mode._speed_snake, 0.0, 255.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_running_mode._brightness_snake, 0, 100), _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._width[_obj.index] != _obj.width))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)Miscellaneous.CheckValueRange(_obj.width, ddr5_running_mode._width_snake, 0, 4));
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._hue[_obj.index] != _obj.hue))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)Miscellaneous.CheckValueRange(_obj.hue, ddr5_running_mode._hue_snake, 0, 80));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._number_of_leds[_obj.index] != _obj.led_number)
							{
								Class_DLL.SetKingstonFuryDDR5NumLED(b, (byte)Miscellaneous.CheckValueRange(_obj.led_number, ddr5_running_mode._number_LED_snake, 1, 32));
							}
							if (_obj.background_color != null && _obj.background_color.Count >= 3)
							{
								Miscellaneous.setDDR5BackgroundColor(_obj.index, _obj.background_color);
							}
							if (_obj.number_colors > 0 && _obj.number_colors <= 10)
							{
								if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.snake || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != _obj.number_colors)
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
				Log.LibCmdLogWriter(string.Format("Snake mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000049 RID: 73
		public static readonly int _direction = 1;

		// Token: 0x0400004A RID: 74
		public static readonly int _direction_slide = 2;

		// Token: 0x0400004B RID: 75
		public static readonly List<int> _direction_cross = new List<int> { 1, 2 };

		// Token: 0x0400004C RID: 76
		public static readonly List<int> _direction_snake = new List<int> { 1, 2 };

		// Token: 0x0400004D RID: 77
		public static readonly int _ir_delay = 0;

		// Token: 0x0400004E RID: 78
		public static readonly int _ir_delay_slide = 3;

		// Token: 0x0400004F RID: 79
		public static readonly int _ir_delay_cross = 0;

		// Token: 0x04000050 RID: 80
		public static readonly int _ir_delay_snake = 12;

		// Token: 0x04000051 RID: 81
		public static readonly int _speed = 21;

		// Token: 0x04000052 RID: 82
		public static readonly int _speed_slide = 8;

		// Token: 0x04000053 RID: 83
		public static readonly int _speed_cross = 8;

		// Token: 0x04000054 RID: 84
		public static readonly int _speed_snake = 40;

		// Token: 0x04000055 RID: 85
		public static readonly int _brightness = 80;

		// Token: 0x04000056 RID: 86
		public static readonly int _brightness_slide = 80;

		// Token: 0x04000057 RID: 87
		public static readonly int _brightness_cross = 80;

		// Token: 0x04000058 RID: 88
		public static readonly int _brightness_snake = 80;

		// Token: 0x04000059 RID: 89
		public static readonly int _width = 0;

		// Token: 0x0400005A RID: 90
		public static readonly int _width_slide = 0;

		// Token: 0x0400005B RID: 91
		public static readonly int _width_cross = 0;

		// Token: 0x0400005C RID: 92
		public static readonly int _width_snake = 0;

		// Token: 0x0400005D RID: 93
		public static readonly int _hue = 0;

		// Token: 0x0400005E RID: 94
		public static readonly int _hue_slide = 0;

		// Token: 0x0400005F RID: 95
		public static readonly int _hue_cross = 0;

		// Token: 0x04000060 RID: 96
		public static readonly int _hue_snake = 0;

		// Token: 0x04000061 RID: 97
		public static readonly List<int> _bg_color = new List<int> { 16, 16, 16 };

		// Token: 0x04000062 RID: 98
		public static readonly List<int> _bg_color_slide = new List<int> { 16, 16, 16 };

		// Token: 0x04000063 RID: 99
		public static readonly List<int> _bg_color_cross = new List<int> { 16, 16, 16 };

		// Token: 0x04000064 RID: 100
		public static readonly List<int> _bg_color_snake = new List<int> { 16, 16, 16 };

		// Token: 0x04000065 RID: 101
		public static readonly int _number_LED = 12;

		// Token: 0x04000066 RID: 102
		public static readonly int _number_LED_slide = 4;

		// Token: 0x04000067 RID: 103
		public static readonly int _number_LED_cross = 3;

		// Token: 0x04000068 RID: 104
		public static readonly int _number_LED_snake = 12;

		// Token: 0x04000069 RID: 105
		public static readonly int _number_colors = 10;

		// Token: 0x0400006A RID: 106
		public static readonly int _number_colors_slide = 10;

		// Token: 0x0400006B RID: 107
		public static readonly int _number_colors_cross = 10;

		// Token: 0x0400006C RID: 108
		public static readonly int _number_colors_snake = 10;
	}
}
