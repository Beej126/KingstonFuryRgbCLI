using System;
using System.Collections.Generic;

namespace FuryControllerService
{
	// Token: 0x02000018 RID: 24
	public class DRAMTimingTableObj
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00007E61 File Offset: 0x00006061
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00007E69 File Offset: 0x00006069
		public int index { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00007E72 File Offset: 0x00006072
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00007E7A File Offset: 0x0000607A
		public int address_offset { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00007E83 File Offset: 0x00006083
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00007E8B File Offset: 0x0000608B
		public double vdd { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00007E94 File Offset: 0x00006094
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00007E9C File Offset: 0x0000609C
		public double vddq { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00007EA5 File Offset: 0x000060A5
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00007EAD File Offset: 0x000060AD
		public double vpp { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00007EB6 File Offset: 0x000060B6
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00007EBE File Offset: 0x000060BE
		public int temperature { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00007EC7 File Offset: 0x000060C7
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00007ECF File Offset: 0x000060CF
		public string part_number { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00007ED8 File Offset: 0x000060D8
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00007EE0 File Offset: 0x000060E0
		public double version { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00007EE9 File Offset: 0x000060E9
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00007EF1 File Offset: 0x000060F1
		public List<DRAMTimingsObj> timing_table { get; set; }
	}
}
