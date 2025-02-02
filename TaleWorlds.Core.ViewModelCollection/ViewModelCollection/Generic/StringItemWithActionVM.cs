using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Generic
{
	// Token: 0x02000021 RID: 33
	public class StringItemWithActionVM : ViewModel
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x00005702 File Offset: 0x00003902
		public StringItemWithActionVM(Action<object> onExecute, string item, object identifier)
		{
			this._onExecute = onExecute;
			this.Identifier = identifier;
			this.ActionText = item;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000571F File Offset: 0x0000391F
		public void ExecuteAction()
		{
			this._onExecute(this.Identifier);
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005732 File Offset: 0x00003932
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000573A File Offset: 0x0000393A
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

		// Token: 0x040000A3 RID: 163
		public object Identifier;

		// Token: 0x040000A4 RID: 164
		protected Action<object> _onExecute;

		// Token: 0x040000A5 RID: 165
		private string _actionText;
	}
}
