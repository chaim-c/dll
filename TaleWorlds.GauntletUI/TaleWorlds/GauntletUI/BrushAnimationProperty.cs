using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200000D RID: 13
	public class BrushAnimationProperty
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003A5A File Offset: 0x00001C5A
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003A62 File Offset: 0x00001C62
		public string LayerName { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003A6B File Offset: 0x00001C6B
		public IEnumerable<BrushAnimationKeyFrame> KeyFrames
		{
			get
			{
				return this._keyFrames.AsReadOnly();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003A78 File Offset: 0x00001C78
		public int Count
		{
			get
			{
				return this._keyFrames.Count;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003A85 File Offset: 0x00001C85
		public BrushAnimationProperty()
		{
			this._keyFrames = new List<BrushAnimationKeyFrame>();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003A98 File Offset: 0x00001C98
		public BrushAnimationKeyFrame GetFrameAfter(float time)
		{
			for (int i = 0; i < this._keyFrames.Count; i++)
			{
				BrushAnimationKeyFrame brushAnimationKeyFrame = this._keyFrames[i];
				if (time < brushAnimationKeyFrame.Time)
				{
					return brushAnimationKeyFrame;
				}
			}
			return null;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public BrushAnimationKeyFrame GetFrameAt(int i)
		{
			if (i >= 0 && i < this._keyFrames.Count)
			{
				return this._keyFrames[i];
			}
			return null;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003AF6 File Offset: 0x00001CF6
		public BrushAnimationProperty Clone()
		{
			BrushAnimationProperty brushAnimationProperty = new BrushAnimationProperty();
			brushAnimationProperty.FillFrom(this);
			return brushAnimationProperty;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003B04 File Offset: 0x00001D04
		private void FillFrom(BrushAnimationProperty collection)
		{
			this.PropertyType = collection.PropertyType;
			this._keyFrames = new List<BrushAnimationKeyFrame>(collection._keyFrames.Count);
			for (int i = 0; i < collection._keyFrames.Count; i++)
			{
				BrushAnimationKeyFrame item = collection._keyFrames[i].Clone();
				this._keyFrames.Add(item);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003B68 File Offset: 0x00001D68
		public void AddKeyFrame(BrushAnimationKeyFrame keyFrame)
		{
			this._keyFrames.Add(keyFrame);
			this._keyFrames = (from k in this._keyFrames
			orderby k.Time
			select k).ToList<BrushAnimationKeyFrame>();
			for (int i = 0; i < this._keyFrames.Count; i++)
			{
				this._keyFrames[i].InitializeIndex(i);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003BDE File Offset: 0x00001DDE
		public void RemoveKeyFrame(BrushAnimationKeyFrame keyFrame)
		{
			this._keyFrames.Remove(keyFrame);
		}

		// Token: 0x0400003A RID: 58
		public BrushAnimationProperty.BrushAnimationPropertyType PropertyType;

		// Token: 0x0400003B RID: 59
		private List<BrushAnimationKeyFrame> _keyFrames;

		// Token: 0x02000077 RID: 119
		public enum BrushAnimationPropertyType
		{
			// Token: 0x040003ED RID: 1005
			Name,
			// Token: 0x040003EE RID: 1006
			ColorFactor,
			// Token: 0x040003EF RID: 1007
			Color,
			// Token: 0x040003F0 RID: 1008
			AlphaFactor,
			// Token: 0x040003F1 RID: 1009
			HueFactor,
			// Token: 0x040003F2 RID: 1010
			SaturationFactor,
			// Token: 0x040003F3 RID: 1011
			ValueFactor,
			// Token: 0x040003F4 RID: 1012
			FontColor,
			// Token: 0x040003F5 RID: 1013
			OverlayXOffset,
			// Token: 0x040003F6 RID: 1014
			OverlayYOffset,
			// Token: 0x040003F7 RID: 1015
			TextGlowColor,
			// Token: 0x040003F8 RID: 1016
			TextOutlineColor,
			// Token: 0x040003F9 RID: 1017
			TextOutlineAmount,
			// Token: 0x040003FA RID: 1018
			TextGlowRadius,
			// Token: 0x040003FB RID: 1019
			TextBlur,
			// Token: 0x040003FC RID: 1020
			TextShadowOffset,
			// Token: 0x040003FD RID: 1021
			TextShadowAngle,
			// Token: 0x040003FE RID: 1022
			TextColorFactor,
			// Token: 0x040003FF RID: 1023
			TextAlphaFactor,
			// Token: 0x04000400 RID: 1024
			TextHueFactor,
			// Token: 0x04000401 RID: 1025
			TextSaturationFactor,
			// Token: 0x04000402 RID: 1026
			TextValueFactor,
			// Token: 0x04000403 RID: 1027
			Sprite,
			// Token: 0x04000404 RID: 1028
			IsHidden,
			// Token: 0x04000405 RID: 1029
			XOffset,
			// Token: 0x04000406 RID: 1030
			YOffset,
			// Token: 0x04000407 RID: 1031
			OverridenWidth,
			// Token: 0x04000408 RID: 1032
			OverridenHeight,
			// Token: 0x04000409 RID: 1033
			WidthPolicy,
			// Token: 0x0400040A RID: 1034
			HeightPolicy,
			// Token: 0x0400040B RID: 1035
			HorizontalFlip,
			// Token: 0x0400040C RID: 1036
			VerticalFlip,
			// Token: 0x0400040D RID: 1037
			OverlayMethod,
			// Token: 0x0400040E RID: 1038
			OverlaySprite,
			// Token: 0x0400040F RID: 1039
			ExtendLeft,
			// Token: 0x04000410 RID: 1040
			ExtendRight,
			// Token: 0x04000411 RID: 1041
			ExtendTop,
			// Token: 0x04000412 RID: 1042
			ExtendBottom,
			// Token: 0x04000413 RID: 1043
			UseRandomBaseOverlayXOffset,
			// Token: 0x04000414 RID: 1044
			UseRandomBaseOverlayYOffset,
			// Token: 0x04000415 RID: 1045
			Font,
			// Token: 0x04000416 RID: 1046
			FontStyle,
			// Token: 0x04000417 RID: 1047
			FontSize
		}
	}
}
