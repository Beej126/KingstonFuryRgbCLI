using System;

namespace FuryControllerService
{
	// Token: 0x02000019 RID: 25
	public class DRAMTimingsObj
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00007F02 File Offset: 0x00006102
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00007F0A File Offset: 0x0000610A
		public bool enable { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00007F13 File Offset: 0x00006113
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00007F1B File Offset: 0x0000611B
		public string name { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00007F24 File Offset: 0x00006124
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00007F2C File Offset: 0x0000612C
		public int frequency { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00007F35 File Offset: 0x00006135
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00007F3D File Offset: 0x0000613D
		public int vdd { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00007F46 File Offset: 0x00006146
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00007F4E File Offset: 0x0000614E
		public int vddq { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00007F57 File Offset: 0x00006157
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00007F5F File Offset: 0x0000615F
		public int vpp { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007F68 File Offset: 0x00006168
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00007F70 File Offset: 0x00006170
		public int t_cl { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00007F79 File Offset: 0x00006179
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00007F81 File Offset: 0x00006181
		public int t_rcd { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00007F8A File Offset: 0x0000618A
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00007F92 File Offset: 0x00006192
		public int t_rp { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00007F9B File Offset: 0x0000619B
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00007FA3 File Offset: 0x000061A3
		public int t_ras { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00007FAC File Offset: 0x000061AC
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00007FB4 File Offset: 0x000061B4
		public int t_rc { get; set; }
	}
}
