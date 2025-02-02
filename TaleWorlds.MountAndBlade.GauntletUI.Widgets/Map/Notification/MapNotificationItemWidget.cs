using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.Notification
{
	// Token: 0x02000111 RID: 273
	public class MapNotificationItemWidget : BrushWidget
	{
		// Token: 0x06000E58 RID: 3672 RVA: 0x00027BE7 File Offset: 0x00025DE7
		public MapNotificationItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00027BFC File Offset: 0x00025DFC
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._imageDetermined)
			{
				this.NotificationRingImageWidget.RegisterBrushStatesOfWidget();
				this.NotificationRingImageWidget.SetState(this.NotificationType);
				this._imageDetermined = true;
			}
			if (!this._sizeDetermined && this.NotificationDescriptionText != null)
			{
				this.DetermineSize();
			}
			bool flag = this._ringHoverBegan || this._extensionHoverBegan || this._removeHoverBegan;
			this._isExtended = flag;
			if (this.RemoveButtonVisualWidget != null)
			{
				this.RemoveButtonVisualWidget.IsVisible = (this._isExtended && base.EventManager.IsControllerActive);
			}
			this.NotificationRingWidget.IsEnabled = !this._removeInitiated;
			this.NotificationExtensionWidget.IsEnabled = !this._removeInitiated;
			this.RemoveNotificationButtonWidget.IsVisible = (flag && !this.IsInspectionForced);
			this.NotificationTextContainerWidget.IsVisible = flag;
			this.RefreshHorizontalPositioning(dt, flag);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00027CF4 File Offset: 0x00025EF4
		private void DetermineSize()
		{
			if (this.NotificationDescriptionText.Size.Y > base.Size.Y - 45f * base._scaleToUse)
			{
				this.NotificationExtensionWidget.Sprite = this.ExtendedWidthSprite;
				this.NotificationExtensionWidget.SuggestedWidth = this.ExtendedWidth;
			}
			else
			{
				this.NotificationExtensionWidget.Sprite = this.DefaultWidthSprite;
			}
			this._sizeDetermined = true;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00027D68 File Offset: 0x00025F68
		private void RefreshHorizontalPositioning(float dt, bool shouldExtend)
		{
			float num = this.NotificationExtensionWidget.Size.X - this.NotificationRingWidget.Size.X + 20f * base._scaleToUse;
			float num2 = -(this.NotificationExtensionWidget.Size.X - (this.NotificationExtensionWidget.Size.X - this.NotificationRingWidget.Size.X)) + 35f * base._scaleToUse;
			float end = shouldExtend ? num2 : num;
			this.NotificationExtensionWidget.ScaledPositionXOffset = this.LocalLerp(this.NotificationExtensionWidget.ScaledPositionXOffset, end, dt * 18f);
			float num3 = 0f;
			if (this._removeInitiated)
			{
				num3 = this.NotificationRingWidget.Size.X;
			}
			else if (!base.IsVisible)
			{
				num3 = this.NotificationRingWidget.Size.X;
			}
			base.ScaledPositionXOffset = this.LocalLerp(base.ScaledPositionXOffset, num3, dt * 18f);
			if (this._removeInitiated && MathF.Abs(base.ScaledPositionXOffset - num3) < 0.7f)
			{
				base.EventFired("OnRemove", Array.Empty<object>());
			}
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00027E91 File Offset: 0x00026091
		private void OnRemoveClick(Widget button)
		{
			if (!this.IsInspectionForced)
			{
				this._removeInitiated = true;
				base.EventFired("OnRemoveBegin", Array.Empty<object>());
			}
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00027EB2 File Offset: 0x000260B2
		private void OnInspectionClick(Widget button)
		{
			base.EventFired("OnInspection", Array.Empty<object>());
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00027EC4 File Offset: 0x000260C4
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x00027ECC File Offset: 0x000260CC
		[Editor(false)]
		public bool IsFocusItem
		{
			get
			{
				return this._isFocusItem;
			}
			set
			{
				if (value != this._isFocusItem)
				{
					this._isFocusItem = value;
					base.OnPropertyChanged(value, "IsFocusItem");
				}
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00027EEA File Offset: 0x000260EA
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x00027EF2 File Offset: 0x000260F2
		[Editor(false)]
		public float DefaultWidth
		{
			get
			{
				return this._defaultWidth;
			}
			set
			{
				if (value != this._defaultWidth)
				{
					this._defaultWidth = value;
					base.OnPropertyChanged(value, "DefaultWidth");
				}
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x00027F10 File Offset: 0x00026110
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x00027F18 File Offset: 0x00026118
		[Editor(false)]
		public float ExtendedWidth
		{
			get
			{
				return this._extendedWidth;
			}
			set
			{
				if (value != this._extendedWidth)
				{
					this._extendedWidth = value;
					base.OnPropertyChanged(value, "ExtendedWidth");
				}
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00027F36 File Offset: 0x00026136
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x00027F40 File Offset: 0x00026140
		[Editor(false)]
		public ButtonWidget RemoveNotificationButtonWidget
		{
			get
			{
				return this._removeNotificationButtonWidget;
			}
			set
			{
				if (this._removeNotificationButtonWidget != value)
				{
					this._removeNotificationButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "RemoveNotificationButtonWidget");
					value.ClickEventHandlers.Add(new Action<Widget>(this.OnRemoveClick));
					value.boolPropertyChanged += this.RemoveButtonWidgetPropertyChanged;
				}
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00027F92 File Offset: 0x00026192
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x00027F9A File Offset: 0x0002619A
		[Editor(false)]
		public Widget NotificationRingImageWidget
		{
			get
			{
				return this._notificationRingImageWidget;
			}
			set
			{
				if (this._notificationRingImageWidget != value)
				{
					this._notificationRingImageWidget = value;
					base.OnPropertyChanged<Widget>(value, "NotificationRingImageWidget");
				}
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00027FB8 File Offset: 0x000261B8
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x00027FC0 File Offset: 0x000261C0
		[Editor(false)]
		public bool IsInspectionForced
		{
			get
			{
				return this._isInspectionForced;
			}
			set
			{
				if (this._isInspectionForced != value)
				{
					this._isInspectionForced = value;
					base.OnPropertyChanged(value, "IsInspectionForced");
				}
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00027FDE File Offset: 0x000261DE
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x00027FE6 File Offset: 0x000261E6
		[Editor(false)]
		public string NotificationType
		{
			get
			{
				return this._notificationType;
			}
			set
			{
				if (this._notificationType != value)
				{
					this._notificationType = value;
					base.OnPropertyChanged<string>(value, "NotificationType");
				}
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00028009 File Offset: 0x00026209
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00028011 File Offset: 0x00026211
		[Editor(false)]
		public Sprite DefaultWidthSprite
		{
			get
			{
				return this._defaultWidthSprite;
			}
			set
			{
				if (this._defaultWidthSprite != value)
				{
					this._defaultWidthSprite = value;
					base.OnPropertyChanged<Sprite>(value, "DefaultWidthSprite");
				}
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0002802F File Offset: 0x0002622F
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x00028037 File Offset: 0x00026237
		[Editor(false)]
		public Sprite ExtendedWidthSprite
		{
			get
			{
				return this._extendedWidthSprite;
			}
			set
			{
				if (this._extendedWidthSprite != value)
				{
					this._extendedWidthSprite = value;
					base.OnPropertyChanged<Sprite>(value, "ExtendedWidthSprite");
				}
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00028055 File Offset: 0x00026255
		private void RemoveButtonWidgetPropertyChanged(PropertyOwnerObject widget, string propertyName, bool propertyValue)
		{
			if (propertyName == "IsHovered")
			{
				this._removeHoverBegan = propertyValue;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0002806B File Offset: 0x0002626B
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x00028074 File Offset: 0x00026274
		[Editor(false)]
		public Widget NotificationRingWidget
		{
			get
			{
				return this._notificationRingWidget;
			}
			set
			{
				if (this._notificationRingWidget != value)
				{
					this._notificationRingWidget = value;
					base.OnPropertyChanged<Widget>(value, "NotificationRingWidget");
					value.boolPropertyChanged += this.RingWidgetOnPropertyChanged;
					value.EventFire += this.InspectionWidgetsEventFire;
				}
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000280C1 File Offset: 0x000262C1
		private void RingWidgetOnPropertyChanged(PropertyOwnerObject widget, string propertyName, bool propertyValue)
		{
			if (propertyName == "IsHovered")
			{
				this._ringHoverBegan = propertyValue;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x000280D7 File Offset: 0x000262D7
		// (set) Token: 0x06000E75 RID: 3701 RVA: 0x000280E0 File Offset: 0x000262E0
		[Editor(false)]
		public Widget NotificationExtensionWidget
		{
			get
			{
				return this._notificationExtensionWidget;
			}
			set
			{
				if (this._notificationExtensionWidget != value)
				{
					this._notificationExtensionWidget = value;
					base.OnPropertyChanged<Widget>(value, "NotificationExtensionWidget");
					value.boolPropertyChanged += this.ExtensionWidgetOnPropertyChanged;
					value.EventFire += this.InspectionWidgetsEventFire;
				}
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x0002812D File Offset: 0x0002632D
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x00028135 File Offset: 0x00026335
		[Editor(false)]
		public Widget NotificationTextContainerWidget
		{
			get
			{
				return this._notificationTextContainerWidget;
			}
			set
			{
				if (this._notificationTextContainerWidget != value)
				{
					this._notificationTextContainerWidget = value;
					base.OnPropertyChanged<Widget>(value, "NotificationTextContainerWidget");
				}
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00028153 File Offset: 0x00026353
		// (set) Token: 0x06000E79 RID: 3705 RVA: 0x0002815B File Offset: 0x0002635B
		[Editor(false)]
		public RichTextWidget NotificationDescriptionText
		{
			get
			{
				return this._notificationDescriptionText;
			}
			set
			{
				if (this._notificationDescriptionText != value)
				{
					this._notificationDescriptionText = value;
					base.OnPropertyChanged<RichTextWidget>(value, "NotificationDescriptionText");
				}
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00028179 File Offset: 0x00026379
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x00028181 File Offset: 0x00026381
		[Editor(false)]
		public InputKeyVisualWidget RemoveButtonVisualWidget
		{
			get
			{
				return this._removeButtonVisualWidget;
			}
			set
			{
				if (this._removeButtonVisualWidget != value)
				{
					this._removeButtonVisualWidget = value;
					base.OnPropertyChanged<InputKeyVisualWidget>(value, "RemoveButtonVisualWidget");
				}
			}
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0002819F File Offset: 0x0002639F
		private void InspectionWidgetsEventFire(Widget widget, string eventName, object[] eventParameters)
		{
			if (eventName == "MouseUp")
			{
				this.OnInspectionClick(widget);
				return;
			}
			if (eventName == "MouseAlternateUp")
			{
				this.OnRemoveClick(this);
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000281CA File Offset: 0x000263CA
		private void ExtensionWidgetOnPropertyChanged(PropertyOwnerObject widget, string propertyName, bool propertyValue)
		{
			if (propertyName == "IsHovered")
			{
				this._extensionHoverBegan = propertyValue;
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x000281E0 File Offset: 0x000263E0
		private float LocalLerp(float start, float end, float delta)
		{
			if (MathF.Abs(start - end) > 1E-45f)
			{
				return (end - start) * delta + start;
			}
			return end;
		}

		// Token: 0x04000697 RID: 1687
		private bool _ringHoverBegan;

		// Token: 0x04000698 RID: 1688
		private bool _extensionHoverBegan;

		// Token: 0x04000699 RID: 1689
		private bool _removeHoverBegan;

		// Token: 0x0400069A RID: 1690
		private bool _removeInitiated;

		// Token: 0x0400069B RID: 1691
		private bool _imageDetermined;

		// Token: 0x0400069C RID: 1692
		private bool _sizeDetermined;

		// Token: 0x0400069D RID: 1693
		private bool _isExtended;

		// Token: 0x0400069E RID: 1694
		private bool _isFocusItem;

		// Token: 0x0400069F RID: 1695
		private float _defaultWidth;

		// Token: 0x040006A0 RID: 1696
		private float _extendedWidth;

		// Token: 0x040006A1 RID: 1697
		private bool _isInspectionForced;

		// Token: 0x040006A2 RID: 1698
		private string _notificationType = "Default";

		// Token: 0x040006A3 RID: 1699
		private Sprite _defaultWidthSprite;

		// Token: 0x040006A4 RID: 1700
		private Sprite _extendedWidthSprite;

		// Token: 0x040006A5 RID: 1701
		private Widget _notificationRingWidget;

		// Token: 0x040006A6 RID: 1702
		private Widget _notificationRingImageWidget;

		// Token: 0x040006A7 RID: 1703
		private Widget _notificationExtensionWidget;

		// Token: 0x040006A8 RID: 1704
		private Widget _notificationTextContainerWidget;

		// Token: 0x040006A9 RID: 1705
		private ButtonWidget _removeNotificationButtonWidget;

		// Token: 0x040006AA RID: 1706
		private RichTextWidget _notificationDescriptionText;

		// Token: 0x040006AB RID: 1707
		private InputKeyVisualWidget _removeButtonVisualWidget;
	}
}
