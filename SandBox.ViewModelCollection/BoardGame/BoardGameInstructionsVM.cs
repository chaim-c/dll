using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.BoardGame
{
	// Token: 0x0200003E RID: 62
	public class BoardGameInstructionsVM : ViewModel
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x000133A8 File Offset: 0x000115A8
		public BoardGameInstructionsVM(CultureObject.BoardGameType boardGameType)
		{
			this._boardGameType = boardGameType;
			this.InstructionList = new MBBindingList<BoardGameInstructionVM>();
			for (int i = 0; i < this.GetNumberOfInstructions(this._boardGameType); i++)
			{
				this.InstructionList.Add(new BoardGameInstructionVM(this._boardGameType, i));
			}
			this._currentInstructionIndex = 0;
			if (this.InstructionList.Count > 0)
			{
				this.InstructionList[0].IsEnabled = true;
			}
			this.RefreshValues();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00013428 File Offset: 0x00011628
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.InstructionsText = GameTexts.FindText("str_how_to_play", null).ToString();
			this.PreviousText = GameTexts.FindText("str_previous", null).ToString();
			this.NextText = GameTexts.FindText("str_next", null).ToString();
			this.InstructionList.ApplyActionOnAllItems(delegate(BoardGameInstructionVM x)
			{
				x.RefreshValues();
			});
			if (this._currentInstructionIndex >= 0 && this._currentInstructionIndex < this.InstructionList.Count)
			{
				TextObject textObject = new TextObject("{=hUSmlhNh}{CURRENT_PAGE}/{TOTAL_PAGES}", null);
				textObject.SetTextVariable("CURRENT_PAGE", (this._currentInstructionIndex + 1).ToString());
				textObject.SetTextVariable("TOTAL_PAGES", this.InstructionList.Count.ToString());
				this.CurrentPageText = textObject.ToString();
				this.IsPreviousButtonEnabled = (this._currentInstructionIndex != 0);
				this.IsNextButtonEnabled = (this._currentInstructionIndex < this.InstructionList.Count - 1);
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00013544 File Offset: 0x00011744
		public void ExecuteShowPrevious()
		{
			if (this._currentInstructionIndex > 0 && this._currentInstructionIndex < this.InstructionList.Count)
			{
				this.InstructionList[this._currentInstructionIndex].IsEnabled = false;
				this._currentInstructionIndex--;
				this.InstructionList[this._currentInstructionIndex].IsEnabled = true;
				this.RefreshValues();
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000135B0 File Offset: 0x000117B0
		public void ExecuteShowNext()
		{
			if (this._currentInstructionIndex >= 0 && this._currentInstructionIndex < this.InstructionList.Count - 1)
			{
				this.InstructionList[this._currentInstructionIndex].IsEnabled = false;
				this._currentInstructionIndex++;
				this.InstructionList[this._currentInstructionIndex].IsEnabled = true;
				this.RefreshValues();
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001361D File Offset: 0x0001181D
		private int GetNumberOfInstructions(CultureObject.BoardGameType game)
		{
			switch (game)
			{
			case CultureObject.BoardGameType.Seega:
				return 4;
			case CultureObject.BoardGameType.Puluc:
				return 5;
			case CultureObject.BoardGameType.Konane:
				return 3;
			case CultureObject.BoardGameType.MuTorere:
				return 2;
			case CultureObject.BoardGameType.Tablut:
				return 4;
			case CultureObject.BoardGameType.BaghChal:
				return 4;
			default:
				return 0;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001364C File Offset: 0x0001184C
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00013654 File Offset: 0x00011854
		[DataSourceProperty]
		public bool IsPreviousButtonEnabled
		{
			get
			{
				return this._isPreviousButtonEnabled;
			}
			set
			{
				if (value != this._isPreviousButtonEnabled)
				{
					this._isPreviousButtonEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsPreviousButtonEnabled");
				}
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00013672 File Offset: 0x00011872
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0001367A File Offset: 0x0001187A
		[DataSourceProperty]
		public bool IsNextButtonEnabled
		{
			get
			{
				return this._isNextButtonEnabled;
			}
			set
			{
				if (value != this._isNextButtonEnabled)
				{
					this._isNextButtonEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsNextButtonEnabled");
				}
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00013698 File Offset: 0x00011898
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x000136A0 File Offset: 0x000118A0
		[DataSourceProperty]
		public string InstructionsText
		{
			get
			{
				return this._instructionsText;
			}
			set
			{
				if (value != this._instructionsText)
				{
					this._instructionsText = value;
					base.OnPropertyChangedWithValue<string>(value, "InstructionsText");
				}
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000136C3 File Offset: 0x000118C3
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x000136CB File Offset: 0x000118CB
		[DataSourceProperty]
		public string PreviousText
		{
			get
			{
				return this._previousText;
			}
			set
			{
				if (value != this._previousText)
				{
					this._previousText = value;
					base.OnPropertyChangedWithValue<string>(value, "PreviousText");
				}
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000136EE File Offset: 0x000118EE
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x000136F6 File Offset: 0x000118F6
		[DataSourceProperty]
		public string NextText
		{
			get
			{
				return this._nextText;
			}
			set
			{
				if (value != this._nextText)
				{
					this._nextText = value;
					base.OnPropertyChangedWithValue<string>(value, "NextText");
				}
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00013719 File Offset: 0x00011919
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00013721 File Offset: 0x00011921
		[DataSourceProperty]
		public string CurrentPageText
		{
			get
			{
				return this._currentPageText;
			}
			set
			{
				if (value != this._currentPageText)
				{
					this._currentPageText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentPageText");
				}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00013744 File Offset: 0x00011944
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0001374C File Offset: 0x0001194C
		[DataSourceProperty]
		public MBBindingList<BoardGameInstructionVM> InstructionList
		{
			get
			{
				return this._instructionList;
			}
			set
			{
				if (value != this._instructionList)
				{
					this._instructionList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BoardGameInstructionVM>>(value, "InstructionList");
				}
			}
		}

		// Token: 0x04000245 RID: 581
		private readonly CultureObject.BoardGameType _boardGameType;

		// Token: 0x04000246 RID: 582
		private int _currentInstructionIndex;

		// Token: 0x04000247 RID: 583
		private bool _isPreviousButtonEnabled;

		// Token: 0x04000248 RID: 584
		private bool _isNextButtonEnabled;

		// Token: 0x04000249 RID: 585
		private string _instructionsText;

		// Token: 0x0400024A RID: 586
		private string _previousText;

		// Token: 0x0400024B RID: 587
		private string _nextText;

		// Token: 0x0400024C RID: 588
		private string _currentPageText;

		// Token: 0x0400024D RID: 589
		private MBBindingList<BoardGameInstructionVM> _instructionList;
	}
}
