using System;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x02000059 RID: 89
	public class ActionOptionDataVM : GenericOptionDataVM
	{
		// Token: 0x060006F1 RID: 1777 RVA: 0x0001B6D8 File Offset: 0x000198D8
		public ActionOptionDataVM(Action onAction, OptionsVM optionsVM, IOptionData option, TextObject name, TextObject optionActionName, TextObject description) : base(optionsVM, option, name, description, OptionsVM.OptionsDataType.ActionOption)
		{
			this._onAction = onAction;
			this._optionActionName = optionActionName;
			this.RefreshValues();
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001B6FC File Offset: 0x000198FC
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this._optionActionName != null)
			{
				this.ActionName = this._optionActionName.ToString();
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001B71D File Offset: 0x0001991D
		private void ExecuteAction()
		{
			Action onAction = this._onAction;
			if (onAction == null)
			{
				return;
			}
			onAction.DynamicInvokeWithLog(Array.Empty<object>());
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001B735 File Offset: 0x00019935
		public override void Cancel()
		{
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001B737 File Offset: 0x00019937
		public override bool IsChanged()
		{
			return false;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001B73A File Offset: 0x0001993A
		public override void ResetData()
		{
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001B73C File Offset: 0x0001993C
		public override void SetValue(float value)
		{
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001B73E File Offset: 0x0001993E
		public override void UpdateValue()
		{
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001B740 File Offset: 0x00019940
		public override void ApplyValue()
		{
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001B742 File Offset: 0x00019942
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0001B74A File Offset: 0x0001994A
		[DataSourceProperty]
		public string ActionName
		{
			get
			{
				return this._actionName;
			}
			set
			{
				if (value != this._actionName)
				{
					this._actionName = value;
					base.OnPropertyChangedWithValue<string>(value, "ActionName");
				}
			}
		}

		// Token: 0x0400034D RID: 845
		private readonly Action _onAction;

		// Token: 0x0400034E RID: 846
		private readonly TextObject _optionActionName;

		// Token: 0x0400034F RID: 847
		private string _actionName;
	}
}
