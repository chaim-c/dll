using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterDeveloper
{
	// Token: 0x02000176 RID: 374
	public class PerkItemButtonWidget : ButtonWidget
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00034E80 File Offset: 0x00033080
		// (set) Token: 0x06001349 RID: 4937 RVA: 0x00034E88 File Offset: 0x00033088
		public Brush NotEarnedPerkBrush { get; set; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x00034E91 File Offset: 0x00033091
		// (set) Token: 0x0600134B RID: 4939 RVA: 0x00034E99 File Offset: 0x00033099
		public Brush EarnedNotSelectedPerkBrush { get; set; }

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x00034EA2 File Offset: 0x000330A2
		// (set) Token: 0x0600134D RID: 4941 RVA: 0x00034EAA File Offset: 0x000330AA
		public Brush InSelectionPerkBrush { get; set; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x00034EB3 File Offset: 0x000330B3
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x00034EBB File Offset: 0x000330BB
		public Brush EarnedActivePerkBrush { get; set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x00034EC4 File Offset: 0x000330C4
		// (set) Token: 0x06001351 RID: 4945 RVA: 0x00034ECC File Offset: 0x000330CC
		public Brush EarnedNotActivePerkBrush { get; set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x00034ED5 File Offset: 0x000330D5
		// (set) Token: 0x06001353 RID: 4947 RVA: 0x00034EDD File Offset: 0x000330DD
		public Brush EarnedPreviousPerkNotSelectedPerkBrush { get; set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x00034EE6 File Offset: 0x000330E6
		// (set) Token: 0x06001355 RID: 4949 RVA: 0x00034EEE File Offset: 0x000330EE
		public BrushWidget PerkVisualWidgetParent { get; set; }

		// Token: 0x06001356 RID: 4950 RVA: 0x00034EF7 File Offset: 0x000330F7
		public PerkItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00034F08 File Offset: 0x00033108
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.PerkVisualWidget != null && ((this.PerkVisualWidget.Sprite != null && base.Context.SpriteData.GetSprite(this.PerkVisualWidget.Sprite.Name) == null) || this.PerkVisualWidget.Sprite == null))
			{
				this.PerkVisualWidget.Sprite = base.Context.SpriteData.GetSprite("SPPerks\\locked_fallback");
			}
			if (this._animState == PerkItemButtonWidget.AnimState.Start)
			{
				this._tickCount++;
				if (this._tickCount > 20)
				{
					this._animState = PerkItemButtonWidget.AnimState.Starting;
					return;
				}
			}
			else if (this._animState == PerkItemButtonWidget.AnimState.Starting)
			{
				this.PerkVisualWidgetParent.BrushRenderer.RestartAnimation();
				this._animState = PerkItemButtonWidget.AnimState.Playing;
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00034FCC File Offset: 0x000331CC
		private void SetColorState(bool isActive)
		{
			if (this.PerkVisualWidget != null)
			{
				float alphaFactor = isActive ? 1f : 1f;
				float colorFactor = isActive ? 1.3f : 0.75f;
				using (IEnumerator<Widget> enumerator = base.AllChildrenAndThis.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BrushWidget brushWidget;
						if ((brushWidget = (enumerator.Current as BrushWidget)) != null)
						{
							foreach (Style style in brushWidget.Brush.Styles)
							{
								for (int i = 0; i < style.LayerCount; i++)
								{
									StyleLayer layer = style.GetLayer(i);
									layer.AlphaFactor = alphaFactor;
									layer.ColorFactor = colorFactor;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000350B4 File Offset: 0x000332B4
		protected override void OnClick()
		{
			base.OnClick();
			if (this._isSelectable)
			{
				base.Context.TwoDimensionContext.PlaySound("popup");
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x000350DC File Offset: 0x000332DC
		private void UpdatePerkStateVisual(int perkState)
		{
			switch (perkState)
			{
			case 0:
				this.PerkVisualWidgetParent.Brush = this.NotEarnedPerkBrush;
				this._isSelectable = false;
				return;
			case 1:
				this.PerkVisualWidgetParent.Brush = this.EarnedNotSelectedPerkBrush;
				this._animState = PerkItemButtonWidget.AnimState.Start;
				this._isSelectable = true;
				return;
			case 2:
				this.PerkVisualWidgetParent.Brush = this.InSelectionPerkBrush;
				this._isSelectable = false;
				return;
			case 3:
				this.PerkVisualWidgetParent.Brush = this.EarnedActivePerkBrush;
				this._isSelectable = false;
				return;
			case 4:
				this.PerkVisualWidgetParent.Brush = this.EarnedNotActivePerkBrush;
				this._isSelectable = false;
				return;
			case 5:
				this.PerkVisualWidgetParent.Brush = this.EarnedPreviousPerkNotSelectedPerkBrush;
				this._isSelectable = false;
				return;
			default:
				Debug.FailedAssert("Perk visual state is not defined", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI.Widgets\\CharacterDeveloper\\PerkItemButtonWidget.cs", "UpdatePerkStateVisual", 134);
				return;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x000351C2 File Offset: 0x000333C2
		// (set) Token: 0x0600135C RID: 4956 RVA: 0x000351CA File Offset: 0x000333CA
		public int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (this._level != value)
				{
					this._level = value;
					base.OnPropertyChanged(value, "Level");
				}
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x000351E8 File Offset: 0x000333E8
		// (set) Token: 0x0600135E RID: 4958 RVA: 0x000351F0 File Offset: 0x000333F0
		public Widget PerkVisualWidget
		{
			get
			{
				return this._perkVisualWidget;
			}
			set
			{
				if (this._perkVisualWidget != value)
				{
					this._perkVisualWidget = value;
					base.OnPropertyChanged<Widget>(value, "PerkVisualWidget");
				}
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0003520E File Offset: 0x0003340E
		// (set) Token: 0x06001360 RID: 4960 RVA: 0x00035216 File Offset: 0x00033416
		public int PerkState
		{
			get
			{
				return this._perkState;
			}
			set
			{
				if (this._perkState != value)
				{
					this._perkState = value;
					base.OnPropertyChanged(value, "PerkState");
					this.UpdatePerkStateVisual(this.PerkState);
				}
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x00035240 File Offset: 0x00033440
		// (set) Token: 0x06001362 RID: 4962 RVA: 0x00035248 File Offset: 0x00033448
		public int AlternativeType
		{
			get
			{
				return this._alternativeType;
			}
			set
			{
				if (this._alternativeType != value)
				{
					this._alternativeType = value;
					base.OnPropertyChanged(value, "AlternativeType");
				}
			}
		}

		// Token: 0x040008CE RID: 2254
		private PerkItemButtonWidget.AnimState _animState;

		// Token: 0x040008CF RID: 2255
		private int _tickCount;

		// Token: 0x040008D0 RID: 2256
		private bool _isSelectable;

		// Token: 0x040008D1 RID: 2257
		private int _level;

		// Token: 0x040008D2 RID: 2258
		private int _alternativeType;

		// Token: 0x040008D3 RID: 2259
		private int _perkState = -1;

		// Token: 0x040008D4 RID: 2260
		private Widget _perkVisualWidget;

		// Token: 0x020001BC RID: 444
		public enum AnimState
		{
			// Token: 0x040009EE RID: 2542
			Idle,
			// Token: 0x040009EF RID: 2543
			Start,
			// Token: 0x040009F0 RID: 2544
			Starting,
			// Token: 0x040009F1 RID: 2545
			Playing
		}
	}
}
