using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.InitialMenu
{
	// Token: 0x0200003D RID: 61
	public class InitialMenuOptionVM : ViewModel
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x00016E58 File Offset: 0x00015058
		public InitialMenuOptionVM(InitialStateOption initialStateOption)
		{
			this.InitialStateOption = initialStateOption;
			this.DisabledHint = new HintViewModel(initialStateOption.IsDisabledAndReason().Item2, null);
			this.EnabledHint = new HintViewModel(initialStateOption.EnabledHint, null);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00016E98 File Offset: 0x00015098
		public void ExecuteAction()
		{
			InitialState initialState = GameStateManager.Current.ActiveState as InitialState;
			if (initialState != null)
			{
				initialState.OnExecutedInitialStateOption(this.InitialStateOption);
				this.InitialStateOption.DoAction();
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00016ECF File Offset: 0x000150CF
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.DisabledHint.HintText = this.InitialStateOption.IsDisabledAndReason().Item2;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00016EF7 File Offset: 0x000150F7
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00016EFF File Offset: 0x000150FF
		[DataSourceProperty]
		public HintViewModel DisabledHint
		{
			get
			{
				return this._disabledHint;
			}
			set
			{
				if (value != this._disabledHint)
				{
					this._disabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DisabledHint");
				}
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00016F1D File Offset: 0x0001511D
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00016F25 File Offset: 0x00015125
		[DataSourceProperty]
		public HintViewModel EnabledHint
		{
			get
			{
				return this._enabledHint;
			}
			set
			{
				if (value != this._enabledHint)
				{
					this._enabledHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EnabledHint");
				}
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00016F43 File Offset: 0x00015143
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this.InitialStateOption.Name.ToString();
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00016F55 File Offset: 0x00015155
		[DataSourceProperty]
		public bool IsDisabled
		{
			get
			{
				return this.InitialStateOption.IsDisabledAndReason().Item1;
			}
		}

		// Token: 0x04000290 RID: 656
		public readonly InitialStateOption InitialStateOption;

		// Token: 0x04000291 RID: 657
		private HintViewModel _disabledHint;

		// Token: 0x04000292 RID: 658
		private HintViewModel _enabledHint;
	}
}
