using System;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission
{
	// Token: 0x02000021 RID: 33
	public abstract class MissionGauntletBattleUIBase : MissionView
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00008AA3 File Offset: 0x00006CA3
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00008AAB File Offset: 0x00006CAB
		private protected bool IsViewActive { protected get; private set; }

		// Token: 0x0600015B RID: 347
		protected abstract void OnCreateView();

		// Token: 0x0600015C RID: 348
		protected abstract void OnDestroyView();

		// Token: 0x0600015D RID: 349 RVA: 0x00008AB4 File Offset: 0x00006CB4
		private void OnEnableView()
		{
			this.OnCreateView();
			this.IsViewActive = true;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00008AC3 File Offset: 0x00006CC3
		private void OnDisableView()
		{
			this.OnDestroyView();
			this.IsViewActive = false;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008AD2 File Offset: 0x00006CD2
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			if (GameNetwork.IsMultiplayer)
			{
				this.OnEnableView();
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008AE8 File Offset: 0x00006CE8
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (!GameNetwork.IsMultiplayer && !MBCommon.IsPaused)
			{
				if (!this.IsViewActive && !BannerlordConfig.HideBattleUI)
				{
					this.OnEnableView();
					return;
				}
				if (this.IsViewActive && BannerlordConfig.HideBattleUI)
				{
					this.OnDisableView();
				}
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00008B35 File Offset: 0x00006D35
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			if (this.IsViewActive)
			{
				this.OnDisableView();
			}
		}
	}
}
