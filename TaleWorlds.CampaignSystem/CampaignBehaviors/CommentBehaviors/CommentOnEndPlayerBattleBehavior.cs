using System;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003F2 RID: 1010
	public class CommentOnEndPlayerBattleBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003EA2 RID: 16034 RVA: 0x00133A6E File Offset: 0x00131C6E
		public override void RegisterEvents()
		{
			CampaignEvents.OnPlayerBattleEndEvent.AddNonSerializedListener(this, new Action<MapEvent>(this.OnPlayerBattleEnded));
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x00133A87 File Offset: 0x00131C87
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x00133A89 File Offset: 0x00131C89
		private void OnPlayerBattleEnded(MapEvent mapEvent)
		{
			if (!mapEvent.IsHideoutBattle || mapEvent.BattleState != BattleState.None)
			{
				LogEntry.AddLogEntry(new PlayerBattleEndedLogEntry(mapEvent));
			}
		}
	}
}
