using System;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions
{
	// Token: 0x0200005F RID: 95
	public abstract class KeyOptionVM : ViewModel
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001C81B File Offset: 0x0001AA1B
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x0001C823 File Offset: 0x0001AA23
		public Key CurrentKey { get; protected set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001C82C File Offset: 0x0001AA2C
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x0001C834 File Offset: 0x0001AA34
		public Key Key { get; protected set; }

		// Token: 0x06000776 RID: 1910 RVA: 0x0001C83D File Offset: 0x0001AA3D
		public KeyOptionVM(string groupId, string id, Action<KeyOptionVM> onKeybindRequest)
		{
			this._groupId = groupId;
			this._id = id;
			this._onKeybindRequest = onKeybindRequest;
		}

		// Token: 0x06000777 RID: 1911
		public abstract void Set(InputKey newKey);

		// Token: 0x06000778 RID: 1912
		public abstract void Update();

		// Token: 0x06000779 RID: 1913
		public abstract void OnDone();

		// Token: 0x0600077A RID: 1914
		internal abstract bool IsChanged();

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0001C85A File Offset: 0x0001AA5A
		// (set) Token: 0x0600077C RID: 1916 RVA: 0x0001C862 File Offset: 0x0001AA62
		[DataSourceProperty]
		public string OptionValueText
		{
			get
			{
				return this._optionValueText;
			}
			set
			{
				if (value != this._optionValueText)
				{
					this._optionValueText = value;
					base.OnPropertyChangedWithValue<string>(value, "OptionValueText");
				}
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0001C885 File Offset: 0x0001AA85
		// (set) Token: 0x0600077E RID: 1918 RVA: 0x0001C88D File Offset: 0x0001AA8D
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

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001C8B0 File Offset: 0x0001AAB0
		// (set) Token: 0x06000780 RID: 1920 RVA: 0x0001C8B8 File Offset: 0x0001AAB8
		[DataSourceProperty]
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				if (value != this._description)
				{
					this._description = value;
					base.OnPropertyChangedWithValue<string>(value, "Description");
				}
			}
		}

		// Token: 0x04000380 RID: 896
		protected readonly string _groupId;

		// Token: 0x04000381 RID: 897
		protected readonly string _id;

		// Token: 0x04000382 RID: 898
		protected readonly Action<KeyOptionVM> _onKeybindRequest;

		// Token: 0x04000383 RID: 899
		private string _optionValueText;

		// Token: 0x04000384 RID: 900
		private string _name;

		// Token: 0x04000385 RID: 901
		private string _description;
	}
}
