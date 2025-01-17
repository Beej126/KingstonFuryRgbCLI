using System;
using System.Collections.Generic;

namespace FuryControllerService
{
	// Token: 0x0200001B RID: 27
	public class DRAMDDR5CtrlObj
	{
		// Token: 0x06000109 RID: 265 RVA: 0x000080E1 File Offset: 0x000062E1
		public DRAMDDR5CtrlObj()
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000080E9 File Offset: 0x000062E9
		public DRAMDDR5CtrlObj(string _mode, string _ctrl_mode)
		{
			this.mode = _mode;
			this.ctrl_mode = _ctrl_mode;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000080FF File Offset: 0x000062FF
		public DRAMDDR5CtrlObj(string _mode, string _ctrl_mode, List<List<int>> _color_table)
		{
			this.mode = _mode;
			this.ctrl_mode = _ctrl_mode;
			this.color_table = _color_table;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000811C File Offset: 0x0000631C
		public DRAMDDR5CtrlObj(string _mode, string _ctrl_mode, List<List<int>> _color_table, List<int> _background_color)
		{
			this.mode = _mode;
			this.ctrl_mode = _ctrl_mode;
			this.color_table = _color_table;
			this.background_color = _background_color;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00008141 File Offset: 0x00006341
		public DRAMDDR5CtrlObj(int _index, string _mode, string _ctrl_mode, List<List<int>> _color_table, List<int> _background_color, List<List<int>> _ctrl_color)
		{
			this.index = _index;
			this.mode = _mode;
			this.ctrl_mode = _ctrl_mode;
			this.color_table = _color_table;
			this.background_color = _background_color;
			this.ctrl_color = _ctrl_color;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00008176 File Offset: 0x00006376
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000817E File Offset: 0x0000637E
		public int index { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00008187 File Offset: 0x00006387
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000818F File Offset: 0x0000638F
		public string mode { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00008198 File Offset: 0x00006398
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000081A0 File Offset: 0x000063A0
		public string ctrl_mode { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000081A9 File Offset: 0x000063A9
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000081B1 File Offset: 0x000063B1
		public bool multicolor { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000081BA File Offset: 0x000063BA
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000081C2 File Offset: 0x000063C2
		public bool reset_default_effect { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000081CB File Offset: 0x000063CB
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000081D3 File Offset: 0x000063D3
		public bool reset_color_table { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000081DC File Offset: 0x000063DC
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000081E4 File Offset: 0x000063E4
		public int direction { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000081ED File Offset: 0x000063ED
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000081F5 File Offset: 0x000063F5
		public int ir_delay { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000081FE File Offset: 0x000063FE
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00008206 File Offset: 0x00006406
		public int speed { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000820F File Offset: 0x0000640F
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00008217 File Offset: 0x00006417
		public int brightness { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00008220 File Offset: 0x00006420
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00008228 File Offset: 0x00006428
		public int width { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00008231 File Offset: 0x00006431
		// (set) Token: 0x06000125 RID: 293 RVA: 0x00008239 File Offset: 0x00006439
		public int hue { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00008242 File Offset: 0x00006442
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000824A File Offset: 0x0000644A
		public int led_number { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00008253 File Offset: 0x00006453
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000825B File Offset: 0x0000645B
		public bool power_saving { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00008264 File Offset: 0x00006464
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000826C File Offset: 0x0000646C
		public int number_colors { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00008275 File Offset: 0x00006475
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000827D File Offset: 0x0000647D
		public List<List<int>> color_table { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00008286 File Offset: 0x00006486
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000828E File Offset: 0x0000648E
		public List<int> background_color { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00008297 File Offset: 0x00006497
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000829F File Offset: 0x0000649F
		public List<List<int>> ctrl_color { get; set; }
	}
}
