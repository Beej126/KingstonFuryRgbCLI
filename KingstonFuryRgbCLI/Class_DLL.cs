using System.Runtime.InteropServices;

namespace KingstonFuryRgbCLI
{
    // Token: 0x02000002 RID: 2
    internal static class Class_DLL
	{
		// Token: 0x06000001 RID: 1
		[DllImport("FuryCTRL.dll")]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool InitialSMBusDriver();

		// Token: 0x06000002 RID: 2
		[DllImport("FuryCTRL.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool Is8Dimm();

		// Token: 0x06000003 RID: 3
		[DllImport("FuryCTRL.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool IsAM4();

		// Token: 0x06000004 RID: 4
		[DllImport("FuryCTRL.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.I1)]
		public static extern bool IsX299();

		// Token: 0x06000005 RID: 5
		[DllImport("FuryCTRL.dll", CharSet = CharSet.Unicode)]
		public static extern bool CheckDRAMSupport(int slot, [MarshalAs(UnmanagedType.BStr)] out string manufac, [MarshalAs(UnmanagedType.BStr)] out string partnum, out bool supportXMP, out byte version);

		// Token: 0x06000006 RID: 6
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterStart();

		// Token: 0x06000007 RID: 7
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSet(int offset, int data);

		// Token: 0x06000008 RID: 8
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetRunningSpeed(int time);

		// Token: 0x06000009 RID: 9
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetBreathSpeed(int up_time, int down_time, int pause_time);

		// Token: 0x0600000A RID: 10
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetRainbowSpeed(int time);

		// Token: 0x0600000B RID: 11
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetMeteorSpeed(int time);

		// Token: 0x0600000C RID: 12
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetBlinkSpeed(int hold_time, int pause_time);

		// Token: 0x0600000D RID: 13
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetBlinkCycleSpeed(int hold_time, int pause_time, int cycle_delay_time);

		// Token: 0x0600000E RID: 14
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetColorCycleSpeed(int hold_time, int change_time);

		// Token: 0x0600000F RID: 15
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetRunningBrightness(int bright);

		// Token: 0x06000010 RID: 16
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetBreathBrightness(int max, int min);

		// Token: 0x06000011 RID: 17
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetRainbowBrightness(int bright);

		// Token: 0x06000012 RID: 18
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetMeteorBrightness(int bright);

		// Token: 0x06000013 RID: 19
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetBlinkBrightness(int bright);

		// Token: 0x06000014 RID: 20
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetColorCycleBrightness(int bright);

		// Token: 0x06000015 RID: 21
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterSetStaticBrightness(int bright);

		// Token: 0x06000016 RID: 22
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterDone();

		// Token: 0x06000017 RID: 23
		[DllImport("FuryCTRL.dll")]
		public static extern bool ParameterRun();

		// Token: 0x06000018 RID: 24
		[DllImport("FuryCTRL.dll")]
		public static extern bool ReleaseDll();

		// Token: 0x06000019 RID: 25
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5CmdStart(byte slaveAddr);

		// Token: 0x0600001A RID: 26
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5CmdDone(byte slaveAddr);

		// Token: 0x0600001B RID: 27
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5CmdBypass(byte slaveAddr);

		// Token: 0x0600001C RID: 28
		[DllImport("FuryCTRL.dll")]
		public static extern bool GetKingstonFuryDDR5CmdValue(byte slaveAddr, out byte data);

		// Token: 0x0600001D RID: 29
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5Init(byte slaveAddr);

		// Token: 0x0600001E RID: 30
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5Style(byte slaveAddr, byte style);

		// Token: 0x0600001F RID: 31
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5MasterSlaveRole(byte slaveAddr, byte role);

		// Token: 0x06000020 RID: 32
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5Direction(byte slaveAddr, byte direction);

		// Token: 0x06000021 RID: 33
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5IRDelay(byte slaveAddr, byte irDelay);

		// Token: 0x06000022 RID: 34
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5Speed(byte slaveAddr, byte speed);

		// Token: 0x06000023 RID: 35
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5Width(byte slaveAddr, byte width);

		// Token: 0x06000024 RID: 36
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5Hue(byte slaveAddr, byte hue);

		// Token: 0x06000025 RID: 37
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5HyperSpeed(byte slaveAddr, byte hyperspeed);

		// Token: 0x06000026 RID: 38
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5ColorHoldTime(byte slaveAddr, int time);

		// Token: 0x06000027 RID: 39
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5ColorChangeTime(byte slaveAddr, int time);

		// Token: 0x06000028 RID: 40
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5BreathModeParameter(byte slaveAddr, byte upMidTime, byte upMaxTime, byte downMidTime, byte downMinTime, byte downHoldTime, byte maxBrightness, byte midBrightness, byte minBrightness);

		// Token: 0x06000029 RID: 41
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5Brightness(byte slaveAddr, byte brightness);

		// Token: 0x0600002A RID: 42
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5MaxMinBrightness(byte slaveAddr, byte maxBrightness, byte minBrightness);

		// Token: 0x0600002B RID: 43
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5BackgroundColor(byte slaveAddr, byte r, byte g, byte b);

		// Token: 0x0600002C RID: 44
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5NumLED(byte slaveAddr, byte num);

		// Token: 0x0600002D RID: 45
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5NumDimm(byte slaveAddr, byte num);

		// Token: 0x0600002E RID: 46
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5SelectColor(byte slaveAddr, byte num);

		// Token: 0x0600002F RID: 47
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5ColorIndex(byte slaveAddr, byte index, byte r, byte g, byte b);

		// Token: 0x06000030 RID: 48
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5LedIndex(byte slaveAddr, byte index, byte r, byte g, byte b);

		// Token: 0x06000031 RID: 49
		[DllImport("FuryCTRL.dll", CharSet = CharSet.Ansi)]
		public static extern bool GetKingstonFuryDDR5XmpProfile(byte slaveAddr, ref Class_DLL.IntelXMP xmp);

		// Token: 0x06000032 RID: 50
		[DllImport("FuryCTRL.dll")]
		public static extern bool GetKingstonFuryDDR5VolSetting(byte slaveAddr, ref int vdd, ref int vddq, ref int vpp);

		// Token: 0x06000033 RID: 51
		[DllImport("FuryCTRL.dll")]
		public static extern bool GetKingstonFuryDDR5Temperature(byte slaveAddr, ref int temp);

		// Token: 0x06000034 RID: 52
		[DllImport("FuryCTRL.dll")]
		public static extern bool GetKingstonFuryDDR5MasterSlaveRole(byte slaveAddr, out byte data);

		// Token: 0x06000035 RID: 53
		[DllImport("FuryCTRL.dll")]
		public static extern bool GetKingstonFuryDDR5Speed(byte slaveAddr, out byte speed);

		// Token: 0x06000036 RID: 54
		[DllImport("FuryCTRL.dll")]
		public static extern bool GetKingstonFuryDDR5ModuleID(byte slaveAddr, out int id);

		// Token: 0x06000037 RID: 55
		[DllImport("FuryCTRL.dll")]
		public static extern bool GetKingstonFuryDDR5Style(byte slaveAddr, out byte effect);

		// Token: 0x06000038 RID: 56
		[DllImport("FuryCTRL.dll")]
		public static extern bool SetKingstonFuryDDR5IRTransConfig(byte slaveAddr, byte num);

		// Token: 0x06000039 RID: 57
		[DllImport("FuryCTRL.dll")]
		public static extern bool GetKingstonFuryDDR5IRTransConfig(byte slaveAddr, out byte data);

		// Token: 0x04000001 RID: 1
		internal const string DLL_FileName = "FuryCTRL.dll";

		// Token: 0x02000042 RID: 66
		public struct XMPData
		{
			// Token: 0x04000168 RID: 360
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] ProfileName;

			// Token: 0x04000169 RID: 361
			public int Frequency;

			// Token: 0x0400016A RID: 362
			public int VDD;

			// Token: 0x0400016B RID: 363
			public int VDDQ;

			// Token: 0x0400016C RID: 364
			public int VPP;

			// Token: 0x0400016D RID: 365
			public int tCL;

			// Token: 0x0400016E RID: 366
			public int tRCD;

			// Token: 0x0400016F RID: 367
			public int tRP;

			// Token: 0x04000170 RID: 368
			public int tRAS;

			// Token: 0x04000171 RID: 369
			public int tRC;

			// Token: 0x04000172 RID: 370
			public bool Enable;
		}

		// Token: 0x02000043 RID: 67
		public struct IntelXMP
		{
			// Token: 0x04000173 RID: 371
			public byte Version;

			// Token: 0x04000174 RID: 372
			public Class_DLL.XMPData ProfileData1;

			// Token: 0x04000175 RID: 373
			public Class_DLL.XMPData ProfileData2;

			// Token: 0x04000176 RID: 374
			public Class_DLL.XMPData ProfileData3;
		}
	}
}
