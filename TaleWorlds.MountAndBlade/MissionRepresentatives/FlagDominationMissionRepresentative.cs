using System;
using System.Collections.Generic;
using NetworkMessages.FromServer;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.MissionRepresentatives
{
	// Token: 0x020003A3 RID: 931
	public class FlagDominationMissionRepresentative : MissionRepresentativeBase
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x000D0995 File Offset: 0x000CEB95
		private bool Forfeited
		{
			get
			{
				return base.Gold < 0;
			}
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x000D09A0 File Offset: 0x000CEBA0
		public int GetGoldAmountForVisual()
		{
			if (base.Gold < 0)
			{
				return 80;
			}
			return base.Gold;
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000D09B4 File Offset: 0x000CEBB4
		public void UpdateSelectedClassServer(Agent agent)
		{
			this._survivedLastRound = (agent != null);
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x000D09C0 File Offset: 0x000CEBC0
		public bool CheckIfSurvivedLastRoundAndReset()
		{
			bool survivedLastRound = this._survivedLastRound;
			this._survivedLastRound = false;
			return survivedLastRound;
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x000D09D0 File Offset: 0x000CEBD0
		public int GetGoldGainsFromKillData(MPPerkObject.MPPerkHandler killerPerkHandler, MPPerkObject.MPPerkHandler assistingHitterPerkHandler, MultiplayerClassDivisions.MPHeroClass victimClass, bool isAssist, bool isFriendly)
		{
			if (isFriendly || this.Forfeited)
			{
				return 0;
			}
			int num;
			if (isAssist)
			{
				num = ((killerPerkHandler != null) ? killerPerkHandler.GetRewardedGoldOnAssist() : 0) + ((assistingHitterPerkHandler != null) ? assistingHitterPerkHandler.GetGoldOnAssist() : 0);
			}
			else
			{
				int num2 = (base.ControlledAgent != null) ? MultiplayerClassDivisions.GetMPHeroClassForCharacter(base.ControlledAgent.Character).TroopBattleCost : 0;
				num = ((killerPerkHandler != null) ? killerPerkHandler.GetGoldOnKill((float)num2, (float)victimClass.TroopBattleCost) : 0);
			}
			if (num > 0)
			{
				GameNetwork.BeginModuleEventAsServer(base.Peer);
				GameNetwork.WriteMessage(new GoldGain(new List<KeyValuePair<ushort, int>>
				{
					new KeyValuePair<ushort, int>(2048, num)
				}));
				GameNetwork.EndModuleEventAsServer();
			}
			return num;
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000D0A7C File Offset: 0x000CEC7C
		public int GetGoldGainFromKillDataAndUpdateFlags(MultiplayerClassDivisions.MPHeroClass victimClass, bool isAssist)
		{
			int num = 0;
			int num2 = 50;
			List<KeyValuePair<ushort, int>> list = new List<KeyValuePair<ushort, int>>();
			if (base.ControlledAgent != null)
			{
				num2 += victimClass.TroopBattleCost - MultiplayerClassDivisions.GetMPHeroClassForCharacter(base.ControlledAgent.Character).TroopBattleCost / 2;
			}
			if (isAssist)
			{
				int num3 = MathF.Max(5, num2 / 10);
				num += num3;
				list.Add(new KeyValuePair<ushort, int>(256, num3));
			}
			else if (base.ControlledAgent != null)
			{
				int num4 = MathF.Max(10, num2 / 5);
				num += num4;
				list.Add(new KeyValuePair<ushort, int>(128, num4));
			}
			if (list.Count > 0 && !base.Peer.Communicator.IsServerPeer && base.Peer.Communicator.IsConnectionActive)
			{
				GameNetwork.BeginModuleEventAsServer(base.Peer);
				GameNetwork.WriteMessage(new GoldGain(list));
				GameNetwork.EndModuleEventAsServer();
			}
			return num;
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x000D0B58 File Offset: 0x000CED58
		public int GetGoldGainsFromAllyDeathReward(int baseAmount)
		{
			if (this.Forfeited)
			{
				return 0;
			}
			if (baseAmount > 0 && !base.Peer.Communicator.IsServerPeer && base.Peer.Communicator.IsConnectionActive)
			{
				GameNetwork.BeginModuleEventAsServer(base.Peer);
				GameNetwork.WriteMessage(new GoldGain(new List<KeyValuePair<ushort, int>>
				{
					new KeyValuePair<ushort, int>(2048, baseAmount)
				}));
				GameNetwork.EndModuleEventAsServer();
			}
			return baseAmount;
		}

		// Token: 0x040015BF RID: 5567
		private bool _survivedLastRound;
	}
}
