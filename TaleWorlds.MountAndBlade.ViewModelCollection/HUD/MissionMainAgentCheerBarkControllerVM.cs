using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Diamond.Cosmetics;
using TaleWorlds.MountAndBlade.Diamond.Cosmetics.CosmeticTypes;
using TaleWorlds.MountAndBlade.Diamond.Lobby;
using TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD
{
	// Token: 0x02000044 RID: 68
	public class MissionMainAgentCheerBarkControllerVM : ViewModel
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x00017984 File Offset: 0x00015B84
		public MissionMainAgentCheerBarkControllerVM(Action<int> onSelectCheer, Action<int> onSelectBark)
		{
			this._onSelectCheer = onSelectCheer;
			this._onSelectBark = onSelectBark;
			this.Nodes = new MBBindingList<CheerBarkNodeItemVM>();
			if (GameNetwork.IsMultiplayer)
			{
				this._ownedTauntCosmetics = NetworkMain.GameClient.OwnedCosmetics.ToList<string>();
				this.UpdatePlayerTauntIndices();
			}
			CheerBarkNodeItemVM.OnSelection += this.OnNodeFocused;
			CheerBarkNodeItemVM.OnNodeFocused += this.OnNodeTooltipToggled;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000179F4 File Offset: 0x00015BF4
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.Nodes.ApplyActionOnAllItems(delegate(CheerBarkNodeItemVM n)
			{
				n.OnFinalize();
			});
			CheerBarkNodeItemVM.OnSelection -= this.OnNodeFocused;
			CheerBarkNodeItemVM.OnNodeFocused -= this.OnNodeTooltipToggled;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00017A54 File Offset: 0x00015C54
		private void PopulateList()
		{
			bool isClient = GameNetwork.IsClient;
			this.IsNodesCategories = isClient;
			this.Nodes.Clear();
			GameKeyContext category = HotKeyManager.GetCategory("CombatHotKeyCategory");
			HotKey hotKey = category.GetHotKey("CheerBarkCloseMenu");
			SkinVoiceManager.SkinVoiceType[] mpBarks = SkinVoiceManager.VoiceType.MpBarks;
			if (isClient)
			{
				HotKey hotKey2 = category.GetHotKey("CheerBarkSelectFirstCategory");
				CheerBarkNodeItemVM cheerBarkNodeItemVM = new CheerBarkNodeItemVM(new TextObject("{=KxH4VVU3}Taunt", null), "cheer", hotKey2, false, TauntUsageManager.TauntUsage.TauntUsageFlag.None);
				this.Nodes.Add(cheerBarkNodeItemVM);
				TauntCosmeticElement[] array = new TauntCosmeticElement[TauntCosmeticElement.MaxNumberOfTaunts];
				foreach (TauntIndexData tauntIndexData in this._playerTauntsWithIndices)
				{
					string tauntId = tauntIndexData.TauntId;
					int tauntIndex = tauntIndexData.TauntIndex;
					TauntCosmeticElement tauntCosmeticElement = CosmeticsManager.GetCosmeticElement(tauntId) as TauntCosmeticElement;
					if (!tauntCosmeticElement.IsFree && !this._ownedTauntCosmetics.Contains(tauntId))
					{
						Debug.FailedAssert("Taunt list have invalid taunt: " + tauntId, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\HUD\\MissionMainAgentCheerBarkControllerVM.cs", "PopulateList", 85);
					}
					else if (tauntIndex >= 0 && tauntIndex < TauntCosmeticElement.MaxNumberOfTaunts)
					{
						array[tauntIndex] = tauntCosmeticElement;
					}
				}
				for (int i = 0; i < array.Length; i++)
				{
					TauntCosmeticElement tauntCosmeticElement2 = array[i];
					if (tauntCosmeticElement2 != null)
					{
						int indexOfAction = TauntUsageManager.GetIndexOfAction(tauntCosmeticElement2.Id);
						TauntUsageManager.TauntUsage.TauntUsageFlag actionNotUsableReason = CosmeticsManagerHelper.GetActionNotUsableReason(Agent.Main, indexOfAction);
						cheerBarkNodeItemVM.AddSubNode(new CheerBarkNodeItemVM(tauntCosmeticElement2.Id, new TextObject("{=!}" + tauntCosmeticElement2.Name, null), tauntCosmeticElement2.Id, this.GetCheerShortcut(i), true, actionNotUsableReason));
					}
					else
					{
						cheerBarkNodeItemVM.AddSubNode(new CheerBarkNodeItemVM(string.Empty, TextObject.Empty, string.Empty, null, true, TauntUsageManager.TauntUsage.TauntUsageFlag.None));
					}
				}
				HotKey hotKey3 = category.GetHotKey("CheerBarkSelectSecondCategory");
				CheerBarkNodeItemVM cheerBarkNodeItemVM2 = new CheerBarkNodeItemVM(new TextObject("{=5Xoilj6r}Shout", null), "bark", hotKey3, false, TauntUsageManager.TauntUsage.TauntUsageFlag.None);
				this.Nodes.Add(cheerBarkNodeItemVM2);
				cheerBarkNodeItemVM2.AddSubNode(new CheerBarkNodeItemVM(new TextObject("{=koX9okuG}None", null), "none", hotKey, true, TauntUsageManager.TauntUsage.TauntUsageFlag.None));
				for (int j = 0; j < mpBarks.Length; j++)
				{
					cheerBarkNodeItemVM2.AddSubNode(new CheerBarkNodeItemVM(mpBarks[j].GetName(), "bark" + j, this.GetCheerShortcut(j), true, TauntUsageManager.TauntUsage.TauntUsageFlag.None));
				}
			}
			else
			{
				ActionIndexCache[] array2 = Agent.DefaultTauntActions.ToArray<ActionIndexCache>();
				this.Nodes.Add(new CheerBarkNodeItemVM(new TextObject("{=koX9okuG}None", null), "none", hotKey, true, TauntUsageManager.TauntUsage.TauntUsageFlag.None));
				for (int k = 0; k < array2.Length; k++)
				{
					this.Nodes.Add(new CheerBarkNodeItemVM(new TextObject("{=!}" + (k + 1).ToString(), null), array2[k].Name, this.GetCheerShortcut(k), true, TauntUsageManager.TauntUsage.TauntUsageFlag.None));
				}
			}
			this.DisabledReasonText = string.Empty;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00017D54 File Offset: 0x00015F54
		private void UpdatePlayerTauntIndices()
		{
			LobbyClient gameClient = NetworkMain.GameClient;
			if (((gameClient != null) ? gameClient.PlayerData : null) != null)
			{
				string playerId = NetworkMain.GameClient.PlayerData.UserId.ToString();
				this._playerTauntsWithIndices = MultiplayerLocalDataManager.Instance.TauntSlotData.GetTauntIndicesForPlayer(playerId);
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00017DA4 File Offset: 0x00015FA4
		private HotKey GetCheerShortcut(int cheerIndex)
		{
			GameKeyContext category = HotKeyManager.GetCategory("CombatHotKeyCategory");
			switch (cheerIndex)
			{
			case 0:
				return category.GetHotKey("CheerBarkItem1");
			case 1:
				return category.GetHotKey("CheerBarkItem2");
			case 2:
				return category.GetHotKey("CheerBarkItem3");
			case 3:
				return category.GetHotKey("CheerBarkItem4");
			default:
				return null;
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00017E08 File Offset: 0x00016008
		public void SelectItem(int itemIndex, int subNodeIndex = -1)
		{
			if (subNodeIndex == -1)
			{
				for (int i = 0; i < this.Nodes.Count; i++)
				{
					this.Nodes[i].IsSelected = (itemIndex == i);
				}
				return;
			}
			if (itemIndex >= 0 && itemIndex < this.Nodes.Count)
			{
				for (int j = 0; j < this.Nodes[itemIndex].SubNodes.Count; j++)
				{
					this.Nodes[itemIndex].SubNodes[j].IsSelected = (subNodeIndex == j);
				}
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00017E98 File Offset: 0x00016098
		public void OnCancelHoldController()
		{
			this.IsActive = false;
			this.Nodes.ApplyActionOnAllItems(delegate(CheerBarkNodeItemVM c)
			{
				c.IsSelected = false;
			});
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00017ECC File Offset: 0x000160CC
		public void OnSelectControllerToggle(bool isActive)
		{
			this.FocusedCheerText = "";
			this.SelectedNodeText = "";
			if (!isActive)
			{
				CheerBarkNodeItemVM cheerBarkNodeItemVM = this.Nodes.FirstOrDefault((CheerBarkNodeItemVM c) => c.IsSelected);
				if (cheerBarkNodeItemVM != null)
				{
					if (this.IsNodesCategories)
					{
						bool flag = cheerBarkNodeItemVM.TypeAsString == "bark";
						CheerBarkNodeItemVM cheerBarkNodeItemVM2;
						if (cheerBarkNodeItemVM == null)
						{
							cheerBarkNodeItemVM2 = null;
						}
						else
						{
							cheerBarkNodeItemVM2 = cheerBarkNodeItemVM.SubNodes.FirstOrDefault((CheerBarkNodeItemVM c) => c.IsSelected);
						}
						CheerBarkNodeItemVM cheerBarkNodeItemVM3 = cheerBarkNodeItemVM2;
						if (cheerBarkNodeItemVM3 != null && cheerBarkNodeItemVM3.TypeAsString != "none")
						{
							if (flag)
							{
								Action<int> onSelectBark = this._onSelectBark;
								if (onSelectBark != null)
								{
									onSelectBark(cheerBarkNodeItemVM.SubNodes.IndexOf(cheerBarkNodeItemVM3) - 1);
								}
							}
							else
							{
								int indexOfAction = TauntUsageManager.GetIndexOfAction(cheerBarkNodeItemVM3.TypeAsString);
								Action<int> onSelectCheer = this._onSelectCheer;
								if (onSelectCheer != null)
								{
									onSelectCheer(indexOfAction);
								}
							}
						}
					}
					else if (cheerBarkNodeItemVM.TypeAsString != "none")
					{
						int num = TauntUsageManager.GetIndexOfAction(cheerBarkNodeItemVM.TypeAsString);
						if (num == -1)
						{
							ActionIndexCache[] defaultTauntActions = Agent.DefaultTauntActions;
							for (int i = 0; i < defaultTauntActions.Length; i++)
							{
								string name = defaultTauntActions[i].Name;
								if (cheerBarkNodeItemVM.TypeAsString == name)
								{
									num = i;
									break;
								}
							}
						}
						Action<int> onSelectCheer2 = this._onSelectCheer;
						if (onSelectCheer2 != null)
						{
							onSelectCheer2(num);
						}
					}
				}
				this.Nodes.ApplyActionOnAllItems(delegate(CheerBarkNodeItemVM n)
				{
					n.IsSelected = false;
				});
			}
			this.IsActive = isActive;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001807C File Offset: 0x0001627C
		public void OnNodeFocused(CheerBarkNodeItemVM focusedNode)
		{
			string selectedNodeText = ((focusedNode != null) ? focusedNode.CheerNameText : null) ?? string.Empty;
			if (this.IsNodesCategories)
			{
				bool flag = focusedNode != null && focusedNode.TypeAsString.Contains("bark");
				string typeId = flag ? "bark" : "cheer";
				this.Nodes.First((CheerBarkNodeItemVM c) => c.TypeAsString == typeId).SelectedNodeText = selectedNodeText;
				return;
			}
			this.SelectedNodeText = selectedNodeText;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000180FE File Offset: 0x000162FE
		public void OnNodeTooltipToggled(CheerBarkNodeItemVM node)
		{
			if (node != null && node.TauntUsageDisabledReason != TauntUsageManager.TauntUsage.TauntUsageFlag.None)
			{
				this.DisabledReasonText = TauntUsageManager.GetActionDisabledReasonText(node.TauntUsageDisabledReason);
				return;
			}
			this.DisabledReasonText = string.Empty;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00018128 File Offset: 0x00016328
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00018130 File Offset: 0x00016330
		[DataSourceProperty]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChangedWithValue(value, "IsActive");
					if (this._isActive)
					{
						this.PopulateList();
					}
				}
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001815C File Offset: 0x0001635C
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00018164 File Offset: 0x00016364
		[DataSourceProperty]
		public string SelectText
		{
			get
			{
				return this._selectText;
			}
			set
			{
				if (value != this._selectText)
				{
					this._selectText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectText");
				}
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00018187 File Offset: 0x00016387
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0001818F File Offset: 0x0001638F
		[DataSourceProperty]
		public string FocusedCheerText
		{
			get
			{
				return this._focusedCheerText;
			}
			set
			{
				if (value != this._focusedCheerText)
				{
					this._focusedCheerText = value;
					base.OnPropertyChangedWithValue<string>(value, "FocusedCheerText");
				}
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x000181B2 File Offset: 0x000163B2
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x000181BA File Offset: 0x000163BA
		[DataSourceProperty]
		public string DisabledReasonText
		{
			get
			{
				return this._disabledReasonText;
			}
			set
			{
				if (value != this._disabledReasonText)
				{
					this._disabledReasonText = value;
					base.OnPropertyChangedWithValue<string>(value, "DisabledReasonText");
				}
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000181DD File Offset: 0x000163DD
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x000181E5 File Offset: 0x000163E5
		[DataSourceProperty]
		public string SelectedNodeText
		{
			get
			{
				return this._selectedNodeText;
			}
			set
			{
				if (value != this._selectedNodeText)
				{
					this._selectedNodeText = value;
					base.OnPropertyChangedWithValue<string>(value, "SelectedNodeText");
				}
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00018208 File Offset: 0x00016408
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00018210 File Offset: 0x00016410
		[DataSourceProperty]
		public bool IsNodesCategories
		{
			get
			{
				return this._isNodesCategories;
			}
			set
			{
				if (value != this._isNodesCategories)
				{
					this._isNodesCategories = value;
					base.OnPropertyChangedWithValue(value, "IsNodesCategories");
				}
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0001822E File Offset: 0x0001642E
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x00018236 File Offset: 0x00016436
		[DataSourceProperty]
		public MBBindingList<CheerBarkNodeItemVM> Nodes
		{
			get
			{
				return this._nodes;
			}
			set
			{
				if (value != this._nodes)
				{
					this._nodes = value;
					base.OnPropertyChangedWithValue<MBBindingList<CheerBarkNodeItemVM>>(value, "Nodes");
				}
			}
		}

		// Token: 0x040002B8 RID: 696
		private const string CheerId = "cheer";

		// Token: 0x040002B9 RID: 697
		private const string BarkId = "bark";

		// Token: 0x040002BA RID: 698
		private const string NoneId = "none";

		// Token: 0x040002BB RID: 699
		private readonly Action<int> _onSelectCheer;

		// Token: 0x040002BC RID: 700
		private readonly Action<int> _onSelectBark;

		// Token: 0x040002BD RID: 701
		private List<string> _ownedTauntCosmetics;

		// Token: 0x040002BE RID: 702
		private IEnumerable<TauntIndexData> _playerTauntsWithIndices;

		// Token: 0x040002BF RID: 703
		private bool _isActive;

		// Token: 0x040002C0 RID: 704
		private bool _isNodesCategories;

		// Token: 0x040002C1 RID: 705
		private string _disabledReasonText;

		// Token: 0x040002C2 RID: 706
		private string _selectedNodeText;

		// Token: 0x040002C3 RID: 707
		private string _selectText;

		// Token: 0x040002C4 RID: 708
		private string _focusedCheerText;

		// Token: 0x040002C5 RID: 709
		private MBBindingList<CheerBarkNodeItemVM> _nodes;
	}
}
