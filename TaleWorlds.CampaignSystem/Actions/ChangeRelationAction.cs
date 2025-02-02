using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000433 RID: 1075
	public static class ChangeRelationAction
	{
		// Token: 0x0600407D RID: 16509 RVA: 0x0013E290 File Offset: 0x0013C490
		private static void ApplyInternal(Hero originalHero, Hero originalGainedRelationWith, int relationChange, bool showQuickNotification, ChangeRelationAction.ChangeRelationDetail detail)
		{
			if (relationChange > 0)
			{
				relationChange = MBRandom.RoundRandomized(Campaign.Current.Models.DiplomacyModel.GetRelationIncreaseFactor(originalHero, originalGainedRelationWith, (float)relationChange));
			}
			if (relationChange != 0)
			{
				Hero hero;
				Hero hero2;
				Campaign.Current.Models.DiplomacyModel.GetHeroesForEffectiveRelation(originalHero, originalGainedRelationWith, out hero, out hero2);
				int value = CharacterRelationManager.GetHeroRelation(hero, hero2) + relationChange;
				value = MBMath.ClampInt(value, -100, 100);
				hero.SetPersonalRelation(hero2, value);
				CampaignEventDispatcher.Instance.OnHeroRelationChanged(hero, hero2, relationChange, showQuickNotification, detail, originalHero, originalGainedRelationWith);
			}
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x0013E30C File Offset: 0x0013C50C
		public static void ApplyPlayerRelation(Hero gainedRelationWith, int relation, bool affectRelatives = true, bool showQuickNotification = true)
		{
			ChangeRelationAction.ApplyInternal(Hero.MainHero, gainedRelationWith, relation, showQuickNotification, ChangeRelationAction.ChangeRelationDetail.Default);
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x0013E31C File Offset: 0x0013C51C
		public static void ApplyRelationChangeBetweenHeroes(Hero hero, Hero gainedRelationWith, int relationChange, bool showQuickNotification = true)
		{
			ChangeRelationAction.ApplyInternal(hero, gainedRelationWith, relationChange, showQuickNotification, ChangeRelationAction.ChangeRelationDetail.Default);
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x0013E328 File Offset: 0x0013C528
		public static void ApplyEmissaryRelation(Hero emissary, Hero gainedRelationWith, int relationChange, bool showQuickNotification = true)
		{
			ChangeRelationAction.ApplyInternal(emissary, gainedRelationWith, relationChange, showQuickNotification, ChangeRelationAction.ChangeRelationDetail.Emissary);
		}

		// Token: 0x0200076B RID: 1899
		public enum ChangeRelationDetail
		{
			// Token: 0x04001F07 RID: 7943
			Default,
			// Token: 0x04001F08 RID: 7944
			Emissary
		}
	}
}
