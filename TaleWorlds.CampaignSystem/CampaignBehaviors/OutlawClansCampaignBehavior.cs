using System;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003B5 RID: 949
	public class OutlawClansCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003A1A RID: 14874 RVA: 0x001116AC File Offset: 0x0010F8AC
		private static void MakeOutlawFactionsEnemyToKingdomFactions()
		{
			foreach (Clan clan in Clan.All)
			{
				if (clan.IsMinorFaction && clan.IsOutlaw)
				{
					foreach (Kingdom kingdom in Kingdom.All)
					{
						if (kingdom.Culture == clan.Culture)
						{
							FactionManager.DeclareWar(kingdom, clan, true);
						}
					}
				}
			}
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x00111758 File Offset: 0x0010F958
		public override void RegisterEvents()
		{
			CampaignEvents.OnNewGameCreatedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreated));
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x00111771 File Offset: 0x0010F971
		private void OnNewGameCreated(CampaignGameStarter starter)
		{
			OutlawClansCampaignBehavior.MakeOutlawFactionsEnemyToKingdomFactions();
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x00111778 File Offset: 0x0010F978
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
