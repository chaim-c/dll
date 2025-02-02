using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD.FormationMarker;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x02000032 RID: 50
	[OverrideView(typeof(MissionSiegeEngineMarkerView))]
	public class MissionGauntletSiegeEngineMarker : MissionGauntletBattleUIBase
	{
		// Token: 0x06000234 RID: 564 RVA: 0x0000D970 File Offset: 0x0000BB70
		protected override void OnCreateView()
		{
			this._dataSource = new MissionSiegeEngineMarkerVM(base.Mission, base.MissionScreen.CombatCamera);
			this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("SiegeEngineMarker", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
			this._orderHandler = base.Mission.GetMissionBehavior<MissionGauntletSingleplayerOrderUIHandler>();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D9EC File Offset: 0x0000BBEC
		public override void OnDeploymentFinished()
		{
			base.OnDeploymentFinished();
			this._siegeEngines = new List<SiegeWeapon>();
			using (List<MissionObject>.Enumerator enumerator = base.Mission.ActiveMissionObjects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SiegeWeapon siegeWeapon;
					if ((siegeWeapon = (enumerator.Current as SiegeWeapon)) != null && siegeWeapon.DestructionComponent != null && siegeWeapon.Side != BattleSideEnum.None)
					{
						this._siegeEngines.Add(siegeWeapon);
					}
				}
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000DA74 File Offset: 0x0000BC74
		protected override void OnDestroyView()
		{
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (base.IsViewActive)
			{
				if (!this._dataSource.IsInitialized && this._siegeEngines != null)
				{
					this._dataSource.InitializeWith(this._siegeEngines);
				}
				if (!this._orderHandler.IsBattleDeployment)
				{
					this._dataSource.IsEnabled = base.Input.IsGameKeyDown(5);
				}
				this._dataSource.Tick(dt);
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000DB12 File Offset: 0x0000BD12
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			if (base.IsViewActive)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 0f;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000DB37 File Offset: 0x0000BD37
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			if (base.IsViewActive)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 1f;
			}
		}

		// Token: 0x04000148 RID: 328
		private List<SiegeWeapon> _siegeEngines;

		// Token: 0x04000149 RID: 329
		private MissionSiegeEngineMarkerVM _dataSource;

		// Token: 0x0400014A RID: 330
		private GauntletLayer _gauntletLayer;

		// Token: 0x0400014B RID: 331
		private MissionGauntletSingleplayerOrderUIHandler _orderHandler;
	}
}
