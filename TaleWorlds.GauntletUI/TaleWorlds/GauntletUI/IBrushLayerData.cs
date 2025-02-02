using System;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000027 RID: 39
	public interface IBrushLayerData
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002E3 RID: 739
		// (set) Token: 0x060002E4 RID: 740
		string Name { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002E5 RID: 741
		// (set) Token: 0x060002E6 RID: 742
		Sprite Sprite { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002E7 RID: 743
		// (set) Token: 0x060002E8 RID: 744
		Color Color { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002E9 RID: 745
		// (set) Token: 0x060002EA RID: 746
		float ColorFactor { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002EB RID: 747
		// (set) Token: 0x060002EC RID: 748
		float AlphaFactor { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002ED RID: 749
		// (set) Token: 0x060002EE RID: 750
		float HueFactor { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002EF RID: 751
		// (set) Token: 0x060002F0 RID: 752
		float SaturationFactor { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002F1 RID: 753
		// (set) Token: 0x060002F2 RID: 754
		float ValueFactor { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002F3 RID: 755
		// (set) Token: 0x060002F4 RID: 756
		bool IsHidden { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002F5 RID: 757
		// (set) Token: 0x060002F6 RID: 758
		float XOffset { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002F7 RID: 759
		// (set) Token: 0x060002F8 RID: 760
		float YOffset { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002F9 RID: 761
		// (set) Token: 0x060002FA RID: 762
		float ExtendLeft { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002FB RID: 763
		// (set) Token: 0x060002FC RID: 764
		float ExtendRight { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002FD RID: 765
		// (set) Token: 0x060002FE RID: 766
		float ExtendTop { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002FF RID: 767
		// (set) Token: 0x06000300 RID: 768
		float ExtendBottom { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000301 RID: 769
		// (set) Token: 0x06000302 RID: 770
		float OverridenWidth { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000303 RID: 771
		// (set) Token: 0x06000304 RID: 772
		float OverridenHeight { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000305 RID: 773
		// (set) Token: 0x06000306 RID: 774
		BrushLayerSizePolicy WidthPolicy { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000307 RID: 775
		// (set) Token: 0x06000308 RID: 776
		BrushLayerSizePolicy HeightPolicy { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000309 RID: 777
		// (set) Token: 0x0600030A RID: 778
		bool HorizontalFlip { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600030B RID: 779
		// (set) Token: 0x0600030C RID: 780
		bool VerticalFlip { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600030D RID: 781
		// (set) Token: 0x0600030E RID: 782
		bool UseOverlayAlphaAsMask { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600030F RID: 783
		// (set) Token: 0x06000310 RID: 784
		BrushOverlayMethod OverlayMethod { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000311 RID: 785
		// (set) Token: 0x06000312 RID: 786
		Sprite OverlaySprite { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000313 RID: 787
		// (set) Token: 0x06000314 RID: 788
		float OverlayXOffset { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000315 RID: 789
		// (set) Token: 0x06000316 RID: 790
		float OverlayYOffset { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000317 RID: 791
		// (set) Token: 0x06000318 RID: 792
		bool UseRandomBaseOverlayXOffset { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000319 RID: 793
		// (set) Token: 0x0600031A RID: 794
		bool UseRandomBaseOverlayYOffset { get; set; }

		// Token: 0x0600031B RID: 795
		float GetValueAsFloat(BrushAnimationProperty.BrushAnimationPropertyType propertyType);

		// Token: 0x0600031C RID: 796
		Color GetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType);

		// Token: 0x0600031D RID: 797
		Sprite GetValueAsSprite(BrushAnimationProperty.BrushAnimationPropertyType propertyType);
	}
}
