using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Nameplate.NameplateNotifications
{
	// Token: 0x0200001D RID: 29
	public class SettlementNotificationItemBaseVM : ViewModel
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000D79C File Offset: 0x0000B99C
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000D7A4 File Offset: 0x0000B9A4
		public int CreatedTick { get; set; }

		// Token: 0x060002BB RID: 699 RVA: 0x0000D7AD File Offset: 0x0000B9AD
		public SettlementNotificationItemBaseVM(Action<SettlementNotificationItemBaseVM> onRemove, int createdTick)
		{
			this._onRemove = onRemove;
			this.RelationType = 0;
			this.CreatedTick = createdTick;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000D7CA File Offset: 0x0000B9CA
		public void ExecuteRemove()
		{
			this._onRemove(this);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		public string CharacterName
		{
			get
			{
				return this._characterName;
			}
			set
			{
				if (value != this._characterName)
				{
					this._characterName = value;
					base.OnPropertyChangedWithValue<string>(value, "CharacterName");
				}
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000D803 File Offset: 0x0000BA03
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000D80B File Offset: 0x0000BA0B
		public int RelationType
		{
			get
			{
				return this._relationType;
			}
			set
			{
				if (value != this._relationType)
				{
					this._relationType = value;
					base.OnPropertyChangedWithValue(value, "RelationType");
				}
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000D829 File Offset: 0x0000BA29
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000D831 File Offset: 0x0000BA31
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				if (value != this._text)
				{
					this._text = value;
					base.OnPropertyChangedWithValue<string>(value, "Text");
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000D854 File Offset: 0x0000BA54
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000D85C File Offset: 0x0000BA5C
		public ImageIdentifierVM CharacterVisual
		{
			get
			{
				return this._characterVisual;
			}
			set
			{
				if (value != this._characterVisual)
				{
					this._characterVisual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "CharacterVisual");
				}
			}
		}

		// Token: 0x04000160 RID: 352
		private readonly Action<SettlementNotificationItemBaseVM> _onRemove;

		// Token: 0x04000162 RID: 354
		private ImageIdentifierVM _characterVisual;

		// Token: 0x04000163 RID: 355
		private string _text;

		// Token: 0x04000164 RID: 356
		private string _characterName;

		// Token: 0x04000165 RID: 357
		private int _relationType;
	}
}
