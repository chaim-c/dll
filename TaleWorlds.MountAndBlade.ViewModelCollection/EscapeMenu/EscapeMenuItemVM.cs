using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu
{
	// Token: 0x0200006F RID: 111
	public class EscapeMenuItemVM : ViewModel
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x00024C96 File Offset: 0x00022E96
		public EscapeMenuItemVM(TextObject item, Action<object> onExecute, object identifier, Func<Tuple<bool, TextObject>> getIsDisabledAndReason, bool isPositiveBehaviored = false)
		{
			this._onExecute = onExecute;
			this._identifier = identifier;
			this._itemObj = item;
			this.ActionText = this._itemObj.ToString();
			this.IsPositiveBehaviored = isPositiveBehaviored;
			this._getIsDisabledAndReason = getIsDisabledAndReason;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00024CD4 File Offset: 0x00022ED4
		public override void RefreshValues()
		{
			base.RefreshValues();
			Func<Tuple<bool, TextObject>> getIsDisabledAndReason = this._getIsDisabledAndReason;
			Tuple<bool, TextObject> tuple = (getIsDisabledAndReason != null) ? getIsDisabledAndReason() : null;
			this.IsDisabled = tuple.Item1;
			this.DisabledHint = new HintViewModel(tuple.Item2, null);
			this.ActionText = this._itemObj.ToString();
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00024D29 File Offset: 0x00022F29
		public void ExecuteAction()
		{
			this._onExecute(this._identifier);
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x00024D3C File Offset: 0x00022F3C
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x00024D44 File Offset: 0x00022F44
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

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x00024D62 File Offset: 0x00022F62
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x00024D6A File Offset: 0x00022F6A
		[DataSourceProperty]
		public string ActionText
		{
			get
			{
				return this._actionText;
			}
			set
			{
				if (value != this._actionText)
				{
					this._actionText = value;
					base.OnPropertyChangedWithValue<string>(value, "ActionText");
				}
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x0600095E RID: 2398 RVA: 0x00024D8D File Offset: 0x00022F8D
		// (set) Token: 0x0600095F RID: 2399 RVA: 0x00024D95 File Offset: 0x00022F95
		[DataSourceProperty]
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsDisabled");
				}
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00024DB3 File Offset: 0x00022FB3
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x00024DBB File Offset: 0x00022FBB
		[DataSourceProperty]
		public bool IsPositiveBehaviored
		{
			get
			{
				return this._isPositiveBehaviored;
			}
			set
			{
				if (value != this._isPositiveBehaviored)
				{
					this._isPositiveBehaviored = value;
					base.OnPropertyChangedWithValue(value, "IsPositiveBehaviored");
				}
			}
		}

		// Token: 0x04000472 RID: 1138
		private readonly object _identifier;

		// Token: 0x04000473 RID: 1139
		private readonly Action<object> _onExecute;

		// Token: 0x04000474 RID: 1140
		private readonly TextObject _itemObj;

		// Token: 0x04000475 RID: 1141
		private readonly Func<Tuple<bool, TextObject>> _getIsDisabledAndReason;

		// Token: 0x04000476 RID: 1142
		private HintViewModel _disabledHint;

		// Token: 0x04000477 RID: 1143
		private string _actionText;

		// Token: 0x04000478 RID: 1144
		private bool _isDisabled;

		// Token: 0x04000479 RID: 1145
		private bool _isPositiveBehaviored;
	}
}
