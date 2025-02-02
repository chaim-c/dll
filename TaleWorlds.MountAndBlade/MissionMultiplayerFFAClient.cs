using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200029D RID: 669
	public class MissionMultiplayerFFAClient : MissionMultiplayerGameModeBaseClient
	{
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x000848D7 File Offset: 0x00082AD7
		public override bool IsGameModeUsingGold
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x000848DA File Offset: 0x00082ADA
		public override bool IsGameModeTactical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x000848DD File Offset: 0x00082ADD
		public override bool IsGameModeUsingRoundCountdown
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x000848E0 File Offset: 0x00082AE0
		public override MultiplayerGameType GameType
		{
			get
			{
				return MultiplayerGameType.FreeForAll;
			}
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x000848E3 File Offset: 0x00082AE3
		public override int GetGoldAmount()
		{
			return 0;
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000848E6 File Offset: 0x00082AE6
		public override void OnGoldAmountChangedForRepresentative(MissionRepresentativeBase representative, int goldAmount)
		{
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x000848E8 File Offset: 0x00082AE8
		public override void AfterStart()
		{
			base.Mission.SetMissionMode(MissionMode.Battle, true);
		}
	}
}
