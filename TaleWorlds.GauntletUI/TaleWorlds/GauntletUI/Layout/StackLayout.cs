using System;
using System.Collections.Generic;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.Layout
{
	// Token: 0x02000044 RID: 68
	public class StackLayout : ILayout
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00010575 File Offset: 0x0000E775
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0001057D File Offset: 0x0000E77D
		public ContainerItemDescription DefaultItemDescription { get; private set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00010586 File Offset: 0x0000E786
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0001058E File Offset: 0x0000E78E
		public LayoutMethod LayoutMethod { get; set; }

		// Token: 0x06000409 RID: 1033 RVA: 0x00010597 File Offset: 0x0000E797
		public StackLayout()
		{
			this.DefaultItemDescription = new ContainerItemDescription();
			this._layoutBoxes = new Dictionary<int, LayoutBox>(64);
			this._parallelMeasureBasicChildDelegate = new TWParallel.ParallelForAuxPredicate(this.ParallelMeasureBasicChild);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000105CC File Offset: 0x0000E7CC
		public ContainerItemDescription GetItemDescription(Widget owner, Widget child, int childIndex)
		{
			Container container;
			if ((container = (owner as Container)) != null)
			{
				return container.GetItemDescription(child.Id, childIndex);
			}
			return this.DefaultItemDescription;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000105F8 File Offset: 0x0000E7F8
		public Vector2 MeasureChildren(Widget widget, Vector2 measureSpec, SpriteData spriteData, float renderScale)
		{
			Container container = widget as Container;
			Vector2 result = default(Vector2);
			if (widget.ChildCount > 0)
			{
				if (this.LayoutMethod == LayoutMethod.HorizontalLeftToRight || this.LayoutMethod == LayoutMethod.HorizontalRightToLeft || this.LayoutMethod == LayoutMethod.HorizontalCentered || this.LayoutMethod == LayoutMethod.HorizontalSpaced)
				{
					result = this.MeasureLinear(widget, measureSpec, AlignmentAxis.Horizontal);
					if (container != null && container.IsDragHovering)
					{
						result.X += 20f;
					}
				}
				else if (this.LayoutMethod == LayoutMethod.VerticalBottomToTop || this.LayoutMethod == LayoutMethod.VerticalTopToBottom || this.LayoutMethod == LayoutMethod.VerticalCentered)
				{
					result = this.MeasureLinear(widget, measureSpec, AlignmentAxis.Vertical);
					if (container != null && container.IsDragHovering)
					{
						result.Y += 20f;
					}
				}
			}
			return result;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000106AC File Offset: 0x0000E8AC
		public void OnLayout(Widget widget, float left, float bottom, float right, float top)
		{
			if (this.LayoutMethod == LayoutMethod.HorizontalLeftToRight || this.LayoutMethod == LayoutMethod.HorizontalRightToLeft || this.LayoutMethod == LayoutMethod.HorizontalCentered || this.LayoutMethod == LayoutMethod.HorizontalSpaced)
			{
				this.LayoutLinearHorizontalLocal(widget, left, bottom, right, top);
				return;
			}
			if (this.LayoutMethod == LayoutMethod.VerticalBottomToTop || this.LayoutMethod == LayoutMethod.VerticalTopToBottom || this.LayoutMethod == LayoutMethod.VerticalCentered)
			{
				this.LayoutLinearVertical(widget, left, bottom, right, top);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00010712 File Offset: 0x0000E912
		private static float GetData(Vector2 vector2, int row)
		{
			if (row == 0)
			{
				return vector2.X;
			}
			return vector2.Y;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00010724 File Offset: 0x0000E924
		private static void SetData(ref Vector2 vector2, int row, float data)
		{
			if (row == 0)
			{
				vector2.X = data;
			}
			vector2.Y = data;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00010738 File Offset: 0x0000E938
		public int GetIndexForDrop(Container widget, Vector2 draggedWidgetPosition)
		{
			int row = 0;
			if (this.LayoutMethod == LayoutMethod.VerticalBottomToTop || this.LayoutMethod == LayoutMethod.VerticalTopToBottom || this.LayoutMethod == LayoutMethod.VerticalCentered)
			{
				row = 1;
			}
			bool flag = this.LayoutMethod == LayoutMethod.HorizontalRightToLeft || this.LayoutMethod == LayoutMethod.VerticalTopToBottom || this.LayoutMethod == LayoutMethod.VerticalCentered;
			float data = StackLayout.GetData(draggedWidgetPosition, row);
			int result = 0;
			bool flag2 = false;
			int num = 0;
			while (num != widget.ChildCount && !flag2)
			{
				Widget child = widget.GetChild(num);
				if (child != null)
				{
					float data2 = StackLayout.GetData(child.GlobalPosition * child.Context.CustomScale, row);
					float num2 = data2 + StackLayout.GetData(child.Size, row);
					float num3 = (data2 + num2) / 2f;
					if (!flag)
					{
						if (data < num3)
						{
							result = num;
							flag2 = true;
						}
					}
					else if (data > num3)
					{
						result = num;
						flag2 = true;
					}
				}
				num++;
			}
			if (!flag2)
			{
				result = widget.ChildCount;
			}
			return result;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00010818 File Offset: 0x0000EA18
		private void ParallelMeasureBasicChild(int startInclusive, int endExclusive)
		{
			for (int i = startInclusive; i < endExclusive; i++)
			{
				Widget child = this._parallelMeasureBasicChildWidget.GetChild(i);
				if (child == null)
				{
					Debug.FailedAssert("Trying to measure a null child for parent" + this._parallelMeasureBasicChildWidget.GetFullIDPath(), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Layout\\StackLayout.cs", "ParallelMeasureBasicChild", 184);
				}
				else if (child.IsVisible)
				{
					AlignmentAxis parallelMeasureBasicChildAlignmentAxis = this._parallelMeasureBasicChildAlignmentAxis;
					if (parallelMeasureBasicChildAlignmentAxis != AlignmentAxis.Horizontal)
					{
						if (parallelMeasureBasicChildAlignmentAxis == AlignmentAxis.Vertical)
						{
							if (child.HeightSizePolicy != SizePolicy.StretchToParent)
							{
								child.Measure(this._parallelMeasureBasicChildMeasureSpec);
							}
						}
					}
					else if (child.WidthSizePolicy != SizePolicy.StretchToParent)
					{
						child.Measure(this._parallelMeasureBasicChildMeasureSpec);
					}
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000108B8 File Offset: 0x0000EAB8
		private Vector2 MeasureLinear(Widget widget, Vector2 measureSpec, AlignmentAxis alignmentAxis)
		{
			this._parallelMeasureBasicChildWidget = widget;
			this._parallelMeasureBasicChildMeasureSpec = measureSpec;
			this._parallelMeasureBasicChildAlignmentAxis = alignmentAxis;
			TWParallel.For(0, widget.ChildCount, this._parallelMeasureBasicChildDelegate, 64);
			this._parallelMeasureBasicChildWidget = null;
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			int num4 = 0;
			for (int i = 0; i < widget.ChildCount; i++)
			{
				Widget child = widget.GetChild(i);
				if (child == null)
				{
					Debug.FailedAssert("Trying to measure a null child for parent" + widget.GetFullIDPath(), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Layout\\StackLayout.cs", "MeasureLinear", 234);
				}
				else if (child.IsVisible)
				{
					ContainerItemDescription itemDescription = this.GetItemDescription(widget, child, i);
					if (alignmentAxis == AlignmentAxis.Horizontal)
					{
						if (child.WidthSizePolicy == SizePolicy.StretchToParent)
						{
							num4++;
							num3 += itemDescription.WidthStretchRatio;
						}
						else
						{
							num2 += child.MeasuredSize.X + child.ScaledMarginLeft + child.ScaledMarginRight;
						}
						num = MathF.Max(num, child.MeasuredSize.Y + child.ScaledMarginTop + child.ScaledMarginBottom);
					}
					else if (alignmentAxis == AlignmentAxis.Vertical)
					{
						if (child.HeightSizePolicy == SizePolicy.StretchToParent)
						{
							num4++;
							num3 += itemDescription.HeightStretchRatio;
						}
						else
						{
							num += child.MeasuredSize.Y + child.ScaledMarginTop + child.ScaledMarginBottom;
						}
						num2 = MathF.Max(num2, child.MeasuredSize.X + child.ScaledMarginLeft + child.ScaledMarginRight);
					}
				}
			}
			if (num4 > 0)
			{
				float num5 = 0f;
				if (alignmentAxis == AlignmentAxis.Horizontal)
				{
					num5 = measureSpec.X - num2;
				}
				else if (alignmentAxis == AlignmentAxis.Vertical)
				{
					num5 = measureSpec.Y - num;
				}
				float num6 = num5;
				int num7 = num4;
				for (int j = 0; j < widget.ChildCount; j++)
				{
					Widget child2 = widget.GetChild(j);
					if (child2 == null)
					{
						Debug.FailedAssert("Trying to measure a null child for parent" + widget.GetFullIDPath(), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Layout\\StackLayout.cs", "MeasureLinear", 296);
					}
					else if (child2.IsVisible && ((alignmentAxis == AlignmentAxis.Horizontal && child2.WidthSizePolicy == SizePolicy.StretchToParent) || (alignmentAxis == AlignmentAxis.Vertical && child2.HeightSizePolicy == SizePolicy.StretchToParent)))
					{
						ContainerItemDescription itemDescription2 = this.GetItemDescription(widget, child2, j);
						Vector2 measureSpec2 = new Vector2(0f, 0f);
						if (num6 <= 0f)
						{
							if (alignmentAxis == AlignmentAxis.Horizontal)
							{
								measureSpec2 = new Vector2(0f, measureSpec.Y);
							}
							else if (alignmentAxis == AlignmentAxis.Vertical)
							{
								measureSpec2 = new Vector2(measureSpec.X, 0f);
							}
						}
						else if (alignmentAxis == AlignmentAxis.Horizontal)
						{
							float x = num5 * itemDescription2.WidthStretchRatio / num3;
							if (num7 == 1)
							{
								x = num6;
							}
							measureSpec2 = new Vector2(x, measureSpec.Y);
						}
						else if (alignmentAxis == AlignmentAxis.Vertical)
						{
							float y = num5 * itemDescription2.HeightStretchRatio / num3;
							if (num7 == 1)
							{
								y = num6;
							}
							measureSpec2 = new Vector2(measureSpec.X, y);
						}
						child2.Measure(measureSpec2);
						num7--;
						if (alignmentAxis == AlignmentAxis.Horizontal)
						{
							num6 -= child2.MeasuredSize.X;
							num2 += child2.MeasuredSize.X;
							num = MathF.Max(num, child2.MeasuredSize.Y);
						}
						else if (alignmentAxis == AlignmentAxis.Vertical)
						{
							num6 -= child2.MeasuredSize.Y;
							num += child2.MeasuredSize.Y;
							num2 = MathF.Max(num2, child2.MeasuredSize.X);
						}
					}
				}
			}
			float x2 = num2;
			float y2 = num;
			return new Vector2(x2, y2);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00010C1C File Offset: 0x0000EE1C
		private void ParallelUpdateLayouts(Widget widget)
		{
			StackLayout.<>c__DisplayClass23_0 CS$<>8__locals1 = new StackLayout.<>c__DisplayClass23_0();
			CS$<>8__locals1.widget = widget;
			CS$<>8__locals1.<>4__this = this;
			TWParallel.For(0, CS$<>8__locals1.widget.ChildCount, new TWParallel.ParallelForAuxPredicate(CS$<>8__locals1.<ParallelUpdateLayouts>g__UpdateChildLayoutMT|0), 16);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00010C5C File Offset: 0x0000EE5C
		private void LayoutLinearHorizontalLocal(Widget widget, float left, float bottom, float right, float top)
		{
			Container container = widget as Container;
			float num = 0f;
			float top2 = 0f;
			float num2 = right - left;
			float bottom2 = bottom - top;
			if (this.LayoutMethod != LayoutMethod.HorizontalRightToLeft && this.LayoutMethod == LayoutMethod.HorizontalCentered)
			{
				float num3 = 0f;
				for (int i = 0; i < widget.ChildCount; i++)
				{
					Widget child = widget.GetChild(i);
					if (child == null)
					{
						Debug.FailedAssert("Trying to measure a null child for parent" + widget.GetFullIDPath(), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Layout\\StackLayout.cs", "LayoutLinearHorizontalLocal", 417);
					}
					else if (child.IsVisible)
					{
						num3 += child.MeasuredSize.X + child.ScaledMarginLeft + child.ScaledMarginRight;
					}
				}
				num = (right - left) / 2f - num3 / 2f;
			}
			this._layoutBoxes.Clear();
			int num4 = 0;
			for (int j = 0; j < widget.ChildCount; j++)
			{
				if (widget.Children[j].IsVisible)
				{
					num4++;
				}
			}
			if (num4 > 0)
			{
				for (int k = 0; k < widget.ChildCount; k++)
				{
					Widget widget2 = widget.Children[k];
					if (widget2 == null)
					{
						Debug.FailedAssert("Trying to measure a null child for parent" + widget.GetFullIDPath(), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Layout\\StackLayout.cs", "LayoutLinearHorizontalLocal", 448);
					}
					else if (widget2.IsVisible)
					{
						float num5 = widget2.MeasuredSize.X + widget2.ScaledMarginLeft + widget2.ScaledMarginRight;
						if (container != null && container.IsDragHovering && k == container.DragHoverInsertionIndex)
						{
							num5 += 20f;
						}
						if (this.LayoutMethod == LayoutMethod.HorizontalRightToLeft)
						{
							num = num2 - num5;
						}
						else if (this.LayoutMethod == LayoutMethod.HorizontalSpaced)
						{
							if (num4 > 1)
							{
								if (k == 0)
								{
									num = 0f;
									num2 = left + widget2.MeasuredSize.X;
								}
								else if (k == widget.ChildCount - 1)
								{
									num2 = right - left;
									num = num2 - widget2.MeasuredSize.X;
								}
								else
								{
									float num6 = (widget.MeasuredSize.X - widget2.MeasuredSize.X * (float)num4) / (float)(num4 - 1);
									num += widget2.MeasuredSize.X + num6;
									num2 = num + widget2.MeasuredSize.X;
								}
							}
							else
							{
								num = widget.MeasuredSize.X / 2f - widget2.MeasuredSize.X / 2f;
								num2 = num + widget2.MeasuredSize.X / 2f;
							}
						}
						else
						{
							num2 = num + num5;
						}
						if (widget.ChildCount < 64)
						{
							widget2.Layout(num, bottom2, num2, top2);
						}
						else
						{
							LayoutBox value = default(LayoutBox);
							value.Left = num;
							value.Right = num2;
							value.Bottom = bottom2;
							value.Top = top2;
							this._layoutBoxes.Add(k, value);
						}
						if (this.LayoutMethod == LayoutMethod.HorizontalRightToLeft)
						{
							num2 = num;
						}
						else if (this.LayoutMethod == LayoutMethod.HorizontalLeftToRight || this.LayoutMethod == LayoutMethod.HorizontalCentered)
						{
							num = num2;
						}
					}
					else
					{
						this._layoutBoxes.Add(k, default(LayoutBox));
					}
				}
			}
			if (widget.ChildCount >= 64)
			{
				this.ParallelUpdateLayouts(widget);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00010FA0 File Offset: 0x0000F1A0
		private void LayoutLinearVertical(Widget widget, float left, float bottom, float right, float top)
		{
			Container container = widget as Container;
			float left2 = 0f;
			float num = 0f;
			float num2 = bottom - top;
			float right2 = right - left;
			if (this.LayoutMethod != LayoutMethod.VerticalTopToBottom && this.LayoutMethod == LayoutMethod.VerticalCentered)
			{
				float num3 = 0f;
				for (int i = 0; i < widget.ChildCount; i++)
				{
					Widget child = widget.GetChild(i);
					if (child != null && child.IsVisible)
					{
						num3 += child.MeasuredSize.Y + child.ScaledMarginTop + child.ScaledMarginBottom;
					}
				}
				num2 = (bottom - top) / 2f - num3 / 2f;
				num = (bottom - top) / 2f + num3 / 2f;
			}
			this._layoutBoxes.Clear();
			for (int j = 0; j < widget.ChildCount; j++)
			{
				Widget child2 = widget.GetChild(j);
				if (child2 != null && child2.IsVisible)
				{
					if (container != null && container.IsDragHovering && j == container.DragHoverInsertionIndex)
					{
						if (this.LayoutMethod == LayoutMethod.VerticalBottomToTop)
						{
							num += 20f;
						}
						else
						{
							num2 -= 20f;
						}
					}
					float num4 = child2.MeasuredSize.Y + child2.ScaledMarginTop + child2.ScaledMarginBottom;
					if (this.LayoutMethod == LayoutMethod.VerticalBottomToTop)
					{
						num2 = num + num4;
					}
					else
					{
						num = num2 - num4;
					}
					if (widget.ChildCount < 64)
					{
						child2.Layout(left2, num2, right2, num);
					}
					else
					{
						LayoutBox value = default(LayoutBox);
						value.Left = left2;
						value.Right = right2;
						value.Bottom = num2;
						value.Top = num;
						this._layoutBoxes.Add(j, value);
					}
					if (this.LayoutMethod == LayoutMethod.VerticalBottomToTop)
					{
						num = num2;
					}
					else
					{
						num2 = num;
					}
				}
				else
				{
					this._layoutBoxes.Add(j, default(LayoutBox));
				}
			}
			if (widget.ChildCount >= 64)
			{
				this.ParallelUpdateLayouts(widget);
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001118C File Offset: 0x0000F38C
		public Vector2 GetDropGizmoPosition(Container widget, Vector2 draggedWidgetPosition)
		{
			int row = 0;
			if (this.LayoutMethod == LayoutMethod.VerticalBottomToTop || this.LayoutMethod == LayoutMethod.VerticalTopToBottom || this.LayoutMethod == LayoutMethod.VerticalCentered)
			{
				row = 1;
			}
			bool flag = this.LayoutMethod == LayoutMethod.HorizontalRightToLeft || this.LayoutMethod == LayoutMethod.VerticalTopToBottom || this.LayoutMethod == LayoutMethod.VerticalCentered;
			int indexForDrop = this.GetIndexForDrop(widget, draggedWidgetPosition);
			int num = indexForDrop - 1;
			Vector2 globalPosition = widget.GlobalPosition;
			Vector2 globalPosition2 = widget.GlobalPosition;
			if (!flag)
			{
				if (num >= 0 && num < widget.ChildCount)
				{
					Widget child = widget.GetChild(num);
					StackLayout.SetData(ref globalPosition, row, StackLayout.GetData(child.GlobalPosition, row) + StackLayout.GetData(child.Size, row));
				}
				if (indexForDrop >= 0 && indexForDrop < widget.ChildCount)
				{
					StackLayout.SetData(ref globalPosition2, row, StackLayout.GetData(widget.GetChild(indexForDrop).GlobalPosition, row));
				}
				else if (indexForDrop >= widget.ChildCount && widget.ChildCount > 0)
				{
					StackLayout.SetData(ref globalPosition2, row, StackLayout.GetData(globalPosition, row) + 20f);
				}
			}
			else
			{
				StackLayout.SetData(ref globalPosition, row, StackLayout.GetData(globalPosition, row) + StackLayout.GetData(widget.Size, row));
				StackLayout.SetData(ref globalPosition2, row, StackLayout.GetData(globalPosition2, row) + StackLayout.GetData(widget.Size, row));
				if (num >= 0 && num < widget.ChildCount)
				{
					Widget child2 = widget.GetChild(num);
					StackLayout.SetData(ref globalPosition, row, StackLayout.GetData(child2.GlobalPosition, row));
				}
				if (indexForDrop >= 0 && indexForDrop < widget.ChildCount)
				{
					Widget child3 = widget.GetChild(indexForDrop);
					StackLayout.SetData(ref globalPosition2, row, StackLayout.GetData(child3.GlobalPosition, row) + StackLayout.GetData(child3.Size, row));
				}
				else if (indexForDrop >= widget.ChildCount && widget.ChildCount > 0)
				{
					StackLayout.SetData(ref globalPosition2, row, StackLayout.GetData(globalPosition, row) - 20f);
				}
			}
			return new Vector2((globalPosition.X + globalPosition2.X) / 2f, (globalPosition.Y + globalPosition2.Y) / 2f);
		}

		// Token: 0x04000202 RID: 514
		private const int DragHoverAperture = 20;

		// Token: 0x04000203 RID: 515
		private readonly Dictionary<int, LayoutBox> _layoutBoxes;

		// Token: 0x04000204 RID: 516
		private Widget _parallelMeasureBasicChildWidget;

		// Token: 0x04000205 RID: 517
		private Vector2 _parallelMeasureBasicChildMeasureSpec;

		// Token: 0x04000206 RID: 518
		private AlignmentAxis _parallelMeasureBasicChildAlignmentAxis;

		// Token: 0x04000207 RID: 519
		private TWParallel.ParallelForAuxPredicate _parallelMeasureBasicChildDelegate;
	}
}
