using System;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000013 RID: 19
	public interface IBrushAnimationState
	{
		// Token: 0x0600013B RID: 315
		void FillFrom(IDataSource source);

		// Token: 0x0600013C RID: 316
		void LerpFrom(IBrushAnimationState start, IDataSource end, float ratio);

		// Token: 0x0600013D RID: 317
		float GetValueAsFloat(BrushAnimationProperty.BrushAnimationPropertyType propertyType);

		// Token: 0x0600013E RID: 318
		Color GetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType);

		// Token: 0x0600013F RID: 319
		Sprite GetValueAsSprite(BrushAnimationProperty.BrushAnimationPropertyType propertyType);

		// Token: 0x06000140 RID: 320
		void SetValueAsFloat(BrushAnimationProperty.BrushAnimationPropertyType propertyType, float value);

		// Token: 0x06000141 RID: 321
		void SetValueAsColor(BrushAnimationProperty.BrushAnimationPropertyType propertyType, in Color value);

		// Token: 0x06000142 RID: 322
		void SetValueAsSprite(BrushAnimationProperty.BrushAnimationPropertyType propertyType, Sprite value);
	}
}
