using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200029C RID: 668
	public class BotData
	{
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060023A3 RID: 9123 RVA: 0x0008485D File Offset: 0x00082A5D
		public int Score
		{
			get
			{
				return this.KillCount * 3 + this.AssistCount;
			}
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0008486E File Offset: 0x00082A6E
		public BotData()
		{
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x00084876 File Offset: 0x00082A76
		public BotData(int kill, int assist, int death, int alive)
		{
			this.KillCount = kill;
			this.DeathCount = death;
			this.AssistCount = assist;
			this.AliveCount = alive;
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0008489B File Offset: 0x00082A9B
		public bool IsAnyValid
		{
			get
			{
				return this.KillCount != 0 || this.DeathCount != 0 || this.AssistCount != 0 || this.AliveCount != 0;
			}
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x000848C0 File Offset: 0x00082AC0
		public void ResetKillDeathAssist()
		{
			this.KillCount = 0;
			this.DeathCount = 0;
			this.AssistCount = 0;
		}

		// Token: 0x04000D11 RID: 3345
		public int AliveCount;

		// Token: 0x04000D12 RID: 3346
		public int KillCount;

		// Token: 0x04000D13 RID: 3347
		public int DeathCount;

		// Token: 0x04000D14 RID: 3348
		public int AssistCount;
	}
}
