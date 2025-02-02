using System;
using System.Numerics;
using System.Xml;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Canvas
{
	// Token: 0x0200004C RID: 76
	public class Canvas
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x000167A2 File Offset: 0x000149A2
		public Canvas(SpriteData spriteData, FontFactory fontFactory)
		{
			this._spriteData = spriteData;
			this._fontFactory = fontFactory;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x000167B8 File Offset: 0x000149B8
		public CanvasObject Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000167C0 File Offset: 0x000149C0
		public void LoadFrom(XmlNode canvasNode)
		{
			this._root = null;
			if (canvasNode != null)
			{
				this._root = new CanvasObject(null, this._fontFactory, this._spriteData);
				foreach (object obj in canvasNode)
				{
					XmlNode xmlNode = (XmlNode)obj;
					CanvasElement canvasElement = null;
					if (xmlNode.Name == "Image")
					{
						CanvasImage canvasImage = new CanvasImage(this._root, this._fontFactory, this._spriteData);
						canvasImage.LoadFrom(xmlNode);
						canvasElement = canvasImage;
					}
					else if (xmlNode.Name == "TextBox")
					{
						CanvasTextBox canvasTextBox = new CanvasTextBox(this._root, this._fontFactory, this._spriteData);
						canvasTextBox.LoadFrom(xmlNode);
						canvasElement = canvasTextBox;
					}
					if (canvasElement != null)
					{
						this._root.Children.Add(canvasElement);
					}
				}
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000168B4 File Offset: 0x00014AB4
		public void Update(float scale)
		{
			this._root.Update(scale);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000168C2 File Offset: 0x00014AC2
		public void DoMeasure(bool fixedWidth, bool fixedHeight, float width, float height)
		{
			this._root.BeginMeasure(fixedWidth, fixedHeight, width, height);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000168D4 File Offset: 0x00014AD4
		public void DoLayout()
		{
			this._root.DoLayout();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000168E1 File Offset: 0x00014AE1
		public void DoRender(Vector2 globalPosition, TwoDimensionDrawContext drawContext)
		{
			this._root.DoRender(globalPosition, drawContext);
		}

		// Token: 0x0400027C RID: 636
		private SpriteData _spriteData;

		// Token: 0x0400027D RID: 637
		private FontFactory _fontFactory;

		// Token: 0x0400027E RID: 638
		private CanvasObject _root;
	}
}
