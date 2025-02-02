using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001FD RID: 509
	public class MultiplayerBattleBannerBearersModel : BattleBannerBearersModel
	{
		// Token: 0x06001C4B RID: 7243 RVA: 0x00062F00 File Offset: 0x00061100
		public override int GetMinimumFormationTroopCountToBearBanners()
		{
			return int.MaxValue;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x00062F07 File Offset: 0x00061107
		public override float GetBannerInteractionDistance(Agent interactingAgent)
		{
			return float.MaxValue;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x00062F0E File Offset: 0x0006110E
		public override bool CanAgentPickUpAnyBanner(Agent agent)
		{
			return false;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x00062F11 File Offset: 0x00061111
		public override bool CanBannerBearerProvideEffectToFormation(Agent agent, Formation formation)
		{
			return false;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00062F14 File Offset: 0x00061114
		public override bool CanAgentBecomeBannerBearer(Agent agent)
		{
			return false;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00062F17 File Offset: 0x00061117
		public override int GetAgentBannerBearingPriority(Agent agent)
		{
			return 0;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00062F1A File Offset: 0x0006111A
		public override bool CanFormationDeployBannerBearers(Formation formation)
		{
			return false;
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x00062F1D File Offset: 0x0006111D
		public override int GetDesiredNumberOfBannerBearersForFormation(Formation formation)
		{
			return 0;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00062F20 File Offset: 0x00061120
		public override ItemObject GetBannerBearerReplacementWeapon(BasicCharacterObject agentCharacter)
		{
			return null;
		}
	}
}
