using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission
{
	// Token: 0x02000024 RID: 36
	[DefaultView]
	public class MissionGauntletCategoryLoadManager : MissionView, IMissionListener
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00008D1F File Offset: 0x00006F1F
		private ITwoDimensionResourceContext _resourceContext
		{
			get
			{
				return UIResourceManager.ResourceContext;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00008D26 File Offset: 0x00006F26
		private ResourceDepot _resourceDepot
		{
			get
			{
				return UIResourceManager.UIResourceDepot;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00008D2D File Offset: 0x00006F2D
		private SpriteData _spriteData
		{
			get
			{
				return UIResourceManager.SpriteData;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00008D34 File Offset: 0x00006F34
		public override void AfterStart()
		{
			base.AfterStart();
			if (this._fullBackgroundCategory == null)
			{
				this._fullBackgroundCategory = this._spriteData.SpriteCategories["ui_fullbackgrounds"];
			}
			if (this._backgroundCategory == null)
			{
				this._backgroundCategory = this._spriteData.SpriteCategories["ui_backgrounds"];
			}
			if (this._fullscreensCategory == null)
			{
				this._fullscreensCategory = this._spriteData.SpriteCategories["ui_fullscreens"];
			}
			if (this._encyclopediaCategory == null)
			{
				this._encyclopediaCategory = this._spriteData.SpriteCategories["ui_encyclopedia"];
			}
			if (this._mapBarCategory == null && this._spriteData.SpriteCategories.ContainsKey("ui_mapbar") && this._spriteData.SpriteCategories["ui_mapbar"].IsLoaded)
			{
				this._mapBarCategory = this._spriteData.SpriteCategories["ui_mapbar"];
			}
			if (this._optionsView == null)
			{
				this._optionsView = base.Mission.GetMissionBehavior<MissionGauntletOptionsUIHandler>();
				base.Mission.AddListener(this);
			}
			this.HandleCategoryLoadingUnloading();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00008E54 File Offset: 0x00007054
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			this._optionsView = null;
			base.Mission.RemoveListener(this);
			this.LoadUnloadAllCategories(true);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008E76 File Offset: 0x00007076
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			this.HandleCategoryLoadingUnloading();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008E88 File Offset: 0x00007088
		private void HandleCategoryLoadingUnloading()
		{
			bool load = true;
			if (base.Mission != null)
			{
				load = this.IsBackgroundsUsedInMission(base.Mission);
			}
			this.LoadUnloadAllCategories(load);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008EB4 File Offset: 0x000070B4
		private void LoadUnloadAllCategories(bool load)
		{
			if (load)
			{
				if (!this._fullBackgroundCategory.IsLoaded)
				{
					this._fullBackgroundCategory.Load(this._resourceContext, this._resourceDepot);
				}
				if (!this._backgroundCategory.IsLoaded)
				{
					this._backgroundCategory.Load(this._resourceContext, this._resourceDepot);
				}
				if (!this._fullscreensCategory.IsLoaded)
				{
					this._fullscreensCategory.Load(this._resourceContext, this._resourceDepot);
				}
				if (!this._encyclopediaCategory.IsLoaded)
				{
					this._encyclopediaCategory.Load(this._resourceContext, this._resourceDepot);
				}
				SpriteCategory mapBarCategory = this._mapBarCategory;
				if (mapBarCategory != null && !mapBarCategory.IsLoaded)
				{
					this._mapBarCategory.Load(this._resourceContext, this._resourceDepot);
					return;
				}
			}
			else
			{
				if (this._fullBackgroundCategory.IsLoaded)
				{
					this._fullBackgroundCategory.Unload();
				}
				if (this._backgroundCategory.IsLoaded)
				{
					this._backgroundCategory.Unload();
				}
				if (this._fullscreensCategory.IsLoaded && !this._optionsView.IsEnabled)
				{
					this._fullscreensCategory.Unload();
				}
				if (this._encyclopediaCategory.IsLoaded)
				{
					Mission mission = base.Mission;
					if (mission == null || mission.Mode != MissionMode.Conversation)
					{
						this._encyclopediaCategory.Unload();
					}
				}
				SpriteCategory mapBarCategory2 = this._mapBarCategory;
				if (mapBarCategory2 != null && mapBarCategory2.IsLoaded)
				{
					this._mapBarCategory.Unload();
				}
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000902F File Offset: 0x0000722F
		private bool IsBackgroundsUsedInMission(Mission mission)
		{
			return mission.IsInventoryAccessAllowed || mission.IsCharacterWindowAccessAllowed || mission.IsClanWindowAccessAllowed || mission.IsKingdomWindowAccessAllowed || mission.IsQuestScreenAccessAllowed || mission.IsPartyWindowAccessAllowed || mission.IsEncyclopediaWindowAccessAllowed;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00009069 File Offset: 0x00007269
		void IMissionListener.OnEquipItemsFromSpawnEquipmentBegin(Agent agent, Agent.CreationType creationType)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000906B File Offset: 0x0000726B
		void IMissionListener.OnEquipItemsFromSpawnEquipment(Agent agent, Agent.CreationType creationType)
		{
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000906D File Offset: 0x0000726D
		void IMissionListener.OnEndMission()
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000906F File Offset: 0x0000726F
		void IMissionListener.OnMissionModeChange(MissionMode oldMissionMode, bool atStart)
		{
			this.HandleCategoryLoadingUnloading();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00009077 File Offset: 0x00007277
		void IMissionListener.OnConversationCharacterChanged()
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00009079 File Offset: 0x00007279
		void IMissionListener.OnResetMission()
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000907B File Offset: 0x0000727B
		void IMissionListener.OnInitialDeploymentPlanMade(BattleSideEnum battleSide, bool isFirstPlan)
		{
		}

		// Token: 0x040000CE RID: 206
		private SpriteCategory _fullBackgroundCategory;

		// Token: 0x040000CF RID: 207
		private SpriteCategory _backgroundCategory;

		// Token: 0x040000D0 RID: 208
		private SpriteCategory _fullscreensCategory;

		// Token: 0x040000D1 RID: 209
		private SpriteCategory _mapBarCategory;

		// Token: 0x040000D2 RID: 210
		private SpriteCategory _encyclopediaCategory;

		// Token: 0x040000D3 RID: 211
		private MissionGauntletOptionsUIHandler _optionsView;
	}
}
