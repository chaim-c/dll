using System;
using System.Linq;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200043A RID: 1082
	public static class DestroyKingdomAction
	{
		// Token: 0x0600409A RID: 16538 RVA: 0x0013E8E4 File Offset: 0x0013CAE4
		private static void ApplyInternal(Kingdom destroyedKingdom, bool isKingdomLeaderDeath = false)
		{
			destroyedKingdom.DeactivateKingdom();
			foreach (Clan clan in destroyedKingdom.Clans.ToList<Clan>())
			{
				if (!clan.IsEliminated)
				{
					if (isKingdomLeaderDeath)
					{
						DestroyClanAction.ApplyByClanLeaderDeath(clan);
					}
					else
					{
						DestroyClanAction.Apply(clan);
					}
					destroyedKingdom.RemoveClanInternal(clan);
				}
			}
			Campaign.Current.FactionManager.RemoveFactionsFromCampaignWars(destroyedKingdom);
			CampaignEventDispatcher.Instance.OnKingdomDestroyed(destroyedKingdom);
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x0013E978 File Offset: 0x0013CB78
		public static void Apply(Kingdom destroyedKingdom)
		{
			DestroyKingdomAction.ApplyInternal(destroyedKingdom, false);
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x0013E981 File Offset: 0x0013CB81
		public static void ApplyByKingdomLeaderDeath(Kingdom destroyedKingdom)
		{
			DestroyKingdomAction.ApplyInternal(destroyedKingdom, true);
		}
	}
}
