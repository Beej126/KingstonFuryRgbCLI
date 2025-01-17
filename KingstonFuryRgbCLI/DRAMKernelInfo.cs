namespace KingstonFuryRgbCLI
{
    // Token: 0x02000015 RID: 21
    public class DRAMKernelInfo
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00007CA5 File Offset: 0x00005EA5
		public DRAMKernelInfo()
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007CAD File Offset: 0x00005EAD
		public DRAMKernelInfo(string _version, string _setup_path, string _setup_args)
		{
			this.version = _version;
			this.setup_path = _setup_path;
			this.setup_args = _setup_args;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00007CCA File Offset: 0x00005ECA
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00007CD2 File Offset: 0x00005ED2
		public string version { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00007CDB File Offset: 0x00005EDB
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00007CE3 File Offset: 0x00005EE3
		public string setup_path { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00007CEC File Offset: 0x00005EEC
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00007CF4 File Offset: 0x00005EF4
		public string setup_args { get; set; }
	}
}
