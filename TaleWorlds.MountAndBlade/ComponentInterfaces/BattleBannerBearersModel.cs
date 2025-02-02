using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003CB RID: 971
	public abstract class BattleBannerBearersModel : GameModel
	{
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600337C RID: 13180 RVA: 0x000D5836 File Offset: 0x000D3A36
		protected BannerBearerLogic BannerBearerLogic
		{
			get
			{
				return this._bannerBearerLogic;
			}
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000D583E File Offset: 0x000D3A3E
		public void InitializeModel(BannerBearerLogic bannerBearerLogic)
		{
			this._bannerBearerLogic = bannerBearerLogic;
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000D5847 File Offset: 0x000D3A47
		public void FinalizeModel()
		{
			this._bannerBearerLogic = null;
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000D5850 File Offset: 0x000D3A50
		public bool IsFormationBanner(Formation formation, SpawnedItemEntity item)
		{
			if (formation == null)
			{
				return false;
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			return bannerBearerLogic != null && bannerBearerLogic.IsFormationBanner(formation, item);
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000D5878 File Offset: 0x000D3A78
		public bool IsBannerSearchingAgent(Agent agent)
		{
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			return bannerBearerLogic != null && bannerBearerLogic.IsBannerSearchingAgent(agent);
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000D5898 File Offset: 0x000D3A98
		public bool IsInteractableFormationBanner(SpawnedItemEntity item, Agent interactingAgent)
		{
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			Formation formation = (bannerBearerLogic != null) ? bannerBearerLogic.GetFormationFromBanner(item) : null;
			return formation == null || formation.Captain == interactingAgent || interactingAgent.Formation == formation || (interactingAgent.IsPlayerControlled && interactingAgent.Team == formation.Team);
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000D58EA File Offset: 0x000D3AEA
		public bool HasFormationBanner(Formation formation)
		{
			if (formation == null)
			{
				return false;
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			return ((bannerBearerLogic != null) ? bannerBearerLogic.GetFormationBanner(formation) : null) != null;
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000D5908 File Offset: 0x000D3B08
		public bool HasBannerOnGround(Formation formation)
		{
			if (formation == null)
			{
				return false;
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			return bannerBearerLogic != null && bannerBearerLogic.HasBannerOnGround(formation);
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000D592D File Offset: 0x000D3B2D
		public ItemObject GetFormationBanner(Formation formation)
		{
			if (formation == null)
			{
				return null;
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			if (bannerBearerLogic == null)
			{
				return null;
			}
			return bannerBearerLogic.GetFormationBanner(formation);
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000D5948 File Offset: 0x000D3B48
		public List<Agent> GetFormationBannerBearers(Formation formation)
		{
			if (formation == null)
			{
				return new List<Agent>();
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			if (bannerBearerLogic != null)
			{
				return bannerBearerLogic.GetFormationBannerBearers(formation);
			}
			return new List<Agent>();
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000D5975 File Offset: 0x000D3B75
		public BannerComponent GetActiveBanner(Formation formation)
		{
			if (formation == null)
			{
				return null;
			}
			BannerBearerLogic bannerBearerLogic = this.BannerBearerLogic;
			if (bannerBearerLogic == null)
			{
				return null;
			}
			return bannerBearerLogic.GetActiveBanner(formation);
		}

		// Token: 0x06003387 RID: 13191
		public abstract int GetMinimumFormationTroopCountToBearBanners();

		// Token: 0x06003388 RID: 13192
		public abstract float GetBannerInteractionDistance(Agent interactingAgent);

		// Token: 0x06003389 RID: 13193
		public abstract bool CanBannerBearerProvideEffectToFormation(Agent agent, Formation formation);

		// Token: 0x0600338A RID: 13194
		public abstract bool CanAgentPickUpAnyBanner(Agent agent);

		// Token: 0x0600338B RID: 13195
		public abstract bool CanAgentBecomeBannerBearer(Agent agent);

		// Token: 0x0600338C RID: 13196
		public abstract int GetAgentBannerBearingPriority(Agent agent);

		// Token: 0x0600338D RID: 13197
		public abstract bool CanFormationDeployBannerBearers(Formation formation);

		// Token: 0x0600338E RID: 13198
		public abstract int GetDesiredNumberOfBannerBearersForFormation(Formation formation);

		// Token: 0x0600338F RID: 13199
		public abstract ItemObject GetBannerBearerReplacementWeapon(BasicCharacterObject agentCharacter);

		// Token: 0x04001650 RID: 5712
		public const float DefaultDetachmentCostMultiplier = 10f;

		// Token: 0x04001651 RID: 5713
		private BannerBearerLogic _bannerBearerLogic;
	}
}
