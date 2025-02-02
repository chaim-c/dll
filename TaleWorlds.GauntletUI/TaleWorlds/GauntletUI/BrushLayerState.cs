using System;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000014 RID: 20
	public struct BrushLayerState : IBrushAnimationState, IDataSource
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00006C10 File Offset: 0x00004E10
		public void FillFrom(IBrushLayerData styleLayer)
		{
			this.ColorFactor = styleLayer.ColorFactor;
			this.AlphaFactor = styleLayer.AlphaFactor;
			this.HueFactor = styleLayer.HueFactor;
			this.SaturationFactor = styleLayer.SaturationFactor;
			this.ValueFactor = styleLayer.ValueFactor;
			this.Color = styleLayer.Color;
			this.OverlayXOffset = styleLayer.OverlayXOffset;
			this.OverlayYOffset = styleLayer.OverlayYOffset;
			this.XOffset = styleLayer.XOffset;
			this.YOffset = styleLayer.YOffset;
			this.Sprite = styleLayer.Sprite;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006CA4 File Offset: 0x00004EA4
		void IBrushAnimationState.FillFrom(IDataSource source)
		{
			StyleLayer styleLayer = (StyleLayer)source;
			this.FillFrom(styleLayer);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006CC0 File Offset: 0x00004EC0
		void IBrushAnimationState.LerpFrom(IBrushAnimationState start, IDataSource end, float ratio)
		{
			BrushLayerState start2 = (BrushLayerState)start;
			IBrushLayerData end2 = (IBrushLayerData)end;
			this.LerpFrom(start2, end2, ratio);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006CE4 File Offset: 0x00004EE4
		public void LerpFrom(BrushLayerState start, IBrushLayerData end, float ratio)
		{
			this.ColorFactor = Mathf.Lerp(start.ColorFactor, end.ColorFactor, ratio);
			this.AlphaFactor = Mathf.Lerp(start.AlphaFactor, end.AlphaFactor, ratio);
			this.HueFactor = Mathf.Lerp(start.HueFactor, end.HueFactor, ratio);
			this.SaturationFactor = Mathf.Lerp(start.SaturationFactor, end.SaturationFactor, ratio);
			this.ValueFactor = Mathf.Lerp(start.ValueFactor, end.ValueFactor, ratio);
			this.Color = Color.Lerp(start.Color, end.Color, ratio);
			this.OverlayXOffset = Mathf.Lerp(start.OverlayXOffset, end.OverlayXOffset, ratio);
			this.OverlayYOffset = Mathf.Lerp(start.OverlayYOffset, end.OverlayYOffset, ratio);
			this.XOffset = Mathf.Lerp(start.XOffset, end.XOffset, ratio);
			this.YOffset = Mathf.Lerp(start.YOffset, end.YOffset, ratio);
			this.Sprite = ((ratio > 0.9f) ? end.Sprite : start.Sprite);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006E00 File Offset: 0x00005000
		public void SetValueAsFloat(BrushAnimationProperty.BrushAnimationPropertyType propertyType, float value)
		{
			switch (propertyType)
			{
			case BrushAnimationProperty.BrushAnimationPropertyType.ColorFactor:
				this.ColorFactor = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.Color:
			case BrushAnimationProperty.BrushAnimationPropertyType.FontColor:
				break;
			case BrushAnimationProperty.BrushAnimationPropertyType.AlphaFactor:
				this.AlphaFactor = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.HueFactor:
				this.HueFactor = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.SaturationFactor:
				this.SaturationFactor = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.ValueFactor:
				this.ValueFactor = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlayXOffset:
				this.OverlayXOffset = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlayYOffset:
				this.OverlayYOffset = value;
				return;
			default:
				if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.XOffset)
				{
					this.XOffset = value;
					return;
				}
				if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.YOffset)
				{
					this.YOffset = value;
					return;
				}
				break;
			}
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushLayerState.cs", "SetValueAsFloat", 109);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006EA3 File Offset: 0x000050A3
		public void SetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType, in Color value)
		{
			if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.Color)
			{
				this.Color = value;
				return;
			}
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushLayerState.cs", "SetValueAsColor", 122);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006ECC File Offset: 0x000050CC
		public void SetValueAsSprite(BrushAnimationProperty.BrushAnimationPropertyType propertyType, Sprite value)
		{
			if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.Sprite)
			{
				this.Sprite = value;
				return;
			}
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushLayerState.cs", "SetValueAsSprite", 135);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006EF4 File Offset: 0x000050F4
		public float GetValueAsFloat(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			switch (propertyType)
			{
			case BrushAnimationProperty.BrushAnimationPropertyType.ColorFactor:
				return this.ColorFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.Color:
			case BrushAnimationProperty.BrushAnimationPropertyType.FontColor:
				break;
			case BrushAnimationProperty.BrushAnimationPropertyType.AlphaFactor:
				return this.AlphaFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.HueFactor:
				return this.HueFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.SaturationFactor:
				return this.SaturationFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.ValueFactor:
				return this.ValueFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlayXOffset:
				return this.OverlayXOffset;
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlayYOffset:
				return this.OverlayYOffset;
			default:
				if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.XOffset)
				{
					return this.XOffset;
				}
				if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.YOffset)
				{
					return this.YOffset;
				}
				break;
			}
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushLayerState.cs", "GetValueAsFloat", 163);
			return 0f;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006F96 File Offset: 0x00005196
		public Color GetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.Color)
			{
				return this.Color;
			}
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushLayerState.cs", "GetValueAsColor", 175);
			return Color.Black;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006FC1 File Offset: 0x000051C1
		public Sprite GetValueAsSprite(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.Sprite)
			{
				return this.Sprite;
			}
			Debug.FailedAssert("Invalid value type or property name for data source.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushLayerState.cs", "GetValueAsSprite", 187);
			return null;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006FEC File Offset: 0x000051EC
		public static void SetValueAsLerpOfValues(ref BrushLayerState currentState, in BrushAnimationKeyFrame startValue, in BrushAnimationKeyFrame endValue, BrushAnimationProperty.BrushAnimationPropertyType propertyType, float ratio)
		{
			switch (propertyType)
			{
			case BrushAnimationProperty.BrushAnimationPropertyType.ColorFactor:
			case BrushAnimationProperty.BrushAnimationPropertyType.AlphaFactor:
			case BrushAnimationProperty.BrushAnimationPropertyType.HueFactor:
			case BrushAnimationProperty.BrushAnimationPropertyType.SaturationFactor:
			case BrushAnimationProperty.BrushAnimationPropertyType.ValueFactor:
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlayXOffset:
			case BrushAnimationProperty.BrushAnimationPropertyType.OverlayYOffset:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineAmount:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowRadius:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextBlur:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowOffset:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowAngle:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextColorFactor:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextAlphaFactor:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextHueFactor:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextSaturationFactor:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextValueFactor:
			case BrushAnimationProperty.BrushAnimationPropertyType.XOffset:
			case BrushAnimationProperty.BrushAnimationPropertyType.YOffset:
				currentState.SetValueAsFloat(propertyType, MathF.Lerp(startValue.GetValueAsFloat(), endValue.GetValueAsFloat(), ratio, 1E-05f));
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.Color:
			case BrushAnimationProperty.BrushAnimationPropertyType.FontColor:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowColor:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineColor:
			{
				Color color = Color.Lerp(startValue.GetValueAsColor(), endValue.GetValueAsColor(), ratio);
				currentState.SetValueAsColor(propertyType, color);
				return;
			}
			case BrushAnimationProperty.BrushAnimationPropertyType.Sprite:
				currentState.SetValueAsSprite(propertyType, ((double)ratio > 0.9) ? endValue.GetValueAsSprite() : startValue.GetValueAsSprite());
				break;
			case BrushAnimationProperty.BrushAnimationPropertyType.IsHidden:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000070CD File Offset: 0x000052CD
		void IBrushAnimationState.SetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType, in Color value)
		{
			this.SetValueAsColor(propertyType, value);
		}

		// Token: 0x0400006A RID: 106
		public Color Color;

		// Token: 0x0400006B RID: 107
		public float ColorFactor;

		// Token: 0x0400006C RID: 108
		public float AlphaFactor;

		// Token: 0x0400006D RID: 109
		public float HueFactor;

		// Token: 0x0400006E RID: 110
		public float SaturationFactor;

		// Token: 0x0400006F RID: 111
		public float ValueFactor;

		// Token: 0x04000070 RID: 112
		public float OverlayXOffset;

		// Token: 0x04000071 RID: 113
		public float OverlayYOffset;

		// Token: 0x04000072 RID: 114
		public float XOffset;

		// Token: 0x04000073 RID: 115
		public float YOffset;

		// Token: 0x04000074 RID: 116
		public Sprite Sprite;
	}
}
