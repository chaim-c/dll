using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer
{
	// Token: 0x0200007D RID: 125
	public class MaterialValueOffsetTextWidget : TextWidget
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x00014FC4 File Offset: 0x000131C4
		public MaterialValueOffsetTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00014FD0 File Offset: 0x000131D0
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._visualDirty)
			{
				base.Brush.TextValueFactor += this.ValueOffset;
				base.Brush.TextSaturationFactor += this.SaturationOffset;
				base.Brush.TextHueFactor += this.HueOffset;
				foreach (Style style in base.Brush.Styles)
				{
					style.TextValueFactor += this.ValueOffset;
					style.TextSaturationFactor += this.SaturationOffset;
					style.TextHueFactor += this.HueOffset;
				}
				this._visualDirty = false;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x000150BC File Offset: 0x000132BC
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x000150C4 File Offset: 0x000132C4
		public float ValueOffset
		{
			get
			{
				return this._valueOffset;
			}
			set
			{
				if (this._valueOffset != value)
				{
					this._valueOffset = value;
					this._visualDirty = true;
				}
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000150DD File Offset: 0x000132DD
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x000150E5 File Offset: 0x000132E5
		public float SaturationOffset
		{
			get
			{
				return this._saturationOffset;
			}
			set
			{
				if (this._saturationOffset != value)
				{
					this._saturationOffset = value;
					this._visualDirty = true;
				}
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x000150FE File Offset: 0x000132FE
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x00015106 File Offset: 0x00013306
		public float HueOffset
		{
			get
			{
				return this._hueOffset;
			}
			set
			{
				if (this._hueOffset != value)
				{
					this._hueOffset = value;
					this._visualDirty = true;
				}
			}
		}

		// Token: 0x0400031B RID: 795
		private bool _visualDirty;

		// Token: 0x0400031C RID: 796
		private float _valueOffset;

		// Token: 0x0400031D RID: 797
		private float _saturationOffset;

		// Token: 0x0400031E RID: 798
		private float _hueOffset;
	}
}
