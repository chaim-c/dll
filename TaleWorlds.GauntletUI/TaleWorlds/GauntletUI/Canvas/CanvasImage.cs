using System;
using System.Numerics;
using System.Xml;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Canvas
{
	// Token: 0x0200004E RID: 78
	public class CanvasImage : CanvasElement
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00016B72 File Offset: 0x00014D72
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x00016B7A File Offset: 0x00014D7A
		public Sprite Sprite { get; set; }

		// Token: 0x06000541 RID: 1345 RVA: 0x00016B83 File Offset: 0x00014D83
		public CanvasImage(CanvasObject parent, FontFactory fontFactory, SpriteData spriteData) : base(parent, fontFactory, spriteData)
		{
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00016B90 File Offset: 0x00014D90
		public override void LoadFrom(XmlNode canvasImageNode)
		{
			base.LoadFrom(canvasImageNode);
			foreach (object obj in canvasImageNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				string value = xmlAttribute.Value;
				if (name == "Sprite")
				{
					this.Sprite = base.SpriteData.GetSprite(value);
				}
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00016C14 File Offset: 0x00014E14
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

		// Token: 0x06000544 RID: 1348 RVA: 0x00016C64 File Offset: 0x00014E64
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
	}
}
