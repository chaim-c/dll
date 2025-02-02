using System;

namespace TaleWorlds.Library.Graph
{
	// Token: 0x020000AF RID: 175
	public class GraphLinePointVM : ViewModel
	{
		// Token: 0x06000654 RID: 1620 RVA: 0x000144B9 File Offset: 0x000126B9
		public GraphLinePointVM(float horizontalValue, float verticalValue)
		{
			this.HorizontalValue = horizontalValue;
			this.VerticalValue = verticalValue;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x000144CF File Offset: 0x000126CF
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x000144D7 File Offset: 0x000126D7
		[DataSourceProperty]
		public float HorizontalValue
		{
			get
			{
				return this._horizontalValue;
			}
			set
			{
				if (value != this._horizontalValue)
				{
					this._horizontalValue = value;
					base.OnPropertyChangedWithValue(value, "HorizontalValue");
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x000144F5 File Offset: 0x000126F5
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x000144FD File Offset: 0x000126FD
		[DataSourceProperty]
		public float VerticalValue
		{
			get
			{
				return this._verticalValue;
			}
			set
			{
				if (value != this._verticalValue)
				{
					this._verticalValue = value;
					base.OnPropertyChangedWithValue(value, "VerticalValue");
				}
			}
		}

		// Token: 0x040001E1 RID: 481
		private float _horizontalValue;

		// Token: 0x040001E2 RID: 482
		private float _verticalValue;
	}
}
