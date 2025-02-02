using System;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.BannerBuilder
{
	// Token: 0x02000186 RID: 390
	public class BannerBuilderEditableAreaWidget : Widget
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00036CAD File Offset: 0x00034EAD
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x00036CB5 File Offset: 0x00034EB5
		public ButtonWidget DragWidgetTopRight { get; set; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00036CBE File Offset: 0x00034EBE
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x00036CC6 File Offset: 0x00034EC6
		public ButtonWidget DragWidgetRight { get; set; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x00036CCF File Offset: 0x00034ECF
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x00036CD7 File Offset: 0x00034ED7
		public ButtonWidget DragWidgetTop { get; set; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x00036CE0 File Offset: 0x00034EE0
		// (set) Token: 0x0600140A RID: 5130 RVA: 0x00036CE8 File Offset: 0x00034EE8
		public ButtonWidget RotateWidget { get; set; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00036CF1 File Offset: 0x00034EF1
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x00036CF9 File Offset: 0x00034EF9
		public BannerTableauWidget BannerTableauWidget { get; set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00036D02 File Offset: 0x00034F02
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x00036D0A File Offset: 0x00034F0A
		public Widget EditableAreaVisualWidget { get; set; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00036D13 File Offset: 0x00034F13
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x00036D1B File Offset: 0x00034F1B
		public int LayerIndex { get; set; }

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00036D24 File Offset: 0x00034F24
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x00036D2C File Offset: 0x00034F2C
		public bool IsMirrorActive { get; set; }

		// Token: 0x06001413 RID: 5139 RVA: 0x00036D35 File Offset: 0x00034F35
		public BannerBuilderEditableAreaWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00036D3E File Offset: 0x00034F3E
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.BannerTableauWidget.MeshIndexToUpdate = this.LayerIndex;
			if (!this._initialized)
			{
				this.Initialize();
			}
			this.UpdateRequiredValues();
			this.UpdateEditableAreaVisual();
			this.HandleCursor();
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00036D78 File Offset: 0x00034F78
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!Input.IsKeyDown(InputKey.LeftMouseButton))
			{
				BannerBuilderEditableAreaWidget.BuilderMode currentMode = this._currentMode;
				if (currentMode != BannerBuilderEditableAreaWidget.BuilderMode.None && currentMode - BannerBuilderEditableAreaWidget.BuilderMode.Rotating <= 4)
				{
					base.EventFired("RefreshBanner", Array.Empty<object>());
				}
			}
			this.HandleRotation();
			this.HandlePositioning();
			this.HandleForEdge(BannerBuilderEditableAreaWidget.EdgeResizeType.Right);
			this.HandleForEdge(BannerBuilderEditableAreaWidget.EdgeResizeType.Top);
			this.HandleForCorner();
			this._latestMousePosition = base.EventManager.MousePosition;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00036DE9 File Offset: 0x00034FE9
		private void Initialize()
		{
			this._centerOfSigil = new Vec2(0f, 0f);
			this._sizeOfSigil = new Vec2(0f, 0f);
			this._initialized = true;
			this.OnIsLayerPatternChanged(false);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00036E24 File Offset: 0x00035024
		private void UpdateRequiredValues()
		{
			float x = this._positionValue.X / (float)this.TotalAreaSize * base.Size.X;
			float y = this._positionValue.Y / (float)this.TotalAreaSize * base.Size.Y;
			this._centerOfSigil.x = x;
			this._centerOfSigil.y = y;
			float x2 = this._sizeValue.X / (float)this.TotalAreaSize * base.Size.X;
			float y2 = this._sizeValue.Y / (float)this.TotalAreaSize * base.Size.Y;
			this._sizeOfSigil.x = x2;
			this._sizeOfSigil.y = y2;
			this._positionLimitMin = 0f;
			this._positionLimitMax = this._positionLimitMin + (float)this.TotalAreaSize;
			this._sizeLimitMax = this.TotalAreaSize;
			this._areaScale = (float)this.TotalAreaSize / base.Size.X;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00036F28 File Offset: 0x00035128
		private void HandlePositioning()
		{
			if (Input.IsKeyDown(InputKey.LeftMouseButton))
			{
				if (this._currentMode == BannerBuilderEditableAreaWidget.BuilderMode.Positioning)
				{
					Vector2 vector = base.EventManager.MousePosition - this._latestMousePosition;
					vector *= (float)this.TotalAreaSize / base.Size.X;
					Vector2 vector2 = new Vector2(this.PositionValue.X, this.PositionValue.Y);
					vector2 += vector;
					vector2 = new Vector2(MathF.Clamp(vector2.X, this._positionLimitMin, this._positionLimitMax), MathF.Clamp(vector2.Y, this._positionLimitMin, this._positionLimitMax));
					this.PositionValue = vector2;
					this.BannerTableauWidget.UpdatePositionValueManual = this.PositionValue;
				}
				if (this._currentMode != BannerBuilderEditableAreaWidget.BuilderMode.Positioning && base.EventManager.HoveredView == this)
				{
					this._currentMode = BannerBuilderEditableAreaWidget.BuilderMode.Positioning;
					return;
				}
			}
			else
			{
				this._currentMode = BannerBuilderEditableAreaWidget.BuilderMode.None;
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00037024 File Offset: 0x00035224
		private void HandleRotation()
		{
			if (Input.IsKeyDown(InputKey.LeftMouseButton))
			{
				if (this._currentMode == BannerBuilderEditableAreaWidget.BuilderMode.Rotating)
				{
					Vec2 vec = base.GlobalPosition + this._centerOfSigil;
					Vector2 vec2 = base.EventManager.MousePosition - new Vector2(vec.X, vec.y);
					vec2.Y *= -1f;
					float num = BannerBuilderEditableAreaWidget.AngleFromDir(vec2);
					this.RotationValue = (float)Math.Round((double)num, 3);
					this.BannerTableauWidget.UpdateRotationValueManualWithMirror = new ValueTuple<float, bool>(this.RotationValue, this.IsMirrorActive);
				}
				if (this._currentMode != BannerBuilderEditableAreaWidget.BuilderMode.Rotating && base.EventManager.HoveredView == this.RotateWidget)
				{
					this._currentMode = BannerBuilderEditableAreaWidget.BuilderMode.Rotating;
				}
			}
			else
			{
				this._currentMode = BannerBuilderEditableAreaWidget.BuilderMode.None;
			}
			this.UpdatePositionOfWidget(this.RotateWidget, BannerBuilderEditableAreaWidget.WidgetPlacementType.Vertical, this.RotationValue, 55f, 30f);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00037118 File Offset: 0x00035318
		private void HandleForEdge(BannerBuilderEditableAreaWidget.EdgeResizeType resizeType)
		{
			ButtonWidget widgetFor = this.GetWidgetFor(resizeType);
			if (Input.IsKeyDown(InputKey.LeftMouseButton))
			{
				if (this._currentMode == BannerBuilderEditableAreaWidget.BuilderMode.HorizontalResizing || this._currentMode == BannerBuilderEditableAreaWidget.BuilderMode.VerticalResizing)
				{
					Vector2 vector = base.EventManager.MousePosition - this._resizeStartMousePosition;
					vector.Y *= -1f;
					Vec2 b = BannerBuilderEditableAreaWidget.DirFromAngle(this.RotationValue);
					b.y *= -1f;
					vector = BannerBuilderEditableAreaWidget.TransformToParent(vector, b);
					vector.X *= -1f;
					vector.Y *= -1f;
					BannerBuilderEditableAreaWidget.BuilderMode currentMode = this._currentMode;
					if (currentMode != BannerBuilderEditableAreaWidget.BuilderMode.HorizontalResizing)
					{
						if (currentMode == BannerBuilderEditableAreaWidget.BuilderMode.VerticalResizing)
						{
							vector.X = 0f;
						}
					}
					else
					{
						vector.Y = 0f;
					}
					vector *= (float)this.TotalAreaSize / base.Size.X * 2f;
					Vec2 vec = new Vec2(this._resizeStartSize.X, this._resizeStartSize.Y);
					vec += vector;
					vec = new Vector2((float)((int)MathF.Clamp((float)((int)vec.X), 2f, (float)this._sizeLimitMax)), (float)((int)MathF.Clamp((float)((int)vec.Y), 2f, (float)this._sizeLimitMax)));
					Vec2 vec2 = this._resizeStartSize - vec;
					if (vec2.x != 0f || vec2.y != 0f)
					{
						this.BannerTableauWidget.UpdateSizeValueManual = vec;
						this.SizeValue = vec;
					}
				}
				if ((this._currentMode != BannerBuilderEditableAreaWidget.BuilderMode.HorizontalResizing || this._currentMode != BannerBuilderEditableAreaWidget.BuilderMode.VerticalResizing) && base.EventManager.HoveredView == widgetFor)
				{
					this._resizeStartMousePosition = base.EventManager.MousePosition;
					this._resizeStartWidget = base.EventManager.HoveredView;
					this._resizeStartSize = this.SizeValue;
					this._currentMode = ((base.EventManager.HoveredView == this.GetWidgetFor(BannerBuilderEditableAreaWidget.EdgeResizeType.Right)) ? BannerBuilderEditableAreaWidget.BuilderMode.HorizontalResizing : BannerBuilderEditableAreaWidget.BuilderMode.VerticalResizing);
				}
			}
			else
			{
				this._resizeStartWidget = null;
				this._resizeStartSize = Vec2.Zero;
				this._currentMode = BannerBuilderEditableAreaWidget.BuilderMode.None;
			}
			this.UpdatePositionOfWidget(widgetFor, (resizeType == BannerBuilderEditableAreaWidget.EdgeResizeType.Right) ? BannerBuilderEditableAreaWidget.WidgetPlacementType.Horizontal : BannerBuilderEditableAreaWidget.WidgetPlacementType.Vertical, this.RotationValue + BannerBuilderEditableAreaWidget.AngleOffsetForEdge(resizeType), 15f, 0f);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00037360 File Offset: 0x00035560
		private void HandleForCorner()
		{
			ButtonWidget dragWidgetTopRight = this.DragWidgetTopRight;
			if (Input.IsKeyDown(InputKey.LeftMouseButton))
			{
				bool flag = Input.IsKeyDown(InputKey.LeftShift) || Input.IsKeyDown(InputKey.RightShift);
				if (this._currentMode == BannerBuilderEditableAreaWidget.BuilderMode.RightCornerResizing)
				{
					Vector2 vector = base.EventManager.MousePosition - this._resizeStartMousePosition;
					vector.Y *= -1f;
					vector *= (float)this.TotalAreaSize / base.Size.X * 2f;
					Vec2 b = BannerBuilderEditableAreaWidget.DirFromAngle(this.RotationValue);
					b.y *= -1f;
					vector = BannerBuilderEditableAreaWidget.TransformToParent(vector, b);
					vector.X *= -1f;
					vector.Y *= -1f;
					Vec2 vec = new Vec2(this._resizeStartSize.X, this._resizeStartSize.Y);
					if (flag)
					{
						Vector2 left = new Vector2(this._centerOfSigil.X, this._centerOfSigil.Y) + base.GlobalPosition;
						float num = (left - this._resizeStartMousePosition).Length();
						bool flag2 = (left - base.EventManager.MousePosition).Length() < num;
						float num2 = vector.Length() * this._areaScale * (float)(flag2 ? -1 : 1);
						float length = this._resizeStartSize.Length;
						float f = num2 / length;
						vec += f * vec * this._areaScale / 4f;
					}
					else
					{
						vec += vector;
					}
					vec = new Vector2((float)((int)MathF.Clamp((float)((int)vec.X), 2f, (float)this._sizeLimitMax)), (float)((int)MathF.Clamp((float)((int)vec.Y), 2f, (float)this._sizeLimitMax)));
					Vec2 vec2 = this._resizeStartSize - vec;
					if (vec2.x != 0f || vec2.y != 0f)
					{
						this.BannerTableauWidget.UpdateSizeValueManual = vec;
						this.SizeValue = vec;
					}
				}
				if (this._currentMode != BannerBuilderEditableAreaWidget.BuilderMode.RightCornerResizing && base.EventManager.HoveredView == dragWidgetTopRight)
				{
					if (!flag || this._resizeStartWidget == null)
					{
						this._resizeStartMousePosition = base.EventManager.MousePosition;
						this._resizeStartWidget = base.EventManager.HoveredView;
						this._resizeStartSize = this.SizeValue;
					}
					this._currentMode = BannerBuilderEditableAreaWidget.BuilderMode.RightCornerResizing;
				}
			}
			else
			{
				this._resizeStartWidget = null;
				this._resizeStartSize = Vec2.Zero;
				this._currentMode = BannerBuilderEditableAreaWidget.BuilderMode.None;
			}
			this.UpdatePositionOfWidget(dragWidgetTopRight, BannerBuilderEditableAreaWidget.WidgetPlacementType.Max, this.RotationValue + BannerBuilderEditableAreaWidget.AngleOffsetForCorner(), 20f, 0f);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00037624 File Offset: 0x00035824
		private void HandleCursor()
		{
			if (this._currentMode == BannerBuilderEditableAreaWidget.BuilderMode.Rotating || base.EventManager.HoveredView == this.RotateWidget)
			{
				base.Context.ActiveCursorOfContext = UIContext.MouseCursors.Rotate;
				return;
			}
			if (this._currentMode == BannerBuilderEditableAreaWidget.BuilderMode.Positioning || base.EventManager.HoveredView == this)
			{
				base.Context.ActiveCursorOfContext = UIContext.MouseCursors.Move;
				return;
			}
			if (this._currentMode == BannerBuilderEditableAreaWidget.BuilderMode.HorizontalResizing || base.EventManager.HoveredView == this.GetWidgetFor(BannerBuilderEditableAreaWidget.EdgeResizeType.Right))
			{
				base.Context.ActiveCursorOfContext = UIContext.MouseCursors.HorizontalResize;
				return;
			}
			if (this._currentMode == BannerBuilderEditableAreaWidget.BuilderMode.VerticalResizing || base.EventManager.HoveredView == this.GetWidgetFor(BannerBuilderEditableAreaWidget.EdgeResizeType.Top))
			{
				base.Context.ActiveCursorOfContext = UIContext.MouseCursors.VerticalResize;
				return;
			}
			if (this._currentMode == BannerBuilderEditableAreaWidget.BuilderMode.RightCornerResizing || base.EventManager.HoveredView == this.DragWidgetTopRight)
			{
				base.Context.ActiveCursorOfContext = UIContext.MouseCursors.DiagonalRightResize;
				return;
			}
			base.Context.ActiveCursorOfContext = UIContext.MouseCursors.Default;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00037708 File Offset: 0x00035908
		private void UpdateEditableAreaVisual()
		{
			this.EditableAreaVisualWidget.HorizontalAlignment = HorizontalAlignment.Center;
			this.EditableAreaVisualWidget.VerticalAlignment = VerticalAlignment.Center;
			this.EditableAreaVisualWidget.WidthSizePolicy = SizePolicy.Fixed;
			this.EditableAreaVisualWidget.HeightSizePolicy = SizePolicy.Fixed;
			float num = (float)this.EditableAreaSize / (float)this.TotalAreaSize;
			this.EditableAreaVisualWidget.ScaledSuggestedWidth = base.Size.X * num;
			this.EditableAreaVisualWidget.ScaledSuggestedHeight = base.Size.Y * num;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00037785 File Offset: 0x00035985
		private ButtonWidget GetWidgetFor(BannerBuilderEditableAreaWidget.EdgeResizeType edgeResizeType)
		{
			if (edgeResizeType != BannerBuilderEditableAreaWidget.EdgeResizeType.Top)
			{
				return this.DragWidgetRight;
			}
			return this.DragWidgetTop;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00037798 File Offset: 0x00035998
		private void UpdatePositionOfWidget(Widget widget, BannerBuilderEditableAreaWidget.WidgetPlacementType placementType, float directionFromCenter, float distanceFromCenterModifier, float distanceFromEdgesModifier = 0f)
		{
			Vec2 v = BannerBuilderEditableAreaWidget.DirFromAngle(directionFromCenter);
			v.y *= -1f;
			float num = 0f;
			switch (placementType)
			{
			case BannerBuilderEditableAreaWidget.WidgetPlacementType.Horizontal:
				num = this._sizeOfSigil.X;
				break;
			case BannerBuilderEditableAreaWidget.WidgetPlacementType.Vertical:
				num = this._sizeOfSigil.Y;
				break;
			case BannerBuilderEditableAreaWidget.WidgetPlacementType.Max:
				num = this._sizeOfSigil.Length;
				break;
			}
			float f = (num * base._inverseScaleToUse + distanceFromCenterModifier) * 0.5f * base._scaleToUse;
			Vec2 pos = this._centerOfSigil + v * f;
			pos.x -= widget.Size.X / 2f;
			pos.y -= widget.Size.Y / 2f;
			this.ApplyPositionOffsetToWidget(widget, pos, distanceFromEdgesModifier * base._scaleToUse);
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00037874 File Offset: 0x00035A74
		private void ApplyPositionOffsetToWidget(Widget widget, Vec2 pos, float additionalModifier = 0f)
		{
			widget.ScaledPositionXOffset = MathF.Clamp(pos.x, 12f + additionalModifier, base.Size.X - (12f + additionalModifier));
			widget.ScaledPositionYOffset = MathF.Clamp(pos.y, 12f + additionalModifier, base.Size.Y - (12f + additionalModifier));
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x000378D7 File Offset: 0x00035AD7
		private void OnIsLayerPatternChanged(bool isLayerPattern)
		{
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x000378D9 File Offset: 0x00035AD9
		private void OnPositionChanged(Vec2 newPosition)
		{
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x000378DB File Offset: 0x00035ADB
		private void OnSizeChanged(Vec2 newSize)
		{
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x000378DD File Offset: 0x00035ADD
		private void OnRotationChanged(float newRotation)
		{
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x000378DF File Offset: 0x00035ADF
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x000378E7 File Offset: 0x00035AE7
		[Editor(false)]
		public bool IsLayerPattern
		{
			get
			{
				return this._isLayerPattern;
			}
			set
			{
				if (this._isLayerPattern != value)
				{
					this._isLayerPattern = value;
					base.OnPropertyChanged(value, "IsLayerPattern");
					this.OnIsLayerPatternChanged(value);
				}
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0003790C File Offset: 0x00035B0C
		// (set) Token: 0x06001428 RID: 5160 RVA: 0x00037914 File Offset: 0x00035B14
		[Editor(false)]
		public Vec2 PositionValue
		{
			get
			{
				return this._positionValue;
			}
			set
			{
				if (this._positionValue != value)
				{
					this._positionValue = value;
					base.OnPropertyChanged(value, "PositionValue");
					this.OnPositionChanged(value);
				}
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0003793E File Offset: 0x00035B3E
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x00037946 File Offset: 0x00035B46
		[Editor(false)]
		public Vec2 SizeValue
		{
			get
			{
				return this._sizeValue;
			}
			set
			{
				if (this._sizeValue != value)
				{
					this._sizeValue = value;
					base.OnPropertyChanged(value, "SizeValue");
					this.OnSizeChanged(value);
				}
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00037970 File Offset: 0x00035B70
		// (set) Token: 0x0600142C RID: 5164 RVA: 0x00037978 File Offset: 0x00035B78
		[Editor(false)]
		public float RotationValue
		{
			get
			{
				return this._rotationValue;
			}
			set
			{
				if (this._rotationValue != value)
				{
					this._rotationValue = value;
					base.OnPropertyChanged(value, "RotationValue");
					this.OnRotationChanged(value);
				}
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0003799D File Offset: 0x00035B9D
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x000379A5 File Offset: 0x00035BA5
		[Editor(false)]
		public int EditableAreaSize
		{
			get
			{
				return this._editableAreaSize;
			}
			set
			{
				if (this._editableAreaSize != value)
				{
					this._editableAreaSize = value;
					base.OnPropertyChanged(value, "EditableAreaSize");
				}
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x000379C3 File Offset: 0x00035BC3
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x000379CB File Offset: 0x00035BCB
		[Editor(false)]
		public int TotalAreaSize
		{
			get
			{
				return this._totalAreaSize;
			}
			set
			{
				if (this._totalAreaSize != value)
				{
					this._totalAreaSize = value;
					base.OnPropertyChanged(value, "TotalAreaSize");
				}
			}
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x000379EC File Offset: 0x00035BEC
		private static Vec2 DirFromAngle(float angle)
		{
			float x = angle * 6.2831855f;
			return new Vec2(-MathF.Sin(x), MathF.Cos(x));
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00037A18 File Offset: 0x00035C18
		private static float AngleFromDir(Vec2 directionVector)
		{
			float num;
			if (directionVector.X < 0f)
			{
				num = (float)Math.Atan2((double)directionVector.X, (double)directionVector.Y) * 57.29578f * -1f;
			}
			else
			{
				num = 360f - (float)Math.Atan2((double)directionVector.X, (double)directionVector.Y) * 57.29578f;
			}
			return num / 360f;
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00037A82 File Offset: 0x00035C82
		private static float AngleOffsetForEdge(BannerBuilderEditableAreaWidget.EdgeResizeType edge)
		{
			return 1f - (float)edge * 0.25f;
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00037A92 File Offset: 0x00035C92
		private static float AngleOffsetForCorner()
		{
			return 0.875f;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00037A9C File Offset: 0x00035C9C
		private static Vec2 TransformToParent(Vec2 a, Vec2 b)
		{
			return new Vec2(b.Y * a.X + b.X * a.Y, -b.X * a.X + b.Y * a.Y);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00037AF0 File Offset: 0x00035CF0
		private static Vector2 TransformToParent(Vector2 a, Vec2 b)
		{
			return new Vector2(b.Y * a.X + b.X * a.Y, -b.X * a.X + b.Y * a.Y);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00037B40 File Offset: 0x00035D40
		private static Vector2 TransformToParent(Vec2 a, Vector2 b)
		{
			return new Vector2(b.Y * a.X + b.X * a.Y, -b.X * a.X + b.Y * a.Y);
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x00037B8D File Offset: 0x00035D8D
		private static Vector2 TransformToParent(Vector2 a, Vector2 b)
		{
			return new Vector2(b.Y * a.X + b.X * a.Y, -b.X * a.Y + b.Y * a.Y);
		}

		// Token: 0x04000925 RID: 2341
		private bool _initialized;

		// Token: 0x04000926 RID: 2342
		private Vec2 _centerOfSigil;

		// Token: 0x04000927 RID: 2343
		private Vec2 _sizeOfSigil;

		// Token: 0x04000928 RID: 2344
		private float _positionLimitMin;

		// Token: 0x04000929 RID: 2345
		private float _positionLimitMax;

		// Token: 0x0400092A RID: 2346
		private float _areaScale;

		// Token: 0x0400092B RID: 2347
		private const int _sizeLimitMin = 2;

		// Token: 0x0400092C RID: 2348
		private int _sizeLimitMax;

		// Token: 0x0400092D RID: 2349
		private BannerBuilderEditableAreaWidget.BuilderMode _currentMode;

		// Token: 0x0400092E RID: 2350
		private Vector2 _latestMousePosition;

		// Token: 0x0400092F RID: 2351
		private Vector2 _resizeStartMousePosition;

		// Token: 0x04000930 RID: 2352
		private Widget _resizeStartWidget;

		// Token: 0x04000931 RID: 2353
		private Vec2 _resizeStartSize;

		// Token: 0x04000932 RID: 2354
		private bool _isLayerPattern;

		// Token: 0x04000933 RID: 2355
		private Vec2 _positionValue;

		// Token: 0x04000934 RID: 2356
		private Vec2 _sizeValue;

		// Token: 0x04000935 RID: 2357
		private float _rotationValue;

		// Token: 0x04000936 RID: 2358
		private int _editableAreaSize;

		// Token: 0x04000937 RID: 2359
		private int _totalAreaSize;

		// Token: 0x020001BD RID: 445
		private enum BuilderMode
		{
			// Token: 0x040009F3 RID: 2547
			None,
			// Token: 0x040009F4 RID: 2548
			Rotating,
			// Token: 0x040009F5 RID: 2549
			Positioning,
			// Token: 0x040009F6 RID: 2550
			HorizontalResizing,
			// Token: 0x040009F7 RID: 2551
			VerticalResizing,
			// Token: 0x040009F8 RID: 2552
			RightCornerResizing
		}

		// Token: 0x020001BE RID: 446
		private enum WidgetPlacementType
		{
			// Token: 0x040009FA RID: 2554
			Horizontal,
			// Token: 0x040009FB RID: 2555
			Vertical,
			// Token: 0x040009FC RID: 2556
			Max
		}

		// Token: 0x020001BF RID: 447
		private enum EdgeResizeType
		{
			// Token: 0x040009FE RID: 2558
			Top,
			// Token: 0x040009FF RID: 2559
			Right
		}
	}
}
