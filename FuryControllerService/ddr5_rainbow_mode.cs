using System;

namespace FuryControllerService
{
	// Token: 0x02000011 RID: 17
	public class ddr5_rainbow_mode
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00006B08 File Offset: 0x00004D08
		public static void set_rainbow_1(DRAMDDR5CtrlObj _obj, int iAPIVer, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_mode, true);
					if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
					{
						byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]);
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_1)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 1);
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_rainbow_mode._ir_delay_rainbow_1);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_1 || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_rainbow_mode._direction_rainbow_1)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_rainbow_mode._direction_rainbow_1);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_1 || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_rainbow_mode._speed_rainbow_1)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_rainbow_mode._speed_rainbow_1);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_rainbow_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_rainbow_mode._brightness, _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_1 || KSDRAMSMBusCtrlObj._width[_obj.index] != ddr5_rainbow_mode._width_rainbow_1))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)ddr5_rainbow_mode._width_rainbow_1);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_1 || KSDRAMSMBusCtrlObj._hue[_obj.index] != ddr5_rainbow_mode._hue_rainbow_1))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)ddr5_rainbow_mode._hue_rainbow_1);
							}
							break;
						case CTRLmode.ctrl:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_1 || KSDRAMSMBusCtrlObj._direction[_obj.index] != _obj.direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)Miscellaneous.CheckValueRange(_obj.direction, ddr5_rainbow_mode._direction_rainbow_1, 1, 2));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_1 || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_rainbow_mode._speed_rainbow_1, 0.0, 60.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_rainbow_mode._brightness, 0, 100), _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_1 || KSDRAMSMBusCtrlObj._width[_obj.index] != _obj.width))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)Miscellaneous.CheckValueRange(_obj.width, ddr5_rainbow_mode._width_rainbow_1, 0, 4));
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_1 || KSDRAMSMBusCtrlObj._hue[_obj.index] != _obj.hue))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)Miscellaneous.CheckValueHue(_obj.hue, ddr5_rainbow_mode._hue_rainbow_1));
							}
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Rainbow 1 mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006ED4 File Offset: 0x000050D4
		public static void set_rainbow_2(DRAMDDR5CtrlObj _obj, int iAPIVer, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_mode, true);
					if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
					{
						byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]);
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_2)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 17);
						}
						if (KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_rainbow_mode._direction_rainbow_2)
						{
							Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_rainbow_mode._direction_rainbow_2);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_2 || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_rainbow_mode._ir_delay_rainbow_2)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_rainbow_mode._ir_delay_rainbow_2);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_2 || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_rainbow_mode._speed_rainbow_2)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_rainbow_mode._speed_rainbow_2);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_rainbow_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_rainbow_mode._brightness, _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_2 || KSDRAMSMBusCtrlObj._width[_obj.index] != ddr5_rainbow_mode._width_rainbow_2))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)ddr5_rainbow_mode._width_rainbow_2);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_2 || KSDRAMSMBusCtrlObj._hue[_obj.index] != ddr5_rainbow_mode._hue_rainbow_2))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)ddr5_rainbow_mode._hue_rainbow_2);
							}
							break;
						case CTRLmode.ctrl:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_2 || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != _obj.ir_delay)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)Miscellaneous.CheckValueRange(_obj.ir_delay, ddr5_rainbow_mode._ir_delay_rainbow_2, 2, 6));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_2 || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_rainbow_mode._speed_rainbow_2, 0.0, 60.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_rainbow_mode._brightness, 0, 100), _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_2 || KSDRAMSMBusCtrlObj._width[_obj.index] != ddr5_rainbow_mode._width_rainbow_2))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)ddr5_rainbow_mode._width_rainbow_2);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_2 || KSDRAMSMBusCtrlObj._hue[_obj.index] != ddr5_rainbow_mode._hue_rainbow_2))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)ddr5_rainbow_mode._hue_rainbow_2);
							}
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Rainbow 2 mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000072B8 File Offset: 0x000054B8
		public static void set_rainbow_3(DRAMDDR5CtrlObj _obj, int iAPIVer, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					CTRLmode ctrlmode = (CTRLmode)Enum.Parse(typeof(CTRLmode), _obj.ctrl_mode, true);
					if (Enum.IsDefined(typeof(CTRLmode), ctrlmode))
					{
						byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]);
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_3)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 1);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_3 || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_rainbow_mode._direction_rainbow_3)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_rainbow_mode._direction_rainbow_3);
							}
							if (KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_rainbow_mode._ir_delay_rainbow_3)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_rainbow_mode._ir_delay_rainbow_3);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_3 || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_rainbow_mode._speed_rainbow_3)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_rainbow_mode._speed_rainbow_3);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_rainbow_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_rainbow_mode._brightness, _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_3 || KSDRAMSMBusCtrlObj._width[_obj.index] != ddr5_rainbow_mode._width_rainbow_3))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)ddr5_rainbow_mode._width_rainbow_3);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_3 || KSDRAMSMBusCtrlObj._hue[_obj.index] != ddr5_rainbow_mode._hue_rainbow_3))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)ddr5_rainbow_mode._hue_rainbow_3);
							}
							break;
						case CTRLmode.ctrl:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_3 || KSDRAMSMBusCtrlObj._direction[_obj.index] != _obj.direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)Miscellaneous.CheckValueRange(_obj.direction, ddr5_rainbow_mode._direction_rainbow_3, 1, 2));
							}
							if (KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != _obj.ir_delay)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)Miscellaneous.CheckValueRange(_obj.ir_delay, ddr5_rainbow_mode._ir_delay_rainbow_3, 2, 4));
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_3 || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_rainbow_mode._speed_rainbow_3, 0.0, 60.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_rainbow_mode._brightness, 0, 100), _obj.power_saving);
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_3 || KSDRAMSMBusCtrlObj._width[_obj.index] != _obj.width))
							{
								Class_DLL.SetKingstonFuryDDR5Width(b, (byte)Miscellaneous.CheckValueRange(_obj.width, ddr5_rainbow_mode._width_rainbow_3, 0, 4));
							}
							if ((iAPIVer == 3 || iAPIVer == 4) && (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.rainbow_3 || KSDRAMSMBusCtrlObj._hue[_obj.index] != _obj.hue))
							{
								Class_DLL.SetKingstonFuryDDR5Hue(b, (byte)Miscellaneous.CheckValueHue(_obj.hue, ddr5_rainbow_mode._hue_rainbow_3));
							}
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Rainbow 3 mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000073 RID: 115
		public static readonly int _direction_rainbow_1 = 1;

		// Token: 0x04000074 RID: 116
		public static readonly int _direction_rainbow_2 = 1;

		// Token: 0x04000075 RID: 117
		public static readonly int _direction_rainbow_3 = 1;

		// Token: 0x04000076 RID: 118
		public static readonly int _ir_delay_rainbow_1 = 0;

		// Token: 0x04000077 RID: 119
		public static readonly int _ir_delay_rainbow_2 = 2;

		// Token: 0x04000078 RID: 120
		public static readonly int _ir_delay_rainbow_3 = 4;

		// Token: 0x04000079 RID: 121
		public static readonly int _speed_rainbow_1 = 25;

		// Token: 0x0400007A RID: 122
		public static readonly int _speed_rainbow_2 = 40;

		// Token: 0x0400007B RID: 123
		public static readonly int _speed_rainbow_3 = 40;

		// Token: 0x0400007C RID: 124
		public static readonly int _brightness = 100;

		// Token: 0x0400007D RID: 125
		public static readonly int _width_rainbow_1 = 0;

		// Token: 0x0400007E RID: 126
		public static readonly int _width_rainbow_2 = 0;

		// Token: 0x0400007F RID: 127
		public static readonly int _width_rainbow_3 = 0;

		// Token: 0x04000080 RID: 128
		public static readonly int _hue_rainbow_1 = 0;

		// Token: 0x04000081 RID: 129
		public static readonly int _hue_rainbow_2 = 0;

		// Token: 0x04000082 RID: 130
		public static readonly int _hue_rainbow_3 = 0;
	}
}
