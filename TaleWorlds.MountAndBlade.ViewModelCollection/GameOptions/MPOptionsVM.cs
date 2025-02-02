using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x02000060 RID: 96
	public class MPOptionsVM : OptionsVM
	{
		// Token: 0x06000781 RID: 1921 RVA: 0x0001C8DB File Offset: 0x0001AADB
		public MPOptionsVM(bool autoHandleClose, Action onChangeBrightnessRequest, Action onChangeExposureRequest, Action<KeyOptionVM> onKeybindRequest) : base(autoHandleClose, OptionsVM.OptionsMode.Multiplayer, onKeybindRequest, onChangeBrightnessRequest, onChangeExposureRequest)
		{
			this.RefreshValues();
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001C911 File Offset: 0x0001AB11
		public MPOptionsVM(Action onClose, Action<KeyOptionVM> onKeybindRequest) : base(OptionsVM.OptionsMode.Multiplayer, onClose, onKeybindRequest, null, null)
		{
			this.RefreshValues();
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001C946 File Offset: 0x0001AB46
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ApplyText = new TextObject("{=BAaS5Dkc}Apply", null).ToString();
			this.RevertText = new TextObject("{=Npqlj5Ln}Revert Changes", null).ToString();
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001C97A File Offset: 0x0001AB7A
		public new void ExecuteCancel()
		{
			base.ExecuteCancel();
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001C982 File Offset: 0x0001AB82
		public void ExecuteApply()
		{
			bool flag = base.IsOptionsChanged();
			base.OnDone();
			InformationManager.DisplayMessage(new InformationMessage(flag ? this._changesAppliedTextObject.ToString() : this._noChangesMadeTextObject.ToString()));
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001C9B4 File Offset: 0x0001ABB4
		public void ForceCancel()
		{
			base.HandleCancel(false);
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x0001C9BD File Offset: 0x0001ABBD
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x0001C9C5 File Offset: 0x0001ABC5
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

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001C9E3 File Offset: 0x0001ABE3
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x0001C9EB File Offset: 0x0001ABEB
		[DataSourceProperty]
		public string ApplyText
		{
			get
			{
				return this._applyText;
			}
			set
			{
				if (value != this._applyText)
				{
					this._applyText = value;
					base.OnPropertyChangedWithValue<string>(value, "ApplyText");
				}
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001CA0E File Offset: 0x0001AC0E
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x0001CA16 File Offset: 0x0001AC16
		[DataSourceProperty]
		public string RevertText
		{
			get
			{
				return this._revertText;
			}
			set
			{
				if (value != this._revertText)
				{
					this._revertText = value;
					base.OnPropertyChangedWithValue<string>(value, "RevertText");
				}
			}
		}

		// Token: 0x04000386 RID: 902
		private TextObject _changesAppliedTextObject = new TextObject("{=SfsnlbyK}Changes applied.", null);

		// Token: 0x04000387 RID: 903
		private TextObject _noChangesMadeTextObject = new TextObject("{=jS5rrX8M}There are no changes to apply.", null);

		// Token: 0x04000388 RID: 904
		private bool _isEnabled;

		// Token: 0x04000389 RID: 905
		private string _applyText;

		// Token: 0x0400038A RID: 906
		private string _revertText;
	}
}
