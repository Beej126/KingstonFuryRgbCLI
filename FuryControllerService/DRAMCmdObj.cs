using System;
using System.Collections.Generic;

namespace FuryControllerService
{
	// Token: 0x02000014 RID: 20
	public class DRAMCmdObj
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00007BCA File Offset: 0x00005DCA
		public DRAMCmdObj()
		{
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00007BD2 File Offset: 0x00005DD2
		public DRAMCmdObj(string _api)
		{
			this.api = _api;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00007BE1 File Offset: 0x00005DE1
		public DRAMCmdObj(string _api, DRAMCtrlObj _ctrl_settings)
		{
			this.api = _api;
			this.ctrl_settings = new DRAMCtrlObj(_ctrl_settings.mode, _ctrl_settings.ctrl_mode);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00007C07 File Offset: 0x00005E07
		public DRAMCmdObj(string _api, Dictionary<string, DRAMDDR5CtrlObj> _ctrl_settings_ddr5)
		{
			this.api = _api;
			this.ctrl_settings_ddr5 = _ctrl_settings_ddr5;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00007C1D File Offset: 0x00005E1D
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00007C25 File Offset: 0x00005E25
		public string api { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00007C2E File Offset: 0x00005E2E
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00007C36 File Offset: 0x00005E36
		public string status { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00007C3F File Offset: 0x00005E3F
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00007C47 File Offset: 0x00005E47
		public string version { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00007C50 File Offset: 0x00005E50
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00007C58 File Offset: 0x00005E58
		public string keep { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00007C61 File Offset: 0x00005E61
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00007C69 File Offset: 0x00005E69
		public DRAMKernelInfo kernel_info { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00007C72 File Offset: 0x00005E72
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00007C7A File Offset: 0x00005E7A
		public Dictionary<string, DRAMInfoObj> dram { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00007C83 File Offset: 0x00005E83
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00007C8B File Offset: 0x00005E8B
		public DRAMCtrlObj ctrl_settings { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00007C94 File Offset: 0x00005E94
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00007C9C File Offset: 0x00005E9C
		public Dictionary<string, DRAMDDR5CtrlObj> ctrl_settings_ddr5 { get; set; }
	}
}
