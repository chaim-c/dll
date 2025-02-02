using System;
using System.Globalization;
using System.Numerics;
using System.Xml;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Canvas
{
	// Token: 0x02000053 RID: 83
	public class CanvasLineText : CanvasLineElement
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000171BB File Offset: 0x000153BB
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x000171C8 File Offset: 0x000153C8
		public string Value
		{
			get
			{
				return this._text.Value;
			}
			set
			{
				this._text.CurrentLanguage = base.FontFactory.GetCurrentLanguage();
				this._text.Value = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x000171EC File Offset: 0x000153EC
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x000171F4 File Offset: 0x000153F4
		public float FontSize { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x000171FD File Offset: 0x000153FD
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00017205 File Offset: 0x00015405
		public Color FontColor { get; set; }

		// Token: 0x0600055E RID: 1374 RVA: 0x0001720E File Offset: 0x0001540E
		public CanvasLineText(CanvasLine line, int segmentIndex, FontFactory fontFactory, SpriteData spriteData) : base(line, segmentIndex, fontFactory, spriteData)
		{
			this._text = new Text(400, 400, fontFactory.DefaultFont, new Func<int, Font>(fontFactory.GetUsableFontForCharacter));
			this.FontColor = Color.White;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00017250 File Offset: 0x00015450
		public void LoadFrom(XmlNode textNode)
		{
			foreach (object obj in textNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				string value = xmlAttribute.Value;
				if (name == "Value")
				{
					this.Value = value;
				}
				else if (name == "FontSize")
				{
					this.FontSize = Convert.ToSingle(value, CultureInfo.InvariantCulture);
				}
				else if (name == "FontColor")
				{
					this.FontColor = Color.ConvertStringToColor(value);
				}
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00017300 File Offset: 0x00015500
		public override void Update(float scale)
		{
			base.Update(scale);
			this._text.FontSize = this.FontSize * scale;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001731C File Offset: 0x0001551C
		protected override Vector2 Measure()
		{
			return this._text.GetPreferredSize(false, 0f, false, 0f, null, 1f);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001733C File Offset: 0x0001553C
		protected override void Render(Vector2 globalPosition, TwoDimensionDrawContext drawContext)
		{
			TextMaterial textMaterial = new TextMaterial();
			textMaterial.Color = this.FontColor;
			Vector2 vector = globalPosition + base.LocalPosition;
			drawContext.Draw(this._text, textMaterial, vector.X, vector.Y, base.Width, base.Height);
		}

		// Token: 0x04000295 RID: 661
		private readonly Text _text;
	}
}
