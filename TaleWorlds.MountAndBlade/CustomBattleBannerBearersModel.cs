using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000203 RID: 515
	public class CustomBattleBannerBearersModel : BattleBannerBearersModel
	{
		// Token: 0x06001C6D RID: 7277 RVA: 0x000633DC File Offset: 0x000615DC
		public override int GetMinimumFormationTroopCountToBearBanners()
		{
			return 2;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000633DF File Offset: 0x000615DF
		public override float GetBannerInteractionDistance(Agent interactingAgent)
		{
			if (!interactingAgent.HasMount)
			{
				return 1.5f;
			}
			return 3f;
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000633F4 File Offset: 0x000615F4
		public override bool CanBannerBearerProvideEffectToFormation(Agent agent, Formation formation)
		{
			return agent.Formation == formation || (agent.IsPlayerControlled && agent.Team == formation.Team);
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x0006341C File Offset: 0x0006161C
		public override bool CanAgentPickUpAnyBanner(Agent agent)
		{
			return agent.IsHuman && agent.Banner == null && agent.CanBeAssignedForScriptedMovement() && (agent.CommonAIComponent == null || !agent.CommonAIComponent.IsPanicked) && (agent.HumanAIComponent == null || !agent.HumanAIComponent.IsInImportantCombatAction());
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x00063470 File Offset: 0x00061670
		public override bool CanAgentBecomeBannerBearer(Agent agent)
		{
			if (CustomBattleBannerBearersModel._missionSpawnLogic == null)
			{
				CustomBattleBannerBearersModel._missionSpawnLogic = Mission.Current.GetMissionBehavior<MissionAgentSpawnLogic>();
			}
			if (CustomBattleBannerBearersModel._missionSpawnLogic != null)
			{
				Formation formation = agent.Formation;
				Team team = (formation != null) ? formation.Team : null;
				if (team != null)
				{
					BasicCharacterObject generalCharacterOfSide = CustomBattleBannerBearersModel._missionSpawnLogic.GetGeneralCharacterOfSide(team.Side);
					return agent.IsHuman && !agent.IsMainAgent && agent.IsAIControlled && agent.Character != generalCharacterOfSide;
				}
			}
			return false;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000634EC File Offset: 0x000616EC
		public override int GetAgentBannerBearingPriority(Agent agent)
		{
			if (!this.CanAgentBecomeBannerBearer(agent))
			{
				return 0;
			}
			if (agent.Formation != null)
			{
				bool calculateHasSignificantNumberOfMounted = agent.Formation.CalculateHasSignificantNumberOfMounted;
				if ((calculateHasSignificantNumberOfMounted && !agent.HasMount) || (!calculateHasSignificantNumberOfMounted && agent.HasMount))
				{
					return 0;
				}
			}
			int num = Math.Min(agent.Character.Level / 4 + 1, CustomBattleBannerBearersModel.BannerBearerPriorityPerTier.Length - 1);
			return CustomBattleBannerBearersModel.BannerBearerPriorityPerTier[num];
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x00063558 File Offset: 0x00061758
		public override bool CanFormationDeployBannerBearers(Formation formation)
		{
			BannerBearerLogic bannerBearerLogic = base.BannerBearerLogic;
			return bannerBearerLogic != null && formation.CountOfUnits >= this.GetMinimumFormationTroopCountToBearBanners() && bannerBearerLogic.GetFormationBanner(formation) != null && formation.UnitsWithoutLooseDetachedOnes.Count(delegate(IFormationUnit unit)
			{
				Agent agent;
				return (agent = (unit as Agent)) != null && this.CanAgentBecomeBannerBearer(agent);
			}) > 0;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x000635A2 File Offset: 0x000617A2
		public override int GetDesiredNumberOfBannerBearersForFormation(Formation formation)
		{
			if (!this.CanFormationDeployBannerBearers(formation))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001C75 RID: 7285 RVA: 0x000635B0 File Offset: 0x000617B0
		public override ItemObject GetBannerBearerReplacementWeapon(BasicCharacterObject agentCharacter)
		{
			if (CustomBattleBannerBearersModel.ReplacementWeapons == null)
			{
				CustomBattleBannerBearersModel.ReplacementWeapons = MBObjectManager.Instance.GetObjectTypeList<ItemObject>().Where(delegate(ItemObject item)
				{
					if (item.PrimaryWeapon != null)
					{
						WeaponComponentData primaryWeapon = item.PrimaryWeapon;
						return primaryWeapon.WeaponClass == WeaponClass.OneHandedSword;
					}
					return false;
				}).ToList<ItemObject>();
			}
			if (CustomBattleBannerBearersModel.ReplacementWeapons.IsEmpty<ItemObject>())
			{
				return null;
			}
			IEnumerable<ItemObject> enumerable = from item in CustomBattleBannerBearersModel.ReplacementWeapons
			where item.Culture != null && item.Culture.GetCultureCode() == agentCharacter.Culture.GetCultureCode()
			select item;
			List<ValueTuple<int, ItemObject>> list = new List<ValueTuple<int, ItemObject>>();
			int minTierDifference = int.MaxValue;
			foreach (ItemObject itemObject in enumerable)
			{
				int num = MathF.Ceiling(((float)agentCharacter.Level - 5f) / 5f);
				num = MathF.Min(MathF.Max(num, 0), 7);
				int num2 = MathF.Abs(itemObject.Tier - (ItemObject.ItemTiers)num);
				if (num2 < minTierDifference)
				{
					minTierDifference = num2;
				}
				list.Add(new ValueTuple<int, ItemObject>(num2, itemObject));
			}
			return (from tuple in list
			where tuple.Item1 == minTierDifference
			select tuple).GetRandomElementInefficiently<ValueTuple<int, ItemObject>>().Item2;
		}

		// Token: 0x0400091B RID: 2331
		private static readonly int[] BannerBearerPriorityPerTier = new int[]
		{
			0,
			1,
			3,
			5,
			6,
			4,
			2
		};

		// Token: 0x0400091C RID: 2332
		private static List<ItemObject> ReplacementWeapons = null;

		// Token: 0x0400091D RID: 2333
		private static MissionAgentSpawnLogic _missionSpawnLogic;
	}
}
