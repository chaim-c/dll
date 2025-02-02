using System;

namespace TaleWorlds.Library.Graph
{
	// Token: 0x020000B0 RID: 176
	public class GraphLineVM : ViewModel
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x0001451B File Offset: 0x0001271B
		public GraphLineVM(string ID, string name)
		{
			this.Points = new MBBindingList<GraphLinePointVM>();
			this.Name = name;
			this.ID = ID;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001453C File Offset: 0x0001273C
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x00014544 File Offset: 0x00012744
		[DataSourceProperty]
		public MBBindingList<GraphLinePointVM> Points
		{
			get
			{
				return this._points;
			}
			set
			{
				if (value != this._points)
				{
					this._points = value;
					base.OnPropertyChangedWithValue<MBBindingList<GraphLinePointVM>>(value, "Points");
				}
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00014562 File Offset: 0x00012762
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x0001456A File Offset: 0x0001276A
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001458D File Offset: 0x0001278D
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x00014595 File Offset: 0x00012795
		[DataSourceProperty]
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if (value != this._ID)
				{
					this._ID = value;
					base.OnPropertyChangedWithValue<string>(value, "ID");
				}
			}
		}

		// Token: 0x040001E3 RID: 483
		private MBBindingList<GraphLinePointVM> _points;

		// Token: 0x040001E4 RID: 484
		private string _name;

		// Token: 0x040001E5 RID: 485
		private string _ID;
	}
}
