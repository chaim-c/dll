using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x0200007C RID: 124
	public class MaterialValueOffsetImageWidget : ImageWidget
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x00014EB8 File Offset: 0x000130B8
		public MaterialValueOffsetImageWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00014EC4 File Offset: 0x000130C4
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._visualDirty)
			{
				foreach (Style style in base.Brush.Styles)
				{
					foreach (StyleLayer styleLayer in style.GetLayers())
					{
						styleLayer.ValueFactor += this.ValueOffset;
						styleLayer.SaturationFactor += this.SaturationOffset;
						styleLayer.HueFactor += this.HueOffset;
					}
				}
				this._visualDirty = false;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00014F7C File Offset: 0x0001317C
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x00014F84 File Offset: 0x00013184
		public float ValueOffset
		{
			get
			{
				return this._valueOffset;
			}
			set
			{
				this._valueOffset = value;
				this._visualDirty = true;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00014F94 File Offset: 0x00013194
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x00014F9C File Offset: 0x0001319C
		public float SaturationOffset
		{
			get
			{
				return this._saturationOffset;
			}
			set
			{
				this._saturationOffset = value;
				this._visualDirty = true;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00014FAC File Offset: 0x000131AC
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x00014FB4 File Offset: 0x000131B4
		public float HueOffset
		{
			get
			{
				return this._hueOffset;
			}
			set
			{
				this._hueOffset = value;
				this._visualDirty = true;
			}
		}

		// Token: 0x04000317 RID: 791
		private bool _visualDirty;

		// Token: 0x04000318 RID: 792
		private float _valueOffset;

		// Token: 0x04000319 RID: 793
		private float _saturationOffset;

		// Token: 0x0400031A RID: 794
		private float _hueOffset;
	}
}
