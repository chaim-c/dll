using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.HUD.FormationMarker;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x02000029 RID: 41
	public class OrderTroopItemVM : OrderSubjectVM
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002FD RID: 765 RVA: 0x0000D908 File Offset: 0x0000BB08
		// (remove) Token: 0x060002FE RID: 766 RVA: 0x0000D93C File Offset: 0x0000BB3C
		public static event Action<OrderTroopItemVM, bool> OnSelectionChange;

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000D96F File Offset: 0x0000BB6F
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000D977 File Offset: 0x0000BB77
		public bool ContainsDeadTroop { get; private set; }

		// Token: 0x06000301 RID: 769 RVA: 0x0000D980 File Offset: 0x0000BB80
		public OrderTroopItemVM(Formation formation, Action<OrderTroopItemVM> setSelected, Func<Formation, int> getMorale)
		{
			this.ActiveFormationClasses = new MBBindingList<OrderTroopItemFormationClassVM>();
			this.ActiveFilters = new MBBindingList<OrderTroopItemFilterVM>();
			this.InitialFormationClass = formation.FormationIndex;
			this.SetFormationClassFromFormation(formation);
			this.Formation = formation;
			this.SetSelected = setSelected;
			this.CurrentMemberCount = (formation.IsPlayerTroopInFormation ? (formation.CountOfUnits - 1) : formation.CountOfUnits);
			this.Morale = getMorale(formation);
			base.UnderAttackOfType = 0;
			base.BehaviorType = 0;
			if (Input.IsControllerConnected)
			{
				bool flag = !Input.IsMouseActive;
			}
			this.UpdateSelectionKeyInfo();
			this.UpdateCommanderInfo();
			this.Formation.OnUnitCountChanged += this.FormationOnOnUnitCountChanged;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000DA3C File Offset: 0x0000BC3C
		public OrderTroopItemVM(OrderTroopItemVM troop, Action<OrderTroopItemVM> setSelected = null)
		{
			this.ActiveFormationClasses = new MBBindingList<OrderTroopItemFormationClassVM>();
			foreach (OrderTroopItemFormationClassVM orderTroopItemFormationClassVM in troop.ActiveFormationClasses)
			{
				this.ActiveFormationClasses.Add(new OrderTroopItemFormationClassVM(troop.Formation, orderTroopItemFormationClassVM.FormationClass));
			}
			this.ActiveFilters = new MBBindingList<OrderTroopItemFilterVM>();
			foreach (OrderTroopItemFilterVM orderTroopItemFilterVM in troop.ActiveFilters)
			{
				this.ActiveFilters.Add(new OrderTroopItemFilterVM(orderTroopItemFilterVM.FilterTypeValue));
			}
			this.InitialFormationClass = troop.InitialFormationClass;
			this.Formation = troop.Formation;
			this.SetSelected = (setSelected ?? troop.SetSelected);
			this.CurrentMemberCount = (troop.Formation.IsPlayerTroopInFormation ? (troop.CurrentMemberCount - 1) : troop.CurrentMemberCount);
			this.Morale = troop.Morale;
			base.UnderAttackOfType = 0;
			base.BehaviorType = 0;
			this.UpdateCommanderInfo();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000DB74 File Offset: 0x0000BD74
		public override void OnFinalize()
		{
			this.Formation.OnUnitCountChanged -= this.FormationOnOnUnitCountChanged;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000DB8D File Offset: 0x0000BD8D
		protected override void OnSelectionStateChanged(bool isSelected)
		{
			Action<OrderTroopItemVM, bool> onSelectionChange = OrderTroopItemVM.OnSelectionChange;
			if (onSelectionChange == null)
			{
				return;
			}
			onSelectionChange(this, isSelected);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
		private void FormationOnOnUnitCountChanged(Formation formation)
		{
			this.CurrentMemberCount = (formation.IsPlayerTroopInFormation ? (formation.CountOfUnits - 1) : formation.CountOfUnits);
			this.UpdateCommanderInfo();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000DBC6 File Offset: 0x0000BDC6
		public void OnFormationAgentRemoved(Agent agent)
		{
			if (!agent.IsActive())
			{
				this.ContainsDeadTroop = true;
			}
			this.UpdateCommanderInfo();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		private void UpdateCommanderInfo()
		{
			Formation formation = this.Formation;
			bool flag;
			if (formation == null)
			{
				flag = (null != null);
			}
			else
			{
				Agent captain = formation.Captain;
				flag = (((captain != null) ? captain.Character : null) != null);
			}
			if (flag && (this.Formation.Captain.Character != this._cachedCommander || this.CommanderImageIdentifier == null))
			{
				this.CommanderImageIdentifier = new ImageIdentifierVM(CharacterCode.CreateFrom(this.Formation.Captain.Character));
				this._cachedCommander = this.Formation.Captain.Character;
				return;
			}
			this.CommanderImageIdentifier = null;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		private void UpdateSelectionKeyInfo()
		{
			if (this.Formation == null)
			{
				return;
			}
			int num = -1;
			if (this.Formation.Index == 0)
			{
				num = 78;
			}
			else if (this.Formation.Index == 1)
			{
				num = 79;
			}
			else if (this.Formation.Index == 2)
			{
				num = 80;
			}
			else if (this.Formation.Index == 3)
			{
				num = 81;
			}
			else if (this.Formation.Index == 4)
			{
				num = 82;
			}
			else if (this.Formation.Index == 5)
			{
				num = 83;
			}
			else if (this.Formation.Index == 6)
			{
				num = 84;
			}
			else if (this.Formation.Index == 7)
			{
				num = 85;
			}
			if (num == -1)
			{
				return;
			}
			GameKey gameKey = HotKeyManager.GetCategory("MissionOrderHotkeyCategory").GetGameKey(num);
			base.SelectionKey = InputKeyItemVM.CreateFromGameKey(gameKey, false);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public bool SetFormationClassFromFormation(Formation formation)
		{
			bool flag = formation.GetCountOfUnitsBelongingToLogicalClass(FormationClass.Infantry) > 0;
			bool flag2 = formation.GetCountOfUnitsBelongingToLogicalClass(FormationClass.Ranged) > 0;
			bool flag3 = formation.GetCountOfUnitsBelongingToLogicalClass(FormationClass.Cavalry) > 0;
			bool flag4 = formation.GetCountOfUnitsBelongingToLogicalClass(FormationClass.HorseArcher) > 0;
			if (flag && this._cachedInfantryItem == null)
			{
				this._cachedInfantryItem = new OrderTroopItemFormationClassVM(formation, FormationClass.Infantry);
				this.ActiveFormationClasses.Add(this._cachedInfantryItem);
			}
			else if (!flag)
			{
				this.ActiveFormationClasses.Remove(this._cachedInfantryItem);
				this._cachedInfantryItem = null;
			}
			if (flag2 && this._cachedRangedItem == null)
			{
				this._cachedRangedItem = new OrderTroopItemFormationClassVM(formation, FormationClass.Ranged);
				this.ActiveFormationClasses.Add(this._cachedRangedItem);
			}
			else if (!flag2)
			{
				this.ActiveFormationClasses.Remove(this._cachedRangedItem);
				this._cachedRangedItem = null;
			}
			if (flag3 && this._cachedCavalryItem == null)
			{
				this._cachedCavalryItem = new OrderTroopItemFormationClassVM(formation, FormationClass.Cavalry);
				this.ActiveFormationClasses.Add(this._cachedCavalryItem);
			}
			else if (!flag3)
			{
				this.ActiveFormationClasses.Remove(this._cachedCavalryItem);
				this._cachedCavalryItem = null;
			}
			if (flag4 && this._cachedHorseArcherItem == null)
			{
				this._cachedHorseArcherItem = new OrderTroopItemFormationClassVM(formation, FormationClass.HorseArcher);
				this.ActiveFormationClasses.Add(this._cachedHorseArcherItem);
			}
			else if (!flag4)
			{
				this.ActiveFormationClasses.Remove(this._cachedHorseArcherItem);
				this._cachedHorseArcherItem = null;
			}
			foreach (OrderTroopItemFormationClassVM orderTroopItemFormationClassVM in this.ActiveFormationClasses)
			{
				orderTroopItemFormationClassVM.UpdateTroopCount();
			}
			this.UpdateCommanderInfo();
			return false;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000DEE0 File Offset: 0x0000C0E0
		public void UpdateFilterData(List<int> usedFilters)
		{
			this.ActiveFilters.Clear();
			foreach (int filterTypeValue in usedFilters)
			{
				this.ActiveFilters.Add(new OrderTroopItemFilterVM(filterTypeValue));
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000DF44 File Offset: 0x0000C144
		public void ExecuteAction()
		{
			this.SetSelected(this);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000DF54 File Offset: 0x0000C154
		public void RefreshTargetedOrderVisual()
		{
			bool hasTarget = false;
			string currentOrderIconId = null;
			string currentTargetFormationType = null;
			Formation targetFormation = this.Formation.TargetFormation;
			if (targetFormation != null)
			{
				OrderSubType activeMovementOrderOfFormation = OrderUIHelper.GetActiveMovementOrderOfFormation(this.Formation);
				if (activeMovementOrderOfFormation != OrderSubType.None && OrderUIHelper.CanOrderHaveTarget(activeMovementOrderOfFormation))
				{
					hasTarget = true;
					currentTargetFormationType = MissionFormationMarkerTargetVM.GetFormationType(targetFormation.PhysicalClass);
					currentOrderIconId = activeMovementOrderOfFormation.ToString();
				}
			}
			this.HasTarget = hasTarget;
			this.CurrentOrderIconId = currentOrderIconId;
			this.CurrentTargetFormationType = currentTargetFormationType;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000DFC2 File Offset: 0x0000C1C2
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000DFCA File Offset: 0x0000C1CA
		[DataSourceProperty]
		public int CurrentMemberCount
		{
			get
			{
				return this._currentMemberCount;
			}
			set
			{
				if (value != this._currentMemberCount)
				{
					this._currentMemberCount = value;
					base.OnPropertyChangedWithValue(value, "CurrentMemberCount");
					this.HaveTroops = (value > 0);
				}
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000DFF2 File Offset: 0x0000C1F2
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000DFFA File Offset: 0x0000C1FA
		[DataSourceProperty]
		public int Morale
		{
			get
			{
				return this._morale;
			}
			set
			{
				if (value != this._morale)
				{
					this._morale = value;
					base.OnPropertyChangedWithValue(value, "Morale");
				}
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000E018 File Offset: 0x0000C218
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000E020 File Offset: 0x0000C220
		[DataSourceProperty]
		public float AmmoPercentage
		{
			get
			{
				return this._ammoPercentage;
			}
			set
			{
				if (value != this._ammoPercentage)
				{
					this._ammoPercentage = value;
					base.OnPropertyChangedWithValue(value, "AmmoPercentage");
				}
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000E03E File Offset: 0x0000C23E
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000E046 File Offset: 0x0000C246
		[DataSourceProperty]
		public bool IsAmmoAvailable
		{
			get
			{
				return this._isAmmoAvailable;
			}
			set
			{
				if (value != this._isAmmoAvailable)
				{
					this._isAmmoAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsAmmoAvailable");
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000E064 File Offset: 0x0000C264
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000E06C File Offset: 0x0000C26C
		[DataSourceProperty]
		public bool HaveTroops
		{
			get
			{
				return this._haveTroops;
			}
			set
			{
				if (value != this._haveTroops)
				{
					this._haveTroops = value;
					base.OnPropertyChangedWithValue(value, "HaveTroops");
				}
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000E08A File Offset: 0x0000C28A
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000E092 File Offset: 0x0000C292
		[DataSourceProperty]
		public bool HasTarget
		{
			get
			{
				return this._hasTarget;
			}
			set
			{
				if (value != this._hasTarget)
				{
					this._hasTarget = value;
					base.OnPropertyChangedWithValue(value, "HasTarget");
				}
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
		[DataSourceProperty]
		public bool IsTargetRelevant
		{
			get
			{
				return this._isTargetRelevant;
			}
			set
			{
				if (value != this._isTargetRelevant)
				{
					this._isTargetRelevant = value;
					base.OnPropertyChangedWithValue(value, "IsTargetRelevant");
				}
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000E0D6 File Offset: 0x0000C2D6
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000E0DE File Offset: 0x0000C2DE
		[DataSourceProperty]
		public string CurrentOrderIconId
		{
			get
			{
				return this._currentOrderIconId;
			}
			set
			{
				if (value != this._currentOrderIconId)
				{
					this._currentOrderIconId = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentOrderIconId");
				}
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000E101 File Offset: 0x0000C301
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000E109 File Offset: 0x0000C309
		[DataSourceProperty]
		public string CurrentTargetFormationType
		{
			get
			{
				return this._currentTargetFormationType;
			}
			set
			{
				if (value != this._currentTargetFormationType)
				{
					this._currentTargetFormationType = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentTargetFormationType");
				}
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000E12C File Offset: 0x0000C32C
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000E134 File Offset: 0x0000C334
		[DataSourceProperty]
		public ImageIdentifierVM CommanderImageIdentifier
		{
			get
			{
				return this._commanderImageIdentifier;
			}
			set
			{
				if (value != this._commanderImageIdentifier)
				{
					this._commanderImageIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "CommanderImageIdentifier");
				}
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000E152 File Offset: 0x0000C352
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000E15A File Offset: 0x0000C35A
		[DataSourceProperty]
		public MBBindingList<OrderTroopItemFormationClassVM> ActiveFormationClasses
		{
			get
			{
				return this._activeFormationClasses;
			}
			set
			{
				if (value != this._activeFormationClasses)
				{
					this._activeFormationClasses = value;
					base.OnPropertyChangedWithValue<MBBindingList<OrderTroopItemFormationClassVM>>(value, "ActiveFormationClasses");
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000E178 File Offset: 0x0000C378
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000E180 File Offset: 0x0000C380
		[DataSourceProperty]
		public MBBindingList<OrderTroopItemFilterVM> ActiveFilters
		{
			get
			{
				return this._activeFilters;
			}
			set
			{
				if (value != this._activeFilters)
				{
					this._activeFilters = value;
					base.OnPropertyChangedWithValue<MBBindingList<OrderTroopItemFilterVM>>(value, "ActiveFilters");
				}
			}
		}

		// Token: 0x04000170 RID: 368
		public FormationClass InitialFormationClass;

		// Token: 0x04000171 RID: 369
		public Formation Formation;

		// Token: 0x04000172 RID: 370
		public Type MachineType;

		// Token: 0x04000173 RID: 371
		public Action<OrderTroopItemVM> SetSelected;

		// Token: 0x04000175 RID: 373
		private OrderTroopItemFormationClassVM _cachedInfantryItem;

		// Token: 0x04000176 RID: 374
		private OrderTroopItemFormationClassVM _cachedRangedItem;

		// Token: 0x04000177 RID: 375
		private OrderTroopItemFormationClassVM _cachedCavalryItem;

		// Token: 0x04000178 RID: 376
		private OrderTroopItemFormationClassVM _cachedHorseArcherItem;

		// Token: 0x04000179 RID: 377
		private BasicCharacterObject _cachedCommander;

		// Token: 0x0400017A RID: 378
		private int _currentMemberCount;

		// Token: 0x0400017B RID: 379
		private int _morale;

		// Token: 0x0400017C RID: 380
		private float _ammoPercentage;

		// Token: 0x0400017D RID: 381
		private bool _isAmmoAvailable;

		// Token: 0x0400017E RID: 382
		private bool _haveTroops;

		// Token: 0x0400017F RID: 383
		private bool _hasTarget;

		// Token: 0x04000180 RID: 384
		private bool _isTargetRelevant;

		// Token: 0x04000181 RID: 385
		private string _currentOrderIconId;

		// Token: 0x04000182 RID: 386
		private string _currentTargetFormationType;

		// Token: 0x04000183 RID: 387
		private ImageIdentifierVM _commanderImageIdentifier;

		// Token: 0x04000184 RID: 388
		private MBBindingList<OrderTroopItemFormationClassVM> _activeFormationClasses;

		// Token: 0x04000185 RID: 389
		private MBBindingList<OrderTroopItemFilterVM> _activeFilters;
	}
}
