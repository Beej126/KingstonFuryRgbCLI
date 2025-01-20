using System;

namespace KingstonFuryRgbCLI
{
	// Token: 0x02000005 RID: 5
	public class ddr5_starry_sky_mode
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000022B4 File Offset: 0x000004B4
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
						if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.starry_sky)
						{
							Class_DLL.SetKingstonFuryDDR5Style(b, 10);
							Class_DLL.SetKingstonFuryDDR5IRDelay(b, (byte)ddr5_starry_sky_mode._ir_delay);
						}
						Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(iNumDimm, 4, 1, 4));
						switch (ctrlmode)
						{
						case CTRLmode.def:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.starry_sky || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_starry_sky_mode._direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_starry_sky_mode._direction);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.starry_sky || KSDRAMSMBusCtrlObj._speed[_obj.index] != ddr5_starry_sky_mode._speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)ddr5_starry_sky_mode._speed);
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != ddr5_starry_sky_mode._brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, ddr5_starry_sky_mode._brightness, _obj.power_saving);
							}
							break;
						case CTRLmode.ctrl:
						case CTRLmode.ctrl_color:
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.starry_sky || KSDRAMSMBusCtrlObj._direction[_obj.index] != ddr5_starry_sky_mode._direction)
							{
								Class_DLL.SetKingstonFuryDDR5Direction(b, (byte)ddr5_starry_sky_mode._direction);
							}
							if (KSDRAMSMBusCtrlObj._mode[_obj.index] != DDR5LEDmode.starry_sky || KSDRAMSMBusCtrlObj._speed[_obj.index] != _obj.speed)
							{
								Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)Miscellaneous.PercentageToValueRange(_obj.speed, ddr5_starry_sky_mode._speed, 0.0, 255.0));
							}
							if (KSDRAMSMBusCtrlObj._brightness[_obj.index] != _obj.brightness || KSDRAMSMBusCtrlObj._power_saving[_obj.index] != _obj.power_saving)
							{
								Miscellaneous.setDDR5Brightness(b, Miscellaneous.CheckValueRange(_obj.brightness, ddr5_starry_sky_mode._brightness, 0, 100), _obj.power_saving);
							}
							break;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Starry Sky mode : {0}", ex.Message), true);
			}
		}

		// Token: 0x04000005 RID: 5
		public static readonly int _direction = 1;

		// Token: 0x04000006 RID: 6
		public static readonly int _ir_delay = 0;

		// Token: 0x04000007 RID: 7
		public static readonly int _speed = 64;

		// Token: 0x04000008 RID: 8
		public static readonly int _brightness = 80;
	}
}
