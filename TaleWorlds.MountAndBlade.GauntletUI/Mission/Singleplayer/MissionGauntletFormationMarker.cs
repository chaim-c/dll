using System;
using System.Collections.Generic;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD.FormationMarker;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x0200002D RID: 45
	[OverrideView(typeof(MissionFormationMarkerUIHandler))]
	public class MissionGauntletFormationMarker : MissionGauntletBattleUIBase
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000C46C File Offset: 0x0000A66C
		protected override void OnCreateView()
		{
			this._formationTargets = new List<CompassItemUpdateParams>();
			this._dataSource = new MissionFormationMarkerVM(base.Mission, base.MissionScreen.CombatCamera);
			this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("FormationMarker", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
			this._orderHandler = base.Mission.GetMissionBehavior<MissionGauntletSingleplayerOrderUIHandler>();
			this._formationTargetHandler = base.Mission.GetMissionBehavior<MissionFormationTargetSelectionHandler>();
			if (this._formationTargetHandler != null)
			{
				this._formationTargetHandler.OnFormationFocused += this.OnFormationFocusedFromHandler;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000C520 File Offset: 0x0000A720
		protected override void OnDestroyView()
		{
			if (this._formationTargetHandler != null)
			{
				this._formationTargetHandler.OnFormationFocused -= this.OnFormationFocusedFromHandler;
			}
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000C578 File Offset: 0x0000A778
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (base.IsViewActive)
			{
				if (!this._orderHandler.IsBattleDeployment)
				{
					MissionFormationMarkerVM dataSource = this._dataSource;
					bool isEnabled;
					if (!base.Input.IsGameKeyDown(5))
					{
						MissionGauntletSingleplayerOrderUIHandler orderHandler = this._orderHandler;
						isEnabled = (orderHandler != null && orderHandler.IsOrderMenuActive);
					}
					else
					{
						isEnabled = true;
					}
					dataSource.IsEnabled = isEnabled;
					if (this._formationTargetHandler != null)
					{
						this._dataSource.SetFocusedFormations(this._focusedFormationsCache);
					}
				}
				MissionFormationMarkerVM dataSource2 = this._dataSource;
				bool isFormationTargetRelevant;
				if (this._formationTargetHandler != null)
				{
					MissionGauntletSingleplayerOrderUIHandler orderHandler2 = this._orderHandler;
					isFormationTargetRelevant = (orderHandler2 != null && orderHandler2.IsOrderMenuActive);
				}
				else
				{
					isFormationTargetRelevant = false;
				}
				dataSource2.IsFormationTargetRelevant = isFormationTargetRelevant;
				this._dataSource.Tick(dt);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000C61F File Offset: 0x0000A81F
		private void OnFormationFocusedFromHandler(MBReadOnlyList<Formation> obj)
		{
			this._focusedFormationsCache = obj;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000C628 File Offset: 0x0000A828
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			if (base.IsViewActive)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 0f;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000C64D File Offset: 0x0000A84D
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			if (base.IsViewActive)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 1f;
			}
		}

		// Token: 0x0400011F RID: 287
		private MissionFormationMarkerVM _dataSource;

		// Token: 0x04000120 RID: 288
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000121 RID: 289
		private List<CompassItemUpdateParams> _formationTargets;

		// Token: 0x04000122 RID: 290
		private MBReadOnlyList<Formation> _focusedFormationsCache;

		// Token: 0x04000123 RID: 291
		private MissionGauntletSingleplayerOrderUIHandler _orderHandler;

		// Token: 0x04000124 RID: 292
		private MissionFormationTargetSelectionHandler _formationTargetHandler;
	}
}
