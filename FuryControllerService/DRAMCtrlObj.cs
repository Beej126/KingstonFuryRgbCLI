using System;
using System.Collections.Generic;

namespace FuryControllerService
{
	// Token: 0x0200001A RID: 26
	public class DRAMCtrlObj
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00007FC5 File Offset: 0x000061C5
		public DRAMCtrlObj()
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007FCD File Offset: 0x000061CD
		public DRAMCtrlObj(string _mode, string _ctrl_mode)
		{
			this.mode = _mode;
			this.ctrl_mode = _ctrl_mode;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007FE3 File Offset: 0x000061E3
		public DRAMCtrlObj(string _mode, string _ctrl_mode, int _speed, int _brightness)
		{
			this.mode = _mode;
			this.ctrl_mode = _ctrl_mode;
			this.speed = _speed;
			this.brightness = _brightness;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008008 File Offset: 0x00006208
		public DRAMCtrlObj(string _mode, string _ctrl_mode, int _speed, int _brightness, int _darkness)
		{
			this.mode = _mode;
			this.ctrl_mode = _ctrl_mode;
			this.speed = _speed;
			this.brightness = _brightness;
			this.darkness = _darkness;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008035 File Offset: 0x00006235
		public DRAMCtrlObj(string _mode, string _ctrl_mode, int _speed, int _brightness, int _darkness, Dictionary<string, DRAMCtrlColorObj> _ctrl_color)
		{
			this.mode = _mode;
			this.ctrl_mode = _ctrl_mode;
			this.speed = _speed;
			this.brightness = _brightness;
			this.darkness = _darkness;
			this.ctrl_color = _ctrl_color;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000806A File Offset: 0x0000626A
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00008072 File Offset: 0x00006272
		public string mode { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000807B File Offset: 0x0000627B
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00008083 File Offset: 0x00006283
		public string ctrl_mode { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000808C File Offset: 0x0000628C
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00008094 File Offset: 0x00006294
		public string other_option { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000809D File Offset: 0x0000629D
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000080A5 File Offset: 0x000062A5
		public int speed { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000080AE File Offset: 0x000062AE
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000080B6 File Offset: 0x000062B6
		public int brightness { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000080BF File Offset: 0x000062BF
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000080C7 File Offset: 0x000062C7
		public int darkness { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000080D0 File Offset: 0x000062D0
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000080D8 File Offset: 0x000062D8
		public Dictionary<string, DRAMCtrlColorObj> ctrl_color { get; set; }
	}
}
