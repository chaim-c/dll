using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.EscapeMenu
{
	// Token: 0x02000149 RID: 329
	public class EscapeMenuButtonWidget : ButtonWidget
	{
		// Token: 0x06001190 RID: 4496 RVA: 0x00030D77 File Offset: 0x0002EF77
		public EscapeMenuButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00030D80 File Offset: 0x0002EF80
		private void PositiveBehavioredStateUpdated()
		{
			if (this.IsPositiveBehaviored)
			{
				base.Brush = this.PositiveBehaviorBrush;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x00030D96 File Offset: 0x0002EF96
		// (set) Token: 0x06001193 RID: 4499 RVA: 0x00030D9E File Offset: 0x0002EF9E
		[Editor(false)]
		public bool IsPositiveBehaviored
		{
			get
			{
				return this._isPositiveBehaviored;
			}
			set
			{
				if (this._isPositiveBehaviored != value)
				{
					this._isPositiveBehaviored = value;
					base.OnPropertyChanged(value, "IsPositiveBehaviored");
					this.PositiveBehavioredStateUpdated();
				}
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00030DC2 File Offset: 0x0002EFC2
		// (set) Token: 0x06001195 RID: 4501 RVA: 0x00030DCA File Offset: 0x0002EFCA
		[Editor(false)]
		public Brush PositiveBehaviorBrush
		{
			get
			{
				return this._positiveBehaviorBrush;
			}
			set
			{
				if (this._positiveBehaviorBrush != value)
				{
					this._positiveBehaviorBrush = value;
					base.OnPropertyChanged<Brush>(value, "PositiveBehaviorBrush");
				}
			}
		}

		// Token: 0x0400080C RID: 2060
		private bool _isPositiveBehaviored;

		// Token: 0x0400080D RID: 2061
		private Brush _positiveBehaviorBrush;
	}
}
