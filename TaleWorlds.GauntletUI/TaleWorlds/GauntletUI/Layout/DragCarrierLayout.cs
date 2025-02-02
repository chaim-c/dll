using System;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Layout
{
	// Token: 0x0200003C RID: 60
	public class DragCarrierLayout : ILayout
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000FE12 File Offset: 0x0000E012
		Vector2 ILayout.MeasureChildren(Widget widget, Vector2 measureSpec, SpriteData spriteData, float renderScale)
		{
			Widget child = widget.GetChild(0);
			child.Measure(measureSpec);
			return child.MeasuredSize;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000FE28 File Offset: 0x0000E028
		void ILayout.OnLayout(Widget widget, float left, float bottom, float right, float top)
		{
			float left2 = 0f;
			float top2 = 0f;
			float right2 = right - left;
			float bottom2 = bottom - top;
			widget.GetChild(0).Layout(left2, bottom2, right2, top2);
		}
	}
}
