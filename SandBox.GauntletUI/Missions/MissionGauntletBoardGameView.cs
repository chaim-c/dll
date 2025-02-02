using System;
using SandBox.BoardGames.MissionLogics;
using SandBox.ViewModelCollection.BoardGame;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI.Missions
{
	// Token: 0x02000015 RID: 21
	[OverrideView(typeof(BoardGameView))]
	public class MissionGauntletBoardGameView : MissionView, IBoardGameHandler
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00007A7D File Offset: 0x00005C7D
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00007A85 File Offset: 0x00005C85
		public MissionBoardGameLogic _missionBoardGameHandler { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00007A8E File Offset: 0x00005C8E
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00007A96 File Offset: 0x00005C96
		public Camera Camera { get; private set; }

		// Token: 0x060000CF RID: 207 RVA: 0x00007A9F File Offset: 0x00005C9F
		public MissionGauntletBoardGameView()
		{
			this.ViewOrderPriority = 2;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007AAE File Offset: 0x00005CAE
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			base.MissionScreen.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("BoardGameHotkeyCategory"));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007AD5 File Offset: 0x00005CD5
		public override void OnMissionScreenActivate()
		{
			base.OnMissionScreenActivate();
			this._missionBoardGameHandler = base.Mission.GetMissionBehavior<MissionBoardGameLogic>();
			if (this._missionBoardGameHandler != null)
			{
				this._missionBoardGameHandler.Handler = this;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00007B02 File Offset: 0x00005D02
		void IBoardGameHandler.Activate()
		{
			this._dataSource.Activate();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007B0F File Offset: 0x00005D0F
		void IBoardGameHandler.SwitchTurns()
		{
			BoardGameVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.SwitchTurns();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007B21 File Offset: 0x00005D21
		void IBoardGameHandler.DiceRoll(int roll)
		{
			BoardGameVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.DiceRoll(roll);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007B34 File Offset: 0x00005D34
		void IBoardGameHandler.Install()
		{
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._spriteCategory = spriteData.SpriteCategories["ui_boardgame"];
			this._spriteCategory.Load(resourceContext, uiresourceDepot);
			this._dataSource = new BoardGameVM();
			this._dataSource.SetRollDiceKey(HotKeyManager.GetCategory("BoardGameHotkeyCategory").GetHotKey("BoardGameRollDice"));
			this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "BoardGame", false);
			this._gauntletMovie = this._gauntletLayer.LoadMovie("BoardGame", this._dataSource);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._cameraHolder = base.Mission.Scene.FindEntityWithTag("camera_holder");
			this.CreateCamera();
			if (this._cameraHolder == null)
			{
				this._cameraHolder = base.Mission.Scene.FindEntityWithTag("camera_holder");
			}
			if (this.Camera == null)
			{
				this.CreateCamera();
			}
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._missionMouseVisibilityState = base.MissionScreen.SceneLayer.InputRestrictions.MouseVisibility;
			this._missionInputRestrictions = base.MissionScreen.SceneLayer.InputRestrictions.InputUsageMask;
			base.MissionScreen.SceneLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
			base.MissionScreen.SceneLayer.IsFocusLayer = true;
			base.MissionScreen.AddLayer(this._gauntletLayer);
			base.MissionScreen.SetLayerCategoriesStateAndDeactivateOthers(new string[]
			{
				"SceneLayer",
				"BoardGame"
			}, true);
			ScreenManager.TrySetFocus(base.MissionScreen.SceneLayer);
			this.SetStaticCamera();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007D04 File Offset: 0x00005F04
		void IBoardGameHandler.Uninstall()
		{
			if (this._dataSource != null)
			{
				this._dataSource.OnFinalize();
				this._dataSource = null;
			}
			this._gauntletLayer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(this._gauntletLayer);
			this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
			base.MissionScreen.SceneLayer.InputRestrictions.SetInputRestrictions(this._missionMouseVisibilityState, this._missionInputRestrictions);
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletMovie = null;
			this._gauntletLayer = null;
			this.Camera = null;
			this._cameraHolder = null;
			base.MissionScreen.CustomCamera = null;
			base.MissionScreen.SetLayerCategoriesStateAndToggleOthers(new string[]
			{
				"BoardGame"
			}, false);
			base.MissionScreen.SetLayerCategoriesState(new string[]
			{
				"SceneLayer"
			}, true);
			this._spriteCategory.Unload();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007DEC File Offset: 0x00005FEC
		private bool IsHotkeyPressedInAnyLayer(string hotkeyID)
		{
			SceneLayer sceneLayer = base.MissionScreen.SceneLayer;
			bool flag = sceneLayer != null && sceneLayer.Input.IsHotKeyPressed(hotkeyID);
			GauntletLayer gauntletLayer = this._gauntletLayer;
			bool flag2 = gauntletLayer != null && gauntletLayer.Input.IsHotKeyPressed(hotkeyID);
			return flag || flag2;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007E34 File Offset: 0x00006034
		private bool IsHotkeyDownInAnyLayer(string hotkeyID)
		{
			SceneLayer sceneLayer = base.MissionScreen.SceneLayer;
			bool flag = sceneLayer != null && sceneLayer.Input.IsHotKeyDown(hotkeyID);
			GauntletLayer gauntletLayer = this._gauntletLayer;
			bool flag2 = gauntletLayer != null && gauntletLayer.Input.IsHotKeyDown(hotkeyID);
			return flag || flag2;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007E7C File Offset: 0x0000607C
		private bool IsGameKeyReleasedInAnyLayer(string hotKeyID)
		{
			SceneLayer sceneLayer = base.MissionScreen.SceneLayer;
			bool flag = sceneLayer != null && sceneLayer.Input.IsHotKeyReleased(hotKeyID);
			GauntletLayer gauntletLayer = this._gauntletLayer;
			bool flag2 = gauntletLayer != null && gauntletLayer.Input.IsHotKeyReleased(hotKeyID);
			return flag || flag2;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00007EC4 File Offset: 0x000060C4
		private void CreateCamera()
		{
			this.Camera = Camera.CreateCamera();
			if (this._cameraHolder != null)
			{
				this.Camera.Entity = this._cameraHolder;
			}
			this.Camera.SetFovVertical(0.7853982f, 1.7777778f, 0.01f, 3000f);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00007F1C File Offset: 0x0000611C
		private void SetStaticCamera()
		{
			if (this._cameraHolder != null && this.Camera.Entity != null)
			{
				base.MissionScreen.CustomCamera = this.Camera;
				return;
			}
			Debug.FailedAssert("[DEBUG]Camera entities are null.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.GauntletUI\\Missions\\MissionGauntletBoardGameView.cs", "SetStaticCamera", 189);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007F78 File Offset: 0x00006178
		public override void OnMissionScreenTick(float dt)
		{
			MissionBoardGameLogic missionBoardGameHandler = this._missionBoardGameHandler;
			if (missionBoardGameHandler != null && missionBoardGameHandler.IsGameInProgress)
			{
				MissionScreen missionScreen = base.MissionScreen;
				if (missionScreen == null || !missionScreen.IsPhotoModeEnabled)
				{
					base.OnMissionScreenTick(dt);
					if (this._gauntletLayer != null && this._dataSource != null)
					{
						if (this.IsHotkeyPressedInAnyLayer("Exit"))
						{
							this._dataSource.ExecuteForfeit();
						}
						else if (this.IsHotkeyPressedInAnyLayer("BoardGameRollDice") && this._dataSource.IsGameUsingDice)
						{
							this._dataSource.ExecuteRoll();
						}
					}
					if (this._missionBoardGameHandler.Board != null)
					{
						Vec3 rayBegin;
						Vec3 rayEnd;
						base.MissionScreen.ScreenPointToWorldRay(base.Input.GetMousePositionRanged(), out rayBegin, out rayEnd);
						this._missionBoardGameHandler.Board.SetUserRay(rayBegin, rayEnd);
					}
					return;
				}
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008040 File Offset: 0x00006240
		public override void OnMissionScreenFinalize()
		{
			if (this._dataSource != null)
			{
				this._dataSource.OnFinalize();
				this._dataSource = null;
			}
			this._gauntletLayer = null;
			this._gauntletMovie = null;
			base.OnMissionScreenFinalize();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008070 File Offset: 0x00006270
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			if (this._gauntletLayer != null)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 0f;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00008095 File Offset: 0x00006295
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			if (this._gauntletLayer != null)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 1f;
			}
		}

		// Token: 0x04000058 RID: 88
		private BoardGameVM _dataSource;

		// Token: 0x04000059 RID: 89
		private GauntletLayer _gauntletLayer;

		// Token: 0x0400005A RID: 90
		private IGauntletMovie _gauntletMovie;

		// Token: 0x0400005D RID: 93
		private GameEntity _cameraHolder;

		// Token: 0x0400005E RID: 94
		private SpriteCategory _spriteCategory;

		// Token: 0x0400005F RID: 95
		private bool _missionMouseVisibilityState;

		// Token: 0x04000060 RID: 96
		private InputUsageMask _missionInputRestrictions;
	}
}
