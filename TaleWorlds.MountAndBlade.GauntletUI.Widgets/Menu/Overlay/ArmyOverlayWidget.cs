using System;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.Overlay
{
	// Token: 0x02000101 RID: 257
	public class ArmyOverlayWidget : OverlayBaseWidget
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x000259B7 File Offset: 0x00023BB7
		public ArmyOverlayWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x000259C8 File Offset: 0x00023BC8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			int num = this._armyListGridWidget.Children.Count((Widget c) => c.IsVisible);
			if (num != this._armyItemCount)
			{
				this.Overlay.SetState("Reset");
				this._armyItemCount = num;
			}
			this.RefreshOverlayExtendState(!this._initialized);
			this.UpdateExtendButtonVisual();
			this._initialized = true;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00025A48 File Offset: 0x00023C48
		private void RefreshOverlayExtendState(bool forceSetPosition)
		{
			string str = this._isInfoBarExtended ? "MapExtended" : "MapNormal";
			string str2 = this._isOverlayExtended ? "OverlayExtended" : "OverlayNormal";
			if (str + str2 != this.Overlay.CurrentState)
			{
				if (!this._isOverlayExtended)
				{
					if (forceSetPosition)
					{
						VisualState visualState;
						this.Overlay.VisualDefinition.VisualStates.TryGetValue(str + str2, out visualState);
						this.Overlay.PositionYOffset = visualState.PositionYOffset;
					}
				}
				else
				{
					float y = this.ArmyListGridWidget.Size.Y;
					float positionYOffset;
					if (this._isInfoBarExtended)
					{
						VisualState visualState2;
						this.Overlay.VisualDefinition.VisualStates.TryGetValue("MapExtendedOverlayNormal", out visualState2);
						positionYOffset = visualState2.PositionYOffset - y * base._inverseScaleToUse;
					}
					else
					{
						VisualState visualState3;
						this.Overlay.VisualDefinition.VisualStates.TryGetValue("MapNormalOverlayNormal", out visualState3);
						positionYOffset = visualState3.PositionYOffset - y * base._inverseScaleToUse;
					}
					if (forceSetPosition)
					{
						this.Overlay.PositionYOffset = positionYOffset;
					}
					VisualState visualState4;
					this.Overlay.VisualDefinition.VisualStates.TryGetValue(str + str2, out visualState4);
					visualState4.PositionYOffset = positionYOffset;
				}
				this.Overlay.SetState(str + str2);
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00025BA4 File Offset: 0x00023DA4
		private void UpdateExtendButtonVisual()
		{
			foreach (Style style in this.ExtendButton.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].VerticalFlip = this._isOverlayExtended;
				}
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00025C1C File Offset: 0x00023E1C
		private void OnExtendButtonClick(Widget button)
		{
			this._isOverlayExtended = !this._isOverlayExtended;
			this.UpdateExtendButtonVisual();
			this.RefreshOverlayExtendState(false);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00025C3C File Offset: 0x00023E3C
		private void OnArmyListPageCountChanged()
		{
			if (this.PageControlWidget.PageCount == 1)
			{
				this.Overlay.PositionXOffset = 40f;
				this.ExtendButton.PositionXOffset = -40f;
				return;
			}
			this.Overlay.PositionXOffset = 0f;
			this.ExtendButton.PositionXOffset = 0f;
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x00025C98 File Offset: 0x00023E98
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x00025CA0 File Offset: 0x00023EA0
		[Editor(false)]
		public Widget Overlay
		{
			get
			{
				return this._overlay;
			}
			set
			{
				if (this._overlay != value)
				{
					this._overlay = value;
					base.OnPropertyChanged<Widget>(value, "Overlay");
				}
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00025CBE File Offset: 0x00023EBE
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x00025CC6 File Offset: 0x00023EC6
		[Editor(false)]
		public GridWidget ArmyListGridWidget
		{
			get
			{
				return this._armyListGridWidget;
			}
			set
			{
				if (this._armyListGridWidget != value)
				{
					this._armyListGridWidget = value;
					base.OnPropertyChanged<GridWidget>(value, "ArmyListGridWidget");
				}
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x00025CE4 File Offset: 0x00023EE4
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x00025CEC File Offset: 0x00023EEC
		[Editor(false)]
		public ButtonWidget ExtendButton
		{
			get
			{
				return this._extendButton;
			}
			set
			{
				if (this._extendButton != value)
				{
					this._extendButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "ExtendButton");
					if (this._extendButton != null)
					{
						this._extendButton.ClickEventHandlers.Add(new Action<Widget>(this.OnExtendButtonClick));
					}
				}
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00025D39 File Offset: 0x00023F39
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x00025D41 File Offset: 0x00023F41
		[Editor(false)]
		public bool IsInfoBarExtended
		{
			get
			{
				return this._isInfoBarExtended;
			}
			set
			{
				if (this._isInfoBarExtended != value)
				{
					this._isInfoBarExtended = value;
					base.OnPropertyChanged(value, "IsInfoBarExtended");
				}
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00025D5F File Offset: 0x00023F5F
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x00025D68 File Offset: 0x00023F68
		[Editor(false)]
		public ContainerPageControlWidget PageControlWidget
		{
			get
			{
				return this._pageControlWidget;
			}
			set
			{
				if (value != this._pageControlWidget)
				{
					if (this._pageControlWidget != null)
					{
						this._pageControlWidget.OnPageCountChanged -= this.OnArmyListPageCountChanged;
					}
					this._pageControlWidget = value;
					this._pageControlWidget.OnPageCountChanged += this.OnArmyListPageCountChanged;
					base.OnPropertyChanged<ContainerPageControlWidget>(value, "PageControlWidget");
				}
			}
		}

		// Token: 0x04000637 RID: 1591
		private bool _isOverlayExtended = true;

		// Token: 0x04000638 RID: 1592
		private int _armyItemCount;

		// Token: 0x04000639 RID: 1593
		private bool _initialized;

		// Token: 0x0400063A RID: 1594
		private Widget _overlay;

		// Token: 0x0400063B RID: 1595
		private bool _isInfoBarExtended;

		// Token: 0x0400063C RID: 1596
		private ButtonWidget _extendButton;

		// Token: 0x0400063D RID: 1597
		private GridWidget _armyListGridWidget;

		// Token: 0x0400063E RID: 1598
		private ContainerPageControlWidget _pageControlWidget;
	}
}
