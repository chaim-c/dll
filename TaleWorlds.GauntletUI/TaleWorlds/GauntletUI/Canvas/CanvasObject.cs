using System;
using System.Collections.Generic;
using System.Numerics;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Canvas
{
	// Token: 0x02000054 RID: 84
	public class CanvasObject
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001738D File Offset: 0x0001558D
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x00017395 File Offset: 0x00015595
		public CanvasObject Parent { get; private set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0001739E File Offset: 0x0001559E
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x000173A6 File Offset: 0x000155A6
		public List<CanvasObject> Children { get; private set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x000173AF File Offset: 0x000155AF
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x000173B7 File Offset: 0x000155B7
		public float Scale { get; private set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x000173C0 File Offset: 0x000155C0
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x000173C8 File Offset: 0x000155C8
		public Vector2 LocalPosition { get; private set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x000173D1 File Offset: 0x000155D1
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x000173D9 File Offset: 0x000155D9
		public float Width { get; private set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x000173E2 File Offset: 0x000155E2
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x000173EA File Offset: 0x000155EA
		public float Height { get; private set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000173F3 File Offset: 0x000155F3
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x000173FB File Offset: 0x000155FB
		public FontFactory FontFactory { get; private set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00017404 File Offset: 0x00015604
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x0001740C File Offset: 0x0001560C
		public SpriteData SpriteData { get; private set; }

		// Token: 0x06000573 RID: 1395 RVA: 0x00017415 File Offset: 0x00015615
		public CanvasObject(CanvasObject parent, FontFactory fontFactory, SpriteData spriteData)
		{
			this.Children = new List<CanvasObject>(32);
			this.Parent = parent;
			this.FontFactory = fontFactory;
			this.SpriteData = spriteData;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00017440 File Offset: 0x00015640
		public virtual void Update(float scale)
		{
			this.Scale = scale;
			this.OnUpdate(scale);
			foreach (CanvasObject canvasObject in this.Children)
			{
				canvasObject.Update(scale);
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000174A0 File Offset: 0x000156A0
		protected virtual void OnUpdate(float scale)
		{
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000174A2 File Offset: 0x000156A2
		public void BeginMeasure(bool fixedWidth, bool fixedHeight, float width, float height)
		{
			this.DoMeasure();
			if (fixedWidth)
			{
				this.Width = width;
			}
			if (fixedHeight)
			{
				this.Height = height;
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000174C0 File Offset: 0x000156C0
		public void DoMeasure()
		{
			Vector2 zero = Vector2.Zero;
			foreach (CanvasObject canvasObject in this.Children)
			{
				canvasObject.DoMeasure();
				Vector2 left = new Vector2(canvasObject.Width, canvasObject.Height);
				Vector2 marginSize = canvasObject.GetMarginSize();
				Vector2 vector = left + marginSize;
				zero.X = Mathf.Max(zero.X, vector.X);
				zero.Y = Mathf.Max(zero.Y, vector.Y);
			}
			Vector2 vector2 = this.Measure();
			this.Width = Mathf.Max(zero.X, vector2.X);
			this.Height = Mathf.Max(zero.Y, vector2.Y);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000175A4 File Offset: 0x000157A4
		public void DoLayout()
		{
			Vector2 localPosition = this.Layout();
			this.LocalPosition = localPosition;
			foreach (CanvasObject canvasObject in this.Children)
			{
				canvasObject.DoLayout();
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00017604 File Offset: 0x00015804
		protected virtual Vector2 Measure()
		{
			return Vector2.Zero;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001760B File Offset: 0x0001580B
		public virtual Vector2 GetMarginSize()
		{
			return Vector2.Zero;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00017612 File Offset: 0x00015812
		protected virtual Vector2 Layout()
		{
			return Vector2.Zero;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001761C File Offset: 0x0001581C
		public void DoRender(Vector2 globalPosition, TwoDimensionDrawContext drawContext)
		{
			this.Render(globalPosition, drawContext);
			foreach (CanvasObject canvasObject in this.Children)
			{
				canvasObject.DoRender(globalPosition + this.LocalPosition, drawContext);
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00017684 File Offset: 0x00015884
		protected virtual void Render(Vector2 globalPosition, TwoDimensionDrawContext drawContext)
		{
		}
	}
}
