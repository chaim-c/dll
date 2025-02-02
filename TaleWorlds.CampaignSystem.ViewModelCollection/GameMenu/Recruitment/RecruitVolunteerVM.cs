using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment
{
	// Token: 0x020000A2 RID: 162
	public class RecruitVolunteerVM : ViewModel
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0003E86B File Offset: 0x0003CA6B
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x0003E873 File Offset: 0x0003CA73
		public Hero OwnerHero { get; private set; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x0003E87C File Offset: 0x0003CA7C
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x0003E884 File Offset: 0x0003CA84
		public List<CharacterObject> VolunteerTroops { get; private set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x0003E88D File Offset: 0x0003CA8D
		public int GoldCost { get; }

		// Token: 0x06001012 RID: 4114 RVA: 0x0003E898 File Offset: 0x0003CA98
		public RecruitVolunteerVM(Hero owner, List<CharacterObject> troops, Action<RecruitVolunteerVM, RecruitVolunteerTroopVM> onRecruit, Action<RecruitVolunteerVM, RecruitVolunteerTroopVM> onRemoveFromCart)
		{
			this.OwnerHero = owner;
			this.VolunteerTroops = troops;
			this._onRecruit = onRecruit;
			this._onRemoveFromCart = onRemoveFromCart;
			this.Owner = new RecruitVolunteerOwnerVM(owner, (int)owner.GetRelationWithPlayer());
			this.Troops = new MBBindingList<RecruitVolunteerTroopVM>();
			int num = 0;
			foreach (CharacterObject characterObject in troops)
			{
				RecruitVolunteerTroopVM recruitVolunteerTroopVM = new RecruitVolunteerTroopVM(this, characterObject, num, new Action<RecruitVolunteerTroopVM>(this.ExecuteRecruit), new Action<RecruitVolunteerTroopVM>(this.ExecuteRemoveFromCart));
				recruitVolunteerTroopVM.CanBeRecruited = false;
				recruitVolunteerTroopVM.PlayerHasEnoughRelation = false;
				if (HeroHelper.HeroCanRecruitFromHero(Hero.MainHero, this.OwnerHero, num))
				{
					recruitVolunteerTroopVM.PlayerHasEnoughRelation = true;
					if (characterObject != null)
					{
						recruitVolunteerTroopVM.CanBeRecruited = true;
					}
				}
				num++;
				this.Troops.Add(recruitVolunteerTroopVM);
			}
			this.RecruitHint = new HintViewModel();
			this.RefreshProperties();
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0003E998 File Offset: 0x0003CB98
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.RefreshProperties();
			RecruitVolunteerOwnerVM owner = this.Owner;
			if (owner != null)
			{
				owner.RefreshValues();
			}
			this.Troops.ApplyActionOnAllItems(delegate(RecruitVolunteerTroopVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0003E9EC File Offset: 0x0003CBEC
		public void ExecuteRecruit(RecruitVolunteerTroopVM troop)
		{
			this._onRecruit(this, troop);
			this.RefreshProperties();
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0003EA01 File Offset: 0x0003CC01
		public void ExecuteRemoveFromCart(RecruitVolunteerTroopVM troop)
		{
			this._onRemoveFromCart(this, troop);
			this.RefreshProperties();
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0003EA18 File Offset: 0x0003CC18
		private void RefreshProperties()
		{
			this.RecruitText = this.GoldCost.ToString();
			if (this.RecruitableNumber == 0)
			{
				this.QuantityText = GameTexts.FindText("str_none", null).ToString();
				return;
			}
			GameTexts.SetVariable("QUANTITY", this.RecruitableNumber.ToString());
			this.QuantityText = GameTexts.FindText("str_x_quantity", null).ToString();
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0003EA84 File Offset: 0x0003CC84
		public void OnRecruitMoveToCart(RecruitVolunteerTroopVM troop)
		{
			MBInformationManager.HideInformations();
			this.Troops.RemoveAt(troop.Index);
			RecruitVolunteerTroopVM recruitVolunteerTroopVM = new RecruitVolunteerTroopVM(this, null, troop.Index, new Action<RecruitVolunteerTroopVM>(this.ExecuteRecruit), new Action<RecruitVolunteerTroopVM>(this.ExecuteRemoveFromCart));
			recruitVolunteerTroopVM.IsTroopEmpty = true;
			recruitVolunteerTroopVM.PlayerHasEnoughRelation = true;
			this.Troops.Insert(troop.Index, recruitVolunteerTroopVM);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0003EAED File Offset: 0x0003CCED
		public void OnRecruitRemovedFromCart(RecruitVolunteerTroopVM troop)
		{
			this.Troops.RemoveAt(troop.Index);
			this.Troops.Insert(troop.Index, troop);
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0003EB12 File Offset: 0x0003CD12
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x0003EB1A File Offset: 0x0003CD1A
		[DataSourceProperty]
		public MBBindingList<RecruitVolunteerTroopVM> Troops
		{
			get
			{
				return this._troops;
			}
			set
			{
				if (value != this._troops)
				{
					this._troops = value;
					base.OnPropertyChangedWithValue<MBBindingList<RecruitVolunteerTroopVM>>(value, "Troops");
				}
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x0003EB38 File Offset: 0x0003CD38
		// (set) Token: 0x0600101C RID: 4124 RVA: 0x0003EB40 File Offset: 0x0003CD40
		[DataSourceProperty]
		public RecruitVolunteerOwnerVM Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				if (value != this._owner)
				{
					this._owner = value;
					base.OnPropertyChangedWithValue<RecruitVolunteerOwnerVM>(value, "Owner");
				}
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x0003EB5E File Offset: 0x0003CD5E
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x0003EB66 File Offset: 0x0003CD66
		[DataSourceProperty]
		public bool CanRecruit
		{
			get
			{
				return this._canRecruit;
			}
			set
			{
				if (value != this._canRecruit)
				{
					this._canRecruit = value;
					base.OnPropertyChangedWithValue(value, "CanRecruit");
				}
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x0003EB84 File Offset: 0x0003CD84
		// (set) Token: 0x06001020 RID: 4128 RVA: 0x0003EB8C File Offset: 0x0003CD8C
		[DataSourceProperty]
		public bool ButtonIsVisible
		{
			get
			{
				return this._buttonIsVisible;
			}
			set
			{
				if (value != this._buttonIsVisible)
				{
					this._buttonIsVisible = value;
					base.OnPropertyChangedWithValue(value, "ButtonIsVisible");
				}
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0003EBAA File Offset: 0x0003CDAA
		// (set) Token: 0x06001022 RID: 4130 RVA: 0x0003EBB2 File Offset: 0x0003CDB2
		[DataSourceProperty]
		public string QuantityText
		{
			get
			{
				return this._quantityText;
			}
			set
			{
				if (value != this._quantityText)
				{
					this._quantityText = value;
					base.OnPropertyChangedWithValue<string>(value, "QuantityText");
				}
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0003EBD5 File Offset: 0x0003CDD5
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x0003EBDD File Offset: 0x0003CDDD
		[DataSourceProperty]
		public string RecruitText
		{
			get
			{
				return this._recruitText;
			}
			set
			{
				if (value != this._recruitText)
				{
					this._recruitText = value;
					base.OnPropertyChangedWithValue<string>(value, "RecruitText");
				}
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0003EC00 File Offset: 0x0003CE00
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x0003EC08 File Offset: 0x0003CE08
		[DataSourceProperty]
		public HintViewModel RecruitHint
		{
			get
			{
				return this._recruitHint;
			}
			set
			{
				if (value != this._recruitHint)
				{
					this._recruitHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RecruitHint");
				}
			}
		}

		// Token: 0x04000776 RID: 1910
		public int RecruitableNumber;

		// Token: 0x04000777 RID: 1911
		private readonly Action<RecruitVolunteerVM, RecruitVolunteerTroopVM> _onRecruit;

		// Token: 0x04000778 RID: 1912
		private readonly Action<RecruitVolunteerVM, RecruitVolunteerTroopVM> _onRemoveFromCart;

		// Token: 0x04000779 RID: 1913
		private string _quantityText;

		// Token: 0x0400077A RID: 1914
		private string _recruitText;

		// Token: 0x0400077B RID: 1915
		private bool _canRecruit;

		// Token: 0x0400077C RID: 1916
		private bool _buttonIsVisible;

		// Token: 0x0400077D RID: 1917
		private HintViewModel _recruitHint;

		// Token: 0x0400077E RID: 1918
		private RecruitVolunteerOwnerVM _owner;

		// Token: 0x0400077F RID: 1919
		private MBBindingList<RecruitVolunteerTroopVM> _troops;
	}
}
