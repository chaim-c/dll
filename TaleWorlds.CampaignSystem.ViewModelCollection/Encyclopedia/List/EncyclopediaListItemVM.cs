using System;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List
{
	// Token: 0x020000C2 RID: 194
	public class EncyclopediaListItemVM : ViewModel
	{
		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0004A95E File Offset: 0x00048B5E
		// (set) Token: 0x0600133E RID: 4926 RVA: 0x0004A966 File Offset: 0x00048B66
		public object Object { get; private set; }

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0004A96F File Offset: 0x00048B6F
		public EncyclopediaListItem ListItem { get; }

		// Token: 0x06001340 RID: 4928 RVA: 0x0004A978 File Offset: 0x00048B78
		public EncyclopediaListItemVM(EncyclopediaListItem listItem)
		{
			this.Object = listItem.Object;
			this.Id = listItem.Id;
			this._type = listItem.TypeName;
			this.ListItem = listItem;
			this.PlayerCanSeeValues = listItem.PlayerCanSeeValues;
			this._onShowTooltip = listItem.OnShowTooltip;
			this.RefreshValues();
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0004A9D4 File Offset: 0x00048BD4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this.ListItem.Name;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0004A9ED File Offset: 0x00048BED
		public void Execute()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this._type, this.Id);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0004AA0A File Offset: 0x00048C0A
		public void SetComparedValue(EncyclopediaListItemComparerBase comparer)
		{
			this.ComparedValue = comparer.GetComparedValueText(this.ListItem);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0004AA1E File Offset: 0x00048C1E
		public void ExecuteBeginTooltip()
		{
			Action onShowTooltip = this._onShowTooltip;
			if (onShowTooltip == null)
			{
				return;
			}
			onShowTooltip();
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0004AA30 File Offset: 0x00048C30
		public void ExecuteEndTooltip()
		{
			if (this._onShowTooltip != null)
			{
				MBInformationManager.HideInformations();
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0004AA3F File Offset: 0x00048C3F
		// (set) Token: 0x06001347 RID: 4935 RVA: 0x0004AA47 File Offset: 0x00048C47
		[DataSourceProperty]
		public bool IsFiltered
		{
			get
			{
				return this._isFiltered;
			}
			set
			{
				if (value != this._isFiltered)
				{
					this._isFiltered = value;
					base.OnPropertyChangedWithValue(value, "IsFiltered");
				}
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0004AA65 File Offset: 0x00048C65
		// (set) Token: 0x06001349 RID: 4937 RVA: 0x0004AA6D File Offset: 0x00048C6D
		[DataSourceProperty]
		public bool PlayerCanSeeValues
		{
			get
			{
				return this._playerCanSeeValues;
			}
			set
			{
				if (value != this._playerCanSeeValues)
				{
					this._playerCanSeeValues = value;
					base.OnPropertyChangedWithValue(value, "PlayerCanSeeValues");
				}
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0004AA8B File Offset: 0x00048C8B
		// (set) Token: 0x0600134B RID: 4939 RVA: 0x0004AA93 File Offset: 0x00048C93
		[DataSourceProperty]
		public string Id
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
					base.OnPropertyChangedWithValue<string>(value, "Id");
				}
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0004AAB6 File Offset: 0x00048CB6
		// (set) Token: 0x0600134D RID: 4941 RVA: 0x0004AABE File Offset: 0x00048CBE
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

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0004AAE1 File Offset: 0x00048CE1
		// (set) Token: 0x0600134F RID: 4943 RVA: 0x0004AAE9 File Offset: 0x00048CE9
		[DataSourceProperty]
		public string ComparedValue
		{
			get
			{
				return this._comparedValue;
			}
			set
			{
				if (value != this._comparedValue)
				{
					this._comparedValue = value;
					base.OnPropertyChangedWithValue<string>(value, "ComparedValue");
				}
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0004AB0C File Offset: 0x00048D0C
		// (set) Token: 0x06001351 RID: 4945 RVA: 0x0004AB14 File Offset: 0x00048D14
		[DataSourceProperty]
		public bool IsBookmarked
		{
			get
			{
				return this._isBookmarked;
			}
			set
			{
				if (value != this._isBookmarked)
				{
					this._isBookmarked = value;
					base.OnPropertyChangedWithValue(value, "IsBookmarked");
				}
			}
		}

		// Token: 0x040008EC RID: 2284
		private readonly string _type;

		// Token: 0x040008ED RID: 2285
		private readonly Action _onShowTooltip;

		// Token: 0x040008EE RID: 2286
		private string _id;

		// Token: 0x040008EF RID: 2287
		private string _name;

		// Token: 0x040008F0 RID: 2288
		private string _comparedValue;

		// Token: 0x040008F1 RID: 2289
		private bool _isFiltered;

		// Token: 0x040008F2 RID: 2290
		private bool _isBookmarked;

		// Token: 0x040008F3 RID: 2291
		private bool _playerCanSeeValues;
	}
}
