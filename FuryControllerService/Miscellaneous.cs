using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FuryControllerService
{
	// Token: 0x0200002B RID: 43
	public class Miscellaneous
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0000B217 File Offset: 0x00009417
		public static void getBaseAddress(int iDDRType)
		{
			if (iDDRType == 1)
			{
				Miscellaneous.iDDRBaseAddress = 88;
				return;
			}
			if (iDDRType == 2)
			{
				Miscellaneous.iDDRBaseAddress = 96;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B230 File Offset: 0x00009430
		public static int PercentageToValueRange(int _percentage, int _default = 1025, double min = 50.0, double max = 2000.0)
		{
			_percentage = 100 - _percentage;
			if (_percentage > 0 && _percentage < 100)
			{
				return (int)((max - min) / 100.0 * (double)_percentage + min);
			}
			if (_percentage == 0)
			{
				return (int)min;
			}
			if (_percentage == 100)
			{
				return (int)max;
			}
			return _default;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B264 File Offset: 0x00009464
		public static int PercentageToValueRangeSmoothVersion(int _percentage, int _default = 1025, double min = 50.0, double max = 2000.0)
		{
			if (_percentage > 0 && _percentage < 100)
			{
				return (int)(-423.0 * Math.Log((double)_percentage) + max);
			}
			if (_percentage == 0)
			{
				return (int)max;
			}
			if (_percentage == 100)
			{
				return (int)min;
			}
			return _default;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B293 File Offset: 0x00009493
		public static int CheckValueRange(int _value, int _default = 50, int min = 0, int max = 100)
		{
			if (_value >= min && _value <= max)
			{
				return _value;
			}
			return _default;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B2A0 File Offset: 0x000094A0
		public static int CheckValueHue(int _value, int _default = 0)
		{
			if (_value == 0)
			{
				return _value;
			}
			if (_value == 20)
			{
				return _value;
			}
			if (_value == 40)
			{
				return _value;
			}
			if (_value == 60)
			{
				return _value;
			}
			if (_value == 80)
			{
				return _value;
			}
			return _default;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public static void setMultiBrightness(DRAMCmdObj _obj, int _value)
		{
			try
			{
				if (_obj.ctrl_settings.ctrl_color != null && _obj.ctrl_settings.ctrl_color.Count > 0)
				{
					if (!Miscellaneous.bIs8Dimm)
					{
						for (int i = 0; i < _obj.ctrl_settings.ctrl_color.Count; i++)
						{
							if (_obj.ctrl_settings.ctrl_color.ElementAt(i).Value != null && _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.index < 4)
							{
								int index = _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.index;
								Thread.Sleep(10);
								Class_DLL.ParameterSet(33 + 48 * index, _value);
								Class_DLL.ParameterSet(36 + 48 * index, _value);
								Class_DLL.ParameterSet(39 + 48 * index, _value);
								Class_DLL.ParameterSet(42 + 48 * index, _value);
								Class_DLL.ParameterSet(45 + 48 * index, _value);
							}
							else
							{
								Log.LibCmdLogWriter(string.Format("Multi-Brightness : Slot count over range..", Array.Empty<object>()), true);
							}
						}
					}
				}
				else
				{
					Log.LibCmdLogWriter(string.Format("Multi-Brightness : brightness empty.", Array.Empty<object>()), true);
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Multi-Brightness : {0}", ex.Message), true);
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B434 File Offset: 0x00009634
		public static void setMultiBrightnessForBreath(DRAMCmdObj _obj, int _maxValue, int _minValue)
		{
			try
			{
				if (_obj.ctrl_settings.ctrl_color != null && _obj.ctrl_settings.ctrl_color.Count > 0)
				{
					if (!Miscellaneous.bIs8Dimm)
					{
						for (int i = 0; i < _obj.ctrl_settings.ctrl_color.Count; i++)
						{
							if (_obj.ctrl_settings.ctrl_color.ElementAt(i).Value != null && _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.index < 4)
							{
								int index = _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.index;
								Thread.Sleep(10);
								Class_DLL.ParameterSet(34 + 48 * index, _maxValue);
								Class_DLL.ParameterSet(35 + 48 * index, _minValue);
								Class_DLL.ParameterSet(37 + 48 * index, _maxValue);
								Class_DLL.ParameterSet(38 + 48 * index, _minValue);
								Class_DLL.ParameterSet(40 + 48 * index, _maxValue);
								Class_DLL.ParameterSet(41 + 48 * index, _minValue);
								Class_DLL.ParameterSet(43 + 48 * index, _maxValue);
								Class_DLL.ParameterSet(44 + 48 * index, _minValue);
								Class_DLL.ParameterSet(46 + 48 * index, _maxValue);
								Class_DLL.ParameterSet(47 + 48 * index, _minValue);
							}
							else
							{
								Log.LibCmdLogWriter(string.Format("Multi-Brightness (Breath) : Slot count over range..", Array.Empty<object>()), true);
							}
						}
					}
				}
				else
				{
					Log.LibCmdLogWriter(string.Format("Multi-Brightness (Breath) : brightness empty.", Array.Empty<object>()), true);
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Multi-Brightness (Breath) : {0}", ex.Message), true);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B5F0 File Offset: 0x000097F0
		public static void setCtrlColor(DRAMCmdObj _obj)
		{
			try
			{
				if (_obj.ctrl_settings.ctrl_color != null && _obj.ctrl_settings.ctrl_color.Count > 0 && _obj.ctrl_settings.ctrl_color.ElementAt(0).Value != null && _obj.ctrl_settings.ctrl_color.ElementAt(0).Value.colors != null && _obj.ctrl_settings.ctrl_color.ElementAt(0).Value.colors.Count > 0)
				{
					List<int> list = _obj.ctrl_settings.ctrl_color.ElementAt(0).Value.colors.ElementAt(0);
					if (list.Count == 3)
					{
						Thread.Sleep(10);
						Class_DLL.ParameterSet(236, list[0]);
						Class_DLL.ParameterSet(237, list[1]);
						Class_DLL.ParameterSet(238, list[2]);
					}
				}
				else
				{
					Log.LibCmdLogWriter(string.Format("One-Color : color empty.", Array.Empty<object>()), true);
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("One-Color : {0}", ex.Message), true);
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B744 File Offset: 0x00009944
		public static void setMultiColor(DRAMCmdObj _obj)
		{
			try
			{
				if (_obj.ctrl_settings.ctrl_color != null && _obj.ctrl_settings.ctrl_color.Count > 0)
				{
					if (!Miscellaneous.bIs8Dimm)
					{
						for (int i = 0; i < _obj.ctrl_settings.ctrl_color.Count; i++)
						{
							if (_obj.ctrl_settings.ctrl_color.ElementAt(i).Value != null && _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.index < 4)
							{
								int index = _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.index;
								if (_obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.Count > 0 && _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.ElementAt(0).Count == 3)
								{
									List<int> list = _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.ElementAt(0);
									Thread.Sleep(10);
									Class_DLL.ParameterSet(17 + 48 * index, list[0]);
									Class_DLL.ParameterSet(18 + 48 * index, list[1]);
									Class_DLL.ParameterSet(19 + 48 * index, list[2]);
									if (_obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.Count > 1 && _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.ElementAt(1).Count == 3)
									{
										List<int> list2 = _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.ElementAt(1);
										Thread.Sleep(10);
										Class_DLL.ParameterSet(20 + 48 * index, list2[0]);
										Class_DLL.ParameterSet(21 + 48 * index, list2[1]);
										Class_DLL.ParameterSet(22 + 48 * index, list2[2]);
										if (_obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.Count > 2 && _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.ElementAt(2).Count == 3)
										{
											List<int> list3 = _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.ElementAt(2);
											Thread.Sleep(10);
											Class_DLL.ParameterSet(23 + 48 * index, list3[0]);
											Class_DLL.ParameterSet(24 + 48 * index, list3[1]);
											Class_DLL.ParameterSet(25 + 48 * index, list3[2]);
											if (_obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.Count > 3 && _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.ElementAt(3).Count == 3)
											{
												List<int> list4 = _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.ElementAt(3);
												Thread.Sleep(10);
												Class_DLL.ParameterSet(26 + 48 * index, list4[0]);
												Class_DLL.ParameterSet(27 + 48 * index, list4[1]);
												Class_DLL.ParameterSet(28 + 48 * index, list4[2]);
												if (_obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.Count > 4 && _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.ElementAt(4).Count == 3)
												{
													List<int> list5 = _obj.ctrl_settings.ctrl_color.ElementAt(i).Value.colors.ElementAt(4);
													Thread.Sleep(10);
													Class_DLL.ParameterSet(29 + 48 * index, list5[0]);
													Class_DLL.ParameterSet(30 + 48 * index, list5[1]);
													Class_DLL.ParameterSet(31 + 48 * index, list5[2]);
												}
												else
												{
													Log.LibCmdLogWriter(string.Format("Multi-Color : color count mismatch.", Array.Empty<object>()), true);
												}
											}
											else
											{
												Log.LibCmdLogWriter(string.Format("Multi-Color : color count mismatch.", Array.Empty<object>()), true);
											}
										}
										else
										{
											Log.LibCmdLogWriter(string.Format("Multi-Color : color count mismatch.", Array.Empty<object>()), true);
										}
									}
									else
									{
										Log.LibCmdLogWriter(string.Format("Multi-Color : color count mismatch.", Array.Empty<object>()), true);
									}
								}
								else
								{
									Log.LibCmdLogWriter(string.Format("Multi-Color : color count mismatch.", Array.Empty<object>()), true);
								}
							}
							else
							{
								Log.LibCmdLogWriter(string.Format("Multi-Color : Slot count over range.", Array.Empty<object>()), true);
							}
						}
					}
				}
				else
				{
					Log.LibCmdLogWriter(string.Format("Multi-Color : color empty.", Array.Empty<object>()), true);
				}
			}
			catch (Exception ex)
			{
				Log.LibCmdLogWriter(string.Format("Multi-Color : {0}", ex.Message), true);
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		public static void resetDDR5ColorsTable(int index)
		{
			byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[index]);
			for (int i = 0; i < Miscellaneous.ddr5_color_table_default.Count; i++)
			{
				if (Miscellaneous.ddr5_color_table_default.ContainsKey(index.ToString()) && Miscellaneous.ddr5_color_table_default[index.ToString()][i].Count >= 3)
				{
					Class_DLL.SetKingstonFuryDDR5ColorIndex(b, (byte)i, (byte)Miscellaneous.ddr5_color_table_default[index.ToString()][i][0], (byte)Miscellaneous.ddr5_color_table_default[index.ToString()][i][1], (byte)Miscellaneous.ddr5_color_table_default[index.ToString()][i][2]);
				}
			}
			if (Miscellaneous.ddr5_bg_color_default.ContainsKey(index.ToString()) && Miscellaneous.ddr5_bg_color_default[index.ToString()].Count >= 3)
			{
				Class_DLL.SetKingstonFuryDDR5BackgroundColor(b, (byte)Miscellaneous.ddr5_bg_color_default[index.ToString()][0], (byte)Miscellaneous.ddr5_bg_color_default[index.ToString()][1], (byte)Miscellaneous.ddr5_bg_color_default[index.ToString()][2]);
			}
			if (Miscellaneous.ddr5_color_table_default.ContainsKey(index.ToString()) && Miscellaneous.ddr5_color_table_cache.ContainsKey(index.ToString()))
			{
				for (int j = 0; j < Miscellaneous.ddr5_color_table_default[index.ToString()].Count<List<int>>(); j++)
				{
					Miscellaneous.ddr5_color_table_cache[index.ToString()][j][0] = Miscellaneous.ddr5_color_table_default[index.ToString()][j][0];
					Miscellaneous.ddr5_color_table_cache[index.ToString()][j][1] = Miscellaneous.ddr5_color_table_default[index.ToString()][j][1];
					Miscellaneous.ddr5_color_table_cache[index.ToString()][j][2] = Miscellaneous.ddr5_color_table_default[index.ToString()][j][2];
					Miscellaneous.ddr5_color_table_cache_out["0"][j][0] = Miscellaneous.ddr5_color_table_default[index.ToString()][j][0];
					Miscellaneous.ddr5_color_table_cache_out["0"][j][1] = Miscellaneous.ddr5_color_table_default[index.ToString()][j][1];
					Miscellaneous.ddr5_color_table_cache_out["0"][j][2] = Miscellaneous.ddr5_color_table_default[index.ToString()][j][2];
				}
			}
			if (Miscellaneous.ddr5_bg_color_default.ContainsKey(index.ToString()) && Miscellaneous.ddr5_bg_color_cache.ContainsKey(index.ToString()))
			{
				Miscellaneous.ddr5_bg_color_cache[index.ToString()][0] = Miscellaneous.ddr5_bg_color_default[index.ToString()][0];
				Miscellaneous.ddr5_bg_color_cache[index.ToString()][1] = Miscellaneous.ddr5_bg_color_default[index.ToString()][1];
				Miscellaneous.ddr5_bg_color_cache[index.ToString()][2] = Miscellaneous.ddr5_bg_color_default[index.ToString()][2];
				Miscellaneous.ddr5_bg_color_cache_out["0"][0] = Miscellaneous.ddr5_bg_color_default[index.ToString()][0];
				Miscellaneous.ddr5_bg_color_cache_out["0"][1] = Miscellaneous.ddr5_bg_color_default[index.ToString()][1];
				Miscellaneous.ddr5_bg_color_cache_out["0"][2] = Miscellaneous.ddr5_bg_color_default[index.ToString()][2];
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		public static void setDDR5BackgroundColor(int index, List<int> ddr5_bg_color)
		{
			byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[index]);
			if (ddr5_bg_color != null && ddr5_bg_color.Count >= 3 && Miscellaneous.ddr5_bg_color_cache != null && Miscellaneous.ddr5_bg_color_cache.Count > index && Miscellaneous.ddr5_bg_color_cache.ContainsKey(index.ToString()) && Miscellaneous.ddr5_bg_color_cache[index.ToString()].Count >= 3 && Miscellaneous.ddr5_bg_color_cache_out["0"].Count >= 3 && (ddr5_bg_color[0] != Miscellaneous.ddr5_bg_color_cache[index.ToString()][0] || ddr5_bg_color[1] != Miscellaneous.ddr5_bg_color_cache[index.ToString()][1] || ddr5_bg_color[2] != Miscellaneous.ddr5_bg_color_cache[index.ToString()][2]))
			{
				int num;
				int num2;
				int num3;
				if (ddr5_bg_color[0] > 26 || ddr5_bg_color[1] > 26 || ddr5_bg_color[2] > 26)
				{
					num = (int)((double)ddr5_bg_color[0] / 10.0);
					num2 = (int)((double)ddr5_bg_color[1] / 10.0);
					num3 = (int)((double)ddr5_bg_color[2] / 10.0);
				}
				else
				{
					num = ddr5_bg_color[0];
					num2 = ddr5_bg_color[1];
					num3 = ddr5_bg_color[2];
				}
				Class_DLL.SetKingstonFuryDDR5BackgroundColor(b, (byte)num, (byte)num2, (byte)num3);
				Miscellaneous.ddr5_bg_color_cache[index.ToString()][0] = ddr5_bg_color[0];
				Miscellaneous.ddr5_bg_color_cache[index.ToString()][1] = ddr5_bg_color[1];
				Miscellaneous.ddr5_bg_color_cache[index.ToString()][2] = ddr5_bg_color[2];
				Miscellaneous.ddr5_bg_color_cache_out["0"][0] = ddr5_bg_color[0];
				Miscellaneous.ddr5_bg_color_cache_out["0"][1] = ddr5_bg_color[1];
				Miscellaneous.ddr5_bg_color_cache_out["0"][2] = ddr5_bg_color[2];
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000C318 File Offset: 0x0000A518
		public static void setDDR5ColorsTable(int index, int number_colors, List<List<int>> ddr5_color_table)
		{
			byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[index]);
			for (int i = 0; i < ddr5_color_table.Count; i++)
			{
				if (number_colors > i && ddr5_color_table[i].Count >= 3 && Miscellaneous.ddr5_color_table_cache.ContainsKey(index.ToString()) && Miscellaneous.ddr5_color_table_cache[index.ToString()].Count > i && Miscellaneous.ddr5_color_table_cache_out["0"].Count > i && (ddr5_color_table[i][0] != Miscellaneous.ddr5_color_table_cache[index.ToString()][i][0] || ddr5_color_table[i][1] != Miscellaneous.ddr5_color_table_cache[index.ToString()][i][1] || ddr5_color_table[i][2] != Miscellaneous.ddr5_color_table_cache[index.ToString()][i][2]))
				{
					Thread.Sleep(10);
					Class_DLL.SetKingstonFuryDDR5ColorIndex(b, (byte)i, (byte)ddr5_color_table[i][0], (byte)ddr5_color_table[i][1], (byte)ddr5_color_table[i][2]);
					Miscellaneous.ddr5_color_table_cache[index.ToString()][i][0] = ddr5_color_table[i][0];
					Miscellaneous.ddr5_color_table_cache[index.ToString()][i][1] = ddr5_color_table[i][1];
					Miscellaneous.ddr5_color_table_cache[index.ToString()][i][2] = ddr5_color_table[i][2];
					Miscellaneous.ddr5_color_table_cache_out["0"][i][0] = ddr5_color_table[i][0];
					Miscellaneous.ddr5_color_table_cache_out["0"][i][1] = ddr5_color_table[i][1];
					Miscellaneous.ddr5_color_table_cache_out["0"][i][2] = ddr5_color_table[i][2];
				}
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000C56C File Offset: 0x0000A76C
		public static void ResumeDDR5ColorsTable(int index)
		{
			byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[index]);
			if (Miscellaneous.ddr5_color_table_cache != null && Miscellaneous.ddr5_color_table_cache.Count > index && Miscellaneous.ddr5_color_table_cache.ContainsKey(index.ToString()))
			{
				for (int i = 0; i < Miscellaneous.ddr5_color_table_cache[index.ToString()].Count; i++)
				{
					if (i < 10 && Miscellaneous.ddr5_color_table_cache[index.ToString()][i].Count >= 3 && Miscellaneous.ddr5_color_table_default.ContainsKey(index.ToString()) && Miscellaneous.ddr5_color_table_default[index.ToString()].Count > i && (Miscellaneous.ddr5_color_table_default[index.ToString()][i][0] != Miscellaneous.ddr5_color_table_cache[index.ToString()][i][0] || Miscellaneous.ddr5_color_table_default[index.ToString()][i][1] != Miscellaneous.ddr5_color_table_cache[index.ToString()][i][1] || Miscellaneous.ddr5_color_table_default[index.ToString()][i][2] != Miscellaneous.ddr5_color_table_cache[index.ToString()][i][2]))
					{
						Thread.Sleep(10);
						Class_DLL.SetKingstonFuryDDR5ColorIndex(b, (byte)i, (byte)Miscellaneous.ddr5_color_table_cache[index.ToString()][i][0], (byte)Miscellaneous.ddr5_color_table_cache[index.ToString()][i][1], (byte)Miscellaneous.ddr5_color_table_cache[index.ToString()][i][2]);
						Miscellaneous.ddr5_color_table_cache_out["0"][i][0] = Miscellaneous.ddr5_color_table_cache[index.ToString()][i][0];
						Miscellaneous.ddr5_color_table_cache_out["0"][i][1] = Miscellaneous.ddr5_color_table_cache[index.ToString()][i][1];
						Miscellaneous.ddr5_color_table_cache_out["0"][i][2] = Miscellaneous.ddr5_color_table_cache[index.ToString()][i][2];
					}
				}
			}
			if (Miscellaneous.ddr5_bg_color_cache != null && Miscellaneous.ddr5_bg_color_cache.Count > index && Miscellaneous.ddr5_bg_color_cache.ContainsKey(index.ToString()) && Miscellaneous.ddr5_bg_color_default.ContainsKey(index.ToString()) && Miscellaneous.ddr5_bg_color_cache[index.ToString()].Count >= 3 && (Miscellaneous.ddr5_bg_color_default[index.ToString()][0] != Miscellaneous.ddr5_bg_color_cache[index.ToString()][0] || Miscellaneous.ddr5_bg_color_default[index.ToString()][1] != Miscellaneous.ddr5_bg_color_cache[index.ToString()][1] || Miscellaneous.ddr5_bg_color_default[index.ToString()][2] != Miscellaneous.ddr5_bg_color_cache[index.ToString()][2]))
			{
				int num;
				int num2;
				int num3;
				if (Miscellaneous.ddr5_bg_color_cache[index.ToString()][0] > 26 || Miscellaneous.ddr5_bg_color_cache[index.ToString()][1] > 26 || Miscellaneous.ddr5_bg_color_cache[index.ToString()][2] > 26)
				{
					num = (int)((double)Miscellaneous.ddr5_bg_color_cache[index.ToString()][0] / 10.0);
					num2 = (int)((double)Miscellaneous.ddr5_bg_color_cache[index.ToString()][1] / 10.0);
					num3 = (int)((double)Miscellaneous.ddr5_bg_color_cache[index.ToString()][2] / 10.0);
				}
				else
				{
					num = Miscellaneous.ddr5_bg_color_cache[index.ToString()][0];
					num2 = Miscellaneous.ddr5_bg_color_cache[index.ToString()][1];
					num3 = Miscellaneous.ddr5_bg_color_cache[index.ToString()][2];
				}
				Class_DLL.SetKingstonFuryDDR5BackgroundColor(b, (byte)num, (byte)num2, (byte)num3);
				Miscellaneous.ddr5_bg_color_cache_out["0"][0] = Miscellaneous.ddr5_bg_color_cache[index.ToString()][0];
				Miscellaneous.ddr5_bg_color_cache_out["0"][1] = Miscellaneous.ddr5_bg_color_cache[index.ToString()][1];
				Miscellaneous.ddr5_bg_color_cache_out["0"][2] = Miscellaneous.ddr5_bg_color_cache[index.ToString()][2];
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		public static void SaveDDR5ColorCache()
		{
			try
			{
				string text = JsonClass.JavaScriptSerialize<Dictionary<string, List<int>>>(Miscellaneous.ddr5_bg_color_cache);
				if (!string.IsNullOrEmpty(text))
				{
					Log.WriteLEDBGColorCache(text);
				}
			}
			catch (Exception)
			{
			}
			try
			{
				string text2 = JsonClass.JavaScriptSerialize<Dictionary<string, List<List<int>>>>(Miscellaneous.ddr5_color_table_cache);
				if (!string.IsNullOrEmpty(text2))
				{
					Log.WriteLEDColorTableCache(text2);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000CB08 File Offset: 0x0000AD08
		public static void setDDR5Brightness(byte bAddress, int _brightness, bool bPowerSaving)
		{
			if (bPowerSaving)
			{
				Class_DLL.SetKingstonFuryDDR5Brightness(bAddress, 10);
				return;
			}
			Class_DLL.SetKingstonFuryDDR5Brightness(bAddress, (byte)_brightness);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000CB20 File Offset: 0x0000AD20
		public static void setDDR5CtrlColors(int index, List<List<int>> ddr5_ctrl_color, bool bResume)
		{
			byte b = (byte)(Miscellaneous.iDDRBaseAddress + KSDRAMSMBusCtrlObj.bDRAMAddressOffset_v2[index]);
			for (int i = 0; i < ddr5_ctrl_color.Count; i++)
			{
				if (ddr5_ctrl_color[i].Count >= 3 && Miscellaneous.ddr5_ctrl_color_cache.ContainsKey(index.ToString()) && Miscellaneous.ddr5_ctrl_color_cache[index.ToString()].Count > i && (bResume || ddr5_ctrl_color[i][0] != Miscellaneous.ddr5_ctrl_color_cache[index.ToString()][i][0] || ddr5_ctrl_color[i][1] != Miscellaneous.ddr5_ctrl_color_cache[index.ToString()][i][1] || ddr5_ctrl_color[i][2] != Miscellaneous.ddr5_ctrl_color_cache[index.ToString()][i][2]))
				{
					Miscellaneous.ddr5_ctrl_color_cache[index.ToString()][i][0] = ddr5_ctrl_color[i][0];
					Miscellaneous.ddr5_ctrl_color_cache[index.ToString()][i][1] = ddr5_ctrl_color[i][1];
					Miscellaneous.ddr5_ctrl_color_cache[index.ToString()][i][2] = ddr5_ctrl_color[i][2];
					if (Miscellaneous.iDDRBaseAddress > 88 && Miscellaneous.iDDRBaseAddress < 95 && i >= 10)
					{
						return;
					}
					Class_DLL.SetKingstonFuryDDR5LedIndex(b, (byte)i, (byte)ddr5_ctrl_color[i][0], (byte)ddr5_ctrl_color[i][1], (byte)ddr5_ctrl_color[i][2]);
				}
			}
		}

		// Token: 0x04000121 RID: 289
		public static int iDDRBaseAddress = 0;

		// Token: 0x04000122 RID: 290
		public static bool bIs8Dimm = false;

		// Token: 0x04000123 RID: 291
		public static readonly Dictionary<string, List<List<int>>> ddr5_color_table_default = new Dictionary<string, List<List<int>>>
		{
			{
				"0",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"1",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"2",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"3",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"4",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"5",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"6",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"7",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			}
		};

		// Token: 0x04000124 RID: 292
		public static Dictionary<string, List<List<int>>> ddr5_color_table_cache = new Dictionary<string, List<List<int>>>
		{
			{
				"0",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"1",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"2",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"3",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"4",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"5",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"6",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			},
			{
				"7",
				new List<List<int>>
				{
					new List<int> { 255, 0, 0 },
					new List<int> { 0, 255, 0 },
					new List<int> { 255, 100, 0 },
					new List<int> { 0, 0, 255 },
					new List<int> { 239, 239, 0 },
					new List<int> { 128, 0, 128 },
					new List<int> { 0, 109, 119 },
					new List<int> { 255, 200, 0 },
					new List<int> { 255, 85, 255 },
					new List<int> { 60, 125, 255 }
				}
			}
		};

		// Token: 0x04000125 RID: 293
		public static Dictionary<string, List<List<int>>> ddr5_color_table_cache_out = new Dictionary<string, List<List<int>>> { 
		{
			"0",
			new List<List<int>>
			{
				new List<int> { 255, 0, 0 },
				new List<int> { 0, 255, 0 },
				new List<int> { 255, 100, 0 },
				new List<int> { 0, 0, 255 },
				new List<int> { 239, 239, 0 },
				new List<int> { 128, 0, 128 },
				new List<int> { 0, 109, 119 },
				new List<int> { 255, 200, 0 },
				new List<int> { 255, 85, 255 },
				new List<int> { 60, 125, 255 }
			}
		} };

		// Token: 0x04000126 RID: 294
		public static readonly Dictionary<string, List<int>> ddr5_bg_color_default = new Dictionary<string, List<int>>
		{
			{
				"0",
				new List<int> { 16, 16, 16 }
			},
			{
				"1",
				new List<int> { 16, 16, 16 }
			},
			{
				"2",
				new List<int> { 16, 16, 16 }
			},
			{
				"3",
				new List<int> { 16, 16, 16 }
			},
			{
				"4",
				new List<int> { 16, 16, 16 }
			},
			{
				"5",
				new List<int> { 16, 16, 16 }
			},
			{
				"6",
				new List<int> { 16, 16, 16 }
			},
			{
				"7",
				new List<int> { 16, 16, 16 }
			}
		};

		// Token: 0x04000127 RID: 295
		public static Dictionary<string, List<int>> ddr5_bg_color_cache = new Dictionary<string, List<int>>
		{
			{
				"0",
				new List<int> { 16, 16, 16 }
			},
			{
				"1",
				new List<int> { 16, 16, 16 }
			},
			{
				"2",
				new List<int> { 16, 16, 16 }
			},
			{
				"3",
				new List<int> { 16, 16, 16 }
			},
			{
				"4",
				new List<int> { 16, 16, 16 }
			},
			{
				"5",
				new List<int> { 16, 16, 16 }
			},
			{
				"6",
				new List<int> { 16, 16, 16 }
			},
			{
				"7",
				new List<int> { 16, 16, 16 }
			}
		};

		// Token: 0x04000128 RID: 296
		public static Dictionary<string, List<int>> ddr5_bg_color_cache_out = new Dictionary<string, List<int>> { 
		{
			"0",
			new List<int> { 16, 16, 16 }
		} };

		// Token: 0x04000129 RID: 297
		public static Dictionary<string, List<List<int>>> ddr5_ctrl_color_cache = new Dictionary<string, List<List<int>>>
		{
			{
				"0",
				new List<List<int>>
				{
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 }
				}
			},
			{
				"1",
				new List<List<int>>
				{
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 }
				}
			},
			{
				"2",
				new List<List<int>>
				{
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 }
				}
			},
			{
				"3",
				new List<List<int>>
				{
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 }
				}
			},
			{
				"4",
				new List<List<int>>
				{
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 }
				}
			},
			{
				"5",
				new List<List<int>>
				{
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 }
				}
			},
			{
				"6",
				new List<List<int>>
				{
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 }
				}
			},
			{
				"7",
				new List<List<int>>
				{
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 },
					new List<int> { 0, 0, 0 }
				}
			}
		};
	}
}
