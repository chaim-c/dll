using System;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Layout
{
	// Token: 0x02000045 RID: 69
	public class TextLayout : ILayout
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x00011381 File Offset: 0x0000F581
		public TextLayout(IText text)
		{
			this._defaultLayout = new DefaultLayout();
			this._text = text;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0001139C File Offset: 0x0000F59C
		Vector2 ILayout.MeasureChildren(Widget widget, Vector2 measureSpec, SpriteData spriteData, float renderScale)
		{
			Vector2 vector = this._defaultLayout.MeasureChildren(widget, measureSpec, spriteData, renderScale);
			bool fixedWidth = widget.WidthSizePolicy != SizePolicy.CoverChildren || widget.MaxWidth != 0f;
			bool fixedHeight = widget.HeightSizePolicy != SizePolicy.CoverChildren || widget.MaxHeight != 0f;
			float x = measureSpec.X;
			float y = measureSpec.Y;
			Vector2 preferredSize = this._text.GetPreferredSize(fixedWidth, x, fixedHeight, y, spriteData, renderScale);
			if (vector.X < preferredSize.X)
			{
				vector.X = preferredSize.X;
			}
			if (vector.Y < preferredSize.Y)
			{
				vector.Y = preferredSize.Y;
			}
			return vector;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00011453 File Offset: 0x0000F653
		void ILayout.OnLayout(Widget widget, float left, float bottom, float right, float top)
		{
			this._defaultLayout.OnLayout(widget, left, bottom, right, top);
		}

		// Token: 0x04000208 RID: 520
		private ILayout _defaultLayout;

		// Token: 0x04000209 RID: 521
		private IText _text;
	}
}
