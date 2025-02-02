using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Tutorial;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Library.EventSystem;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x02000032 RID: 50
	public class OrderOfBattleVM : ViewModel
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00010E50 File Offset: 0x0000F050
		protected int TotalFormationCount
		{
			get
			{
				return this._mission.PlayerTeam.FormationsIncludingEmpty.Count;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00010E67 File Offset: 0x0000F067
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x00010E6F File Offset: 0x0000F06F
		public List<ValueTuple<int, List<int>>> CurrentConfiguration { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00010E78 File Offset: 0x0000F078
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00010E80 File Offset: 0x0000F080
		public bool IsOrderPreconfigured { get; protected set; }

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010E8C File Offset: 0x0000F08C
		public OrderOfBattleVM()
		{
			this._allFormations = new List<OrderOfBattleFormationItemVM>();
			this._allHeroes = new List<OrderOfBattleHeroItemVM>();
			this._selectedHeroes = new List<OrderOfBattleHeroItemVM>();
			Game.Current.EventManager.RegisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.RefreshValues();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010F50 File Offset: 0x0000F150
		public override void RefreshValues()
		{
			this.BeginMissionText = new TextObject("{=SYYOSOoa}Ready", null).ToString();
			Mission mission = this._mission;
			if (mission != null && mission.IsSiegeBattle)
			{
				this.AutoDeployText = GameTexts.FindText("str_auto_deploy", null).ToString();
			}
			else
			{
				this.AutoDeployText = new TextObject("{=ADKHovtz}Reset Deployment", null).ToString();
			}
			this.MissingFormationsHint = new HintViewModel(this._missingFormationsHintText, null);
			this.SelectAllHint = new HintViewModel(this._selectAllHintText, null);
			this.ClearSelectionHint = new HintViewModel(this._clearSelectionHintText, null);
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM f)
			{
				f.RefreshValues();
			});
			MBBindingList<OrderOfBattleHeroItemVM> unassignedHeroes = this.UnassignedHeroes;
			if (unassignedHeroes == null)
			{
				return;
			}
			unassignedHeroes.ApplyActionOnAllItems(delegate(OrderOfBattleHeroItemVM c)
			{
				c.RefreshValues();
			});
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00011044 File Offset: 0x0000F244
		public override void OnFinalize()
		{
			base.OnFinalize();
			Game.Current.EventManager.UnregisterEvent<TutorialNotificationElementChangeEvent>(new Action<TutorialNotificationElementChangeEvent>(this.OnTutorialNotificationElementIDChange));
			this.FinalizeFormationCallbacks();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00011070 File Offset: 0x0000F270
		private void InitializeFormationCallbacks()
		{
			OrderOfBattleFormationItemVM.OnClassSelectionToggled = new Action<OrderOfBattleFormationItemVM>(this.OnClassSelectionToggled);
			OrderOfBattleFormationItemVM.OnFilterSelectionToggled = new Action<OrderOfBattleFormationItemVM>(this.OnFilterSelectionToggled);
			OrderOfBattleFormationItemVM.OnHeroesChanged = new Action(this.OnHeroesChanged);
			OrderOfBattleFormationItemVM.OnFilterUseToggled = new Action<OrderOfBattleFormationItemVM>(this.OnFilterUseToggled);
			OrderOfBattleFormationItemVM.OnSelection = new Action<OrderOfBattleFormationItemVM>(this.SelectFormationItem);
			OrderOfBattleFormationItemVM.OnDeselection = new Action<OrderOfBattleFormationItemVM>(this.DeselectFormationItem);
			OrderOfBattleFormationItemVM.GetTotalTroopCountWithFilter = new Func<DeploymentFormationClass, FormationFilterType, int>(this.GetTroopCountWithFilter);
			OrderOfBattleFormationItemVM.GetFormationWithCondition = new Func<Func<OrderOfBattleFormationItemVM, bool>, IEnumerable<OrderOfBattleFormationItemVM>>(this.GetFormationItemsWithCondition);
			OrderOfBattleFormationItemVM.HasAnyTroopWithClass = new Func<FormationClass, bool>(this.HasAnyTroopWithClass);
			OrderOfBattleFormationItemVM.OnAcceptCommander = new Action<OrderOfBattleFormationItemVM>(this.OnFormationAcceptCommander);
			OrderOfBattleFormationItemVM.OnAcceptHeroTroops = new Action<OrderOfBattleFormationItemVM>(this.OnFormationAcceptHeroTroops);
			OrderOfBattleFormationClassVM.OnWeightAdjustedCallback = new Action<OrderOfBattleFormationClassVM>(this.OnWeightAdjusted);
			OrderOfBattleFormationClassVM.OnClassChanged = new Action<OrderOfBattleFormationClassVM, FormationClass>(this.OnFormationClassChanged);
			OrderOfBattleFormationClassVM.CanAdjustWeight = new Func<OrderOfBattleFormationClassVM, bool>(this.CanAdjustWeight);
			OrderOfBattleFormationClassVM.GetTotalCountOfTroopType = new Func<FormationClass, int>(this.GetVisibleTotalTroopCountOfType);
			OrderOfBattleHeroItemVM.OnHeroAssignmentBegin = new Action<OrderOfBattleHeroItemVM>(this.OnHeroAssignmentBegin);
			OrderOfBattleHeroItemVM.OnHeroAssignmentEnd = new Action<OrderOfBattleHeroItemVM>(this.OnHeroAssignmentEnd);
			OrderOfBattleHeroItemVM.GetAgentTooltip = new Func<Agent, List<TooltipProperty>>(this.GetAgentTooltip);
			OrderOfBattleHeroItemVM.OnHeroSelection = new Action<OrderOfBattleHeroItemVM>(this.OnHeroSelection);
			OrderOfBattleHeroItemVM.OnHeroAssignedFormationChanged = new Action<OrderOfBattleHeroItemVM>(this.OnHeroAssignedFormationChanged);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000111D4 File Offset: 0x0000F3D4
		private void FinalizeFormationCallbacks()
		{
			OrderOfBattleFormationItemVM.OnClassSelectionToggled = null;
			OrderOfBattleFormationItemVM.OnFilterSelectionToggled = null;
			OrderOfBattleFormationItemVM.OnHeroesChanged = null;
			OrderOfBattleFormationItemVM.OnFilterUseToggled = null;
			OrderOfBattleFormationItemVM.OnSelection = null;
			OrderOfBattleFormationItemVM.OnDeselection = null;
			OrderOfBattleFormationItemVM.GetTotalTroopCountWithFilter = null;
			OrderOfBattleFormationItemVM.GetFormationWithCondition = null;
			OrderOfBattleFormationItemVM.HasAnyTroopWithClass = null;
			OrderOfBattleFormationItemVM.OnAcceptCommander = null;
			OrderOfBattleFormationItemVM.OnAcceptHeroTroops = null;
			OrderOfBattleFormationClassVM.OnWeightAdjustedCallback = null;
			OrderOfBattleFormationClassVM.OnClassChanged = null;
			OrderOfBattleFormationClassVM.CanAdjustWeight = null;
			OrderOfBattleFormationClassVM.GetTotalCountOfTroopType = null;
			OrderOfBattleHeroItemVM.OnHeroAssignmentBegin = null;
			OrderOfBattleHeroItemVM.OnHeroAssignmentEnd = null;
			OrderOfBattleHeroItemVM.GetAgentTooltip = null;
			OrderOfBattleHeroItemVM.OnHeroSelection = null;
			OrderOfBattleHeroItemVM.OnHeroAssignedFormationChanged = null;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001125C File Offset: 0x0000F45C
		public void Tick()
		{
			foreach (OrderOfBattleFormationItemVM orderOfBattleFormationItemVM in this._allFormations)
			{
				if (orderOfBattleFormationItemVM != null)
				{
					orderOfBattleFormationItemVM.Tick();
				}
				if (orderOfBattleFormationItemVM != null)
				{
					this.EnsureAllFormationTypesAreSet(orderOfBattleFormationItemVM);
				}
			}
			if (this._isInitialized)
			{
				if (this._isHeroSelectionDirty)
				{
					this.UpdateHeroItemSelection();
					this._isHeroSelectionDirty = false;
				}
				if (this._isTroopCountsDirty)
				{
					this.UpdateTroopTypeLookUpTable();
					this._isTroopCountsDirty = false;
				}
				if (this._isMissingFormationsDirty)
				{
					this.RefreshMissingFormations();
					this._isMissingFormationsDirty = false;
				}
				if (!this._isUnitDeployRefreshed)
				{
					this.OnUnitDeployed();
					this._isUnitDeployRefreshed = true;
				}
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00011318 File Offset: 0x0000F518
		[Conditional("DEBUG")]
		private void EnsureAllFormationPercentagesAreValid()
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001131C File Offset: 0x0000F51C
		private void EnsureAllFormationTypesAreSet(OrderOfBattleFormationItemVM f)
		{
			if (this.IsPlayerGeneral && f.OrderOfBattleFormationClassInt == 0 && f.Formation.CountOfUnits > 0)
			{
				bool oldValue = this._orderController.BackupAndDisableGesturesEnabled();
				for (int i = 0; i < this._allFormations.Count; i++)
				{
					OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = this._allFormations[i];
					if (this._orderController.SelectedFormations.Contains((orderOfBattleFormationItemVM != null) ? orderOfBattleFormationItemVM.Formation : null))
					{
						this._orderController.DeselectFormation((orderOfBattleFormationItemVM != null) ? orderOfBattleFormationItemVM.Formation : null);
					}
				}
				Func<OrderOfBattleFormationClassVM, bool> <>9__2;
				OrderOfBattleFormationItemVM orderOfBattleFormationItemVM2 = this._allFormations.Find(delegate(OrderOfBattleFormationItemVM other)
				{
					IEnumerable<OrderOfBattleFormationClassVM> classes = other.Classes;
					Func<OrderOfBattleFormationClassVM, bool> predicate;
					if ((predicate = <>9__2) == null)
					{
						predicate = (<>9__2 = ((OrderOfBattleFormationClassVM fc) => fc.Class == f.Formation.PhysicalClass));
					}
					return classes.Any(predicate);
				});
				if (orderOfBattleFormationItemVM2 == null)
				{
					orderOfBattleFormationItemVM2 = this._allFormations.Find((OrderOfBattleFormationItemVM other) => other.OrderOfBattleFormationClassInt != 0);
				}
				if (orderOfBattleFormationItemVM2 != null)
				{
					Formation formation = orderOfBattleFormationItemVM2.Formation;
					this._orderController.SelectFormation(f.Formation);
					this._orderController.SetOrderWithFormationAndNumber(OrderType.Transfer, formation, f.Formation.CountOfUnits);
					for (int j = 0; j < this._allFormations.Count; j++)
					{
						OrderOfBattleFormationItemVM orderOfBattleFormationItemVM3 = this._allFormations[j];
						if (this._orderController.SelectedFormations.Contains(orderOfBattleFormationItemVM3.Formation))
						{
							this._orderController.DeselectFormation(orderOfBattleFormationItemVM3.Formation);
						}
					}
					orderOfBattleFormationItemVM2.OnSizeChanged();
					f.OnSizeChanged();
					this.RefreshWeights();
					this._orderController.RestoreGesturesEnabled(oldValue);
				}
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000114D0 File Offset: 0x0000F6D0
		public void Initialize(Mission mission, Camera missionCamera, Action<int> selectFormationAtIndex, Action<int> deselectFormationAtIndex, Action clearFormationSelection, Action onAutoDeploy, Action onBeginMission, Dictionary<int, Agent> formationIndicesAndSergeants)
		{
			this._mission = mission;
			this._missionCamera = missionCamera;
			this._selectFormationAtIndex = selectFormationAtIndex;
			this._deselectFormationAtIndex = deselectFormationAtIndex;
			this._clearFormationSelection = clearFormationSelection;
			this._onAutoDeploy = onAutoDeploy;
			this._onBeginMission = onBeginMission;
			this._bannerBearerLogic = mission.GetMissionBehavior<BannerBearerLogic>();
			if (this._bannerBearerLogic != null)
			{
				this._bannerBearerLogic.OnBannerBearersUpdated += this.OnBannerBearersUpdated;
				this._bannerBearerLogic.OnBannerBearerAgentUpdated += this.OnBannerAgentUpdated;
			}
			this.InitializeFormationCallbacks();
			this._isInitialized = false;
			this._orderController = Mission.Current.PlayerTeam.PlayerOrderController;
			this._orderController.OnSelectedFormationsChanged += this.OnSelectedFormationsChanged;
			this._orderController.OnOrderIssued += this.OnOrderIssued;
			this.CurrentConfiguration = new List<ValueTuple<int, List<int>>>();
			this._availableTroopTypes = MissionGameModels.Current.BattleInitializationModel.GetAllAvailableTroopTypes();
			this.IsPlayerGeneral = this._mission.PlayerTeam.IsPlayerGeneral;
			this.FormationsFirstHalf = new MBBindingList<OrderOfBattleFormationItemVM>();
			this.FormationsSecondHalf = new MBBindingList<OrderOfBattleFormationItemVM>();
			this.UnassignedHeroes = new MBBindingList<OrderOfBattleHeroItemVM>();
			this._visibleTroopTypeCountLookup = new Dictionary<FormationClass, int>
			{
				{
					FormationClass.Infantry,
					0
				},
				{
					FormationClass.Ranged,
					0
				},
				{
					FormationClass.Cavalry,
					0
				},
				{
					FormationClass.HorseArcher,
					0
				}
			};
			for (int i = 0; i < this.TotalFormationCount; i++)
			{
				OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = new OrderOfBattleFormationItemVM(this._missionCamera);
				if (i < this.TotalFormationCount / 2)
				{
					this.FormationsFirstHalf.Add(orderOfBattleFormationItemVM);
				}
				else
				{
					this.FormationsSecondHalf.Add(orderOfBattleFormationItemVM);
				}
				this._allFormations.Add(orderOfBattleFormationItemVM);
				Formation formation = this._mission.PlayerTeam.FormationsIncludingEmpty.ElementAt(i);
				orderOfBattleFormationItemVM.RefreshFormation(formation, DeploymentFormationClass.Unset, false);
			}
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM f)
			{
				f.OnSizeChanged();
			});
			using (List<Agent>.Enumerator enumerator = this._mission.PlayerTeam.GetHeroAgents().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Agent heroAgent = enumerator.Current;
					this._allFormations.FirstOrDefault((OrderOfBattleFormationItemVM f) => heroAgent.Formation == f.Formation);
					OrderOfBattleHeroItemVM item = new OrderOfBattleHeroItemVM(heroAgent);
					this._allHeroes.Add(item);
					if (this.IsPlayerGeneral || heroAgent.IsMainAgent)
					{
						this.UnassignedHeroes.Add(item);
					}
				}
			}
			if (!this.IsPlayerGeneral)
			{
				using (Dictionary<int, Agent>.Enumerator enumerator2 = formationIndicesAndSergeants.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<int, Agent> preAssignedCommander = enumerator2.Current;
						this._allHeroes.First((OrderOfBattleHeroItemVM h) => h.Agent == preAssignedCommander.Value).SetIsPreAssigned(true);
						this.AssignCommander(preAssignedCommander.Value, this._allFormations[preAssignedCommander.Key]);
					}
				}
			}
			this.IsEnabled = true;
			this.SetAllFormationsLockState(true);
			this.LoadConfiguration();
			this.SetAllFormationsLockState(false);
			this.SetInitialHeroFormations();
			this.DistributeAllTroops();
			this._isInitialized = true;
			this.RefreshWeights();
			this.DeselectAllFormations();
			this.OnUnitDeployed();
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM f)
			{
				f.UpdateAdjustable();
			});
			if (!this.IsPlayerGeneral)
			{
				this.SelectHeroItem(this._allHeroes.FirstOrDefault((OrderOfBattleHeroItemVM h) => h.Agent.IsMainAgent));
			}
			this._isMissingFormationsDirty = true;
			this._isTroopCountsDirty = true;
			this.RefreshValues();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000118B8 File Offset: 0x0000FAB8
		private void UpdateTroopTypeLookUpTable()
		{
			for (FormationClass formationClass = FormationClass.Infantry; formationClass < FormationClass.NumberOfDefaultFormations; formationClass++)
			{
				this._visibleTroopTypeCountLookup[formationClass] = 0;
			}
			for (int i = 0; i < this._allFormations.Count; i++)
			{
				if (this._allFormations[i].Formation != null)
				{
					for (int j = 0; j < this._allFormations[i].Classes.Count; j++)
					{
						OrderOfBattleFormationClassVM orderOfBattleFormationClassVM = this._allFormations[i].Classes[j];
						if (!orderOfBattleFormationClassVM.IsUnset)
						{
							int visibleCountOfUnitsInClass = OrderOfBattleUIHelper.GetVisibleCountOfUnitsInClass(orderOfBattleFormationClassVM);
							Dictionary<FormationClass, int> visibleTroopTypeCountLookup = this._visibleTroopTypeCountLookup;
							FormationClass @class = orderOfBattleFormationClassVM.Class;
							visibleTroopTypeCountLookup[@class] += visibleCountOfUnitsInClass;
						}
					}
				}
			}
			foreach (OrderOfBattleFormationItemVM orderOfBattleFormationItemVM in this._allFormations)
			{
				orderOfBattleFormationItemVM.OnSizeChanged();
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000119C0 File Offset: 0x0000FBC0
		private void SetAllFormationsLockState(bool isLocked)
		{
			for (int i = 0; i < this._allFormations.Count; i++)
			{
				for (int j = 0; j < this._allFormations[i].Classes.Count; j++)
				{
					this._allFormations[i].Classes[j].SetWeightAdjustmentLock(isLocked);
				}
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00011A24 File Offset: 0x0000FC24
		private void OnBannerBearersUpdated(Formation formation)
		{
			if (this._isInitialized)
			{
				foreach (OrderOfBattleFormationItemVM orderOfBattleFormationItemVM in this._allFormations)
				{
					orderOfBattleFormationItemVM.Formation.QuerySystem.Expire();
				}
				this._isTroopCountsDirty = true;
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00011A90 File Offset: 0x0000FC90
		private void OnBannerAgentUpdated(Agent agent, bool isBannerBearer)
		{
			if (this._isInitialized && (agent.Team.IsPlayerTeam || agent.Team.IsPlayerAlly) && this._orderController.SelectedFormations.Contains(agent.Formation))
			{
				this._orderController.DeselectFormation(agent.Formation);
				this._orderController.SelectFormation(agent.Formation);
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00011AFC File Offset: 0x0000FCFC
		private OrderOfBattleFormationItemVM GetFirstAvailableFormationWithAnyClass(params FormationClass[] classes)
		{
			OrderOfBattleVM.<>c__DisplayClass51_0 CS$<>8__locals1 = new OrderOfBattleVM.<>c__DisplayClass51_0();
			CS$<>8__locals1.classes = classes;
			int i;
			int j;
			for (i = 0; i < CS$<>8__locals1.classes.Length; i = j + 1)
			{
				OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = this._allFormations.FirstOrDefault((OrderOfBattleFormationItemVM f) => f.HasClass(CS$<>8__locals1.classes[i]));
				if (orderOfBattleFormationItemVM != null)
				{
					return orderOfBattleFormationItemVM;
				}
				j = i;
			}
			return null;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00011B70 File Offset: 0x0000FD70
		private OrderOfBattleFormationItemVM GetInitialHeroFormation(OrderOfBattleHeroItemVM hero)
		{
			FormationClass heroClass = FormationClass.NumberOfAllFormations;
			for (FormationClass formationClass = FormationClass.Infantry; formationClass < FormationClass.NumberOfDefaultFormations; formationClass++)
			{
				if (OrderOfBattleUIHelper.IsAgentInFormationClass(hero.Agent, formationClass))
				{
					heroClass = formationClass;
				}
			}
			if (heroClass == FormationClass.NumberOfAllFormations)
			{
				return null;
			}
			OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = null;
			foreach (OrderOfBattleFormationItemVM orderOfBattleFormationItemVM2 in this._allFormations)
			{
				if (orderOfBattleFormationItemVM2.Commander.Agent == hero.Agent || orderOfBattleFormationItemVM2.HeroTroops.Contains(hero))
				{
					hero.Agent.Formation = orderOfBattleFormationItemVM2.Formation;
					return orderOfBattleFormationItemVM2;
				}
				if (orderOfBattleFormationItemVM2.Formation == hero.Agent.Formation)
				{
					for (int i = orderOfBattleFormationItemVM2.Classes.Count - 1; i >= 0; i--)
					{
						if (!orderOfBattleFormationItemVM2.Classes[i].IsUnset && orderOfBattleFormationItemVM2.Classes[i].Class == heroClass)
						{
							return orderOfBattleFormationItemVM2;
						}
					}
				}
				if (orderOfBattleFormationItemVM != null)
				{
					break;
				}
			}
			if (!this.UnassignedHeroes.Contains(hero))
			{
				this.UnassignedHeroes.Add(hero);
			}
			Func<OrderOfBattleFormationClassVM, bool> <>9__1;
			OrderOfBattleFormationItemVM orderOfBattleFormationItemVM3 = this._allFormations.FirstOrDefault(delegate(OrderOfBattleFormationItemVM x)
			{
				IEnumerable<OrderOfBattleFormationClassVM> classes = x.Classes;
				Func<OrderOfBattleFormationClassVM, bool> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = ((OrderOfBattleFormationClassVM c) => c.Class == heroClass));
				}
				return classes.Any(predicate);
			});
			if (orderOfBattleFormationItemVM3 != null)
			{
				hero.Agent.Formation = orderOfBattleFormationItemVM3.Formation;
				return orderOfBattleFormationItemVM3;
			}
			FormationClass[] array = null;
			if (heroClass == FormationClass.HorseArcher)
			{
				FormationClass[] array2 = new FormationClass[3];
				array2[0] = FormationClass.Cavalry;
				array2[1] = FormationClass.Ranged;
				array = array2;
			}
			else if (heroClass == FormationClass.Cavalry)
			{
				FormationClass[] array3 = new FormationClass[2];
				array3[0] = FormationClass.Ranged;
				array = array3;
			}
			else if (heroClass == FormationClass.Ranged)
			{
				array = new FormationClass[1];
			}
			if (array != null)
			{
				OrderOfBattleFormationItemVM firstAvailableFormationWithAnyClass = this.GetFirstAvailableFormationWithAnyClass(array);
				if (firstAvailableFormationWithAnyClass != null)
				{
					hero.Agent.Formation = firstAvailableFormationWithAnyClass.Formation;
					return firstAvailableFormationWithAnyClass;
				}
			}
			return null;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00011D68 File Offset: 0x0000FF68
		[return: TupleElementNames(new string[]
		{
			"Hero",
			"WasCommander"
		})]
		private List<ValueTuple<OrderOfBattleHeroItemVM, bool>> ClearAllHeroAssignments()
		{
			List<ValueTuple<OrderOfBattleHeroItemVM, bool>> list = new List<ValueTuple<OrderOfBattleHeroItemVM, bool>>();
			for (int i = 0; i < this._allFormations.Count; i++)
			{
				if (this._allFormations[i].HasCommander)
				{
					OrderOfBattleHeroItemVM commander = this._allFormations[i].Commander;
					list.Add(new ValueTuple<OrderOfBattleHeroItemVM, bool>(commander, true));
					this.ClearHeroAssignment(commander);
				}
				for (int j = this._allFormations[i].HeroTroops.Count - 1; j >= 0; j--)
				{
					OrderOfBattleHeroItemVM orderOfBattleHeroItemVM = this._allFormations[i].HeroTroops[j];
					list.Add(new ValueTuple<OrderOfBattleHeroItemVM, bool>(orderOfBattleHeroItemVM, false));
					this.ClearHeroAssignment(orderOfBattleHeroItemVM);
				}
			}
			return list;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00011E24 File Offset: 0x00010024
		private void AssignHeroesToInitialFormations([TupleElementNames(new string[]
		{
			"Hero",
			"WasCommander"
		})] List<ValueTuple<OrderOfBattleHeroItemVM, bool>> heroes)
		{
			for (int i = 0; i < heroes.Count; i++)
			{
				if (heroes[i].Item2)
				{
					this.AssignCommander(heroes[i].Item1.Agent, heroes[i].Item1.InitialFormationItem);
				}
				else
				{
					heroes[i].Item1.InitialFormationItem.AddHeroTroop(heroes[i].Item1);
				}
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00011E9C File Offset: 0x0001009C
		private void SetInitialHeroFormations()
		{
			for (int i = 0; i < this._allHeroes.Count; i++)
			{
				OrderOfBattleFormationItemVM initialHeroFormation = this.GetInitialHeroFormation(this._allHeroes[i]);
				if (initialHeroFormation != null)
				{
					this._allHeroes[i].SetInitialFormation(initialHeroFormation);
				}
				else
				{
					OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = this._allFormations.FirstOrDefault(delegate(OrderOfBattleFormationItemVM f)
					{
						if (f.HasFormation)
						{
							return f.Classes.Any((OrderOfBattleFormationClassVM c) => !c.IsUnset);
						}
						return false;
					});
					if (orderOfBattleFormationItemVM != null)
					{
						this._allHeroes[i].SetInitialFormation(orderOfBattleFormationItemVM);
					}
					else
					{
						Debug.FailedAssert("Failed to find an initial formation for hero: " + this._allHeroes[i].Agent.Name, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\OrderOfBattle\\OrderOfBattleVM.cs", "SetInitialHeroFormations", 639);
					}
				}
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00011F65 File Offset: 0x00010165
		protected virtual void LoadConfiguration()
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00011F67 File Offset: 0x00010167
		protected virtual void SaveConfiguration()
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00011F6C File Offset: 0x0001016C
		protected virtual List<TooltipProperty> GetAgentTooltip(Agent agent)
		{
			if (agent == null)
			{
				return new List<TooltipProperty>
				{
					new TooltipProperty("", string.Empty, 0, false, TooltipProperty.TooltipPropertyFlags.RundownSeperator)
				};
			}
			List<TooltipProperty> list = new List<TooltipProperty>
			{
				new TooltipProperty(agent.Name, string.Empty, 0, false, TooltipProperty.TooltipPropertyFlags.Title)
			};
			BannerComponent bannerComponent;
			if (agent.FormationBanner != null && (bannerComponent = (agent.FormationBanner.ItemComponent as BannerComponent)) != null)
			{
				list.Add(new TooltipProperty(this._bannerText.ToString(), agent.FormationBanner.Name.ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				GameTexts.SetVariable("RANK", bannerComponent.BannerEffect.Name);
				string content = string.Empty;
				if (bannerComponent.BannerEffect.IncrementType == BannerEffect.EffectIncrementType.AddFactor)
				{
					GameTexts.FindText("str_NUMBER_percent", null).SetTextVariable("NUMBER", ((int)Math.Abs(bannerComponent.GetBannerEffectBonus() * 100f)).ToString());
					object obj;
					content = obj.ToString();
				}
				else if (bannerComponent.BannerEffect.IncrementType == BannerEffect.EffectIncrementType.Add)
				{
					content = bannerComponent.GetBannerEffectBonus().ToString();
				}
				GameTexts.SetVariable("NUMBER", content);
				list.Add(new TooltipProperty(this._bannerEffectText.ToString(), GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			else
			{
				list.Add(new TooltipProperty(this._noBannerEquippedText.ToString(), string.Empty, 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			list.Add(new TooltipProperty(string.Empty, string.Empty, 0, false, TooltipProperty.TooltipPropertyFlags.None));
			return list;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000120F8 File Offset: 0x000102F8
		private bool HasAnyTroopWithClass(FormationClass formationClass)
		{
			return this._availableTroopTypes.Contains(formationClass);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00012108 File Offset: 0x00010308
		private void RefreshWeights()
		{
			if (this._isSaving || !this._isInitialized)
			{
				return;
			}
			List<OrderOfBattleFormationClassVM> list = new List<OrderOfBattleFormationClassVM>();
			for (int i = 0; i < this._allFormations.Count; i++)
			{
				OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = this._allFormations[i];
				for (int j = 0; j < orderOfBattleFormationItemVM.Classes.Count; j++)
				{
					OrderOfBattleFormationClassVM orderOfBattleFormationClassVM = orderOfBattleFormationItemVM.Classes[j];
					if (orderOfBattleFormationClassVM.Class != FormationClass.NumberOfAllFormations)
					{
						list.Add(orderOfBattleFormationClassVM);
					}
				}
			}
			for (int k = 0; k < list.Count; k++)
			{
				OrderOfBattleFormationClassVM orderOfBattleFormationClassVM2 = list[k];
				orderOfBattleFormationClassVM2.SetWeightAdjustmentLock(true);
				float num = (float)OrderOfBattleUIHelper.GetCountOfRealUnitsInClass(orderOfBattleFormationClassVM2);
				float num2 = 0f;
				for (int l = 0; l < list.Count; l++)
				{
					OrderOfBattleFormationClassVM orderOfBattleFormationClassVM3 = list[k];
					if (orderOfBattleFormationClassVM3.Class == orderOfBattleFormationClassVM2.Class)
					{
						int countOfRealUnitsInClass = OrderOfBattleUIHelper.GetCountOfRealUnitsInClass(orderOfBattleFormationClassVM3);
						if (countOfRealUnitsInClass < 0 || countOfRealUnitsInClass > orderOfBattleFormationClassVM3.BelongedFormationItem.Formation.CountOfUnits)
						{
							orderOfBattleFormationClassVM3.SetWeightAdjustmentLock(true);
							orderOfBattleFormationClassVM3.Weight = 0;
							orderOfBattleFormationClassVM3.SetWeightAdjustmentLock(false);
							Debug.FailedAssert("Formation unit count is out of bounds! Skipping...", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\OrderOfBattle\\OrderOfBattleVM.cs", "RefreshWeights", 757);
							Debug.Print("Formation unit count is out of bounds! Skipping...", 0, Debug.DebugColor.White, 17592186044416UL);
						}
						else
						{
							num2 += (float)countOfRealUnitsInClass;
						}
					}
				}
				orderOfBattleFormationClassVM2.Weight = MathF.Round(num / num2 * 100f);
				orderOfBattleFormationClassVM2.IsLocked = !this.IsPlayerGeneral;
				orderOfBattleFormationClassVM2.SetWeightAdjustmentLock(false);
			}
			for (FormationClass formationClass = FormationClass.Infantry; formationClass < FormationClass.NumberOfDefaultFormations; formationClass++)
			{
				List<OrderOfBattleFormationClassVM> list2 = new List<OrderOfBattleFormationClassVM>();
				for (int m = 0; m < list.Count; m++)
				{
					OrderOfBattleFormationClassVM orderOfBattleFormationClassVM4 = list[m];
					if (orderOfBattleFormationClassVM4.Class == formationClass)
					{
						list2.Add(orderOfBattleFormationClassVM4);
					}
				}
				if (list2.Count > 1)
				{
					int num3 = 0;
					for (int n = 0; n < list2.Count; n++)
					{
						OrderOfBattleFormationClassVM orderOfBattleFormationClassVM5 = list2[n];
						if (orderOfBattleFormationClassVM5.Weight < 0 || orderOfBattleFormationClassVM5.Weight > 100)
						{
							orderOfBattleFormationClassVM5.SetWeightAdjustmentLock(true);
							orderOfBattleFormationClassVM5.Weight = 0;
							orderOfBattleFormationClassVM5.SetWeightAdjustmentLock(false);
							Debug.FailedAssert("Item weight is out of bounds! Skipping...", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\OrderOfBattle\\OrderOfBattleVM.cs", "RefreshWeights", 798);
							Debug.Print("Item weight is out of bounds! Skipping...", 0, Debug.DebugColor.White, 17592186044416UL);
						}
						else
						{
							num3 += orderOfBattleFormationClassVM5.Weight;
						}
					}
					for (int num4 = MathF.Abs(num3 - 100); num4 > 0; num4--)
					{
						bool flag = num3 < 100;
						object obj;
						if (!flag)
						{
							obj = list2.MaxBy((OrderOfBattleFormationClassVM c) => c.Weight);
						}
						else
						{
							obj = list2.MinBy((OrderOfBattleFormationClassVM c) => c.Weight);
						}
						object obj2 = obj;
						obj2.SetWeightAdjustmentLock(true);
						obj2.Weight += (flag ? 1 : -1);
						obj2.SetWeightAdjustmentLock(false);
					}
				}
			}
			list.ForEach(delegate(OrderOfBattleFormationClassVM fc)
			{
				fc.UpdateWeightAdjustable();
			});
			list.ForEach(delegate(OrderOfBattleFormationClassVM fc)
			{
				fc.UpdateWeightText();
			});
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00012470 File Offset: 0x00010670
		public void OnAllFormationsAssignedSergeants(Dictionary<int, Agent> preAssignedCommanders)
		{
			foreach (KeyValuePair<int, Agent> keyValuePair in preAssignedCommanders)
			{
				this.AssignCommander(keyValuePair.Value, this._allFormations[keyValuePair.Key]);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000124D8 File Offset: 0x000106D8
		private void OnClassSelectionToggled(OrderOfBattleFormationItemVM formationItem)
		{
			if (formationItem != null && formationItem.IsClassSelectionActive)
			{
				this._lastEnabledClassSelection = formationItem;
				return;
			}
			this._lastEnabledClassSelection = null;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000124F4 File Offset: 0x000106F4
		private void OnFilterSelectionToggled(OrderOfBattleFormationItemVM formationItem)
		{
			if (formationItem != null && formationItem.IsFilterSelectionActive)
			{
				this._lastEnabledFilterSelection = formationItem;
				return;
			}
			this._lastEnabledFilterSelection = null;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00012510 File Offset: 0x00010710
		public bool IsAnyClassSelectionEnabled()
		{
			return this._lastEnabledClassSelection != null;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001251B File Offset: 0x0001071B
		public bool IsAnyFilterSelectionEnabled()
		{
			return this._lastEnabledFilterSelection != null;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00012526 File Offset: 0x00010726
		public void ExecuteDisableAllClassSelections()
		{
			if (this._lastEnabledClassSelection != null)
			{
				this._lastEnabledClassSelection.IsClassSelectionActive = false;
				this._lastEnabledClassSelection = null;
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00012543 File Offset: 0x00010743
		public void ExecuteDisableAllFilterSelections()
		{
			if (this._lastEnabledFilterSelection != null)
			{
				this._lastEnabledFilterSelection.IsFilterSelectionActive = false;
				this._lastEnabledFilterSelection = null;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00012560 File Offset: 0x00010760
		private void SelectHeroItem(OrderOfBattleHeroItemVM heroItem)
		{
			if (!this._selectedHeroes.Contains(heroItem))
			{
				heroItem.IsSelected = true;
				this._selectedHeroes.Add(heroItem);
				this.UpdateHeroItemSelection();
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00012589 File Offset: 0x00010789
		private void DeselectHeroItem(OrderOfBattleHeroItemVM heroItem)
		{
			heroItem.IsSelected = false;
			this._selectedHeroes.Remove(heroItem);
			this.UpdateHeroItemSelection();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000125A5 File Offset: 0x000107A5
		private void ToggleHeroItemSelection(OrderOfBattleHeroItemVM heroItem)
		{
			if (this._selectedHeroes.Contains(heroItem))
			{
				this.DeselectHeroItem(heroItem);
			}
			else
			{
				this.SelectHeroItem(heroItem);
			}
			this.UpdateHeroItemSelection();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000125CC File Offset: 0x000107CC
		private void UpdateHeroItemSelection()
		{
			bool flag = this._selectedHeroes.Count > 0;
			foreach (OrderOfBattleFormationItemVM orderOfBattleFormationItemVM in this._allFormations)
			{
				bool hasOwnHeroTroopInSelection = orderOfBattleFormationItemVM.HeroTroops.Any((OrderOfBattleHeroItemVM heroTroop) => this._selectedHeroes.Contains(heroTroop));
				orderOfBattleFormationItemVM.OnHeroSelectionUpdated(this._selectedHeroes.Count, hasOwnHeroTroopInSelection);
			}
			bool isPoolAcceptingCommander;
			if (flag)
			{
				isPoolAcceptingCommander = this._selectedHeroes.All((OrderOfBattleHeroItemVM hero) => hero.IsLeadingAFormation);
			}
			else
			{
				isPoolAcceptingCommander = false;
			}
			this.IsPoolAcceptingCommander = isPoolAcceptingCommander;
			bool isPoolAcceptingHeroTroops;
			if (flag && !this.IsPoolAcceptingCommander)
			{
				isPoolAcceptingHeroTroops = this._selectedHeroes.All((OrderOfBattleHeroItemVM hero) => hero.IsAssignedToAFormation);
			}
			else
			{
				isPoolAcceptingHeroTroops = false;
			}
			this.IsPoolAcceptingHeroTroops = isPoolAcceptingHeroTroops;
			this.SelectedHeroCount = this._selectedHeroes.Count;
			this.HasSelectedHeroes = flag;
			this.LastSelectedHeroItem = ((this._selectedHeroes.Count > 0) ? this._selectedHeroes[this._selectedHeroes.Count - 1] : null);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001270C File Offset: 0x0001090C
		private void OnHeroAssignmentBegin(OrderOfBattleHeroItemVM heroItem)
		{
			this.SelectHeroItem(heroItem);
			this._selectedHeroes.ForEach(delegate(OrderOfBattleHeroItemVM hero)
			{
				hero.IsShown = false;
			});
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001273F File Offset: 0x0001093F
		private void OnHeroAssignmentEnd(OrderOfBattleHeroItemVM heroItem)
		{
			this._selectedHeroes.ForEach(delegate(OrderOfBattleHeroItemVM hero)
			{
				hero.IsShown = true;
			});
			this.UpdateHeroItemSelection();
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00012771 File Offset: 0x00010971
		private void ClearAndSelectHeroItem(OrderOfBattleHeroItemVM heroItem)
		{
			this.ClearHeroItemSelection();
			this.SelectHeroItem(heroItem);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00012780 File Offset: 0x00010980
		private void ClearHeroAssignment(OrderOfBattleHeroItemVM heroItem)
		{
			if (heroItem.IsLeadingAFormation)
			{
				heroItem.CurrentAssignedFormationItem.UnassignCommander();
				return;
			}
			if (heroItem.IsAssignedToAFormation)
			{
				heroItem.CurrentAssignedFormationItem.RemoveHeroTroop(heroItem);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000127AC File Offset: 0x000109AC
		protected void AssignCommander(Agent agent, OrderOfBattleFormationItemVM formationItem)
		{
			OrderOfBattleHeroItemVM orderOfBattleHeroItemVM = this._allHeroes.FirstOrDefault((OrderOfBattleHeroItemVM h) => h.Agent == agent);
			if (formationItem != null && orderOfBattleHeroItemVM != null && formationItem.Commander != orderOfBattleHeroItemVM)
			{
				if (formationItem.HasCommander)
				{
					formationItem.Commander.IsSelected = false;
					formationItem.UnassignCommander();
				}
				formationItem.Commander = orderOfBattleHeroItemVM;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001280E File Offset: 0x00010A0E
		private void ClearHeroItemSelection()
		{
			this._selectedHeroes.ForEach(delegate(OrderOfBattleHeroItemVM hero)
			{
				hero.IsSelected = false;
			});
			this._selectedHeroes.Clear();
			this.UpdateHeroItemSelection();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001284C File Offset: 0x00010A4C
		public void ExecuteAcceptHeroes()
		{
			foreach (OrderOfBattleHeroItemVM orderOfBattleHeroItemVM in this._selectedHeroes)
			{
				this.ClearHeroAssignment(orderOfBattleHeroItemVM);
				orderOfBattleHeroItemVM.IsShown = true;
			}
			this.ClearHeroItemSelection();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000128AC File Offset: 0x00010AAC
		public void ExecuteSelectAllHeroes()
		{
			this.ClearHeroItemSelection();
			foreach (OrderOfBattleHeroItemVM heroItem in this.UnassignedHeroes)
			{
				this.SelectHeroItem(heroItem);
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00012900 File Offset: 0x00010B00
		public void ExecuteClearHeroSelection()
		{
			this.ClearHeroItemSelection();
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00012908 File Offset: 0x00010B08
		private void OnFormationAcceptCommander(OrderOfBattleFormationItemVM formationItem)
		{
			if (this._selectedHeroes.Count != 1)
			{
				this._selectedHeroes.ForEach(delegate(OrderOfBattleHeroItemVM hero)
				{
					hero.IsShown = true;
				});
				this.ClearHeroItemSelection();
				return;
			}
			OrderOfBattleHeroItemVM orderOfBattleHeroItemVM = this._selectedHeroes[0];
			this.ClearHeroAssignment(orderOfBattleHeroItemVM);
			this.AssignCommander(orderOfBattleHeroItemVM.Agent, formationItem);
			this.ClearHeroItemSelection();
			orderOfBattleHeroItemVM.IsShown = true;
			if (!this.IsPlayerGeneral)
			{
				this._mission.GetMissionBehavior<AssignPlayerRoleInTeamMissionController>().OnPlayerChoiceMade(formationItem.Formation.Index, false);
			}
			Game game = Game.Current;
			if (game == null)
			{
				return;
			}
			game.EventManager.TriggerEvent<OrderOfBattleHeroAssignedToFormationEvent>(new OrderOfBattleHeroAssignedToFormationEvent(orderOfBattleHeroItemVM.Agent, formationItem.Formation));
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000129CC File Offset: 0x00010BCC
		private void OnFormationAcceptHeroTroops(OrderOfBattleFormationItemVM formationItem)
		{
			foreach (OrderOfBattleHeroItemVM orderOfBattleHeroItemVM in this._selectedHeroes)
			{
				this.ClearHeroAssignment(orderOfBattleHeroItemVM);
				formationItem.AddHeroTroop(orderOfBattleHeroItemVM);
				orderOfBattleHeroItemVM.IsShown = true;
			}
			this.ClearHeroItemSelection();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00012A34 File Offset: 0x00010C34
		private void OnHeroSelection(OrderOfBattleHeroItemVM heroSlotItem)
		{
			if (!this.IsPlayerGeneral)
			{
				this.ToggleHeroItemSelection(heroSlotItem);
				return;
			}
			if (heroSlotItem.IsLeadingAFormation)
			{
				this.ClearAndSelectHeroItem(heroSlotItem);
				return;
			}
			this.ToggleHeroItemSelection(heroSlotItem);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00012A60 File Offset: 0x00010C60
		private void OnFilterUseToggled(OrderOfBattleFormationItemVM formationItem)
		{
			foreach (OrderOfBattleFormationClassVM orderOfBattleFormationClassVM in formationItem.Classes)
			{
				if (orderOfBattleFormationClassVM.Class != FormationClass.NumberOfAllFormations)
				{
					this.DistributeTroops(orderOfBattleFormationClassVM);
				}
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00012AB8 File Offset: 0x00010CB8
		public void OnDeploymentFinalized(bool playerDeployed)
		{
			if (playerDeployed)
			{
				this._isSaving = true;
				OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = this._allFormations.FirstOrDefault((OrderOfBattleFormationItemVM f) => f.Commander.Agent == Agent.Main);
				if (orderOfBattleFormationItemVM != null)
				{
					this._mission.GetMissionBehavior<AssignPlayerRoleInTeamMissionController>().OnPlayerChoiceMade(orderOfBattleFormationItemVM.Formation.Index, true);
				}
				this.SaveConfiguration();
				this._isSaving = false;
				this._orderController.OnSelectedFormationsChanged -= this.OnSelectedFormationsChanged;
				this._orderController.OnOrderIssued -= this.OnOrderIssued;
			}
			this.IsEnabled = false;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00012B60 File Offset: 0x00010D60
		private void OnHeroAssignedFormationChanged(OrderOfBattleHeroItemVM heroItem)
		{
			if (heroItem.IsAssignedToAFormation)
			{
				this.UnassignedHeroes.Remove(this.UnassignedHeroes.FirstOrDefault((OrderOfBattleHeroItemVM h) => h.Agent == heroItem.Agent));
			}
			else if (this.IsPlayerGeneral || heroItem.Agent.IsMainAgent)
			{
				this.UnassignedHeroes.Insert(0, heroItem);
			}
			this._isTroopCountsDirty = true;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00012BDF File Offset: 0x00010DDF
		private bool CanAdjustWeight(OrderOfBattleFormationClassVM formationClass)
		{
			return this._isInitialized && OrderOfBattleUIHelper.GetMatchingClasses(this._allFormations, formationClass, null).Count > 1;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00012C00 File Offset: 0x00010E00
		private void OnWeightAdjusted(OrderOfBattleFormationClassVM formationClass)
		{
			if (!this._isInitialized)
			{
				return;
			}
			this.DistributeWeights(formationClass);
			this.DistributeTroops(formationClass);
			EventManager eventManager = Game.Current.EventManager;
			OrderOfBattleFormationItemVM belongedFormationItem = formationClass.BelongedFormationItem;
			eventManager.TriggerEvent<OrderOfBattleFormationWeightChangedEvent>(new OrderOfBattleFormationWeightChangedEvent((belongedFormationItem != null) ? belongedFormationItem.Formation : null));
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00012C40 File Offset: 0x00010E40
		private void DistributeTroops(OrderOfBattleFormationClassVM formationClass)
		{
			List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>> massTransferDataForFormation = this.GetMassTransferDataForFormation(formationClass);
			if (massTransferDataForFormation.Count > 0)
			{
				this._orderController.RearrangeFormationsAccordingToFilters(this._mission.PlayerTeam, massTransferDataForFormation);
				this.RefreshFormationsWithClass(formationClass.Class);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00012C84 File Offset: 0x00010E84
		private void DistributeWeights(OrderOfBattleFormationClassVM formationClass)
		{
			List<OrderOfBattleFormationClassVM> matchingClasses = OrderOfBattleUIHelper.GetMatchingClasses(this._allFormations, formationClass, null);
			List<OrderOfBattleFormationClassVM> matchingClasses2 = OrderOfBattleUIHelper.GetMatchingClasses(this._allFormations, formationClass, (OrderOfBattleFormationClassVM fc) => !fc.IsLocked);
			if (matchingClasses2.Count == 1)
			{
				formationClass.SetWeightAdjustmentLock(true);
				formationClass.Weight = formationClass.PreviousWeight;
				formationClass.SetWeightAdjustmentLock(false);
				return;
			}
			int num = OrderOfBattleUIHelper.GetMatchingClasses(this._allFormations, formationClass, (OrderOfBattleFormationClassVM fc) => fc.IsLocked).Sum((OrderOfBattleFormationClassVM fc) => fc.Weight);
			int adjustableWeight = 100 - num;
			if (formationClass.Weight > adjustableWeight)
			{
				formationClass.SetWeightAdjustmentLock(true);
				formationClass.Weight = adjustableWeight;
				formationClass.SetWeightAdjustmentLock(false);
				matchingClasses2.Remove(formationClass);
				matchingClasses2.ForEach(delegate(OrderOfBattleFormationClassVM c)
				{
					c.SetWeightAdjustmentLock(true);
					c.Weight = 0;
					c.SetWeightAdjustmentLock(false);
				});
				return;
			}
			matchingClasses2.Remove(formationClass);
			int changePerClass = MathF.Round((float)(formationClass.PreviousWeight - formationClass.Weight) / (float)matchingClasses2.Count);
			matchingClasses2.ForEach(delegate(OrderOfBattleFormationClassVM formation)
			{
				formation.SetWeightAdjustmentLock(true);
			});
			if (changePerClass != 0)
			{
				matchingClasses2.ForEach(delegate(OrderOfBattleFormationClassVM formation)
				{
					int num4 = MBMath.ClampInt(changePerClass, -formation.Weight, adjustableWeight - formation.Weight);
					formation.Weight += num4;
				});
			}
			int num2 = matchingClasses.Sum((OrderOfBattleFormationClassVM c) => c.Weight);
			while (matchingClasses2.Count > 0 && num2 != 100)
			{
				int num3 = num2;
				if (num2 > 100)
				{
					OrderOfBattleFormationClassVM formationClassWithExtremumWeight = OrderOfBattleUIHelper.GetFormationClassWithExtremumWeight(matchingClasses2, false);
					if (formationClassWithExtremumWeight != null)
					{
						OrderOfBattleFormationClassVM orderOfBattleFormationClassVM = formationClassWithExtremumWeight;
						int weight = orderOfBattleFormationClassVM.Weight;
						orderOfBattleFormationClassVM.Weight = weight - 1;
						num2--;
					}
				}
				else if (num2 < 100)
				{
					OrderOfBattleFormationClassVM formationClassWithExtremumWeight2 = OrderOfBattleUIHelper.GetFormationClassWithExtremumWeight(matchingClasses2, true);
					if (formationClassWithExtremumWeight2 != null)
					{
						OrderOfBattleFormationClassVM orderOfBattleFormationClassVM2 = formationClassWithExtremumWeight2;
						int weight = orderOfBattleFormationClassVM2.Weight;
						orderOfBattleFormationClassVM2.Weight = weight + 1;
						num2++;
					}
				}
				if (num3 == num2)
				{
					Debug.FailedAssert("Failed to sum up all weights to 100", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\OrderOfBattle\\OrderOfBattleVM.cs", "DistributeWeights", 1236);
					break;
				}
			}
			matchingClasses2.ForEach(delegate(OrderOfBattleFormationClassVM formation)
			{
				formation.SetWeightAdjustmentLock(false);
			});
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00012EE8 File Offset: 0x000110E8
		private void DistributeAllTroops()
		{
			if (this._mission.PlayerTeam == null)
			{
				Debug.FailedAssert("Player team should be initialized before distributing troops", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\OrderOfBattle\\OrderOfBattleVM.cs", "DistributeAllTroops", 1248);
				Debug.Print("Player team should be initialized before distributing troops", 0, Debug.DebugColor.White, 17592186044416UL);
				return;
			}
			List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>> list = new List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>>();
			List<FormationClass> list2 = new List<FormationClass>();
			for (int i = 0; i < this._allFormations.Count; i++)
			{
				for (int j = 0; j < this._allFormations[i].Classes.Count; j++)
				{
					OrderOfBattleFormationClassVM orderOfBattleFormationClassVM = this._allFormations[i].Classes[j];
					if (!orderOfBattleFormationClassVM.IsUnset && !list2.Contains(orderOfBattleFormationClassVM.Class))
					{
						List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>> massTransferDataForFormation = this.GetMassTransferDataForFormation(orderOfBattleFormationClassVM);
						list.AddRange(massTransferDataForFormation);
						list2.Add(orderOfBattleFormationClassVM.Class);
					}
				}
				if (list.Count > 0)
				{
					this._orderController.RearrangeFormationsAccordingToFilters(this._mission.PlayerTeam, list);
				}
				list.Clear();
				if (list2.Count == 4)
				{
					break;
				}
			}
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM f)
			{
				f.OnSizeChanged();
			});
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00013024 File Offset: 0x00011224
		private List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>> GetMassTransferDataForFormationClass(Formation targetFormation, FormationClass formationClass)
		{
			List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>> list = new List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>>();
			List<OrderOfBattleFormationItemVM> list2 = new List<OrderOfBattleFormationItemVM>();
			List<int> list3 = new List<int>();
			int num = 0;
			for (int i = 0; i < this._allFormations.Count; i++)
			{
				int totalCountOfUnitsInClass = OrderOfBattleUIHelper.GetTotalCountOfUnitsInClass(this._allFormations[i].Formation, formationClass);
				if (totalCountOfUnitsInClass > 0 || this._allFormations[i].Formation == targetFormation)
				{
					list2.Add(this._allFormations[i]);
					list3.Add(totalCountOfUnitsInClass);
					num += totalCountOfUnitsInClass;
				}
			}
			if (list2.Count == 1)
			{
				return list;
			}
			if (num > 0)
			{
				List<int> list4 = new List<int>();
				for (int j = 0; j < list2.Count; j++)
				{
					int item = (list2[j].Formation == targetFormation) ? num : 0;
					list4.Add(item);
				}
				int num2;
				while (list4.Count > 0 && (num2 = list4.Sum()) != num)
				{
					int num3 = num2 - num;
					int num4;
					if (num3 <= 0)
					{
						num4 = list4.IndexOfMin((int c) => c);
					}
					else
					{
						num4 = list4.IndexOfMax((int c) => c);
					}
					List<int> list5 = list4;
					int index = num4;
					list5[index] -= Math.Sign(num3);
				}
				for (int k = 0; k < list4.Count; k++)
				{
					OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = list2[k];
					Team.TroopFilter troopFilter = OrderOfBattleUIHelper.GetTroopFilterForClass(new FormationClass[]
					{
						formationClass
					});
					troopFilter |= OrderOfBattleUIHelper.GetTroopFilterForFormationFilter((from f in orderOfBattleFormationItemVM.FilterItems
					where f.IsActive
					select f.FilterType).ToArray<FormationFilterType>());
					if (troopFilter != (Team.TroopFilter)0)
					{
						Tuple<Formation, int, Team.TroopFilter, List<Agent>> item2 = OrderOfBattleUIHelper.CreateMassTransferData(orderOfBattleFormationItemVM, formationClass, troopFilter, list4[k]);
						list.Add(item2);
					}
				}
			}
			return list;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00013250 File Offset: 0x00011450
		private List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>> GetMassTransferDataForFormation(OrderOfBattleFormationClassVM formationClass)
		{
			List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>> list = new List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>>();
			List<OrderOfBattleFormationClassVM> allFormationClassesWith = this.GetAllFormationClassesWith(formationClass.Class);
			if (allFormationClassesWith.Count == 1)
			{
				return list;
			}
			int num = allFormationClassesWith.Sum((OrderOfBattleFormationClassVM c) => OrderOfBattleUIHelper.GetCountOfRealUnitsInClass(c));
			if (num > 0)
			{
				List<int> list2 = new List<int>();
				for (int i = 0; i < allFormationClassesWith.Count; i++)
				{
					int item = MathF.Ceiling((float)allFormationClassesWith[i].Weight / 100f * (float)num);
					list2.Add(item);
				}
				int num2;
				while (list2.Count > 0 && (num2 = list2.Sum()) != num)
				{
					int num3 = num2 - num;
					int num4;
					if (num3 <= 0)
					{
						num4 = list2.IndexOfMin((int c) => c);
					}
					else
					{
						num4 = list2.IndexOfMax((int c) => c);
					}
					List<int> list3 = list2;
					int index = num4;
					list3[index] -= Math.Sign(num3);
				}
				for (int j = 0; j < list2.Count; j++)
				{
					OrderOfBattleFormationItemVM belongedFormationItem = allFormationClassesWith[j].BelongedFormationItem;
					Team.TroopFilter troopFilter = OrderOfBattleUIHelper.GetTroopFilterForClass((from c in belongedFormationItem.Classes
					where !c.IsUnset
					select c.Class).ToArray<FormationClass>());
					troopFilter |= OrderOfBattleUIHelper.GetTroopFilterForFormationFilter((from f in belongedFormationItem.FilterItems
					where f.IsActive
					select f.FilterType).ToArray<FormationFilterType>());
					if (troopFilter != (Team.TroopFilter)0)
					{
						Tuple<Formation, int, Team.TroopFilter, List<Agent>> item2 = OrderOfBattleUIHelper.CreateMassTransferData(allFormationClassesWith[j], allFormationClassesWith[j].Class, troopFilter, list2[j]);
						list.Add(item2);
					}
				}
			}
			return list;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00013494 File Offset: 0x00011694
		private List<OrderOfBattleFormationClassVM> GetAllFormationClassesWith(FormationClass formationClass)
		{
			List<OrderOfBattleFormationClassVM> list = new List<OrderOfBattleFormationClassVM>();
			if (formationClass >= FormationClass.NumberOfDefaultFormations)
			{
				return list;
			}
			for (int i = 0; i < this._allFormations.Count; i++)
			{
				for (int j = 0; j < this._allFormations[i].Classes.Count; j++)
				{
					if (this._allFormations[i].Classes[j].Class == formationClass)
					{
						list.Add(this._allFormations[i].Classes[j]);
					}
				}
			}
			return list;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00013524 File Offset: 0x00011724
		private void RefreshFormationsWithClass(FormationClass formationClass)
		{
			for (int i = 0; i < this._allFormations.Count; i++)
			{
				for (int j = 0; j < this._allFormations[i].Classes.Count; j++)
				{
					if (this._allFormations[i].Classes[j].Class == formationClass)
					{
						this._allFormations[i].OnSizeChanged();
						break;
					}
				}
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001359C File Offset: 0x0001179C
		private List<Agent> GetLockedAgents()
		{
			List<Agent> list = new List<Agent>();
			foreach (OrderOfBattleFormationItemVM orderOfBattleFormationItemVM in this._allFormations)
			{
				if (orderOfBattleFormationItemVM.Commander.Agent != null)
				{
					list.Add(orderOfBattleFormationItemVM.Commander.Agent);
				}
				foreach (OrderOfBattleHeroItemVM orderOfBattleHeroItemVM in orderOfBattleFormationItemVM.HeroTroops)
				{
					list.Add(orderOfBattleHeroItemVM.Agent);
				}
			}
			return list;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00013650 File Offset: 0x00011850
		private void OnFormationClassChanged(OrderOfBattleFormationClassVM formationClassItem, FormationClass newFormationClass)
		{
			if (!this._isInitialized)
			{
				return;
			}
			List<OrderOfBattleFormationClassVM> previousFormationClasses = new List<OrderOfBattleFormationClassVM>();
			List<OrderOfBattleFormationClassVM> newFormationClasses = new List<OrderOfBattleFormationClassVM>();
			Func<OrderOfBattleFormationClassVM, bool> <>9__5;
			Func<OrderOfBattleFormationClassVM, bool> <>9__6;
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM formation)
			{
				List<OrderOfBattleFormationClassVM> previousFormationClasses = previousFormationClasses;
				IEnumerable<OrderOfBattleFormationClassVM> source = formation.Classes.ToList<OrderOfBattleFormationClassVM>();
				Func<OrderOfBattleFormationClassVM, bool> predicate3;
				if ((predicate3 = <>9__5) == null)
				{
					predicate3 = (<>9__5 = ((OrderOfBattleFormationClassVM fc) => fc.Class == formationClassItem.Class));
				}
				previousFormationClasses.AddRange(source.Where(predicate3));
				List<OrderOfBattleFormationClassVM> newFormationClasses = newFormationClasses;
				IEnumerable<OrderOfBattleFormationClassVM> source2 = formation.Classes.ToList<OrderOfBattleFormationClassVM>();
				Func<OrderOfBattleFormationClassVM, bool> predicate4;
				if ((predicate4 = <>9__6) == null)
				{
					predicate4 = (<>9__6 = ((OrderOfBattleFormationClassVM fc) => fc.Class == newFormationClass));
				}
				newFormationClasses.AddRange(source2.Where(predicate4));
			});
			if (newFormationClasses.Count > 0)
			{
				formationClassItem.Weight = 0;
			}
			else
			{
				this.TransferAllAvailableTroopsToFormation(formationClassItem.BelongedFormationItem, newFormationClass);
				formationClassItem.SetWeightAdjustmentLock(true);
				formationClassItem.Weight = 100;
				formationClassItem.SetWeightAdjustmentLock(false);
			}
			newFormationClasses.Add(formationClassItem);
			previousFormationClasses.ForEach(delegate(OrderOfBattleFormationClassVM fc)
			{
				fc.IsAdjustable = (formationClassItem.Class != FormationClass.NumberOfAllFormations && previousFormationClasses.Count > 2);
			});
			newFormationClasses.ForEach(delegate(OrderOfBattleFormationClassVM fc)
			{
				fc.IsAdjustable = (newFormationClass != FormationClass.NumberOfAllFormations && newFormationClasses.Count > 1);
			});
			List<OrderOfBattleFormationClassVM> allClasses = new List<OrderOfBattleFormationClassVM>();
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM formation)
			{
				allClasses.AddRange(from fc in formation.Classes
				where fc.Class != FormationClass.NumberOfAllFormations
				select fc);
			});
			if (newFormationClass != FormationClass.NumberOfAllFormations || allClasses.Contains(formationClassItem))
			{
				allClasses.Remove(formationClassItem);
				allClasses.Add(new OrderOfBattleFormationClassVM(formationClassItem.BelongedFormationItem, newFormationClass));
			}
			bool oldValue = this._orderController.BackupAndDisableGesturesEnabled();
			IEnumerable<OrderOfBattleFormationItemVM> allFormations = this._allFormations;
			Func<OrderOfBattleFormationItemVM, bool> <>9__8;
			Func<OrderOfBattleFormationItemVM, bool> predicate;
			if ((predicate = <>9__8) == null)
			{
				Func<OrderOfBattleFormationClassVM, bool> <>9__9;
				predicate = (<>9__8 = delegate(OrderOfBattleFormationItemVM f)
				{
					IEnumerable<OrderOfBattleFormationClassVM> classes2 = f.Classes;
					Func<OrderOfBattleFormationClassVM, bool> predicate3;
					if ((predicate3 = <>9__9) == null)
					{
						predicate3 = (<>9__9 = ((OrderOfBattleFormationClassVM c) => c.Class != newFormationClass));
					}
					return classes2.All(predicate3);
				});
			}
			Func<OrderOfBattleFormationClassVM, bool> <>9__10;
			Action<OrderOfBattleFormationItemVM> <>9__11;
			foreach (OrderOfBattleFormationItemVM orderOfBattleFormationItemVM in allFormations.Where(predicate))
			{
				IEnumerable<OrderOfBattleFormationClassVM> classes = orderOfBattleFormationItemVM.Classes;
				Func<OrderOfBattleFormationClassVM, bool> predicate2;
				if ((predicate2 = <>9__10) == null)
				{
					predicate2 = (<>9__10 = ((OrderOfBattleFormationClassVM c) => c.Class == newFormationClass));
				}
				ValueTuple<int, bool, bool> relevantTroopTransferParameters = OrderOfBattleUIHelper.GetRelevantTroopTransferParameters(classes.FirstOrDefault(predicate2));
				if (relevantTroopTransferParameters.Item1 > 0)
				{
					List<OrderOfBattleFormationItemVM> allFormations2 = this._allFormations;
					Action<OrderOfBattleFormationItemVM> action;
					if ((action = <>9__11) == null)
					{
						action = (<>9__11 = delegate(OrderOfBattleFormationItemVM f)
						{
							if (this._orderController.SelectedFormations.Contains(f.Formation))
							{
								this._orderController.DeselectFormation(f.Formation);
							}
						});
					}
					allFormations2.ForEach(action);
					this._orderController.SelectFormation(orderOfBattleFormationItemVM.Formation);
					this._orderController.TransferUnitWithPriorityFunction(formationClassItem.BelongedFormationItem.Formation, relevantTroopTransferParameters.Item1, false, false, false, false, relevantTroopTransferParameters.Item2, relevantTroopTransferParameters.Item3, true, this.GetLockedAgents());
					orderOfBattleFormationItemVM.OnSizeChanged();
					formationClassItem.BelongedFormationItem.OnSizeChanged();
				}
			}
			this._isTroopCountsDirty = true;
			this._isHeroSelectionDirty = true;
			this._isMissingFormationsDirty = true;
			this._orderController.RestoreGesturesEnabled(oldValue);
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM f)
			{
				f.UpdateAdjustable();
			});
			EventManager eventManager = Game.Current.EventManager;
			OrderOfBattleFormationItemVM belongedFormationItem = formationClassItem.BelongedFormationItem;
			eventManager.TriggerEvent<OrderOfBattleFormationClassChangedEvent>(new OrderOfBattleFormationClassChangedEvent((belongedFormationItem != null) ? belongedFormationItem.Formation : null));
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013970 File Offset: 0x00011B70
		private void TransferAllAvailableTroopsToFormation(OrderOfBattleFormationItemVM formation, FormationClass formationClass)
		{
			List<Tuple<Formation, int, Team.TroopFilter, List<Agent>>> massTransferDataForFormationClass = this.GetMassTransferDataForFormationClass(formation.Formation, formationClass);
			if (massTransferDataForFormationClass.Count > 0)
			{
				this._orderController.RearrangeFormationsAccordingToFilters(this._mission.PlayerTeam, massTransferDataForFormationClass);
				this.RefreshFormationsWithClass(formationClass);
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000139B4 File Offset: 0x00011BB4
		private void RefreshMissingFormations()
		{
			if (this.IsPlayerGeneral)
			{
				List<OrderOfBattleFormationClassVM> allClasses = new List<OrderOfBattleFormationClassVM>();
				this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM formation)
				{
					allClasses.AddRange(from fc in formation.Classes
					where fc.Class != FormationClass.NumberOfAllFormations
					select fc);
				});
				bool flag = false;
				using (List<FormationClass>.Enumerator enumerator = this._availableTroopTypes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						FormationClass availableTroopType = enumerator.Current;
						if (allClasses.All((OrderOfBattleFormationClassVM c) => c.Class != availableTroopType))
						{
							if (Mission.Current.IsSiegeBattle)
							{
								if (availableTroopType != FormationClass.HorseArcher && availableTroopType != FormationClass.Cavalry)
								{
									flag = true;
								}
							}
							else
							{
								flag = true;
							}
							if (flag)
							{
								this.MissingFormationsHint.HintText.SetTextVariable("FORMATION_CLASS", availableTroopType.GetLocalizedName());
								this.CanStartMission = false;
								break;
							}
						}
					}
				}
				this.CanStartMission = !flag;
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013AB8 File Offset: 0x00011CB8
		private OrderOfBattleFormationItemVM GetFormationItemAtIndex(int index)
		{
			if (index < this.TotalFormationCount / 2)
			{
				return this.FormationsFirstHalf.ElementAt(index);
			}
			if (index < this.TotalFormationCount)
			{
				return this.FormationsSecondHalf.ElementAt(index - this.TotalFormationCount / 2);
			}
			return null;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00013AF2 File Offset: 0x00011CF2
		private IEnumerable<OrderOfBattleFormationItemVM> GetFormationItemsWithCondition(Func<OrderOfBattleFormationItemVM, bool> condition)
		{
			return this._allFormations.Where(condition);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00013B00 File Offset: 0x00011D00
		private void OnSelectedFormationsChanged()
		{
			if (!this._isInitialized)
			{
				return;
			}
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM f)
			{
				f.IsSelected = false;
			});
			using (List<Formation>.Enumerator enumerator = this._orderController.SelectedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation selectedFormation = enumerator.Current;
					this._allFormations.FirstOrDefault((OrderOfBattleFormationItemVM f) => f.Formation == selectedFormation).IsSelected = true;
				}
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00013BAC File Offset: 0x00011DAC
		private void SelectFormationItem(OrderOfBattleFormationItemVM formationItem)
		{
			formationItem.IsSelected = true;
			this._selectFormationAtIndex(formationItem.Formation.Index);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00013BCC File Offset: 0x00011DCC
		private void DeselectFormationItem(OrderOfBattleFormationItemVM formationItem)
		{
			Formation formation = formationItem.Formation;
			if (formation != null && formation.Index >= 0)
			{
				Mission.Current.PlayerTeam.PlayerOrderController.DeselectFormation(formationItem.Formation);
				Action<int> deselectFormationAtIndex = this._deselectFormationAtIndex;
				if (deselectFormationAtIndex == null)
				{
					return;
				}
				deselectFormationAtIndex(formationItem.Formation.Index);
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013C28 File Offset: 0x00011E28
		public void SelectFormationItemAtIndex(int index)
		{
			this._allFormations.FirstOrDefault((OrderOfBattleFormationItemVM f) => f.Formation.Index == index).IsSelected = true;
			this._selectFormationAtIndex(index);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00013C70 File Offset: 0x00011E70
		public void FocusFormationItemAtIndex(int index)
		{
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM f)
			{
				f.IsBeingFocused = false;
			});
			this._allFormations.FirstOrDefault((OrderOfBattleFormationItemVM f) => f.Formation.Index == index).IsBeingFocused = true;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013CD4 File Offset: 0x00011ED4
		public void DeselectAllFormations()
		{
			foreach (OrderOfBattleFormationItemVM orderOfBattleFormationItemVM in this._allFormations)
			{
				orderOfBattleFormationItemVM.IsSelected = false;
			}
			Action clearFormationSelection = this._clearFormationSelection;
			if (clearFormationSelection == null)
			{
				return;
			}
			clearFormationSelection();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00013D38 File Offset: 0x00011F38
		public void OnUnitDeployed()
		{
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM f)
			{
				if (f != null)
				{
					f.RefreshMarkerWorldPosition();
				}
			});
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013D64 File Offset: 0x00011F64
		public bool OnEscape()
		{
			if (this._allFormations.Any((OrderOfBattleFormationItemVM f) => f.IsSelected))
			{
				this.DeselectAllFormations();
				return true;
			}
			return false;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00013D9C File Offset: 0x00011F9C
		private int GetTroopCountWithFilter(DeploymentFormationClass orderOfBattleFormationClass, FormationFilterType filterType)
		{
			int num = 0;
			List<FormationClass> formationClasses = orderOfBattleFormationClass.GetFormationClasses();
			foreach (OrderOfBattleFormationItemVM orderOfBattleFormationItemVM in this._allFormations)
			{
				List<FormationClass> second = (from c in orderOfBattleFormationItemVM.Classes
				select c.Class into c
				where c != FormationClass.NumberOfAllFormations
				select c).ToList<FormationClass>();
				if (formationClasses.Intersect(second).Any<FormationClass>())
				{
					switch (filterType)
					{
					case FormationFilterType.Shield:
						num += orderOfBattleFormationItemVM.Formation.GetCountOfUnitsWithCondition((Agent a) => a.HasShieldCached);
						break;
					case FormationFilterType.Spear:
						num += orderOfBattleFormationItemVM.Formation.GetCountOfUnitsWithCondition((Agent a) => a.HasSpearCached);
						break;
					case FormationFilterType.Thrown:
						num += orderOfBattleFormationItemVM.Formation.GetCountOfUnitsWithCondition((Agent a) => a.HasThrownCached);
						break;
					case FormationFilterType.Heavy:
						num += orderOfBattleFormationItemVM.Formation.GetCountOfUnitsWithCondition((Agent a) => MissionGameModels.Current.AgentStatCalculateModel.HasHeavyArmor(a));
						break;
					case FormationFilterType.HighTier:
						num += orderOfBattleFormationItemVM.Formation.GetCountOfUnitsWithCondition((Agent a) => a.Character.GetBattleTier() >= 4);
						break;
					case FormationFilterType.LowTier:
						num += orderOfBattleFormationItemVM.Formation.GetCountOfUnitsWithCondition((Agent a) => a.Character.GetBattleTier() <= 3);
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00013FB4 File Offset: 0x000121B4
		protected void ClearFormationItem(OrderOfBattleFormationItemVM formationItem)
		{
			formationItem.FormationClassSelector.SelectedIndex = 0;
			formationItem.UnassignCommander();
			foreach (OrderOfBattleFormationClassVM orderOfBattleFormationClassVM in formationItem.Classes)
			{
				orderOfBattleFormationClassVM.IsLocked = false;
				orderOfBattleFormationClassVM.Weight = 0;
				orderOfBattleFormationClassVM.Class = FormationClass.NumberOfAllFormations;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00014020 File Offset: 0x00012220
		private int GetVisibleTotalTroopCountOfType(FormationClass formationClass)
		{
			return this._visibleTroopTypeCountLookup[formationClass];
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001402E File Offset: 0x0001222E
		private void OnOrderIssued(OrderType orderType, MBReadOnlyList<Formation> appliedFormations, OrderController orderController, params object[] delegateParams)
		{
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM x)
			{
				x.RefreshMarkerWorldPosition();
			});
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001405A File Offset: 0x0001225A
		private void OnHeroesChanged()
		{
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM f)
			{
				f.OnSizeChanged();
				f.UpdateAdjustable();
			});
			this.RefreshWeights();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001408C File Offset: 0x0001228C
		public void ExecuteAutoDeploy()
		{
			if (this.IsPlayerGeneral)
			{
				this._onAutoDeploy();
				this.AfterAutoDeploy();
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000140A8 File Offset: 0x000122A8
		private void AfterAutoDeploy()
		{
			foreach (OrderOfBattleFormationItemVM orderOfBattleFormationItemVM in this._allFormations)
			{
				orderOfBattleFormationItemVM.RefreshFormation(orderOfBattleFormationItemVM.Formation, DeploymentFormationClass.Unset, false);
			}
			this.ClearHeroItemSelection();
			this.ClearAllHeroAssignments();
			this.RefreshWeights();
			this.OnUnitDeployed();
			this._allFormations.ForEach(delegate(OrderOfBattleFormationItemVM f)
			{
				f.UpdateAdjustable();
			});
			this._isMissingFormationsDirty = true;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001414C File Offset: 0x0001234C
		public void ExecuteBeginMission()
		{
			List<ValueTuple<int, List<int>>> currentConfiguration = this.CurrentConfiguration;
			if (currentConfiguration != null)
			{
				currentConfiguration.Clear();
			}
			foreach (OrderOfBattleFormationItemVM orderOfBattleFormationItemVM in this._allFormations)
			{
				List<ValueTuple<int, List<int>>> currentConfiguration2 = this.CurrentConfiguration;
				if (currentConfiguration2 != null)
				{
					currentConfiguration2.Add(new ValueTuple<int, List<int>>(orderOfBattleFormationItemVM.Formation.Index, (from f in orderOfBattleFormationItemVM.ActiveFilterItems
					select (int)f.FilterType).ToList<int>()));
				}
			}
			if (this._bannerBearerLogic != null)
			{
				this._bannerBearerLogic.OnBannerBearersUpdated -= this.OnBannerBearersUpdated;
				this._bannerBearerLogic.OnBannerBearerAgentUpdated -= this.OnBannerAgentUpdated;
			}
			Action onBeginMission = this._onBeginMission;
			if (onBeginMission != null)
			{
				onBeginMission();
			}
			MBInformationManager.HideInformations();
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00014248 File Offset: 0x00012448
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x00014250 File Offset: 0x00012450
		[DataSourceProperty]
		public bool IsPoolAcceptingHeroTroops
		{
			get
			{
				return this._isPoolAcceptingHeroTroops;
			}
			set
			{
				if (value != this._isPoolAcceptingHeroTroops)
				{
					this._isPoolAcceptingHeroTroops = value;
					base.OnPropertyChangedWithValue(value, "IsPoolAcceptingHeroTroops");
				}
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001426E File Offset: 0x0001246E
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x00014276 File Offset: 0x00012476
		[DataSourceProperty]
		public bool CanStartMission
		{
			get
			{
				return this._canStartMission;
			}
			set
			{
				if (value != this._canStartMission)
				{
					this._canStartMission = value;
					base.OnPropertyChangedWithValue(value, "CanStartMission");
				}
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00014294 File Offset: 0x00012494
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0001429C File Offset: 0x0001249C
		[DataSourceProperty]
		public string BeginMissionText
		{
			get
			{
				return this._beginMissionText;
			}
			set
			{
				if (value != this._beginMissionText)
				{
					this._beginMissionText = value;
					base.OnPropertyChangedWithValue<string>(value, "BeginMissionText");
				}
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x000142BF File Offset: 0x000124BF
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x000142C7 File Offset: 0x000124C7
		[DataSourceProperty]
		public bool HasSelectedHeroes
		{
			get
			{
				return this._hasSelectedHeroes;
			}
			set
			{
				if (value != this._hasSelectedHeroes)
				{
					this._hasSelectedHeroes = value;
					base.OnPropertyChangedWithValue(value, "HasSelectedHeroes");
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x000142E5 File Offset: 0x000124E5
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x000142ED File Offset: 0x000124ED
		[DataSourceProperty]
		public MBBindingList<OrderOfBattleFormationItemVM> FormationsFirstHalf
		{
			get
			{
				return this._formationsFirstHalf;
			}
			set
			{
				if (value != this._formationsFirstHalf)
				{
					this._formationsFirstHalf = value;
					base.OnPropertyChangedWithValue<MBBindingList<OrderOfBattleFormationItemVM>>(value, "FormationsFirstHalf");
				}
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001430B File Offset: 0x0001250B
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00014313 File Offset: 0x00012513
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00014331 File Offset: 0x00012531
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00014339 File Offset: 0x00012539
		[DataSourceProperty]
		public bool AreCameraControlsEnabled
		{
			get
			{
				return this._areCameraControlsEnabled;
			}
			set
			{
				if (value != this._areCameraControlsEnabled)
				{
					this._areCameraControlsEnabled = value;
					base.OnPropertyChangedWithValue(value, "AreCameraControlsEnabled");
				}
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00014357 File Offset: 0x00012557
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0001435F File Offset: 0x0001255F
		[DataSourceProperty]
		public bool IsPlayerGeneral
		{
			get
			{
				return this._isPlayerGeneral;
			}
			set
			{
				if (value != this._isPlayerGeneral)
				{
					this._isPlayerGeneral = value;
					base.OnPropertyChangedWithValue(value, "IsPlayerGeneral");
				}
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0001437D File Offset: 0x0001257D
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00014385 File Offset: 0x00012585
		[DataSourceProperty]
		public bool IsPoolAcceptingCommander
		{
			get
			{
				return this._isPoolAcceptingCommander;
			}
			set
			{
				if (value != this._isPoolAcceptingCommander)
				{
					this._isPoolAcceptingCommander = value;
					base.OnPropertyChangedWithValue(value, "IsPoolAcceptingCommander");
				}
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x000143A3 File Offset: 0x000125A3
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x000143AB File Offset: 0x000125AB
		[DataSourceProperty]
		public int SelectedHeroCount
		{
			get
			{
				return this._selectedHeroCount;
			}
			set
			{
				if (value != this._selectedHeroCount)
				{
					this._selectedHeroCount = value;
					base.OnPropertyChangedWithValue(value, "SelectedHeroCount");
				}
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x000143C9 File Offset: 0x000125C9
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x000143D1 File Offset: 0x000125D1
		[DataSourceProperty]
		public HintViewModel ClearSelectionHint
		{
			get
			{
				return this._clearSelectionHint;
			}
			set
			{
				if (value != this._clearSelectionHint)
				{
					this._clearSelectionHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ClearSelectionHint");
				}
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x000143EF File Offset: 0x000125EF
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x000143F7 File Offset: 0x000125F7
		[DataSourceProperty]
		public string AutoDeployText
		{
			get
			{
				return this._autoDeployText;
			}
			set
			{
				if (value != this._autoDeployText)
				{
					this._autoDeployText = value;
					base.OnPropertyChangedWithValue<string>(value, "AutoDeployText");
				}
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0001441A File Offset: 0x0001261A
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00014422 File Offset: 0x00012622
		[DataSourceProperty]
		public HintViewModel SelectAllHint
		{
			get
			{
				return this._selectAllHint;
			}
			set
			{
				if (value != this._selectAllHint)
				{
					this._selectAllHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "SelectAllHint");
				}
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00014440 File Offset: 0x00012640
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00014448 File Offset: 0x00012648
		[DataSourceProperty]
		public HintViewModel MissingFormationsHint
		{
			get
			{
				return this._missingFormationsHint;
			}
			set
			{
				if (value != this._missingFormationsHint)
				{
					this._missingFormationsHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "MissingFormationsHint");
				}
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00014466 File Offset: 0x00012666
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0001446E File Offset: 0x0001266E
		[DataSourceProperty]
		public OrderOfBattleHeroItemVM LastSelectedHeroItem
		{
			get
			{
				return this._lastSelectedHeroItem;
			}
			set
			{
				if (value != this._lastSelectedHeroItem)
				{
					this._lastSelectedHeroItem = value;
					base.OnPropertyChangedWithValue<OrderOfBattleHeroItemVM>(value, "LastSelectedHeroItem");
				}
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0001448C File Offset: 0x0001268C
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00014494 File Offset: 0x00012694
		[DataSourceProperty]
		public MBBindingList<OrderOfBattleFormationItemVM> FormationsSecondHalf
		{
			get
			{
				return this._formationsSecondHalf;
			}
			set
			{
				if (value != this._formationsSecondHalf)
				{
					this._formationsSecondHalf = value;
					base.OnPropertyChangedWithValue<MBBindingList<OrderOfBattleFormationItemVM>>(value, "FormationsSecondHalf");
				}
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000144B2 File Offset: 0x000126B2
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x000144BA File Offset: 0x000126BA
		[DataSourceProperty]
		public MBBindingList<OrderOfBattleHeroItemVM> UnassignedHeroes
		{
			get
			{
				return this._unassignedHeroes;
			}
			set
			{
				if (value != this._unassignedHeroes)
				{
					this._unassignedHeroes = value;
					base.OnPropertyChangedWithValue<MBBindingList<OrderOfBattleHeroItemVM>>(value, "UnassignedHeroes");
				}
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000144D8 File Offset: 0x000126D8
		private void OnTutorialNotificationElementIDChange(TutorialNotificationElementChangeEvent obj)
		{
			if (obj.NewNotificationElementID != this._latestTutorialElementID)
			{
				if (this._latestTutorialElementID != null)
				{
					if (this._isAssignCaptainHighlightApplied)
					{
						this.SetHighlightEmptyCaptainFormations(false);
						this.SetHighlightMainAgentPortait(false);
						this._isAssignCaptainHighlightApplied = false;
					}
					if (this._isCreateFormationHighlightApplied)
					{
						this.SetHighlightFormationTypeSelection(false);
						this.SetHighlightFormationWeights(false);
						this._isCreateFormationHighlightApplied = false;
					}
				}
				this._latestTutorialElementID = obj.NewNotificationElementID;
				if (this._latestTutorialElementID != null)
				{
					if (this._latestTutorialElementID == "AssignCaptain" && !this._isAssignCaptainHighlightApplied)
					{
						this.SetHighlightEmptyCaptainFormations(true);
						this.SetHighlightMainAgentPortait(true);
						this._isAssignCaptainHighlightApplied = true;
					}
					if (this._latestTutorialElementID == "CreateFormation" && !this._isCreateFormationHighlightApplied)
					{
						this.SetHighlightFormationTypeSelection(true);
						this.SetHighlightFormationWeights(true);
						this._isCreateFormationHighlightApplied = true;
					}
				}
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000145B0 File Offset: 0x000127B0
		private void SetHighlightMainAgentPortait(bool state)
		{
			for (int i = 0; i < this._allHeroes.Count; i++)
			{
				OrderOfBattleHeroItemVM orderOfBattleHeroItemVM = this._allHeroes[i];
				if (orderOfBattleHeroItemVM.Agent.IsMainAgent)
				{
					orderOfBattleHeroItemVM.IsHighlightActive = state;
					return;
				}
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000145F8 File Offset: 0x000127F8
		private void SetHighlightEmptyCaptainFormations(bool state)
		{
			for (int i = 0; i < this._allFormations.Count; i++)
			{
				OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = this._allFormations[i];
				if (!state || (!orderOfBattleFormationItemVM.HasCommander && orderOfBattleFormationItemVM.HasFormation))
				{
					orderOfBattleFormationItemVM.IsCaptainSlotHighlightActive = state;
				}
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00014644 File Offset: 0x00012844
		private void SetHighlightFormationTypeSelection(bool state)
		{
			for (int i = 0; i < this._allFormations.Count; i++)
			{
				OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = this._allFormations[i];
				if (!state || orderOfBattleFormationItemVM.IsAdjustable)
				{
					this._allFormations[i].IsTypeSelectionHighlightActive = state;
				}
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00014694 File Offset: 0x00012894
		private void SetHighlightFormationWeights(bool state)
		{
			for (int i = 0; i < this._allFormations.Count; i++)
			{
				OrderOfBattleFormationItemVM orderOfBattleFormationItemVM = this._allFormations[i];
				for (int j = 0; j < orderOfBattleFormationItemVM.Classes.Count; j++)
				{
					orderOfBattleFormationItemVM.Classes[j].IsWeightHighlightActive = state;
				}
			}
		}

		// Token: 0x040001F9 RID: 505
		private readonly TextObject _bannerText = new TextObject("{=FvYhaE3z}Banner", null);

		// Token: 0x040001FA RID: 506
		private readonly TextObject _bannerEffectText = new TextObject("{=zjcZZgUY}Banner Effect", null);

		// Token: 0x040001FB RID: 507
		private readonly TextObject _noBannerEquippedText = new TextObject("{=suyl7WWa}No banner equipped", null);

		// Token: 0x040001FC RID: 508
		private readonly TextObject _missingFormationsHintText = new TextObject("{=2AGvFYk9}To start the mission, you need to have at least one formation with {FORMATION_CLASS} class.", null);

		// Token: 0x040001FD RID: 509
		private readonly TextObject _selectAllHintText = new TextObject("{=YwbymaBc}Select all heroes", null);

		// Token: 0x040001FE RID: 510
		private bool _isSaving;

		// Token: 0x040001FF RID: 511
		private readonly TextObject _clearSelectionHintText = new TextObject("{=Sbb8YcJM}Deselect all selected heroes", null);

		// Token: 0x04000200 RID: 512
		private Dictionary<FormationClass, int> _visibleTroopTypeCountLookup;

		// Token: 0x04000201 RID: 513
		private bool _isUnitDeployRefreshed;

		// Token: 0x04000202 RID: 514
		private Action<int> _selectFormationAtIndex;

		// Token: 0x04000203 RID: 515
		private readonly List<OrderOfBattleHeroItemVM> _selectedHeroes;

		// Token: 0x04000204 RID: 516
		private Action<int> _deselectFormationAtIndex;

		// Token: 0x04000205 RID: 517
		protected readonly List<OrderOfBattleHeroItemVM> _allHeroes;

		// Token: 0x04000206 RID: 518
		private List<FormationClass> _availableTroopTypes;

		// Token: 0x04000207 RID: 519
		private bool _isInitialized;

		// Token: 0x0400020A RID: 522
		protected List<OrderOfBattleFormationItemVM> _allFormations;

		// Token: 0x0400020B RID: 523
		private Action _clearFormationSelection;

		// Token: 0x0400020C RID: 524
		private Action _onAutoDeploy;

		// Token: 0x0400020D RID: 525
		private Action _onBeginMission;

		// Token: 0x0400020E RID: 526
		private Mission _mission;

		// Token: 0x0400020F RID: 527
		private Camera _missionCamera;

		// Token: 0x04000210 RID: 528
		private BannerBearerLogic _bannerBearerLogic;

		// Token: 0x04000211 RID: 529
		private OrderController _orderController;

		// Token: 0x04000212 RID: 530
		private bool _isMissingFormationsDirty;

		// Token: 0x04000213 RID: 531
		private bool _isHeroSelectionDirty;

		// Token: 0x04000214 RID: 532
		private bool _isTroopCountsDirty;

		// Token: 0x04000215 RID: 533
		private OrderOfBattleFormationItemVM _lastEnabledClassSelection;

		// Token: 0x04000216 RID: 534
		private OrderOfBattleFormationItemVM _lastEnabledFilterSelection;

		// Token: 0x04000217 RID: 535
		private bool _isEnabled;

		// Token: 0x04000218 RID: 536
		private bool _isPlayerGeneral;

		// Token: 0x04000219 RID: 537
		private bool _areCameraControlsEnabled;

		// Token: 0x0400021A RID: 538
		private bool _canStartMission = true;

		// Token: 0x0400021B RID: 539
		private bool _isPoolAcceptingCommander;

		// Token: 0x0400021C RID: 540
		private bool _isPoolAcceptingHeroTroops;

		// Token: 0x0400021D RID: 541
		private string _beginMissionText;

		// Token: 0x0400021E RID: 542
		private bool _hasSelectedHeroes;

		// Token: 0x0400021F RID: 543
		private int _selectedHeroCount;

		// Token: 0x04000220 RID: 544
		private MBBindingList<OrderOfBattleFormationItemVM> _formationsSecondHalf;

		// Token: 0x04000221 RID: 545
		private HintViewModel _missingFormationsHint;

		// Token: 0x04000222 RID: 546
		private HintViewModel _selectAllHint;

		// Token: 0x04000223 RID: 547
		private HintViewModel _clearSelectionHint;

		// Token: 0x04000224 RID: 548
		private string _autoDeployText;

		// Token: 0x04000225 RID: 549
		private MBBindingList<OrderOfBattleHeroItemVM> _unassignedHeroes;

		// Token: 0x04000226 RID: 550
		private OrderOfBattleHeroItemVM _lastSelectedHeroItem;

		// Token: 0x04000227 RID: 551
		private MBBindingList<OrderOfBattleFormationItemVM> _formationsFirstHalf;

		// Token: 0x04000228 RID: 552
		private string _latestTutorialElementID;

		// Token: 0x04000229 RID: 553
		private const string _assignCaptainHighlightID = "AssignCaptain";

		// Token: 0x0400022A RID: 554
		private const string _createFormationHighlightID = "CreateFormation";

		// Token: 0x0400022B RID: 555
		private bool _isAssignCaptainHighlightApplied;

		// Token: 0x0400022C RID: 556
		private bool _isCreateFormationHighlightApplied;
	}
}
