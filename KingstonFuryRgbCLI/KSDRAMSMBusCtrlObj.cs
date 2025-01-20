using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace KingstonFuryRgbCLI
{
	// Token: 0x02000021 RID: 33
	public class KSDRAMSMBusCtrlObj
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00008334 File Offset: 0x00006534
		public KSDRAMSMBusCtrlObj(Dictionary<string, DRAMInfoObj> _dDRAMInfos, bool _bExpire)
		{
			if (_dDRAMInfos != null && _bExpire)
			{
				this.dDRAMInfos = _dDRAMInfos;
				KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2 = new List<bool> { false, false, false, false, false, false, false, false };
				for (int i = 0; i < this.dDRAMInfos.Count; i++)
				{
					if (this.dDRAMInfos.ElementAt(i).Value.api_ver == 2 || this.dDRAMInfos.ElementAt(i).Value.api_ver == 3 || this.dDRAMInfos.ElementAt(i).Value.api_ver == 4)
					{
						int num = this.dDRAMInfos.ElementAt(i).Value.index % 8;
						if (num >= 0 && num < 8)
						{
							KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2[num] = true;
							KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[num] = this.dDRAMInfos.ElementAt(i).Value.address_offset;
							KSDRAMSMBusCtrlObj.bDRAMMasterSlave_v2[num] = this.dDRAMInfos.ElementAt(i).Value.master_slave;
						}
						else
						{
							Log.LibCmdLogWriter(string.Format("index = {0} tempIndex = {1} master_slave = {2} address_offset = {3}", new object[]
							{
								this.dDRAMInfos.ElementAt(i).Value.index.ToString(),
								num.ToString(),
								this.dDRAMInfos.ElementAt(i).Value.master_slave.ToString(),
								this.dDRAMInfos.ElementAt(i).Value.address_offset.ToString()
							}), true);
						}
					}
				}
				KSDRAMSMBusCtrlObj._mode = new List<DDR5LEDmode>
				{
					DDR5LEDmode.rainbow_1,
					DDR5LEDmode.rainbow_1,
					DDR5LEDmode.rainbow_1,
					DDR5LEDmode.rainbow_1,
					DDR5LEDmode.rainbow_1,
					DDR5LEDmode.rainbow_1,
					DDR5LEDmode.rainbow_1,
					DDR5LEDmode.rainbow_1
				};
				KSDRAMSMBusCtrlObj._direction = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };
				KSDRAMSMBusCtrlObj._ir_delay = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };
				KSDRAMSMBusCtrlObj._speed = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };
				KSDRAMSMBusCtrlObj._brightness = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };
				KSDRAMSMBusCtrlObj._width = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };
				KSDRAMSMBusCtrlObj._hue = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };
				KSDRAMSMBusCtrlObj._number_of_leds = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };
				KSDRAMSMBusCtrlObj._number_colors = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };
				KSDRAMSMBusCtrlObj._power_saving = new List<bool> { false, false, false, false, false, false, false, false };
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000087B4 File Offset: 0x000069B4
		public int SetDRAMCmdObj(DRAMCmdObj _obj)
		{
			if (this.dDRAMInfos != null && this.dDRAMInfos.Count > 0)
			{
				if (_obj != null)
				{
					if (_obj.ctrl_settings_ddr5 != null && _obj.ctrl_settings_ddr5.Count > 0)
					{
						this._DRAMCmdObj_v2 = _obj;
						return 0;
					}
					if (_obj.ctrl_settings != null && !string.IsNullOrEmpty(_obj.ctrl_settings.mode) && !string.IsNullOrEmpty(_obj.ctrl_settings.ctrl_mode))
					{
						if (_obj.ctrl_settings.ctrl_mode == CTRLmode.independence.ToString() && !this.CheckDRAMSlot(_obj))
						{
							Log.LibCmdLogWriter(string.Format("DRAM Info : Information not match.", Array.Empty<object>()), true);
							return 2;
						}
						this._DRAMCmdObj_v1 = _obj;
						return 0;
					}
				}
				Log.LibCmdLogWriter(string.Format("DRAM LED Object (set) : invalid.", Array.Empty<object>()), true);
				return 3;
			}
			Log.LibCmdLogWriter(string.Format("DRAM Info : Empty.", Array.Empty<object>()), true);
			return 1;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000088A9 File Offset: 0x00006AA9
		public DRAMCmdObj GetDRAMCmdObj()
		{
			if (this._DRAMCmdObj_v2 != null)
			{
				return this._DRAMCmdObj_v2;
			}
			if (this._DRAMCmdObj_v1 != null)
			{
				return this._DRAMCmdObj_v1;
			}
			return null;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000088CC File Offset: 0x00006ACC
		public bool ApplyDRAMLEDmode(int iAPIVer, bool bResume = false)
		{
			if (iAPIVer == 1 && this._DRAMCmdObj_v1 != null && !string.IsNullOrEmpty(this._DRAMCmdObj_v1.ctrl_settings.mode) && !string.IsNullOrEmpty(this._DRAMCmdObj_v1.ctrl_settings.ctrl_mode))
			{
				try
				{
					if (Native.WaitForSingleObject(FuryController_Service.IntPtr_DRAM, 3000U) == 0U)
					{
						Class_DLL.ParameterStart();
						LEDmode ledmode = (LEDmode)Enum.Parse(typeof(LEDmode), this._DRAMCmdObj_v1.ctrl_settings.mode, true);
						if (Enum.IsDefined(typeof(LEDmode), ledmode))
						{
							switch (ledmode)
							{
							case LEDmode.all_off:
								all_off_mode.set(this._DRAMCmdObj_v1);
								break;
							case LEDmode.running:
								running_mode.set(this._DRAMCmdObj_v1);
								break;
							case LEDmode.breath:
								breath_mode.set(this._DRAMCmdObj_v1);
								break;
							case LEDmode.color_cycle:
								color_cycle_mode.set(this._DRAMCmdObj_v1);
								break;
							case LEDmode.rainbow:
								rainbow_mode.set(this._DRAMCmdObj_v1);
								break;
							case LEDmode.blink:
								blink_mode.set(this._DRAMCmdObj_v1);
								break;
							case LEDmode.double_blink:
								double_blink_mode.set(this._DRAMCmdObj_v1);
								break;
							case LEDmode.meteor:
								meteor_mode.set(this._DRAMCmdObj_v1);
								break;
							case LEDmode.static_color:
								static_color_mode.set(this._DRAMCmdObj_v1);
								break;
							}
						}
						Thread.Sleep(10);
						Class_DLL.ParameterDone();
						Thread.Sleep(10);
						Class_DLL.ParameterRun();
					}
				}
				catch (Exception ex)
				{
					Log.LibCmdLogWriter(string.Format("DRAM LED Object (apply) : {0}", ex.Message), true);
				}
				finally
				{
					if (FuryController_Service.IntPtr_DRAM != IntPtr.Zero)
					{
						try
						{
							Native.ReleaseMutex(FuryController_Service.IntPtr_DRAM);
						}
						catch (Exception)
						{
						}
					}
				}
				return true;
			}
			if ((iAPIVer == 2 || iAPIVer == 3 || iAPIVer == 4) && this._DRAMCmdObj_v2 != null && this._DRAMCmdObj_v2.ctrl_settings_ddr5 != null && this._DRAMCmdObj_v2.ctrl_settings_ddr5.Count > 0 && this.dDRAMInfos != null)
			{
				try
				{
					if (Native.WaitForSingleObject(FuryController_Service.IntPtr_DRAM, 3000U) == 0U)
					{
						try
						{
							this.CommandStart();
							for (int i = 0; i < KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2.Count; i++)
							{
								string text = "slot_" + i.ToString();
								if (KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2[i] && this._DRAMCmdObj_v2.ctrl_settings_ddr5.ContainsKey(text))
								{
									byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text].index]);
									DDR5LEDmode ddr5LEDmode = (DDR5LEDmode)Enum.Parse(typeof(DDR5LEDmode), this._DRAMCmdObj_v2.ctrl_settings_ddr5[text].mode, true);
									if (this._DRAMCmdObj_v2.ctrl_settings_ddr5[text].reset_default_effect)
									{
										if (iAPIVer == 2 || iAPIVer == 4)
										{
											this._DRAMCmdObj_v2.ctrl_settings_ddr5[text].mode = "rainbow_1";
											this._DRAMCmdObj_v2.ctrl_settings_ddr5[text].ctrl_mode = "def";
											ddr5LEDmode = DDR5LEDmode.rainbow_1;
										}
										else if (iAPIVer == 3)
										{
											this._DRAMCmdObj_v2.ctrl_settings_ddr5[text].mode = "racing";
											this._DRAMCmdObj_v2.ctrl_settings_ddr5[text].ctrl_mode = "def";
											ddr5LEDmode = DDR5LEDmode.racing;
										}
									}
									if (iAPIVer == 4)
									{
										Class_DLL.SetKingstonFuryDDR5IRTransConfig(b, 0);
									}
									if (Enum.IsDefined(typeof(DDR5LEDmode), ddr5LEDmode))
									{
										if (ddr5LEDmode <= DDR5LEDmode.snake)
										{
											if (ddr5LEDmode <= DDR5LEDmode.rainbow_3)
											{
												if (ddr5LEDmode <= DDR5LEDmode.all_off)
												{
													if (ddr5LEDmode != DDR5LEDmode.static_color)
													{
														if (ddr5LEDmode != DDR5LEDmode.all_off)
														{
															goto IL_0453;
														}
														goto IL_0453;
													}
												}
												else if (ddr5LEDmode != DDR5LEDmode.rainbow_1 && ddr5LEDmode != DDR5LEDmode.rainbow_3)
												{
													goto IL_0453;
												}
											}
											else if (ddr5LEDmode <= DDR5LEDmode.breath)
											{
												if (ddr5LEDmode != DDR5LEDmode.ecg && ddr5LEDmode != DDR5LEDmode.breath)
												{
													goto IL_0453;
												}
											}
											else if (ddr5LEDmode != DDR5LEDmode.dynamic_color && ddr5LEDmode - DDR5LEDmode.running > 3)
											{
												goto IL_0453;
											}
										}
										else if (ddr5LEDmode <= DDR5LEDmode.count_down)
										{
											if (ddr5LEDmode <= DDR5LEDmode.racing)
											{
												if (ddr5LEDmode != DDR5LEDmode.comet)
												{
													if (ddr5LEDmode - DDR5LEDmode.rain > 2)
													{
														goto IL_0453;
													}
													Class_DLL.SetKingstonFuryDDR5MasterSlaveRole(b, 0);
													goto IL_0453;
												}
											}
											else if (ddr5LEDmode != DDR5LEDmode.electric_current && ddr5LEDmode != DDR5LEDmode.count_down)
											{
												goto IL_0453;
											}
										}
										else if (ddr5LEDmode <= DDR5LEDmode.starry_sky)
										{
											if (ddr5LEDmode != DDR5LEDmode.flame && ddr5LEDmode != DDR5LEDmode.starry_sky)
											{
												goto IL_0453;
											}
										}
										else if (ddr5LEDmode != DDR5LEDmode.fury && ddr5LEDmode != DDR5LEDmode.rainbow_2)
										{
											goto IL_0453;
										}
										Class_DLL.SetKingstonFuryDDR5MasterSlaveRole(b, (byte)KSDRAMSMBusCtrlObj.bDRAMMasterSlave_v2[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text].index]);
									}
								}
								IL_0453:;
							}
							this.CommandDoneAndRun();
							this.CommandStart();
							for (int j = 0; j < KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2.Count; j++)
							{
								string text2 = "slot_" + j.ToString();
								if (KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2[j] && this._DRAMCmdObj_v2.ctrl_settings_ddr5.ContainsKey(text2))
								{
									DDR5LEDmode ddr5LEDmode2 = (DDR5LEDmode)Enum.Parse(typeof(DDR5LEDmode), this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].mode, true);
									if (Enum.IsDefined(typeof(DDR5LEDmode), ddr5LEDmode2))
									{
										if (ddr5LEDmode2 <= DDR5LEDmode.dynamic_color)
										{
											if (ddr5LEDmode2 <= DDR5LEDmode.rainbow_1)
											{
												if (ddr5LEDmode2 != DDR5LEDmode.static_color)
												{
													if (ddr5LEDmode2 != DDR5LEDmode.all_off)
													{
														if (ddr5LEDmode2 == DDR5LEDmode.rainbow_1)
														{
															ddr5_rainbow_mode.set_rainbow_1(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], iAPIVer, this.dDRAMInfos.Count, bResume);
														}
													}
													else
													{
														ddr5_static_color_mode.set_all_off(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
													}
												}
												else
												{
													ddr5_static_color_mode.set(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
												}
											}
											else if (ddr5LEDmode2 <= DDR5LEDmode.ecg)
											{
												if (ddr5LEDmode2 != DDR5LEDmode.rainbow_3)
												{
													if (ddr5LEDmode2 == DDR5LEDmode.ecg)
													{
														ddr5_ecg_mode.set(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
													}
												}
												else
												{
													ddr5_rainbow_mode.set_rainbow_3(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], iAPIVer, this.dDRAMInfos.Count, bResume);
												}
											}
											else if (ddr5LEDmode2 != DDR5LEDmode.breath)
											{
												if (ddr5LEDmode2 == DDR5LEDmode.dynamic_color)
												{
													ddr5_dynamic_color_mode.set(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
												}
											}
											else
											{
												ddr5_breath_mode.set(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
											}
										}
										else if (ddr5LEDmode2 <= DDR5LEDmode.count_down)
										{
											if (ddr5LEDmode2 <= DDR5LEDmode.racing)
											{
												switch (ddr5LEDmode2)
												{
												case DDR5LEDmode.running:
													ddr5_running_mode.set(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], iAPIVer, this.dDRAMInfos.Count, bResume);
													break;
												case DDR5LEDmode.slide:
													ddr5_running_mode.set_slide(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], iAPIVer, this.dDRAMInfos.Count, bResume);
													break;
												case DDR5LEDmode.cross:
													ddr5_running_mode.set_cross(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], iAPIVer, this.dDRAMInfos.Count, bResume);
													break;
												case DDR5LEDmode.snake:
													ddr5_running_mode.set_snake(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], iAPIVer, this.dDRAMInfos.Count, bResume);
													break;
												default:
													switch (ddr5LEDmode2)
													{
													case DDR5LEDmode.comet:
														ddr5_comet_mode.set(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
														break;
													case DDR5LEDmode.rain:
														ddr5_comet_mode.set_rain(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
														break;
													case DDR5LEDmode.firework:
														ddr5_comet_mode.set_firework(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
														break;
													case DDR5LEDmode.racing:
														if (iAPIVer == 3 || iAPIVer == 4)
														{
															ddr5_comet_mode.set_racing(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], iAPIVer, this.dDRAMInfos.Count, bResume);
														}
														break;
													}
													break;
												}
											}
											else if (ddr5LEDmode2 != DDR5LEDmode.electric_current)
											{
												if (ddr5LEDmode2 == DDR5LEDmode.count_down)
												{
													ddr5_count_down_mode.set(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
												}
											}
											else
											{
												ddr5_electric_current_mode.set(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
											}
										}
										else if (ddr5LEDmode2 <= DDR5LEDmode.starry_sky)
										{
											if (ddr5LEDmode2 != DDR5LEDmode.flame)
											{
												if (ddr5LEDmode2 == DDR5LEDmode.starry_sky)
												{
													ddr5_starry_sky_mode.set(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
												}
											}
											else
											{
												ddr5_flame_mode.set(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
											}
										}
										else if (ddr5LEDmode2 != DDR5LEDmode.fury)
										{
											if (ddr5LEDmode2 == DDR5LEDmode.rainbow_2)
											{
												ddr5_rainbow_mode.set_rainbow_2(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], iAPIVer, this.dDRAMInfos.Count, bResume);
											}
										}
										else
										{
											ddr5_fury_mode.set(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2], this.dDRAMInfos.Count, bResume);
										}
										KSDRAMSMBusCtrlObj._mode[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index] = ddr5LEDmode2;
										KSDRAMSMBusCtrlObj._direction[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index] = this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].direction;
										KSDRAMSMBusCtrlObj._ir_delay[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index] = this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].ir_delay;
										KSDRAMSMBusCtrlObj._speed[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index] = this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].speed;
										KSDRAMSMBusCtrlObj._brightness[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index] = this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].brightness;
										if ((iAPIVer == 3 || iAPIVer == 4) && (this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].multicolor || ddr5LEDmode2 == DDR5LEDmode.rainbow_1 || ddr5LEDmode2 == DDR5LEDmode.rainbow_3))
										{
											KSDRAMSMBusCtrlObj._width[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index] = this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].width;
											KSDRAMSMBusCtrlObj._hue[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index] = this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].hue;
										}
										KSDRAMSMBusCtrlObj._multicolor[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index] = this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].multicolor;
										KSDRAMSMBusCtrlObj._number_of_leds[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index] = this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].led_number;
										KSDRAMSMBusCtrlObj._number_colors[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index] = this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].number_colors;
										KSDRAMSMBusCtrlObj._power_saving[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index] = this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].power_saving;
									}
									if (bResume)
									{
										Miscellaneous.ResumeDDR5ColorsTable(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index);
									}
									if (this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].reset_color_table)
									{
										Miscellaneous.resetDDR5ColorsTable(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index);
										this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].reset_color_table = false;
										if (this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].background_color != null && Miscellaneous.ddr5_bg_color_default.ContainsKey(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index.ToString()))
										{
											this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].background_color = Miscellaneous.ddr5_bg_color_default[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index.ToString()];
										}
										if (this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].color_table != null && Miscellaneous.ddr5_color_table_default.ContainsKey(this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index.ToString()) && Miscellaneous.ddr5_color_table_default[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index.ToString()].Count >= this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].color_table.Count)
										{
											for (int k = 0; k < this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].color_table.Count; k++)
											{
												this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].color_table[k] = Miscellaneous.ddr5_color_table_default[this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].index.ToString()][k];
											}
										}
									}
									this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].reset_default_effect = false;
									this._DRAMCmdObj_v2.ctrl_settings_ddr5[text2].reset_color_table = false;
								}
							}
							this.CommandDoneAndRun();
						}
						catch (Exception ex2)
						{
							Log.LibCmdLogWriter(string.Format("DRAM LED Object (apply) : {0}", ex2.Message), true);
						}
					}
				}
				catch (Exception ex3)
				{
					Log.LibCmdLogWriter(string.Format("DRAM LED Object Mutex (apply) : {0}", ex3.Message), true);
				}
				finally
				{
					if (FuryController_Service.IntPtr_DRAM != IntPtr.Zero)
					{
						try
						{
							Native.ReleaseMutex(FuryController_Service.IntPtr_DRAM);
						}
						catch (Exception)
						{
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000980C File Offset: 0x00007A0C
		public bool SetDRAMDDR5_AllOff()
		{
			if (this.dDRAMInfos != null && this.dDRAMInfos.Count > 0)
			{
				try
				{
					if (Native.WaitForSingleObject(FuryController_Service.IntPtr_DRAM, 3000U) == 0U)
					{
						try
						{
							this.CommandStart();
							for (int i = 0; i < KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2.Count; i++)
							{
								string text = "slot_" + i.ToString();
								if (KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2[i] && this.dDRAMInfos.ContainsKey(text))
								{
									byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[i]);
									Class_DLL.SetKingstonFuryDDR5MasterSlaveRole(b, (byte)KSDRAMSMBusCtrlObj.bDRAMMasterSlave_v2[i]);
									Class_DLL.SetKingstonFuryDDR5Brightness(b, 0);
								}
							}
							this.CommandDoneAndRun();
						}
						catch (Exception ex)
						{
							Log.LibCmdLogWriter(string.Format("DRAM LED AllOff : {0}", ex.Message), true);
						}
					}
				}
				catch (Exception ex2)
				{
					Log.LibCmdLogWriter(string.Format("DRAM LED AllOff Mutex : {0}", ex2.Message), true);
				}
				finally
				{
					if (FuryController_Service.IntPtr_DRAM != IntPtr.Zero)
					{
						try
						{
							Native.ReleaseMutex(FuryController_Service.IntPtr_DRAM);
						}
						catch (Exception)
						{
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009954 File Offset: 0x00007B54
		public bool SetDRAMDDR5_DefaultRainbow()
		{
			if (this.dDRAMInfos != null && this.dDRAMInfos.Count > 0)
			{
				try
				{
					if (Native.WaitForSingleObject(FuryController_Service.IntPtr_DRAM, 3000U) == 0U)
					{
						try
						{
							this.CommandStart();
							for (int i = 0; i < KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2.Count; i++)
							{
								string text = "slot_" + i.ToString();
								if (KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2[i] && this.dDRAMInfos.ContainsKey(text))
								{
									byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[i]);
									Class_DLL.SetKingstonFuryDDR5Style(b, 1);
									Class_DLL.SetKingstonFuryDDR5MasterSlaveRole(b, (byte)KSDRAMSMBusCtrlObj.bDRAMMasterSlave_v2[i]);
									Class_DLL.SetKingstonFuryDDR5IRDelay(b, 0);
									Class_DLL.SetKingstonFuryDDR5Direction(b, 1);
									Class_DLL.SetKingstonFuryDDR5Speed(b, 25);
									Miscellaneous.setDDR5Brightness(b, 80, false);
									Class_DLL.SetKingstonFuryDDR5Width(b, 0);
									Class_DLL.SetKingstonFuryDDR5Hue(b, 0);
									Class_DLL.SetKingstonFuryDDR5HyperSpeed(b, 0);
								}
							}
							this.CommandDoneAndRun();
						}
						catch (Exception ex)
						{
							Log.LibCmdLogWriter(string.Format("DRAM LED Default : {0}", ex.Message), true);
						}
					}
				}
				catch (Exception ex2)
				{
					Log.LibCmdLogWriter(string.Format("DRAM LED Default Mutex : {0}", ex2.Message), true);
				}
				finally
				{
					if (FuryController_Service.IntPtr_DRAM != IntPtr.Zero)
					{
						try
						{
							Native.ReleaseMutex(FuryController_Service.IntPtr_DRAM);
						}
						catch (Exception)
						{
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00009B0C File Offset: 0x00007D0C
		public bool SetDRAMDDR5_DefaultRacing()
		{
			if (this.dDRAMInfos != null && this.dDRAMInfos.Count > 0)
			{
				try
				{
					if (Native.WaitForSingleObject(FuryController_Service.IntPtr_DRAM, 3000U) == 0U)
					{
						try
						{
							List<int> list = new List<int> { 19, 8, 23, 17 };
							this.CommandStart();
							for (int i = 0; i < KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2.Count; i++)
							{
								string text = "slot_" + i.ToString();
								if (KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2[i] && this.dDRAMInfos.ContainsKey(text))
								{
									byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[i]);
									Class_DLL.SetKingstonFuryDDR5Style(b, 13);
									Class_DLL.SetKingstonFuryDDR5MasterSlaveRole(b, 0);
									Class_DLL.SetKingstonFuryDDR5IRDelay(b, 0);
									Class_DLL.SetKingstonFuryDDR5NumLED(b, 3);
									Class_DLL.SetKingstonFuryDDR5NumDimm(b, (byte)Miscellaneous.CheckValueRange(this.dDRAMInfos.Count, 4, 1, 4));
									Class_DLL.SetKingstonFuryDDR5Direction(b, 1);
									Class_DLL.SetKingstonFuryDDR5Speed(b, (byte)list[this.dDRAMInfos[text].index % 4]);
									Miscellaneous.setDDR5Brightness(b, 80, false);
									Class_DLL.SetKingstonFuryDDR5Width(b, 5);
									Class_DLL.SetKingstonFuryDDR5Hue(b, 0);
									Class_DLL.SetKingstonFuryDDR5HyperSpeed(b, 0);
								}
							}
							this.CommandDoneAndRun();
						}
						catch (Exception ex)
						{
							Log.LibCmdLogWriter(string.Format("DRAM LED Default : {0}", ex.Message), true);
						}
					}
				}
				catch (Exception ex2)
				{
					Log.LibCmdLogWriter(string.Format("DRAM LED Default Mutex : {0}", ex2.Message), true);
				}
				finally
				{
					if (FuryController_Service.IntPtr_DRAM != IntPtr.Zero)
					{
						try
						{
							Native.ReleaseMutex(FuryController_Service.IntPtr_DRAM);
						}
						catch (Exception)
						{
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009D20 File Offset: 0x00007F20
		protected bool CommandStart()
		{
			for (int i = KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2.Count - 1; i >= 0; i--)
			{
				if (KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2[i])
				{
					Class_DLL.SetKingstonFuryDDR5CmdStart((byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[i]));
				}
			}
			Thread.Sleep(50);
			return true;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009D74 File Offset: 0x00007F74
		protected bool CommandInit()
		{
			for (int i = 0; i < KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2.Count; i++)
			{
				if (KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2[i])
				{
					Class_DLL.SetKingstonFuryDDR5Init((byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[i]));
				}
			}
			return true;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00009DBC File Offset: 0x00007FBC
		protected bool CommandDoneAndRun()
		{
			for (int i = 0; i < KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2.Count; i++)
			{
				if (KSDRAMSMBusCtrlObj.bDRAMSlotUsed_v2[i])
				{
					Class_DLL.SetKingstonFuryDDR5CmdDone((byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[i]));
				}
			}
			Thread.Sleep(50);
			return true;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009E0C File Offset: 0x0000800C
		protected bool CheckDRAMSlot(DRAMCmdObj _obj)
		{
			try
			{
				if (_obj.ctrl_settings.ctrl_color != null && _obj.ctrl_settings.ctrl_color.Count > 0)
				{
					for (int i = _obj.ctrl_settings.ctrl_color.Count - 1; i >= 0; i--)
					{
						if (_obj.ctrl_settings.ctrl_color.ElementAt(i).Value != null)
						{
							int index = _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.index;
							if (this.dDRAMInfos != null && !this.dDRAMInfos.ContainsKey("slot_" + index.ToString()))
							{
								_obj.ctrl_settings.ctrl_color.Remove(_obj.ctrl_settings.ctrl_color.ElementAt(i).Key);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("DRAM LED Object (check) : {0}", ex.Message), true);
				return false;
			}
			return _obj.ctrl_settings.ctrl_color != null && _obj.ctrl_settings.ctrl_color.Count > 0;
		}

		// Token: 0x040000FC RID: 252
		private readonly Dictionary<string, DRAMInfoObj> dDRAMInfos;

		// Token: 0x040000FD RID: 253
		public DRAMCmdObj _DRAMCmdObj_v1;

		// Token: 0x040000FE RID: 254
		public DRAMCmdObj _DRAMCmdObj_v2;

		// Token: 0x040000FF RID: 255
		public static List<bool> bDRAMSlotUsed_v2 = new List<bool> { false, false, false, false, false, false, false, false };

		// Token: 0x04000100 RID: 256
		public static List<int> bDRAMAddressOffset_v2 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };

		// Token: 0x04000101 RID: 257
		public static List<int> bDRAMMasterSlave_v2 = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 };

		// Token: 0x04000102 RID: 258
		//private static bool bIsAM4 = false;

		// Token: 0x04000103 RID: 259
		public static List<DDR5LEDmode> _mode = new List<DDR5LEDmode>
		{
			DDR5LEDmode.rainbow_1,
			DDR5LEDmode.rainbow_1,
			DDR5LEDmode.rainbow_1,
			DDR5LEDmode.rainbow_1,
			DDR5LEDmode.rainbow_1,
			DDR5LEDmode.rainbow_1,
			DDR5LEDmode.rainbow_1,
			DDR5LEDmode.rainbow_1
		};

		// Token: 0x04000104 RID: 260
		public static List<int> _direction = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };

		// Token: 0x04000105 RID: 261
		public static List<int> _ir_delay = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };

		// Token: 0x04000106 RID: 262
		public static List<int> _speed = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };

		// Token: 0x04000107 RID: 263
		public static List<int> _brightness = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };

		// Token: 0x04000108 RID: 264
		public static List<int> _width = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };

		// Token: 0x04000109 RID: 265
		public static List<int> _hue = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };

		// Token: 0x0400010A RID: 266
		public static List<bool> _multicolor = new List<bool> { false, false, false, false, false, false, false, false };

		// Token: 0x0400010B RID: 267
		public static List<int> _number_of_leds = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };

		// Token: 0x0400010C RID: 268
		public static List<int> _number_colors = new List<int> { -1, -1, -1, -1, -1, -1, -1, -1 };

		// Token: 0x0400010D RID: 269
		public static List<bool> _power_saving = new List<bool> { false, false, false, false, false, false, false, false };
	}
}
