using System;
using System.Collections.Generic;
using System.Numerics;
using System.Xml;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Canvas
{
	// Token: 0x02000055 RID: 85
	public class CanvasTextBox : CanvasElement
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x00017686 File Offset: 0x00015886
		public CanvasTextBox(CanvasObject parent, FontFactory fontFactory, SpriteData spriteData) : base(parent, fontFactory, spriteData)
		{
			this._lines = new List<CanvasLine>();
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001769C File Offset: 0x0001589C
		public override void LoadFrom(XmlNode canvasTextNode)
		{
			base.LoadFrom(canvasTextNode);
			int num = 0;
			foreach (object obj in canvasTextNode)
			{
				XmlNode lineNode = (XmlNode)obj;
				CanvasLine canvasLine = new CanvasLine(this, num, base.FontFactory, base.SpriteData);
				canvasLine.LoadFrom(lineNode);
				this._lines.Add(canvasLine);
				base.Children.Add(canvasLine);
				num++;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0001772C File Offset: 0x0001592C
		protected override Vector2 Measure()
		{
			float num = 0f;
			float num2 = 0f;
			foreach (CanvasLine canvasLine in this._lines)
			{
				num = Mathf.Max(num, canvasLine.Width);
				num2 += canvasLine.Height;
			}
			return new Vector2(num, num2);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000177A4 File Offset: 0x000159A4
		public float GetVerticalPositionOf(int index)
		{
			float num = 0f;
			int num2 = 0;
			foreach (CanvasLine canvasLine in this._lines)
			{
				if (num2 == index)
				{
					break;
				}
				num += canvasLine.Height;
				num2++;
			}
			return num;
		}

		// Token: 0x040002A0 RID: 672
		private List<CanvasLine> _lines;
	}
}
