using System;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Tutorial
{
	// Token: 0x0200000F RID: 15
	public class ElementNotificationVM : ViewModel
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000034C4 File Offset: 0x000016C4
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000034CC File Offset: 0x000016CC
		[DataSourceProperty]
		public string ElementID
		{
			get
			{
				return this._elementID;
			}
			set
			{
				if (value != this._elementID)
				{
					this._elementID = value;
					base.OnPropertyChangedWithValue<string>(value, "ElementID");
				}
			}
		}

		// Token: 0x04000050 RID: 80
		private string _elementID = string.Empty;
	}
}
