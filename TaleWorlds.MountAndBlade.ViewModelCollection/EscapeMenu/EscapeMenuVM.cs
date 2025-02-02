using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu
{
	// Token: 0x02000070 RID: 112
	public class EscapeMenuVM : ViewModel
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x00024DDC File Offset: 0x00022FDC
		public EscapeMenuVM(IEnumerable<EscapeMenuItemVM> items, TextObject title = null)
		{
			this._titleObj = title;
			this.MenuItems = new MBBindingList<EscapeMenuItemVM>();
			if (items != null)
			{
				foreach (EscapeMenuItemVM item in items)
				{
					this.MenuItems.Add(item);
				}
			}
			this.Tips = new GameTipsVM(true, true);
			this.RefreshValues();
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00024E58 File Offset: 0x00023058
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject titleObj = this._titleObj;
			this.Title = (((titleObj != null) ? titleObj.ToString() : null) ?? "");
			this.MenuItems.ApplyActionOnAllItems(delegate(EscapeMenuItemVM x)
			{
				x.RefreshValues();
			});
			this.Tips.RefreshValues();
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00024EC1 File Offset: 0x000230C1
		public virtual void Tick(float dt)
		{
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00024EC4 File Offset: 0x000230C4
		public void RefreshItems(IEnumerable<EscapeMenuItemVM> items)
		{
			this.MenuItems.Clear();
			foreach (EscapeMenuItemVM item in items)
			{
				this.MenuItems.Add(item);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00024F1C File Offset: 0x0002311C
		// (set) Token: 0x06000967 RID: 2407 RVA: 0x00024F24 File Offset: 0x00023124
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (value != this._title)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x00024F47 File Offset: 0x00023147
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x00024F4F File Offset: 0x0002314F
		[DataSourceProperty]
		public MBBindingList<EscapeMenuItemVM> MenuItems
		{
			get
			{
				return this._menuItems;
			}
			set
			{
				if (value != this._menuItems)
				{
					this._menuItems = value;
					base.OnPropertyChangedWithValue<MBBindingList<EscapeMenuItemVM>>(value, "MenuItems");
				}
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x00024F6D File Offset: 0x0002316D
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x00024F75 File Offset: 0x00023175
		[DataSourceProperty]
		public GameTipsVM Tips
		{
			get
			{
				return this._tips;
			}
			set
			{
				if (value != this._tips)
				{
					this._tips = value;
					base.OnPropertyChangedWithValue<GameTipsVM>(value, "Tips");
				}
			}
		}

		// Token: 0x0400047A RID: 1146
		private readonly TextObject _titleObj;

		// Token: 0x0400047B RID: 1147
		private string _title;

		// Token: 0x0400047C RID: 1148
		private MBBindingList<EscapeMenuItemVM> _menuItems;

		// Token: 0x0400047D RID: 1149
		private GameTipsVM _tips;
	}
}
