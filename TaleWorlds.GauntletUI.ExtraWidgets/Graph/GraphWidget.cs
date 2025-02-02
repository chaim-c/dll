using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.ExtraWidgets.Graph
{
	// Token: 0x02000017 RID: 23
	public class GraphWidget : Widget
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00006E03 File Offset: 0x00005003
		public GraphWidget(UIContext context) : base(context)
		{
			this.RefreshOnNextLateUpdate();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006E14 File Offset: 0x00005014
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			bool flag = Mathf.Abs(this._totalSizeCached.X - base.Size.X) > 1E-05f || Mathf.Abs(this._totalSizeCached.Y - base.Size.Y) > 1E-05f;
			this._totalSizeCached = base.Size;
			if (flag)
			{
				this.RefreshOnNextLateUpdate();
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006E8C File Offset: 0x0000508C
		private void Refresh()
		{
			if (this._dynamicWidgetsContainer != null)
			{
				base.RemoveChild(this._dynamicWidgetsContainer);
			}
			this._dynamicWidgetsContainer = new Widget(base.Context)
			{
				WidthSizePolicy = SizePolicy.StretchToParent,
				HeightSizePolicy = SizePolicy.StretchToParent
			};
			base.AddChildAtIndex(this._dynamicWidgetsContainer, 0);
			this._planeExtendedSize = base.Size * base._inverseScaleToUse - new Vec2(this.LeftSpace + this.RightSpace, this.TopSpace + this.BottomSpace);
			this._planeSize = this._planeExtendedSize - new Vec2(this.PlaneMarginRight, this.PlaneMarginTop);
			Widget widget = new Widget(base.Context)
			{
				WidthSizePolicy = SizePolicy.StretchToParent,
				HeightSizePolicy = SizePolicy.StretchToParent,
				MarginLeft = this.LeftSpace,
				MarginRight = this.RightSpace,
				MarginBottom = this.BottomSpace,
				MarginTop = this.TopSpace,
				DoNotAcceptEvents = true,
				DoNotPassEventsToChildren = true
			};
			this._dynamicWidgetsContainer.AddChild(widget);
			this.RefreshPlaneLines(widget);
			this.RefreshLabels(this._dynamicWidgetsContainer, true);
			this.RefreshLabels(this._dynamicWidgetsContainer, false);
			this.RefreshGraphLines();
			this._willRefreshThisFrame = false;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006FD0 File Offset: 0x000051D0
		private void RefreshPlaneLines(Widget planeWidget)
		{
			int num = 1;
			ListPanel listPanel = this.CreatePlaneLinesListPanel(LayoutMethod.VerticalBottomToTop);
			float marginBottom = this._planeSize.Y / (float)this.RowCount - (float)num;
			for (int i = 0; i < this.RowCount; i++)
			{
				Widget widget = new Widget(base.Context)
				{
					WidthSizePolicy = SizePolicy.StretchToParent,
					HeightSizePolicy = SizePolicy.Fixed,
					SuggestedHeight = (float)num,
					MarginBottom = marginBottom,
					Sprite = this.PlaneLineSprite,
					Color = this.PlaneLineColor
				};
				listPanel.AddChild(widget);
			}
			ListPanel listPanel2 = this.CreatePlaneLinesListPanel(LayoutMethod.HorizontalLeftToRight);
			float marginLeft = this._planeSize.X / (float)this.ColumnCount - (float)num;
			for (int j = 0; j < this.ColumnCount; j++)
			{
				Widget widget2 = new Widget(base.Context)
				{
					WidthSizePolicy = SizePolicy.Fixed,
					HeightSizePolicy = SizePolicy.StretchToParent,
					SuggestedWidth = (float)num,
					MarginLeft = marginLeft,
					Sprite = this.PlaneLineSprite,
					Color = this.PlaneLineColor
				};
				listPanel2.AddChild(widget2);
			}
			planeWidget.AddChild(listPanel);
			planeWidget.AddChild(listPanel2);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000070EC File Offset: 0x000052EC
		private void RefreshLabels(Widget container, bool isHorizontal)
		{
			int num = isHorizontal ? this.HorizontalLabelCount : this.VerticalLabelCount;
			float num2 = isHorizontal ? this.HorizontalMaxValue : this.VerticalMaxValue;
			float num3 = isHorizontal ? this.HorizontalMinValue : this.VerticalMinValue;
			if (num > 1)
			{
				int num4 = isHorizontal ? 2 : 4;
				ListPanel listPanel = new ListPanel(base.Context)
				{
					WidthSizePolicy = (isHorizontal ? SizePolicy.StretchToParent : SizePolicy.Fixed),
					HeightSizePolicy = (isHorizontal ? SizePolicy.Fixed : SizePolicy.StretchToParent),
					SuggestedWidth = (isHorizontal ? 0f : (this.LeftSpace - (float)num4)),
					SuggestedHeight = (isHorizontal ? (this.BottomSpace - (float)num4) : 0f),
					HorizontalAlignment = HorizontalAlignment.Left,
					VerticalAlignment = VerticalAlignment.Bottom,
					MarginLeft = (isHorizontal ? this.LeftSpace : 0f),
					MarginBottom = (isHorizontal ? 0f : this.BottomSpace),
					DoNotAcceptEvents = true,
					DoNotPassEventsToChildren = true
				};
				listPanel.StackLayout.LayoutMethod = (isHorizontal ? LayoutMethod.HorizontalLeftToRight : LayoutMethod.VerticalTopToBottom);
				float num5 = (num2 - num3) / (float)(num - 1);
				for (int i = 0; i < num - 1; i++)
				{
					float labelValue = num3 + num5 * (float)i;
					TextWidget widget = this.CreateLabelText(labelValue, isHorizontal);
					listPanel.AddChild(widget);
				}
				Widget widget2 = new Widget(base.Context)
				{
					WidthSizePolicy = (isHorizontal ? SizePolicy.Fixed : SizePolicy.StretchToParent),
					HeightSizePolicy = (isHorizontal ? SizePolicy.StretchToParent : SizePolicy.Fixed),
					SuggestedWidth = (isHorizontal ? (this.RightSpace + this.PlaneMarginRight) : 0f),
					SuggestedHeight = (isHorizontal ? 0f : (this.TopSpace + this.PlaneMarginTop))
				};
				TextWidget widget3 = this.CreateLabelText(num2, isHorizontal);
				widget2.AddChild(widget3);
				listPanel.AddChild(widget2);
				container.AddChild(listPanel);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000072B4 File Offset: 0x000054B4
		private void RefreshGraphLines()
		{
			if (this.LineContainerWidget != null)
			{
				using (List<Widget>.Enumerator enumerator = this.LineContainerWidget.Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GraphLineWidget graphLineWidget;
						if ((graphLineWidget = (enumerator.Current as GraphLineWidget)) != null)
						{
							this.RefreshLine(graphLineWidget);
						}
					}
				}
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000731C File Offset: 0x0000551C
		private void RefreshLine(GraphLineWidget graphLineWidget)
		{
			graphLineWidget.MarginLeft = this.LeftSpace;
			graphLineWidget.MarginRight = this.RightSpace + this.PlaneMarginRight;
			graphLineWidget.MarginBottom = this.BottomSpace;
			graphLineWidget.MarginTop = this.TopSpace + this.PlaneMarginTop;
			Widget pointContainerWidget = graphLineWidget.PointContainerWidget;
			using (List<Widget>.Enumerator enumerator = (((pointContainerWidget != null) ? pointContainerWidget.Children : null) ?? new List<Widget>()).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GraphLinePointWidget graphLinePointWidget;
					if ((graphLinePointWidget = (enumerator.Current as GraphLinePointWidget)) != null)
					{
						this.RefreshPoint(graphLinePointWidget, graphLineWidget);
					}
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000073CC File Offset: 0x000055CC
		private void RefreshPoint(GraphLinePointWidget graphLinePointWidget, GraphLineWidget graphLineWidget)
		{
			bool flag = this.HorizontalMaxValue - this.HorizontalMinValue > 1E-05f;
			bool flag2 = this.VerticalMaxValue - this.VerticalMinValue > 1E-05f;
			if (flag && flag2)
			{
				float num = (graphLinePointWidget.HorizontalValue - this.HorizontalMinValue) / (this.HorizontalMaxValue - this.HorizontalMinValue);
				num = MathF.Clamp(num, 0f, 1f);
				float marginLeft = this._planeSize.X * num - graphLinePointWidget.SuggestedWidth * 0.5f;
				float num2 = (graphLinePointWidget.VerticalValue - this.VerticalMinValue) / (this.VerticalMaxValue - this.VerticalMinValue);
				num2 = MathF.Clamp(num2, 0f, 1f);
				float marginBottom = this._planeSize.Y * num2 - graphLinePointWidget.SuggestedHeight * 0.5f;
				string state = string.IsNullOrEmpty(graphLineWidget.LineBrushStateName) ? "Default" : graphLineWidget.LineBrushStateName;
				graphLinePointWidget.MarginLeft = marginLeft;
				graphLinePointWidget.MarginBottom = marginBottom;
				graphLinePointWidget.SetState(state);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000074CF File Offset: 0x000056CF
		private ListPanel CreatePlaneLinesListPanel(LayoutMethod layoutMethod)
		{
			return new ListPanel(base.Context)
			{
				WidthSizePolicy = SizePolicy.StretchToParent,
				HeightSizePolicy = SizePolicy.StretchToParent,
				MarginTop = this.PlaneMarginTop,
				MarginRight = this.PlaneMarginRight,
				StackLayout = 
				{
					LayoutMethod = layoutMethod
				}
			};
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007510 File Offset: 0x00005710
		private TextWidget CreateLabelText(float labelValue, bool isHorizontal)
		{
			TextWidget textWidget = new TextWidget(base.Context)
			{
				WidthSizePolicy = SizePolicy.StretchToParent,
				HeightSizePolicy = SizePolicy.StretchToParent,
				Text = labelValue.ToString("G" + this.NumberOfValueLabelDecimalPlaces.ToString())
			};
			Brush brush = isHorizontal ? this.HorizontalValueLabelsBrush : this.VerticalValueLabelsBrush;
			if (brush != null)
			{
				textWidget.Brush = brush.Clone();
			}
			textWidget.Brush.TextHorizontalAlignment = (isHorizontal ? TextHorizontalAlignment.Left : TextHorizontalAlignment.Right);
			textWidget.Brush.TextVerticalAlignment = (isHorizontal ? TextVerticalAlignment.Top : TextVerticalAlignment.Bottom);
			return textWidget;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000075A4 File Offset: 0x000057A4
		private void OnLineContainerEventFire(Widget widget, string eventName, object[] eventArgs)
		{
			GraphLineWidget graphLineWidget;
			if (eventArgs.Length != 0 && (graphLineWidget = (eventArgs[0] as GraphLineWidget)) != null)
			{
				if (eventName == "ItemAdd")
				{
					GraphLineWidget graphLineWidget3 = graphLineWidget;
					graphLineWidget3.OnPointAdded = (Action<GraphLineWidget, GraphLinePointWidget>)Delegate.Combine(graphLineWidget3.OnPointAdded, new Action<GraphLineWidget, GraphLinePointWidget>(this.OnPointAdded));
					this.AddLateUpdateAction(delegate
					{
						this.RefreshLine(graphLineWidget);
					});
					return;
				}
				if (eventName == "ItemRemove")
				{
					GraphLineWidget graphLineWidget2 = graphLineWidget;
					graphLineWidget2.OnPointAdded = (Action<GraphLineWidget, GraphLinePointWidget>)Delegate.Remove(graphLineWidget2.OnPointAdded, new Action<GraphLineWidget, GraphLinePointWidget>(this.OnPointAdded));
				}
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00007654 File Offset: 0x00005854
		private void OnPointAdded(GraphLineWidget graphLineWidget, GraphLinePointWidget graphLinePointWidget)
		{
			this.AddLateUpdateAction(delegate
			{
				this.RefreshPoint(graphLinePointWidget, graphLineWidget);
			});
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007690 File Offset: 0x00005890
		private void AddLateUpdateAction(Action action)
		{
			base.EventManager.AddLateUpdateAction(this, delegate(float _)
			{
				Action action2 = action;
				if (action2 == null)
				{
					return;
				}
				action2();
			}, 1);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000076C3 File Offset: 0x000058C3
		private void RefreshOnNextLateUpdate()
		{
			if (!this._willRefreshThisFrame)
			{
				this._willRefreshThisFrame = true;
				this.AddLateUpdateAction(new Action(this.Refresh));
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000076E6 File Offset: 0x000058E6
		// (set) Token: 0x06000137 RID: 311 RVA: 0x000076EE File Offset: 0x000058EE
		public int RowCount
		{
			get
			{
				return this._rowCount;
			}
			set
			{
				if (value != this._rowCount)
				{
					this._rowCount = value;
					base.OnPropertyChanged(value, "RowCount");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00007712 File Offset: 0x00005912
		// (set) Token: 0x06000139 RID: 313 RVA: 0x0000771A File Offset: 0x0000591A
		public int ColumnCount
		{
			get
			{
				return this._columnCount;
			}
			set
			{
				if (value != this._columnCount)
				{
					this._columnCount = value;
					base.OnPropertyChanged(value, "ColumnCount");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000773E File Offset: 0x0000593E
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00007746 File Offset: 0x00005946
		public int HorizontalLabelCount
		{
			get
			{
				return this._horizontalLabelCount;
			}
			set
			{
				if (value != this._horizontalLabelCount)
				{
					this._horizontalLabelCount = value;
					base.OnPropertyChanged(value, "HorizontalLabelCount");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000776A File Offset: 0x0000596A
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00007772 File Offset: 0x00005972
		public float HorizontalMinValue
		{
			get
			{
				return this._horizontalMinValue;
			}
			set
			{
				if (value != this._horizontalMinValue)
				{
					this._horizontalMinValue = value;
					base.OnPropertyChanged(value, "HorizontalMinValue");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00007796 File Offset: 0x00005996
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000779E File Offset: 0x0000599E
		public float HorizontalMaxValue
		{
			get
			{
				return this._horizontalMaxValue;
			}
			set
			{
				if (value != this._horizontalMaxValue)
				{
					this._horizontalMaxValue = value;
					base.OnPropertyChanged(value, "HorizontalMaxValue");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000077C2 File Offset: 0x000059C2
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000077CA File Offset: 0x000059CA
		public int VerticalLabelCount
		{
			get
			{
				return this._verticalLabelCount;
			}
			set
			{
				if (value != this._verticalLabelCount)
				{
					this._verticalLabelCount = value;
					base.OnPropertyChanged(value, "VerticalLabelCount");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000077EE File Offset: 0x000059EE
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000077F6 File Offset: 0x000059F6
		public float VerticalMinValue
		{
			get
			{
				return this._verticalMinValue;
			}
			set
			{
				if (value != this._verticalMinValue)
				{
					this._verticalMinValue = value;
					base.OnPropertyChanged(value, "VerticalMinValue");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000781A File Offset: 0x00005A1A
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00007822 File Offset: 0x00005A22
		public float VerticalMaxValue
		{
			get
			{
				return this._verticalMaxValue;
			}
			set
			{
				if (value != this._verticalMaxValue)
				{
					this._verticalMaxValue = value;
					base.OnPropertyChanged(value, "VerticalMaxValue");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00007846 File Offset: 0x00005A46
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000784E File Offset: 0x00005A4E
		public Sprite PlaneLineSprite
		{
			get
			{
				return this._planeLineSprite;
			}
			set
			{
				if (value != this._planeLineSprite)
				{
					this._planeLineSprite = value;
					base.OnPropertyChanged<Sprite>(value, "PlaneLineSprite");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00007872 File Offset: 0x00005A72
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000787A File Offset: 0x00005A7A
		public Color PlaneLineColor
		{
			get
			{
				return this._planeLineColor;
			}
			set
			{
				if (value != this._planeLineColor)
				{
					this._planeLineColor = value;
					base.OnPropertyChanged(value, "PlaneLineColor");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000078A3 File Offset: 0x00005AA3
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000078AB File Offset: 0x00005AAB
		public float LeftSpace
		{
			get
			{
				return this._leftSpace;
			}
			set
			{
				if (value != this._leftSpace)
				{
					this._leftSpace = value;
					base.OnPropertyChanged(value, "LeftSpace");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000078CF File Offset: 0x00005ACF
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000078D7 File Offset: 0x00005AD7
		public float TopSpace
		{
			get
			{
				return this._topSpace;
			}
			set
			{
				if (value != this._topSpace)
				{
					this._topSpace = value;
					base.OnPropertyChanged(value, "TopSpace");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000078FB File Offset: 0x00005AFB
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00007903 File Offset: 0x00005B03
		public float RightSpace
		{
			get
			{
				return this._rightSpace;
			}
			set
			{
				if (value != this._rightSpace)
				{
					this._rightSpace = value;
					base.OnPropertyChanged(value, "RightSpace");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00007927 File Offset: 0x00005B27
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000792F File Offset: 0x00005B2F
		public float BottomSpace
		{
			get
			{
				return this._bottomSpace;
			}
			set
			{
				if (value != this._bottomSpace)
				{
					this._bottomSpace = value;
					base.OnPropertyChanged(value, "BottomSpace");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00007953 File Offset: 0x00005B53
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000795B File Offset: 0x00005B5B
		public float PlaneMarginTop
		{
			get
			{
				return this._planeMarginTop;
			}
			set
			{
				if (value != this._planeMarginTop)
				{
					this._planeMarginTop = value;
					base.OnPropertyChanged(value, "PlaneMarginTop");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000797F File Offset: 0x00005B7F
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00007987 File Offset: 0x00005B87
		public float PlaneMarginRight
		{
			get
			{
				return this._planeMarginRight;
			}
			set
			{
				if (value != this._planeMarginRight)
				{
					this._planeMarginRight = value;
					base.OnPropertyChanged(value, "PlaneMarginRight");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000079AB File Offset: 0x00005BAB
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000079B3 File Offset: 0x00005BB3
		public int NumberOfValueLabelDecimalPlaces
		{
			get
			{
				return this._numberOfValueLabelDecimalPlaces;
			}
			set
			{
				if (value != this._numberOfValueLabelDecimalPlaces)
				{
					this._numberOfValueLabelDecimalPlaces = value;
					base.OnPropertyChanged(value, "NumberOfValueLabelDecimalPlaces");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000079D7 File Offset: 0x00005BD7
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000079DF File Offset: 0x00005BDF
		public Brush HorizontalValueLabelsBrush
		{
			get
			{
				return this._horizontalValueLabelsBrush;
			}
			set
			{
				if (value != this._horizontalValueLabelsBrush)
				{
					this._horizontalValueLabelsBrush = value;
					base.OnPropertyChanged<Brush>(value, "HorizontalValueLabelsBrush");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00007A03 File Offset: 0x00005C03
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00007A0B File Offset: 0x00005C0B
		public Brush VerticalValueLabelsBrush
		{
			get
			{
				return this._verticalValueLabelsBrush;
			}
			set
			{
				if (value != this._verticalValueLabelsBrush)
				{
					this._verticalValueLabelsBrush = value;
					base.OnPropertyChanged<Brush>(value, "VerticalValueLabelsBrush");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007A2F File Offset: 0x00005C2F
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00007A37 File Offset: 0x00005C37
		public Brush LineBrush
		{
			get
			{
				return this._lineBrush;
			}
			set
			{
				if (value != this._lineBrush)
				{
					this._lineBrush = value;
					base.OnPropertyChanged<Brush>(value, "LineBrush");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007A5B File Offset: 0x00005C5B
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00007A64 File Offset: 0x00005C64
		public Widget LineContainerWidget
		{
			get
			{
				return this._lineContainerWidget;
			}
			set
			{
				if (value != this._lineContainerWidget)
				{
					if (this._lineContainerWidget != null)
					{
						this._lineContainerWidget.EventFire -= this.OnLineContainerEventFire;
						using (List<Widget>.Enumerator enumerator = this.LineContainerWidget.Children.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								GraphLineWidget graphLineWidget;
								if ((graphLineWidget = (enumerator.Current as GraphLineWidget)) != null)
								{
									GraphLineWidget graphLineWidget2 = graphLineWidget;
									graphLineWidget2.OnPointAdded = (Action<GraphLineWidget, GraphLinePointWidget>)Delegate.Remove(graphLineWidget2.OnPointAdded, new Action<GraphLineWidget, GraphLinePointWidget>(this.OnPointAdded));
								}
							}
						}
					}
					this._lineContainerWidget = value;
					if (this._lineContainerWidget != null)
					{
						this._lineContainerWidget.EventFire += this.OnLineContainerEventFire;
						using (List<Widget>.Enumerator enumerator = this.LineContainerWidget.Children.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								GraphLineWidget graphLineWidget3;
								if ((graphLineWidget3 = (enumerator.Current as GraphLineWidget)) != null)
								{
									GraphLineWidget graphLineWidget4 = graphLineWidget3;
									graphLineWidget4.OnPointAdded = (Action<GraphLineWidget, GraphLinePointWidget>)Delegate.Combine(graphLineWidget4.OnPointAdded, new Action<GraphLineWidget, GraphLinePointWidget>(this.OnPointAdded));
								}
							}
						}
					}
					base.OnPropertyChanged<Widget>(value, "LineContainerWidget");
					this.RefreshOnNextLateUpdate();
				}
			}
		}

		// Token: 0x04000091 RID: 145
		private Widget _dynamicWidgetsContainer;

		// Token: 0x04000092 RID: 146
		private bool _willRefreshThisFrame;

		// Token: 0x04000093 RID: 147
		private Vec2 _planeExtendedSize;

		// Token: 0x04000094 RID: 148
		private Vec2 _planeSize;

		// Token: 0x04000095 RID: 149
		private Vec2 _totalSizeCached;

		// Token: 0x04000096 RID: 150
		private Widget _lineContainerWidget;

		// Token: 0x04000097 RID: 151
		private int _rowCount;

		// Token: 0x04000098 RID: 152
		private int _columnCount;

		// Token: 0x04000099 RID: 153
		private int _horizontalLabelCount;

		// Token: 0x0400009A RID: 154
		private float _horizontalMinValue;

		// Token: 0x0400009B RID: 155
		private float _horizontalMaxValue;

		// Token: 0x0400009C RID: 156
		private int _verticalLabelCount;

		// Token: 0x0400009D RID: 157
		private float _verticalMinValue;

		// Token: 0x0400009E RID: 158
		private float _verticalMaxValue;

		// Token: 0x0400009F RID: 159
		private Sprite _planeLineSprite;

		// Token: 0x040000A0 RID: 160
		private Color _planeLineColor;

		// Token: 0x040000A1 RID: 161
		private float _leftSpace;

		// Token: 0x040000A2 RID: 162
		private float _topSpace;

		// Token: 0x040000A3 RID: 163
		private float _rightSpace;

		// Token: 0x040000A4 RID: 164
		private float _bottomSpace;

		// Token: 0x040000A5 RID: 165
		private float _planeMarginTop;

		// Token: 0x040000A6 RID: 166
		private float _planeMarginRight;

		// Token: 0x040000A7 RID: 167
		private int _numberOfValueLabelDecimalPlaces;

		// Token: 0x040000A8 RID: 168
		private Brush _horizontalValueLabelsBrush;

		// Token: 0x040000A9 RID: 169
		private Brush _verticalValueLabelsBrush;

		// Token: 0x040000AA RID: 170
		private Brush _lineBrush;
	}
}
