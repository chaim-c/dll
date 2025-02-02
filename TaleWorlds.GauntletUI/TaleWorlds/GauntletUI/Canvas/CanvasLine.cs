using System;
using System.Collections.Generic;
using System.Numerics;
using System.Xml;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Canvas
{
	// Token: 0x0200004F RID: 79
	public class CanvasLine : CanvasObject
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00016CCF File Offset: 0x00014ECF
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00016CD7 File Offset: 0x00014ED7
		public CanvasLineAlignment Alignment { get; set; }

		// Token: 0x06000547 RID: 1351 RVA: 0x00016CE0 File Offset: 0x00014EE0
		public CanvasLine(CanvasTextBox textBox, int lineIndex, FontFactory fontFactory, SpriteData spriteData) : base(textBox, fontFactory, spriteData)
		{
			this._elements = new List<CanvasLineElement>();
			this._lineIndex = lineIndex;
			this._textBox = textBox;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00016D08 File Offset: 0x00014F08
		protected override Vector2 Measure()
		{
			float num = 0f;
			float num2 = 0f;
			foreach (CanvasLineElement canvasLineElement in this._elements)
			{
				num += canvasLineElement.Width;
				num2 = Mathf.Max(num2, canvasLineElement.Height);
			}
			return new Vector2(num, num2);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00016D80 File Offset: 0x00014F80
		protected override Vector2 Layout()
		{
			Vector2 zero = Vector2.Zero;
			if (this.Alignment == CanvasLineAlignment.Left)
			{
				zero.X = 0f;
			}
			else if (this.Alignment == CanvasLineAlignment.Center)
			{
				zero.X = (base.Parent.Width - base.Width) * 0.5f;
			}
			else if (this.Alignment == CanvasLineAlignment.Right)
			{
				zero.X = base.Parent.Width - base.Width;
			}
			zero.Y = this._textBox.GetVerticalPositionOf(this._lineIndex);
			return zero;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00016E10 File Offset: 0x00015010
		public void LoadFrom(XmlNode lineNode)
		{
			foreach (object obj in lineNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				string value = xmlAttribute.Value;
				if (name == "Alignment")
				{
					this.Alignment = (CanvasLineAlignment)Enum.Parse(typeof(CanvasLineAlignment), value);
				}
			}
			int num = 0;
			foreach (object obj2 in lineNode)
			{
				XmlNode xmlNode = (XmlNode)obj2;
				CanvasLineElement canvasLineElement = null;
				if (xmlNode.Name == "LineImage")
				{
					CanvasLineImage canvasLineImage = new CanvasLineImage(this, num, base.FontFactory, base.SpriteData);
					canvasLineImage.LoadFrom(xmlNode);
					canvasLineElement = canvasLineImage;
				}
				else if (xmlNode.Name == "Text")
				{
					CanvasLineText canvasLineText = new CanvasLineText(this, num, base.FontFactory, base.SpriteData);
					canvasLineText.LoadFrom(xmlNode);
					canvasLineElement = canvasLineText;
				}
				if (canvasLineElement != null)
				{
					this._elements.Add(canvasLineElement);
					base.Children.Add(canvasLineElement);
					num++;
				}
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00016F68 File Offset: 0x00015168
		public float GetHorizontalPositionOf(int index)
		{
			float num = 0f;
			int num2 = 0;
			foreach (CanvasLineElement canvasLineElement in this._elements)
			{
				if (num2 == index)
				{
					break;
				}
				num += canvasLineElement.Width;
				num2++;
			}
			return num;
		}

		// Token: 0x04000288 RID: 648
		private List<CanvasLineElement> _elements;

		// Token: 0x04000289 RID: 649
		private CanvasTextBox _textBox;

		// Token: 0x0400028A RID: 650
		private int _lineIndex;
	}
}
