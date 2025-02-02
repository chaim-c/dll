using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Source.Missions.Handlers
{
	// Token: 0x020003B7 RID: 951
	public class BasicMissionHandler : MissionLogic
	{
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x000D34DF File Offset: 0x000D16DF
		// (set) Token: 0x060032DB RID: 13019 RVA: 0x000D34E7 File Offset: 0x000D16E7
		public bool IsWarningWidgetOpened { get; private set; }

		// Token: 0x060032DC RID: 13020 RVA: 0x000D34F0 File Offset: 0x000D16F0
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this.IsWarningWidgetOpened = false;
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000D34FF File Offset: 0x000D16FF
		public void CreateWarningWidgetForResult(BattleEndLogic.ExitResult result)
		{
			if (!GameNetwork.IsClient)
			{
				MBCommon.PauseGameEngine();
			}
			this._isSurrender = (result == BattleEndLogic.ExitResult.SurrenderSiege);
			InformationManager.ShowInquiry(this._isSurrender ? this.GetSurrenderPopupData() : this.GetRetreatPopUpData(), true, false);
			this.IsWarningWidgetOpened = true;
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000D353B File Offset: 0x000D173B
		private void CloseSelectionWidget()
		{
			if (!this.IsWarningWidgetOpened)
			{
				return;
			}
			this.IsWarningWidgetOpened = false;
			if (!GameNetwork.IsClient)
			{
				MBCommon.UnPauseGameEngine();
			}
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000D3559 File Offset: 0x000D1759
		private void OnEventCancelSelectionWidget()
		{
			this.CloseSelectionWidget();
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000D3564 File Offset: 0x000D1764
		private void OnEventAcceptSelectionWidget()
		{
			MissionLogic[] array = base.Mission.MissionLogics.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnBattleEnded();
			}
			this.CloseSelectionWidget();
			if (this._isSurrender)
			{
				base.Mission.SurrenderMission();
				return;
			}
			base.Mission.RetreatMission();
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000D35C0 File Offset: 0x000D17C0
		private InquiryData GetRetreatPopUpData()
		{
			return new InquiryData("", GameTexts.FindText("str_retreat_question", null).ToString(), true, true, GameTexts.FindText("str_ok", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action(this.OnEventAcceptSelectionWidget), new Action(this.OnEventCancelSelectionWidget), "", 0f, null, null, null);
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000D3630 File Offset: 0x000D1830
		private InquiryData GetSurrenderPopupData()
		{
			return new InquiryData(GameTexts.FindText("str_surrender", null).ToString(), GameTexts.FindText("str_surrender_question", null).ToString(), true, true, GameTexts.FindText("str_ok", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action(this.OnEventAcceptSelectionWidget), new Action(this.OnEventCancelSelectionWidget), "", 0f, null, null, null);
		}

		// Token: 0x0400160C RID: 5644
		private bool _isSurrender;
	}
}
