﻿using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party
{
	// Token: 0x02000028 RID: 40
	public class PartyTradeVM : ViewModel
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002FE RID: 766 RVA: 0x00013BB8 File Offset: 0x00011DB8
		// (remove) Token: 0x060002FF RID: 767 RVA: 0x00013BEC File Offset: 0x00011DEC
		public static event Action RemoveZeroCounts;

		// Token: 0x06000300 RID: 768 RVA: 0x00013C20 File Offset: 0x00011E20
		public PartyTradeVM(PartyScreenLogic partyScreenLogic, TroopRosterElement troopRoster, PartyScreenLogic.PartyRosterSide side, bool isTransfarable, bool isPrisoner, Action<int, bool> onApplyTransaction)
		{
			this._partyScreenLogic = partyScreenLogic;
			this._referenceTroopRoster = troopRoster;
			this._side = side;
			this._onApplyTransaction = onApplyTransaction;
			this._otherSide = ((side == PartyScreenLogic.PartyRosterSide.Right) ? PartyScreenLogic.PartyRosterSide.Left : PartyScreenLogic.PartyRosterSide.Right);
			this.IsTransfarable = isTransfarable;
			this._isPrisoner = isPrisoner;
			this.UpdateTroopData(troopRoster, side, true);
			this.RefreshValues();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00013C84 File Offset: 0x00011E84
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ThisStockLbl = GameTexts.FindText("str_party_your_party", null).ToString();
			this.TotalStockLbl = GameTexts.FindText("str_party_total_units", null).ToString();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00013CB8 File Offset: 0x00011EB8
		public void UpdateTroopData(TroopRosterElement troopRoster, PartyScreenLogic.PartyRosterSide side, bool forceUpdate = true)
		{
			if (side != PartyScreenLogic.PartyRosterSide.Left && side != PartyScreenLogic.PartyRosterSide.Right)
			{
				Debug.FailedAssert("Troop has to be either from left or right side", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\Party\\PartyTradeVM.cs", "UpdateTroopData", 50);
				return;
			}
			TroopRosterElement? troopRosterElement = null;
			TroopRosterElement? troopRosterElement2 = null;
			troopRosterElement = new TroopRosterElement?(troopRoster);
			troopRosterElement2 = this.FindTroopFromSide(troopRoster.Character, this._otherSide, this._isPrisoner);
			this.InitialThisStock = ((troopRosterElement != null) ? troopRosterElement.GetValueOrDefault().Number : 0);
			this.InitialOtherStock = ((troopRosterElement2 != null) ? troopRosterElement2.GetValueOrDefault().Number : 0);
			this.TotalStock = this.InitialThisStock + this.InitialOtherStock;
			this.ThisStock = this.InitialThisStock;
			this.OtherStock = this.InitialOtherStock;
			if (forceUpdate)
			{
				this.ThisStockUpdated();
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00013D8C File Offset: 0x00011F8C
		public TroopRosterElement? FindTroopFromSide(CharacterObject character, PartyScreenLogic.PartyRosterSide side, bool isPrisoner)
		{
			TroopRosterElement? result = null;
			TroopRoster[] array = isPrisoner ? this._partyScreenLogic.PrisonerRosters : this._partyScreenLogic.MemberRosters;
			int num = array[(int)side].FindIndexOfTroop(character);
			if (num >= 0)
			{
				result = new TroopRosterElement?(array[(int)side].GetElementCopyAtIndex(num));
			}
			return result;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00013DDC File Offset: 0x00011FDC
		private void ThisStockUpdated()
		{
			this.ExecuteApplyTransaction();
			this.OtherStock = this.TotalStock - this.ThisStock;
			this.IsThisStockIncreasable = (this.OtherStock > 0);
			this.IsOtherStockIncreasable = (this.OtherStock < this.TotalStock && this.IsTransfarable);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00013E2E File Offset: 0x0001202E
		public void ExecuteIncreasePlayerStock()
		{
			if (this.OtherStock > 0)
			{
				this.ThisStock++;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00013E47 File Offset: 0x00012047
		public void ExecuteIncreaseOtherStock()
		{
			if (this.OtherStock < this.TotalStock)
			{
				this.ThisStock--;
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00013E65 File Offset: 0x00012065
		public void ExecuteReset()
		{
			this.OtherStock = this.InitialOtherStock;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00013E74 File Offset: 0x00012074
		public void ExecuteApplyTransaction()
		{
			int num = this.ThisStock - this.InitialThisStock;
			bool arg = (num >= 0 && this._side == PartyScreenLogic.PartyRosterSide.Right) || (num <= 0 && this._side == PartyScreenLogic.PartyRosterSide.Left);
			if (num == 0 || this._onApplyTransaction == null)
			{
				return;
			}
			if (num < 0)
			{
				PartyScreenLogic.PartyRosterSide otherSide = this._otherSide;
			}
			else
			{
				PartyScreenLogic.PartyRosterSide side = this._side;
			}
			int arg2 = MathF.Abs(num);
			this._onApplyTransaction(arg2, arg);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00013EE4 File Offset: 0x000120E4
		public void ExecuteRemoveZeroCounts()
		{
			Action removeZeroCounts = PartyTradeVM.RemoveZeroCounts;
			if (removeZeroCounts == null)
			{
				return;
			}
			removeZeroCounts();
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00013EF5 File Offset: 0x000120F5
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00013EFD File Offset: 0x000120FD
		[DataSourceProperty]
		public bool IsTransfarable
		{
			get
			{
				return this._isTransfarable;
			}
			set
			{
				if (value != this._isTransfarable)
				{
					this._isTransfarable = value;
					base.OnPropertyChangedWithValue(value, "IsTransfarable");
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00013F1B File Offset: 0x0001211B
		// (set) Token: 0x0600030D RID: 781 RVA: 0x00013F23 File Offset: 0x00012123
		[DataSourceProperty]
		public string ThisStockLbl
		{
			get
			{
				return this._thisStockLbl;
			}
			set
			{
				if (value != this._thisStockLbl)
				{
					this._thisStockLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "ThisStockLbl");
				}
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00013F46 File Offset: 0x00012146
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00013F4E File Offset: 0x0001214E
		[DataSourceProperty]
		public string TotalStockLbl
		{
			get
			{
				return this._totalStockLbl;
			}
			set
			{
				if (value != this._totalStockLbl)
				{
					this._totalStockLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalStockLbl");
				}
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00013F71 File Offset: 0x00012171
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00013F79 File Offset: 0x00012179
		[DataSourceProperty]
		public int ThisStock
		{
			get
			{
				return this._thisStock;
			}
			set
			{
				if (value != this._thisStock)
				{
					this._thisStock = value;
					base.OnPropertyChangedWithValue(value, "ThisStock");
					this.ThisStockUpdated();
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00013F9D File Offset: 0x0001219D
		// (set) Token: 0x06000313 RID: 787 RVA: 0x00013FA5 File Offset: 0x000121A5
		[DataSourceProperty]
		public int InitialThisStock
		{
			get
			{
				return this._initialThisStock;
			}
			set
			{
				if (value != this._initialThisStock)
				{
					this._initialThisStock = value;
					base.OnPropertyChangedWithValue(value, "InitialThisStock");
				}
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00013FC3 File Offset: 0x000121C3
		// (set) Token: 0x06000315 RID: 789 RVA: 0x00013FCB File Offset: 0x000121CB
		[DataSourceProperty]
		public int OtherStock
		{
			get
			{
				return this._otherStock;
			}
			set
			{
				if (value != this._otherStock)
				{
					this._otherStock = value;
					base.OnPropertyChangedWithValue(value, "OtherStock");
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00013FE9 File Offset: 0x000121E9
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00013FF1 File Offset: 0x000121F1
		[DataSourceProperty]
		public int InitialOtherStock
		{
			get
			{
				return this._initialOtherStock;
			}
			set
			{
				if (value != this._initialOtherStock)
				{
					this._initialOtherStock = value;
					base.OnPropertyChangedWithValue(value, "InitialOtherStock");
				}
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0001400F File Offset: 0x0001220F
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00014017 File Offset: 0x00012217
		[DataSourceProperty]
		public int TotalStock
		{
			get
			{
				return this._totalStock;
			}
			set
			{
				if (value != this._totalStock)
				{
					this._totalStock = value;
					base.OnPropertyChangedWithValue(value, "TotalStock");
				}
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00014035 File Offset: 0x00012235
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0001403D File Offset: 0x0001223D
		[DataSourceProperty]
		public bool IsThisStockIncreasable
		{
			get
			{
				return this._isThisStockIncreasable;
			}
			set
			{
				if (value != this._isThisStockIncreasable)
				{
					this._isThisStockIncreasable = value;
					base.OnPropertyChangedWithValue(value, "IsThisStockIncreasable");
				}
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0001405B File Offset: 0x0001225B
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00014063 File Offset: 0x00012263
		[DataSourceProperty]
		public bool IsOtherStockIncreasable
		{
			get
			{
				return this._isOtherStockIncreasable;
			}
			set
			{
				if (value != this._isOtherStockIncreasable)
				{
					this._isOtherStockIncreasable = value;
					base.OnPropertyChangedWithValue(value, "IsOtherStockIncreasable");
				}
			}
		}

		// Token: 0x0400015D RID: 349
		private readonly PartyScreenLogic _partyScreenLogic;

		// Token: 0x0400015E RID: 350
		private readonly Action<int, bool> _onApplyTransaction;

		// Token: 0x0400015F RID: 351
		private readonly bool _isPrisoner;

		// Token: 0x04000160 RID: 352
		private TroopRosterElement _referenceTroopRoster;

		// Token: 0x04000161 RID: 353
		private readonly PartyScreenLogic.PartyRosterSide _side;

		// Token: 0x04000162 RID: 354
		private PartyScreenLogic.PartyRosterSide _otherSide;

		// Token: 0x04000163 RID: 355
		private bool _isTransfarable;

		// Token: 0x04000164 RID: 356
		private string _thisStockLbl;

		// Token: 0x04000165 RID: 357
		private string _totalStockLbl;

		// Token: 0x04000166 RID: 358
		private int _thisStock = -1;

		// Token: 0x04000167 RID: 359
		private int _initialThisStock;

		// Token: 0x04000168 RID: 360
		private int _otherStock;

		// Token: 0x04000169 RID: 361
		private int _initialOtherStock;

		// Token: 0x0400016A RID: 362
		private int _totalStock;

		// Token: 0x0400016B RID: 363
		private bool _isThisStockIncreasable;

		// Token: 0x0400016C RID: 364
		private bool _isOtherStockIncreasable;
	}
}
