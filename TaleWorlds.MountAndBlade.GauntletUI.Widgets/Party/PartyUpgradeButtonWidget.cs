using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x02000064 RID: 100
	public class PartyUpgradeButtonWidget : ButtonWidget
	{
		// Token: 0x06000565 RID: 1381 RVA: 0x000106BF File Offset: 0x0000E8BF
		public PartyUpgradeButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000106C8 File Offset: 0x0000E8C8
		private void UpdateVisual()
		{
			if (this.ImageIdentifierWidget == null || this.UnavailableBrush == null || this.InsufficientBrush == null)
			{
				return;
			}
			if (!this.IsAvailable)
			{
				this.ImageIdentifierWidget.Brush.GlobalColor = new Color(1f, 1f, 1f, 1f);
				this.ImageIdentifierWidget.Brush.SaturationFactor = -100f;
				base.IsEnabled = true;
				base.Brush = this.UnavailableBrush;
				return;
			}
			if (this.IsAvailable && this.IsInsufficient)
			{
				this.ImageIdentifierWidget.Brush.GlobalColor = new Color(0.9f, 0.5f, 0.5f, 1f);
				this.ImageIdentifierWidget.Brush.SaturationFactor = -150f;
				base.IsEnabled = true;
				base.Brush = this.InsufficientBrush;
				return;
			}
			this.ImageIdentifierWidget.Brush.GlobalColor = new Color(1f, 1f, 1f, 1f);
			this.ImageIdentifierWidget.Brush.SaturationFactor = 0f;
			base.IsEnabled = true;
			base.Brush = this.DefaultBrush;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x000107FB File Offset: 0x0000E9FB
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x00010803 File Offset: 0x0000EA03
		[Editor(false)]
		public ImageIdentifierWidget ImageIdentifierWidget
		{
			get
			{
				return this._imageIdentifierWidget;
			}
			set
			{
				if (this._imageIdentifierWidget != value)
				{
					this._imageIdentifierWidget = value;
					base.OnPropertyChanged<ImageIdentifierWidget>(value, "ImageIdentifierWidget");
				}
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00010821 File Offset: 0x0000EA21
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x00010829 File Offset: 0x0000EA29
		[Editor(false)]
		public Brush DefaultBrush
		{
			get
			{
				return this._defaultBrush;
			}
			set
			{
				if (this._defaultBrush != value)
				{
					this._defaultBrush = value;
					base.OnPropertyChanged<Brush>(value, "DefaultBrush");
				}
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x00010847 File Offset: 0x0000EA47
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0001084F File Offset: 0x0000EA4F
		[Editor(false)]
		public Brush UnavailableBrush
		{
			get
			{
				return this._unavailableBrush;
			}
			set
			{
				if (this._unavailableBrush != value)
				{
					this._unavailableBrush = value;
					base.OnPropertyChanged<Brush>(value, "UnavailableBrush");
				}
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0001086D File Offset: 0x0000EA6D
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x00010875 File Offset: 0x0000EA75
		[Editor(false)]
		public Brush InsufficientBrush
		{
			get
			{
				return this._insufficientBrush;
			}
			set
			{
				if (this._insufficientBrush != value)
				{
					this._insufficientBrush = value;
					base.OnPropertyChanged<Brush>(value, "InsufficientBrush");
				}
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00010893 File Offset: 0x0000EA93
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0001089B File Offset: 0x0000EA9B
		[Editor(false)]
		public bool IsAvailable
		{
			get
			{
				return this._isAvailable;
			}
			set
			{
				if (this._isAvailable != value)
				{
					this._isAvailable = value;
					base.OnPropertyChanged(value, "IsAvailable");
				}
				this.UpdateVisual();
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000108BF File Offset: 0x0000EABF
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x000108C7 File Offset: 0x0000EAC7
		[Editor(false)]
		public bool IsInsufficient
		{
			get
			{
				return this._isInsufficient;
			}
			set
			{
				if (this._isInsufficient != value)
				{
					this._isInsufficient = value;
					base.OnPropertyChanged(value, "IsInsufficient");
				}
				this.UpdateVisual();
			}
		}

		// Token: 0x04000255 RID: 597
		private ImageIdentifierWidget _imageIdentifierWidget;

		// Token: 0x04000256 RID: 598
		private Brush _defaultBrush;

		// Token: 0x04000257 RID: 599
		private Brush _unavailableBrush;

		// Token: 0x04000258 RID: 600
		private Brush _insufficientBrush;

		// Token: 0x04000259 RID: 601
		private bool _isAvailable;

		// Token: 0x0400025A RID: 602
		private bool _isInsufficient;
	}
}
