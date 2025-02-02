using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.GameOver
{
	// Token: 0x02000038 RID: 56
	public class GameOverStatCategoryVM : ViewModel
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x00012D51 File Offset: 0x00010F51
		public GameOverStatCategoryVM(StatCategory category, Action<GameOverStatCategoryVM> onSelect)
		{
			this._category = category;
			this._onSelect = onSelect;
			this.Items = new MBBindingList<GameOverStatItemVM>();
			this.ID = category.ID;
			this.RefreshValues();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00012D84 File Offset: 0x00010F84
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Items.Clear();
			this.Name = GameTexts.FindText("str_game_over_stat_category", this._category.ID).ToString();
			foreach (StatItem item in this._category.Items)
			{
				this.Items.Add(new GameOverStatItemVM(item));
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00012E14 File Offset: 0x00011014
		public void ExecuteSelectCategory()
		{
			Action<GameOverStatCategoryVM> onSelect = this._onSelect;
			if (onSelect == null)
			{
				return;
			}
			onSelect.DynamicInvokeWithLog(new object[]
			{
				this
			});
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x00012E31 File Offset: 0x00011031
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x00012E39 File Offset: 0x00011039
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

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00012E5C File Offset: 0x0001105C
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x00012E64 File Offset: 0x00011064
		[DataSourceProperty]
		public string ID
		{
			get
			{
				return this._id;
			}
			set
			{
				if (value != this._id)
				{
					this._id = value;
					base.OnPropertyChangedWithValue<string>(value, "ID");
				}
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x00012E87 File Offset: 0x00011087
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x00012E8F File Offset: 0x0001108F
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00012EAD File Offset: 0x000110AD
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00012EB5 File Offset: 0x000110B5
		[DataSourceProperty]
		public MBBindingList<GameOverStatItemVM> Items
		{
			get
			{
				return this._items;
			}
			set
			{
				if (value != this._items)
				{
					this._items = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameOverStatItemVM>>(value, "Items");
				}
			}
		}

		// Token: 0x04000229 RID: 553
		private readonly StatCategory _category;

		// Token: 0x0400022A RID: 554
		private readonly Action<GameOverStatCategoryVM> _onSelect;

		// Token: 0x0400022B RID: 555
		private string _name;

		// Token: 0x0400022C RID: 556
		private string _id;

		// Token: 0x0400022D RID: 557
		private bool _isSelected;

		// Token: 0x0400022E RID: 558
		private MBBindingList<GameOverStatItemVM> _items;
	}
}
