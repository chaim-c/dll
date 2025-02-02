using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Generic
{
	// Token: 0x02000020 RID: 32
	public class BoolItemWithActionVM : ViewModel
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000056AC File Offset: 0x000038AC
		// (set) Token: 0x0600019F RID: 415 RVA: 0x000056B4 File Offset: 0x000038B4
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000056D2 File Offset: 0x000038D2
		public void ExecuteAction()
		{
			this._onExecute(this.Identifier);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000056E5 File Offset: 0x000038E5
		public BoolItemWithActionVM(Action<object> onExecute, bool isActive, object identifier)
		{
			this._onExecute = onExecute;
			this.Identifier = identifier;
			this.IsActive = isActive;
		}

		// Token: 0x040000A0 RID: 160
		public object Identifier;

		// Token: 0x040000A1 RID: 161
		protected Action<object> _onExecute;

		// Token: 0x040000A2 RID: 162
		private bool _isActive;
	}
}
