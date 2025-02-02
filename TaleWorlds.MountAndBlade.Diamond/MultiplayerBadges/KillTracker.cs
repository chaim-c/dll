using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x02000174 RID: 372
	public class KillTracker : GameBadgeTracker
	{
		// Token: 0x06000A4C RID: 2636 RVA: 0x00010D90 File Offset: 0x0000EF90
		public KillTracker(string badgeId, BadgeCondition condition, Dictionary<ValueTuple<PlayerId, string, string>, int> dataDictionary)
		{
			this._badgeId = badgeId;
			this._condition = condition;
			this._dataDictionary = dataDictionary;
			this._faction = null;
			this._troop = null;
			string faction;
			if (condition.Parameters.TryGetValue("faction", out faction))
			{
				this._faction = faction;
			}
			string troop;
			if (condition.Parameters.TryGetValue("troop", out troop))
			{
				this._troop = troop;
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00010DFC File Offset: 0x0000EFFC
		public override void OnKill(KillData killData)
		{
			if (killData.KillerId.IsValid && killData.VictimId.IsValid && !killData.KillerId.Equals(killData.VictimId) && (this._faction == null || this._faction == killData.KillerFaction) && (this._troop == null || this._troop == killData.KillerTroop))
			{
				int num;
				if (!this._dataDictionary.TryGetValue(new ValueTuple<PlayerId, string, string>(killData.KillerId, this._badgeId, this._condition.StringId), out num))
				{
					num = 0;
				}
				this._dataDictionary[new ValueTuple<PlayerId, string, string>(killData.KillerId, this._badgeId, this._condition.StringId)] = num + 1;
			}
		}

		// Token: 0x040004ED RID: 1261
		private readonly string _badgeId;

		// Token: 0x040004EE RID: 1262
		private readonly BadgeCondition _condition;

		// Token: 0x040004EF RID: 1263
		private readonly Dictionary<ValueTuple<PlayerId, string, string>, int> _dataDictionary;

		// Token: 0x040004F0 RID: 1264
		private readonly string _faction;

		// Token: 0x040004F1 RID: 1265
		private readonly string _troop;
	}
}
