using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.CharacterCreation.Culture
{
	// Token: 0x0200017E RID: 382
	public class CharacterCreationBackgroundGradientBrushWidget : BrushWidget
	{
		// Token: 0x060013BD RID: 5053 RVA: 0x00036021 File Offset: 0x00034221
		public CharacterCreationBackgroundGradientBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0003602C File Offset: 0x0003422C
		private void SetCultureBackground(Color cultureColor1)
		{
			foreach (Style style in base.Brush.Styles)
			{
				StyleLayer[] layers = style.GetLayers();
				for (int i = 0; i < layers.Length; i++)
				{
					layers[i].Color = cultureColor1;
				}
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0003609C File Offset: 0x0003429C
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x000360A4 File Offset: 0x000342A4
		[Editor(false)]
		public Color CultureColor1
		{
			get
			{
				return this._cultureColor1;
			}
			set
			{
				if (this._cultureColor1 != value)
				{
					this._cultureColor1 = value;
					base.OnPropertyChanged(value, "CultureColor1");
					this.SetCultureBackground(value);
				}
			}
		}

		// Token: 0x040008FD RID: 2301
		private Color _cultureColor1;
	}
}
