using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000AC RID: 172
	public class MultiplayerArmoryCosmeticsSectionWidget : Widget
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x00019D9A File Offset: 0x00017F9A
		public MultiplayerArmoryCosmeticsSectionWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00019DA4 File Offset: 0x00017FA4
		private void AnimateTauntAssignmentStates(float dt)
		{
			this._tauntAssignmentStateTimer += dt;
			float amount;
			if (this._tauntAssignmentStateTimer < this.TauntAssignmentStateAnimationDuration)
			{
				amount = this._tauntAssignmentStateTimer / this.TauntAssignmentStateAnimationDuration;
				base.EventManager.AddLateUpdateAction(this, new Action<float>(this.AnimateTauntAssignmentStates), 1);
			}
			else
			{
				amount = 1f;
			}
			float valueFrom = this.IsTauntAssignmentActive ? 1f : this.TauntAssignmentStateAlpha;
			float valueTo = this.IsTauntAssignmentActive ? this.TauntAssignmentStateAlpha : 1f;
			float alpha = MathF.Lerp(valueFrom, valueTo, amount, 1E-05f);
			this.SetWidgetAlpha(this.TopSectionParent, alpha);
			this.SetWidgetAlpha(this.BottomSectionParent, alpha);
			this.SetWidgetAlpha(this.SortControlsParent, alpha);
			this.SetWidgetAlpha(this.CategorySeparatorWidget, alpha);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00019E68 File Offset: 0x00018068
		private void SetWidgetAlpha(Widget widget, float alpha)
		{
			if (widget != null)
			{
				widget.IsVisible = (alpha != 0f);
				widget.SetGlobalAlphaRecursively(alpha);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x00019E85 File Offset: 0x00018085
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x00019E90 File Offset: 0x00018090
		public bool IsTauntAssignmentActive
		{
			get
			{
				return this._isTauntAssignmentActive;
			}
			set
			{
				if (value != this._isTauntAssignmentActive)
				{
					this._isTauntAssignmentActive = value;
					base.OnPropertyChanged(value, "IsTauntAssignmentActive");
					this._tauntAssignmentStateTimer = 0f;
					base.EventManager.AddLateUpdateAction(this, new Action<float>(this.AnimateTauntAssignmentStates), 1);
				}
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00019EDD File Offset: 0x000180DD
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x00019EE5 File Offset: 0x000180E5
		public float TauntAssignmentStateAnimationDuration
		{
			get
			{
				return this._tauntAssignmentStateAnimationDuration;
			}
			set
			{
				if (value != this._tauntAssignmentStateAnimationDuration)
				{
					this._tauntAssignmentStateAnimationDuration = value;
					base.OnPropertyChanged(value, "TauntAssignmentStateAnimationDuration");
				}
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x00019F03 File Offset: 0x00018103
		// (set) Token: 0x06000915 RID: 2325 RVA: 0x00019F0B File Offset: 0x0001810B
		public float TauntAssignmentStateAlpha
		{
			get
			{
				return this._tauntAssignmentStateAlpha;
			}
			set
			{
				if (value != this._tauntAssignmentStateAlpha)
				{
					this._tauntAssignmentStateAlpha = value;
					base.OnPropertyChanged(value, "TauntAssignmentStateAlpha");
				}
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x00019F29 File Offset: 0x00018129
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x00019F31 File Offset: 0x00018131
		public Widget TopSectionParent
		{
			get
			{
				return this._topSectionParent;
			}
			set
			{
				if (value != this._topSectionParent)
				{
					this._topSectionParent = value;
					base.OnPropertyChanged<Widget>(value, "TopSectionParent");
				}
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x00019F4F File Offset: 0x0001814F
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x00019F57 File Offset: 0x00018157
		public Widget BottomSectionParent
		{
			get
			{
				return this._bottomSectionParent;
			}
			set
			{
				if (value != this._bottomSectionParent)
				{
					this._bottomSectionParent = value;
					base.OnPropertyChanged<Widget>(value, "BottomSectionParent");
				}
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00019F75 File Offset: 0x00018175
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x00019F7D File Offset: 0x0001817D
		public Widget SortControlsParent
		{
			get
			{
				return this._sortControlsParent;
			}
			set
			{
				if (value != this._sortControlsParent)
				{
					this._sortControlsParent = value;
					base.OnPropertyChanged<Widget>(value, "SortControlsParent");
				}
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00019F9B File Offset: 0x0001819B
		// (set) Token: 0x0600091D RID: 2333 RVA: 0x00019FA3 File Offset: 0x000181A3
		public Widget CategorySeparatorWidget
		{
			get
			{
				return this._categorySeparatorWidget;
			}
			set
			{
				if (value != this._categorySeparatorWidget)
				{
					this._categorySeparatorWidget = value;
					base.OnPropertyChanged<Widget>(value, "CategorySeparatorWidget");
				}
			}
		}

		// Token: 0x0400041F RID: 1055
		private float _tauntAssignmentStateTimer;

		// Token: 0x04000420 RID: 1056
		private bool _isTauntAssignmentActive;

		// Token: 0x04000421 RID: 1057
		private float _tauntAssignmentStateAnimationDuration;

		// Token: 0x04000422 RID: 1058
		private float _tauntAssignmentStateAlpha;

		// Token: 0x04000423 RID: 1059
		private Widget _topSectionParent;

		// Token: 0x04000424 RID: 1060
		private Widget _bottomSectionParent;

		// Token: 0x04000425 RID: 1061
		private Widget _sortControlsParent;

		// Token: 0x04000426 RID: 1062
		private Widget _categorySeparatorWidget;
	}
}
