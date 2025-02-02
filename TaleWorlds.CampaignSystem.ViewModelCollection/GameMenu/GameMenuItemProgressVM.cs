using System;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu
{
	// Token: 0x0200008A RID: 138
	public class GameMenuItemProgressVM : ViewModel
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x00037770 File Offset: 0x00035970
		public GameMenuItemProgressVM(MenuContext context, int virtualIndex)
		{
			this._context = context;
			this._gameMenuManager = Campaign.Current.GameMenuManager;
			this._virtualIndex = virtualIndex;
			this._text1 = Campaign.Current.GameMenuManager.GetVirtualMenuOptionText(this._context, this._virtualIndex).ToString();
			this._text2 = Campaign.Current.GameMenuManager.GetVirtualMenuOptionText2(this._context, this._virtualIndex).ToString();
			this.RefreshValues();
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00037809 File Offset: 0x00035A09
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Refresh();
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00037818 File Offset: 0x00035A18
		private void Refresh()
		{
			switch (this._gameMenuManager.GetVirtualMenuAndOptionType(this._context))
			{
			case GameMenu.MenuAndOptionType.WaitMenuShowProgressAndHoursOption:
			{
				float virtualMenuTargetWaitHours = Campaign.Current.GameMenuManager.GetVirtualMenuTargetWaitHours(this._context);
				if (virtualMenuTargetWaitHours > 1f)
				{
					GameTexts.SetVariable("PLURAL_HOURS", 1);
				}
				else
				{
					GameTexts.SetVariable("PLURAL_HOURS", 0);
				}
				GameTexts.SetVariable("HOUR", MathF.Round(virtualMenuTargetWaitHours).ToString("0.0"));
				this.ProgressText = GameTexts.FindText("str_hours", null).ToString();
				goto IL_C3;
			}
			case GameMenu.MenuAndOptionType.WaitMenuShowOnlyProgressOption:
				this.ProgressText = "";
				goto IL_C3;
			}
			Debug.FailedAssert("Shouldn't create game menu progress for normal options", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\GameMenuItemProgressVM.cs", "Refresh", 62);
			return;
			IL_C3:
			this.Text = (Campaign.Current.GameMenuManager.GetVirtualMenuIsWaitActive(this._context) ? this._text2 : this._text1);
			float virtualMenuProgress = Campaign.Current.GameMenuManager.GetVirtualMenuProgress(this._context);
			this.Progress = (float)MathF.Round(virtualMenuProgress * 100f);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0003793E File Offset: 0x00035B3E
		public void OnTick()
		{
			this.Refresh();
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x00037946 File Offset: 0x00035B46
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x0003794E File Offset: 0x00035B4E
		[DataSourceProperty]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (value != this._text)
				{
					this._text = value;
					base.OnPropertyChangedWithValue<string>(value, "Text");
				}
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00037971 File Offset: 0x00035B71
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x00037979 File Offset: 0x00035B79
		[DataSourceProperty]
		public string ProgressText
		{
			get
			{
				return this._progressText;
			}
			set
			{
				if (value != this._progressText)
				{
					this._progressText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProgressText");
				}
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0003799C File Offset: 0x00035B9C
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x000379A4 File Offset: 0x00035BA4
		[DataSourceProperty]
		public float Progress
		{
			get
			{
				return this._progress;
			}
			set
			{
				if (value != this._progress)
				{
					this._progress = value;
					base.OnPropertyChangedWithValue(value, "Progress");
				}
			}
		}

		// Token: 0x04000645 RID: 1605
		private readonly MenuContext _context;

		// Token: 0x04000646 RID: 1606
		private readonly GameMenuManager _gameMenuManager;

		// Token: 0x04000647 RID: 1607
		private readonly int _virtualIndex;

		// Token: 0x04000648 RID: 1608
		private string _text1 = "";

		// Token: 0x04000649 RID: 1609
		private string _text2 = "";

		// Token: 0x0400064A RID: 1610
		private string _text;

		// Token: 0x0400064B RID: 1611
		private string _progressText;

		// Token: 0x0400064C RID: 1612
		private float _progress;
	}
}
