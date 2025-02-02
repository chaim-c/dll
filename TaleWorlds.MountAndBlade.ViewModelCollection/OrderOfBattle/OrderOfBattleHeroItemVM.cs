using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x02000030 RID: 48
	public class OrderOfBattleHeroItemVM : ViewModel
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00010340 File Offset: 0x0000E540
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00010348 File Offset: 0x0000E548
		public ItemObject BannerOfHero { get; private set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00010351 File Offset: 0x0000E551
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00010359 File Offset: 0x0000E559
		public bool IsAssignedBeforePlayer { get; private set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00010362 File Offset: 0x0000E562
		// (set) Token: 0x060003BB RID: 955 RVA: 0x0001036A File Offset: 0x0000E56A
		public Formation InitialFormation { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00010373 File Offset: 0x0000E573
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0001037B File Offset: 0x0000E57B
		public OrderOfBattleFormationItemVM InitialFormationItem { get; private set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00010384 File Offset: 0x0000E584
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0001038C File Offset: 0x0000E58C
		public OrderOfBattleFormationItemVM CurrentAssignedFormationItem
		{
			get
			{
				return this._currentAssignedFormationItem;
			}
			set
			{
				if (value != this._currentAssignedFormationItem)
				{
					this._currentAssignedFormationItem = value;
					if (this._currentAssignedFormationItem == null)
					{
						this.OnAssignmentRemoved();
					}
					this.IsAssignedToAFormation = (this._currentAssignedFormationItem != null);
					this.IsLeadingAFormation = (this._currentAssignedFormationItem != null && this._currentAssignedFormationItem.Formation.Captain == this.Agent);
					this.OnAssignedFormationChanged();
				}
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000103F5 File Offset: 0x0000E5F5
		public OrderOfBattleHeroItemVM()
		{
			this.IsDisabled = true;
			this.RefreshValues();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001042C File Offset: 0x0000E62C
		public OrderOfBattleHeroItemVM(Agent agent)
		{
			this.Agent = agent;
			this.BannerOfHero = agent.FormationBanner;
			this.IsDisabled = (!Mission.Current.PlayerTeam.IsPlayerGeneral && !agent.IsMainAgent);
			this.IsShown = true;
			this.IsMainHero = this.Agent.IsMainAgent;
			this.ImageIdentifier = new ImageIdentifierVM(CharacterCode.CreateFrom(this.Agent.Character));
			this.Tooltip = new BasicTooltipViewModel(() => this.GetCommanderTooltip());
			this.RefreshValues();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000104E7 File Offset: 0x0000E6E7
		public void SetInitialFormation(OrderOfBattleFormationItemVM formation)
		{
			if (this.InitialFormationItem != null)
			{
				Debug.FailedAssert("Initial formation for hero is already set", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\OrderOfBattle\\OrderOfBattleHeroItemVM.cs", "SetInitialFormation", 76);
			}
			if (formation != null)
			{
				this.InitialFormationItem = formation;
				this.InitialFormation = formation.Formation;
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001051D File Offset: 0x0000E71D
		public override void RefreshValues()
		{
			Func<Agent, List<TooltipProperty>> getAgentTooltip = OrderOfBattleHeroItemVM.GetAgentTooltip;
			this._cachedTooltipProperties = ((getAgentTooltip != null) ? getAgentTooltip(this.Agent) : null);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001053C File Offset: 0x0000E73C
		private List<TooltipProperty> GetCommanderTooltip()
		{
			return this._cachedTooltipProperties;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00010544 File Offset: 0x0000E744
		public void OnAssignmentRemoved()
		{
			if (this.CurrentAssignedFormationItem != null)
			{
				this.CurrentAssignedFormationItem.Formation.Refresh();
			}
			if (this.InitialFormation != null)
			{
				this.Agent.Formation = this.InitialFormation;
				this.InitialFormation.Refresh();
				this.Agent.Team.DetachmentManager.RemoveScoresOfAgentFromDetachments(this.Agent);
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000105A8 File Offset: 0x0000E7A8
		public void RefreshInformation()
		{
			if (this.Agent != null)
			{
				this.ImageIdentifier = new ImageIdentifierVM(CharacterCode.CreateFrom(this.Agent.Character));
				return;
			}
			this.ImageIdentifier = new ImageIdentifierVM(CharacterCode.CreateEmpty());
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000105DE File Offset: 0x0000E7DE
		private void OnAssignedFormationChanged()
		{
			Action<OrderOfBattleHeroItemVM> onHeroAssignedFormationChanged = OrderOfBattleHeroItemVM.OnHeroAssignedFormationChanged;
			if (onHeroAssignedFormationChanged != null)
			{
				onHeroAssignedFormationChanged(this);
			}
			this.RefreshAssignmentInfo();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000105F8 File Offset: 0x0000E7F8
		public void RefreshAssignmentInfo()
		{
			if (!this.IsLeadingAFormation)
			{
				this.HasMismatchedAssignment = false;
				return;
			}
			DeploymentFormationClass orderOfBattleClass = this.CurrentAssignedFormationItem.GetOrderOfBattleClass();
			if (this.Agent.HasMount)
			{
				if (orderOfBattleClass == DeploymentFormationClass.Infantry || orderOfBattleClass == DeploymentFormationClass.Ranged || orderOfBattleClass == DeploymentFormationClass.InfantryAndRanged)
				{
					this.HasMismatchedAssignment = true;
					this.MismatchedAssignmentDescriptionText = this._mismatchMountedText.ToString();
					return;
				}
			}
			else if (orderOfBattleClass == DeploymentFormationClass.Cavalry || orderOfBattleClass == DeploymentFormationClass.HorseArcher || orderOfBattleClass == DeploymentFormationClass.CavalryAndHorseArcher)
			{
				this.HasMismatchedAssignment = true;
				this.MismatchedAssignmentDescriptionText = this._mismatchDismountedText.ToString();
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00010677 File Offset: 0x0000E877
		public void SetIsPreAssigned(bool isPreAssigned)
		{
			this.IsAssignedBeforePlayer = isPreAssigned;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00010680 File Offset: 0x0000E880
		private void ExecuteSelection()
		{
			Action<OrderOfBattleHeroItemVM> onHeroSelection = OrderOfBattleHeroItemVM.OnHeroSelection;
			if (onHeroSelection == null)
			{
				return;
			}
			onHeroSelection(this);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00010692 File Offset: 0x0000E892
		private void ExecuteBeginAssignment()
		{
			Action<OrderOfBattleHeroItemVM> onHeroAssignmentBegin = OrderOfBattleHeroItemVM.OnHeroAssignmentBegin;
			if (onHeroAssignmentBegin == null)
			{
				return;
			}
			onHeroAssignmentBegin(this);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000106A4 File Offset: 0x0000E8A4
		private void ExecuteEndAssignment()
		{
			Action<OrderOfBattleHeroItemVM> onHeroAssignmentEnd = OrderOfBattleHeroItemVM.OnHeroAssignmentEnd;
			if (onHeroAssignmentEnd == null)
			{
				return;
			}
			onHeroAssignmentEnd(this);
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003CD RID: 973 RVA: 0x000106B6 File Offset: 0x0000E8B6
		// (set) Token: 0x060003CE RID: 974 RVA: 0x000106BE File Offset: 0x0000E8BE
		[DataSourceProperty]
		public string MismatchedAssignmentDescriptionText
		{
			get
			{
				return this._mismatchedAssignmentDescriptionText;
			}
			set
			{
				if (value != this._mismatchedAssignmentDescriptionText)
				{
					this._mismatchedAssignmentDescriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "MismatchedAssignmentDescriptionText");
				}
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003CF RID: 975 RVA: 0x000106E1 File Offset: 0x0000E8E1
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x000106E9 File Offset: 0x0000E8E9
		[DataSourceProperty]
		public bool IsAssignedToAFormation
		{
			get
			{
				return this._isAssignedToAFormation;
			}
			set
			{
				if (value != this._isAssignedToAFormation)
				{
					this._isAssignedToAFormation = value;
					base.OnPropertyChangedWithValue(value, "IsAssignedToAFormation");
				}
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00010707 File Offset: 0x0000E907
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0001070F File Offset: 0x0000E90F
		[DataSourceProperty]
		public bool IsLeadingAFormation
		{
			get
			{
				return this._isLeadingAFormation;
			}
			set
			{
				if (value != this._isLeadingAFormation)
				{
					this._isLeadingAFormation = value;
					base.OnPropertyChangedWithValue(value, "IsLeadingAFormation");
				}
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0001072D File Offset: 0x0000E92D
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00010735 File Offset: 0x0000E935
		[DataSourceProperty]
		public bool HasMismatchedAssignment
		{
			get
			{
				return this._hasMismatchedAssignment;
			}
			set
			{
				if (value != this._hasMismatchedAssignment)
				{
					this._hasMismatchedAssignment = value;
					base.OnPropertyChangedWithValue(value, "HasMismatchedAssignment");
				}
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00010753 File Offset: 0x0000E953
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0001075B File Offset: 0x0000E95B
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

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00010779 File Offset: 0x0000E979
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00010781 File Offset: 0x0000E981
		public bool IsDisabled
		{
			get
			{
				return this._isDisabled;
			}
			set
			{
				if (value != this._isDisabled)
				{
					this._isDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsDisabled");
				}
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0001079F File Offset: 0x0000E99F
		// (set) Token: 0x060003DA RID: 986 RVA: 0x000107A7 File Offset: 0x0000E9A7
		public bool IsShown
		{
			get
			{
				return this._isShown;
			}
			set
			{
				if (value != this._isShown)
				{
					this._isShown = value;
					base.OnPropertyChangedWithValue(value, "IsShown");
				}
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003DB RID: 987 RVA: 0x000107C5 File Offset: 0x0000E9C5
		// (set) Token: 0x060003DC RID: 988 RVA: 0x000107CD File Offset: 0x0000E9CD
		public bool IsMainHero
		{
			get
			{
				return this._isMainHero;
			}
			set
			{
				if (value != this._isMainHero)
				{
					this._isMainHero = value;
					base.OnPropertyChangedWithValue(value, "IsMainHero");
				}
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003DD RID: 989 RVA: 0x000107EB File Offset: 0x0000E9EB
		// (set) Token: 0x060003DE RID: 990 RVA: 0x000107F3 File Offset: 0x0000E9F3
		[DataSourceProperty]
		public ImageIdentifierVM ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (value != this._imageIdentifier)
				{
					this._imageIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ImageIdentifier");
				}
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00010811 File Offset: 0x0000EA11
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00010819 File Offset: 0x0000EA19
		[DataSourceProperty]
		public BasicTooltipViewModel Tooltip
		{
			get
			{
				return this._tooltip;
			}
			set
			{
				if (value != this._tooltip)
				{
					this._tooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Tooltip");
				}
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00010837 File Offset: 0x0000EA37
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0001083F File Offset: 0x0000EA3F
		[DataSourceProperty]
		public bool IsHighlightActive
		{
			get
			{
				return this._isHighlightActive;
			}
			set
			{
				if (value != this._isHighlightActive)
				{
					this._isHighlightActive = value;
					base.OnPropertyChangedWithValue(value, "IsHighlightActive");
				}
			}
		}

		// Token: 0x040001E0 RID: 480
		private readonly TextObject _mismatchMountedText = new TextObject("{=jR237DSU}Commander is mounted!", null);

		// Token: 0x040001E1 RID: 481
		private readonly TextObject _mismatchDismountedText = new TextObject("{=i0esOr2l}Commander is not mounted!", null);

		// Token: 0x040001E2 RID: 482
		public static Action<OrderOfBattleHeroItemVM> OnHeroSelection;

		// Token: 0x040001E3 RID: 483
		public static Action<OrderOfBattleHeroItemVM> OnHeroAssignedFormationChanged;

		// Token: 0x040001E4 RID: 484
		public static Func<Agent, List<TooltipProperty>> GetAgentTooltip;

		// Token: 0x040001E5 RID: 485
		public static Action<OrderOfBattleHeroItemVM> OnHeroAssignmentBegin;

		// Token: 0x040001E6 RID: 486
		public static Action<OrderOfBattleHeroItemVM> OnHeroAssignmentEnd;

		// Token: 0x040001E7 RID: 487
		public readonly Agent Agent;

		// Token: 0x040001EC RID: 492
		private List<TooltipProperty> _cachedTooltipProperties;

		// Token: 0x040001ED RID: 493
		private OrderOfBattleFormationItemVM _currentAssignedFormationItem;

		// Token: 0x040001EE RID: 494
		private string _mismatchedAssignmentDescriptionText;

		// Token: 0x040001EF RID: 495
		private bool _isAssignedToAFormation;

		// Token: 0x040001F0 RID: 496
		private bool _isLeadingAFormation;

		// Token: 0x040001F1 RID: 497
		private bool _hasMismatchedAssignment;

		// Token: 0x040001F2 RID: 498
		private bool _isSelected;

		// Token: 0x040001F3 RID: 499
		private bool _isDisabled;

		// Token: 0x040001F4 RID: 500
		private bool _isShown;

		// Token: 0x040001F5 RID: 501
		private bool _isMainHero;

		// Token: 0x040001F6 RID: 502
		private ImageIdentifierVM _imageIdentifier;

		// Token: 0x040001F7 RID: 503
		private BasicTooltipViewModel _tooltip;

		// Token: 0x040001F8 RID: 504
		private bool _isHighlightActive;
	}
}
