using System;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party.PartyTroopManagerPopUp
{
	// Token: 0x02000030 RID: 48
	public class PartyTroopManagerItemVM : ViewModel
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001968A File Offset: 0x0001788A
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x00019692 File Offset: 0x00017892
		public Action<PartyTroopManagerItemVM> SetFocused { get; private set; }

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001969B File Offset: 0x0001789B
		public PartyTroopManagerItemVM(PartyCharacterVM baseTroop, Action<PartyTroopManagerItemVM> setFocused)
		{
			this.PartyCharacter = baseTroop;
			this.SetFocused = setFocused;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000196B1 File Offset: 0x000178B1
		public void ExecuteSetFocused()
		{
			if (this.PartyCharacter.Character != null)
			{
				Action<PartyTroopManagerItemVM> setFocused = this.SetFocused;
				if (setFocused != null)
				{
					setFocused(this);
				}
				this.IsFocused = true;
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000196D9 File Offset: 0x000178D9
		public void ExecuteSetUnfocused()
		{
			Action<PartyTroopManagerItemVM> setFocused = this.SetFocused;
			if (setFocused != null)
			{
				setFocused(null);
			}
			this.IsFocused = false;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000196F4 File Offset: 0x000178F4
		public void ExecuteOpenTroopEncyclopedia()
		{
			this.PartyCharacter.ExecuteOpenTroopEncyclopedia();
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00019701 File Offset: 0x00017901
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x00019709 File Offset: 0x00017909
		[DataSourceProperty]
		public bool IsFocused
		{
			get
			{
				return this._isFocused;
			}
			set
			{
				if (value != this._isFocused)
				{
					this._isFocused = value;
					base.OnPropertyChangedWithValue(value, "IsFocused");
				}
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00019727 File Offset: 0x00017927
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x0001972F File Offset: 0x0001792F
		[DataSourceProperty]
		public PartyCharacterVM PartyCharacter
		{
			get
			{
				return this._partyCharacter;
			}
			set
			{
				if (value != this._partyCharacter)
				{
					this._partyCharacter = value;
					base.OnPropertyChangedWithValue<PartyCharacterVM>(value, "PartyCharacter");
				}
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001974D File Offset: 0x0001794D
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x0001975A File Offset: 0x0001795A
		[DataSourceProperty]
		public bool IsTroopUpgradable
		{
			get
			{
				return this.PartyCharacter.IsTroopUpgradable;
			}
			set
			{
				if (value != this.PartyCharacter.IsTroopUpgradable)
				{
					this.PartyCharacter.IsTroopUpgradable = value;
					base.OnPropertyChangedWithValue(value, "IsTroopUpgradable");
				}
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00019782 File Offset: 0x00017982
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0001978F File Offset: 0x0001798F
		[DataSourceProperty]
		public bool IsTroopRecruitable
		{
			get
			{
				return this.PartyCharacter.IsTroopRecruitable;
			}
			set
			{
				if (value != this.PartyCharacter.IsTroopRecruitable)
				{
					this.PartyCharacter.IsTroopRecruitable = value;
					base.OnPropertyChangedWithValue(value, "IsTroopRecruitable");
				}
			}
		}

		// Token: 0x040001FF RID: 511
		private bool _isFocused;

		// Token: 0x04000200 RID: 512
		private PartyCharacterVM _partyCharacter;
	}
}
