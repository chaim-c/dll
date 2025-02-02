using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tutorial
{
	// Token: 0x02000048 RID: 72
	public class TutorialPanelImageWidget : ImageWidget
	{
		// Token: 0x060003D0 RID: 976 RVA: 0x0000C624 File Offset: 0x0000A824
		public TutorialPanelImageWidget(UIContext context) : base(context)
		{
			base.UseGlobalTimeForAnimation = true;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000C634 File Offset: 0x0000A834
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._animState == TutorialPanelImageWidget.AnimState.Start)
			{
				this._tickCount++;
				if (this._tickCount > 20)
				{
					this._animState = TutorialPanelImageWidget.AnimState.Starting;
					return;
				}
			}
			else if (this._animState == TutorialPanelImageWidget.AnimState.Starting)
			{
				BrushListPanel tutorialPanel = this.TutorialPanel;
				if (tutorialPanel != null)
				{
					tutorialPanel.BrushRenderer.RestartAnimation();
				}
				this._animState = TutorialPanelImageWidget.AnimState.Playing;
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000C697 File Offset: 0x0000A897
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			this.Initialize();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		private void Initialize()
		{
			if (base.IsDisabled)
			{
				this.SetState("Disabled");
				this._animState = TutorialPanelImageWidget.AnimState.Idle;
				this._tickCount = 0;
			}
			else if (this._animState != TutorialPanelImageWidget.AnimState.Start)
			{
				this.SetState("Default");
				this._animState = TutorialPanelImageWidget.AnimState.Start;
				base.Context.TwoDimensionContext.PlaySound("panels/tutorial");
			}
			base.IsVisible = base.IsEnabled;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000C714 File Offset: 0x0000A914
		protected override void RefreshState()
		{
			base.RefreshState();
			this.Initialize();
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000C722 File Offset: 0x0000A922
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000C72A File Offset: 0x0000A92A
		[Editor(false)]
		public BrushListPanel TutorialPanel
		{
			get
			{
				return this._tutorialPanel;
			}
			set
			{
				if (this._tutorialPanel != value)
				{
					this._tutorialPanel = value;
					base.OnPropertyChanged<BrushListPanel>(value, "TutorialPanel");
					if (this._tutorialPanel != null)
					{
						this._tutorialPanel.UseGlobalTimeForAnimation = true;
					}
				}
			}
		}

		// Token: 0x0400019F RID: 415
		private TutorialPanelImageWidget.AnimState _animState;

		// Token: 0x040001A0 RID: 416
		private int _tickCount;

		// Token: 0x040001A1 RID: 417
		private BrushListPanel _tutorialPanel;

		// Token: 0x02000199 RID: 409
		public enum AnimState
		{
			// Token: 0x04000978 RID: 2424
			Idle,
			// Token: 0x04000979 RID: 2425
			Start,
			// Token: 0x0400097A RID: 2426
			Starting,
			// Token: 0x0400097B RID: 2427
			Playing
		}
	}
}
