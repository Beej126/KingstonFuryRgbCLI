using System.Collections.Generic;

namespace KingstonFuryRgbCLI
{
    // Token: 0x0200001C RID: 28
    public class DRAMCtrlColorObj
	{
		// Token: 0x06000132 RID: 306 RVA: 0x000082A8 File Offset: 0x000064A8
		public DRAMCtrlColorObj()
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000082B0 File Offset: 0x000064B0
		public DRAMCtrlColorObj(int _index, List<List<int>> _colors)
		{
			this.index = _index;
			this.colors = _colors;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000082C6 File Offset: 0x000064C6
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000082CE File Offset: 0x000064CE
		public int index { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000082D7 File Offset: 0x000064D7
		// (set) Token: 0x06000137 RID: 311 RVA: 0x000082DF File Offset: 0x000064DF
		public List<List<int>> colors { get; set; }
	}
}
