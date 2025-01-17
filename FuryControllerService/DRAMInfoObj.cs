using System;

namespace FuryControllerService
{
	// Token: 0x02000016 RID: 22
	public class DRAMInfoObj
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00007CFD File Offset: 0x00005EFD
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00007D05 File Offset: 0x00005F05
		public int index { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00007D0E File Offset: 0x00005F0E
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00007D16 File Offset: 0x00005F16
		public int address_offset { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00007D1F File Offset: 0x00005F1F
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00007D27 File Offset: 0x00005F27
		public int master_slave { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00007D30 File Offset: 0x00005F30
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00007D38 File Offset: 0x00005F38
		public string manufac { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00007D41 File Offset: 0x00005F41
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00007D49 File Offset: 0x00005F49
		public string brand { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00007D52 File Offset: 0x00005F52
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00007D5A File Offset: 0x00005F5A
		public string product { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00007D63 File Offset: 0x00005F63
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00007D6B File Offset: 0x00005F6B
		public string part_number { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00007D74 File Offset: 0x00005F74
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00007D7C File Offset: 0x00005F7C
		public bool support_xmp { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00007D85 File Offset: 0x00005F85
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00007D8D File Offset: 0x00005F8D
		public int type { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00007D96 File Offset: 0x00005F96
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00007D9E File Offset: 0x00005F9E
		public int api_ver { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00007DA7 File Offset: 0x00005FA7
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00007DAF File Offset: 0x00005FAF
		public int model_id { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00007DB8 File Offset: 0x00005FB8
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00007DC0 File Offset: 0x00005FC0
		public int mode { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00007DC9 File Offset: 0x00005FC9
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00007DD1 File Offset: 0x00005FD1
		public int speed { get; set; }
	}
}
