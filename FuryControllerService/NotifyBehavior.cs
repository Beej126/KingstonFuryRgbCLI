using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace FuryControllerService
{
	// Token: 0x02000007 RID: 7
	public class NotifyBehavior : WebSocketBehavior
	{
		// Token: 0x0600004C RID: 76 RVA: 0x0000298B File Offset: 0x00000B8B
		protected override void OnOpen()
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002990 File Offset: 0x00000B90
		protected override void OnMessage(MessageEventArgs e)
		{
			try
			{
				if (e != null && !string.IsNullOrEmpty(e.Data))
				{
					string text = FuryContorller_Service.MainWindow_OnRequestFromClient(StringEncryptDecrypt.Decrypt(e.Data, "3m23s45i599"));
					if (!string.IsNullOrEmpty(text))
					{
						base.Send(StringEncryptDecrypt.Encrypt(text, "3m23s45i599"));
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029F4 File Offset: 0x00000BF4
		protected override void OnClose(CloseEventArgs e)
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029F6 File Offset: 0x00000BF6
		protected override void OnError(ErrorEventArgs e)
		{
		}
	}
}
