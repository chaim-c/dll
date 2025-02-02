using System;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.SaveLoad
{
	// Token: 0x0200000F RID: 15
	public class SavedGameGroupVM : ViewModel
	{
		// Token: 0x06000136 RID: 310 RVA: 0x000078F4 File Offset: 0x00005AF4
		public SavedGameGroupVM()
		{
			this.SavedGamesList = new MBBindingList<SavedGameVM>();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007907 File Offset: 0x00005B07
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.SavedGamesList.ApplyActionOnAllItems(delegate(SavedGameVM s)
			{
				s.RefreshValues();
			});
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00007939 File Offset: 0x00005B39
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00007941 File Offset: 0x00005B41
		[DataSourceProperty]
		public bool IsFilteredOut
		{
			get
			{
				return this._isFilteredOut;
			}
			set
			{
				if (value != this._isFilteredOut)
				{
					this._isFilteredOut = value;
					base.OnPropertyChangedWithValue(value, "IsFilteredOut");
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000795F File Offset: 0x00005B5F
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00007967 File Offset: 0x00005B67
		[DataSourceProperty]
		public MBBindingList<SavedGameVM> SavedGamesList
		{
			get
			{
				return this._savedGamesList;
			}
			set
			{
				if (value != this._savedGamesList)
				{
					this._savedGamesList = value;
					base.OnPropertyChangedWithValue<MBBindingList<SavedGameVM>>(value, "SavedGamesList");
				}
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00007985 File Offset: 0x00005B85
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000798D File Offset: 0x00005B8D
		[DataSourceProperty]
		public string IdentifierID
		{
			get
			{
				return this._identifierID;
			}
			set
			{
				if (value != this._identifierID)
				{
					this._identifierID = value;
					base.OnPropertyChangedWithValue<string>(value, "IdentifierID");
				}
			}
		}

		// Token: 0x0400007E RID: 126
		private bool _isFilteredOut;

		// Token: 0x0400007F RID: 127
		private MBBindingList<SavedGameVM> _savedGamesList;

		// Token: 0x04000080 RID: 128
		private string _identifierID;
	}
}
