using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000039 RID: 57
	public class ScoreboardAnimatedTextWidget : TextWidget
	{
		// Token: 0x06000331 RID: 817 RVA: 0x0000A626 File Offset: 0x00008826
		public ScoreboardAnimatedTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000A62F File Offset: 0x0000882F
		private void HandleValueChanged(int value)
		{
			base.Text = ((!this.ShowZero && value == 0) ? "" : value.ToString());
			base.BrushRenderer.RestartAnimation();
			base.RegisterUpdateBrushes();
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000A661 File Offset: 0x00008861
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000A669 File Offset: 0x00008869
		[Editor(false)]
		public int ValueAsInt
		{
			get
			{
				return this._valueAsInt;
			}
			set
			{
				if (value != this._valueAsInt)
				{
					this._valueAsInt = value;
					base.OnPropertyChanged(value, "ValueAsInt");
					this.HandleValueChanged(value);
				}
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000A68E File Offset: 0x0000888E
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000A696 File Offset: 0x00008896
		[Editor(false)]
		public bool ShowZero
		{
			get
			{
				return this._showZero;
			}
			set
			{
				if (this._showZero != value)
				{
					this._showZero = value;
					base.OnPropertyChanged(value, "ShowZero");
					this.HandleValueChanged(this._valueAsInt);
				}
			}
		}

		// Token: 0x04000151 RID: 337
		private bool _showZero;

		// Token: 0x04000152 RID: 338
		private int _valueAsInt;
	}
}
