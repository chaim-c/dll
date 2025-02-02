using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020000FF RID: 255
	public class BasicBattleAgentOrigin : IAgentOriginBase
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x000162CE File Offset: 0x000144CE
		bool IAgentOriginBase.IsUnderPlayersCommand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x000162D1 File Offset: 0x000144D1
		uint IAgentOriginBase.FactionColor
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x000162D4 File Offset: 0x000144D4
		uint IAgentOriginBase.FactionColor2
		{
			get
			{
				return 0U;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x000162D7 File Offset: 0x000144D7
		IBattleCombatant IAgentOriginBase.BattleCombatant
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000162DA File Offset: 0x000144DA
		int IAgentOriginBase.UniqueSeed
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x000162DD File Offset: 0x000144DD
		int IAgentOriginBase.Seed
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000162E0 File Offset: 0x000144E0
		Banner IAgentOriginBase.Banner
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000162E3 File Offset: 0x000144E3
		BasicCharacterObject IAgentOriginBase.Troop
		{
			get
			{
				return this._troop;
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x000162EB File Offset: 0x000144EB
		public BasicBattleAgentOrigin(BasicCharacterObject troop)
		{
			this._troop = troop;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x000162FA File Offset: 0x000144FA
		void IAgentOriginBase.SetWounded()
		{
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x000162FC File Offset: 0x000144FC
		void IAgentOriginBase.SetKilled()
		{
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x000162FE File Offset: 0x000144FE
		void IAgentOriginBase.SetRouted()
		{
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00016300 File Offset: 0x00014500
		void IAgentOriginBase.OnAgentRemoved(float agentHealth)
		{
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00016302 File Offset: 0x00014502
		void IAgentOriginBase.OnScoreHit(BasicCharacterObject victim, BasicCharacterObject captain, int damage, bool isFatal, bool isTeamKill, WeaponComponentData attackerWeapon)
		{
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00016304 File Offset: 0x00014504
		void IAgentOriginBase.SetBanner(Banner banner)
		{
		}

		// Token: 0x040002B0 RID: 688
		private BasicCharacterObject _troop;
	}
}
