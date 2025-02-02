using System;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000C8 RID: 200
	public class ListTypeVM : ViewModel
	{
		// Token: 0x06001389 RID: 5001 RVA: 0x0004B72C File Offset: 0x0004992C
		public ListTypeVM(EncyclopediaPage encyclopediaPage)
		{
			this.EncyclopediaPage = encyclopediaPage;
			this.ID = encyclopediaPage.GetIdentifierNames()[0];
			this.ImageID = encyclopediaPage.GetStringID();
			this.Order = encyclopediaPage.HomePageOrderIndex;
			this.RefreshValues();
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0004B767 File Offset: 0x00049967
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.EncyclopediaPage.GetName().ToString();
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0004B785 File Offset: 0x00049985
		public void Execute()
		{
			Campaign.Current.EncyclopediaManager.GoToLink("ListPage", this.ID);
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x0004B7A1 File Offset: 0x000499A1
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x0004B7A9 File Offset: 0x000499A9
		[DataSourceProperty]
		public string ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if (value != this._id)
				{
					this._id = value;
					base.OnPropertyChangedWithValue<string>(value, "ID");
				}
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0004B7CC File Offset: 0x000499CC
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x0004B7D4 File Offset: 0x000499D4
		[DataSourceProperty]
		public int Order
		{
			get
			{
				return this._order;
			}
			set
			{
				if (value != this._order)
				{
					this._order = value;
					base.OnPropertyChangedWithValue(value, "Order");
				}
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0004B7F2 File Offset: 0x000499F2
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x0004B7FA File Offset: 0x000499FA
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

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x0004B81D File Offset: 0x00049A1D
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x0004B825 File Offset: 0x00049A25
		[DataSourceProperty]
		public string ImageID
		{
			get
			{
				return this._imageId;
			}
			set
			{
				if (value != this._imageId)
				{
					this._imageId = value;
					base.OnPropertyChangedWithValue<string>(value, "ImageID");
				}
			}
		}

		// Token: 0x04000909 RID: 2313
		public readonly EncyclopediaPage EncyclopediaPage;

		// Token: 0x0400090A RID: 2314
		private string _name;

		// Token: 0x0400090B RID: 2315
		private string _id;

		// Token: 0x0400090C RID: 2316
		private string _imageId;

		// Token: 0x0400090D RID: 2317
		private int _order;
	}
}
