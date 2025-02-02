using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Order
{
	// Token: 0x0200006D RID: 109
	public class OrderTroopItemBrushWidget : BrushWidget
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x00011884 File Offset: 0x0000FA84
		public Widget SelectionFrameWidget { get; set; }

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001188D File Offset: 0x0000FA8D
		public OrderTroopItemBrushWidget(UIContext context) : base(context)
		{
			base.AddState("Selected");
			base.AddState("Disabled");
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000118B3 File Offset: 0x0000FAB3
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.SelectionFrameWidget != null)
			{
				this.SelectionFrameWidget.IsVisible = (base.EventManager.IsControllerActive && this.IsSelectionActive);
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000118E5 File Offset: 0x0000FAE5
		private void SelectionStateChanged()
		{
			this.UpdateBackgroundState();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000118ED File Offset: 0x0000FAED
		private void SelectableStateChanged()
		{
			this.UpdateBackgroundState();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000118F5 File Offset: 0x0000FAF5
		private void CurrentMemberCountChanged()
		{
			this.UpdateBackgroundState();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000118FD File Offset: 0x0000FAFD
		private void UpdateBackgroundState()
		{
			if (this.CurrentMemberCount <= 0 || !this.IsSelectable)
			{
				this.SetState("Disabled");
				return;
			}
			this.SetState(this.IsSelected ? "Selected" : "Default");
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00011936 File Offset: 0x0000FB36
		private void UpdateBrush()
		{
			if (this.MeleeCardBrush == null || this.RangedCardBrush == null)
			{
				return;
			}
			if (this.HasAmmo)
			{
				base.Brush = this.RangedCardBrush;
				return;
			}
			base.Brush = this.MeleeCardBrush;
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001196A File Offset: 0x0000FB6A
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00011972 File Offset: 0x0000FB72
		[Editor(false)]
		public int CurrentMemberCount
		{
			get
			{
				return this._currentMemberCount;
			}
			set
			{
				if (this._currentMemberCount != value)
				{
					this._currentMemberCount = value;
					base.OnPropertyChanged(value, "CurrentMemberCount");
					this.CurrentMemberCountChanged();
				}
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00011996 File Offset: 0x0000FB96
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x0001199E File Offset: 0x0000FB9E
		[Editor(false)]
		public bool IsSelectable
		{
			get
			{
				return this._isSelectable;
			}
			set
			{
				if (this._isSelectable != value)
				{
					this._isSelectable = value;
					base.OnPropertyChanged(value, "IsSelectable");
					this.SelectableStateChanged();
				}
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x000119C2 File Offset: 0x0000FBC2
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x000119CA File Offset: 0x0000FBCA
		[Editor(false)]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (this._isSelected != value)
				{
					this._isSelected = value;
					base.OnPropertyChanged(value, "IsSelected");
					this.SelectionStateChanged();
				}
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x000119EE File Offset: 0x0000FBEE
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x000119F6 File Offset: 0x0000FBF6
		[Editor(false)]
		public bool IsSelectionActive
		{
			get
			{
				return this._isSelectionActive;
			}
			set
			{
				if (this._isSelectionActive != value)
				{
					this._isSelectionActive = value;
					base.OnPropertyChanged(value, "IsSelectionActive");
				}
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00011A14 File Offset: 0x0000FC14
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x00011A1C File Offset: 0x0000FC1C
		[Editor(false)]
		public bool HasAmmo
		{
			get
			{
				return this._hasAmmo;
			}
			set
			{
				if (this._hasAmmo != value)
				{
					this._hasAmmo = value;
					base.OnPropertyChanged(value, "HasAmmo");
					this.UpdateBrush();
				}
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00011A40 File Offset: 0x0000FC40
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x00011A48 File Offset: 0x0000FC48
		[Editor(false)]
		public Brush RangedCardBrush
		{
			get
			{
				return this._rangedCardBrush;
			}
			set
			{
				if (value != this._rangedCardBrush)
				{
					this._rangedCardBrush = value;
					base.OnPropertyChanged<Brush>(value, "RangedCardBrush");
					this.UpdateBrush();
				}
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00011A6C File Offset: 0x0000FC6C
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x00011A74 File Offset: 0x0000FC74
		[Editor(false)]
		public Brush MeleeCardBrush
		{
			get
			{
				return this._meleeCardBrush;
			}
			set
			{
				if (value != this._meleeCardBrush)
				{
					this._meleeCardBrush = value;
					base.OnPropertyChanged<Brush>(value, "MeleeCardBrush");
					this.UpdateBrush();
				}
			}
		}

		// Token: 0x04000284 RID: 644
		private int _currentMemberCount;

		// Token: 0x04000285 RID: 645
		private bool _isSelectable;

		// Token: 0x04000286 RID: 646
		private bool _isSelected;

		// Token: 0x04000287 RID: 647
		private bool _isSelectionActive;

		// Token: 0x04000288 RID: 648
		private bool _hasAmmo = true;

		// Token: 0x04000289 RID: 649
		private Brush _rangedCardBrush;

		// Token: 0x0400028A RID: 650
		private Brush _meleeCardBrush;
	}
}
