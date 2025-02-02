using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.BoardGame
{
	// Token: 0x0200003F RID: 63
	public class BoardGameInstructionVM : ViewModel
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x0001376A File Offset: 0x0001196A
		public BoardGameInstructionVM(CultureObject.BoardGameType game, int instructionIndex)
		{
			this._game = game;
			this._instructionIndex = instructionIndex;
			this.GameType = this._game.ToString();
			this.RefreshValues();
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000137A0 File Offset: 0x000119A0
		public override void RefreshValues()
		{
			base.RefreshValues();
			GameTexts.SetVariable("newline", "\n");
			this.TitleText = GameTexts.FindText("str_board_game_title", this._game.ToString() + "_" + this._instructionIndex).ToString();
			this.DescriptionText = GameTexts.FindText("str_board_game_instruction", this._game.ToString() + "_" + this._instructionIndex).ToString();
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00013838 File Offset: 0x00011A38
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x00013840 File Offset: 0x00011A40
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001385E File Offset: 0x00011A5E
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00013866 File Offset: 0x00011A66
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00013889 File Offset: 0x00011A89
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x00013891 File Offset: 0x00011A91
		[DataSourceProperty]
		public string DescriptionText
		{
			get
			{
				return this._descriptionText;
			}
			set
			{
				if (value != this._descriptionText)
				{
					this._descriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "DescriptionText");
				}
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x000138B4 File Offset: 0x00011AB4
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x000138BC File Offset: 0x00011ABC
		[DataSourceProperty]
		public string GameType
		{
			get
			{
				return this._gameType;
			}
			set
			{
				if (value != this._gameType)
				{
					this._gameType = value;
					base.OnPropertyChangedWithValue<string>(value, "GameType");
				}
			}
		}

		// Token: 0x0400024E RID: 590
		private readonly CultureObject.BoardGameType _game;

		// Token: 0x0400024F RID: 591
		private readonly int _instructionIndex;

		// Token: 0x04000250 RID: 592
		private bool _isEnabled;

		// Token: 0x04000251 RID: 593
		private string _titleText;

		// Token: 0x04000252 RID: 594
		private string _descriptionText;

		// Token: 0x04000253 RID: 595
		private string _gameType;
	}
}
