using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby
{
	// Token: 0x02000177 RID: 375
	public class MultiplayerLocalDataManager
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00010F25 File Offset: 0x0000F125
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x00010F2C File Offset: 0x0000F12C
		public static MultiplayerLocalDataManager Instance { get; private set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x00010F34 File Offset: 0x0000F134
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x00010F3C File Offset: 0x0000F13C
		public TauntSlotDataContainer TauntSlotData { get; private set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00010F45 File Offset: 0x0000F145
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x00010F4D File Offset: 0x0000F14D
		public MatchHistoryDataContainer MatchHistory { get; private set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00010F56 File Offset: 0x0000F156
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00010F5E File Offset: 0x0000F15E
		public FavoriteServerDataContainer FavoriteServers { get; private set; }

		// Token: 0x06000A5D RID: 2653 RVA: 0x00010F67 File Offset: 0x0000F167
		private MultiplayerLocalDataManager()
		{
			this.TauntSlotData = new TauntSlotDataContainer();
			this.MatchHistory = new MatchHistoryDataContainer();
			this.FavoriteServers = new FavoriteServerDataContainer();
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00010F90 File Offset: 0x0000F190
		public static void InitializeManager()
		{
			if (MultiplayerLocalDataManager.Instance != null)
			{
				Debug.FailedAssert("Multiplayer local data manager is already initialized", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "InitializeManager", 34);
				return;
			}
			MultiplayerLocalDataManager.Instance = new MultiplayerLocalDataManager();
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00010FBA File Offset: 0x0000F1BA
		public static void FinalizeManager()
		{
			if (MultiplayerLocalDataManager.Instance == null)
			{
				Debug.FailedAssert("Multiplayer local data manager is not initialized", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "FinalizeManager", 45);
				return;
			}
			MultiplayerLocalDataManager.Instance.WaitForAsyncOperations();
			MultiplayerLocalDataManager.Instance = null;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00010FEC File Offset: 0x0000F1EC
		public async void Tick(float dt)
		{
			if (!this._isBusy)
			{
				this._isBusy = true;
				await this.TauntSlotData.Tick(dt);
				await this.MatchHistory.Tick(dt);
				await this.FavoriteServers.Tick(dt);
				this._isBusy = false;
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001102D File Offset: 0x0000F22D
		private void WaitForAsyncOperations()
		{
			while (this._isBusy)
			{
			}
		}

		// Token: 0x040004FD RID: 1277
		internal const float FileUpdateIntervalInSeconds = 2f;

		// Token: 0x04000501 RID: 1281
		private bool _isBusy;
	}
}
