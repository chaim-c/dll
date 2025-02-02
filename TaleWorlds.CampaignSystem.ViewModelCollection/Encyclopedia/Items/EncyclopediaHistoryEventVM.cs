using System;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000CC RID: 204
	public class EncyclopediaHistoryEventVM : EncyclopediaLinkVM
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x0004BAE0 File Offset: 0x00049CE0
		public EncyclopediaHistoryEventVM(IEncyclopediaLog log)
		{
			this._log = log;
			this.RefreshValues();
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0004BAF8 File Offset: 0x00049CF8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.HistoryEventTimeText = this._log.GameTime.ToString();
			this.HistoryEventText = this._log.GetEncyclopediaText().ToString();
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0004BB40 File Offset: 0x00049D40
		public void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0004BB52 File Offset: 0x00049D52
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x0004BB5A File Offset: 0x00049D5A
		[DataSourceProperty]
		public string HistoryEventTimeText
		{
			get
			{
				return this._historyEventTimeText;
			}
			set
			{
				if (value != this._historyEventTimeText)
				{
					this._historyEventTimeText = value;
					base.OnPropertyChangedWithValue<string>(value, "HistoryEventTimeText");
				}
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0004BB7D File Offset: 0x00049D7D
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x0004BB85 File Offset: 0x00049D85
		[DataSourceProperty]
		public string HistoryEventText
		{
			get
			{
				return this._historyEventText;
			}
			set
			{
				if (value != this._historyEventText)
				{
					this._historyEventText = value;
					base.OnPropertyChangedWithValue<string>(value, "HistoryEventText");
				}
			}
		}

		// Token: 0x04000918 RID: 2328
		private readonly IEncyclopediaLog _log;

		// Token: 0x04000919 RID: 2329
		private string _historyEventText;

		// Token: 0x0400091A RID: 2330
		private string _historyEventTimeText;
	}
}
