using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x02000170 RID: 368
	public class BadgeOwnerKillTracker : GameBadgeTracker
	{
		// Token: 0x06000A35 RID: 2613 RVA: 0x00010AE0 File Offset: 0x0000ECE0
		public BadgeOwnerKillTracker(string badgeId, BadgeCondition condition, Dictionary<ValueTuple<PlayerId, string, string>, int> dataDictionary)
		{
			this._badgeId = badgeId;
			this._condition = condition;
			this._playerBadgeMap = new Dictionary<PlayerId, bool>();
			this._dataDictionary = dataDictionary;
			this._requiredBadges = new List<string>();
			foreach (KeyValuePair<string, string> keyValuePair in condition.Parameters)
			{
				if (keyValuePair.Key.StartsWith("required_badge."))
				{
					this._requiredBadges.Add(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00010B7C File Offset: 0x0000ED7C
		public override void OnPlayerJoin(PlayerData playerData)
		{
			this._playerBadgeMap[playerData.PlayerId] = this._requiredBadges.Contains(playerData.ShownBadgeId);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00010BA0 File Offset: 0x0000EDA0
		public override void OnKill(KillData killData)
		{
			bool flag;
			if (killData.KillerId.IsValid && killData.VictimId.IsValid && !killData.KillerId.Equals(killData.VictimId) && this._playerBadgeMap.TryGetValue(killData.VictimId, out flag) && flag)
			{
				this._playerBadgeMap[killData.KillerId] = true;
				int num;
				if (!this._dataDictionary.TryGetValue(new ValueTuple<PlayerId, string, string>(killData.KillerId, this._badgeId, this._condition.StringId), out num))
				{
					num = 0;
				}
				this._dataDictionary[new ValueTuple<PlayerId, string, string>(killData.KillerId, this._badgeId, this._condition.StringId)] = num + 1;
			}
		}

		// Token: 0x040004E1 RID: 1249
		private readonly string _badgeId;

		// Token: 0x040004E2 RID: 1250
		private readonly BadgeCondition _condition;

		// Token: 0x040004E3 RID: 1251
		private readonly List<string> _requiredBadges;

		// Token: 0x040004E4 RID: 1252
		private readonly Dictionary<ValueTuple<PlayerId, string, string>, int> _dataDictionary;

		// Token: 0x040004E5 RID: 1253
		private readonly Dictionary<PlayerId, bool> _playerBadgeMap;
	}
}
