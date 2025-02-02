using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Barter
{
	// Token: 0x02000185 RID: 389
	public class BarterTupleItemButtonWidget : ButtonWidget
	{
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x00036BAC File Offset: 0x00034DAC
		// (set) Token: 0x060013F9 RID: 5113 RVA: 0x00036BB4 File Offset: 0x00034DB4
		public ListPanel SliderParentList { get; set; }

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x00036BBD File Offset: 0x00034DBD
		// (set) Token: 0x060013FB RID: 5115 RVA: 0x00036BC5 File Offset: 0x00034DC5
		public TextWidget CountText { get; set; }

		// Token: 0x060013FC RID: 5116 RVA: 0x00036BCE File Offset: 0x00034DCE
		public BarterTupleItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00036BD7 File Offset: 0x00034DD7
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._initialized)
			{
				this.Refresh();
				this._initialized = true;
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00036BF8 File Offset: 0x00034DF8
		private void Refresh()
		{
			this.SliderParentList.IsVisible = (this.IsMultiple && this.IsOffered);
			this.CountText.IsHidden = (this.IsMultiple && this.IsOffered);
			base.IsSelected = this.IsOffered;
			base.DoNotAcceptEvents = this.IsOffered;
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00036C55 File Offset: 0x00034E55
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x00036C5D File Offset: 0x00034E5D
		[Editor(false)]
		public bool IsMultiple
		{
			get
			{
				return this._isMultiple;
			}
			set
			{
				if (this._isMultiple != value)
				{
					this._isMultiple = value;
					base.OnPropertyChanged(value, "IsMultiple");
					this.Refresh();
				}
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x00036C81 File Offset: 0x00034E81
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x00036C89 File Offset: 0x00034E89
		[Editor(false)]
		public bool IsOffered
		{
			get
			{
				return this._isOffered;
			}
			set
			{
				if (this._isOffered != value)
				{
					this._isOffered = value;
					base.OnPropertyChanged(value, "IsOffered");
					this.Refresh();
				}
			}
		}

		// Token: 0x0400091A RID: 2330
		private bool _initialized;

		// Token: 0x0400091B RID: 2331
		private bool _isMultiple;

		// Token: 0x0400091C RID: 2332
		private bool _isOffered;
	}
}
