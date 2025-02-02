using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.Overlay
{
	// Token: 0x02000104 RID: 260
	public class OverlayPopupWidget : Widget
	{
		// Token: 0x06000DB9 RID: 3513 RVA: 0x0002610B File Offset: 0x0002430B
		public OverlayPopupWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00026114 File Offset: 0x00024314
		public void SetCurrentCharacter(GameMenuPartyItemButtonWidget item)
		{
			this.NameTextWidget.Text = item.Name;
			this.DescriptionTextWidget.Text = item.Description;
			this.LocationTextWidget.Text = item.Location;
			this.PowerTextWidget.Text = item.Power;
			if (item.CurrentCharacterImageWidget != null)
			{
				this.CurrentCharacterImageWidget.ImageId = item.CurrentCharacterImageWidget.ImageId;
				this.CurrentCharacterImageWidget.ImageTypeCode = item.CurrentCharacterImageWidget.ImageTypeCode;
				this.CurrentCharacterImageWidget.AdditionalArgs = item.CurrentCharacterImageWidget.AdditionalArgs;
			}
			if (!base.ParentWidget.IsVisible)
			{
				this.OpenPopup();
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000261C2 File Offset: 0x000243C2
		private void OpenPopup()
		{
			base.ParentWidget.IsVisible = true;
			base.EventFired("OnOpen", Array.Empty<object>());
			this._isOpen = true;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000261E7 File Offset: 0x000243E7
		private void ClosePopup()
		{
			base.ParentWidget.IsVisible = false;
			base.EventFired("OnClose", Array.Empty<object>());
			this._isOpen = false;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0002620C File Offset: 0x0002440C
		public void OnCloseButtonClick(Widget widget)
		{
			this.ClosePopup();
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00026214 File Offset: 0x00024414
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isOpen && !base.IsRecursivelyVisible())
			{
				this.ClosePopup();
			}
			else if (!this._isOpen && base.IsRecursivelyVisible())
			{
				this.OpenPopup();
			}
			if (!(base.EventManager.LatestMouseDownWidget is GameMenuPartyItemButtonWidget) && base.EventManager.LatestMouseDownWidget != this && base.EventManager.LatestMouseDownWidget != this._closeButton && base.ParentWidget.IsVisible && (!base.CheckIsMyChildRecursive(base.EventManager.LatestMouseDownWidget) || this.ActionButtonsList.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget)))
			{
				this.ClosePopup();
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000262C7 File Offset: 0x000244C7
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x000262CF File Offset: 0x000244CF
		[Editor(false)]
		public ImageIdentifierWidget CurrentCharacterImageWidget
		{
			get
			{
				return this._currentCharacterImageWidget;
			}
			set
			{
				if (this._currentCharacterImageWidget != value)
				{
					this._currentCharacterImageWidget = value;
					base.OnPropertyChanged<ImageIdentifierWidget>(value, "CurrentCharacterImageWidget");
				}
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x000262ED File Offset: 0x000244ED
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x000262F5 File Offset: 0x000244F5
		[Editor(false)]
		public TextWidget LocationTextWidget
		{
			get
			{
				return this._locationTextWidget;
			}
			set
			{
				if (this._locationTextWidget != value)
				{
					this._locationTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "LocationTextWidget");
				}
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00026313 File Offset: 0x00024513
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x0002631B File Offset: 0x0002451B
		[Editor(false)]
		public TextWidget NameTextWidget
		{
			get
			{
				return this._nameTextWidget;
			}
			set
			{
				if (this._nameTextWidget != value)
				{
					this._nameTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "NameTextWidget");
				}
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00026339 File Offset: 0x00024539
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x00026341 File Offset: 0x00024541
		[Editor(false)]
		public TextWidget PowerTextWidget
		{
			get
			{
				return this._powerTextWidget;
			}
			set
			{
				if (this._powerTextWidget != value)
				{
					this._powerTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "PowerTextWidget");
				}
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0002635F File Offset: 0x0002455F
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x00026367 File Offset: 0x00024567
		[Editor(false)]
		public TextWidget DescriptionTextWidget
		{
			get
			{
				return this._descriptionTextWidget;
			}
			set
			{
				if (this._descriptionTextWidget != value)
				{
					this._descriptionTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "DescriptionTextWidget");
				}
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00026385 File Offset: 0x00024585
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x0002638D File Offset: 0x0002458D
		[Editor(false)]
		public Widget RelationBackgroundWidget
		{
			get
			{
				return this._relationBackgroundWidget;
			}
			set
			{
				if (this._relationBackgroundWidget != value)
				{
					this._relationBackgroundWidget = value;
					base.OnPropertyChanged<Widget>(value, "RelationBackgroundWidget");
				}
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x000263AB File Offset: 0x000245AB
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x000263B3 File Offset: 0x000245B3
		[Editor(false)]
		public ListPanel ActionButtonsList
		{
			get
			{
				return this._actionButtonsList;
			}
			set
			{
				if (this._actionButtonsList != value)
				{
					this._actionButtonsList = value;
					base.OnPropertyChanged<ListPanel>(value, "ActionButtonsList");
				}
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x000263D1 File Offset: 0x000245D1
		// (set) Token: 0x06000DCE RID: 3534 RVA: 0x000263DC File Offset: 0x000245DC
		[Editor(false)]
		public ButtonWidget CloseButton
		{
			get
			{
				return this._closeButton;
			}
			set
			{
				if (this._closeButton != value)
				{
					ButtonWidget closeButton = this._closeButton;
					if (closeButton != null)
					{
						closeButton.ClickEventHandlers.Remove(new Action<Widget>(this.OnCloseButtonClick));
					}
					this._closeButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "CloseButton");
					ButtonWidget closeButton2 = this._closeButton;
					if (closeButton2 == null)
					{
						return;
					}
					closeButton2.ClickEventHandlers.Add(new Action<Widget>(this.OnCloseButtonClick));
				}
			}
		}

		// Token: 0x04000650 RID: 1616
		private bool _isOpen;

		// Token: 0x04000651 RID: 1617
		private ImageIdentifierWidget _currentCharacterImageWidget;

		// Token: 0x04000652 RID: 1618
		private TextWidget _locationTextWidget;

		// Token: 0x04000653 RID: 1619
		private TextWidget _descriptionTextWidget;

		// Token: 0x04000654 RID: 1620
		private TextWidget _powerTextWidget;

		// Token: 0x04000655 RID: 1621
		private TextWidget _nameTextWidget;

		// Token: 0x04000656 RID: 1622
		private Widget _relationBackgroundWidget;

		// Token: 0x04000657 RID: 1623
		private ButtonWidget _closeButton;

		// Token: 0x04000658 RID: 1624
		private ListPanel _actionButtonsList;
	}
}
