using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000375 RID: 885
	public class MBInitialScreenBase : ScreenBase, IGameStateListener
	{
		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x000C9BB0 File Offset: 0x000C7DB0
		// (set) Token: 0x060030A2 RID: 12450 RVA: 0x000C9BB8 File Offset: 0x000C7DB8
		private protected InitialState _state { protected get; private set; }

		// Token: 0x060030A3 RID: 12451 RVA: 0x000C9BC1 File Offset: 0x000C7DC1
		public MBInitialScreenBase(InitialState state)
		{
			this._state = state;
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000C9BD0 File Offset: 0x000C7DD0
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000C9BD2 File Offset: 0x000C7DD2
		void IGameStateListener.OnDeactivate()
		{
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000C9BD4 File Offset: 0x000C7DD4
		void IGameStateListener.OnInitialize()
		{
			this._state.OnInitialMenuOptionInvoked += this.OnExecutedInitialStateOption;
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000C9BED File Offset: 0x000C7DED
		void IGameStateListener.OnFinalize()
		{
			this._state.OnInitialMenuOptionInvoked -= this.OnExecutedInitialStateOption;
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000C9C06 File Offset: 0x000C7E06
		private void OnExecutedInitialStateOption(InitialStateOption target)
		{
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000C9C08 File Offset: 0x000C7E08
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._sceneLayer = new SceneLayer("SceneLayer", true, true);
			base.AddLayer(this._sceneLayer);
			this._sceneLayer.SceneView.SetResolutionScaling(true);
			this._sceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._camera = Camera.CreateCamera();
			Common.MemoryCleanupGC(false);
			if (Game.Current != null)
			{
				Game.Current.Destroy();
			}
			MBMusicManager.Initialize();
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000C9C8B File Offset: 0x000C7E8B
		protected override void OnFinalize()
		{
			this._camera = null;
			this._sceneLayer = null;
			this._cameraAnimationEntity = null;
			this._scene = null;
			base.OnFinalize();
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x000C9CB0 File Offset: 0x000C7EB0
		protected sealed override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (this._buttonInvokeMessage)
			{
				this._buttonInvokeMessage = false;
				Module.CurrentModule.ExecuteInitialStateOptionWithId(this._buttonToInvoke);
			}
			if (this._sceneLayer == null)
			{
				Console.WriteLine("InitialScreen::OnFrameTick scene view null");
			}
			if (this._scene == null)
			{
				return;
			}
			if (this._sceneLayer != null && this._sceneLayer.SceneView.ReadyToRender())
			{
				if (this._frameCountSinceReadyToRender > 8)
				{
					Utilities.DisableGlobalLoadingWindow();
					LoadingWindow.DisableGlobalLoadingWindow();
				}
				else
				{
					this._frameCountSinceReadyToRender++;
				}
			}
			if (this._sceneLayer != null)
			{
				this._sceneLayer.SetCamera(this._camera);
			}
			SoundManager.SetListenerFrame(this._camera.Frame);
			this._scene.Tick(dt);
			if (Input.IsKeyDown(InputKey.LeftControl) && Input.IsKeyReleased(InputKey.E))
			{
				MBInitialScreenBase.OnEditModeEnterPress();
			}
			if (ScreenManager.TopScreen == this)
			{
				this.OnInitialScreenTick(dt);
			}
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000C9D9C File Offset: 0x000C7F9C
		protected virtual void OnInitialScreenTick(float dt)
		{
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000C9D9E File Offset: 0x000C7F9E
		protected override void OnActivate()
		{
			base.OnActivate();
			if (Utilities.renderingActive)
			{
				this.RefreshScene();
			}
			this._frameCountSinceReadyToRender = 0;
			if (NativeConfig.DoLocalizationCheckAtStartup)
			{
				LocalizedTextManager.CheckValidity(new List<string>());
			}
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000C9DCC File Offset: 0x000C7FCC
		private void RefreshScene()
		{
			if (this._scene == null)
			{
				this._scene = Scene.CreateNewScene(true, false, DecalAtlasGroup.All, "mono_renderscene");
				this._scene.SetName("MBInitialScreenBase");
				this._scene.SetPlaySoundEventsAfterReadyToRender(true);
				SceneInitializationData sceneInitializationData = new SceneInitializationData(true);
				this._scene.Read("main_menu_a", ref sceneInitializationData, "");
				for (int i = 0; i < 40; i++)
				{
					this._scene.Tick(0.1f);
				}
				Vec3 vec = default(Vec3);
				this._scene.FindEntityWithTag("camera_instance").GetCameraParamsFromCameraScript(this._camera, ref vec);
			}
			SoundManager.SetListenerFrame(this._camera.Frame);
			if (this._sceneLayer != null)
			{
				this._sceneLayer.SetScene(this._scene);
				this._sceneLayer.SceneView.SetEnable(true);
				this._sceneLayer.SceneView.SetSceneUsesShadows(true);
			}
			this._cameraAnimationEntity = GameEntity.CreateEmpty(this._scene, true);
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000C9ED7 File Offset: 0x000C80D7
		private void OnSceneEditorWindowOpen()
		{
			GameStateManager.Current.CleanAndPushState(GameStateManager.Current.CreateState<EditorState>(), 0);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000C9EEE File Offset: 0x000C80EE
		protected override void OnDeactivate()
		{
			this._sceneLayer.SceneView.SetEnable(false);
			this._sceneLayer.SceneView.ClearAll(true, true);
			this._scene.ManualInvalidate();
			this._scene = null;
			base.OnDeactivate();
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000C9F2B File Offset: 0x000C812B
		protected override void OnPause()
		{
			LoadingWindow.DisableGlobalLoadingWindow();
			base.OnPause();
			if (this._scene != null)
			{
				this._scene.FinishSceneSounds();
			}
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000C9F51 File Offset: 0x000C8151
		protected override void OnResume()
		{
			base.OnResume();
			if (this._scene != null)
			{
				int frameCountSinceReadyToRender = this._frameCountSinceReadyToRender;
			}
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000C9F70 File Offset: 0x000C8170
		public static void DoExitButtonAction()
		{
			MBAPI.IMBScreen.OnExitButtonClick();
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000C9F7C File Offset: 0x000C817C
		public bool StartedRendering()
		{
			return this._sceneLayer.SceneView.ReadyToRender();
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000C9F8E File Offset: 0x000C818E
		public static void OnEditModeEnterPress()
		{
			MBAPI.IMBScreen.OnEditModeEnterPress();
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000C9F9A File Offset: 0x000C819A
		public static void OnEditModeEnterRelease()
		{
			MBAPI.IMBScreen.OnEditModeEnterRelease();
		}

		// Token: 0x040014BC RID: 5308
		private Camera _camera;

		// Token: 0x040014BD RID: 5309
		protected SceneLayer _sceneLayer;

		// Token: 0x040014BE RID: 5310
		private int _frameCountSinceReadyToRender;

		// Token: 0x040014BF RID: 5311
		private const int _numOfFramesToWaitAfterReadyToRender = 8;

		// Token: 0x040014C0 RID: 5312
		private GameEntity _cameraAnimationEntity;

		// Token: 0x040014C1 RID: 5313
		private Scene _scene;

		// Token: 0x040014C2 RID: 5314
		private bool _buttonInvokeMessage;

		// Token: 0x040014C3 RID: 5315
		private string _buttonToInvoke;
	}
}
