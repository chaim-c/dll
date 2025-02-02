using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Inquiries
{
	// Token: 0x0200003A RID: 58
	public class SingleQueryPopUpVM : PopUpBaseVM
	{
		// Token: 0x06000521 RID: 1313 RVA: 0x000164B2 File Offset: 0x000146B2
		public SingleQueryPopUpVM(Action closeQuery) : base(closeQuery)
		{
			base.ButtonOkHint = new HintViewModel();
			base.ButtonCancelHint = new HintViewModel();
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000164D4 File Offset: 0x000146D4
		public override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (this._data != null)
			{
				this.UpdateButtonEnabledStates();
				if (this._data.ExpireTime > 0f)
				{
					if (this._queryTimer > this._data.ExpireTime)
					{
						Action timeoutAction = this._data.TimeoutAction;
						if (timeoutAction != null)
						{
							timeoutAction();
						}
						base.CloseQuery();
						return;
					}
					this._queryTimer += dt;
					this.RemainingQueryTime = this._data.ExpireTime - this._queryTimer;
				}
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001655E File Offset: 0x0001475E
		public override void ExecuteAffirmativeAction()
		{
			Action affirmativeAction = this._data.AffirmativeAction;
			if (affirmativeAction != null)
			{
				affirmativeAction();
			}
			base.CloseQuery();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001657C File Offset: 0x0001477C
		public override void ExecuteNegativeAction()
		{
			Action negativeAction = this._data.NegativeAction;
			if (negativeAction != null)
			{
				negativeAction();
			}
			base.CloseQuery();
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001659A File Offset: 0x0001479A
		public override void OnClearData()
		{
			base.OnClearData();
			this._data = null;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000165AC File Offset: 0x000147AC
		private void UpdateButtonEnabledStates()
		{
			if (this._data.GetIsAffirmativeOptionEnabled != null)
			{
				ValueTuple<bool, string> valueTuple = this._data.GetIsAffirmativeOptionEnabled();
				base.IsButtonOkEnabled = valueTuple.Item1;
				if (!string.Equals(this._lastButtonOkHint, valueTuple.Item2, StringComparison.OrdinalIgnoreCase))
				{
					base.ButtonOkHint.HintText = (string.IsNullOrEmpty(valueTuple.Item2) ? TextObject.Empty : new TextObject("{=!}" + valueTuple.Item2, null));
					this._lastButtonOkHint = valueTuple.Item2;
				}
			}
			else
			{
				base.IsButtonOkEnabled = true;
				base.ButtonOkHint.HintText = TextObject.Empty;
				this._lastButtonOkHint = string.Empty;
			}
			if (this._data.GetIsNegativeOptionEnabled != null)
			{
				ValueTuple<bool, string> valueTuple2 = this._data.GetIsNegativeOptionEnabled();
				base.IsButtonCancelEnabled = valueTuple2.Item1;
				if (!string.Equals(this._lastButtonCancelHint, valueTuple2.Item2, StringComparison.OrdinalIgnoreCase))
				{
					base.ButtonCancelHint.HintText = (string.IsNullOrEmpty(valueTuple2.Item2) ? TextObject.Empty : new TextObject("{=!}" + valueTuple2.Item2, null));
					this._lastButtonCancelHint = valueTuple2.Item2;
					return;
				}
			}
			else
			{
				base.IsButtonCancelEnabled = true;
				base.ButtonCancelHint.HintText = TextObject.Empty;
				this._lastButtonCancelHint = string.Empty;
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00016700 File Offset: 0x00014900
		public void SetData(InquiryData data)
		{
			this._data = data;
			base.TitleText = this._data.TitleText;
			base.PopUpLabel = this._data.Text;
			base.ButtonOkLabel = this._data.AffirmativeText;
			base.ButtonCancelLabel = this._data.NegativeText;
			base.IsButtonOkShown = this._data.IsAffirmativeOptionShown;
			base.IsButtonCancelShown = this._data.IsNegativeOptionShown;
			this.IsTimerShown = (this._data.ExpireTime > 0f);
			base.IsButtonOkEnabled = true;
			base.IsButtonCancelEnabled = true;
			this.UpdateButtonEnabledStates();
			this._queryTimer = 0f;
			this.TotalQueryTime = (float)MathF.Round(this._data.ExpireTime);
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x000167C8 File Offset: 0x000149C8
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x000167D0 File Offset: 0x000149D0
		[DataSourceProperty]
		public float RemainingQueryTime
		{
			get
			{
				return this._remainingQueryTime;
			}
			set
			{
				if (value != this._remainingQueryTime)
				{
					this._remainingQueryTime = value;
					base.OnPropertyChangedWithValue(value, "RemainingQueryTime");
				}
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x000167EE File Offset: 0x000149EE
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x000167F6 File Offset: 0x000149F6
		[DataSourceProperty]
		public float TotalQueryTime
		{
			get
			{
				return this._totalQueryTime;
			}
			set
			{
				if (value != this._totalQueryTime)
				{
					this._totalQueryTime = value;
					base.OnPropertyChangedWithValue(value, "TotalQueryTime");
				}
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00016814 File Offset: 0x00014A14
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0001681C File Offset: 0x00014A1C
		[DataSourceProperty]
		public bool IsTimerShown
		{
			get
			{
				return this._isTimerShown;
			}
			set
			{
				if (value != this._isTimerShown)
				{
					this._isTimerShown = value;
					base.OnPropertyChangedWithValue(value, "IsTimerShown");
				}
			}
		}

		// Token: 0x0400027C RID: 636
		private InquiryData _data;

		// Token: 0x0400027D RID: 637
		private float _queryTimer;

		// Token: 0x0400027E RID: 638
		private string _lastButtonOkHint;

		// Token: 0x0400027F RID: 639
		private string _lastButtonCancelHint;

		// Token: 0x04000280 RID: 640
		private float _remainingQueryTime;

		// Token: 0x04000281 RID: 641
		private float _totalQueryTime;

		// Token: 0x04000282 RID: 642
		private bool _isTimerShown;
	}
}
