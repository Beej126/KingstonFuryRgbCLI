using System;

namespace FuryControllerService
{
	// Token: 0x02000012 RID: 18
	public class ddr5_static_color_mode
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00007748 File Offset: 0x00005948
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
							Class_DLL.SetKingstonFuryDDR5Style(b, 0);
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.static_color || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_static_color_mode._direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_static_color_mode._direction);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.static_color || KSDRAMSMBusCtrlObj._ir_delay[_obj.index] != ddr5_static_color_mode._ir_delay)
							{
								Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_static_color_mode._ir_delay);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.static_color || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_static_color_mode._speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_static_color_mode._speed);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.static_color || KSDRAMSMBusCtrlObj._number_colors[_obj.index] != ddr5_static_color_mode._number_colors)
							{
								Class_DLL.SetKingstonFuryDDR5SelectColor(b, (byte)ddr5_static_color_mode._number_colors);
							}
							Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
							if (ctrlmode != CTRLmode.def)
							{
								if (ctrlmode - CTRLmode.ctrl <= 1)
								{
									if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
									{
										Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_static_color_mode._brightness, 0, 100), _obj.power_saving);
									}
									if (!bResume && !_obj.reset_color_table && _obj.color_table != null && _obj.color_table.Count >= ddr5_static_color_mode._number_colors)
									{
										Miscellaneous.setDDR5ColorsTable(_obj.index, ddr5_static_color_mode._number_colors, _obj.color_table);
									}
								}
							}
							else if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_static_color_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_static_color_mode._brightness, _obj.power_saving);
							}
						}
						else
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 16);
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_static_color_mode._brightness, 0, 100), _obj.power_saving);
							}
							Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
							if (_obj.ctrl_color != null && _obj.ctrl_color.Count >= 10)
							{
								Miscellaneous.setDDR5CtrlColors(_obj.index, _obj.ctrl_color, bResume);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Static Color mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00007A80 File Offset: 0x00005C80
		public static void set_all_off(DRAMDDR5CtrlObj _obj, int iNumDimm, bool bResume = false)
		{
			try
			{
				if (_obj != null && _obj.index >= 0 && _obj.index <= 7)
				{
					Class_DLL.SetKingstonFuryDDR5Brightness((byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[_obj.index]), 0);
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("All Off mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000083 RID: 131
		public static readonly int _direction = 1;

		// Token: 0x04000084 RID: 132
		public static readonly int _ir_delay = 0;

		// Token: 0x04000085 RID: 133
		public static readonly int _speed = 0;

		// Token: 0x04000086 RID: 134
		public static readonly int _brightness = 80;

		// Token: 0x04000087 RID: 135
		public static readonly int _number_colors = 1;
	}
}
