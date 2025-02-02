using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003C9 RID: 969
	public abstract class BattleInitializationModel : GameModel
	{
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600336E RID: 13166 RVA: 0x000D57C1 File Offset: 0x000D39C1
		// (set) Token: 0x0600336F RID: 13167 RVA: 0x000D57C9 File Offset: 0x000D39C9
		public bool BypassPlayerDeployment { get; private set; }

		// Token: 0x06003370 RID: 13168
		public abstract List<FormationClass> GetAllAvailableTroopTypes();

		// Token: 0x06003371 RID: 13169
		protected abstract bool CanPlayerSideDeployWithOrderOfBattleAux();

		// Token: 0x06003372 RID: 13170 RVA: 0x000D57D2 File Offset: 0x000D39D2
		public bool CanPlayerSideDeployWithOrderOfBattle()
		{
			if (!this._isCanPlayerSideDeployWithOOBCached)
			{
				this._cachedCanPlayerSideDeployWithOOB = (!this.BypassPlayerDeployment && this.CanPlayerSideDeployWithOrderOfBattleAux());
				this._isCanPlayerSideDeployWithOOBCached = true;
			}
			return this._cachedCanPlayerSideDeployWithOOB;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000D5800 File Offset: 0x000D3A00
		public void InitializeModel()
		{
			this._isCanPlayerSideDeployWithOOBCached = false;
			this._isInitialized = true;
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000D5810 File Offset: 0x000D3A10
		public void FinalizeModel()
		{
			this._isInitialized = false;
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000D5819 File Offset: 0x000D3A19
		public void SetBypassPlayerDeployment(bool value)
		{
			this.BypassPlayerDeployment = value;
		}

		// Token: 0x0400164B RID: 5707
		public const int MinimumTroopCountForPlayerDeployment = 20;

		// Token: 0x0400164D RID: 5709
		private bool _cachedCanPlayerSideDeployWithOOB;

		// Token: 0x0400164E RID: 5710
		private bool _isCanPlayerSideDeployWithOOBCached;

		// Token: 0x0400164F RID: 5711
		private bool _isInitialized;
	}
}
