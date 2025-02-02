using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.OrderOfBattle
{
	// Token: 0x020000E1 RID: 225
	public class OrderOfBattleFormationClassLockBrushWidget : BrushWidget
	{
		// Token: 0x06000BAA RID: 2986 RVA: 0x00020504 File Offset: 0x0001E704
		public OrderOfBattleFormationClassLockBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002050D File Offset: 0x0001E70D
		private void OnLockStateSet()
		{
			if (this.IsLocked)
			{
				base.Brush = this.LockedBrush;
			}
			else
			{
				base.Brush = this.UnlockedBrush;
			}
			this._isInitialStateSet = true;
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00020538 File Offset: 0x0001E738
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x00020540 File Offset: 0x0001E740
		[Editor(false)]
		public bool IsLocked
		{
			get
			{
				return this._isLocked;
			}
			set
			{
				if (value != this._isLocked || !this._isInitialStateSet)
				{
					this._isLocked = value;
					base.OnPropertyChanged(value, "IsLocked");
					this.OnLockStateSet();
				}
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0002056C File Offset: 0x0001E76C
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x00020574 File Offset: 0x0001E774
		[Editor(false)]
		public Brush LockedBrush
		{
			get
			{
				return this._lockedBrush;
			}
			set
			{
				if (value != this._lockedBrush)
				{
					this._lockedBrush = value;
					base.OnPropertyChanged<Brush>(value, "LockedBrush");
				}
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x00020592 File Offset: 0x0001E792
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x0002059A File Offset: 0x0001E79A
		[Editor(false)]
		public Brush UnlockedBrush
		{
			get
			{
				return this._unlockedBrush;
			}
			set
			{
				if (value != this._unlockedBrush)
				{
					this._unlockedBrush = value;
					base.OnPropertyChanged<Brush>(value, "UnlockedBrush");
				}
			}
		}

		// Token: 0x0400054E RID: 1358
		private bool _isInitialStateSet;

		// Token: 0x0400054F RID: 1359
		private bool _isLocked;

		// Token: 0x04000550 RID: 1360
		private Brush _lockedBrush;

		// Token: 0x04000551 RID: 1361
		private Brush _unlockedBrush;
	}
}
