using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace TaskScheduler
{
	// Token: 0x0200003F RID: 63
	[CompilerGenerated]
	[TypeIdentifier("e34cb9f1-c7f7-424c-be29-027dcc09363a", "TaskScheduler._TASK_LOGON_TYPE")]
	public enum _TASK_LOGON_TYPE
	{
		// Token: 0x04000158 RID: 344
		TASK_LOGON_NONE,
		// Token: 0x04000159 RID: 345
		TASK_LOGON_PASSWORD,
		// Token: 0x0400015A RID: 346
		TASK_LOGON_S4U,
		// Token: 0x0400015B RID: 347
		TASK_LOGON_INTERACTIVE_TOKEN,
		// Token: 0x0400015C RID: 348
		TASK_LOGON_GROUP,
		// Token: 0x0400015D RID: 349
		TASK_LOGON_SERVICE_ACCOUNT,
		// Token: 0x0400015E RID: 350
		TASK_LOGON_INTERACTIVE_TOKEN_OR_PASSWORD
	}
}
