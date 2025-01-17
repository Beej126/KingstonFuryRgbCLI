namespace KingstonFuryRgbCLI
{
    // Token: 0x02000013 RID: 19
    public class DRAMJsonRoot
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00007B15 File Offset: 0x00005D15
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00007B1D File Offset: 0x00005D1D
		public DRAMCmdObj root { get; set; }

		// Token: 0x06000080 RID: 128 RVA: 0x00007B26 File Offset: 0x00005D26
		public DRAMJsonRoot()
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00007B2E File Offset: 0x00005D2E
		public DRAMJsonRoot(string api)
		{
			this.root = new DRAMCmdObj(api);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00007B42 File Offset: 0x00005D42
		public DRAMJsonRoot(string _api, string _status)
		{
			this.root.api = _api;
			this.root.status = _status;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00007B62 File Offset: 0x00005D62
		public DRAMJsonRoot(string _api, string _status, DRAMKernelInfo _kernel_info)
		{
			this.root.api = _api;
			this.root.status = _status;
			this.root.kernel_info = _kernel_info;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00007B8E File Offset: 0x00005D8E
		public DRAMJsonRoot(string _api, string _status, DRAMCmdObj _root)
		{
			this.root = _root;
			this.root.api = _api;
			this.root.status = _status;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00007BB5 File Offset: 0x00005DB5
		public DRAMJsonRoot(string api, DRAMCtrlObj _ctrl_settings)
		{
			this.root = new DRAMCmdObj(api, _ctrl_settings);
		}
	}
}
