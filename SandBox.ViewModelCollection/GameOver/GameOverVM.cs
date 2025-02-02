using System;
using SandBox.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.ViewModelCollection.GameOver
{
	// Token: 0x0200003D RID: 61
	public class GameOverVM : ViewModel
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x00013080 File Offset: 0x00011280
		public GameOverVM(GameOverState.GameOverReason reason, Action onClose)
		{
			this._onClose = onClose;
			this._reason = reason;
			this._statsProvider = new GameOverStatsProvider();
			this.Categories = new MBBindingList<GameOverStatCategoryVM>();
			this.IsPositiveGameOver = (this._reason == GameOverState.GameOverReason.Victory);
			this.ClanBanner = new ImageIdentifierVM(BannerCode.CreateFrom(Hero.MainHero.ClanBanner), true);
			this.ReasonAsString = Enum.GetName(typeof(GameOverState.GameOverReason), this._reason);
			this.RefreshValues();
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00013108 File Offset: 0x00011308
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.CloseText = (this.IsPositiveGameOver ? new TextObject("{=DM6luo3c}Continue", null).ToString() : GameTexts.FindText("str_main_menu", null).ToString());
			this.TitleText = GameTexts.FindText("str_game_over_title", this.ReasonAsString).ToString();
			this.StatisticsTitle = GameTexts.FindText("str_statistics", null).ToString();
			this.Categories.Clear();
			foreach (StatCategory category in this._statsProvider.GetGameOverStats())
			{
				this.Categories.Add(new GameOverStatCategoryVM(category, new Action<GameOverStatCategoryVM>(this.OnCategorySelection)));
			}
			this.OnCategorySelection(this.Categories[0]);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000131F4 File Offset: 0x000113F4
		private void OnCategorySelection(GameOverStatCategoryVM newCategory)
		{
			if (this._currentCategory != null)
			{
				this._currentCategory.IsSelected = false;
			}
			this._currentCategory = newCategory;
			if (this._currentCategory != null)
			{
				this._currentCategory.IsSelected = true;
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00013225 File Offset: 0x00011425
		public void ExecuteClose()
		{
			Action onClose = this._onClose;
			if (onClose == null)
			{
				return;
			}
			onClose.DynamicInvokeWithLog(Array.Empty<object>());
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001323D File Offset: 0x0001143D
		public void SetCloseInputKey(HotKey hotKey)
		{
			this.CloseInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001324C File Offset: 0x0001144C
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM closeInputKey = this.CloseInputKey;
			if (closeInputKey == null)
			{
				return;
			}
			closeInputKey.OnFinalize();
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00013264 File Offset: 0x00011464
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0001326C File Offset: 0x0001146C
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

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001328F File Offset: 0x0001148F
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x00013297 File Offset: 0x00011497
		[DataSourceProperty]
		public string StatisticsTitle
		{
			get
			{
				return this._statisticsTitle;
			}
			set
			{
				if (value != this._statisticsTitle)
				{
					this._statisticsTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "StatisticsTitle");
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x000132BA File Offset: 0x000114BA
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x000132C2 File Offset: 0x000114C2
		[DataSourceProperty]
		public string ReasonAsString
		{
			get
			{
				return this._reasonAsString;
			}
			set
			{
				if (value != this._reasonAsString)
				{
					this._reasonAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "ReasonAsString");
				}
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000132E5 File Offset: 0x000114E5
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x000132ED File Offset: 0x000114ED
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

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00013310 File Offset: 0x00011510
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x00013318 File Offset: 0x00011518
		[DataSourceProperty]
		public ImageIdentifierVM ClanBanner
		{
			get
			{
				return this._clanBanner;
			}
			set
			{
				if (value != this._clanBanner)
				{
					this._clanBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ClanBanner");
				}
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00013336 File Offset: 0x00011536
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0001333E File Offset: 0x0001153E
		[DataSourceProperty]
		public bool IsPositiveGameOver
		{
			get
			{
				return this._isPositiveGameOver;
			}
			set
			{
				if (value != this._isPositiveGameOver)
				{
					this._isPositiveGameOver = value;
					base.OnPropertyChangedWithValue(value, "IsPositiveGameOver");
				}
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0001335C File Offset: 0x0001155C
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00013364 File Offset: 0x00011564
		[DataSourceProperty]
		public InputKeyItemVM CloseInputKey
		{
			get
			{
				return this._closeInputKey;
			}
			set
			{
				if (value != this._closeInputKey)
				{
					this._closeInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CloseInputKey");
				}
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00013382 File Offset: 0x00011582
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0001338A File Offset: 0x0001158A
		[DataSourceProperty]
		public MBBindingList<GameOverStatCategoryVM> Categories
		{
			get
			{
				return this._categories;
			}
			set
			{
				if (value != this._categories)
				{
					this._categories = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameOverStatCategoryVM>>(value, "Categories");
				}
			}
		}

		// Token: 0x04000239 RID: 569
		private readonly Action _onClose;

		// Token: 0x0400023A RID: 570
		private readonly GameOverStatsProvider _statsProvider;

		// Token: 0x0400023B RID: 571
		private readonly GameOverState.GameOverReason _reason;

		// Token: 0x0400023C RID: 572
		private GameOverStatCategoryVM _currentCategory;

		// Token: 0x0400023D RID: 573
		private string _closeText;

		// Token: 0x0400023E RID: 574
		private string _titleText;

		// Token: 0x0400023F RID: 575
		private string _reasonAsString;

		// Token: 0x04000240 RID: 576
		private string _statisticsTitle;

		// Token: 0x04000241 RID: 577
		private bool _isPositiveGameOver;

		// Token: 0x04000242 RID: 578
		private InputKeyItemVM _closeInputKey;

		// Token: 0x04000243 RID: 579
		private ImageIdentifierVM _clanBanner;

		// Token: 0x04000244 RID: 580
		private MBBindingList<GameOverStatCategoryVM> _categories;
	}
}
