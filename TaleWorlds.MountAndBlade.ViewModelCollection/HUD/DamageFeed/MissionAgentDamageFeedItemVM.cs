using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.DamageFeed
{
	// Token: 0x02000055 RID: 85
	public class MissionAgentDamageFeedItemVM : ViewModel
	{
		// Token: 0x060006BD RID: 1725 RVA: 0x0001B044 File Offset: 0x00019244
		public MissionAgentDamageFeedItemVM(string feedText, Action<MissionAgentDamageFeedItemVM> onRemove)
		{
			this._onRemove = onRemove;
			this.FeedText = feedText;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001B05A File Offset: 0x0001925A
		public void ExecuteRemove()
		{
			this._onRemove(this);
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001B068 File Offset: 0x00019268
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0001B070 File Offset: 0x00019270
		[DataSourceProperty]
		public string FeedText
		{
			get
			{
				return this._feedText;
			}
			set
			{
				if (value != this._feedText)
				{
					this._feedText = value;
					base.OnPropertyChangedWithValue<string>(value, "FeedText");
				}
			}
		}

		// Token: 0x04000337 RID: 823
		private readonly Action<MissionAgentDamageFeedItemVM> _onRemove;

		// Token: 0x04000338 RID: 824
		private string _feedText;
	}
}
