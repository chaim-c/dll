using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Inquiries
{
	// Token: 0x0200003B RID: 59
	public class TextQueryPopUpVM : PopUpBaseVM
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x0001683A File Offset: 0x00014A3A
		public TextQueryPopUpVM(Action closeQuery) : base(closeQuery)
		{
			this.DoneButtonDisabledReasonHint = new HintViewModel();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00016850 File Offset: 0x00014A50
		public void SetData(TextInquiryData data)
		{
			this._data = data;
			base.TitleText = this._data.TitleText;
			base.PopUpLabel = this._data.Text;
			base.ButtonOkLabel = this._data.AffirmativeText;
			base.ButtonCancelLabel = this._data.NegativeText;
			base.IsButtonOkShown = this._data.IsAffirmativeOptionShown;
			base.IsButtonCancelShown = this._data.IsNegativeOptionShown;
			this.IsInputObfuscated = this._data.IsInputObfuscated;
			this.InputText = this._data.DefaultInputText;
			Func<string, Tuple<bool, string>> textCondition = this._data.TextCondition;
			base.IsButtonOkEnabled = (textCondition == null || textCondition(this.InputText).Item1);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00016914 File Offset: 0x00014B14
		public override void ExecuteAffirmativeAction()
		{
			Action<string> affirmativeAction = this._data.AffirmativeAction;
			if (affirmativeAction != null)
			{
				affirmativeAction(this.InputText);
			}
			base.CloseQuery();
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00016938 File Offset: 0x00014B38
		public override void ExecuteNegativeAction()
		{
			Action negativeAction = this._data.NegativeAction;
			if (negativeAction != null)
			{
				negativeAction();
			}
			base.CloseQuery();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00016956 File Offset: 0x00014B56
		public override void OnClearData()
		{
			base.OnClearData();
			this._data = null;
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x000169FC File Offset: 0x00014BFC
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00016968 File Offset: 0x00014B68
		[DataSourceProperty]
		public string InputText
		{
			get
			{
				return this._inputText;
			}
			set
			{
				if (value != this._inputText)
				{
					this._inputText = value;
					base.OnPropertyChangedWithValue<string>(value, "InputText");
					Func<string, Tuple<bool, string>> textCondition = this._data.TextCondition;
					Tuple<bool, string> tuple = (textCondition != null) ? textCondition(value) : null;
					base.IsButtonOkEnabled = (tuple == null || tuple.Item1);
					this.DoneButtonDisabledReasonHint.HintText = (string.IsNullOrEmpty((tuple != null) ? tuple.Item2 : null) ? TextObject.Empty : new TextObject("{=!}" + tuple.Item2, null));
				}
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00016A04 File Offset: 0x00014C04
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00016A0C File Offset: 0x00014C0C
		public bool IsInputObfuscated
		{
			get
			{
				return this._isInputObfuscated;
			}
			set
			{
				if (value != this._isInputObfuscated)
				{
					this._isInputObfuscated = value;
					base.OnPropertyChangedWithValue(value, "IsInputObfuscated");
				}
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00016A2A File Offset: 0x00014C2A
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x00016A32 File Offset: 0x00014C32
		[DataSourceProperty]
		public HintViewModel DoneButtonDisabledReasonHint
		{
			get
			{
				return this._doneButtonDisabledReasonHint;
			}
			set
			{
				if (value != this._doneButtonDisabledReasonHint)
				{
					this._doneButtonDisabledReasonHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DoneButtonDisabledReasonHint");
				}
			}
		}

		// Token: 0x04000283 RID: 643
		private TextInquiryData _data;

		// Token: 0x04000284 RID: 644
		[DataSourceProperty]
		private string _inputText;

		// Token: 0x04000285 RID: 645
		private bool _isInputObfuscated;

		// Token: 0x04000286 RID: 646
		private HintViewModel _doneButtonDisabledReasonHint;
	}
}
