using System;
using System.Linq;
using TaleWorlds.CampaignSystem.Encyclopedia;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia
{
	// Token: 0x020000B1 RID: 177
	public class EncyclopediaHomeVM : EncyclopediaPageVM
	{
		// Token: 0x06001174 RID: 4468 RVA: 0x00045440 File Offset: 0x00043640
		public EncyclopediaHomeVM(EncyclopediaPageArgs args) : base(args)
		{
			this.Lists = new MBBindingList<ListTypeVM>();
			foreach (EncyclopediaPage encyclopediaPage in from p in Campaign.Current.EncyclopediaManager.GetEncyclopediaPages()
			orderby p.HomePageOrderIndex
			select p)
			{
				this.Lists.Add(new ListTypeVM(encyclopediaPage));
			}
			this.RefreshValues();
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000454DC File Offset: 0x000436DC
		public override void Refresh()
		{
			base.Refresh();
			this.RefreshValues();
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000454EC File Offset: 0x000436EC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this._baseName = GameTexts.FindText("str_encyclopedia_name", null).ToString();
			this.HomeTitleText = GameTexts.FindText("str_encyclopedia_name", null).ToString();
			this.Lists.ApplyActionOnAllItems(delegate(ListTypeVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00045555 File Offset: 0x00043755
		public override string GetNavigationBarURL()
		{
			return GameTexts.FindText("str_encyclopedia_home", null).ToString() + " \\";
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00045571 File Offset: 0x00043771
		public override string GetName()
		{
			return this._baseName;
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00045579 File Offset: 0x00043779
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x00045581 File Offset: 0x00043781
		[DataSourceProperty]
		public bool IsListActive
		{
			get
			{
				return this._isListActive;
			}
			set
			{
				if (value != this._isListActive)
				{
					this._isListActive = value;
					base.OnPropertyChangedWithValue(value, "IsListActive");
				}
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0004559F File Offset: 0x0004379F
		// (set) Token: 0x0600117C RID: 4476 RVA: 0x000455A7 File Offset: 0x000437A7
		[DataSourceProperty]
		public string HomeTitleText
		{
			get
			{
				return this._homeTitleText;
			}
			set
			{
				if (value != this._homeTitleText)
				{
					this._homeTitleText = value;
					base.OnPropertyChangedWithValue<string>(value, "HomeTitleText");
				}
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x000455CA File Offset: 0x000437CA
		// (set) Token: 0x0600117E RID: 4478 RVA: 0x000455D2 File Offset: 0x000437D2
		[DataSourceProperty]
		public MBBindingList<ListTypeVM> Lists
		{
			get
			{
				return this._lists;
			}
			set
			{
				if (value != this._lists)
				{
					this._lists = value;
					base.OnPropertyChangedWithValue<MBBindingList<ListTypeVM>>(value, "Lists");
				}
			}
		}

		// Token: 0x04000820 RID: 2080
		private string _baseName;

		// Token: 0x04000821 RID: 2081
		private MBBindingList<ListTypeVM> _lists;

		// Token: 0x04000822 RID: 2082
		private bool _isListActive;

		// Token: 0x04000823 RID: 2083
		private string _homeTitleText;
	}
}
