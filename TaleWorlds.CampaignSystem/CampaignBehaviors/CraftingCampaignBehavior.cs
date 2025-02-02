using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.CraftingSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000383 RID: 899
	public class CraftingCampaignBehavior : CampaignBehaviorBase, ICraftingCampaignBehavior, ICampaignBehavior
	{
		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000E4C8A File Offset: 0x000E2E8A
		public IReadOnlyDictionary<Town, CraftingCampaignBehavior.CraftingOrderSlots> CraftingOrders
		{
			get
			{
				return this._craftingOrders;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06003520 RID: 13600 RVA: 0x000E4C92 File Offset: 0x000E2E92
		public IReadOnlyCollection<WeaponDesign> CraftingHistory
		{
			get
			{
				return this._craftingHistory;
			}
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000E4C9C File Offset: 0x000E2E9C
		public override void SyncData(IDataStore dataStore)
		{
			dataStore.SyncData<ItemObject>("_latestCraftedItem", ref this._latestCraftedItem);
			dataStore.SyncData<Dictionary<ItemObject, CraftingCampaignBehavior.CraftedItemInitializationData>>("_craftedItemDictionary", ref this._craftedItemDictionary);
			dataStore.SyncData<Dictionary<Hero, CraftingCampaignBehavior.HeroCraftingRecord>>("_heroCraftingRecordsNew", ref this._heroCraftingRecords);
			dataStore.SyncData<Dictionary<Town, CraftingCampaignBehavior.CraftingOrderSlots>>("_craftingOrders", ref this._craftingOrders);
			dataStore.SyncData<List<WeaponDesign>>("_craftingHistory", ref this._craftingHistory);
			dataStore.SyncData<Dictionary<CraftingTemplate, List<CraftingPiece>>>("_openedPartsDictionary", ref this._openedPartsDictionary);
			dataStore.SyncData<Dictionary<CraftingTemplate, float>>("_openNewPartXpDictionary", ref this._openNewPartXpDictionary);
			if (dataStore.IsLoading && MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("e1.8.0", 45697))
			{
				List<CraftingPiece> list = new List<CraftingPiece>();
				dataStore.SyncData<List<CraftingPiece>>("_openedParts", ref list);
				if (list != null)
				{
					this._openedPartsDictionary = new Dictionary<CraftingTemplate, List<CraftingPiece>>();
					foreach (CraftingTemplate craftingTemplate in CraftingTemplate.All)
					{
						this._openedPartsDictionary.Add(craftingTemplate, new List<CraftingPiece>());
						foreach (CraftingPiece item in list)
						{
							if (craftingTemplate.Pieces.Contains(item) && !this._openedPartsDictionary[craftingTemplate].Contains(item))
							{
								this._openedPartsDictionary[craftingTemplate].Add(item);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000E4E40 File Offset: 0x000E3040
		public override void RegisterEvents()
		{
			CampaignEvents.OnNewGameCreatedPartialFollowUpEndEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnNewGameCreatedPartialFollowUpEnd));
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
			CampaignEvents.OnNewItemCraftedEvent.AddNonSerializedListener(this, new Action<ItemObject, ItemModifier, bool>(this.OnNewItemCrafted));
			CampaignEvents.HourlyTickEvent.AddNonSerializedListener(this, new Action(this.HourlyTick));
			CampaignEvents.DailyTickSettlementEvent.AddNonSerializedListener(this, new Action<Settlement>(this.DailyTickSettlement));
			CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(this.DailyTick));
			CampaignEvents.HeroKilledEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnHeroKilled));
			CampaignEvents.OnGameLoadedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnGameLoaded));
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000E4F08 File Offset: 0x000E3108
		private void OnNewGameCreatedPartialFollowUpEnd(CampaignGameStarter starter)
		{
			this.InitializeLists();
			MBList<Hero> mblist = new MBList<Hero>();
			foreach (Town town in Town.AllTowns)
			{
				Settlement settlement = town.Settlement;
				mblist.AddRange(settlement.HeroesWithoutParty);
				foreach (MobileParty mobileParty in settlement.Parties)
				{
					if (mobileParty.LeaderHero != null && !mobileParty.IsMainParty)
					{
						mblist.Add(mobileParty.LeaderHero);
					}
				}
				if (mblist.Count > 0)
				{
					for (int i = 0; i < 6; i++)
					{
						if (this.CraftingOrders[settlement.Town].GetAvailableSlot() > -1)
						{
							this.CreateTownOrder(mblist.GetRandomElement<Hero>(), i);
						}
					}
				}
				mblist.Clear();
			}
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000E5018 File Offset: 0x000E3218
		private void DailyTickSettlement(Settlement settlement)
		{
			if (settlement.IsTown && this.CraftingOrders[settlement.Town].IsThereAvailableSlot())
			{
				List<Hero> list = new List<Hero>();
				list.AddRange(settlement.HeroesWithoutParty);
				foreach (MobileParty mobileParty in settlement.Parties)
				{
					if (mobileParty.LeaderHero != null && !mobileParty.IsMainParty)
					{
						list.Add(mobileParty.LeaderHero);
					}
				}
				foreach (Hero hero in list)
				{
					if (hero != Hero.MainHero && MBRandom.RandomFloat <= 0.05f)
					{
						int availableSlot = this.CraftingOrders[settlement.Town].GetAvailableSlot();
						if (availableSlot > -1)
						{
							this.CreateTownOrder(hero, availableSlot);
						}
					}
				}
				list = null;
			}
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000E5130 File Offset: 0x000E3330
		private void DailyTick()
		{
			foreach (KeyValuePair<Town, CraftingCampaignBehavior.CraftingOrderSlots> keyValuePair in this.CraftingOrders)
			{
				foreach (CraftingOrder craftingOrder in keyValuePair.Value.Slots)
				{
					if (craftingOrder != null && MBRandom.RandomFloat <= 0.05f)
					{
						this.ReplaceCraftingOrder(keyValuePair.Key, craftingOrder);
					}
				}
			}
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000E51B8 File Offset: 0x000E33B8
		private void HourlyTick()
		{
			foreach (KeyValuePair<Hero, CraftingCampaignBehavior.HeroCraftingRecord> keyValuePair in this._heroCraftingRecords)
			{
				int maxHeroCraftingStamina = this.GetMaxHeroCraftingStamina(keyValuePair.Key);
				if (keyValuePair.Value.CraftingStamina < maxHeroCraftingStamina)
				{
					Hero key = keyValuePair.Key;
					if (key.CurrentSettlement != null)
					{
						keyValuePair.Value.CraftingStamina = MathF.Min(maxHeroCraftingStamina, keyValuePair.Value.CraftingStamina + this.GetStaminaHourlyRecoveryRate(key));
					}
				}
			}
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000E5258 File Offset: 0x000E3458
		private void OnHeroKilled(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			this.RemoveOrdersOfHeroWithoutCompletionIfExists(victim);
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000E5264 File Offset: 0x000E3464
		private void OnGameLoaded(CampaignGameStarter campaignGameStarter)
		{
			this.InitializeLists();
			foreach (KeyValuePair<Town, CraftingCampaignBehavior.CraftingOrderSlots> keyValuePair in this._craftingOrders)
			{
				for (int i = 0; i < 6; i++)
				{
					CraftingOrder craftingOrder = keyValuePair.Value.Slots[i];
					if (craftingOrder != null && (craftingOrder.PreCraftedWeaponDesignItem == DefaultItems.Trash || craftingOrder.PreCraftedWeaponDesignItem == null || !craftingOrder.PreCraftedWeaponDesignItem.IsReady))
					{
						this.CancelOrder(keyValuePair.Key, craftingOrder);
					}
				}
			}
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x000E5304 File Offset: 0x000E3504
		private int GetStaminaHourlyRecoveryRate(Hero hero)
		{
			int num = 5 + MathF.Round((float)hero.GetSkillValue(DefaultSkills.Crafting) * 0.025f);
			if (hero.GetPerkValue(DefaultPerks.Athletics.Stamina))
			{
				num += MathF.Round((float)num * DefaultPerks.Athletics.Stamina.PrimaryBonus);
			}
			return num;
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000E5350 File Offset: 0x000E3550
		private void OnNewItemCrafted(ItemObject itemObject, ItemModifier overridenItemModifier, bool isCraftingOrderItem)
		{
			if (!this._craftedItemDictionary.ContainsKey(itemObject))
			{
				CultureObject @object = MBObjectManager.Instance.GetObject<CultureObject>(itemObject.Culture.StringId);
				CraftingCampaignBehavior.CraftedItemInitializationData value = new CraftingCampaignBehavior.CraftedItemInitializationData(itemObject.WeaponDesign, itemObject.Name, @object);
				this._craftedItemDictionary.Add(itemObject, value);
			}
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000E53A4 File Offset: 0x000E35A4
		private void AddResearchPoints(CraftingTemplate craftingTemplate, int researchPoints)
		{
			Dictionary<CraftingTemplate, float> openNewPartXpDictionary = this._openNewPartXpDictionary;
			CraftingTemplate craftingTemplate2 = craftingTemplate;
			openNewPartXpDictionary[craftingTemplate2] += (float)researchPoints;
			int count = craftingTemplate.Pieces.Count;
			int num = craftingTemplate.Pieces.Count((CraftingPiece x) => this.IsOpened(x, craftingTemplate));
			float num2 = Campaign.Current.Models.SmithingModel.ResearchPointsNeedForNewPart(count, num);
			do
			{
				if (this._openNewPartXpDictionary[craftingTemplate] > num2)
				{
					openNewPartXpDictionary = this._openNewPartXpDictionary;
					craftingTemplate2 = craftingTemplate;
					openNewPartXpDictionary[craftingTemplate2] -= num2;
					if (this.OpenNewPart(craftingTemplate))
					{
						num++;
					}
				}
				num2 = Campaign.Current.Models.SmithingModel.ResearchPointsNeedForNewPart(count, craftingTemplate.Pieces.Count((CraftingPiece x) => this.IsOpened(x, craftingTemplate)));
			}
			while (this._openNewPartXpDictionary[craftingTemplate] > num2 && num < count);
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000E54C8 File Offset: 0x000E36C8
		private bool OpenNewPart(CraftingTemplate craftingTemplate)
		{
			int num = int.MaxValue;
			MBList<CraftingPiece> mblist = new MBList<CraftingPiece>();
			foreach (CraftingPiece craftingPiece in craftingTemplate.Pieces)
			{
				int pieceTier = craftingPiece.PieceTier;
				if (num >= pieceTier && !craftingPiece.IsHiddenOnDesigner && !this.IsOpened(craftingPiece, craftingTemplate))
				{
					if (num > craftingPiece.PieceTier)
					{
						mblist.Clear();
						num = pieceTier;
					}
					mblist.Add(craftingPiece);
				}
			}
			if (mblist.Count > 0)
			{
				CraftingPiece randomElement = mblist.GetRandomElement<CraftingPiece>();
				this.OpenPart(randomElement, craftingTemplate, true);
				return true;
			}
			return false;
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000E5578 File Offset: 0x000E3778
		private void OpenPart(CraftingPiece selectedPiece, CraftingTemplate craftingTemplate, bool showNotification = true)
		{
			this._openedPartsDictionary[craftingTemplate].Add(selectedPiece);
			CampaignEventDispatcher.Instance.CraftingPartUnlocked(selectedPiece);
			if (showNotification)
			{
				TextObject textObject = new TextObject("{=p9F90bc0}New Smithing Part Unlocked: {PART_NAME} for {WEAPON_TYPE}.", null);
				textObject.SetTextVariable("PART_NAME", selectedPiece.Name);
				textObject.SetTextVariable("WEAPON_TYPE", craftingTemplate.TemplateName);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000E55E0 File Offset: 0x000E37E0
		public bool IsOpened(CraftingPiece craftingPiece, CraftingTemplate craftingTemplate)
		{
			return craftingPiece.IsGivenByDefault || this._openedPartsDictionary[craftingTemplate].Contains(craftingPiece);
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000E5600 File Offset: 0x000E3800
		public void InitializeCraftingElements()
		{
			List<ItemObject> list = new List<ItemObject>();
			foreach (KeyValuePair<ItemObject, CraftingCampaignBehavior.CraftedItemInitializationData> keyValuePair in this._craftedItemDictionary)
			{
				bool flag = true;
				WeaponDesign weaponDesign = keyValuePair.Value.CraftedData;
				if (!weaponDesign.Template.IsReady)
				{
					flag = false;
				}
				if (flag && MBSaveLoad.IsUpdatingGameVersion && MBSaveLoad.LastLoadedGameVersion < ApplicationVersion.FromString("v1.1.0", 45697))
				{
					WeaponDesignElement[] array = new WeaponDesignElement[keyValuePair.Value.CraftedData.UsedPieces.Length];
					for (int i = 0; i < keyValuePair.Value.CraftedData.UsedPieces.Length; i++)
					{
						array[i] = keyValuePair.Value.CraftedData.UsedPieces[i].GetCopy();
					}
					weaponDesign = new WeaponDesign(weaponDesign.Template, weaponDesign.WeaponName, array);
				}
				if (flag)
				{
					foreach (PieceData pieceData in weaponDesign.Template.BuildOrders)
					{
						bool flag2 = false;
						foreach (WeaponDesignElement weaponDesignElement in weaponDesign.UsedPieces)
						{
							if (pieceData.PieceType == weaponDesignElement.CraftingPiece.PieceType && weaponDesignElement.CraftingPiece.IsValid)
							{
								flag2 = true;
							}
						}
						if (!flag2)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					ItemObject itemObject = Crafting.InitializePreCraftedWeaponOnLoad(keyValuePair.Key, weaponDesign, keyValuePair.Value.ItemName, keyValuePair.Value.Culture);
					if (itemObject == DefaultItems.Trash || itemObject == null)
					{
						list.Add(keyValuePair.Key);
						if (MBObjectManager.Instance.GetObject(keyValuePair.Key.Id) != null)
						{
							MBObjectManager.Instance.UnregisterObject(keyValuePair.Key);
						}
					}
					else
					{
						ItemObject.InitAsPlayerCraftedItem(ref itemObject);
						itemObject.IsReady = true;
					}
				}
				else
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (ItemObject key in list)
			{
				this._craftedItemDictionary.Remove(key);
			}
			foreach (KeyValuePair<Town, CraftingCampaignBehavior.CraftingOrderSlots> keyValuePair2 in this.CraftingOrders)
			{
				foreach (CraftingOrder craftingOrder in keyValuePair2.Value.Slots)
				{
					if (craftingOrder != null && !craftingOrder.IsPreCraftedWeaponDesignValid())
					{
						keyValuePair2.Value.RemoveTownOrder(craftingOrder);
					}
					else if (craftingOrder != null)
					{
						craftingOrder.InitializeCraftingOrderOnLoad();
					}
				}
				List<CraftingOrder> list2 = new List<CraftingOrder>();
				foreach (CraftingOrder craftingOrder2 in keyValuePair2.Value.CustomOrders)
				{
					if (!craftingOrder2.IsPreCraftedWeaponDesignValid())
					{
						list2.Add(craftingOrder2);
					}
					else
					{
						craftingOrder2.InitializeCraftingOrderOnLoad();
					}
				}
				foreach (CraftingOrder order in list2)
				{
					keyValuePair2.Value.RemoveCustomOrder(order);
				}
			}
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000E59E8 File Offset: 0x000E3BE8
		public int GetCraftingDifficulty(WeaponDesign weaponDesign)
		{
			return Campaign.Current.Models.SmithingModel.CalculateWeaponDesignDifficulty(weaponDesign);
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000E5A00 File Offset: 0x000E3C00
		private void InitializeLists()
		{
			if (this._craftingOrders.IsEmpty<KeyValuePair<Town, CraftingCampaignBehavior.CraftingOrderSlots>>())
			{
				foreach (Settlement settlement in Settlement.All)
				{
					if (settlement.IsTown)
					{
						this._craftingOrders.Add(settlement.Town, new CraftingCampaignBehavior.CraftingOrderSlots());
					}
				}
			}
			foreach (KeyValuePair<CraftingTemplate, List<CraftingPiece>> keyValuePair in this._openedPartsDictionary.ToList<KeyValuePair<CraftingTemplate, List<CraftingPiece>>>())
			{
				if (!CraftingTemplate.All.Contains(keyValuePair.Key))
				{
					this._openedPartsDictionary.Remove(keyValuePair.Key);
				}
			}
			foreach (KeyValuePair<CraftingTemplate, float> keyValuePair2 in this._openNewPartXpDictionary.ToList<KeyValuePair<CraftingTemplate, float>>())
			{
				if (!CraftingTemplate.All.Contains(keyValuePair2.Key))
				{
					this._openNewPartXpDictionary.Remove(keyValuePair2.Key);
				}
			}
			foreach (CraftingTemplate craftingTemplate in CraftingTemplate.All)
			{
				if (!this._openNewPartXpDictionary.ContainsKey(craftingTemplate))
				{
					this._openNewPartXpDictionary.Add(craftingTemplate, 0f);
				}
				if (!this._openedPartsDictionary.ContainsKey(craftingTemplate))
				{
					this._openedPartsDictionary.Add(craftingTemplate, new List<CraftingPiece>());
				}
				foreach (CraftingPiece item in this._openedPartsDictionary[craftingTemplate].ToList<CraftingPiece>())
				{
					if (!craftingTemplate.Pieces.Contains(item))
					{
						this._openedPartsDictionary[craftingTemplate].Remove(item);
					}
				}
			}
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000E5C38 File Offset: 0x000E3E38
		private void AddDialogs(CampaignGameStarter campaignGameStarter)
		{
			campaignGameStarter.AddDialogLine("blacksmith_begin", "start", "blacksmith_player", "{=gYByVHQy}Good day, {?PLAYER.GENDER}madam{?}sir{\\?}. How may I help you?", new ConversationSentence.OnConditionDelegate(this.conversation_blacksmith_begin_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("blacksmith_craft_items", "blacksmith_player", "blacksmith_player_ok", "{=VXKGD0ta}I want to use your forge.", () => Campaign.Current.IsCraftingEnabled, new ConversationSentence.OnConsequenceDelegate(this.conversation_blacksmith_craft_items_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("blacksmith_leave", "blacksmith_player", "close_window", "{=iW9iKbb8}Nothing.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("blacksmith_player_ok", "blacksmith_player_ok", "blacksmith_player_after_craft", "{=FJ0uAo6p}{CRAFTING_END_TEXT}", new ConversationSentence.OnConditionDelegate(this.conversation_blacksmith_player_ok_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("blacksmith_player_after_craft_accept", "blacksmith_player_after_craft", "player_blacksmith_after_craft", "{=QUn2ugIX}Thank you. Here's your pay.", new ConversationSentence.OnConditionDelegate(this.conversation_blacksmith_player_after_craft_accept_on_condition), new ConversationSentence.OnConsequenceDelegate(this.conversation_blacksmith_player_after_craft_accept_on_consequence), 100, null, null);
			campaignGameStarter.AddDialogLine("blacksmith_player_after_craft_anything_else", "player_blacksmith_after_craft", "blacksmith_player_1", "{=IvY187PJ}No matter. Anything else?", new ConversationSentence.OnConditionDelegate(this.conversation_blacksmith_player_after_craft_anything_else_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("blacksmith_craft_items_1", "blacksmith_player_1", "blacksmith_player_ok", "{=hrn1Cdwo}There is something else I need you to make.", () => Campaign.Current.IsCraftingEnabled, new ConversationSentence.OnConsequenceDelegate(this.conversation_blacksmith_craft_items_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("blacksmith_leave_1", "blacksmith_player_1", "close_window", "{=iW9iKbb8}Nothing.", null, null, 100, null, null);
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000E5DD3 File Offset: 0x000E3FD3
		public void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.AddDialogs(campaignGameStarter);
		}

		// Token: 0x06003534 RID: 13620 RVA: 0x000E5DDC File Offset: 0x000E3FDC
		private bool conversation_blacksmith_begin_on_condition()
		{
			return CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.Blacksmith;
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x000E5DEC File Offset: 0x000E3FEC
		private void conversation_blacksmith_craft_items_on_consequence()
		{
			CraftingHelper.OpenCrafting(CraftingTemplate.All[0], null);
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x000E5E00 File Offset: 0x000E4000
		private bool conversation_blacksmith_player_ok_on_condition()
		{
			if (CharacterObject.OneToOneConversationCharacter.Occupation == Occupation.Blacksmith)
			{
				if (this._latestCraftedItem != null)
				{
					int value = this._latestCraftedItem.Value;
					TextObject textObject = new TextObject("{=zr80RMa6}This will cost you {AMOUNT}{GOLD_ICON}. Paid in advance, of course.", null);
					textObject.SetTextVariable("AMOUNT", value.ToString());
					MBTextManager.SetTextVariable("CRAFTING_END_TEXT", textObject, false);
				}
				else
				{
					MBTextManager.SetTextVariable("CRAFTING_END_TEXT", new TextObject("{=rrQx9jaV}It seems you're not interested.", null), false);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000E5E78 File Offset: 0x000E4078
		private bool conversation_blacksmith_player_after_craft_accept_on_condition()
		{
			if (this._latestCraftedItem != null)
			{
				int value = this._latestCraftedItem.Value;
				return Hero.MainHero.Gold >= value;
			}
			return false;
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x000E5EAB File Offset: 0x000E40AB
		private void conversation_blacksmith_player_after_craft_accept_on_consequence()
		{
			ItemRoster itemRoster = new ItemRoster();
			itemRoster.AddToCounts(this._latestCraftedItem, 1);
			this._latestCraftedItem = null;
			InventoryManager.OpenScreenAsTrade(itemRoster, Settlement.CurrentSettlement.Town, InventoryManager.InventoryCategoryType.None, null);
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x000E5ED8 File Offset: 0x000E40D8
		private bool conversation_blacksmith_player_after_craft_anything_else_on_condition()
		{
			this._latestCraftedItem = null;
			return true;
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000E5EE2 File Offset: 0x000E40E2
		public bool CanHeroUsePart(Hero hero, CraftingPiece craftingPiece)
		{
			return true;
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x000E5EE5 File Offset: 0x000E40E5
		public int GetHeroCraftingStamina(Hero hero)
		{
			return this.GetRecordForCompanion(hero).CraftingStamina;
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000E5EF4 File Offset: 0x000E40F4
		private CraftingCampaignBehavior.HeroCraftingRecord GetRecordForCompanion(Hero hero)
		{
			CraftingCampaignBehavior.HeroCraftingRecord heroCraftingRecord;
			if (!this._heroCraftingRecords.TryGetValue(hero, out heroCraftingRecord))
			{
				heroCraftingRecord = new CraftingCampaignBehavior.HeroCraftingRecord(this.GetMaxHeroCraftingStamina(hero));
				this._heroCraftingRecords[hero] = heroCraftingRecord;
			}
			return heroCraftingRecord;
		}

		// Token: 0x0600353D RID: 13629 RVA: 0x000E5F2C File Offset: 0x000E412C
		public void SetHeroCraftingStamina(Hero hero, int value)
		{
			this.GetRecordForCompanion(hero).CraftingStamina = MathF.Max(0, value);
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x000E5F44 File Offset: 0x000E4144
		public void SetCraftedWeaponName(ItemObject craftedWeaponItem, TextObject name)
		{
			CraftingCampaignBehavior.CraftedItemInitializationData craftedItemInitializationData;
			if (this._craftedItemDictionary.TryGetValue(craftedWeaponItem, out craftedItemInitializationData))
			{
				this._craftedItemDictionary[craftedWeaponItem] = new CraftingCampaignBehavior.CraftedItemInitializationData(craftedItemInitializationData.CraftedData, name, craftedItemInitializationData.Culture);
			}
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x000E5F7F File Offset: 0x000E417F
		public int GetMaxHeroCraftingStamina(Hero hero)
		{
			return 100 + MathF.Round((float)hero.GetSkillValue(DefaultSkills.Crafting) * 0.5f);
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000E5F9C File Offset: 0x000E419C
		public void DoRefinement(Hero hero, Crafting.RefiningFormula refineFormula)
		{
			ItemRoster itemRoster = MobileParty.MainParty.ItemRoster;
			if (refineFormula.Input1Count > 0)
			{
				ItemObject craftingMaterialItem = Campaign.Current.Models.SmithingModel.GetCraftingMaterialItem(refineFormula.Input1);
				itemRoster.AddToCounts(craftingMaterialItem, -refineFormula.Input1Count);
			}
			if (refineFormula.Input2Count > 0)
			{
				ItemObject craftingMaterialItem2 = Campaign.Current.Models.SmithingModel.GetCraftingMaterialItem(refineFormula.Input2);
				itemRoster.AddToCounts(craftingMaterialItem2, -refineFormula.Input2Count);
			}
			if (refineFormula.OutputCount > 0)
			{
				ItemObject craftingMaterialItem3 = Campaign.Current.Models.SmithingModel.GetCraftingMaterialItem(refineFormula.Output);
				itemRoster.AddToCounts(craftingMaterialItem3, refineFormula.OutputCount);
			}
			if (refineFormula.Output2Count > 0)
			{
				ItemObject craftingMaterialItem4 = Campaign.Current.Models.SmithingModel.GetCraftingMaterialItem(refineFormula.Output2);
				itemRoster.AddToCounts(craftingMaterialItem4, refineFormula.Output2Count);
			}
			hero.AddSkillXp(DefaultSkills.Crafting, (float)Campaign.Current.Models.SmithingModel.GetSkillXpForRefining(ref refineFormula));
			int energyCostForRefining = Campaign.Current.Models.SmithingModel.GetEnergyCostForRefining(ref refineFormula, hero);
			this.SetHeroCraftingStamina(hero, this.GetHeroCraftingStamina(hero) - energyCostForRefining);
			CampaignEventDispatcher.Instance.OnItemsRefined(hero, refineFormula);
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000E60D8 File Offset: 0x000E42D8
		public void DoSmelting(Hero hero, EquipmentElement equipmentElement)
		{
			ItemRoster itemRoster = MobileParty.MainParty.ItemRoster;
			ItemObject item = equipmentElement.Item;
			int[] smeltingOutputForItem = Campaign.Current.Models.SmithingModel.GetSmeltingOutputForItem(item);
			for (int i = 8; i >= 0; i--)
			{
				if (smeltingOutputForItem[i] != 0)
				{
					itemRoster.AddToCounts(Campaign.Current.Models.SmithingModel.GetCraftingMaterialItem((CraftingMaterials)i), smeltingOutputForItem[i]);
				}
			}
			itemRoster.AddToCounts(equipmentElement, -1);
			hero.AddSkillXp(DefaultSkills.Crafting, (float)Campaign.Current.Models.SmithingModel.GetSkillXpForSmelting(item));
			int energyCostForSmelting = Campaign.Current.Models.SmithingModel.GetEnergyCostForSmelting(item, hero);
			this.SetHeroCraftingStamina(hero, this.GetHeroCraftingStamina(hero) - energyCostForSmelting);
			this.AddResearchPoints(item.WeaponDesign.Template, Campaign.Current.Models.SmithingModel.GetPartResearchGainForSmeltingItem(item, hero));
			CampaignEventDispatcher.Instance.OnEquipmentSmeltedByHero(hero, equipmentElement);
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000E61CC File Offset: 0x000E43CC
		public ItemObject CreateCraftedWeaponInFreeBuildMode(Hero hero, WeaponDesign weaponDesign, ItemModifier weaponModifier = null)
		{
			CraftingCampaignBehavior.SpendMaterials(weaponDesign);
			CraftingState craftingState;
			if ((craftingState = (GameStateManager.Current.ActiveState as CraftingState)) != null)
			{
				ItemObject currentCraftedItemObject = craftingState.CraftingLogic.GetCurrentCraftedItemObject(true);
				ItemObject.InitAsPlayerCraftedItem(ref currentCraftedItemObject);
				MBObjectManager.Instance.RegisterObject<ItemObject>(currentCraftedItemObject);
				if (weaponModifier == null)
				{
					PartyBase.MainParty.ItemRoster.AddToCounts(currentCraftedItemObject, 1);
				}
				else
				{
					EquipmentElement rosterElement = new EquipmentElement(currentCraftedItemObject, weaponModifier, null, false);
					PartyBase.MainParty.ItemRoster.AddToCounts(rosterElement, 1);
				}
				CampaignEventDispatcher.Instance.OnNewItemCrafted(currentCraftedItemObject, weaponModifier, false);
				hero.AddSkillXp(DefaultSkills.Crafting, (float)Campaign.Current.Models.SmithingModel.GetSkillXpForSmithingInFreeBuildMode(currentCraftedItemObject));
				int energyCostForSmithing = Campaign.Current.Models.SmithingModel.GetEnergyCostForSmithing(currentCraftedItemObject, hero);
				this.SetHeroCraftingStamina(hero, this.GetHeroCraftingStamina(hero) - energyCostForSmithing);
				this.AddResearchPoints(weaponDesign.Template, Campaign.Current.Models.SmithingModel.GetPartResearchGainForSmithingItem(currentCraftedItemObject, hero, true));
				this.AddItemToHistory(craftingState.CraftingLogic.CurrentWeaponDesign);
				return currentCraftedItemObject;
			}
			return null;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000E62D8 File Offset: 0x000E44D8
		public ItemObject CreateCraftedWeaponInCraftingOrderMode(Hero crafterHero, CraftingOrder craftingOrder, WeaponDesign weaponDesign)
		{
			CraftingCampaignBehavior.SpendMaterials(weaponDesign);
			CraftingState craftingState;
			if ((craftingState = (GameStateManager.Current.ActiveState as CraftingState)) != null)
			{
				ItemObject currentCraftedItemObject = craftingState.CraftingLogic.GetCurrentCraftedItemObject(true);
				ItemObject.InitAsPlayerCraftedItem(ref currentCraftedItemObject);
				MBObjectManager.Instance.RegisterObject<ItemObject>(currentCraftedItemObject);
				Campaign.Current.CampaignEvents.OnNewItemCrafted(currentCraftedItemObject, null, true);
				float xpAmount = craftingOrder.GetOrderExperience(currentCraftedItemObject, this._currentItemModifier) + (float)Campaign.Current.Models.SmithingModel.GetSkillXpForSmithingInCraftingOrderMode(currentCraftedItemObject);
				crafterHero.AddSkillXp(DefaultSkills.Crafting, xpAmount);
				int energyCostForSmithing = Campaign.Current.Models.SmithingModel.GetEnergyCostForSmithing(currentCraftedItemObject, crafterHero);
				this.SetHeroCraftingStamina(crafterHero, this.GetHeroCraftingStamina(crafterHero) - energyCostForSmithing);
				this.AddResearchPoints(weaponDesign.Template, Campaign.Current.Models.SmithingModel.GetPartResearchGainForSmithingItem(currentCraftedItemObject, crafterHero, false));
				return currentCraftedItemObject;
			}
			return null;
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000E63B4 File Offset: 0x000E45B4
		private static void SpendMaterials(WeaponDesign weaponDesign)
		{
			ItemRoster itemRoster = MobileParty.MainParty.ItemRoster;
			int[] smithingCostsForWeaponDesign = Campaign.Current.Models.SmithingModel.GetSmithingCostsForWeaponDesign(weaponDesign);
			for (int i = 8; i >= 0; i--)
			{
				if (smithingCostsForWeaponDesign[i] != 0)
				{
					itemRoster.AddToCounts(Campaign.Current.Models.SmithingModel.GetCraftingMaterialItem((CraftingMaterials)i), smithingCostsForWeaponDesign[i]);
				}
			}
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000E6412 File Offset: 0x000E4612
		private void AddItemToHistory(WeaponDesign design)
		{
			while (this._craftingHistory.Count >= 10)
			{
				this._craftingHistory.RemoveAt(0);
			}
			this._craftingHistory.Add(design);
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000E6440 File Offset: 0x000E4640
		public void CreateTownOrder(Hero orderOwner, int orderSlot)
		{
			if (orderOwner.CurrentSettlement == null || !orderOwner.CurrentSettlement.IsTown)
			{
				Debug.Print(string.Concat(new string[]
				{
					"Order owner: ",
					orderOwner.StringId,
					" Settlement",
					(orderOwner.CurrentSettlement == null) ? "null" : orderOwner.CurrentSettlement.StringId,
					" Order owner party: ",
					(orderOwner.PartyBelongedTo == null) ? "null" : orderOwner.PartyBelongedTo.StringId
				}), 0, Debug.DebugColor.White, 17592186044416UL);
			}
			float townOrderDifficulty = this.GetTownOrderDifficulty(orderOwner.CurrentSettlement.Town, orderSlot);
			int pieceTier = (int)townOrderDifficulty / 50;
			CraftingTemplate randomElement = CraftingTemplate.All.GetRandomElement<CraftingTemplate>();
			WeaponDesign weaponDesignTemplate = new WeaponDesign(randomElement, TextObject.Empty, this.GetWeaponPieces(randomElement, pieceTier));
			this._craftingOrders[orderOwner.CurrentSettlement.Town].AddTownOrder(new CraftingOrder(orderOwner, townOrderDifficulty, weaponDesignTemplate, randomElement, orderSlot));
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000E6538 File Offset: 0x000E4738
		private float GetTownOrderDifficulty(Town town, int orderSlot)
		{
			int num = 0;
			switch (orderSlot)
			{
			case 0:
				num = MBRandom.RandomInt(40, 80);
				break;
			case 1:
				num = MBRandom.RandomInt(80, 120);
				break;
			case 2:
				num = MBRandom.RandomInt(120, 160);
				break;
			case 3:
				num = MBRandom.RandomInt(160, 200);
				break;
			case 4:
				num = MBRandom.RandomInt(200, 241);
				break;
			case 5:
				num = Hero.MainHero.GetSkillValue(DefaultSkills.Crafting);
				break;
			}
			return (float)num + town.Prosperity / 500f;
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000E65D4 File Offset: 0x000E47D4
		public CraftingOrder CreateCustomOrderForHero(Hero orderOwner, float orderDifficulty = -1f, WeaponDesign weaponDesign = null, CraftingTemplate craftingTemplate = null)
		{
			if (orderDifficulty < 0f)
			{
				orderDifficulty = this.GetRandomOrderDifficulty(orderOwner.CurrentSettlement.Town);
			}
			if (craftingTemplate == null)
			{
				craftingTemplate = CraftingTemplate.All.GetRandomElement<CraftingTemplate>();
			}
			if (weaponDesign == null)
			{
				int pieceTier = (int)orderDifficulty / 40;
				weaponDesign = new WeaponDesign(craftingTemplate, TextObject.Empty, this.GetWeaponPieces(craftingTemplate, pieceTier));
			}
			CraftingOrder craftingOrder = new CraftingOrder(orderOwner, orderDifficulty, weaponDesign, craftingTemplate, -1);
			this._craftingOrders[orderOwner.CurrentSettlement.Town].AddCustomOrder(craftingOrder);
			return craftingOrder;
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000E665C File Offset: 0x000E485C
		private float GetRandomOrderDifficulty(Town town)
		{
			int num = MBRandom.RandomInt(0, 6);
			int num2 = 0;
			switch (num)
			{
			case 0:
				num2 = MBRandom.RandomInt(40, 80);
				break;
			case 1:
				num2 = MBRandom.RandomInt(80, 120);
				break;
			case 2:
				num2 = MBRandom.RandomInt(120, 160);
				break;
			case 3:
				num2 = MBRandom.RandomInt(160, 200);
				break;
			case 4:
				num2 = MBRandom.RandomInt(200, 241);
				break;
			case 5:
				num2 = Hero.MainHero.GetSkillValue(DefaultSkills.Crafting);
				break;
			}
			return (float)num2 + town.Prosperity / 500f;
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000E6700 File Offset: 0x000E4900
		private WeaponDesignElement[] GetWeaponPieces(CraftingTemplate craftingTemplate, int pieceTier)
		{
			WeaponDesignElement[] array = new WeaponDesignElement[4];
			List<WeaponDesignElement>[] array2 = new List<WeaponDesignElement>[4];
			foreach (CraftingPiece craftingPiece in craftingTemplate.Pieces)
			{
				bool flag = false;
				foreach (PieceData pieceData in craftingTemplate.BuildOrders)
				{
					if (pieceData.PieceType == craftingPiece.PieceType)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					int pieceType = (int)craftingPiece.PieceType;
					if (array2[pieceType] == null)
					{
						array2[pieceType] = new List<WeaponDesignElement>();
					}
					array2[pieceType].Add(WeaponDesignElement.CreateUsablePiece(craftingPiece, 100));
				}
			}
			Func<WeaponDesignElement, bool> <>9__0;
			for (int j = 0; j < array.Length; j++)
			{
				if (array2[j] != null)
				{
					WeaponDesignElement[] array3 = array;
					int num = j;
					List<WeaponDesignElement> source = array2[j];
					Func<WeaponDesignElement, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((WeaponDesignElement p) => !p.CraftingPiece.IsHiddenOnDesigner && p.CraftingPiece.PieceTier == pieceTier));
					}
					WeaponDesignElement weaponDesignElement;
					if ((weaponDesignElement = source.FirstOrDefaultQ(predicate)) == null)
					{
						weaponDesignElement = array2[j].FirstOrDefaultQ((WeaponDesignElement p) => !p.CraftingPiece.IsHiddenOnDesigner && p.CraftingPiece.PieceTier == 1);
					}
					WeaponDesignElement weaponDesignElement2;
					if ((weaponDesignElement2 = weaponDesignElement) == null)
					{
						weaponDesignElement2 = array2[j].First((WeaponDesignElement p) => !p.CraftingPiece.IsHiddenOnDesigner);
					}
					array3[num] = weaponDesignElement2;
				}
				else
				{
					array[j] = WeaponDesignElement.GetInvalidPieceForType((CraftingPiece.PieceTypes)j);
				}
			}
			return array;
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000E688C File Offset: 0x000E4A8C
		private void ReplaceCraftingOrder(Town town, CraftingOrder order)
		{
			MBList<Hero> mblist = new MBList<Hero>();
			Settlement settlement = town.Settlement;
			mblist.AddRange(settlement.HeroesWithoutParty);
			foreach (MobileParty mobileParty in settlement.Parties)
			{
				if (mobileParty.LeaderHero != null && !mobileParty.IsMainParty)
				{
					mblist.Add(mobileParty.LeaderHero);
				}
			}
			int difficultyLevel = order.DifficultyLevel;
			this._craftingOrders[town].RemoveTownOrder(order);
			if (mblist.Count > 0)
			{
				this.CreateTownOrder(mblist.GetRandomElement<Hero>(), difficultyLevel);
			}
			mblist = null;
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000E6944 File Offset: 0x000E4B44
		public void GetOrderResult(CraftingOrder craftingOrder, ItemObject craftedItem, out bool isSucceed, out TextObject orderRemark, out TextObject orderResult, out int finalReward)
		{
			finalReward = this.CalculateOrderPriceDifference(craftingOrder, craftedItem);
			float num;
			float num2;
			bool flag;
			bool flag2;
			craftingOrder.CheckForBonusesAndPenalties(craftedItem, this._currentItemModifier, out num, out num2, out flag, out flag2);
			isSucceed = (num >= num2 && flag && flag2);
			int num3 = finalReward - craftingOrder.BaseGoldReward;
			orderRemark = TextObject.Empty;
			if (isSucceed)
			{
				orderResult = new TextObject("{=Nn49hU2W}The client is satisfied.", null);
				if (num3 == 0)
				{
					orderRemark = new TextObject("{=FWHvvZFq}\"This is exactly what I wanted. Here is your money, you've earned it.\"", null);
					return;
				}
				if ((float)num3 > 0f)
				{
					orderRemark = new TextObject("{=raCa7QXj}\"This is even better than what I have imagined. Here is your money, and I'm putting a little extra for your effort.\"", null);
					return;
				}
			}
			else
			{
				orderResult = new TextObject("{=bC2jevlu}The client is displeased.", null);
				if (finalReward <= 0)
				{
					orderRemark = new TextObject("{=NZynd8vT}\"This weapon is worthless. I'm not giving you a dime!\"", null);
					return;
				}
				if (finalReward < craftingOrder.BaseGoldReward)
				{
					TextObject textObject = TextObject.Empty;
					if (!flag || !flag2)
					{
						textObject = new TextObject("{=WyuIksRB}\"This weapon does not have the damage type I wanted. I'm cutting {AMOUNT}{GOLD_ICON} from the price.\"", null);
					}
					else
					{
						textObject = new TextObject("{=wU76OPxM}\"This is worse than what I've asked for. I'm cutting {AMOUNT}{GOLD_ICON} from the price.\"", null);
					}
					textObject.SetTextVariable("AMOUNT", MathF.Abs(num3));
					orderRemark = textObject;
				}
			}
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000E6A40 File Offset: 0x000E4C40
		private int CalculateOrderPriceDifference(CraftingOrder craftingOrder, ItemObject craftedItem)
		{
			float num;
			float num2;
			bool flag;
			bool flag2;
			craftingOrder.CheckForBonusesAndPenalties(craftedItem, this._currentItemModifier, out num, out num2, out flag, out flag2);
			float num3 = (float)craftingOrder.BaseGoldReward;
			if (!num.ApproximatelyEqualsTo(0f, 1E-05f) && !num2.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				if (num < num2 || !flag || !flag2)
				{
					float b = (float)Campaign.Current.Models.TradeItemPriceFactorModel.GetTheoreticalMaxItemMarketValue(craftedItem) / (float)Campaign.Current.Models.TradeItemPriceFactorModel.GetTheoreticalMaxItemMarketValue(craftingOrder.PreCraftedWeaponDesignItem);
					num3 = (float)craftingOrder.BaseGoldReward * 0.5f * MathF.Min(1f, b);
					if (num3 > (float)craftingOrder.BaseGoldReward)
					{
						num3 = (float)craftingOrder.BaseGoldReward * 0.5f;
					}
				}
				else if (num > num2)
				{
					num3 = (float)craftingOrder.BaseGoldReward * (1f + (num - num2) / num2 * 0.1f);
				}
			}
			return (int)num3;
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000E6B30 File Offset: 0x000E4D30
		public void CompleteOrder(Town town, CraftingOrder craftingOrder, ItemObject craftedItem, Hero completerHero)
		{
			int amount = this.CalculateOrderPriceDifference(craftingOrder, craftedItem);
			bool flag;
			TextObject textObject;
			TextObject textObject2;
			int num;
			this.GetOrderResult(craftingOrder, craftedItem, out flag, out textObject, out textObject2, out num);
			GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, amount, false);
			if (this._craftingOrders[town].CustomOrders.Contains(craftingOrder))
			{
				this._craftingOrders[town].RemoveCustomOrder(craftingOrder);
			}
			else
			{
				if (craftingOrder.IsLordOrder)
				{
					this.ChangeCraftedOrderWithTheNoblesWeaponIfItIsBetter(craftedItem, craftingOrder);
					if (craftingOrder.OrderOwner.PartyBelongedTo != null)
					{
						this.GiveTroopToNobleAtWeaponTier((int)craftedItem.Tier, craftingOrder.OrderOwner);
					}
					if (flag && completerHero.GetPerkValue(DefaultPerks.Crafting.SteelMaker3))
					{
						ChangeRelationAction.ApplyRelationChangeBetweenHeroes(completerHero, craftingOrder.OrderOwner, (int)DefaultPerks.Crafting.SteelMaker3.SecondaryBonus, true);
					}
				}
				else
				{
					craftingOrder.OrderOwner.AddPower((float)(craftedItem.Tier + 1));
					if (flag && completerHero.GetPerkValue(DefaultPerks.Crafting.ExperiencedSmith))
					{
						ChangeRelationAction.ApplyRelationChangeBetweenHeroes(completerHero, craftingOrder.OrderOwner, (int)DefaultPerks.Crafting.ExperiencedSmith.SecondaryBonus, true);
					}
				}
				this._craftingOrders[town].RemoveTownOrder(craftingOrder);
			}
			CampaignEventDispatcher.Instance.OnCraftingOrderCompleted(town, craftingOrder, craftedItem, completerHero);
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000E6C4D File Offset: 0x000E4E4D
		public ItemModifier GetCurrentItemModifier()
		{
			return this._currentItemModifier;
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000E6C55 File Offset: 0x000E4E55
		public void SetCurrentItemModifier(ItemModifier modifier)
		{
			this._currentItemModifier = modifier;
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000E6C60 File Offset: 0x000E4E60
		private void RemoveOrdersOfHeroWithoutCompletionIfExists(Hero hero)
		{
			new List<CraftingOrder>();
			foreach (KeyValuePair<Town, CraftingCampaignBehavior.CraftingOrderSlots> keyValuePair in this._craftingOrders)
			{
				for (int i = 0; i < 6; i++)
				{
					if (keyValuePair.Value.Slots[i] != null && keyValuePair.Value.Slots[i].OrderOwner == hero)
					{
						keyValuePair.Value.RemoveTownOrder(keyValuePair.Value.Slots[i]);
					}
				}
			}
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000E6D00 File Offset: 0x000E4F00
		public void CancelCustomOrder(Town town, CraftingOrder craftingOrder)
		{
			if (this._craftingOrders[town].CustomOrders.Contains(craftingOrder))
			{
				this._craftingOrders[town].RemoveCustomOrder(craftingOrder);
				return;
			}
			Debug.FailedAssert("Trying to cancel a custom order that doesn't exist.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\CampaignBehaviors\\CraftingCampaignBehavior.cs", "CancelCustomOrder", 1250);
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000E6D52 File Offset: 0x000E4F52
		private void CancelOrder(Town town, CraftingOrder craftingOrder)
		{
			this._craftingOrders[town].RemoveTownOrder(craftingOrder);
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000E6D68 File Offset: 0x000E4F68
		private void ChangeCraftedOrderWithTheNoblesWeaponIfItIsBetter(ItemObject craftedItem, CraftingOrder craftingOrder)
		{
			Equipment battleEquipment = craftingOrder.OrderOwner.BattleEquipment;
			for (int i = 0; i < 12; i++)
			{
				if (!battleEquipment[i].IsEmpty)
				{
					WeaponClass weaponClass = craftedItem.PrimaryWeapon.WeaponClass;
					WeaponComponentData primaryWeapon = battleEquipment[i].Item.PrimaryWeapon;
					WeaponClass? weaponClass2 = (primaryWeapon != null) ? new WeaponClass?(primaryWeapon.WeaponClass) : null;
					if (weaponClass == weaponClass2.GetValueOrDefault() & weaponClass2 != null)
					{
						ItemObject item = battleEquipment[i].Item;
						int thrustSpeed = item.PrimaryWeapon.ThrustSpeed;
						int thrustSpeed2 = craftedItem.PrimaryWeapon.ThrustSpeed;
						int swingSpeed = item.PrimaryWeapon.SwingSpeed;
						int swingSpeed2 = craftedItem.PrimaryWeapon.SwingSpeed;
						int missileSpeed = item.PrimaryWeapon.MissileSpeed;
						int missileSpeed2 = craftedItem.PrimaryWeapon.MissileSpeed;
						float weaponBalance = item.PrimaryWeapon.WeaponBalance;
						float weaponBalance2 = craftedItem.PrimaryWeapon.WeaponBalance;
						int thrustDamage = item.PrimaryWeapon.ThrustDamage;
						int thrustDamage2 = craftedItem.PrimaryWeapon.ThrustDamage;
						DamageTypes thrustDamageType = item.PrimaryWeapon.ThrustDamageType;
						DamageTypes thrustDamageType2 = craftedItem.PrimaryWeapon.ThrustDamageType;
						int swingDamage = item.PrimaryWeapon.SwingDamage;
						int swingDamage2 = craftedItem.PrimaryWeapon.SwingDamage;
						DamageTypes swingDamageType = item.PrimaryWeapon.SwingDamageType;
						DamageTypes swingDamageType2 = craftedItem.PrimaryWeapon.SwingDamageType;
						int accuracy = item.PrimaryWeapon.Accuracy;
						int accuracy2 = craftedItem.PrimaryWeapon.Accuracy;
						float weight = item.Weight;
						float weight2 = craftedItem.Weight;
						if (thrustSpeed2 > thrustSpeed && swingSpeed2 > swingSpeed && missileSpeed2 > missileSpeed && weaponBalance2 > weaponBalance && thrustDamage2 > thrustDamage && thrustDamageType == thrustDamageType2 && swingDamage2 > swingDamage && swingDamageType2 == swingDamageType && accuracy2 > accuracy && weight2 < weight)
						{
							battleEquipment[i] = new EquipmentElement(craftedItem, null, null, false);
							return;
						}
					}
				}
			}
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000E6F50 File Offset: 0x000E5150
		private void GiveTroopToNobleAtWeaponTier(int tier, Hero noble)
		{
			CharacterObject characterObject = noble.Culture.BasicTroop;
			for (int i = 0; i < tier; i++)
			{
				if (characterObject.UpgradeTargets.Length != 0)
				{
					characterObject = characterObject.UpgradeTargets.GetRandomElement<CharacterObject>();
				}
			}
			noble.PartyBelongedTo.AddElementToMemberRoster(characterObject, 1, false);
		}

		// Token: 0x0400112A RID: 4394
		private const float WaitTargetHours = 8f;

		// Token: 0x0400112B RID: 4395
		private const float CraftingOrderReplaceChance = 0.05f;

		// Token: 0x0400112C RID: 4396
		private const float CreateCraftingOrderChance = 0.05f;

		// Token: 0x0400112D RID: 4397
		private const int TownCraftingOrderCount = 6;

		// Token: 0x0400112E RID: 4398
		private const int DefaultCraftingOrderPieceTier = 1;

		// Token: 0x0400112F RID: 4399
		private const int CraftingOrderTroopBonusAmount = 1;

		// Token: 0x04001130 RID: 4400
		private const int MinOrderDifficulty = 40;

		// Token: 0x04001131 RID: 4401
		private const int MaxOrderDifficulty = 240;

		// Token: 0x04001132 RID: 4402
		private const int MaxCraftingHistoryDesigns = 10;

		// Token: 0x04001133 RID: 4403
		private ItemObject _latestCraftedItem;

		// Token: 0x04001134 RID: 4404
		private ItemModifier _currentItemModifier;

		// Token: 0x04001135 RID: 4405
		private Dictionary<CraftingTemplate, List<CraftingPiece>> _openedPartsDictionary = new Dictionary<CraftingTemplate, List<CraftingPiece>>();

		// Token: 0x04001136 RID: 4406
		private Dictionary<CraftingTemplate, float> _openNewPartXpDictionary = new Dictionary<CraftingTemplate, float>();

		// Token: 0x04001137 RID: 4407
		private Dictionary<ItemObject, CraftingCampaignBehavior.CraftedItemInitializationData> _craftedItemDictionary = new Dictionary<ItemObject, CraftingCampaignBehavior.CraftedItemInitializationData>();

		// Token: 0x04001138 RID: 4408
		private Dictionary<Hero, CraftingCampaignBehavior.HeroCraftingRecord> _heroCraftingRecords = new Dictionary<Hero, CraftingCampaignBehavior.HeroCraftingRecord>();

		// Token: 0x04001139 RID: 4409
		private List<WeaponDesign> _craftingHistory = new List<WeaponDesign>();

		// Token: 0x0400113A RID: 4410
		private Dictionary<Town, CraftingCampaignBehavior.CraftingOrderSlots> _craftingOrders = new Dictionary<Town, CraftingCampaignBehavior.CraftingOrderSlots>();

		// Token: 0x020006D6 RID: 1750
		public class CraftingCampaignBehaviorTypeDefiner : SaveableTypeDefiner
		{
			// Token: 0x0600571C RID: 22300 RVA: 0x00180579 File Offset: 0x0017E779
			public CraftingCampaignBehaviorTypeDefiner() : base(150000)
			{
			}

			// Token: 0x0600571D RID: 22301 RVA: 0x00180586 File Offset: 0x0017E786
			protected override void DefineClassTypes()
			{
				base.AddClassDefinition(typeof(CraftingCampaignBehavior.CraftedItemInitializationData), 10, null);
				base.AddClassDefinition(typeof(CraftingCampaignBehavior.HeroCraftingRecord), 20, null);
				base.AddClassDefinition(typeof(CraftingCampaignBehavior.CraftingOrderSlots), 30, null);
			}

			// Token: 0x0600571E RID: 22302 RVA: 0x001805C4 File Offset: 0x0017E7C4
			protected override void DefineContainerDefinitions()
			{
				base.ConstructContainerDefinition(typeof(Dictionary<ItemObject, CraftingCampaignBehavior.CraftedItemInitializationData>));
				base.ConstructContainerDefinition(typeof(Dictionary<Hero, CraftingCampaignBehavior.HeroCraftingRecord>));
				base.ConstructContainerDefinition(typeof(Dictionary<Town, CraftingCampaignBehavior.CraftingOrderSlots>));
				base.ConstructContainerDefinition(typeof(List<CraftingOrder>));
			}
		}

		// Token: 0x020006D7 RID: 1751
		internal class CraftedItemInitializationData
		{
			// Token: 0x0600571F RID: 22303 RVA: 0x00180611 File Offset: 0x0017E811
			public CraftedItemInitializationData(WeaponDesign craftedData, TextObject itemName, CultureObject culture)
			{
				this.CraftedData = craftedData;
				this.ItemName = itemName;
				this.Culture = culture;
			}

			// Token: 0x06005720 RID: 22304 RVA: 0x0018062E File Offset: 0x0017E82E
			internal static void AutoGeneratedStaticCollectObjectsCraftedItemInitializationData(object o, List<object> collectedObjects)
			{
				((CraftingCampaignBehavior.CraftedItemInitializationData)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06005721 RID: 22305 RVA: 0x0018063C File Offset: 0x0017E83C
			protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				collectedObjects.Add(this.CraftedData);
				collectedObjects.Add(this.ItemName);
				collectedObjects.Add(this.Culture);
			}

			// Token: 0x06005722 RID: 22306 RVA: 0x00180662 File Offset: 0x0017E862
			internal static object AutoGeneratedGetMemberValueCraftedData(object o)
			{
				return ((CraftingCampaignBehavior.CraftedItemInitializationData)o).CraftedData;
			}

			// Token: 0x06005723 RID: 22307 RVA: 0x0018066F File Offset: 0x0017E86F
			internal static object AutoGeneratedGetMemberValueItemName(object o)
			{
				return ((CraftingCampaignBehavior.CraftedItemInitializationData)o).ItemName;
			}

			// Token: 0x06005724 RID: 22308 RVA: 0x0018067C File Offset: 0x0017E87C
			internal static object AutoGeneratedGetMemberValueCulture(object o)
			{
				return ((CraftingCampaignBehavior.CraftedItemInitializationData)o).Culture;
			}

			// Token: 0x04001C41 RID: 7233
			[SaveableField(10)]
			public readonly WeaponDesign CraftedData;

			// Token: 0x04001C42 RID: 7234
			[SaveableField(20)]
			public readonly TextObject ItemName;

			// Token: 0x04001C43 RID: 7235
			[SaveableField(30)]
			public readonly CultureObject Culture;
		}

		// Token: 0x020006D8 RID: 1752
		internal class HeroCraftingRecord
		{
			// Token: 0x06005725 RID: 22309 RVA: 0x00180689 File Offset: 0x0017E889
			public HeroCraftingRecord(int maxStamina)
			{
				this.CraftingStamina = maxStamina;
			}

			// Token: 0x06005726 RID: 22310 RVA: 0x00180698 File Offset: 0x0017E898
			internal static void AutoGeneratedStaticCollectObjectsHeroCraftingRecord(object o, List<object> collectedObjects)
			{
				((CraftingCampaignBehavior.HeroCraftingRecord)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06005727 RID: 22311 RVA: 0x001806A6 File Offset: 0x0017E8A6
			protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
			}

			// Token: 0x06005728 RID: 22312 RVA: 0x001806A8 File Offset: 0x0017E8A8
			internal static object AutoGeneratedGetMemberValueCraftingStamina(object o)
			{
				return ((CraftingCampaignBehavior.HeroCraftingRecord)o).CraftingStamina;
			}

			// Token: 0x04001C44 RID: 7236
			[SaveableField(10)]
			public int CraftingStamina;
		}

		// Token: 0x020006D9 RID: 1753
		public class CraftingOrderSlots
		{
			// Token: 0x170013B0 RID: 5040
			// (get) Token: 0x06005729 RID: 22313 RVA: 0x001806BA File Offset: 0x0017E8BA
			public MBReadOnlyList<CraftingOrder> CustomOrders
			{
				get
				{
					return this._customOrders;
				}
			}

			// Token: 0x0600572A RID: 22314 RVA: 0x001806C4 File Offset: 0x0017E8C4
			public CraftingOrderSlots()
			{
				this.Slots = new CraftingOrder[6];
				for (int i = 0; i < 6; i++)
				{
					this.Slots[i] = null;
				}
				this._customOrders = new MBList<CraftingOrder>();
			}

			// Token: 0x0600572B RID: 22315 RVA: 0x00180703 File Offset: 0x0017E903
			[LoadInitializationCallback]
			private void OnLoad()
			{
				if (this._customOrders == null)
				{
					this._customOrders = new MBList<CraftingOrder>();
				}
			}

			// Token: 0x0600572C RID: 22316 RVA: 0x00180718 File Offset: 0x0017E918
			public bool IsThereAvailableSlot()
			{
				for (int i = 0; i < 6; i++)
				{
					if (this.Slots[i] == null)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600572D RID: 22317 RVA: 0x00180740 File Offset: 0x0017E940
			public int GetAvailableSlot()
			{
				for (int i = 0; i < 6; i++)
				{
					if (this.Slots[i] == null)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x0600572E RID: 22318 RVA: 0x00180766 File Offset: 0x0017E966
			internal void AddTownOrder(CraftingOrder craftingOrder)
			{
				this.Slots[craftingOrder.DifficultyLevel] = craftingOrder;
			}

			// Token: 0x0600572F RID: 22319 RVA: 0x00180776 File Offset: 0x0017E976
			internal void RemoveTownOrder(CraftingOrder craftingOrder)
			{
				this.Slots[craftingOrder.DifficultyLevel] = null;
			}

			// Token: 0x06005730 RID: 22320 RVA: 0x00180786 File Offset: 0x0017E986
			internal void AddCustomOrder(CraftingOrder order)
			{
				this._customOrders.Add(order);
			}

			// Token: 0x06005731 RID: 22321 RVA: 0x00180794 File Offset: 0x0017E994
			internal void RemoveCustomOrder(CraftingOrder order)
			{
				this._customOrders.Remove(order);
			}

			// Token: 0x06005732 RID: 22322 RVA: 0x001807A3 File Offset: 0x0017E9A3
			internal static void AutoGeneratedStaticCollectObjectsCraftingOrderSlots(object o, List<object> collectedObjects)
			{
				((CraftingCampaignBehavior.CraftingOrderSlots)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06005733 RID: 22323 RVA: 0x001807B1 File Offset: 0x0017E9B1
			protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				collectedObjects.Add(this.Slots);
				collectedObjects.Add(this._customOrders);
			}

			// Token: 0x06005734 RID: 22324 RVA: 0x001807CB File Offset: 0x0017E9CB
			internal static object AutoGeneratedGetMemberValueSlots(object o)
			{
				return ((CraftingCampaignBehavior.CraftingOrderSlots)o).Slots;
			}

			// Token: 0x06005735 RID: 22325 RVA: 0x001807D8 File Offset: 0x0017E9D8
			internal static object AutoGeneratedGetMemberValue_customOrders(object o)
			{
				return ((CraftingCampaignBehavior.CraftingOrderSlots)o)._customOrders;
			}

			// Token: 0x04001C45 RID: 7237
			private const int SlotCount = 6;

			// Token: 0x04001C46 RID: 7238
			[SaveableField(10)]
			public CraftingOrder[] Slots;

			// Token: 0x04001C47 RID: 7239
			[SaveableField(30)]
			private MBList<CraftingOrder> _customOrders;
		}
	}
}
