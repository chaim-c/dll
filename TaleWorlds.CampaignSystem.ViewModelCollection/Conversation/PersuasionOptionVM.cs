using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Conversation.Persuasion;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Conversation
{
	// Token: 0x020000FB RID: 251
	public class PersuasionOptionVM : ViewModel
	{
		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x00058303 File Offset: 0x00056503
		private ConversationSentenceOption _option
		{
			get
			{
				return this._manager.CurOptions[this._index];
			}
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0005831C File Offset: 0x0005651C
		public PersuasionOptionVM(ConversationManager manager, int index, Action onReadyToContinue)
		{
			this._index = index;
			this._manager = manager;
			this._onReadyToContinue = onReadyToContinue;
			if (ConversationManager.GetPersuasionIsActive() && this._option.HasPersuasion)
			{
				float num;
				float num2;
				float num3;
				float num4;
				this._manager.GetPersuasionChances(this._option, out num, out num2, out num3, out num4);
				this.CritFailChance = (int)(num3 * 100f);
				this.FailChance = (int)(num4 * 100f);
				this.SuccessChance = (int)(num * 100f);
				this.CritSuccessChance = (int)(num2 * 100f);
				this._args = this._option.PersuationOptionArgs;
			}
			this.RefreshValues();
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000583C8 File Offset: 0x000565C8
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (ConversationManager.GetPersuasionIsActive() && this._option.HasPersuasion)
			{
				GameTexts.SetVariable("NUMBER", this.CritFailChance);
				this.CritFailChanceText = GameTexts.FindText("str_NUMBER_percent", null).ToString();
				GameTexts.SetVariable("NUMBER", this.FailChance);
				this.FailChanceText = GameTexts.FindText("str_NUMBER_percent", null).ToString();
				GameTexts.SetVariable("NUMBER", this.SuccessChance);
				this.SuccessChanceText = GameTexts.FindText("str_NUMBER_percent", null).ToString();
				GameTexts.SetVariable("NUMBER", this.CritSuccessChance);
				this.CritSuccessChanceText = GameTexts.FindText("str_NUMBER_percent", null).ToString();
				this.CritFailHint = new BasicTooltipViewModel(delegate()
				{
					GameTexts.SetVariable("LEFT", GameTexts.FindText("str_persuasion_critical_fail", null));
					GameTexts.SetVariable("NUMBER", this.CritFailChance);
					GameTexts.SetVariable("RIGHT", GameTexts.FindText("str_NUMBER_percent", null));
					return GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				});
				this.FailHint = new BasicTooltipViewModel(delegate()
				{
					GameTexts.SetVariable("LEFT", GameTexts.FindText("str_persuasion_fail", null));
					GameTexts.SetVariable("NUMBER", this.FailChance);
					GameTexts.SetVariable("RIGHT", GameTexts.FindText("str_NUMBER_percent", null));
					return GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				});
				this.SuccessHint = new BasicTooltipViewModel(delegate()
				{
					GameTexts.SetVariable("LEFT", GameTexts.FindText("str_persuasion_success", null));
					GameTexts.SetVariable("NUMBER", this.SuccessChance);
					GameTexts.SetVariable("RIGHT", GameTexts.FindText("str_NUMBER_percent", null));
					return GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				});
				this.CritSuccessHint = new BasicTooltipViewModel(delegate()
				{
					GameTexts.SetVariable("LEFT", GameTexts.FindText("str_persuasion_critical_success", null));
					GameTexts.SetVariable("NUMBER", this.CritSuccessChance);
					GameTexts.SetVariable("RIGHT", GameTexts.FindText("str_NUMBER_percent", null));
					return GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				});
				this.ProgressingOptionHint = new HintViewModel(GameTexts.FindText("str_persuasion_progressing_hint", null), null);
				this.BlockingOptionHint = new HintViewModel(GameTexts.FindText("str_persuasion_blocking_hint", null), null);
				this.IsABlockingOption = this._args.CanBlockOtherOption;
				this.IsAProgressingOption = this._args.CanMoveToTheNextReservation;
			}
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00058539 File Offset: 0x00056739
		internal void OnPersuasionProgress(Tuple<PersuasionOptionArgs, PersuasionOptionResult> result)
		{
			this.IsPersuasionResultReady = true;
			if (result.Item1 == this._args)
			{
				this.PersuasionResultIndex = (int)result.Item2;
			}
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0005855C File Offset: 0x0005675C
		public string GetPersuasionAdditionalText()
		{
			string text = null;
			if (this._args != null)
			{
				if (this._args.SkillUsed != null)
				{
					text = ((Hero.MainHero.GetSkillValue(this._args.SkillUsed) <= 50) ? "<a style=\"Conversation.Persuasion.Neutral\"><b>{TEXT}</b></a>" : "<a style=\"Conversation.Persuasion.Positive\"><b>{TEXT}</b></a>").Replace("{TEXT}", this._args.SkillUsed.Name.ToString());
				}
				if (this._args.TraitUsed != null && !this._args.TraitUsed.IsHidden)
				{
					int traitLevel = Hero.MainHero.GetTraitLevel(this._args.TraitUsed);
					string text2;
					if (traitLevel == 0)
					{
						text2 = "<a style=\"Conversation.Persuasion.Neutral\"><b>{TEXT}</b></a>";
					}
					else
					{
						text2 = (((traitLevel > 0 && this._args.TraitEffect == TraitEffect.Positive) || (traitLevel < 0 && this._args.TraitEffect == TraitEffect.Negative)) ? "<a style=\"Conversation.Persuasion.Positive\"><b>{TEXT}</b></a>" : "<a style=\"Conversation.Persuasion.Negative\"><b>{TEXT}</b></a>");
					}
					text2 = text2.Replace("{TEXT}", this._args.TraitUsed.Name.ToString());
					if (text != null)
					{
						GameTexts.SetVariable("LEFT", text);
						GameTexts.SetVariable("RIGHT", text2);
						text = GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString();
					}
					else
					{
						text = text2;
					}
				}
				if (this._args.TraitCorrelation != null)
				{
					foreach (Tuple<TraitObject, int> tuple in this._args.TraitCorrelation)
					{
						if (tuple.Item2 != 0 && this._args.TraitUsed != tuple.Item1 && !tuple.Item1.IsHidden)
						{
							int traitLevel2 = Hero.MainHero.GetTraitLevel(tuple.Item1);
							string text3;
							if (traitLevel2 == 0)
							{
								text3 = "<a style=\"Conversation.Persuasion.Neutral\"><b>{TEXT}</b></a>";
							}
							else
							{
								text3 = ((traitLevel2 * tuple.Item2 > 0) ? "<a style=\"Conversation.Persuasion.Positive\"><b>{TEXT}</b></a>" : "<a style=\"Conversation.Persuasion.Negative\"><b>{TEXT}</b></a>");
							}
							text3 = text3.Replace("{TEXT}", tuple.Item1.Name.ToString());
							if (text != null)
							{
								GameTexts.SetVariable("LEFT", text);
								GameTexts.SetVariable("RIGHT", text3);
								text = GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString();
							}
							else
							{
								text = text3;
							}
						}
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				GameTexts.SetVariable("STR", text);
				return GameTexts.FindText("str_STR_in_parentheses", null).ToString();
			}
			return string.Empty;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000587BD File Offset: 0x000569BD
		public void ExecuteReadyToContinue()
		{
			Action onReadyToContinue = this._onReadyToContinue;
			if (onReadyToContinue == null)
			{
				return;
			}
			onReadyToContinue.DynamicInvokeWithLog(Array.Empty<object>());
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x000587D5 File Offset: 0x000569D5
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x000587DD File Offset: 0x000569DD
		[DataSourceProperty]
		public bool IsPersuasionResultReady
		{
			get
			{
				return this._isPersuasionResultReady;
			}
			set
			{
				if (this._isPersuasionResultReady != value)
				{
					this._isPersuasionResultReady = value;
					base.OnPropertyChangedWithValue(value, "IsPersuasionResultReady");
				}
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000587FB File Offset: 0x000569FB
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x00058803 File Offset: 0x00056A03
		[DataSourceProperty]
		public bool IsABlockingOption
		{
			get
			{
				return this._isABlockingOption;
			}
			set
			{
				if (this._isABlockingOption != value)
				{
					this._isABlockingOption = value;
					base.OnPropertyChangedWithValue(value, "IsABlockingOption");
				}
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x00058821 File Offset: 0x00056A21
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x00058829 File Offset: 0x00056A29
		[DataSourceProperty]
		public bool IsAProgressingOption
		{
			get
			{
				return this._isAProgressingOption;
			}
			set
			{
				if (this._isAProgressingOption != value)
				{
					this._isAProgressingOption = value;
					base.OnPropertyChangedWithValue(value, "IsAProgressingOption");
				}
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x00058847 File Offset: 0x00056A47
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x0005884F File Offset: 0x00056A4F
		[DataSourceProperty]
		public int SuccessChance
		{
			get
			{
				return this._successChance;
			}
			set
			{
				if (this._successChance != value)
				{
					this._successChance = value;
					base.OnPropertyChangedWithValue(value, "SuccessChance");
				}
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0005886D File Offset: 0x00056A6D
		// (set) Token: 0x060017DF RID: 6111 RVA: 0x00058875 File Offset: 0x00056A75
		[DataSourceProperty]
		public int PersuasionResultIndex
		{
			get
			{
				return this._persuasionResultIndex;
			}
			set
			{
				if (this._persuasionResultIndex != value)
				{
					this._persuasionResultIndex = value;
					base.OnPropertyChangedWithValue(value, "PersuasionResultIndex");
				}
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x00058893 File Offset: 0x00056A93
		// (set) Token: 0x060017E1 RID: 6113 RVA: 0x0005889B File Offset: 0x00056A9B
		[DataSourceProperty]
		public int FailChance
		{
			get
			{
				return this._failChance;
			}
			set
			{
				if (this._failChance != value)
				{
					this._failChance = value;
					base.OnPropertyChangedWithValue(value, "FailChance");
				}
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x000588B9 File Offset: 0x00056AB9
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x000588C1 File Offset: 0x00056AC1
		[DataSourceProperty]
		public int CritSuccessChance
		{
			get
			{
				return this._critSuccessChance;
			}
			set
			{
				if (this._critSuccessChance != value)
				{
					this._critSuccessChance = value;
					base.OnPropertyChangedWithValue(value, "CritSuccessChance");
				}
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x000588DF File Offset: 0x00056ADF
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x000588E7 File Offset: 0x00056AE7
		[DataSourceProperty]
		public int CritFailChance
		{
			get
			{
				return this._critFailChance;
			}
			set
			{
				if (this._critFailChance != value)
				{
					this._critFailChance = value;
					base.OnPropertyChangedWithValue(value, "CritFailChance");
				}
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00058905 File Offset: 0x00056B05
		// (set) Token: 0x060017E7 RID: 6119 RVA: 0x0005890D File Offset: 0x00056B0D
		[DataSourceProperty]
		public string FailChanceText
		{
			get
			{
				return this._failChanceText;
			}
			set
			{
				if (this._failChanceText != value)
				{
					this._failChanceText = value;
					base.OnPropertyChangedWithValue<string>(value, "FailChanceText");
				}
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x00058930 File Offset: 0x00056B30
		// (set) Token: 0x060017E9 RID: 6121 RVA: 0x00058938 File Offset: 0x00056B38
		[DataSourceProperty]
		public string CritFailChanceText
		{
			get
			{
				return this._critFailChanceText;
			}
			set
			{
				if (this._critFailChanceText != value)
				{
					this._critFailChanceText = value;
					base.OnPropertyChangedWithValue<string>(value, "CritFailChanceText");
				}
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x0005895B File Offset: 0x00056B5B
		// (set) Token: 0x060017EB RID: 6123 RVA: 0x00058963 File Offset: 0x00056B63
		[DataSourceProperty]
		public string SuccessChanceText
		{
			get
			{
				return this._successChanceText;
			}
			set
			{
				if (this._successChanceText != value)
				{
					this._successChanceText = value;
					base.OnPropertyChangedWithValue<string>(value, "SuccessChanceText");
				}
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x00058986 File Offset: 0x00056B86
		// (set) Token: 0x060017ED RID: 6125 RVA: 0x0005898E File Offset: 0x00056B8E
		[DataSourceProperty]
		public string CritSuccessChanceText
		{
			get
			{
				return this._critSuccessChanceText;
			}
			set
			{
				if (this._critSuccessChanceText != value)
				{
					this._critSuccessChanceText = value;
					base.OnPropertyChangedWithValue<string>(value, "CritSuccessChanceText");
				}
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x000589B1 File Offset: 0x00056BB1
		// (set) Token: 0x060017EF RID: 6127 RVA: 0x000589B9 File Offset: 0x00056BB9
		[DataSourceProperty]
		public BasicTooltipViewModel CritFailHint
		{
			get
			{
				return this._critFailHint;
			}
			set
			{
				if (this._critFailHint != value)
				{
					this._critFailHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "CritFailHint");
				}
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x000589D7 File Offset: 0x00056BD7
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x000589DF File Offset: 0x00056BDF
		[DataSourceProperty]
		public BasicTooltipViewModel FailHint
		{
			get
			{
				return this._failHint;
			}
			set
			{
				if (this._failHint != value)
				{
					this._failHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "FailHint");
				}
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x000589FD File Offset: 0x00056BFD
		// (set) Token: 0x060017F3 RID: 6131 RVA: 0x00058A05 File Offset: 0x00056C05
		[DataSourceProperty]
		public BasicTooltipViewModel SuccessHint
		{
			get
			{
				return this._successHint;
			}
			set
			{
				if (this._successHint != value)
				{
					this._successHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "SuccessHint");
				}
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x00058A23 File Offset: 0x00056C23
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x00058A2B File Offset: 0x00056C2B
		[DataSourceProperty]
		public BasicTooltipViewModel CritSuccessHint
		{
			get
			{
				return this._critSuccessHint;
			}
			set
			{
				if (this._critSuccessHint != value)
				{
					this._critSuccessHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "CritSuccessHint");
				}
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x00058A49 File Offset: 0x00056C49
		// (set) Token: 0x060017F7 RID: 6135 RVA: 0x00058A51 File Offset: 0x00056C51
		[DataSourceProperty]
		public HintViewModel BlockingOptionHint
		{
			get
			{
				return this._blockingOptionHint;
			}
			set
			{
				if (this._blockingOptionHint != value)
				{
					this._blockingOptionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "BlockingOptionHint");
				}
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x00058A6F File Offset: 0x00056C6F
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x00058A77 File Offset: 0x00056C77
		[DataSourceProperty]
		public HintViewModel ProgressingOptionHint
		{
			get
			{
				return this._progressingOptionHint;
			}
			set
			{
				if (this._progressingOptionHint != value)
				{
					this._progressingOptionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ProgressingOptionHint");
				}
			}
		}

		// Token: 0x04000B1B RID: 2843
		private const int _minSkillValueForPositive = 50;

		// Token: 0x04000B1C RID: 2844
		private readonly ConversationManager _manager;

		// Token: 0x04000B1D RID: 2845
		private readonly PersuasionOptionArgs _args;

		// Token: 0x04000B1E RID: 2846
		private readonly Action _onReadyToContinue;

		// Token: 0x04000B1F RID: 2847
		private readonly int _index;

		// Token: 0x04000B20 RID: 2848
		private int _critFailChance;

		// Token: 0x04000B21 RID: 2849
		private int _failChance;

		// Token: 0x04000B22 RID: 2850
		private int _successChance;

		// Token: 0x04000B23 RID: 2851
		private int _critSuccessChance;

		// Token: 0x04000B24 RID: 2852
		private bool _isPersuasionResultReady;

		// Token: 0x04000B25 RID: 2853
		private int _persuasionResultIndex = -1;

		// Token: 0x04000B26 RID: 2854
		private bool _isABlockingOption;

		// Token: 0x04000B27 RID: 2855
		private bool _isAProgressingOption;

		// Token: 0x04000B28 RID: 2856
		private string _critFailChanceText;

		// Token: 0x04000B29 RID: 2857
		private string _failChanceText;

		// Token: 0x04000B2A RID: 2858
		private string _successChanceText;

		// Token: 0x04000B2B RID: 2859
		private string _critSuccessChanceText;

		// Token: 0x04000B2C RID: 2860
		private BasicTooltipViewModel _critFailHint;

		// Token: 0x04000B2D RID: 2861
		private BasicTooltipViewModel _failHint;

		// Token: 0x04000B2E RID: 2862
		private BasicTooltipViewModel _successHint;

		// Token: 0x04000B2F RID: 2863
		private BasicTooltipViewModel _critSuccessHint;

		// Token: 0x04000B30 RID: 2864
		private HintViewModel _progressingOptionHint;

		// Token: 0x04000B31 RID: 2865
		private HintViewModel _blockingOptionHint;
	}
}
