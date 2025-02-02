using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.BodyGenerator
{
	// Token: 0x02000037 RID: 55
	[OverrideView(typeof(FaceGeneratorScreen))]
	public class GauntletBodyGeneratorScreen : ScreenBase, IFaceGeneratorScreen
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000113CB File Offset: 0x0000F5CB
		public IFaceGeneratorHandler Handler
		{
			get
			{
				return this._facegenLayer;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000113D4 File Offset: 0x0000F5D4
		public GauntletBodyGeneratorScreen(BasicCharacterObject character, bool openedFromMultiplayer, IFaceGeneratorCustomFilter filter)
		{
			this._facegenLayer = new BodyGeneratorView(new ControlCharacterCreationStage(this.OnExit), GameTexts.FindText("str_done", null), new ControlCharacterCreationStage(this.OnExit), GameTexts.FindText("str_cancel", null), character, openedFromMultiplayer, filter, null, null, null, null, null);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00011428 File Offset: 0x0000F628
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			this._facegenLayer.OnTick(dt);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0001143D File Offset: 0x0000F63D
		public void OnExit()
		{
			ScreenManager.PopScreen();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00011444 File Offset: 0x0000F644
		protected override void OnInitialize()
		{
			base.OnInitialize();
			Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
			base.AddLayer(this._facegenLayer.GauntletLayer);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0001146D File Offset: 0x0000F66D
		protected override void OnFinalize()
		{
			base.OnFinalize();
			if (LoadingWindow.GetGlobalLoadingWindowState())
			{
				LoadingWindow.DisableGlobalLoadingWindow();
			}
			Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00011491 File Offset: 0x0000F691
		protected override void OnActivate()
		{
			base.OnActivate();
			base.AddLayer(this._facegenLayer.SceneLayer);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000114AC File Offset: 0x0000F6AC
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			this._facegenLayer.OnFinalize();
			LoadingWindow.EnableGlobalLoadingWindow();
			MBInformationManager.HideInformations();
			Mission mission = Mission.Current;
			if (mission != null)
			{
				foreach (Agent agent in mission.Agents)
				{
					agent.EquipItemsFromSpawnEquipment(false);
					agent.UpdateAgentProperties();
				}
			}
		}

		// Token: 0x04000192 RID: 402
		private const int ViewOrderPriority = 15;

		// Token: 0x04000193 RID: 403
		private readonly BodyGeneratorView _facegenLayer;
	}
}
