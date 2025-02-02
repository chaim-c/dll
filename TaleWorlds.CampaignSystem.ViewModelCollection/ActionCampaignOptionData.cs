using System;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x0200000D RID: 13
	public class ActionCampaignOptionData : CampaignOptionData
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003D40 File Offset: 0x00001F40
		public ActionCampaignOptionData(string identifier, int priorityIndex, CampaignOptionEnableState enableState, Action action, Func<CampaignOptionDisableStatus> getIsDisabledWithReason = null) : base(identifier, priorityIndex, enableState, null, null, getIsDisabledWithReason, false, null, null)
		{
			this._action = action;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003D65 File Offset: 0x00001F65
		public override CampaignOptionDataType GetDataType()
		{
			return CampaignOptionDataType.Action;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003D68 File Offset: 0x00001F68
		public void ExecuteAction()
		{
			Action action = this._action;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x04000056 RID: 86
		private Action _action;
	}
}
