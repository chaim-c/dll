using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000D1 RID: 209
	public class CompassMarkerTextWidget : TextWidget
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x0001E471 File Offset: 0x0001C671
		public CompassMarkerTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0001E47A File Offset: 0x0001C67A
		private void UpdateBrush()
		{
			if (this.PrimaryBrush != null && this.SecondaryBrush != null)
			{
				base.Brush = (this.IsPrimary ? this.PrimaryBrush : this.SecondaryBrush);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		public bool IsPrimary
		{
			get
			{
				return this._isPrimary;
			}
			set
			{
				if (this._isPrimary != value)
				{
					this._isPrimary = value;
					base.OnPropertyChanged(value, "IsPrimary");
					this.UpdateBrush();
				}
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x0001E4DC File Offset: 0x0001C6DC
		public float Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (Math.Abs(this._position - value) > 1E-45f)
				{
					this._position = value;
					base.OnPropertyChanged(value, "Position");
				}
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0001E505 File Offset: 0x0001C705
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x0001E50D File Offset: 0x0001C70D
		public Brush PrimaryBrush
		{
			get
			{
				return this._primaryBrush;
			}
			set
			{
				if (this._primaryBrush != value)
				{
					this._primaryBrush = value;
					base.OnPropertyChanged<Brush>(value, "PrimaryBrush");
					this.UpdateBrush();
				}
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001E531 File Offset: 0x0001C731
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x0001E539 File Offset: 0x0001C739
		public Brush SecondaryBrush
		{
			get
			{
				return this._secondaryBrush;
			}
			set
			{
				if (this._secondaryBrush != value)
				{
					this._secondaryBrush = value;
					base.OnPropertyChanged<Brush>(value, "SecondaryBrush");
					this.UpdateBrush();
				}
			}
		}

		// Token: 0x040004E4 RID: 1252
		private bool _isPrimary;

		// Token: 0x040004E5 RID: 1253
		private float _position;

		// Token: 0x040004E6 RID: 1254
		private Brush _primaryBrush;

		// Token: 0x040004E7 RID: 1255
		private Brush _secondaryBrush;
	}
}
