using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TaleWorlds.Library.Graph
{
	// Token: 0x020000B1 RID: 177
	public class GraphVM : ViewModel
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x000145B8 File Offset: 0x000127B8
		public GraphVM(string horizontalAxisLabel, string verticalAxisLabel)
		{
			this.Lines = new MBBindingList<GraphLineVM>();
			this.HorizontalAxisLabel = horizontalAxisLabel;
			this.VerticalAxisLabel = verticalAxisLabel;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000145DC File Offset: 0x000127DC
		public void Draw([TupleElementNames(new string[]
		{
			"line",
			"points"
		})] IEnumerable<ValueTuple<GraphLineVM, IEnumerable<GraphLinePointVM>>> linesWithPoints, in Vec2 horizontalRange, in Vec2 verticalRange, float autoRangeHorizontalCoefficient = 1f, float autoRangeVerticalCoefficient = 1f, bool useAutoHorizontalRange = false, bool useAutoVerticalRange = false)
		{
			this.Lines.Clear();
			float num = float.MaxValue;
			float num2 = float.MinValue;
			float num3 = float.MaxValue;
			float num4 = float.MinValue;
			foreach (ValueTuple<GraphLineVM, IEnumerable<GraphLinePointVM>> valueTuple in linesWithPoints)
			{
				GraphLineVM item = valueTuple.Item1;
				foreach (GraphLinePointVM graphLinePointVM in valueTuple.Item2)
				{
					if (useAutoHorizontalRange)
					{
						if (graphLinePointVM.HorizontalValue < num)
						{
							num = graphLinePointVM.HorizontalValue;
						}
						if (graphLinePointVM.HorizontalValue > num2)
						{
							num2 = graphLinePointVM.HorizontalValue;
						}
					}
					if (useAutoVerticalRange)
					{
						if (graphLinePointVM.VerticalValue < num3)
						{
							num3 = graphLinePointVM.VerticalValue;
						}
						if (graphLinePointVM.VerticalValue > num4)
						{
							num4 = graphLinePointVM.VerticalValue;
						}
					}
					item.Points.Add(graphLinePointVM);
				}
				this.Lines.Add(item);
			}
			Vec2 vec = horizontalRange;
			float x = vec.X;
			vec = horizontalRange;
			float y = vec.Y;
			vec = verticalRange;
			float x2 = vec.X;
			vec = verticalRange;
			float y2 = vec.Y;
			bool flag = num != float.MaxValue && num2 != float.MinValue;
			bool flag2 = num3 != float.MaxValue && num4 != float.MinValue;
			if (useAutoHorizontalRange && flag)
			{
				GraphVM.ExtendRangeToNearestMultipleOfCoefficient(num, num2, autoRangeHorizontalCoefficient, out x, out y);
			}
			if (useAutoVerticalRange && flag2)
			{
				GraphVM.ExtendRangeToNearestMultipleOfCoefficient(num3, num4, autoRangeVerticalCoefficient, out x2, out y2);
			}
			this.HorizontalMinValue = x;
			this.HorizontalMaxValue = y;
			this.VerticalMinValue = x2;
			this.VerticalMaxValue = y2;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000147B4 File Offset: 0x000129B4
		private static void ExtendRangeToNearestMultipleOfCoefficient(float minValue, float maxValue, float coefficient, out float extendedMinValue, out float extendedMaxValue)
		{
			if (coefficient > 1E-05f)
			{
				extendedMinValue = (float)MathF.Floor(minValue / coefficient) * coefficient;
				extendedMaxValue = (float)MathF.Ceiling(maxValue / coefficient) * coefficient;
				if (extendedMinValue.ApproximatelyEqualsTo(extendedMaxValue, 1E-05f))
				{
					if (extendedMinValue - coefficient > 0f)
					{
						extendedMinValue -= coefficient;
						return;
					}
					extendedMaxValue += coefficient;
					return;
				}
			}
			else
			{
				extendedMinValue = minValue;
				extendedMaxValue = maxValue;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00014819 File Offset: 0x00012A19
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x00014821 File Offset: 0x00012A21
		[DataSourceProperty]
		public MBBindingList<GraphLineVM> Lines
		{
			get
			{
				return this._lines;
			}
			set
			{
				if (value != this._lines)
				{
					this._lines = value;
					base.OnPropertyChangedWithValue<MBBindingList<GraphLineVM>>(value, "Lines");
				}
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001483F File Offset: 0x00012A3F
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x00014847 File Offset: 0x00012A47
		[DataSourceProperty]
		public string HorizontalAxisLabel
		{
			get
			{
				return this._horizontalAxisLabel;
			}
			set
			{
				if (value != this._horizontalAxisLabel)
				{
					this._horizontalAxisLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "HorizontalAxisLabel");
				}
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001486A File Offset: 0x00012A6A
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x00014872 File Offset: 0x00012A72
		[DataSourceProperty]
		public string VerticalAxisLabel
		{
			get
			{
				return this._verticalAxisLabel;
			}
			set
			{
				if (value != this._verticalAxisLabel)
				{
					this._verticalAxisLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "VerticalAxisLabel");
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00014895 File Offset: 0x00012A95
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0001489D File Offset: 0x00012A9D
		[DataSourceProperty]
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
					base.OnPropertyChangedWithValue(value, "HorizontalMinValue");
				}
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x000148BB File Offset: 0x00012ABB
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x000148C3 File Offset: 0x00012AC3
		[DataSourceProperty]
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
					base.OnPropertyChangedWithValue(value, "HorizontalMaxValue");
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x000148E1 File Offset: 0x00012AE1
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x000148E9 File Offset: 0x00012AE9
		[DataSourceProperty]
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
					base.OnPropertyChangedWithValue(value, "VerticalMinValue");
				}
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00014907 File Offset: 0x00012B07
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0001490F File Offset: 0x00012B0F
		[DataSourceProperty]
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
					base.OnPropertyChangedWithValue(value, "VerticalMaxValue");
				}
			}
		}

		// Token: 0x040001E6 RID: 486
		private MBBindingList<GraphLineVM> _lines;

		// Token: 0x040001E7 RID: 487
		private string _horizontalAxisLabel;

		// Token: 0x040001E8 RID: 488
		private string _verticalAxisLabel;

		// Token: 0x040001E9 RID: 489
		private float _horizontalMinValue;

		// Token: 0x040001EA RID: 490
		private float _horizontalMaxValue;

		// Token: 0x040001EB RID: 491
		private float _verticalMinValue;

		// Token: 0x040001EC RID: 492
		private float _verticalMaxValue;
	}
}
