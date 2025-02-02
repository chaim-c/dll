using System;
using TaleWorlds.Library.Http;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D4 RID: 724
	public class CommunityClient
	{
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x00099C21 File Offset: 0x00097E21
		// (set) Token: 0x060027E3 RID: 10211 RVA: 0x00099C29 File Offset: 0x00097E29
		public bool IsInGame { get; private set; }

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x00099C32 File Offset: 0x00097E32
		// (set) Token: 0x060027E5 RID: 10213 RVA: 0x00099C3A File Offset: 0x00097E3A
		public ICommunityClientHandler Handler { get; set; }

		// Token: 0x060027E6 RID: 10214 RVA: 0x00099C43 File Offset: 0x00097E43
		public CommunityClient()
		{
			this._httpDriver = HttpDriverManager.GetDefaultHttpDriver();
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x00099C56 File Offset: 0x00097E56
		public void QuitFromGame()
		{
			if (this.IsInGame)
			{
				this.IsInGame = false;
				ICommunityClientHandler handler = this.Handler;
				if (handler == null)
				{
					return;
				}
				handler.OnQuitFromGame();
			}
		}

		// Token: 0x04000EBD RID: 3773
		private IHttpDriver _httpDriver;
	}
}
