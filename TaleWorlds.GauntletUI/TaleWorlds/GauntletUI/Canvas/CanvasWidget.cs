using System;
using System.Numerics;
using System.Xml;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Canvas
{
	// Token: 0x02000056 RID: 86
	public class CanvasWidget : Widget, ILayout
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001780C File Offset: 0x00015A0C
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0001781C File Offset: 0x00015A1C
		[Editor(false)]
		public string CanvasAsString
		{
			get
			{
				return this._canvasNode.ToString();
			}
			set
			{
				if ((this._canvasNode == null && value != null) || this._canvasNode.ToString() != value)
				{
					if (!string.IsNullOrEmpty(value))
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.LoadXml(value);
						this._canvasNode = xmlDocument.DocumentElement;
					}
					else
					{
						this._canvasNode = null;
					}
					this._requiresUpdate = true;
					base.OnPropertyChanged<string>(value, "CanvasAsString");
					base.SetMeasureAndLayoutDirty();
				}
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001788A File Offset: 0x00015A8A
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x00017892 File Offset: 0x00015A92
		public XmlElement CanvasNode
		{
			get
			{
				return this._canvasNode;
			}
			set
			{
				if (this._canvasNode != value)
				{
					this._canvasNode = value;
					this._requiresUpdate = true;
					base.OnPropertyChanged<XmlElement>(value, "CanvasNode");
					base.SetMeasureAndLayoutDirty();
				}
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000178BD File Offset: 0x00015ABD
		public CanvasWidget(UIContext context) : base(context)
		{
			this._defaultLayout = new DefaultLayout();
			base.LayoutImp = this;
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x000178D8 File Offset: 0x00015AD8
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.DoUpdate();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000178E7 File Offset: 0x00015AE7
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.DoUpdate();
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000178F6 File Offset: 0x00015AF6
		private void DoUpdate()
		{
			if (this._requiresUpdate || this._canvas == null)
			{
				this.UpdateCanvas();
			}
			this._canvas.Update(base._scaleToUse);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00017920 File Offset: 0x00015B20
		private void UpdateCanvas()
		{
			this._canvas = new Canvas(base.EventManager.Context.SpriteData, base.EventManager.Context.FontFactory);
			this._canvas.LoadFrom(this.CanvasNode);
			this._requiresUpdate = false;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00017970 File Offset: 0x00015B70
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			base.OnRender(twoDimensionContext, drawContext);
			if (this._canvas != null)
			{
				this._canvas.DoRender(base.GlobalPosition, drawContext);
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00017994 File Offset: 0x00015B94
		Vector2 ILayout.MeasureChildren(Widget widget, Vector2 measureSpec, SpriteData spriteData, float renderScale)
		{
			Vector2 vector = this._defaultLayout.MeasureChildren(widget, measureSpec, spriteData, renderScale);
			if (this._canvas != null)
			{
				this._canvas.DoMeasure(base.WidthSizePolicy != SizePolicy.CoverChildren || base.MaxWidth != 0f, base.HeightSizePolicy != SizePolicy.CoverChildren || base.MaxHeight != 0f, measureSpec.X, measureSpec.Y);
				vector.X = Mathf.Max(this._canvas.Root.Width, vector.X);
				vector.Y = Mathf.Max(this._canvas.Root.Height, vector.Y);
			}
			return vector;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00017A51 File Offset: 0x00015C51
		void ILayout.OnLayout(Widget widget, float left, float bottom, float right, float top)
		{
			if (this._canvas != null)
			{
				this._canvas.DoLayout();
			}
			this._defaultLayout.OnLayout(widget, left, bottom, right, top);
		}

		// Token: 0x040002A1 RID: 673
		private ILayout _defaultLayout;

		// Token: 0x040002A2 RID: 674
		private bool _requiresUpdate;

		// Token: 0x040002A3 RID: 675
		private XmlElement _canvasNode;

		// Token: 0x040002A4 RID: 676
		private Canvas _canvas;
	}
}
