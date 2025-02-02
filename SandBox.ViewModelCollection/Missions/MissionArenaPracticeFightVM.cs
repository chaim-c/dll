using System;
using SandBox.Missions.MissionLogics.Arena;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Missions
{
	// Token: 0x02000025 RID: 37
	public class MissionArenaPracticeFightVM : ViewModel
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0000E5F5 File Offset: 0x0000C7F5
		public MissionArenaPracticeFightVM(ArenaPracticeFightMissionController practiceMissionController)
		{
			this._practiceMissionController = practiceMissionController;
			this._mission = practiceMissionController.Mission;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000E610 File Offset: 0x0000C810
		public void Tick()
		{
			this.IsPlayerPracticing = this._practiceMissionController.IsPlayerPracticing;
			Agent mainAgent = this._mission.MainAgent;
			if (mainAgent != null && mainAgent.IsActive())
			{
				int killCount = this._mission.MainAgent.KillCount;
				GameTexts.SetVariable("BEATEN_OPPONENT_COUNT", killCount);
				this.OpponentsBeatenText = GameTexts.FindText("str_beaten_opponent", null).ToString();
			}
			int remainingOpponentCount = this._practiceMissionController.RemainingOpponentCount;
			GameTexts.SetVariable("REMAINING_OPPONENT_COUNT", remainingOpponentCount);
			this.OpponentsRemainingText = GameTexts.FindText("str_remaining_opponent", null).ToString();
			this.UpdatePrizeText();
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		public void UpdatePrizeText()
		{
			bool remainingOpponentCount = this._practiceMissionController.RemainingOpponentCount != 0;
			int opponentCountBeatenByPlayer = this._practiceMissionController.OpponentCountBeatenByPlayer;
			int content = 0;
			if (!remainingOpponentCount)
			{
				content = 250;
			}
			else if (opponentCountBeatenByPlayer >= 3)
			{
				if (opponentCountBeatenByPlayer < 6)
				{
					content = 5;
				}
				else if (opponentCountBeatenByPlayer < 10)
				{
					content = 10;
				}
				else if (opponentCountBeatenByPlayer < 20)
				{
					content = 25;
				}
				else
				{
					content = 60;
				}
			}
			GameTexts.SetVariable("DENAR_AMOUNT", content);
			GameTexts.SetVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
			this.PrizeText = GameTexts.FindText("str_earned_denar", null).ToString();
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000E72F File Offset: 0x0000C92F
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000E737 File Offset: 0x0000C937
		[DataSourceProperty]
		public string OpponentsBeatenText
		{
			get
			{
				return this._opponentsBeatenText;
			}
			set
			{
				if (this._opponentsBeatenText != value)
				{
					this._opponentsBeatenText = value;
					base.OnPropertyChangedWithValue<string>(value, "OpponentsBeatenText");
				}
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000E75A File Offset: 0x0000C95A
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000E762 File Offset: 0x0000C962
		[DataSourceProperty]
		public string PrizeText
		{
			get
			{
				return this._prizeText;
			}
			set
			{
				if (this._prizeText != value)
				{
					this._prizeText = value;
					base.OnPropertyChangedWithValue<string>(value, "PrizeText");
				}
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000E785 File Offset: 0x0000C985
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000E78D File Offset: 0x0000C98D
		[DataSourceProperty]
		public string OpponentsRemainingText
		{
			get
			{
				return this._opponentsRemainingText;
			}
			set
			{
				if (this._opponentsRemainingText != value)
				{
					this._opponentsRemainingText = value;
					base.OnPropertyChangedWithValue<string>(value, "OpponentsRemainingText");
				}
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000E7B0 File Offset: 0x0000C9B0
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		public bool IsPlayerPracticing
		{
			get
			{
				return this._isPlayerPracticing;
			}
			set
			{
				if (this._isPlayerPracticing != value)
				{
					this._isPlayerPracticing = value;
					base.OnPropertyChangedWithValue(value, "IsPlayerPracticing");
				}
			}
		}

		// Token: 0x04000179 RID: 377
		private readonly Mission _mission;

		// Token: 0x0400017A RID: 378
		private readonly ArenaPracticeFightMissionController _practiceMissionController;

		// Token: 0x0400017B RID: 379
		private string _opponentsBeatenText;

		// Token: 0x0400017C RID: 380
		private string _opponentsRemainingText;

		// Token: 0x0400017D RID: 381
		private bool _isPlayerPracticing;

		// Token: 0x0400017E RID: 382
		private string _prizeText;
	}
}
