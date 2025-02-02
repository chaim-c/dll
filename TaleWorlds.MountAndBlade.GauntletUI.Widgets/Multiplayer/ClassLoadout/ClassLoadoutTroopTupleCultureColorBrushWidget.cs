using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.ClassLoadout
{
	// Token: 0x020000C3 RID: 195
	public class ClassLoadoutTroopTupleCultureColorBrushWidget : BrushWidget
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x0001D2AF File Offset: 0x0001B4AF
		public ClassLoadoutTroopTupleCultureColorBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0001D2B8 File Offset: 0x0001B4B8
		private void UpdateColor()
		{
			foreach (Style style in base.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].Color = this.CultureColor;
				}
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0001D32C File Offset: 0x0001B52C
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x0001D334 File Offset: 0x0001B534
		public Color CultureColor
		{
			get
			{
				return this._cultureColor;
			}
			set
			{
				if (value != this._cultureColor)
				{
					this._cultureColor = value;
					base.OnPropertyChanged(value, "CultureColor");
					this.UpdateColor();
				}
			}
		}

		// Token: 0x040004BA RID: 1210
		private Color _cultureColor;
	}
}
