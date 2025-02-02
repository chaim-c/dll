using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.Compass
{
	// Token: 0x02000057 RID: 87
	public class CompassMarkerVM : ViewModel
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0001B17A File Offset: 0x0001937A
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x0001B182 File Offset: 0x00019382
		public float Angle { get; private set; }

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001B18B File Offset: 0x0001938B
		public CompassMarkerVM(bool isPrimary, float angle, string text)
		{
			this.IsPrimary = isPrimary;
			this.Angle = angle;
			this.Text = (this.IsPrimary ? text : ("-" + text + "-"));
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001B1C2 File Offset: 0x000193C2
		public void Refresh(float circleX, float x, float distance)
		{
			this.FullPosition = circleX;
			this.Position = x;
			this.Distance = MathF.Round(distance);
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001B1DE File Offset: 0x000193DE
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0001B1E6 File Offset: 0x000193E6
		[DataSourceProperty]
		public bool IsPrimary
		{
			get
			{
				return this._isPrimary;
			}
			set
			{
				if (value != this._isPrimary)
				{
					this._isPrimary = value;
					base.OnPropertyChangedWithValue(value, "IsPrimary");
				}
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001B204 File Offset: 0x00019404
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0001B20C File Offset: 0x0001940C
		[DataSourceProperty]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (value != this._text)
				{
					this._text = value;
					base.OnPropertyChangedWithValue<string>(value, "Text");
				}
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001B22F File Offset: 0x0001942F
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0001B237 File Offset: 0x00019437
		[DataSourceProperty]
		public int Distance
		{
			get
			{
				return this._distance;
			}
			set
			{
				if (value != this._distance)
				{
					this._distance = value;
					base.OnPropertyChangedWithValue(value, "Distance");
				}
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0001B255 File Offset: 0x00019455
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0001B25D File Offset: 0x0001945D
		[DataSourceProperty]
		public float Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (MathF.Abs(value - this._position) > 1E-45f)
				{
					this._position = value;
					base.OnPropertyChangedWithValue(value, "Position");
				}
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0001B286 File Offset: 0x00019486
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0001B28E File Offset: 0x0001948E
		[DataSourceProperty]
		public float FullPosition
		{
			get
			{
				return this._fullPosition;
			}
			set
			{
				if (MathF.Abs(value - this._fullPosition) > 1E-45f)
				{
					this._fullPosition = value;
					base.OnPropertyChangedWithValue(value, "FullPosition");
				}
			}
		}

		// Token: 0x0400033C RID: 828
		private bool _isPrimary;

		// Token: 0x0400033D RID: 829
		private string _text;

		// Token: 0x0400033E RID: 830
		private int _distance;

		// Token: 0x0400033F RID: 831
		private float _position;

		// Token: 0x04000340 RID: 832
		private float _fullPosition;
	}
}
