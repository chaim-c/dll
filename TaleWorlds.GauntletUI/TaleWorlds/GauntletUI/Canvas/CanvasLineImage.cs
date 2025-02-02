using System;
using System.Numerics;
using System.Xml;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Canvas
{
	// Token: 0x02000052 RID: 82
	public class CanvasLineImage : CanvasLineElement
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00017051 File Offset: 0x00015251
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00017059 File Offset: 0x00015259
		public Sprite Sprite { get; set; }

		// Token: 0x06000554 RID: 1364 RVA: 0x00017062 File Offset: 0x00015262
		public CanvasLineImage(CanvasLine line, int segmentIndex, FontFactory fontFactory, SpriteData spriteData) : base(line, segmentIndex, fontFactory, spriteData)
		{
			this._fontFactory = fontFactory;
			this._spriteData = spriteData;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00017080 File Offset: 0x00015280
		public void LoadFrom(XmlNode imageNode)
		{
			foreach (object obj in imageNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				string value = xmlAttribute.Value;
				if (name == "Sprite")
				{
					this.Sprite = this._spriteData.GetSprite(value);
				}
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00017100 File Offset: 0x00015300
		protected override Vector2 Measure()
		{
			Vector2 zero = Vector2.Zero;
			if (this.Sprite != null)
			{
				zero.X = (float)this.Sprite.Width * base.Scale;
				zero.Y = (float)this.Sprite.Height * base.Scale;
			}
			return zero;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00017150 File Offset: 0x00015350
		protected override void Render(Vector2 globalPosition, TwoDimensionDrawContext drawContext)
		{
			if (this.Sprite != null)
			{
				Texture texture = this.Sprite.Texture;
				if (texture != null)
				{
					SimpleMaterial simpleMaterial = new SimpleMaterial();
					simpleMaterial.Texture = texture;
					Vector2 vector = globalPosition + base.LocalPosition;
					drawContext.DrawSprite(this.Sprite, simpleMaterial, vector.X, vector.Y, base.Scale, base.Width, base.Height, false, false);
				}
			}
		}

		// Token: 0x04000292 RID: 658
		private FontFactory _fontFactory;

		// Token: 0x04000293 RID: 659
		private SpriteData _spriteData;
	}
}
