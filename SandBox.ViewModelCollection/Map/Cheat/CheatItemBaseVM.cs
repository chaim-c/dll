using System;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Map.Cheat
{
	// Token: 0x02000030 RID: 48
	public abstract class CheatItemBaseVM : ViewModel
	{
		// Token: 0x0600038F RID: 911 RVA: 0x00010F3C File Offset: 0x0000F13C
		public CheatItemBaseVM()
		{
		}

		// Token: 0x06000390 RID: 912
		public abstract void ExecuteAction();

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00010F44 File Offset: 0x0000F144
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00010F4C File Offset: 0x0000F14C
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x040001E0 RID: 480
		private string _name;
	}
}
