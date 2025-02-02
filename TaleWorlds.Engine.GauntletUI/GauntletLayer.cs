using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.GauntletUI.PrefabSystem;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.Engine.GauntletUI
{
	// Token: 0x02000004 RID: 4
	public class GauntletLayer : ScreenLayer
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020DC File Offset: 0x000002DC
		public GauntletLayer(int localOrder, string categoryId = "GauntletLayer", bool shouldClear = false) : base(localOrder, categoryId)
		{
			this.MoviesAndDataSources = new List<Tuple<IGauntletMovie, ViewModel>>();
			this.WidgetFactory = UIResourceManager.WidgetFactory;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this.TwoDimensionView = TwoDimensionView.CreateTwoDimension();
			if (shouldClear)
			{
				this.TwoDimensionView.SetClearColor(255U);
				this.TwoDimensionView.SetRenderOption(View.ViewRenderOptions.ClearColor, true);
			}
			this.TwoDimensionEnginePlatform = new TwoDimensionEnginePlatform(this.TwoDimensionView);
			TwoDimensionContext twoDimensionContext = new TwoDimensionContext(this.TwoDimensionEnginePlatform, UIResourceManager.ResourceContext, uiresourceDepot);
			this.EngineInputService = new EngineInputService(base.Input);
			this.UIContext = new UIContext(twoDimensionContext, base.Input, this.EngineInputService, UIResourceManager.SpriteData, UIResourceManager.FontFactory, UIResourceManager.BrushFactory);
			this.UIContext.ScaleModifier = base.Scale;
			this.UIContext.Initialize();
			this.GamepadNavigationContext = new GauntletGamepadNavigationContext(new Func<Vector2, bool>(this.GetIsBlockedAtPosition), new Func<int>(this.GetLastScreenOrder), new Func<bool>(this.GetIsAvailableForGamepadNavigation));
			this.UIContext.InitializeGamepadNavigation(this.GamepadNavigationContext);
			base.MouseEnabled = true;
			this.UIContext.EventManager.LoseFocus += this.EventManagerOnLoseFocus;
			this.UIContext.EventManager.GainFocus += this.EventManagerOnGainFocus;
			this.UIContext.EventManager.OnGetIsHitThisFrame = new Func<bool>(this.GetIsHitThisFrame);
			this.UIContext.EventManager.UsableArea = base.UsableArea;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002260 File Offset: 0x00000460
		private void EventManagerOnLoseFocus()
		{
			if (!base.IsFocusLayer)
			{
				ScreenManager.TryLoseFocus(this);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002270 File Offset: 0x00000470
		private void EventManagerOnGainFocus()
		{
			ScreenManager.TrySetFocus(this);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002278 File Offset: 0x00000478
		public IGauntletMovie LoadMovie(string movieName, ViewModel dataSource)
		{
			bool isUsingGeneratedPrefabs = UIConfig.GetIsUsingGeneratedPrefabs();
			bool isHotReloadEnabled = UIConfig.GetIsHotReloadEnabled();
			IGauntletMovie gauntletMovie = GauntletMovie.Load(this.UIContext, this.WidgetFactory, movieName, dataSource, !isUsingGeneratedPrefabs, isHotReloadEnabled);
			this.MoviesAndDataSources.Add(new Tuple<IGauntletMovie, ViewModel>(gauntletMovie, dataSource));
			return gauntletMovie;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C0 File Offset: 0x000004C0
		public void ReleaseMovie(IGauntletMovie movie)
		{
			Tuple<IGauntletMovie, ViewModel> item = this.MoviesAndDataSources.SingleOrDefault((Tuple<IGauntletMovie, ViewModel> t) => t.Item1 == movie);
			this.MoviesAndDataSources.Remove(item);
			movie.Release();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000230A File Offset: 0x0000050A
		protected override void OnActivate()
		{
			base.OnActivate();
			this.TwoDimensionView.SetEnable(true);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000231E File Offset: 0x0000051E
		protected override void OnDeactivate()
		{
			this.TwoDimensionView.Clear();
			this.TwoDimensionView.SetEnable(false);
			base.OnDeactivate();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002340 File Offset: 0x00000540
		protected override void Tick(float dt)
		{
			base.Tick(dt);
			this.TwoDimensionEnginePlatform.Reset();
			this.UIContext.Update(dt);
			foreach (Tuple<IGauntletMovie, ViewModel> tuple in this.MoviesAndDataSources)
			{
				tuple.Item1.Update();
			}
			base.ActiveCursor = (CursorType)this.UIContext.ActiveCursorOfContext;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023C4 File Offset: 0x000005C4
		protected override void LateTick(float dt)
		{
			base.LateTick(dt);
			this.TwoDimensionView.BeginFrame();
			this.UIContext.LateUpdate(dt);
			this.TwoDimensionView.EndFrame();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023EF File Offset: 0x000005EF
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.EngineInputService.UpdateInputDevices(base.KeyboardEnabled, base.MouseEnabled, base.GamepadEnabled);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002415 File Offset: 0x00000615
		protected override void Update(IReadOnlyList<int> lastKeysPressed)
		{
			Widget focusedWidget = this.UIContext.EventManager.FocusedWidget;
			if (focusedWidget == null)
			{
				return;
			}
			focusedWidget.HandleInput(lastKeysPressed);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002434 File Offset: 0x00000634
		protected override void OnFinalize()
		{
			foreach (Tuple<IGauntletMovie, ViewModel> tuple in this.MoviesAndDataSources)
			{
				tuple.Item1.Release();
			}
			this.UIContext.EventManager.LoseFocus -= this.EventManagerOnLoseFocus;
			this.UIContext.EventManager.GainFocus -= this.EventManagerOnGainFocus;
			this.UIContext.EventManager.OnGetIsHitThisFrame = null;
			this.UIContext.OnFinalize();
			base.OnFinalize();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024E4 File Offset: 0x000006E4
		protected override void RefreshGlobalOrder(ref int currentOrder)
		{
			this.TwoDimensionView.SetRenderOrder(currentOrder);
			currentOrder++;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024F9 File Offset: 0x000006F9
		public override void ProcessEvents()
		{
			base.ProcessEvents();
			this.UIContext.UpdateInput(base._usedInputs);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002514 File Offset: 0x00000714
		public override bool HitTest(Vector2 position)
		{
			foreach (Tuple<IGauntletMovie, ViewModel> tuple in this.MoviesAndDataSources)
			{
				if (this.UIContext.HitTest(tuple.Item1.RootWidget, position))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002580 File Offset: 0x00000780
		private bool GetIsBlockedAtPosition(Vector2 position)
		{
			return ScreenManager.IsLayerBlockedAtPosition(this, position);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000258C File Offset: 0x0000078C
		public override bool HitTest()
		{
			foreach (Tuple<IGauntletMovie, ViewModel> tuple in this.MoviesAndDataSources)
			{
				if (this.UIContext.HitTest(tuple.Item1.RootWidget))
				{
					return true;
				}
			}
			this.UIContext.EventManager.SetHoveredView(null);
			return false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002608 File Offset: 0x00000808
		public override bool FocusTest()
		{
			foreach (Tuple<IGauntletMovie, ViewModel> tuple in this.MoviesAndDataSources)
			{
				if (this.UIContext.FocusTest(tuple.Item1.RootWidget))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002674 File Offset: 0x00000874
		public override bool IsFocusedOnInput()
		{
			return this.UIContext.EventManager.FocusedWidget is EditableTextWidget;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000268E File Offset: 0x0000088E
		protected override void OnLoseFocus()
		{
			this.UIContext.EventManager.ClearFocus();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026A0 File Offset: 0x000008A0
		public override void OnOnScreenKeyboardDone(string inputText)
		{
			base.OnOnScreenKeyboardDone(inputText);
			this.UIContext.OnOnScreenkeyboardTextInputDone(inputText);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026B5 File Offset: 0x000008B5
		public override void OnOnScreenKeyboardCanceled()
		{
			base.OnOnScreenKeyboardCanceled();
			this.UIContext.OnOnScreenKeyboardCanceled();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026C8 File Offset: 0x000008C8
		public override void UpdateLayout()
		{
			base.UpdateLayout();
			this.UIContext.ScaleModifier = base.Scale;
			this.UIContext.EventManager.UsableArea = base.UsableArea;
			this.MoviesAndDataSources.ForEach(delegate(Tuple<IGauntletMovie, ViewModel> m)
			{
				m.Item2.RefreshValues();
			});
			this.MoviesAndDataSources.ForEach(delegate(Tuple<IGauntletMovie, ViewModel> m)
			{
				m.Item1.RefreshBindingWithChildren();
			});
			this.UIContext.EventManager.UpdateLayout();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002766 File Offset: 0x00000966
		public bool GetIsAvailableForGamepadNavigation()
		{
			return base.LastActiveState && base.IsActive && (base.MouseEnabled || base.GamepadEnabled) && (base.IsFocusLayer || (base.InputRestrictions.InputUsageMask & InputUsageMask.Mouse) > InputUsageMask.Invalid);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027A4 File Offset: 0x000009A4
		private bool GetIsHitThisFrame()
		{
			return base.IsHitThisFrame;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027AC File Offset: 0x000009AC
		private int GetLastScreenOrder()
		{
			return base.ScreenOrderInLastFrame;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000027B4 File Offset: 0x000009B4
		public override void DrawDebugInfo()
		{
			foreach (Tuple<IGauntletMovie, ViewModel> tuple in this.MoviesAndDataSources)
			{
				IGauntletMovie item = tuple.Item1;
				ViewModel item2 = tuple.Item2;
				Imgui.Text("Movie: " + item.MovieName);
				Imgui.Text("Data Source: " + (((item2 != null) ? item2.GetType().Name : null) ?? "No Datasource"));
			}
			base.DrawDebugInfo();
			Imgui.Text("Press 'Shift+F' to take widget hierarchy snapshot.");
			this.UIContext.DrawWidgetDebugInfo();
		}

		// Token: 0x04000006 RID: 6
		public readonly TwoDimensionView TwoDimensionView;

		// Token: 0x04000007 RID: 7
		public readonly UIContext UIContext;

		// Token: 0x04000008 RID: 8
		public readonly IGamepadNavigationContext GamepadNavigationContext;

		// Token: 0x04000009 RID: 9
		public readonly List<Tuple<IGauntletMovie, ViewModel>> MoviesAndDataSources;

		// Token: 0x0400000A RID: 10
		public readonly TwoDimensionEnginePlatform TwoDimensionEnginePlatform;

		// Token: 0x0400000B RID: 11
		public readonly EngineInputService EngineInputService;

		// Token: 0x0400000C RID: 12
		public readonly WidgetFactory WidgetFactory;
	}
}
