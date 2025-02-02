using System;
using System.Globalization;
using System.Numerics;
using System.Xml;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Canvas
{
	// Token: 0x0200004D RID: 77
	public abstract class CanvasElement : CanvasObject
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x000168F0 File Offset: 0x00014AF0
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x000168F8 File Offset: 0x00014AF8
		public float PositionX { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00016901 File Offset: 0x00014B01
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00016909 File Offset: 0x00014B09
		public float PositionY { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x00016912 File Offset: 0x00014B12
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0001691A File Offset: 0x00014B1A
		public float RelativePositionX
		{
			get
			{
				return this._relativePositionX;
			}
			set
			{
				this._relativePositionX = value;
				this._usingRelativeX = true;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001692A File Offset: 0x00014B2A
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00016932 File Offset: 0x00014B32
		public float RelativePositionY
		{
			get
			{
				return this._relativePositionY;
			}
			set
			{
				this._relativePositionY = value;
				this._usingRelativeY = true;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00016942 File Offset: 0x00014B42
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0001694A File Offset: 0x00014B4A
		public float PivotX { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00016953 File Offset: 0x00014B53
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0001695B File Offset: 0x00014B5B
		public float PivotY { get; set; }

		// Token: 0x0600053B RID: 1339 RVA: 0x00016964 File Offset: 0x00014B64
		protected CanvasElement(CanvasObject parent, FontFactory fontFactory, SpriteData spriteData) : base(parent, fontFactory, spriteData)
		{
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00016970 File Offset: 0x00014B70
		public virtual void LoadFrom(XmlNode canvasImageNode)
		{
			foreach (object obj in canvasImageNode.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string name = xmlAttribute.Name;
				string value = xmlAttribute.Value;
				if (name == "PositionX")
				{
					this.PositionX = Convert.ToSingle(value, CultureInfo.InvariantCulture);
				}
				else if (name == "PositionY")
				{
					this.PositionY = Convert.ToSingle(value, CultureInfo.InvariantCulture);
				}
				else if (name == "RelativePositionX")
				{
					this.RelativePositionX = Convert.ToSingle(value, CultureInfo.InvariantCulture);
				}
				else if (name == "RelativePositionY")
				{
					this.RelativePositionY = Convert.ToSingle(value, CultureInfo.InvariantCulture);
				}
				else if (name == "PivotX")
				{
					this.PivotX = Convert.ToSingle(value, CultureInfo.InvariantCulture);
				}
				else if (name == "PivotY")
				{
					this.PivotY = Convert.ToSingle(value, CultureInfo.InvariantCulture);
				}
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00016A98 File Offset: 0x00014C98
		protected sealed override Vector2 Layout()
		{
			Vector2 result = new Vector2(this.PositionX, this.PositionY);
			if (this._usingRelativeX)
			{
				result.X = base.Parent.Width * this.RelativePositionX;
			}
			else
			{
				result.X *= base.Scale;
			}
			if (this._usingRelativeY)
			{
				result.Y = base.Parent.Height * this.RelativePositionY;
			}
			else
			{
				result.Y *= base.Scale;
			}
			result.X -= this.PivotX * base.Width;
			result.Y -= this.PivotY * base.Height;
			return result;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00016B51 File Offset: 0x00014D51
		public override Vector2 GetMarginSize()
		{
			return new Vector2(this.PositionX * base.Scale, this.PositionY * base.Scale);
		}

		// Token: 0x0400027F RID: 639
		public bool _usingRelativeX;

		// Token: 0x04000280 RID: 640
		public bool _usingRelativeY;

		// Token: 0x04000283 RID: 643
		private float _relativePositionX;

		// Token: 0x04000284 RID: 644
		private float _relativePositionY;
	}
}
