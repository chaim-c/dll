using System;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Nameplate
{
	// Token: 0x0200007A RID: 122
	public class SettlementNameplateWidget : Widget, IComparable<SettlementNameplateWidget>
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x00014102 File Offset: 0x00012302
		public SettlementNameplateWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001412F File Offset: 0x0001232F
		private float _screenEdgeAlphaTarget
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00014136 File Offset: 0x00012336
		private float _normalNeutralAlphaTarget
		{
			get
			{
				return 0.35f;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0001413D File Offset: 0x0001233D
		private float _normalAllyAlphaTarget
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00014144 File Offset: 0x00012344
		private float _normalEnemyAlphaTarget
		{
			get
			{
				return 0.35f;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001414B File Offset: 0x0001234B
		private float _trackedAlphaTarget
		{
			get
			{
				return 0.8f;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00014152 File Offset: 0x00012352
		private float _trackedColorFactorTarget
		{
			get
			{
				return 1.3f;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00014159 File Offset: 0x00012359
		private float _normalColorFactorTarget
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00014160 File Offset: 0x00012360
		protected override void OnParallelUpdate(float dt)
		{
			base.OnParallelUpdate(dt);
			SettlementNameplateItemWidget currentNameplate = this._currentNameplate;
			if (currentNameplate != null)
			{
				currentNameplate.ParallelUpdate(dt);
			}
			if (currentNameplate != null && this._cachedItemSize != currentNameplate.Size)
			{
				this._cachedItemSize = currentNameplate.Size;
				ListPanel eventsListPanel = this._eventsListPanel;
				ListPanel notificationListPanel = this._notificationListPanel;
				if (eventsListPanel != null)
				{
					eventsListPanel.ScaledPositionXOffset = this._cachedItemSize.X;
				}
				if (notificationListPanel != null)
				{
					notificationListPanel.ScaledPositionYOffset = -this._cachedItemSize.Y;
				}
				base.SuggestedWidth = this._cachedItemSize.X * base._inverseScaleToUse;
				base.SuggestedHeight = this._cachedItemSize.Y * base._inverseScaleToUse;
				base.ScaledSuggestedWidth = this._cachedItemSize.X;
				base.ScaledSuggestedHeight = this._cachedItemSize.Y;
			}
			base.IsEnabled = this.IsVisibleOnMap;
			this.UpdateNameplateTransparencyAndBrightness(dt);
			this.UpdatePosition(dt);
			this.UpdateTutorialState();
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00014258 File Offset: 0x00012458
		private void UpdatePosition(float dt)
		{
			SettlementNameplateItemWidget currentNameplate = this._currentNameplate;
			MapEventVisualBrushWidget mapEventVisualBrushWidget = (currentNameplate != null) ? currentNameplate.MapEventVisualWidget : null;
			if (currentNameplate == null || mapEventVisualBrushWidget == null)
			{
				Debug.FailedAssert("Related widget null on UpdatePosition!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Nameplate\\SettlementNameplateWidget.cs", "UpdatePosition", 105);
				return;
			}
			bool flag = false;
			this._positionTimer += dt;
			if (this.IsVisibleOnMap || this._positionTimer < 2f)
			{
				float x = base.Context.EventManager.PageSize.X;
				float y = base.Context.EventManager.PageSize.Y;
				Vec2 vec = this.Position;
				if (this.IsTracked)
				{
					if (this.WSign > 0 && vec.x - base.Size.X / 2f > 0f && vec.x + base.Size.X / 2f < x && vec.y > 0f && vec.y + base.Size.Y < y)
					{
						base.ScaledPositionXOffset = vec.x - base.Size.X / 2f;
						base.ScaledPositionYOffset = vec.y - base.Size.Y;
					}
					else
					{
						Vec2 vec2 = new Vec2(x / 2f, y / 2f);
						vec -= vec2;
						if (this.WSign < 0)
						{
							vec *= -1f;
						}
						float radian = Mathf.Atan2(vec.y, vec.x) - 1.5707964f;
						float num = Mathf.Cos(radian);
						float num2 = Mathf.Sin(radian);
						float num3 = num / num2;
						Vec2 vec3 = vec2 * 1f;
						vec = ((num > 0f) ? new Vec2(-vec3.y / num3, vec2.y) : new Vec2(vec3.y / num3, -vec2.y));
						if (vec.x > vec3.x)
						{
							vec = new Vec2(vec3.x, -vec3.x * num3);
						}
						else if (vec.x < -vec3.x)
						{
							vec = new Vec2(-vec3.x, vec3.x * num3);
						}
						vec += vec2;
						flag = (vec.y - base.Size.Y - mapEventVisualBrushWidget.Size.Y <= 0f);
						base.ScaledPositionXOffset = Mathf.Clamp(vec.x - base.Size.X / 2f, 0f, x - currentNameplate.Size.X);
						base.ScaledPositionYOffset = Mathf.Clamp(vec.y - base.Size.Y, 0f, y - (currentNameplate.Size.Y + 55f));
					}
				}
				else
				{
					base.ScaledPositionXOffset = vec.x - base.Size.X / 2f;
					base.ScaledPositionYOffset = vec.y - base.Size.Y;
				}
			}
			if (flag)
			{
				mapEventVisualBrushWidget.VerticalAlignment = VerticalAlignment.Bottom;
				mapEventVisualBrushWidget.ScaledPositionYOffset = mapEventVisualBrushWidget.Size.Y;
				return;
			}
			mapEventVisualBrushWidget.VerticalAlignment = VerticalAlignment.Top;
			mapEventVisualBrushWidget.ScaledPositionYOffset = -mapEventVisualBrushWidget.Size.Y;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000145BE File Offset: 0x000127BE
		private void OnNotificationListUpdated(Widget widget)
		{
			this._updatePositionNextFrame = true;
			this.AddLateUpdateAction();
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000145CD File Offset: 0x000127CD
		private void OnNotificationListUpdated(Widget parentWidget, Widget addedWidget)
		{
			this._updatePositionNextFrame = true;
			this.AddLateUpdateAction();
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000145DC File Offset: 0x000127DC
		private void AddLateUpdateAction()
		{
			if (!this._lateUpdateActionAdded)
			{
				base.EventManager.AddLateUpdateAction(this, new Action<float>(this.CustomLateUpdate), 1);
				this._lateUpdateActionAdded = true;
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00014606 File Offset: 0x00012806
		private void CustomLateUpdate(float dt)
		{
			if (this._updatePositionNextFrame)
			{
				this.UpdatePosition(dt);
				this._updatePositionNextFrame = false;
			}
			this._lateUpdateActionAdded = false;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00014625 File Offset: 0x00012825
		private void UpdateTutorialState()
		{
			if (this._tutorialAnimState == SettlementNameplateWidget.TutorialAnimState.Start)
			{
				this._tutorialAnimState = SettlementNameplateWidget.TutorialAnimState.FirstFrame;
			}
			else
			{
				SettlementNameplateWidget.TutorialAnimState tutorialAnimState = this._tutorialAnimState;
			}
			if (this.IsTargetedByTutorial)
			{
				this.SetState("Default");
				return;
			}
			this.SetState("Disabled");
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00014664 File Offset: 0x00012864
		private void SetNameplateTypeVisual(int type)
		{
			if (this._currentNameplate == null)
			{
				this.SmallNameplateWidget.IsVisible = false;
				this.NormalNameplateWidget.IsVisible = false;
				this.BigNameplateWidget.IsVisible = false;
				switch (type)
				{
				case 0:
					this._currentNameplate = this.SmallNameplateWidget;
					this.SmallNameplateWidget.IsVisible = true;
					return;
				case 1:
					this._currentNameplate = this.NormalNameplateWidget;
					this.NormalNameplateWidget.IsVisible = true;
					return;
				case 2:
					this._currentNameplate = this.BigNameplateWidget;
					this.BigNameplateWidget.IsVisible = true;
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00014700 File Offset: 0x00012900
		private void SetNameplateRelationType(int type)
		{
			if (this._currentNameplate != null)
			{
				switch (type)
				{
				case 0:
					this._currentNameplate.Color = Color.Black;
					return;
				case 1:
					this._currentNameplate.Color = Color.ConvertStringToColor("#245E05FF");
					return;
				case 2:
					this._currentNameplate.Color = Color.ConvertStringToColor("#870707FF");
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00014764 File Offset: 0x00012964
		private void UpdateNameplateTransparencyAndBrightness(float dt)
		{
			SettlementNameplateItemWidget currentNameplate = this._currentNameplate;
			TextWidget textWidget = (currentNameplate != null) ? currentNameplate.SettlementNameTextWidget : null;
			MaskedTextureWidget maskedTextureWidget = (currentNameplate != null) ? currentNameplate.SettlementBannerWidget : null;
			GridWidget gridWidget = (currentNameplate != null) ? currentNameplate.SettlementPartiesGridWidget : null;
			Widget widget = (currentNameplate != null) ? currentNameplate.SettlementNameplateInspectedWidget : null;
			ListPanel eventsListPanel = this._eventsListPanel;
			if (currentNameplate == null || textWidget == null || maskedTextureWidget == null || gridWidget == null || widget == null || eventsListPanel == null)
			{
				Debug.FailedAssert("Related widget null on UpdateNameplateTransparencyAndBrightness!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\Nameplate\\SettlementNameplateWidget.cs", "UpdateNameplateTransparencyAndBrightness", 342);
				return;
			}
			float amount = dt * this._lerpModifier;
			if (this.IsVisibleOnMap)
			{
				base.IsVisible = true;
				float valueTo = this.DetermineTargetAlphaValue();
				float valueTo2 = this.DetermineTargetColorFactor();
				float alphaFactor = MathF.Lerp(currentNameplate.AlphaFactor, valueTo, amount, 1E-05f);
				float colorFactor = MathF.Lerp(currentNameplate.ColorFactor, valueTo2, amount, 1E-05f);
				float num = MathF.Lerp(textWidget.ReadOnlyBrush.GlobalAlphaFactor, 1f, amount, 1E-05f);
				currentNameplate.AlphaFactor = alphaFactor;
				currentNameplate.ColorFactor = colorFactor;
				textWidget.Brush.GlobalAlphaFactor = num;
				maskedTextureWidget.Brush.GlobalAlphaFactor = num;
				gridWidget.SetGlobalAlphaRecursively(num);
				eventsListPanel.SetGlobalAlphaRecursively(num);
			}
			else if (currentNameplate.AlphaFactor > this._lerpThreshold)
			{
				float num2 = MathF.Lerp(currentNameplate.AlphaFactor, 0f, amount, 1E-05f);
				currentNameplate.AlphaFactor = num2;
				textWidget.Brush.GlobalAlphaFactor = num2;
				maskedTextureWidget.Brush.GlobalAlphaFactor = num2;
				gridWidget.SetGlobalAlphaRecursively(num2);
				eventsListPanel.SetGlobalAlphaRecursively(num2);
			}
			else
			{
				base.IsVisible = false;
			}
			if (this.IsInRange && this.IsVisibleOnMap)
			{
				if (Math.Abs(widget.AlphaFactor - 1f) > this._lerpThreshold)
				{
					widget.AlphaFactor = MathF.Lerp(widget.AlphaFactor, 1f, amount, 1E-05f);
					return;
				}
			}
			else if (currentNameplate.AlphaFactor - 0f > this._lerpThreshold)
			{
				widget.AlphaFactor = MathF.Lerp(widget.AlphaFactor, 0f, amount, 1E-05f);
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00014978 File Offset: 0x00012B78
		private float DetermineTargetAlphaValue()
		{
			if (this.IsInsideWindow)
			{
				if (this.IsTracked)
				{
					return this._trackedAlphaTarget;
				}
				if (this.RelationType == 0)
				{
					return this._normalNeutralAlphaTarget;
				}
				if (this.RelationType == 1)
				{
					return this._normalAllyAlphaTarget;
				}
				return this._normalEnemyAlphaTarget;
			}
			else
			{
				if (this.IsTracked)
				{
					return this._screenEdgeAlphaTarget;
				}
				return 0f;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000149D6 File Offset: 0x00012BD6
		private float DetermineTargetColorFactor()
		{
			if (this.IsTracked)
			{
				return this._trackedColorFactorTarget;
			}
			return this._normalColorFactorTarget;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000149F0 File Offset: 0x00012BF0
		public int CompareTo(SettlementNameplateWidget other)
		{
			return other.DistanceToCamera.CompareTo(this.DistanceToCamera);
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00014A11 File Offset: 0x00012C11
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00014A19 File Offset: 0x00012C19
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (this._position != value)
				{
					this._position = value;
					base.OnPropertyChanged(value, "Position");
				}
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x00014A3C File Offset: 0x00012C3C
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00014A44 File Offset: 0x00012C44
		public bool IsVisibleOnMap
		{
			get
			{
				return this._isVisibleOnMap;
			}
			set
			{
				if (this._isVisibleOnMap != value)
				{
					if (this._isVisibleOnMap && !value)
					{
						this._positionTimer = 0f;
					}
					this._isVisibleOnMap = value;
					base.OnPropertyChanged(value, "IsVisibleOnMap");
				}
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00014A78 File Offset: 0x00012C78
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00014A80 File Offset: 0x00012C80
		public bool IsTracked
		{
			get
			{
				return this._isTracked;
			}
			set
			{
				if (this._isTracked != value)
				{
					this._isTracked = value;
					base.OnPropertyChanged(value, "IsTracked");
				}
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x00014A9E File Offset: 0x00012C9E
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x00014AA6 File Offset: 0x00012CA6
		public bool IsTargetedByTutorial
		{
			get
			{
				return this._isTargetedByTutorial;
			}
			set
			{
				if (this._isTargetedByTutorial != value)
				{
					this._isTargetedByTutorial = value;
					base.OnPropertyChanged(value, "IsTargetedByTutorial");
					if (value)
					{
						this._tutorialAnimState = SettlementNameplateWidget.TutorialAnimState.Start;
					}
				}
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x00014ACE File Offset: 0x00012CCE
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x00014AD6 File Offset: 0x00012CD6
		public bool IsInsideWindow
		{
			get
			{
				return this._isInsideWindow;
			}
			set
			{
				if (this._isInsideWindow != value)
				{
					this._isInsideWindow = value;
					base.OnPropertyChanged(value, "IsInsideWindow");
				}
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00014AF4 File Offset: 0x00012CF4
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x00014AFC File Offset: 0x00012CFC
		public bool IsInRange
		{
			get
			{
				return this._isInRange;
			}
			set
			{
				if (this._isInRange != value)
				{
					this._isInRange = value;
				}
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00014B0E File Offset: 0x00012D0E
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x00014B16 File Offset: 0x00012D16
		public int NameplateType
		{
			get
			{
				return this._nameplateType;
			}
			set
			{
				if (this._nameplateType != value)
				{
					this._nameplateType = value;
					base.OnPropertyChanged(value, "NameplateType");
					this.SetNameplateTypeVisual(value);
				}
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00014B3B File Offset: 0x00012D3B
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x00014B43 File Offset: 0x00012D43
		public int RelationType
		{
			get
			{
				return this._relationType;
			}
			set
			{
				if (this._relationType != value)
				{
					this._relationType = value;
					base.OnPropertyChanged(value, "RelationType");
					this.SetNameplateRelationType(value);
				}
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00014B68 File Offset: 0x00012D68
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x00014B70 File Offset: 0x00012D70
		public int WSign
		{
			get
			{
				return this._wSign;
			}
			set
			{
				if (this._wSign != value)
				{
					this._wSign = value;
					base.OnPropertyChanged(value, "WSign");
				}
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00014B8E File Offset: 0x00012D8E
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x00014B96 File Offset: 0x00012D96
		public float WPos
		{
			get
			{
				return this._wPos;
			}
			set
			{
				if (this._wPos != value)
				{
					this._wPos = value;
					base.OnPropertyChanged(value, "WPos");
				}
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00014BB4 File Offset: 0x00012DB4
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x00014BBC File Offset: 0x00012DBC
		public float DistanceToCamera
		{
			get
			{
				return this._distanceToCamera;
			}
			set
			{
				if (this._distanceToCamera != value)
				{
					this._distanceToCamera = value;
					base.OnPropertyChanged(value, "DistanceToCamera");
				}
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00014BDA File Offset: 0x00012DDA
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x00014BE4 File Offset: 0x00012DE4
		public ListPanel NotificationListPanel
		{
			get
			{
				return this._notificationListPanel;
			}
			set
			{
				if (this._notificationListPanel != value)
				{
					this._notificationListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "NotificationListPanel");
					this._notificationListPanel.ItemAddEventHandlers.Add(new Action<Widget, Widget>(this.OnNotificationListUpdated));
					this._notificationListPanel.ItemAfterRemoveEventHandlers.Add(new Action<Widget>(this.OnNotificationListUpdated));
				}
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00014C45 File Offset: 0x00012E45
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x00014C4D File Offset: 0x00012E4D
		public ListPanel EventsListPanel
		{
			get
			{
				return this._eventsListPanel;
			}
			set
			{
				if (value != this._eventsListPanel)
				{
					this._eventsListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "EventsListPanel");
				}
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00014C6B File Offset: 0x00012E6B
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00014C73 File Offset: 0x00012E73
		public SettlementNameplateItemWidget SmallNameplateWidget
		{
			get
			{
				return this._smallNameplateWidget;
			}
			set
			{
				if (this._smallNameplateWidget != value)
				{
					this._smallNameplateWidget = value;
					base.OnPropertyChanged<SettlementNameplateItemWidget>(value, "SmallNameplateWidget");
				}
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00014C91 File Offset: 0x00012E91
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x00014C99 File Offset: 0x00012E99
		public SettlementNameplateItemWidget NormalNameplateWidget
		{
			get
			{
				return this._normalNameplateWidget;
			}
			set
			{
				if (this._normalNameplateWidget != value)
				{
					this._normalNameplateWidget = value;
					base.OnPropertyChanged<SettlementNameplateItemWidget>(value, "NormalNameplateWidget");
				}
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00014CB7 File Offset: 0x00012EB7
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x00014CBF File Offset: 0x00012EBF
		public SettlementNameplateItemWidget BigNameplateWidget
		{
			get
			{
				return this._bigNameplateWidget;
			}
			set
			{
				if (this._bigNameplateWidget != value)
				{
					this._bigNameplateWidget = value;
					base.OnPropertyChanged<SettlementNameplateItemWidget>(value, "BigNameplateWidget");
				}
			}
		}

		// Token: 0x040002F6 RID: 758
		private float _positionTimer;

		// Token: 0x040002F7 RID: 759
		private SettlementNameplateItemWidget _currentNameplate;

		// Token: 0x040002F8 RID: 760
		private bool _updatePositionNextFrame;

		// Token: 0x040002F9 RID: 761
		private SettlementNameplateWidget.TutorialAnimState _tutorialAnimState;

		// Token: 0x040002FA RID: 762
		private float _lerpThreshold = 5E-05f;

		// Token: 0x040002FB RID: 763
		private float _lerpModifier = 10f;

		// Token: 0x040002FC RID: 764
		private Vector2 _cachedItemSize;

		// Token: 0x040002FD RID: 765
		private bool _lateUpdateActionAdded;

		// Token: 0x040002FE RID: 766
		private Vec2 _position;

		// Token: 0x040002FF RID: 767
		private bool _isVisibleOnMap;

		// Token: 0x04000300 RID: 768
		private bool _isTracked;

		// Token: 0x04000301 RID: 769
		private bool _isInsideWindow;

		// Token: 0x04000302 RID: 770
		private bool _isTargetedByTutorial;

		// Token: 0x04000303 RID: 771
		private int _nameplateType = -1;

		// Token: 0x04000304 RID: 772
		private int _relationType = -1;

		// Token: 0x04000305 RID: 773
		private int _wSign;

		// Token: 0x04000306 RID: 774
		private float _wPos;

		// Token: 0x04000307 RID: 775
		private float _distanceToCamera;

		// Token: 0x04000308 RID: 776
		private bool _isInRange;

		// Token: 0x04000309 RID: 777
		private SettlementNameplateItemWidget _smallNameplateWidget;

		// Token: 0x0400030A RID: 778
		private SettlementNameplateItemWidget _normalNameplateWidget;

		// Token: 0x0400030B RID: 779
		private SettlementNameplateItemWidget _bigNameplateWidget;

		// Token: 0x0400030C RID: 780
		private ListPanel _notificationListPanel;

		// Token: 0x0400030D RID: 781
		private ListPanel _eventsListPanel;

		// Token: 0x020001A2 RID: 418
		public enum TutorialAnimState
		{
			// Token: 0x0400098C RID: 2444
			Idle,
			// Token: 0x0400098D RID: 2445
			Start,
			// Token: 0x0400098E RID: 2446
			FirstFrame,
			// Token: 0x0400098F RID: 2447
			Playing
		}
	}
}
