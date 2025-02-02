using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.PlatformService;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000376 RID: 886
	public class MBProfileSelectionScreenBase : ScreenBase, IGameStateListener
	{
		// Token: 0x060030B7 RID: 12471 RVA: 0x000C9FA6 File Offset: 0x000C81A6
		public MBProfileSelectionScreenBase(ProfileSelectionState state)
		{
			this._state = state;
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x000C9FB5 File Offset: 0x000C81B5
		protected sealed override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (ScreenManager.TopScreen == this)
			{
				this.OnProfileSelectionTick(dt);
			}
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x000C9FCD File Offset: 0x000C81CD
		protected virtual void OnProfileSelectionTick(float dt)
		{
		}

		// Token: 0x060030BA RID: 12474 RVA: 0x000C9FCF File Offset: 0x000C81CF
		protected void OnActivateProfileSelection()
		{
			PlatformServices.Instance.LoginUser();
		}

		// Token: 0x060030BB RID: 12475 RVA: 0x000C9FDB File Offset: 0x000C81DB
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x000C9FDD File Offset: 0x000C81DD
		void IGameStateListener.OnDeactivate()
		{
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x000C9FDF File Offset: 0x000C81DF
		void IGameStateListener.OnFinalize()
		{
		}

		// Token: 0x060030BE RID: 12478 RVA: 0x000C9FE1 File Offset: 0x000C81E1
		void IGameStateListener.OnInitialize()
		{
			Utilities.DisableGlobalLoadingWindow();
			LoadingWindow.DisableGlobalLoadingWindow();
		}

		// Token: 0x040014C5 RID: 5317
		private ProfileSelectionState _state;
	}
}
