using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000010 RID: 16
	public class ColorButtonWidget : ButtonWidget
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00003BB3 File Offset: 0x00001DB3
		public ColorButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003BBC File Offset: 0x00001DBC
		private void ApplyStringColorToBrush(string color)
		{
			Color color2 = Color.ConvertStringToColor(color);
			foreach (Style style in base.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].Color = color2;
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003C30 File Offset: 0x00001E30
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003C38 File Offset: 0x00001E38
		[Editor(false)]
		public string ColorToApply
		{
			get
			{
				return this._colorToApply;
			}
			set
			{
				if (this._colorToApply != value)
				{
					this._colorToApply = value;
					base.OnPropertyChanged<string>(value, "ColorToApply");
					if (!string.IsNullOrEmpty(value))
					{
						this.ApplyStringColorToBrush(value);
					}
				}
			}
		}

		// Token: 0x04000058 RID: 88
		private string _colorToApply;
	}
}
