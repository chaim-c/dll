using System;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000069 RID: 105
	public class ScrollablePanelFixedHeaderWidget : Widget
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001DFE7 File Offset: 0x0001C1E7
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0001DFEF File Offset: 0x0001C1EF
		public Widget FixedHeader { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001DFF8 File Offset: 0x0001C1F8
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x0001E000 File Offset: 0x0001C200
		public float TopOffset { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001E009 File Offset: 0x0001C209
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0001E011 File Offset: 0x0001C211
		public float BottomOffset { get; set; } = float.MinValue;

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001E01A File Offset: 0x0001C21A
		public ScrollablePanelFixedHeaderWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001E035 File Offset: 0x0001C235
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isDirty)
			{
				base.EventFired("FixedHeaderPropertyChanged", Array.Empty<object>());
				this._isDirty = false;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001E05D File Offset: 0x0001C25D
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x0001E065 File Offset: 0x0001C265
		public float HeaderHeight
		{
			get
			{
				return this._headerHeight;
			}
			set
			{
				if (value != this._headerHeight)
				{
					this._headerHeight = value;
					base.SuggestedHeight = this._headerHeight;
					this._isDirty = true;
				}
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0001E08A File Offset: 0x0001C28A
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x0001E092 File Offset: 0x0001C292
		public float AdditionalTopOffset
		{
			get
			{
				return this._additionalTopOffset;
			}
			set
			{
				if (value != this._additionalTopOffset)
				{
					this._additionalTopOffset = value;
					this._isDirty = true;
				}
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0001E0AB File Offset: 0x0001C2AB
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0001E0B3 File Offset: 0x0001C2B3
		public float AdditionalBottomOffset
		{
			get
			{
				return this._additionalBottomOffset;
			}
			set
			{
				if (value != this._additionalBottomOffset)
				{
					this._additionalBottomOffset = value;
					this._isDirty = true;
				}
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0001E0CC File Offset: 0x0001C2CC
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0001E0D4 File Offset: 0x0001C2D4
		[Editor(false)]
		public bool IsRelevant
		{
			get
			{
				return this._isRelevant;
			}
			set
			{
				if (value != this._isRelevant)
				{
					this._isRelevant = value;
					base.IsVisible = value;
					this._isDirty = true;
					base.OnPropertyChanged(value, "IsRelevant");
				}
			}
		}

		// Token: 0x0400032E RID: 814
		private bool _isDirty;

		// Token: 0x04000332 RID: 818
		private float _headerHeight;

		// Token: 0x04000333 RID: 819
		private float _additionalTopOffset;

		// Token: 0x04000334 RID: 820
		private float _additionalBottomOffset;

		// Token: 0x04000335 RID: 821
		private bool _isRelevant = true;
	}
}
