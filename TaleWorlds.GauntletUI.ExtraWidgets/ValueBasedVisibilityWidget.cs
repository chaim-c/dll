using System;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI.ExtraWidgets
{
	// Token: 0x02000014 RID: 20
	public class ValueBasedVisibilityWidget : Widget
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006B11 File Offset: 0x00004D11
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00006B19 File Offset: 0x00004D19
		public ValueBasedVisibilityWidget.WatchTypes WatchType { get; set; }

		// Token: 0x06000114 RID: 276 RVA: 0x00006B22 File Offset: 0x00004D22
		public ValueBasedVisibilityWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006B3D File Offset: 0x00004D3D
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00006B48 File Offset: 0x00004D48
		[Editor(false)]
		public int IndexToWatch
		{
			get
			{
				return this._indexToWatch;
			}
			set
			{
				if (this._indexToWatch != value)
				{
					this._indexToWatch = value;
					base.OnPropertyChanged(value, "IndexToWatch");
					switch (this.WatchType)
					{
					case ValueBasedVisibilityWidget.WatchTypes.Equal:
						base.IsVisible = (value == this.IndexToBeVisible);
						return;
					case ValueBasedVisibilityWidget.WatchTypes.BiggerThan:
						base.IsVisible = (value > this.IndexToBeVisible);
						return;
					case ValueBasedVisibilityWidget.WatchTypes.BiggerThanEqual:
						base.IsVisible = (value >= this.IndexToBeVisible);
						return;
					case ValueBasedVisibilityWidget.WatchTypes.LessThan:
						base.IsVisible = (value < this.IndexToBeVisible);
						return;
					case ValueBasedVisibilityWidget.WatchTypes.LessThanEqual:
						base.IsVisible = (value <= this.IndexToBeVisible);
						return;
					case ValueBasedVisibilityWidget.WatchTypes.NotEqual:
						base.IsVisible = (value != this.IndexToBeVisible);
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006C02 File Offset: 0x00004E02
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00006C0C File Offset: 0x00004E0C
		[Editor(false)]
		public float IndexToWatchFloat
		{
			get
			{
				return this._indexToWatchFloat;
			}
			set
			{
				if (this._indexToWatchFloat != value)
				{
					this._indexToWatchFloat = value;
					base.OnPropertyChanged(value, "IndexToWatchFloat");
					switch (this.WatchType)
					{
					case ValueBasedVisibilityWidget.WatchTypes.Equal:
						base.IsVisible = (value == this.IndexToBeVisibleFloat);
						return;
					case ValueBasedVisibilityWidget.WatchTypes.BiggerThan:
						base.IsVisible = (value > this.IndexToBeVisibleFloat);
						return;
					case ValueBasedVisibilityWidget.WatchTypes.BiggerThanEqual:
						base.IsVisible = (value >= this.IndexToBeVisibleFloat);
						return;
					case ValueBasedVisibilityWidget.WatchTypes.LessThan:
						base.IsVisible = (value < this.IndexToBeVisibleFloat);
						return;
					case ValueBasedVisibilityWidget.WatchTypes.LessThanEqual:
						base.IsVisible = (value <= this.IndexToBeVisibleFloat);
						return;
					case ValueBasedVisibilityWidget.WatchTypes.NotEqual:
						base.IsVisible = (value != this.IndexToBeVisibleFloat);
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006CC6 File Offset: 0x00004EC6
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00006CCE File Offset: 0x00004ECE
		[Editor(false)]
		public int IndexToBeVisible
		{
			get
			{
				return this._indexToBeVisible;
			}
			set
			{
				if (this._indexToBeVisible != value)
				{
					this._indexToBeVisible = value;
					base.OnPropertyChanged(value, "IndexToBeVisible");
				}
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006CEC File Offset: 0x00004EEC
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00006CF4 File Offset: 0x00004EF4
		[Editor(false)]
		public float IndexToBeVisibleFloat
		{
			get
			{
				return this._indexToBeVisibleFloat;
			}
			set
			{
				if (this._indexToBeVisibleFloat != value)
				{
					this._indexToBeVisibleFloat = value;
					base.OnPropertyChanged(value, "IndexToBeVisibleFloat");
				}
			}
		}

		// Token: 0x04000088 RID: 136
		private int _indexToBeVisible;

		// Token: 0x04000089 RID: 137
		private int _indexToWatch = -1;

		// Token: 0x0400008A RID: 138
		private float _indexToBeVisibleFloat;

		// Token: 0x0400008B RID: 139
		private float _indexToWatchFloat = -1f;

		// Token: 0x0200001C RID: 28
		public enum WatchTypes
		{
			// Token: 0x040000BB RID: 187
			Equal,
			// Token: 0x040000BC RID: 188
			BiggerThan,
			// Token: 0x040000BD RID: 189
			BiggerThanEqual,
			// Token: 0x040000BE RID: 190
			LessThan,
			// Token: 0x040000BF RID: 191
			LessThanEqual,
			// Token: 0x040000C0 RID: 192
			NotEqual
		}
	}
}
