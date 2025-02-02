﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks;
using TaleWorlds.AchievementSystem;

namespace TaleWorlds.PlatformService.Steam
{
	// Token: 0x02000002 RID: 2
	public class SteamAchievementService : IAchievementService
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
		public SteamAchievementService(SteamPlatformServices steamPlatformServices)
		{
			this._platform = steamPlatformServices;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002062 File Offset: 0x00000262
		bool IAchievementService.SetStat(string name, int value)
		{
			return this.SetStat(name, value);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000206C File Offset: 0x0000026C
		Task<int> IAchievementService.GetStat(string name)
		{
			return this.GetStat(name);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002075 File Offset: 0x00000275
		Task<int[]> IAchievementService.GetStats(string[] names)
		{
			return this.GetStats(names);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000207E File Offset: 0x0000027E
		bool IAchievementService.IsInitializationCompleted()
		{
			return true;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002081 File Offset: 0x00000281
		public void Tick(float dt)
		{
			this.StoreStats();
			if (this._statsInvalidatedElapsed != -1f)
			{
				this._statsInvalidatedElapsed += dt;
			}
			this._statsStoredElapsed += dt;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020B4 File Offset: 0x000002B4
		private bool IsAchievementUnlocked(string id)
		{
			SteamUserStats.StoreStats();
			bool result;
			SteamUserStats.GetAchievement(id, out result);
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020D1 File Offset: 0x000002D1
		private void ClearAchievement(string name)
		{
			SteamUserStats.ClearAchievement(name);
			SteamUserStats.StoreStats();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020E0 File Offset: 0x000002E0
		public void Initialize()
		{
			SteamUserStats.RequestCurrentStats();
			this._userStatsReceivedT = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate(this.UserStatsReceived));
			this._userStatsStoredT = Callback<UserStatsStored_t>.Create(new Callback<UserStatsStored_t>.DispatchDelegate(this.UserStatsStored));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002116 File Offset: 0x00000316
		private void UserStatsReceived(UserStatsReceived_t userStatsReceivedT)
		{
			if ((ulong)SteamUtils.GetAppID().m_AppId == userStatsReceivedT.m_nGameID && userStatsReceivedT.m_eResult == EResult.k_EResultOK)
			{
				this._statsInitialized = true;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000213B File Offset: 0x0000033B
		private void UserStatsStored(UserStatsStored_t userStatsStoredT)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000213D File Offset: 0x0000033D
		internal bool SetStat(string name, int value)
		{
			if (!this._statsInitialized)
			{
				return false;
			}
			if (!SteamUserStats.SetStat(name, value))
			{
				return false;
			}
			this.InvalidateStats();
			return true;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000215C File Offset: 0x0000035C
		internal Task<int> GetStat(string name)
		{
			if (!this._statsInitialized)
			{
				return Task.FromResult<int>(-1);
			}
			int result = -1;
			SteamUserStats.GetStat(name, out result);
			return Task.FromResult<int>(result);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000218C File Offset: 0x0000038C
		internal Task<int[]> GetStats(string[] names)
		{
			if (!this._statsInitialized)
			{
				return Task.FromResult<int[]>(null);
			}
			List<int> list = new List<int>();
			foreach (string pchName in names)
			{
				int item = -1;
				SteamUserStats.GetStat(pchName, out item);
				list.Add(item);
			}
			return Task.FromResult<int[]>(list.ToArray());
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021DD File Offset: 0x000003DD
		private void InvalidateStats()
		{
			this._statsInvalidatedElapsed = 0f;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021EA File Offset: 0x000003EA
		private void StoreStats()
		{
			if (this._statsInvalidatedElapsed > 5f && this._statsStoredElapsed > 60f)
			{
				this._statsStoredElapsed = 0f;
				if (SteamUserStats.StoreStats())
				{
					this._statsInvalidatedElapsed = -1f;
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private SteamPlatformServices _platform;

		// Token: 0x04000002 RID: 2
		private float _statsInvalidatedElapsed = -1f;

		// Token: 0x04000003 RID: 3
		private float _statsStoredElapsed;

		// Token: 0x04000004 RID: 4
		private Callback<UserStatsReceived_t> _userStatsReceivedT;

		// Token: 0x04000005 RID: 5
		private Callback<UserStatsStored_t> _userStatsStoredT;

		// Token: 0x04000006 RID: 6
		private bool _statsInitialized;

		// Token: 0x04000007 RID: 7
		private const int StatInvalidationInterval = 5;

		// Token: 0x04000008 RID: 8
		private const int StoreStatsInterval = 60;
	}
}
