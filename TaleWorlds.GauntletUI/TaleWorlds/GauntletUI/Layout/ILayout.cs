using System;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Layout
{
	// Token: 0x02000041 RID: 65
	public interface ILayout
	{
		// Token: 0x06000403 RID: 1027
		Vector2 MeasureChildren(Widget widget, Vector2 measureSpec, SpriteData spriteData, float renderScale);

		// Token: 0x06000404 RID: 1028
		void OnLayout(Widget widget, float left, float bottom, float right, float top);
	}
}
