using System;
using System.Threading;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Launcher.Library.UserDatas;
using TaleWorlds.TwoDimension;
using TaleWorlds.TwoDimension.Standalone;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x0200000A RID: 10
	public class StandaloneUIDomain : FrameworkDomain
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002CA5 File Offset: 0x00000EA5
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002CAD File Offset: 0x00000EAD
		public UserDataManager UserDataManager { get; private set; }

		// Token: 0x0600004D RID: 77 RVA: 0x00002CB6 File Offset: 0x00000EB6
		public StandaloneUIDomain(GraphicsForm graphicsForm, ResourceDepot resourceDepot)
		{
			this._graphicsForm = graphicsForm;
			this._resourceDepot = resourceDepot;
			this.UserDataManager = new UserDataManager();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public override void Update()
		{
			if (this._synchronizationContext == null)
			{
				this._synchronizationContext = new SingleThreadedSynchronizationContext();
				SynchronizationContext.SetSynchronizationContext(this._synchronizationContext);
			}
			if (!this._initialized)
			{
				GauntletGamepadNavigationManager.Initialize();
				this.UserDataManager.LoadUserData();
				Input.Initialize(new StandaloneInputManager(this._graphicsForm), null);
				this._graphicsForm.InitializeGraphicsContext(this._resourceDepot);
				this._graphicsContext = this._graphicsForm.GraphicsContext;
				TwoDimensionPlatform twoDimensionPlatform = new TwoDimensionPlatform(this._graphicsForm, true);
				this._twoDimensionContext = new TwoDimensionContext(twoDimensionPlatform, twoDimensionPlatform, this._resourceDepot);
				StandaloneInputService inputService = new StandaloneInputService(this._graphicsForm);
				InputContext inputContext = new InputContext();
				inputContext.MouseOnMe = true;
				inputContext.IsKeysAllowed = true;
				inputContext.IsMouseButtonAllowed = true;
				inputContext.IsMouseWheelAllowed = true;
				this._gauntletUIContext = new UIContext(this._twoDimensionContext, inputContext, inputService);
				this._gauntletUIContext.IsDynamicScaleEnabled = false;
				this._gauntletUIContext.Initialize();
				this._launcherUI = new LauncherUI(this.UserDataManager, this._gauntletUIContext, new Action(this.OnCloseRequest), new Action(this.OnMinimizeRequest));
				this._launcherUI.Initialize();
				this._initialized = true;
			}
			this._resourceDepot.CheckForChanges();
			this._synchronizationContext.Tick();
			bool mouseOverDragArea = this._launcherUI.CheckMouseOverWindowDragArea();
			this._graphicsForm.UpdateInput(mouseOverDragArea);
			this._graphicsForm.BeginFrame();
			Input.Update();
			this._graphicsForm.Update();
			this._gauntletUIContext.UpdateInput(InputType.MouseButton | InputType.MouseWheel | InputType.Key);
			this._gauntletUIContext.Update(0.016666668f);
			this._launcherUI.Update();
			this._gauntletUIContext.LateUpdate(0.016666668f);
			this._graphicsForm.PostRender();
			this._graphicsContext.SwapBuffers();
			if (this._shouldDestroy)
			{
				this.DestroyAux();
				this._shouldDestroy = false;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002EB5 File Offset: 0x000010B5
		public string AdditionalArgs
		{
			get
			{
				if (this._launcherUI == null)
				{
					return "";
				}
				return this._launcherUI.AdditionalArgs;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002ED0 File Offset: 0x000010D0
		public bool HasUnofficialModulesSelected
		{
			get
			{
				return this._launcherUI.HasUnofficialModulesSelected;
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EDD File Offset: 0x000010DD
		public override void Destroy()
		{
			this._shouldDestroy = true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EE8 File Offset: 0x000010E8
		private void DestroyAux()
		{
			GauntletGamepadNavigationManager instance = GauntletGamepadNavigationManager.Instance;
			if (instance != null)
			{
				instance.OnFinalize();
			}
			this._synchronizationContext = null;
			this._initialized = false;
			GraphicsContext graphicsContext = this._graphicsContext;
			if (graphicsContext != null)
			{
				graphicsContext.DestroyContext();
			}
			this._gauntletUIContext = null;
			this._launcherUI = null;
			GraphicsForm graphicsForm = this._graphicsForm;
			if (graphicsForm == null)
			{
				return;
			}
			graphicsForm.Destroy();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F42 File Offset: 0x00001142
		private void OnStartGameRequest()
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F44 File Offset: 0x00001144
		private void OnCloseRequest()
		{
			Environment.Exit(0);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F4C File Offset: 0x0000114C
		private void OnMinimizeRequest()
		{
			this._graphicsForm.MinimizeWindow();
		}

		// Token: 0x0400002C RID: 44
		private SingleThreadedSynchronizationContext _synchronizationContext;

		// Token: 0x0400002D RID: 45
		private bool _initialized;

		// Token: 0x0400002E RID: 46
		private bool _shouldDestroy;

		// Token: 0x0400002F RID: 47
		private GraphicsForm _graphicsForm;

		// Token: 0x04000030 RID: 48
		private GraphicsContext _graphicsContext;

		// Token: 0x04000031 RID: 49
		private UIContext _gauntletUIContext;

		// Token: 0x04000032 RID: 50
		private TwoDimensionContext _twoDimensionContext;

		// Token: 0x04000033 RID: 51
		private LauncherUI _launcherUI;

		// Token: 0x04000034 RID: 52
		private readonly ResourceDepot _resourceDepot;
	}
}
