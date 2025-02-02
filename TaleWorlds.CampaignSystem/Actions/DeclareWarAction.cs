using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000438 RID: 1080
	public static class DeclareWarAction
	{
		// Token: 0x0600408F RID: 16527 RVA: 0x0013E46C File Offset: 0x0013C66C
		private static void ApplyInternal(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail declareWarDetail)
		{
			FactionManager.DeclareWar(faction1, faction2, false);
			if (faction1.IsKingdomFaction && (float)faction2.Fiefs.Count > 1f + (float)faction1.Fiefs.Count * 0.2f)
			{
				Kingdom kingdom = (Kingdom)faction1;
				kingdom.PoliticalStagnation = (int)((float)kingdom.PoliticalStagnation * 0.85f - 3f);
				if (kingdom.PoliticalStagnation < 0)
				{
					kingdom.PoliticalStagnation = 0;
				}
			}
			if (faction2.IsKingdomFaction && (float)faction1.Fiefs.Count > 1f + (float)faction2.Fiefs.Count * 0.2f)
			{
				Kingdom kingdom2 = (Kingdom)faction2;
				kingdom2.PoliticalStagnation = (int)((float)kingdom2.PoliticalStagnation * 0.85f - 3f);
				if (kingdom2.PoliticalStagnation < 0)
				{
					kingdom2.PoliticalStagnation = 0;
				}
			}
			if (faction1 == Hero.MainHero.MapFaction || faction2 == Hero.MainHero.MapFaction)
			{
				IFaction dirtySide = (faction1 == Hero.MainHero.MapFaction) ? faction2 : faction1;
				IEnumerable<Settlement> all = Settlement.All;
				Func<Settlement, bool> predicate;
				Func<Settlement, bool> <>9__0;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((Settlement party) => party.IsVisible && party.MapFaction == dirtySide));
				}
				foreach (Settlement settlement in all.Where(predicate))
				{
					settlement.Party.SetVisualAsDirty();
				}
				IEnumerable<MobileParty> all2 = MobileParty.All;
				Func<MobileParty, bool> predicate2;
				Func<MobileParty, bool> <>9__1;
				if ((predicate2 = <>9__1) == null)
				{
					predicate2 = (<>9__1 = ((MobileParty party) => party.IsVisible && party.MapFaction == dirtySide));
				}
				foreach (MobileParty mobileParty in all2.Where(predicate2))
				{
					mobileParty.Party.SetVisualAsDirty();
				}
			}
			CampaignEventDispatcher.Instance.OnWarDeclared(faction1, faction2, declareWarDetail);
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x0013E668 File Offset: 0x0013C868
		public static void ApplyByKingdomDecision(IFaction faction1, IFaction faction2)
		{
			DeclareWarAction.ApplyInternal(faction1, faction2, DeclareWarAction.DeclareWarDetail.CausedByKingdomDecision);
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x0013E672 File Offset: 0x0013C872
		public static void ApplyByDefault(IFaction faction1, IFaction faction2)
		{
			DeclareWarAction.ApplyInternal(faction1, faction2, DeclareWarAction.DeclareWarDetail.Default);
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x0013E67C File Offset: 0x0013C87C
		public static void ApplyByPlayerHostility(IFaction faction1, IFaction faction2)
		{
			DeclareWarAction.ApplyInternal(faction1, faction2, DeclareWarAction.DeclareWarDetail.CausedByPlayerHostility);
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x0013E686 File Offset: 0x0013C886
		public static void ApplyByRebellion(IFaction faction1, IFaction faction2)
		{
			DeclareWarAction.ApplyInternal(faction1, faction2, DeclareWarAction.DeclareWarDetail.CausedByRebellion);
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x0013E690 File Offset: 0x0013C890
		public static void ApplyByCrimeRatingChange(IFaction faction1, IFaction faction2)
		{
			DeclareWarAction.ApplyInternal(faction1, faction2, DeclareWarAction.DeclareWarDetail.CausedByCrimeRatingChange);
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x0013E69A File Offset: 0x0013C89A
		public static void ApplyByKingdomCreation(IFaction faction1, IFaction faction2)
		{
			DeclareWarAction.ApplyInternal(faction1, faction2, DeclareWarAction.DeclareWarDetail.CausedByKingdomCreation);
		}

		// Token: 0x0200076C RID: 1900
		public enum DeclareWarDetail
		{
			// Token: 0x04001F0A RID: 7946
			Default,
			// Token: 0x04001F0B RID: 7947
			CausedByPlayerHostility,
			// Token: 0x04001F0C RID: 7948
			CausedByKingdomDecision,
			// Token: 0x04001F0D RID: 7949
			CausedByRebellion,
			// Token: 0x04001F0E RID: 7950
			CausedByCrimeRatingChange,
			// Token: 0x04001F0F RID: 7951
			CausedByKingdomCreation
		}
	}
}
