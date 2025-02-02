using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Nameplate
{
	// Token: 0x02000076 RID: 118
	public class PartyNameplateWidget : Widget
	{
		// Token: 0x06000668 RID: 1640 RVA: 0x00012E35 File Offset: 0x00011035
		public PartyNameplateWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00012E50 File Offset: 0x00011050
		private float _animSpeedModifier
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00012E57 File Offset: 0x00011057
		private int _armyFontSizeOffset
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00012E5B File Offset: 0x0001105B
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00012E63 File Offset: 0x00011063
		public Widget HeadGroupWidget { get; set; }

		// Token: 0x0600066D RID: 1645 RVA: 0x00012E6C File Offset: 0x0001106C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isFirstFrame)
			{
				this.NameplateFullNameTextWidget.Brush.GlobalAlphaFactor = 0f;
				this.NameplateTextWidget.Brush.GlobalAlphaFactor = 0f;
				this.NameplateExtraInfoTextWidget.Brush.GlobalAlphaFactor = 0f;
				this.PartyBannerWidget.Brush.GlobalAlphaFactor = 0f;
				this.SpeedTextWidget.AlphaFactor = 0f;
				this._defaultNameplateFontSize = this.NameplateTextWidget.ReadOnlyBrush.FontSize;
				this._isFirstFrame = false;
			}
			int num = this.IsArmy ? (this._defaultNameplateFontSize + this._armyFontSizeOffset) : this._defaultNameplateFontSize;
			if (this.NameplateTextWidget.Brush.FontSize != num)
			{
				this.NameplateTextWidget.Brush.FontSize = num;
			}
			this.UpdateNameplatesScreenPosition();
			this.UpdateNameplatesVisibility(dt);
			this.UpdateTutorialStatus();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00012F64 File Offset: 0x00011164
		private void UpdateNameplatesVisibility(float dt)
		{
			float end = 0f;
			float end2;
			if (this.IsMainParty)
			{
				this._latestIsOutside = this.IsNameplateOutsideScreen();
				this.MainPartyArrowWidget.IsVisible = this._latestIsOutside;
				this.NameplateTextWidget.IsVisible = (!this._latestIsOutside && !this.IsInArmy && !this.IsPrisoner && !this.IsInSettlement);
				this.NameplateFullNameTextWidget.IsVisible = (!this._latestIsOutside && !this.IsInArmy && !this.IsPrisoner && !this.IsInSettlement);
				this.SpeedTextWidget.IsVisible = (!this._latestIsOutside && !this.IsInArmy && !this.IsPrisoner && !this.IsInSettlement);
				this.SpeedIconWidget.IsVisible = (!this._latestIsOutside && !this.IsInArmy && !this.IsPrisoner && !this.IsInSettlement);
				this.TrackerFrame.IsVisible = this._latestIsOutside;
				end2 = (float)((!this._latestIsOutside && !this.IsInArmy && !this.IsPrisoner && !this.IsInSettlement) ? 1 : 0);
				this.PartyBannerWidget.IsVisible = (!this._latestIsOutside && !this.IsInArmy && !this.IsPrisoner && !this.IsInSettlement);
				this.NameplateExtraInfoTextWidget.IsVisible = (!this._latestIsOutside && !this.IsInArmy && !this.IsPrisoner && !this.IsInSettlement);
				base.IsEnabled = this._latestIsOutside;
			}
			else
			{
				this.MainPartyArrowWidget.IsVisible = false;
				this.NameplateTextWidget.IsVisible = true;
				this.NameplateFullNameTextWidget.IsVisible = true;
				this.SpeedTextWidget.IsVisible = true;
				this.SpeedIconWidget.IsVisible = true;
				this.TrackerFrame.IsVisible = false;
				this.PartyBannerWidget.IsVisible = true;
				end2 = 1f;
				base.IsEnabled = false;
			}
			if (!this.IsVisibleOnMap && !this.IsMainParty)
			{
				this.NameplateTextWidget.IsVisible = false;
				this.NameplateFullNameTextWidget.IsVisible = false;
				this.SpeedTextWidget.IsVisible = false;
				this.SpeedIconWidget.IsVisible = false;
				end2 = 0f;
			}
			else
			{
				this._initialDelayAmount -= dt;
				if (this._initialDelayAmount <= 0f)
				{
					end = (float)(this.ShouldShowFullName ? 1 : 0);
				}
				else
				{
					end = 1f;
				}
			}
			this.NameplateTextWidget.Brush.GlobalAlphaFactor = this.LocalLerp(this.NameplateTextWidget.ReadOnlyBrush.GlobalAlphaFactor, end2, dt * this._animSpeedModifier);
			this.NameplateFullNameTextWidget.Brush.GlobalAlphaFactor = this.LocalLerp(this.NameplateFullNameTextWidget.ReadOnlyBrush.GlobalAlphaFactor, end, dt * this._animSpeedModifier);
			this.SpeedTextWidget.Brush.GlobalAlphaFactor = this.LocalLerp(this.SpeedTextWidget.ReadOnlyBrush.GlobalAlphaFactor, end, dt * this._animSpeedModifier);
			this.SpeedIconWidget.AlphaFactor = this.LocalLerp(this.SpeedIconWidget.AlphaFactor, end, dt * this._animSpeedModifier);
			this.NameplateExtraInfoTextWidget.Brush.GlobalAlphaFactor = this.LocalLerp(this.NameplateExtraInfoTextWidget.ReadOnlyBrush.GlobalAlphaFactor, (float)(this.ShouldShowFullName ? 1 : 0), dt * this._animSpeedModifier);
			this.PartyBannerWidget.Brush.GlobalAlphaFactor = this.LocalLerp(this.PartyBannerWidget.ReadOnlyBrush.GlobalAlphaFactor, end2, dt * this._animSpeedModifier);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000132FC File Offset: 0x000114FC
		private void UpdateNameplatesScreenPosition()
		{
			this._screenWidth = base.Context.EventManager.PageSize.X;
			this._screenHeight = base.Context.EventManager.PageSize.Y;
			if (!this.IsVisibleOnMap && !this.IsMainParty)
			{
				base.IsHidden = true;
				return;
			}
			if (this.IsMainParty)
			{
				if (!this.IsBehind && this.Position.x + base.Size.X <= this._screenWidth && this.Position.y - base.Size.Y <= this._screenHeight && this.Position.x >= 0f && this.Position.y >= 0f)
				{
					Widget headGroupWidget = this.HeadGroupWidget;
					float num = (headGroupWidget != null) ? headGroupWidget.Size.Y : 0f;
					this.NameplateLayoutListPanel.ScaledPositionYOffset = this.Position.y - this.HeadPosition.y + num;
					if (this.IsHigh)
					{
						base.ScaledPositionXOffset = MathF.Clamp(this.HeadPosition.x - base.Size.X / 2f, 0f, this._screenWidth - base.Size.X);
					}
					else
					{
						base.ScaledPositionXOffset = MathF.Clamp(this.HeadPosition.x - base.Size.X / 2f, 0f, this._screenWidth - base.Size.X);
					}
					base.ScaledPositionYOffset = this.HeadPosition.y - num;
				}
				else
				{
					Vec2 vec = new Vec2(base.Context.EventManager.PageSize.X / 2f, base.Context.EventManager.PageSize.Y / 2f);
					Vec2 vec2 = this.HeadPosition;
					vec2 -= vec;
					if (this.IsBehind)
					{
						vec2 *= -1f;
					}
					float radian = Mathf.Atan2(vec2.y, vec2.x) - 1.5707964f;
					float num2 = Mathf.Cos(radian);
					float num3 = Mathf.Sin(radian);
					vec2 = vec + new Vec2(num3 * 150f, num2 * 150f);
					float num4 = num2 / num3;
					Vec2 vec3 = vec * 1f;
					vec2 = ((num2 > 0f) ? new Vec2(-vec3.y / num4, vec.y) : new Vec2(vec3.y / num4, -vec.y));
					if (vec2.x > vec3.x)
					{
						vec2 = new Vec2(vec3.x, -vec3.x * num4);
					}
					else if (vec2.x < -vec3.x)
					{
						vec2 = new Vec2(-vec3.x, vec3.x * num4);
					}
					vec2 += vec;
					base.ScaledPositionXOffset = MathF.Clamp(vec2.x - base.Size.X / 2f, 0f, this._screenWidth - base.Size.X);
					base.ScaledPositionYOffset = MathF.Clamp(vec2.y - base.Size.Y / 2f, 0f, this._screenHeight - base.Size.Y);
				}
			}
			else
			{
				Widget headGroupWidget2 = this.HeadGroupWidget;
				float num5 = (headGroupWidget2 != null) ? headGroupWidget2.Size.Y : 0f;
				this.NameplateLayoutListPanel.ScaledPositionYOffset = this.Position.y - this.HeadPosition.y + num5;
				base.ScaledPositionXOffset = this.HeadPosition.x - base.Size.X / 2f;
				base.ScaledPositionYOffset = this.HeadPosition.y - num5;
				base.IsHidden = (base.ScaledPositionXOffset > base.Context.TwoDimensionContext.Width || base.ScaledPositionYOffset > base.Context.TwoDimensionContext.Height || base.ScaledPositionXOffset + base.Size.X < 0f || base.ScaledPositionYOffset + base.Size.Y < 0f);
			}
			this.NameplateLayoutListPanel.PositionXOffset = (base.Size.X / 2f - this.PartyBannerWidget.Size.X) * base._inverseScaleToUse;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00013791 File Offset: 0x00011991
		private void UpdateTutorialStatus()
		{
			if (this._tutorialAnimState == PartyNameplateWidget.TutorialAnimState.Start)
			{
				this._tutorialAnimState = PartyNameplateWidget.TutorialAnimState.FirstFrame;
			}
			else
			{
				PartyNameplateWidget.TutorialAnimState tutorialAnimState = this._tutorialAnimState;
			}
			if (this.IsTargetedByTutorial)
			{
				this.SetState("Default");
				return;
			}
			this.SetState("Disabled");
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000137D0 File Offset: 0x000119D0
		private bool IsNameplateOutsideScreen()
		{
			return this.Position.x + base.Size.X > this._screenWidth || this.Position.y - base.Size.Y > this._screenHeight || this.Position.x < 0f || this.Position.y < 0f || this.IsBehind || this.IsHigh;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001384F File Offset: 0x00011A4F
		private float LocalLerp(float start, float end, float delta)
		{
			if (Math.Abs(start - end) > 1E-45f)
			{
				return (end - start) * delta + start;
			}
			return end;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00013869 File Offset: 0x00011A69
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x00013871 File Offset: 0x00011A71
		public ListPanel NameplateLayoutListPanel
		{
			get
			{
				return this._nameplateLayoutListPanel;
			}
			set
			{
				if (this._nameplateLayoutListPanel != value)
				{
					this._nameplateLayoutListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "NameplateLayoutListPanel");
				}
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001388F File Offset: 0x00011A8F
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x00013897 File Offset: 0x00011A97
		public MaskedTextureWidget PartyBannerWidget
		{
			get
			{
				return this._partyBannerWidget;
			}
			set
			{
				if (this._partyBannerWidget != value)
				{
					this._partyBannerWidget = value;
					base.OnPropertyChanged<MaskedTextureWidget>(value, "PartyBannerWidget");
				}
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x000138B5 File Offset: 0x00011AB5
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x000138BD File Offset: 0x00011ABD
		public Widget TrackerFrame
		{
			get
			{
				return this._trackerFrame;
			}
			set
			{
				if (this._trackerFrame != value)
				{
					this._trackerFrame = value;
					base.OnPropertyChanged<Widget>(value, "TrackerFrame");
				}
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x000138DB File Offset: 0x00011ADB
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x000138E3 File Offset: 0x00011AE3
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

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00013906 File Offset: 0x00011B06
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0001390E File Offset: 0x00011B0E
		public Vec2 HeadPosition
		{
			get
			{
				return this._headPosition;
			}
			set
			{
				if (this._headPosition != value)
				{
					this._headPosition = value;
					base.OnPropertyChanged(value, "HeadPosition");
				}
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x00013931 File Offset: 0x00011B31
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x00013939 File Offset: 0x00011B39
		public bool ShouldShowFullName
		{
			get
			{
				return this._shouldShowFullName;
			}
			set
			{
				if (this._shouldShowFullName != value)
				{
					this._shouldShowFullName = value;
					base.OnPropertyChanged(value, "ShouldShowFullName");
				}
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00013957 File Offset: 0x00011B57
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0001395F File Offset: 0x00011B5F
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
					this._tutorialAnimState = PartyNameplateWidget.TutorialAnimState.Start;
				}
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00013984 File Offset: 0x00011B84
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0001398C File Offset: 0x00011B8C
		public bool IsInArmy
		{
			get
			{
				return this._isInArmy;
			}
			set
			{
				if (this._isInArmy != value)
				{
					this._isInArmy = value;
					base.OnPropertyChanged(value, "IsInArmy");
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000139AA File Offset: 0x00011BAA
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x000139B2 File Offset: 0x00011BB2
		public bool IsInSettlement
		{
			get
			{
				return this._isInSettlement;
			}
			set
			{
				if (this._isInSettlement != value)
				{
					this._isInSettlement = value;
					base.OnPropertyChanged(value, "IsInSettlement");
				}
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x000139D0 File Offset: 0x00011BD0
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x000139D8 File Offset: 0x00011BD8
		public bool IsArmy
		{
			get
			{
				return this._isArmy;
			}
			set
			{
				if (this._isArmy != value)
				{
					this._isArmy = value;
					base.OnPropertyChanged(value, "IsArmy");
				}
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000139F6 File Offset: 0x00011BF6
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x000139FE File Offset: 0x00011BFE
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
					this._isVisibleOnMap = value;
					base.OnPropertyChanged(value, "IsVisibleOnMap");
				}
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x00013A1C File Offset: 0x00011C1C
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00013A24 File Offset: 0x00011C24
		public bool IsMainParty
		{
			get
			{
				return this._isMainParty;
			}
			set
			{
				if (this._isMainParty != value)
				{
					this._isMainParty = value;
					base.OnPropertyChanged(value, "IsMainParty");
				}
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00013A42 File Offset: 0x00011C42
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x00013A4A File Offset: 0x00011C4A
		public bool IsInside
		{
			get
			{
				return this._isInside;
			}
			set
			{
				if (this._isInside != value)
				{
					this._isInside = value;
					base.OnPropertyChanged(value, "IsInside");
				}
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00013A68 File Offset: 0x00011C68
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00013A70 File Offset: 0x00011C70
		public bool IsHigh
		{
			get
			{
				return this._isHigh;
			}
			set
			{
				if (this._isHigh != value)
				{
					this._isHigh = value;
					base.OnPropertyChanged(value, "IsHigh");
				}
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00013A8E File Offset: 0x00011C8E
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00013A96 File Offset: 0x00011C96
		public bool IsBehind
		{
			get
			{
				return this._isBehind;
			}
			set
			{
				if (this._isBehind != value)
				{
					this._isBehind = value;
					base.OnPropertyChanged(value, "IsBehind");
				}
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00013AB4 File Offset: 0x00011CB4
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00013ABC File Offset: 0x00011CBC
		public bool IsPrisoner
		{
			get
			{
				return this._isPrisoner;
			}
			set
			{
				if (this._isPrisoner != value)
				{
					this._isPrisoner = value;
					base.OnPropertyChanged(value, "IsPrisoner");
				}
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00013ADA File Offset: 0x00011CDA
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00013AE2 File Offset: 0x00011CE2
		public TextWidget NameplateTextWidget
		{
			get
			{
				return this._nameplateTextWidget;
			}
			set
			{
				if (this._nameplateTextWidget != value)
				{
					this._nameplateTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "NameplateTextWidget");
				}
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00013B00 File Offset: 0x00011D00
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00013B08 File Offset: 0x00011D08
		public TextWidget NameplateExtraInfoTextWidget
		{
			get
			{
				return this._nameplateExtraInfoTextWidget;
			}
			set
			{
				if (this._nameplateExtraInfoTextWidget != value)
				{
					this._nameplateExtraInfoTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "NameplateExtraInfoTextWidget");
				}
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00013B26 File Offset: 0x00011D26
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00013B2E File Offset: 0x00011D2E
		public TextWidget NameplateFullNameTextWidget
		{
			get
			{
				return this._nameplateFullNameTextWidget;
			}
			set
			{
				if (this._nameplateFullNameTextWidget != value)
				{
					this._nameplateFullNameTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "NameplateFullNameTextWidget");
				}
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00013B4C File Offset: 0x00011D4C
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00013B54 File Offset: 0x00011D54
		public TextWidget SpeedTextWidget
		{
			get
			{
				return this._speedTextWidget;
			}
			set
			{
				if (this._speedTextWidget != value)
				{
					this._speedTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "SpeedTextWidget");
				}
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00013B72 File Offset: 0x00011D72
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x00013B7A File Offset: 0x00011D7A
		public Widget SpeedIconWidget
		{
			get
			{
				return this._speedIconWidget;
			}
			set
			{
				if (value != this._speedIconWidget)
				{
					this._speedIconWidget = value;
					base.OnPropertyChanged<Widget>(value, "SpeedIconWidget");
				}
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00013B98 File Offset: 0x00011D98
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00013BA0 File Offset: 0x00011DA0
		public Widget MainPartyArrowWidget
		{
			get
			{
				return this._mainPartyArrowWidget;
			}
			set
			{
				if (this._mainPartyArrowWidget != value)
				{
					this._mainPartyArrowWidget = value;
					base.OnPropertyChanged<Widget>(value, "MainPartyArrowWidget");
				}
			}
		}

		// Token: 0x040002C8 RID: 712
		private bool _isFirstFrame = true;

		// Token: 0x040002C9 RID: 713
		private float _screenWidth;

		// Token: 0x040002CA RID: 714
		private float _screenHeight;

		// Token: 0x040002CB RID: 715
		private bool _latestIsOutside;

		// Token: 0x040002CC RID: 716
		private float _initialDelayAmount = 2f;

		// Token: 0x040002CD RID: 717
		private int _defaultNameplateFontSize;

		// Token: 0x040002CE RID: 718
		private PartyNameplateWidget.TutorialAnimState _tutorialAnimState;

		// Token: 0x040002D0 RID: 720
		private Vec2 _position;

		// Token: 0x040002D1 RID: 721
		private Vec2 _headPosition;

		// Token: 0x040002D2 RID: 722
		private TextWidget _nameplateTextWidget;

		// Token: 0x040002D3 RID: 723
		private TextWidget _nameplateFullNameTextWidget;

		// Token: 0x040002D4 RID: 724
		private TextWidget _speedTextWidget;

		// Token: 0x040002D5 RID: 725
		private Widget _speedIconWidget;

		// Token: 0x040002D6 RID: 726
		private TextWidget _nameplateExtraInfoTextWidget;

		// Token: 0x040002D7 RID: 727
		private Widget _trackerFrame;

		// Token: 0x040002D8 RID: 728
		private Widget _mainPartyArrowWidget;

		// Token: 0x040002D9 RID: 729
		private ListPanel _nameplateLayoutListPanel;

		// Token: 0x040002DA RID: 730
		private MaskedTextureWidget _partyBannerWidget;

		// Token: 0x040002DB RID: 731
		private bool _isVisibleOnMap;

		// Token: 0x040002DC RID: 732
		private bool _isMainParty;

		// Token: 0x040002DD RID: 733
		private bool _isInside;

		// Token: 0x040002DE RID: 734
		private bool _isBehind;

		// Token: 0x040002DF RID: 735
		private bool _isHigh;

		// Token: 0x040002E0 RID: 736
		private bool _isInArmy;

		// Token: 0x040002E1 RID: 737
		private bool _isInSettlement;

		// Token: 0x040002E2 RID: 738
		private bool _isArmy;

		// Token: 0x040002E3 RID: 739
		private bool _isTargetedByTutorial;

		// Token: 0x040002E4 RID: 740
		private bool _shouldShowFullName;

		// Token: 0x040002E5 RID: 741
		private bool _isPrisoner;

		// Token: 0x020001A1 RID: 417
		public enum TutorialAnimState
		{
			// Token: 0x04000987 RID: 2439
			Idle,
			// Token: 0x04000988 RID: 2440
			Start,
			// Token: 0x04000989 RID: 2441
			FirstFrame,
			// Token: 0x0400098A RID: 2442
			Playing
		}
	}
}
