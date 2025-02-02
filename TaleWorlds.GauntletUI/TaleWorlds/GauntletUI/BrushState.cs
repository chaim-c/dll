using System;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000017 RID: 23
	public struct BrushState : IBrushAnimationState, IDataSource
	{
		// Token: 0x06000179 RID: 377 RVA: 0x000089E4 File Offset: 0x00006BE4
		public void FillFrom(Style style)
		{
			this.FontColor = style.FontColor;
			this.TextGlowColor = style.TextGlowColor;
			this.TextOutlineColor = style.TextOutlineColor;
			this.TextOutlineAmount = style.TextOutlineAmount;
			this.TextGlowRadius = style.TextGlowRadius;
			this.TextBlur = style.TextBlur;
			this.TextShadowOffset = style.TextShadowOffset;
			this.TextShadowAngle = style.TextShadowAngle;
			this.TextColorFactor = style.TextColorFactor;
			this.TextAlphaFactor = style.TextAlphaFactor;
			this.TextHueFactor = style.TextHueFactor;
			this.TextSaturationFactor = style.TextSaturationFactor;
			this.TextValueFactor = style.TextValueFactor;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008A90 File Offset: 0x00006C90
		public void LerpFrom(BrushState start, Style end, float ratio)
		{
			this.FontColor = Color.Lerp(start.FontColor, end.FontColor, ratio);
			this.TextGlowColor = Color.Lerp(start.TextGlowColor, end.TextGlowColor, ratio);
			this.TextOutlineColor = Color.Lerp(start.TextOutlineColor, end.TextOutlineColor, ratio);
			this.TextOutlineAmount = Mathf.Lerp(start.TextOutlineAmount, end.TextOutlineAmount, ratio);
			this.TextGlowRadius = Mathf.Lerp(start.TextGlowRadius, end.TextGlowRadius, ratio);
			this.TextBlur = Mathf.Lerp(start.TextBlur, end.TextBlur, ratio);
			this.TextShadowOffset = Mathf.Lerp(start.TextShadowOffset, end.TextShadowOffset, ratio);
			this.TextShadowAngle = Mathf.Lerp(start.TextShadowAngle, end.TextShadowAngle, ratio);
			this.TextColorFactor = Mathf.Lerp(start.TextColorFactor, end.TextColorFactor, ratio);
			this.TextAlphaFactor = Mathf.Lerp(start.TextAlphaFactor, end.TextAlphaFactor, ratio);
			this.TextHueFactor = Mathf.Lerp(start.TextHueFactor, end.TextHueFactor, ratio);
			this.TextSaturationFactor = Mathf.Lerp(start.TextSaturationFactor, end.TextSaturationFactor, ratio);
			this.TextValueFactor = Mathf.Lerp(start.TextValueFactor, end.TextValueFactor, ratio);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008BD8 File Offset: 0x00006DD8
		void IBrushAnimationState.FillFrom(IDataSource source)
		{
			Style style = (Style)source;
			this.FillFrom(style);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008BF4 File Offset: 0x00006DF4
		void IBrushAnimationState.LerpFrom(IBrushAnimationState start, IDataSource end, float ratio)
		{
			BrushState start2 = (BrushState)start;
			Style end2 = (Style)end;
			this.LerpFrom(start2, end2, ratio);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008C18 File Offset: 0x00006E18
		public float GetValueAsFloat(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			switch (propertyType)
			{
			case BrushAnimationProperty.BrushAnimationPropertyType.FontColor:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowColor:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineColor:
				Debug.FailedAssert("Invalid value type for BrushState.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "GetValueAsFloat", 102);
				return 0f;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineAmount:
				return this.TextOutlineAmount;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowRadius:
				return this.TextGlowRadius;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextBlur:
				return this.TextBlur;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowOffset:
				return this.TextShadowOffset;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowAngle:
				return this.TextShadowAngle;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextColorFactor:
				return this.TextColorFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextAlphaFactor:
				return this.TextAlphaFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextHueFactor:
				return this.TextHueFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextSaturationFactor:
				return this.TextSaturationFactor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextValueFactor:
				return this.TextValueFactor;
			}
			Debug.FailedAssert("Invalid BrushState property.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "GetValueAsFloat", 106);
			return 0f;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008CE8 File Offset: 0x00006EE8
		public Color GetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			switch (propertyType)
			{
			case BrushAnimationProperty.BrushAnimationPropertyType.FontColor:
				return this.FontColor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowColor:
				return this.TextGlowColor;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineColor:
				return this.TextOutlineColor;
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
				Debug.FailedAssert("Invalid value type for BrushState.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "GetValueAsColor", 132);
				return Color.Black;
			}
			Debug.FailedAssert("Invalid BrushState property.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "GetValueAsColor", 135);
			return Color.Black;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008D90 File Offset: 0x00006F90
		public Sprite GetValueAsSprite(BrushAnimationProperty.BrushAnimationPropertyType propertyType)
		{
			if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.FontColor || propertyType - BrushAnimationProperty.BrushAnimationPropertyType.TextGlowColor <= 11)
			{
				Debug.FailedAssert("Invalid value type for BrushState.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "GetValueAsSprite", 157);
				return null;
			}
			Debug.FailedAssert("Invalid BrushState property.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "GetValueAsSprite", 161);
			return null;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008DE0 File Offset: 0x00006FE0
		public void SetValueAsFloat(BrushAnimationProperty.BrushAnimationPropertyType propertyType, float value)
		{
			switch (propertyType)
			{
			case BrushAnimationProperty.BrushAnimationPropertyType.FontColor:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowColor:
			case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineColor:
				Debug.FailedAssert("Invalid value type for BrushState.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "SetValueAsFloat", 204);
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineAmount:
				this.TextOutlineAmount = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowRadius:
				this.TextGlowRadius = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextBlur:
				this.TextBlur = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowOffset:
				this.TextShadowOffset = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextShadowAngle:
				this.TextShadowAngle = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextColorFactor:
				this.TextColorFactor = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextAlphaFactor:
				this.TextAlphaFactor = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextHueFactor:
				this.TextHueFactor = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextSaturationFactor:
				this.TextSaturationFactor = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextValueFactor:
				this.TextValueFactor = value;
				return;
			}
			Debug.FailedAssert("Invalid BrushState property.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "SetValueAsFloat", 208);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008EB8 File Offset: 0x000070B8
		public void SetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType, in Color value)
		{
			switch (propertyType)
			{
			case BrushAnimationProperty.BrushAnimationPropertyType.FontColor:
				this.FontColor = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextGlowColor:
				this.TextGlowColor = value;
				return;
			case BrushAnimationProperty.BrushAnimationPropertyType.TextOutlineColor:
				this.TextOutlineColor = value;
				return;
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
				Debug.FailedAssert("Invalid value type for BrushState.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "SetValueAsColor", 237);
				return;
			}
			Debug.FailedAssert("Invalid BrushState property.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "SetValueAsColor", 240);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008F68 File Offset: 0x00007168
		public void SetValueAsSprite(BrushAnimationProperty.BrushAnimationPropertyType propertyType, Sprite value)
		{
			if (propertyType == BrushAnimationProperty.BrushAnimationPropertyType.FontColor || propertyType - BrushAnimationProperty.BrushAnimationPropertyType.TextGlowColor <= 11)
			{
				Debug.FailedAssert("Invalid value type for BrushState.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "SetValueAsSprite", 262);
				return;
			}
			Debug.FailedAssert("Invalid BrushState property.", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushState.cs", "SetValueAsSprite", 265);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008FB4 File Offset: 0x000071B4
		public TextMaterial CreateTextMaterial(TwoDimensionDrawContext drawContext)
		{
			TextMaterial textMaterial = drawContext.CreateTextMaterial();
			textMaterial.Color = this.FontColor;
			textMaterial.GlowColor = this.TextGlowColor;
			textMaterial.OutlineColor = this.TextOutlineColor;
			textMaterial.OutlineAmount = this.TextOutlineAmount;
			textMaterial.GlowRadius = this.TextGlowRadius;
			textMaterial.Blur = this.TextBlur;
			textMaterial.ShadowOffset = this.TextShadowOffset;
			textMaterial.ShadowAngle = this.TextShadowAngle;
			textMaterial.ColorFactor = this.TextColorFactor;
			textMaterial.AlphaFactor = this.TextAlphaFactor;
			textMaterial.HueFactor = this.TextHueFactor;
			textMaterial.SaturationFactor = this.TextSaturationFactor;
			textMaterial.ValueFactor = this.TextValueFactor;
			return textMaterial;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00009063 File Offset: 0x00007263
		void IBrushAnimationState.SetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType, in Color value)
		{
			this.SetValueAsColor(propertyType, value);
		}

		// Token: 0x0400008A RID: 138
		public Color FontColor;

		// Token: 0x0400008B RID: 139
		public Color TextGlowColor;

		// Token: 0x0400008C RID: 140
		public Color TextOutlineColor;

		// Token: 0x0400008D RID: 141
		public float TextOutlineAmount;

		// Token: 0x0400008E RID: 142
		public float TextGlowRadius;

		// Token: 0x0400008F RID: 143
		public float TextBlur;

		// Token: 0x04000090 RID: 144
		public float TextShadowOffset;

		// Token: 0x04000091 RID: 145
		public float TextShadowAngle;

		// Token: 0x04000092 RID: 146
		public float TextColorFactor;

		// Token: 0x04000093 RID: 147
		public float TextAlphaFactor;

		// Token: 0x04000094 RID: 148
		public float TextHueFactor;

		// Token: 0x04000095 RID: 149
		public float TextSaturationFactor;

		// Token: 0x04000096 RID: 150
		public float TextValueFactor;
	}
}
