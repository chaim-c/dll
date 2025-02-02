using System;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000047 RID: 71
	public class ControllerEquippedItemVM : EquipmentActionItemVM
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x00018C40 File Offset: 0x00016E40
		public ControllerEquippedItemVM(string item, string itemTypeAsString, object identifier, HotKey key, Action<EquipmentActionItemVM> onSelection) : base(item, itemTypeAsString, identifier, onSelection, false)
		{
			if (key != null)
			{
				this.ShortcutKey = InputKeyItemVM.CreateFromHotKey(key, true);
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00018C60 File Offset: 0x00016E60
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM shortcutKey = this.ShortcutKey;
			if (shortcutKey != null)
			{
				shortcutKey.OnFinalize();
			}
			this.ShortcutKey = null;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00018C80 File Offset: 0x00016E80
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x00018C88 File Offset: 0x00016E88
		[DataSourceProperty]
		public InputKeyItemVM ShortcutKey
		{
			get
			{
				return this._shortcutKey;
			}
			set
			{
				if (value != this._shortcutKey)
				{
					this._shortcutKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ShortcutKey");
				}
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00018CA6 File Offset: 0x00016EA6
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x00018CAE File Offset: 0x00016EAE
		[DataSourceProperty]
		public float DropProgress
		{
			get
			{
				return this._dropProgress;
			}
			set
			{
				if (value != this._dropProgress)
				{
					this._dropProgress = value;
					base.OnPropertyChangedWithValue(value, "DropProgress");
				}
			}
		}

		// Token: 0x040002DA RID: 730
		private InputKeyItemVM _shortcutKey;

		// Token: 0x040002DB RID: 731
		private float _dropProgress;
	}
}
