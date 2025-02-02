using System;
using SandBox.BoardGames;
using SandBox.BoardGames.MissionLogics;
using SandBox.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.BoardGame
{
	// Token: 0x02000040 RID: 64
	public class BoardGameVM : ViewModel
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x000138E0 File Offset: 0x00011AE0
		public BoardGameVM()
		{
			this._missionBoardGameHandler = Mission.Current.GetMissionBehavior<MissionBoardGameLogic>();
			this.BoardGameType = this._missionBoardGameHandler.CurrentBoardGame.ToString();
			this.IsGameUsingDice = this._missionBoardGameHandler.RequiresDiceRolling();
			this.DiceResult = "-";
			this.Instructions = new BoardGameInstructionsVM(this._missionBoardGameHandler.CurrentBoardGame);
			this.RefreshValues();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001395C File Offset: 0x00011B5C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.RollDiceText = GameTexts.FindText("str_roll_dice", null).ToString();
			this.CloseText = GameTexts.FindText("str_close", null).ToString();
			this.ForfeitText = GameTexts.FindText("str_forfeit", null).ToString();
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000139B1 File Offset: 0x00011BB1
		public void Activate()
		{
			this.SwitchTurns();
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000139B9 File Offset: 0x00011BB9
		public void DiceRoll(int roll)
		{
			this.DiceResult = roll.ToString();
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000139C8 File Offset: 0x00011BC8
		public void SwitchTurns()
		{
			this.IsPlayersTurn = (this._missionBoardGameHandler.Board.PlayerTurn == PlayerTurn.PlayerOne || this._missionBoardGameHandler.Board.PlayerTurn == PlayerTurn.PlayerOneWaiting);
			this.TurnOwnerText = (this.IsPlayersTurn ? GameTexts.FindText("str_your_turn", null).ToString() : GameTexts.FindText("str_opponents_turn", null).ToString());
			this.DiceResult = "-";
			this.CanRoll = (this.IsPlayersTurn && this.IsGameUsingDice);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00013A55 File Offset: 0x00011C55
		public void ExecuteRoll()
		{
			if (this.CanRoll)
			{
				this._missionBoardGameHandler.RollDice();
				this.CanRoll = false;
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00013A74 File Offset: 0x00011C74
		public void ExecuteForfeit()
		{
			if (this._missionBoardGameHandler.Board.IsReady && this._missionBoardGameHandler.IsGameInProgress)
			{
				TextObject textObject = new TextObject("{=azJulvrp}{?IS_BETTING}You are going to lose {BET_AMOUNT}{GOLD_ICON} if you forfeit.{newline}{?}{\\?}Do you really want to forfeit?", null);
				textObject.SetTextVariable("IS_BETTING", (this._missionBoardGameHandler.BetAmount > 0) ? 1 : 0);
				textObject.SetTextVariable("BET_AMOUNT", this._missionBoardGameHandler.BetAmount);
				textObject.SetTextVariable("GOLD_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
				textObject.SetTextVariable("newline", "{=!}\n");
				InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_forfeit", null).ToString(), textObject.ToString(), true, true, new TextObject("{=aeouhelq}Yes", null).ToString(), new TextObject("{=8OkPHu4f}No", null).ToString(), new Action(this._missionBoardGameHandler.ForfeitGame), null, "", 0f, null, null, null), true, false);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00013B6C File Offset: 0x00011D6C
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM rollDiceKey = this.RollDiceKey;
			if (rollDiceKey == null)
			{
				return;
			}
			rollDiceKey.OnFinalize();
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00013B84 File Offset: 0x00011D84
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00013B8C File Offset: 0x00011D8C
		[DataSourceProperty]
		public BoardGameInstructionsVM Instructions
		{
			get
			{
				return this._instructions;
			}
			set
			{
				if (value != this._instructions)
				{
					this._instructions = value;
					base.OnPropertyChangedWithValue<BoardGameInstructionsVM>(value, "Instructions");
				}
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00013BAA File Offset: 0x00011DAA
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x00013BB2 File Offset: 0x00011DB2
		[DataSourceProperty]
		public bool CanRoll
		{
			get
			{
				return this._canRoll;
			}
			set
			{
				if (value != this._canRoll)
				{
					this._canRoll = value;
					base.OnPropertyChangedWithValue(value, "CanRoll");
				}
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00013BD0 File Offset: 0x00011DD0
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00013BD8 File Offset: 0x00011DD8
		[DataSourceProperty]
		public bool IsPlayersTurn
		{
			get
			{
				return this._isPlayersTurn;
			}
			set
			{
				if (value != this._isPlayersTurn)
				{
					this._isPlayersTurn = value;
					base.OnPropertyChangedWithValue(value, "IsPlayersTurn");
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00013BF6 File Offset: 0x00011DF6
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00013BFE File Offset: 0x00011DFE
		[DataSourceProperty]
		public bool IsGameUsingDice
		{
			get
			{
				return this._isGameUsingDice;
			}
			set
			{
				if (value != this._isGameUsingDice)
				{
					this._isGameUsingDice = value;
					base.OnPropertyChangedWithValue(value, "IsGameUsingDice");
				}
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00013C1C File Offset: 0x00011E1C
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00013C24 File Offset: 0x00011E24
		[DataSourceProperty]
		public string DiceResult
		{
			get
			{
				return this._diceResult;
			}
			set
			{
				if (value != this._diceResult)
				{
					this._diceResult = value;
					base.OnPropertyChangedWithValue<string>(value, "DiceResult");
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00013C47 File Offset: 0x00011E47
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00013C4F File Offset: 0x00011E4F
		[DataSourceProperty]
		public string RollDiceText
		{
			get
			{
				return this._rollDiceText;
			}
			set
			{
				if (value != this._rollDiceText)
				{
					this._rollDiceText = value;
					base.OnPropertyChangedWithValue<string>(value, "RollDiceText");
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00013C72 File Offset: 0x00011E72
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x00013C7A File Offset: 0x00011E7A
		[DataSourceProperty]
		public string TurnOwnerText
		{
			get
			{
				return this._turnOwnerText;
			}
			set
			{
				if (value != this._turnOwnerText)
				{
					this._turnOwnerText = value;
					base.OnPropertyChangedWithValue<string>(value, "TurnOwnerText");
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00013C9D File Offset: 0x00011E9D
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x00013CA5 File Offset: 0x00011EA5
		[DataSourceProperty]
		public string BoardGameType
		{
			get
			{
				return this._boardGameType;
			}
			set
			{
				if (value != this._boardGameType)
				{
					this._boardGameType = value;
					base.OnPropertyChangedWithValue<string>(value, "BoardGameType");
				}
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00013CC8 File Offset: 0x00011EC8
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00013CD0 File Offset: 0x00011ED0
		[DataSourceProperty]
		public string CloseText
		{
			get
			{
				return this._closeText;
			}
			set
			{
				if (value != this._closeText)
				{
					this._closeText = value;
					base.OnPropertyChangedWithValue<string>(value, "CloseText");
				}
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00013CF3 File Offset: 0x00011EF3
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x00013CFB File Offset: 0x00011EFB
		[DataSourceProperty]
		public string ForfeitText
		{
			get
			{
				return this._forfeitText;
			}
			set
			{
				if (value != this._forfeitText)
				{
					this._forfeitText = value;
					base.OnPropertyChangedWithValue<string>(value, "ForfeitText");
				}
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00013D1E File Offset: 0x00011F1E
		public void SetRollDiceKey(HotKey key)
		{
			this.RollDiceKey = InputKeyItemVM.CreateFromHotKey(key, false);
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00013D2D File Offset: 0x00011F2D
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x00013D35 File Offset: 0x00011F35
		[DataSourceProperty]
		public InputKeyItemVM RollDiceKey
		{
			get
			{
				return this._rollDiceKey;
			}
			set
			{
				if (value != this._rollDiceKey)
				{
					this._rollDiceKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "RollDiceKey");
				}
			}
		}

		// Token: 0x04000254 RID: 596
		private readonly MissionBoardGameLogic _missionBoardGameHandler;

		// Token: 0x04000255 RID: 597
		private BoardGameInstructionsVM _instructions;

		// Token: 0x04000256 RID: 598
		private string _turnOwnerText;

		// Token: 0x04000257 RID: 599
		private string _boardGameType;

		// Token: 0x04000258 RID: 600
		private bool _isGameUsingDice;

		// Token: 0x04000259 RID: 601
		private bool _isPlayersTurn;

		// Token: 0x0400025A RID: 602
		private bool _canRoll;

		// Token: 0x0400025B RID: 603
		private string _diceResult;

		// Token: 0x0400025C RID: 604
		private string _rollDiceText;

		// Token: 0x0400025D RID: 605
		private string _closeText;

		// Token: 0x0400025E RID: 606
		private string _forfeitText;

		// Token: 0x0400025F RID: 607
		private InputKeyItemVM _rollDiceKey;
	}
}
