﻿using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Information.RundownTooltip;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x0200001C RID: 28
	public static class TooltipRefresherCollection
	{
		// Token: 0x0600019D RID: 413 RVA: 0x0000B644 File Offset: 0x00009844
		public static void RefreshExplainedNumberTooltip(RundownTooltipVM explainedNumberTooltip, object[] args)
		{
			explainedNumberTooltip.IsActive = explainedNumberTooltip.IsInitializedProperly;
			if (explainedNumberTooltip.IsActive)
			{
				Func<ExplainedNumber> func = args[0] as Func<ExplainedNumber>;
				Func<ExplainedNumber> func2 = args[1] as Func<ExplainedNumber>;
				explainedNumberTooltip.Lines.Clear();
				Func<ExplainedNumber> func3 = (explainedNumberTooltip.IsExtended && func2 != null) ? func2 : func;
				if (func3 != null)
				{
					ExplainedNumber explainedNumber = func3();
					explainedNumberTooltip.CurrentExpectedChange = explainedNumber.ResultNumber;
					foreach (ValueTuple<string, float> valueTuple in explainedNumber.GetLines())
					{
						explainedNumberTooltip.Lines.Add(new RundownLineVM(valueTuple.Item1, valueTuple.Item2));
					}
				}
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000B70C File Offset: 0x0000990C
		public static void RefreshTrackTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			Track track = args[0] as Track;
			propertyBasedTooltipVM.Mode = 1;
			MapTrackModel mapTrackModel = Campaign.Current.Models.MapTrackModel;
			if (mapTrackModel != null)
			{
				TextObject textObject = mapTrackModel.TrackTitle(track);
				propertyBasedTooltipVM.AddProperty("", textObject.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
				foreach (ValueTuple<TextObject, string> valueTuple in mapTrackModel.GetTrackDescription(track))
				{
					TextObject item = valueTuple.Item1;
					propertyBasedTooltipVM.AddProperty((item != null) ? item.ToString() : null, valueTuple.Item2, 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000B7B8 File Offset: 0x000099B8
		public static void RefreshHeroTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			Hero hero = args[0] as Hero;
			bool flag = (bool)args[1];
			StringHelpers.SetCharacterProperties("NPC", hero.CharacterObject, null, false);
			TextObject textObject;
			bool flag2 = CampaignUIHelper.IsHeroInformationHidden(hero, out textObject);
			if (hero.IsEnemy(Hero.MainHero))
			{
				propertyBasedTooltipVM.Mode = 3;
			}
			else if (hero == Hero.MainHero || hero.IsFriend(Hero.MainHero))
			{
				propertyBasedTooltipVM.Mode = 2;
			}
			else
			{
				propertyBasedTooltipVM.Mode = 1;
			}
			propertyBasedTooltipVM.AddProperty("", hero.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
			if (!hero.IsNotable && !hero.IsWanderer)
			{
				Clan clan = hero.Clan;
				if (((clan != null) ? clan.Kingdom : null) != null)
				{
					propertyBasedTooltipVM.AddProperty("", CampaignUIHelper.GetHeroKingdomRank(hero), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (Game.Current.IsDevelopmentMode)
				{
					Clan clan2 = hero.Clan;
					if (((clan2 != null) ? clan2.Leader : null) == hero)
					{
						propertyBasedTooltipVM.AddProperty("DEBUG Clan Leader", "", 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
				if (Game.Current.IsDevelopmentMode)
				{
					Clan clan3 = hero.Clan;
					if (((clan3 != null) ? clan3.Kingdom : null) != null)
					{
						Hero hero2 = hero;
						IFaction mapFaction = hero.MapFaction;
						if (hero2 == ((mapFaction != null) ? mapFaction.Leader : null))
						{
							Clan clan4 = hero.Clan;
							if (((clan4 != null) ? clan4.Kingdom : null) != null)
							{
								propertyBasedTooltipVM.AddProperty("DEBUG Kingdom Gold", hero.Clan.Kingdom.KingdomBudgetWallet.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
							}
						}
						propertyBasedTooltipVM.AddProperty("DEBUG Gold", hero.Gold.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
						if (Game.Current.IsDevelopmentMode && hero.Clan != null && hero.Clan.IsUnderMercenaryService)
						{
							propertyBasedTooltipVM.AddProperty("DEBUG Mercenary Award", hero.Clan.MercenaryAwardMultiplier.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
						}
						if (Game.Current.IsDevelopmentMode)
						{
							Clan clan5 = hero.Clan;
							if (((clan5 != null) ? clan5.Leader : null) == hero)
							{
								propertyBasedTooltipVM.AddProperty("DEBUG Debt To Kingdom", hero.Clan.DebtToKingdom.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
							}
						}
					}
				}
				if (Game.Current.IsDevelopmentMode && hero.PartyBelongedTo != null && !hero.IsSpecial)
				{
					propertyBasedTooltipVM.AddProperty("DEBUG Party Size", hero.PartyBelongedTo.MemberRoster.TotalManCount.ToString() + "/" + hero.PartyBelongedTo.Party.PartySizeLimit.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty("DEBUG Party Position", ((int)hero.PartyBelongedTo.Position2D.X).ToString() + "," + ((int)hero.PartyBelongedTo.Position2D.Y).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty("DEBUG Party Wage", hero.PartyBelongedTo.TotalWage.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (Game.Current.IsDevelopmentMode && hero.PartyBelongedTo != null)
				{
					propertyBasedTooltipVM.AddProperty("DEBUG Party Morale", hero.PartyBelongedTo.Morale.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (Game.Current.IsDevelopmentMode && hero.PartyBelongedTo != null)
				{
					propertyBasedTooltipVM.AddProperty("DEBUG Starving", hero.PartyBelongedTo.Party.IsStarving.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (Game.Current.IsDevelopmentMode)
				{
					IFaction mapFaction2 = hero.MapFaction;
					if (((mapFaction2 != null) ? mapFaction2.Leader : null) != null && hero != hero.MapFaction.Leader)
					{
						propertyBasedTooltipVM.AddProperty("DEBUG King Relation", hero.GetRelation(hero.MapFaction.Leader).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
				if (Game.Current.IsDevelopmentMode && hero.PartyBelongedToAsPrisoner != null)
				{
					propertyBasedTooltipVM.AddProperty("DEBUG Prisoner at", hero.PartyBelongedToAsPrisoner.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			if (hero.Clan != null)
			{
				propertyBasedTooltipVM.AddProperty("", hero.Clan.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			propertyBasedTooltipVM.AddProperty("", "", -1, TooltipProperty.TooltipPropertyFlags.None);
			if (!flag2)
			{
				int num = 0;
				foreach (Settlement settlement in Settlement.All)
				{
					if (settlement.IsTown)
					{
						Town town = settlement.Town;
						for (int i = 0; i < town.Workshops.Length; i++)
						{
							if (town.Workshops[i].Owner == hero && !town.Workshops[i].WorkshopType.IsHidden)
							{
								if (num == 0)
								{
									MBTextManager.SetTextVariable("STR1", new TextObject("{=VZjxs5Dt}Owner of ", null), false);
									MBTextManager.SetTextVariable("STR2", town.Workshops[i].WorkshopType.Name, false);
									string text = GameTexts.FindText("str_STR1_STR2", null).ToString();
									MBTextManager.SetTextVariable("LEFT", text, false);
									MBTextManager.SetTextVariable("PROPERTIES", text, false);
								}
								else
								{
									MBTextManager.SetTextVariable("RIGHT", town.Workshops[i].WorkshopType.Name, false);
									string text2 = GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString();
									MBTextManager.SetTextVariable("LEFT", text2, false);
									MBTextManager.SetTextVariable("PROPERTIES", text2, false);
								}
								num++;
							}
						}
					}
					if (settlement.IsTown || settlement.IsVillage)
					{
						foreach (Alley alley in settlement.Alleys)
						{
							if (alley.Owner == hero)
							{
								if (num == 0)
								{
									MBTextManager.SetTextVariable("STR1", new TextObject("{=VZjxs5Dt}Owner of ", null), false);
									MBTextManager.SetTextVariable("STR2", alley.Name, false);
									string text3 = GameTexts.FindText("str_STR1_STR2", null).ToString();
									MBTextManager.SetTextVariable("STR1", text3, false);
									MBTextManager.SetTextVariable("NUMBER_OF_MEN", Campaign.Current.Models.AlleyModel.GetTroopsOfAIOwnedAlley(alley).TotalManCount);
									MBTextManager.SetTextVariable("STR2", GameTexts.FindText("str_men_count_in_paranthesis_wo_wounded", null), false);
									text3 = GameTexts.FindText("str_STR1_STR2", null).ToString();
									MBTextManager.SetTextVariable("LEFT", text3, false);
									MBTextManager.SetTextVariable("PROPERTIES", text3, false);
								}
								else
								{
									MBTextManager.SetTextVariable("STR1", alley.Name, false);
									MBTextManager.SetTextVariable("NUMBER_OF_MEN", Campaign.Current.Models.AlleyModel.GetTroopsOfAIOwnedAlley(alley).TotalManCount);
									MBTextManager.SetTextVariable("STR2", GameTexts.FindText("str_men_count_in_paranthesis_wo_wounded", null), false);
									MBTextManager.SetTextVariable("RIGHT", GameTexts.FindText("str_STR1_STR2", null).ToString(), false);
									string text4 = GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString();
									MBTextManager.SetTextVariable("LEFT", text4, false);
									MBTextManager.SetTextVariable("PROPERTIES", text4, false);
								}
								num++;
							}
						}
					}
				}
				string value = new TextObject("{=j8uZBakZ}{PROPERTIES}", null).ToString();
				if (num > 0)
				{
					propertyBasedTooltipVM.AddProperty("", value, 0, TooltipProperty.TooltipPropertyFlags.MultiLine);
				}
				int num2 = 0;
				TextObject textObject2 = new TextObject("{=C2qpwFq5}Owner of {SETTLEMENTS}", null);
				foreach (Settlement settlement2 in Settlement.All)
				{
					if (settlement2.IsFortification && settlement2.OwnerClan != null && settlement2.OwnerClan.Leader == hero)
					{
						if (num2 == 0)
						{
							MBTextManager.SetTextVariable("SETTLEMENTS", settlement2.Name, false);
						}
						else
						{
							MBTextManager.SetTextVariable("RIGHT", settlement2.Name.ToString(), false);
							MBTextManager.SetTextVariable("LEFT", new TextObject("{=!}{SETTLEMENTS}", null).ToString(), false);
							MBTextManager.SetTextVariable("SETTLEMENTS", GameTexts.FindText("str_LEFT_comma_RIGHT", null).ToString(), false);
						}
						num2++;
					}
				}
				if (num2 > 0)
				{
					propertyBasedTooltipVM.AddProperty("", textObject2.ToString(), 0, TooltipProperty.TooltipPropertyFlags.MultiLine);
				}
				if (hero.OwnedCaravans.Count > 0)
				{
					TextObject textObject3 = new TextObject("{=TEkWkzbH}Owned Caravans: {CARAVAN_COUNT}", null);
					textObject3.SetTextVariable("CARAVAN_COUNT", hero.OwnedCaravans.Count);
					propertyBasedTooltipVM.AddProperty("", textObject3.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (hero.GovernorOf != null)
				{
					MBTextManager.SetTextVariable("STR1", new TextObject("{=jQdBl4hf}Governor of ", null), false);
					MBTextManager.SetTextVariable("STR2", hero.GovernorOf.Name, false);
					TextObject @object = GameTexts.FindText("str_STR1_STR2", null);
					propertyBasedTooltipVM.AddProperty("", new Func<string>(@object.ToString), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (hero != Hero.MainHero)
				{
					MBTextManager.SetTextVariable("LEFT", GameTexts.FindText("str_tooltip_label_relation", null), false);
					string definition = GameTexts.FindText("str_LEFT_ONLY", null).ToString();
					propertyBasedTooltipVM.AddProperty(definition, ((int)hero.GetRelationWithPlayer()).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			if (hero.HomeSettlement != null)
			{
				MBTextManager.SetTextVariable("LEFT", GameTexts.FindText("str_tooltip_label_home", null), false);
				string definition2 = GameTexts.FindText("str_LEFT_ONLY", null).ToString();
				propertyBasedTooltipVM.AddProperty(definition2, hero.HomeSettlement.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (hero.IsNotable)
			{
				MBTextManager.SetTextVariable("LEFT", GameTexts.FindText("str_notable_power", null), false);
				string definition3 = GameTexts.FindText("str_LEFT_colon", null).ToString();
				MBTextManager.SetTextVariable("RANK", Campaign.Current.Models.NotablePowerModel.GetPowerRankName(hero).ToString(), false);
				MBTextManager.SetTextVariable("NUMBER", ((int)hero.Power).ToString(), false);
				string value2 = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString();
				propertyBasedTooltipVM.AddProperty(definition3, value2, 0, TooltipProperty.TooltipPropertyFlags.None);
				if (Game.Current.IsDevelopmentMode)
				{
					propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.None);
					ExplainedNumber explainedNumber = Campaign.Current.Models.NotablePowerModel.CalculateDailyPowerChangeForHero(hero, true);
					propertyBasedTooltipVM.AddProperty("[DEV] Daily Power Change", explainedNumber.ResultNumber.ToString("+0.##;-0.##;0"), 0, TooltipProperty.TooltipPropertyFlags.RundownResult);
					propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
					foreach (ValueTuple<string, float> valueTuple in explainedNumber.GetLines())
					{
						string item = valueTuple.Item1;
						float item2 = valueTuple.Item2;
						propertyBasedTooltipVM.AddProperty("[DEV] " + item, item2.ToString("+0.##;-0.##;0"), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
					propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			MBTextManager.SetTextVariable("LEFT", GameTexts.FindText("str_tooltip_label_type", null), false);
			string definition4 = GameTexts.FindText("str_LEFT_ONLY", null).ToString();
			propertyBasedTooltipVM.AddProperty(definition4, HeroHelper.GetCharacterTypeName(hero).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			if (hero.CurrentSettlement != null && LocationComplex.Current != null && hero.CurrentSettlement == Hero.MainHero.CurrentSettlement && LocationComplex.Current.GetLocationOfCharacter(hero) != null)
			{
				MBTextManager.SetTextVariable("LEFT", GameTexts.FindText("str_tooltip_label_location", null), false);
				string definition5 = GameTexts.FindText("str_LEFT_ONLY", null).ToString();
				propertyBasedTooltipVM.AddProperty(definition5, LocationComplex.Current.GetLocationOfCharacter(hero).DoorName.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (hero.CurrentSettlement != null && hero.IsNotable && hero.SupporterOf != null)
			{
				MBTextManager.SetTextVariable("LEFT", GameTexts.FindText("str_tooltip_label_supporter_of", null), false);
				string definition6 = GameTexts.FindText("str_LEFT_ONLY", null).ToString();
				propertyBasedTooltipVM.AddProperty(definition6, hero.SupporterOf.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (flag)
			{
				List<ValueTuple<CampaignUIHelper.IssueQuestFlags, TextObject, TextObject>> questStateOfHero = CampaignUIHelper.GetQuestStateOfHero(hero);
				for (int j = 0; j < questStateOfHero.Count; j++)
				{
					string questExplanationOfHero = CampaignUIHelper.GetQuestExplanationOfHero(questStateOfHero[j].Item1);
					if (!string.IsNullOrEmpty(questExplanationOfHero))
					{
						propertyBasedTooltipVM.AddProperty("", "", -1, TooltipProperty.TooltipPropertyFlags.None);
						propertyBasedTooltipVM.AddProperty(questExplanationOfHero, questStateOfHero[j].Item2.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
			}
			if (!hero.IsAlive)
			{
				propertyBasedTooltipVM.AddProperty("", GameTexts.FindText("str_dead", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		public static void RefreshInventoryTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			InventoryLogic inventoryLogic = args[0] as InventoryLogic;
			propertyBasedTooltipVM.Mode = 0;
			List<ValueTuple<ItemRosterElement, int>> soldItems = inventoryLogic.GetSoldItems();
			List<ValueTuple<ItemRosterElement, int>> boughtItems = inventoryLogic.GetBoughtItems();
			TextObject textObject = new TextObject("{=bPFjmYCI}{SHOP_NAME} x {SHOP_DIFFERENCE_COUNT}", null);
			TextObject textObject2 = new TextObject("{=lxwGbRwu}x {SHOP_DIFFERENCE_COUNT}", null);
			TextObject textObject3 = inventoryLogic.IsTrading ? textObject : textObject2;
			int num = 0;
			int num2 = 40;
			foreach (ValueTuple<ItemRosterElement, int> valueTuple in soldItems)
			{
				if (num == num2)
				{
					break;
				}
				TextObject textObject4 = textObject3;
				string tag = "SHOP_NAME";
				ItemRosterElement item = valueTuple.Item1;
				textObject4.SetTextVariable(tag, item.EquipmentElement.GetModifiedItemName());
				TextObject textObject5 = textObject3;
				string tag2 = "SHOP_DIFFERENCE_COUNT";
				item = valueTuple.Item1;
				textObject5.SetTextVariable(tag2, item.Amount);
				if (inventoryLogic.IsTrading)
				{
					string definition = textObject3.ToString();
					string str = "+";
					int item2 = valueTuple.Item2;
					propertyBasedTooltipVM.AddColoredProperty(definition, str + item2.ToString(), UIColors.PositiveIndicator, 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				else
				{
					item = valueTuple.Item1;
					propertyBasedTooltipVM.AddColoredProperty(item.EquipmentElement.GetModifiedItemName().ToString(), textObject3.ToString(), UIColors.NegativeIndicator, 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				num++;
			}
			foreach (ValueTuple<ItemRosterElement, int> valueTuple2 in boughtItems)
			{
				if (num == num2)
				{
					break;
				}
				TextObject textObject6 = textObject3;
				string tag3 = "SHOP_NAME";
				ItemRosterElement item = valueTuple2.Item1;
				textObject6.SetTextVariable(tag3, item.EquipmentElement.GetModifiedItemName());
				TextObject textObject7 = textObject3;
				string tag4 = "SHOP_DIFFERENCE_COUNT";
				item = valueTuple2.Item1;
				textObject7.SetTextVariable(tag4, item.Amount);
				if (inventoryLogic.IsTrading)
				{
					propertyBasedTooltipVM.AddColoredProperty(textObject3.ToString(), (-valueTuple2.Item2).ToString(), UIColors.NegativeIndicator, 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				else
				{
					item = valueTuple2.Item1;
					propertyBasedTooltipVM.AddColoredProperty(item.EquipmentElement.GetModifiedItemName().ToString(), textObject3.ToString(), UIColors.PositiveIndicator, 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				num++;
			}
			if (num == num2)
			{
				int num3 = soldItems.Count + boughtItems.Count - num;
				if (num3 > 0)
				{
					TextObject textObject8 = new TextObject("{=OpsiBFCu}... and {COUNT} more items.", null);
					textObject8.SetTextVariable("COUNT", num3);
					propertyBasedTooltipVM.AddProperty("", textObject8.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000C740 File Offset: 0x0000A940
		public static void RefreshCraftingPartTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			WeaponDesignElement weaponDesignElement = args[0] as WeaponDesignElement;
			propertyBasedTooltipVM.Mode = 0;
			propertyBasedTooltipVM.AddProperty("", weaponDesignElement.CraftingPiece.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
			TextObject textObject = GameTexts.FindText("str_crafting_piece_type", weaponDesignElement.CraftingPiece.PieceType.ToString());
			propertyBasedTooltipVM.AddProperty("", textObject.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty(new TextObject("{=Oo3fkeab}Difficulty: ", null).ToString(), Campaign.Current.Models.SmithingModel.GetCraftingPartDifficulty(weaponDesignElement.CraftingPiece).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty(new TextObject("{=XUtiwiYP}Length: ", null).ToString(), MathF.Round(weaponDesignElement.CraftingPiece.Length * 100f, 2).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_weight_text", null).ToString(), MathF.Round(weaponDesignElement.CraftingPiece.Weight, 2).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			if (weaponDesignElement.CraftingPiece.PieceType == CraftingPiece.PieceTypes.Blade)
			{
				if (weaponDesignElement.CraftingPiece.BladeData.SwingDamageType != DamageTypes.Invalid)
				{
					DamageTypes swingDamageType = weaponDesignElement.CraftingPiece.BladeData.SwingDamageType;
					MBTextManager.SetTextVariable("SWING_DAMAGE_FACTOR", MathF.Round(weaponDesignElement.CraftingPiece.BladeData.SwingDamageFactor, 2) + " " + GameTexts.FindText("str_damage_types", swingDamageType.ToString()).ToString()[0].ToString(), false);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=nYYUQQm0}Swing Damage Factor ", null).ToString(), new TextObject("{=aTdrjrEh}{SWING_DAMAGE_FACTOR}", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (weaponDesignElement.CraftingPiece.BladeData.ThrustDamageType != DamageTypes.Invalid)
				{
					DamageTypes thrustDamageType = weaponDesignElement.CraftingPiece.BladeData.ThrustDamageType;
					MBTextManager.SetTextVariable("THRUST_DAMAGE_FACTOR", MathF.Round(weaponDesignElement.CraftingPiece.BladeData.ThrustDamageFactor, 2) + " " + GameTexts.FindText("str_damage_types", thrustDamageType.ToString()).ToString()[0].ToString(), false);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=KTKBKmvp}Thrust Damage Factor ", null).ToString(), new TextObject("{=DNq9bdvV}{THRUST_DAMAGE_FACTOR}", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			if (weaponDesignElement.CraftingPiece.ArmorBonus != 0)
			{
				propertyBasedTooltipVM.AddModifierProperty(new TextObject("{=7Xynf4IA}Hand Armor", null).ToString(), weaponDesignElement.CraftingPiece.ArmorBonus, 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (weaponDesignElement.CraftingPiece.SwingDamageBonus != 0)
			{
				propertyBasedTooltipVM.AddModifierProperty(new TextObject("{=QeToaiLt}Swing Damage", null).ToString(), weaponDesignElement.CraftingPiece.SwingDamageBonus, 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (weaponDesignElement.CraftingPiece.SwingSpeedBonus != 0)
			{
				propertyBasedTooltipVM.AddModifierProperty(new TextObject("{=sVZaIPoQ}Swing Speed", null).ToString(), weaponDesignElement.CraftingPiece.SwingSpeedBonus, 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (weaponDesignElement.CraftingPiece.ThrustDamageBonus != 0)
			{
				propertyBasedTooltipVM.AddModifierProperty(new TextObject("{=dO95yR9b}Thrust Damage", null).ToString(), weaponDesignElement.CraftingPiece.ThrustDamageBonus, 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (weaponDesignElement.CraftingPiece.ThrustSpeedBonus != 0)
			{
				propertyBasedTooltipVM.AddModifierProperty(new TextObject("{=4uMWNDoi}Thrust Speed", null).ToString(), weaponDesignElement.CraftingPiece.ThrustSpeedBonus, 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (weaponDesignElement.CraftingPiece.HandlingBonus != 0)
			{
				propertyBasedTooltipVM.AddModifierProperty(new TextObject("{=oibdTnXP}Handling", null).ToString(), weaponDesignElement.CraftingPiece.HandlingBonus, 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (weaponDesignElement.CraftingPiece.AccuracyBonus != 0)
			{
				propertyBasedTooltipVM.AddModifierProperty(new TextObject("{=TAnabTdy}Accuracy", null).ToString(), weaponDesignElement.CraftingPiece.AccuracyBonus, 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty(new TextObject("{=hr4MuPnt}Required Materials", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty("", string.Empty, -1, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
			foreach (ValueTuple<CraftingMaterials, int> valueTuple in weaponDesignElement.CraftingPiece.MaterialsUsed)
			{
				ItemObject craftingMaterialItem = Campaign.Current.Models.SmithingModel.GetCraftingMaterialItem(valueTuple.Item1);
				if (craftingMaterialItem != null)
				{
					string definition = craftingMaterialItem.Name.ToString();
					int item = valueTuple.Item2;
					propertyBasedTooltipVM.AddProperty(definition, item.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		public static void RefreshCharacterTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			CharacterObject characterObject = args[0] as CharacterObject;
			propertyBasedTooltipVM.Mode = 1;
			propertyBasedTooltipVM.AddProperty("", characterObject.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
			TextObject textObject = GameTexts.FindText("str_party_troop_tier", null);
			textObject.SetTextVariable("TIER_LEVEL", characterObject.Tier);
			propertyBasedTooltipVM.AddProperty("", textObject.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			if (characterObject.UpgradeTargets.Length != 0)
			{
				GameTexts.SetVariable("XP_AMOUNT", characterObject.GetUpgradeXpCost(PartyBase.MainParty, 0));
				propertyBasedTooltipVM.AddProperty("", GameTexts.FindText("str_required_xp_to_upgrade", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (characterObject.TroopWage > 0)
			{
				GameTexts.SetVariable("LEFT", GameTexts.FindText("str_wage", null));
				GameTexts.SetVariable("STR1", characterObject.TroopWage);
				GameTexts.SetVariable("STR2", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">");
				GameTexts.SetVariable("RIGHT", GameTexts.FindText("str_STR1_space_STR2", null));
				propertyBasedTooltipVM.AddProperty("", GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty("", GameTexts.FindText("str_skills", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
			foreach (SkillObject skillObject in Skills.All)
			{
				if (characterObject.GetSkillValue(skillObject) > 0)
				{
					propertyBasedTooltipVM.AddProperty(skillObject.Name.ToString(), characterObject.GetSkillValue(skillObject).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000CDA8 File Offset: 0x0000AFA8
		public static void RefreshItemTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			EquipmentElement? equipmentElement = args[0] as EquipmentElement?;
			ItemObject item = equipmentElement.Value.Item;
			propertyBasedTooltipVM.Mode = 1;
			propertyBasedTooltipVM.AddProperty("", item.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
			propertyBasedTooltipVM.AddProperty(new TextObject("{=zMMqgxb1}Type", null).ToString(), GameTexts.FindText("str_inventory_type_" + (int)item.Type, null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty(" ", " ", 0, TooltipProperty.TooltipPropertyFlags.None);
			if (Game.Current.IsDevelopmentMode)
			{
				if (item.Culture != null)
				{
					propertyBasedTooltipVM.AddProperty("Culture: ", item.Culture.StringId, 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				else
				{
					propertyBasedTooltipVM.AddProperty("Culture: ", "No Culture", 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				propertyBasedTooltipVM.AddProperty("ID: ", item.StringId, 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (item.RelevantSkill != null && item.Difficulty > 0)
			{
				propertyBasedTooltipVM.AddProperty(new TextObject("{=dWYm9GsC}Requires", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty(" ", " ", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
				propertyBasedTooltipVM.AddProperty(item.RelevantSkill.Name.ToString(), item.Difficulty.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty(" ", " ", 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			propertyBasedTooltipVM.AddProperty(new TextObject("{=4Dd2xgPm}Weight", null).ToString(), item.Weight.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			string text = "";
			if (item.IsUniqueItem)
			{
				text = text + GameTexts.FindText("str_inventory_flag_unique", null).ToString() + " ";
			}
			if (item.ItemFlags.HasAnyFlag(ItemFlags.NotUsableByFemale))
			{
				text = text + GameTexts.FindText("str_inventory_flag_male_only", null).ToString() + " ";
			}
			if (item.ItemFlags.HasAnyFlag(ItemFlags.NotUsableByMale))
			{
				text = text + GameTexts.FindText("str_inventory_flag_female_only", null).ToString() + " ";
			}
			if (!string.IsNullOrEmpty(text))
			{
				propertyBasedTooltipVM.AddProperty(new TextObject("{=eHVq6yDa}Item Properties", null).ToString(), text, 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (item.HasArmorComponent)
			{
				if (Campaign.Current != null)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=US7UmBbt}Armor Tier", null).ToString(), ((int)(item.Tier + 1)).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (item.ArmorComponent.HeadArmor != 0)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=O3dhjtOS}Head Armor", null).ToString(), equipmentElement.Value.GetModifiedHeadArmor().ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (item.ArmorComponent.BodyArmor != 0)
				{
					if (item.Type == ItemObject.ItemTypeEnum.HorseHarness)
					{
						propertyBasedTooltipVM.AddProperty(new TextObject("{=kftE5nvv}Horse Armor", null).ToString(), equipmentElement.Value.GetModifiedMountBodyArmor().ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
					else
					{
						propertyBasedTooltipVM.AddProperty(new TextObject("{=HkfY3Ds5}Body Armor", null).ToString(), equipmentElement.Value.GetModifiedBodyArmor().ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
				if (item.ArmorComponent.ArmArmor != 0)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=kx7q8ybD}Arm Armor", null).ToString(), equipmentElement.Value.GetModifiedArmArmor().ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (item.ArmorComponent.LegArmor != 0)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=eIws123Z}Leg Armor", null).ToString(), equipmentElement.Value.GetModifiedLegArmor().ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			else if (item.WeaponComponent != null && item.Weapons.Count > 0)
			{
				int num = (item.Weapons.Count > 1 && propertyBasedTooltipVM.IsExtended) ? 1 : 0;
				WeaponComponentData weaponComponentData = item.Weapons[num];
				propertyBasedTooltipVM.AddProperty(new TextObject("{=sqdzHOPe}Class", null).ToString(), GameTexts.FindText("str_inventory_weapon", ((int)weaponComponentData.WeaponClass).ToString()).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				if (Campaign.Current != null)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=hn9TPqhK}Weapon Tier", null).ToString(), ((int)(item.Tier + 1)).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				ItemObject.ItemTypeEnum itemTypeFromWeaponClass = WeaponComponentData.GetItemTypeFromWeaponClass(weaponComponentData.WeaponClass);
				if (itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.OneHandedWeapon || itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.TwoHandedWeapon || itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Polearm)
				{
					if (weaponComponentData.SwingDamageType != DamageTypes.Invalid)
					{
						propertyBasedTooltipVM.AddProperty(new TextObject("{=sVZaIPoQ}Swing Speed", null).ToString(), equipmentElement.Value.GetModifiedSwingSpeedForUsage(num).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
						propertyBasedTooltipVM.AddProperty(new TextObject("{=QeToaiLt}Swing Damage", null).ToString(), equipmentElement.Value.GetModifiedSwingDamageForUsage(num).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
					if (weaponComponentData.ThrustDamageType != DamageTypes.Invalid)
					{
						propertyBasedTooltipVM.AddProperty(new TextObject("{=4uMWNDoi}Thrust Speed", null).ToString(), equipmentElement.Value.GetModifiedThrustSpeedForUsage(num).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
						propertyBasedTooltipVM.AddProperty(new TextObject("{=dO95yR9b}Thrust Damage", null).ToString(), equipmentElement.Value.GetModifiedThrustDamageForUsage(num).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
					propertyBasedTooltipVM.AddProperty(new TextObject("{=ZcybPatO}Weapon Length", null).ToString(), weaponComponentData.WeaponLength.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=oibdTnXP}Handling", null).ToString(), weaponComponentData.Handling.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Thrown)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=ZcybPatO}Weapon Length", null).ToString(), weaponComponentData.WeaponLength.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=s31DnnAf}Damage", null).ToString(), ItemHelper.GetMissileDamageText(weaponComponentData, equipmentElement.Value.ItemModifier).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=bAqDnkaT}Missile Speed", null).ToString(), equipmentElement.Value.GetModifiedMissileSpeedForUsage(num).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=TAnabTdy}Accuracy", null).ToString(), weaponComponentData.Accuracy.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=twtbH1zv}Stack Amount", null).ToString(), equipmentElement.Value.GetModifiedStackCountForUsage(num).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Shield)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=6GSXsdeX}Speed", null).ToString(), equipmentElement.Value.GetModifiedSwingSpeedForUsage(num).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=oBbiVeKE}Hit Points", null).ToString(), equipmentElement.Value.GetModifiedMaximumHitPointsForUsage(num).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Bow || itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Crossbow)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=6GSXsdeX}Speed", null).ToString(), equipmentElement.Value.GetModifiedSwingSpeedForUsage(num).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=s31DnnAf}Damage", null).ToString(), ItemHelper.GetThrustDamageText(weaponComponentData, equipmentElement.Value.ItemModifier).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=TAnabTdy}Accuracy", null).ToString(), weaponComponentData.Accuracy.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=bAqDnkaT}Missile Speed", null).ToString(), equipmentElement.Value.GetModifiedMissileSpeedForUsage(num).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					if (itemTypeFromWeaponClass == ItemObject.ItemTypeEnum.Crossbow)
					{
						propertyBasedTooltipVM.AddProperty(new TextObject("{=cnmRwV4s}Ammo Limit", null).ToString(), weaponComponentData.MaxDataValue.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
				if (item != null && item.HasBannerComponent)
				{
					bool flag;
					if (item == null)
					{
						flag = (null != null);
					}
					else
					{
						BannerComponent bannerComponent = item.BannerComponent;
						flag = (((bannerComponent != null) ? bannerComponent.BannerEffect : null) != null);
					}
					TextObject textObject;
					if (flag)
					{
						GameTexts.SetVariable("RANK", item.BannerComponent.BannerEffect.Name);
						string content = string.Empty;
						if (item.BannerComponent.BannerEffect.IncrementType == BannerEffect.EffectIncrementType.AddFactor)
						{
							GameTexts.FindText("str_NUMBER_percent", null).SetTextVariable("NUMBER", ((int)Math.Abs(item.BannerComponent.GetBannerEffectBonus() * 100f)).ToString());
							object obj;
							content = obj.ToString();
						}
						else if (item.BannerComponent.BannerEffect.IncrementType == BannerEffect.EffectIncrementType.Add)
						{
							content = item.BannerComponent.GetBannerEffectBonus().ToString();
						}
						GameTexts.SetVariable("NUMBER", content);
						textObject = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null);
					}
					else
					{
						textObject = new TextObject("{=koX9okuG}None", null);
					}
					propertyBasedTooltipVM.AddProperty(new TextObject("{=DbXZjPdf}Banner Effect: ", null).ToString(), textObject.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (weaponComponentData.IsAmmo)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=TAnabTdy}Accuracy", null).ToString(), weaponComponentData.Accuracy.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=s31DnnAf}Damage", null).ToString(), ItemHelper.GetThrustDamageText(weaponComponentData, equipmentElement.Value.ItemModifier).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=twtbH1zv}Stack Amount", null).ToString(), equipmentElement.Value.GetModifiedStackCountForUsage(num).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (item.Weapons.Any(delegate(WeaponComponentData x)
				{
					string weaponDescriptionId = x.WeaponDescriptionId;
					return weaponDescriptionId != null && weaponDescriptionId.IndexOf("couch", StringComparison.OrdinalIgnoreCase) >= 0;
				}))
				{
					propertyBasedTooltipVM.AddProperty("", GameTexts.FindText("str_inventory_flag_couchable", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (item.Weapons.Any(delegate(WeaponComponentData x)
				{
					string weaponDescriptionId = x.WeaponDescriptionId;
					return weaponDescriptionId != null && weaponDescriptionId.IndexOf("bracing", StringComparison.OrdinalIgnoreCase) >= 0;
				}))
				{
					propertyBasedTooltipVM.AddProperty("", GameTexts.FindText("str_inventory_flag_braceable", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			else if (item.HasHorseComponent)
			{
				if (item.HorseComponent.IsMount)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=8BlMRMiR}Horse Tier", null).ToString(), ((int)(item.Tier + 1)).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=Mfbc4rQR}Charge Damage", null).ToString(), equipmentElement.Value.GetModifiedMountCharge(EquipmentElement.Invalid).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=6GSXsdeX}Speed", null).ToString(), equipmentElement.Value.GetModifiedMountSpeed(EquipmentElement.Invalid).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=rg7OuWS2}Maneuver", null).ToString(), equipmentElement.Value.GetModifiedMountManeuver(EquipmentElement.Invalid).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=oBbiVeKE}Hit Points", null).ToString(), equipmentElement.Value.GetModifiedMountHitPoints().ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=ZUgoQ1Ws}Horse Type", null).ToString(), item.ItemCategory.GetName().ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			else if (item.HasFoodComponent)
			{
				if (item.FoodComponent.MoraleBonus > 0)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=myMbtwXi}Morale Bonus", null).ToString(), item.FoodComponent.MoraleBonus.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (item.IsFood)
				{
					propertyBasedTooltipVM.AddProperty(new TextObject("{=qSi4DlT4}Food", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			if (item.HasBannerComponent)
			{
				BannerComponent bannerComponent2 = item.BannerComponent;
				if (((bannerComponent2 != null) ? bannerComponent2.BannerEffect : null) != null)
				{
					GameTexts.SetVariable("RANK", item.BannerComponent.BannerEffect.Name);
					string content2 = string.Empty;
					if (item.BannerComponent.BannerEffect.IncrementType == BannerEffect.EffectIncrementType.AddFactor)
					{
						GameTexts.FindText("str_NUMBER_percent", null).SetTextVariable("NUMBER", ((int)Math.Abs(item.BannerComponent.GetBannerEffectBonus() * 100f)).ToString());
						object obj2;
						content2 = obj2.ToString();
					}
					else if (item.BannerComponent.BannerEffect.IncrementType == BannerEffect.EffectIncrementType.Add)
					{
						content2 = item.BannerComponent.GetBannerEffectBonus().ToString();
					}
					GameTexts.SetVariable("NUMBER", content2);
					propertyBasedTooltipVM.AddProperty(new TextObject("{=DbXZjPdf}Banner Effect: ", null).ToString(), GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		public static void RefreshBuildingTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			Building building = args[0] as Building;
			propertyBasedTooltipVM.Mode = 1;
			propertyBasedTooltipVM.AddProperty("", building.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
			if (building.BuildingType.IsDefaultProject)
			{
				propertyBasedTooltipVM.AddProperty("", new TextObject("{=bd7oAQq6}Daily", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			else
			{
				propertyBasedTooltipVM.AddProperty(new TextObject("{=IJdjwXvn}Current Level: ", null).ToString(), building.CurrentLevel.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			propertyBasedTooltipVM.AddProperty("", building.Explanation.ToString(), 0, TooltipProperty.TooltipPropertyFlags.MultiLine);
			propertyBasedTooltipVM.AddProperty("", building.GetBonusExplanation().ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000DB38 File Offset: 0x0000BD38
		public static void RefreshWorkshopTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			Workshop workshop = args[0] as Workshop;
			propertyBasedTooltipVM.Mode = 1;
			propertyBasedTooltipVM.AddProperty("", workshop.WorkshopType.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
			propertyBasedTooltipVM.AddProperty(new TextObject("{=qRqnrtdX}Owner", null).ToString(), workshop.Owner.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, 0, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty(new TextObject("{=xtt9Oxer}Productions", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
			IEnumerable<ValueTuple<ItemCategory, int>> enumerable = workshop.WorkshopType.Productions.SelectMany((WorkshopType.Production p) => p.Inputs).Distinct(TooltipRefresherCollection.itemCategoryDistinctComparer);
			IEnumerable<ValueTuple<ItemCategory, int>> enumerable2 = workshop.WorkshopType.Productions.SelectMany((WorkshopType.Production p) => p.Outputs).Distinct(TooltipRefresherCollection.itemCategoryDistinctComparer);
			if (enumerable.Any<ValueTuple<ItemCategory, int>>())
			{
				propertyBasedTooltipVM.AddProperty(new TextObject("{=XCz81XYm}Inputs", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.DefaultSeperator);
				foreach (ValueTuple<ItemCategory, int> valueTuple in enumerable)
				{
					propertyBasedTooltipVM.AddProperty(" ", valueTuple.Item1.GetName().ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (enumerable2.Any<ValueTuple<ItemCategory, int>>())
			{
				propertyBasedTooltipVM.AddProperty(new TextObject("{=ErnykQEH}Outputs", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.DefaultSeperator);
				foreach (ValueTuple<ItemCategory, int> valueTuple2 in enumerable2)
				{
					propertyBasedTooltipVM.AddProperty(" ", valueTuple2.Item1.GetName().ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000DD80 File Offset: 0x0000BF80
		public static void RefreshEncounterTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			bool flag = (int)args[0] != 0;
			List<MobileParty> parties3 = new List<MobileParty>
			{
				MobileParty.MainParty
			};
			List<MobileParty> parties2 = new List<MobileParty>
			{
				Campaign.Current.ConversationManager.ConversationParty
			};
			PlayerEncounter.Current.FindAllNpcPartiesWhoWillJoinEvent(ref parties3, ref parties2);
			List<MobileParty> parties = null;
			if (!flag)
			{
				parties = parties3;
				propertyBasedTooltipVM.Mode = 2;
			}
			else
			{
				parties = parties2;
				propertyBasedTooltipVM.Mode = 3;
			}
			TroopRoster troopRoster = TroopRoster.CreateDummyTroopRoster();
			TroopRoster troopRoster2 = TroopRoster.CreateDummyTroopRoster();
			foreach (MobileParty mobileParty in parties)
			{
				for (int i = 0; i < mobileParty.MemberRoster.Count; i++)
				{
					TroopRosterElement elementCopyAtIndex = mobileParty.MemberRoster.GetElementCopyAtIndex(i);
					troopRoster.AddToCounts(elementCopyAtIndex.Character, elementCopyAtIndex.Number, false, elementCopyAtIndex.WoundedNumber, 0, true, -1);
				}
				for (int j = 0; j < mobileParty.PrisonRoster.Count; j++)
				{
					TroopRosterElement elementCopyAtIndex2 = mobileParty.PrisonRoster.GetElementCopyAtIndex(j);
					troopRoster2.AddToCounts(elementCopyAtIndex2.Character, elementCopyAtIndex2.Number, false, elementCopyAtIndex2.WoundedNumber, 0, true, -1);
				}
			}
			Func<TroopRoster> funcToDoBeforeLambda = delegate()
			{
				TroopRoster troopRoster3 = TroopRoster.CreateDummyTroopRoster();
				foreach (MobileParty mobileParty3 in parties)
				{
					for (int k = 0; k < mobileParty3.MemberRoster.Count; k++)
					{
						TroopRosterElement elementCopyAtIndex3 = mobileParty3.MemberRoster.GetElementCopyAtIndex(k);
						troopRoster3.AddToCounts(elementCopyAtIndex3.Character, elementCopyAtIndex3.Number, false, elementCopyAtIndex3.WoundedNumber, 0, true, -1);
					}
				}
				return troopRoster3;
			};
			Func<TroopRoster> funcToDoBeforeLambda2 = delegate()
			{
				TroopRoster troopRoster3 = TroopRoster.CreateDummyTroopRoster();
				foreach (MobileParty mobileParty3 in parties)
				{
					for (int k = 0; k < mobileParty3.PrisonRoster.Count; k++)
					{
						TroopRosterElement elementCopyAtIndex3 = mobileParty3.PrisonRoster.GetElementCopyAtIndex(k);
						troopRoster3.AddToCounts(elementCopyAtIndex3.Character, elementCopyAtIndex3.Number, false, elementCopyAtIndex3.WoundedNumber, 0, true, -1);
					}
				}
				return troopRoster3;
			};
			bool flag2 = false;
			foreach (MobileParty mobileParty2 in parties)
			{
				flag2 = (flag2 || mobileParty2.IsInspected);
				propertyBasedTooltipVM.AddProperty("", mobileParty2.Name.ToString(), 1, TooltipProperty.TooltipPropertyFlags.None);
				string a = mobileParty2.Name.ToString();
				IFaction mapFaction = mobileParty2.MapFaction;
				if (a != ((mapFaction != null) ? mapFaction.Name.ToString() : null))
				{
					string definition = "";
					IFaction mapFaction2 = mobileParty2.MapFaction;
					propertyBasedTooltipVM.AddProperty(definition, ((mapFaction2 != null) ? mapFaction2.Name.ToString() : null) ?? "", 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			if (troopRoster.Count > 0)
			{
				TooltipRefresherCollection.AddPartyTroopProperties(propertyBasedTooltipVM, troopRoster, GameTexts.FindText("str_map_tooltip_troops", null), flag2, funcToDoBeforeLambda);
			}
			if (troopRoster2.Count > 0)
			{
				TooltipRefresherCollection.AddPartyTroopProperties(propertyBasedTooltipVM, troopRoster2, GameTexts.FindText("str_map_tooltip_prisoners", null), flag2, funcToDoBeforeLambda2);
			}
			if (!Campaign.Current.IsMapTooltipLongForm && !propertyBasedTooltipVM.IsExtended)
			{
				propertyBasedTooltipVM.AddProperty("", "", -1, TooltipProperty.TooltipPropertyFlags.None);
				GameTexts.SetVariable("EXTEND_KEY", propertyBasedTooltipVM.GetKeyText(TooltipRefresherCollection.ExtendKeyId));
				propertyBasedTooltipVM.AddProperty("", GameTexts.FindText("str_map_tooltip_info", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000E064 File Offset: 0x0000C264
		public static void RefreshMapEventTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			MapEvent mapEvent = args[0] as MapEvent;
			propertyBasedTooltipVM.Mode = 4;
			TooltipProperty.TooltipPropertyFlags tooltipPropertyFlags;
			if (FactionManager.IsAtWarAgainstFaction(mapEvent.AttackerSide.LeaderParty.MapFaction, PartyBase.MainParty.MapFaction))
			{
				tooltipPropertyFlags = TooltipProperty.TooltipPropertyFlags.WarFirstEnemy;
			}
			else if (mapEvent.AttackerSide.LeaderParty.MapFaction == PartyBase.MainParty.MapFaction || FactionManager.IsAlliedWithFaction(mapEvent.AttackerSide.LeaderParty.MapFaction, PartyBase.MainParty.MapFaction))
			{
				tooltipPropertyFlags = TooltipProperty.TooltipPropertyFlags.WarFirstAlly;
			}
			else
			{
				tooltipPropertyFlags = TooltipProperty.TooltipPropertyFlags.WarFirstNeutral;
			}
			TooltipProperty.TooltipPropertyFlags tooltipPropertyFlags2;
			if (FactionManager.IsAtWarAgainstFaction(mapEvent.DefenderSide.LeaderParty.MapFaction, PartyBase.MainParty.MapFaction))
			{
				tooltipPropertyFlags2 = TooltipProperty.TooltipPropertyFlags.WarSecondEnemy;
			}
			else if (mapEvent.DefenderSide.LeaderParty.MapFaction == PartyBase.MainParty.MapFaction || FactionManager.IsAlliedWithFaction(mapEvent.DefenderSide.LeaderParty.MapFaction, PartyBase.MainParty.MapFaction))
			{
				tooltipPropertyFlags2 = TooltipProperty.TooltipPropertyFlags.WarSecondAlly;
			}
			else
			{
				tooltipPropertyFlags2 = TooltipProperty.TooltipPropertyFlags.WarSecondNeutral;
			}
			propertyBasedTooltipVM.AddProperty("", "", 1, tooltipPropertyFlags | tooltipPropertyFlags2);
			if (mapEvent.IsSiegeAssault)
			{
				TextObject textObject = new TextObject("{=43HYUImy}{SETTLEMENT}'s Siege", null);
				textObject.SetTextVariable("SETTLEMENT", mapEvent.MapEventSettlement.Name);
				propertyBasedTooltipVM.AddProperty("", textObject.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
			}
			else if (mapEvent.IsRaid)
			{
				TextObject textObject2 = new TextObject("{=T9bndUYP}{SETTLEMENT}'s Raid", null);
				textObject2.SetTextVariable("SETTLEMENT", mapEvent.MapEventSettlement.Name);
				propertyBasedTooltipVM.AddProperty("", textObject2.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
			}
			else
			{
				TextObject textObject3 = new TextObject("{=CnsIzaWo}Field Battle", null);
				propertyBasedTooltipVM.AddProperty("", textObject3.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
			}
			propertyBasedTooltipVM.AddProperty("", "", -1, TooltipProperty.TooltipPropertyFlags.None);
			TooltipRefresherCollection.AddEncounterParties(propertyBasedTooltipVM, mapEvent.AttackerSide.Parties, mapEvent.DefenderSide.Parties, propertyBasedTooltipVM.IsExtended);
			if (!propertyBasedTooltipVM.IsExtended)
			{
				propertyBasedTooltipVM.AddProperty("", "", -1, TooltipProperty.TooltipPropertyFlags.None);
				GameTexts.SetVariable("EXTEND_KEY", propertyBasedTooltipVM.GetKeyText(TooltipRefresherCollection.ExtendKeyId));
				propertyBasedTooltipVM.AddProperty("", GameTexts.FindText("str_map_tooltip_info", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
		public static void RefreshSettlementTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			TooltipRefresherCollection.<>c__DisplayClass15_0 CS$<>8__locals1 = new TooltipRefresherCollection.<>c__DisplayClass15_0();
			CS$<>8__locals1.settlement = (args[0] as Settlement);
			bool flag = (bool)args[1];
			CS$<>8__locals1.settlementAsParty = CS$<>8__locals1.settlement.Party;
			if (CS$<>8__locals1.settlementAsParty != null)
			{
				if (FactionManager.IsAtWarAgainstFaction(CS$<>8__locals1.settlementAsParty.MapFaction, PartyBase.MainParty.MapFaction))
				{
					propertyBasedTooltipVM.Mode = 3;
				}
				else if (CS$<>8__locals1.settlementAsParty.MapFaction == PartyBase.MainParty.MapFaction || FactionManager.IsAlliedWithFaction(CS$<>8__locals1.settlementAsParty.MapFaction, PartyBase.MainParty.MapFaction))
				{
					propertyBasedTooltipVM.Mode = 2;
				}
				else
				{
					propertyBasedTooltipVM.Mode = 1;
				}
				if (Game.Current.IsDevelopmentMode && CS$<>8__locals1.settlement.IsHideout)
				{
					propertyBasedTooltipVM.AddProperty("", string.Concat(new object[]
					{
						CS$<>8__locals1.settlement.Name,
						" (",
						CS$<>8__locals1.settlementAsParty.Id,
						")"
					}), 1, TooltipProperty.TooltipPropertyFlags.None);
				}
				else
				{
					propertyBasedTooltipVM.AddProperty("", CS$<>8__locals1.settlement.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
				}
				TextObject textObject;
				bool flag2 = !CampaignUIHelper.IsSettlementInformationHidden(CS$<>8__locals1.settlement, out textObject);
				propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_owner", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
				TextObject textObject2 = new TextObject("{=HAaElX8X}{PARTY_OWNERS_FACTION}", null);
				TextObject variable = (CS$<>8__locals1.settlement.OwnerClan == null) ? new TextObject("{=3PzgpFGq}Neutral", null) : CS$<>8__locals1.settlement.OwnerClan.Name;
				textObject2.SetTextVariable("PARTY_OWNERS_FACTION", variable);
				propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_clan", null).ToString(), textObject2.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				if (CS$<>8__locals1.settlementAsParty.MapFaction != null)
				{
					TextObject textObject3 = new TextObject("{=s6koeapc}{MAP_FACTION}", null);
					TextObject textObject4 = textObject3;
					string tag = "MAP_FACTION";
					IFaction mapFaction = CS$<>8__locals1.settlementAsParty.MapFaction;
					textObject4.SetTextVariable(tag, ((mapFaction != null) ? mapFaction.Name : null) ?? new TextObject("{=!}ERROR", null));
					propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_faction", null).ToString(), textObject3.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (CS$<>8__locals1.settlement.Culture != null && !TextObject.IsNullOrEmpty(CS$<>8__locals1.settlement.Culture.Name))
				{
					TextObject textObject5 = new TextObject("{=!}{CULTURE}", null);
					textObject5.SetTextVariable("CULTURE", CS$<>8__locals1.settlement.Culture.Name);
					propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_culture", null).ToString(), textObject5.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (flag2)
				{
					if (CS$<>8__locals1.settlementAsParty.IsSettlement && (CS$<>8__locals1.settlementAsParty.Settlement.IsVillage || CS$<>8__locals1.settlementAsParty.Settlement.IsTown || CS$<>8__locals1.settlementAsParty.Settlement.IsCastle))
					{
						propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
						propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_information", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
						propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
					}
					if (CS$<>8__locals1.settlement.IsVillage || CS$<>8__locals1.settlement.IsFortification)
					{
						propertyBasedTooltipVM.AddProperty(CS$<>8__locals1.settlementAsParty.Settlement.IsFortification ? GameTexts.FindText("str_map_tooltip_prosperity", null).ToString() : GameTexts.FindText("str_map_tooltip_hearths", null).ToString(), new Func<string>(CS$<>8__locals1.<RefreshSettlementTooltip>g__getProsperity|0), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
					if (CS$<>8__locals1.settlement.IsFortification)
					{
						propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_loyalty", null).ToString(), new Func<string>(CS$<>8__locals1.<RefreshSettlementTooltip>g__getLoyalty|1), 0, TooltipProperty.TooltipPropertyFlags.None);
						propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_security", null).ToString(), CS$<>8__locals1.<RefreshSettlementTooltip>g__getSecurity|2(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
					if (CS$<>8__locals1.settlement.IsVillage || CS$<>8__locals1.settlement.IsFortification)
					{
						propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_map_tooltip_militia", null).ToString(), new Func<string>(CS$<>8__locals1.<RefreshSettlementTooltip>g__getMilitia|3), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
					if (CS$<>8__locals1.settlement.IsFortification)
					{
						propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_map_tooltip_garrison", null).ToString(), new Func<string>(CS$<>8__locals1.<RefreshSettlementTooltip>g__getGarrison|6), 0, TooltipProperty.TooltipPropertyFlags.None);
						propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_map_tooltip_food_stocks", null).ToString(), new Func<string>(CS$<>8__locals1.<RefreshSettlementTooltip>g__getFood|7), 0, TooltipProperty.TooltipPropertyFlags.None);
						int wallLevel = CS$<>8__locals1.settlementAsParty.Settlement.Town.GetWallLevel();
						propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_map_tooltip_wall_level", null).ToString(), wallLevel.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
				if (CS$<>8__locals1.settlement.IsVillage)
				{
					string definition = GameTexts.FindText("str_bound_settlement", null).ToString();
					string value = CS$<>8__locals1.settlementAsParty.Settlement.Village.Bound.Name.ToString();
					propertyBasedTooltipVM.AddProperty(definition, value, 0, TooltipProperty.TooltipPropertyFlags.None);
					if (CS$<>8__locals1.settlementAsParty.Settlement.Village.TradeBound != null)
					{
						string definition2 = GameTexts.FindText("str_trade_bound_settlement", null).ToString();
						string value2 = CS$<>8__locals1.settlementAsParty.Settlement.Village.TradeBound.Name.ToString();
						propertyBasedTooltipVM.AddProperty(definition2, value2, 0, TooltipProperty.TooltipPropertyFlags.None);
					}
					ItemObject primaryProduction = CS$<>8__locals1.settlementAsParty.Settlement.Village.VillageType.PrimaryProduction;
					propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_primary_production", null).ToString(), primaryProduction.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (CS$<>8__locals1.settlement.BoundVillages.Count > 0)
				{
					string text = "";
					string definition3 = GameTexts.FindText("str_bound_village", null).ToString();
					if (CS$<>8__locals1.settlementAsParty.Settlement.BoundVillages.Count == 1)
					{
						text = CS$<>8__locals1.settlementAsParty.Settlement.BoundVillages[0].Name.ToString();
					}
					else
					{
						for (int i = 0; i < CS$<>8__locals1.settlementAsParty.Settlement.BoundVillages.Count; i++)
						{
							if (i + 1 != CS$<>8__locals1.settlementAsParty.Settlement.BoundVillages.Count)
							{
								text = text + CS$<>8__locals1.settlementAsParty.Settlement.BoundVillages[i].Name.ToString() + ",\n";
							}
							else
							{
								text += CS$<>8__locals1.settlementAsParty.Settlement.BoundVillages[i].Name.ToString();
							}
						}
					}
					propertyBasedTooltipVM.AddProperty(definition3, text, 0, TooltipProperty.TooltipPropertyFlags.None);
					if (propertyBasedTooltipVM.IsExtended && CS$<>8__locals1.settlement.IsTown && CS$<>8__locals1.settlement.Town.TradeBoundVillages.Count > 0)
					{
						string text2 = "";
						string definition4 = GameTexts.FindText("str_trade_bound_village", null).ToString();
						for (int j = 0; j < CS$<>8__locals1.settlement.Town.TradeBoundVillages.Count; j++)
						{
							if (j + 1 != CS$<>8__locals1.settlement.Town.TradeBoundVillages.Count)
							{
								text2 = text2 + CS$<>8__locals1.settlement.Town.TradeBoundVillages[j].Name.ToString() + ",\n";
							}
							else
							{
								text2 += CS$<>8__locals1.settlement.Town.TradeBoundVillages[j].Name.ToString();
							}
						}
						propertyBasedTooltipVM.AddProperty(definition4, text2, 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
				if (Game.Current.IsDevelopmentMode && CS$<>8__locals1.settlement.IsTown)
				{
					propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty("[DEV] " + GameTexts.FindText("str_shops", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
					int num = 1;
					foreach (Workshop workshop in CS$<>8__locals1.settlementAsParty.Settlement.Town.Workshops)
					{
						if (workshop.WorkshopType != null)
						{
							propertyBasedTooltipVM.AddProperty("[DEV] Shop " + num.ToString(), workshop.WorkshopType.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
							num++;
						}
					}
				}
				TroopRoster troopRoster = TroopRoster.CreateDummyTroopRoster();
				TroopRoster troopRoster2 = TroopRoster.CreateDummyTroopRoster();
				TroopRoster.CreateDummyTroopRoster();
				Func<TroopRoster> func = delegate()
				{
					TroopRoster troopRoster3 = TroopRoster.CreateDummyTroopRoster();
					foreach (MobileParty mobileParty4 in CS$<>8__locals1.settlement.Parties)
					{
						if (!FactionManager.IsAtWarAgainstFaction(mobileParty4.MapFaction, CS$<>8__locals1.settlementAsParty.MapFaction) && (mobileParty4.Aggressiveness >= 0.01f || mobileParty4.IsGarrison || mobileParty4.IsMilitia) && !mobileParty4.IsMainParty)
						{
							for (int m = 0; m < mobileParty4.MemberRoster.Count; m++)
							{
								TroopRosterElement elementCopyAtIndex = mobileParty4.MemberRoster.GetElementCopyAtIndex(m);
								troopRoster3.AddToCounts(elementCopyAtIndex.Character, elementCopyAtIndex.Number, false, elementCopyAtIndex.WoundedNumber, 0, true, -1);
							}
						}
					}
					return troopRoster3;
				};
				Func<TroopRoster> func2 = delegate()
				{
					TroopRoster troopRoster3 = TroopRoster.CreateDummyTroopRoster();
					foreach (MobileParty mobileParty4 in CS$<>8__locals1.settlement.Parties)
					{
						if (!mobileParty4.IsMainParty && !FactionManager.IsAtWarAgainstFaction(mobileParty4.MapFaction, CS$<>8__locals1.settlementAsParty.MapFaction))
						{
							for (int m = 0; m < mobileParty4.PrisonRoster.Count; m++)
							{
								TroopRosterElement elementCopyAtIndex = mobileParty4.PrisonRoster.GetElementCopyAtIndex(m);
								troopRoster3.AddToCounts(elementCopyAtIndex.Character, elementCopyAtIndex.Number, false, elementCopyAtIndex.WoundedNumber, 0, true, -1);
							}
						}
					}
					for (int n = 0; n < CS$<>8__locals1.settlementAsParty.PrisonRoster.Count; n++)
					{
						TroopRosterElement elementCopyAtIndex2 = CS$<>8__locals1.settlementAsParty.PrisonRoster.GetElementCopyAtIndex(n);
						troopRoster3.AddToCounts(elementCopyAtIndex2.Character, elementCopyAtIndex2.Number, false, elementCopyAtIndex2.WoundedNumber, 0, true, -1);
					}
					return troopRoster3;
				};
				troopRoster2 = func2();
				if (propertyBasedTooltipVM.IsExtended)
				{
					troopRoster = func();
					if (troopRoster.Count > 0)
					{
						TooltipRefresherCollection.AddPartyTroopProperties(propertyBasedTooltipVM, troopRoster, GameTexts.FindText("str_map_tooltip_troops", null), flag || flag2, func);
					}
				}
				else
				{
					propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
					if (!CS$<>8__locals1.settlement.IsHideout && (flag2 || flag))
					{
						List<MobileParty> list = new List<MobileParty>();
						Town town = CS$<>8__locals1.settlement.Town;
						bool flag3 = town == null || !town.InRebelliousState;
						for (int l = 0; l < CS$<>8__locals1.settlement.Parties.Count; l++)
						{
							MobileParty mobileParty = CS$<>8__locals1.settlement.Parties[l];
							bool flag4 = flag3 && mobileParty.IsMilitia;
							if (FactionManager.IsAlliedWithFaction(CS$<>8__locals1.settlementAsParty.MapFaction, mobileParty.MapFaction) && (mobileParty.IsLordParty || flag4 || mobileParty.IsGarrison))
							{
								list.Add(mobileParty);
							}
						}
						list.Sort(CampaignUIHelper.MobilePartyPrecedenceComparerInstance);
						List<MobileParty> list2 = (from p in CS$<>8__locals1.settlement.Parties
						where !p.IsLordParty && !p.IsMilitia && !p.IsGarrison
						select p).ToList<MobileParty>();
						list2.Sort(CampaignUIHelper.MobilePartyPrecedenceComparerInstance);
						if (list.Count > 0)
						{
							int num2 = list.Sum((MobileParty p) => p.Party.NumberOfHealthyMembers);
							int num3 = list.Sum((MobileParty p) => p.Party.NumberOfWoundedTotalMembers);
							string value3 = num2 + ((num3 > 0) ? ("+" + num3 + GameTexts.FindText("str_party_nameplate_wounded_abbr", null).ToString()) : "");
							propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_map_tooltip_defenders", null).ToString(), value3, 0, TooltipProperty.TooltipPropertyFlags.None);
							propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
							foreach (MobileParty mobileParty2 in list)
							{
								propertyBasedTooltipVM.AddProperty(mobileParty2.Name.ToString(), CampaignUIHelper.GetPartyNameplateText(mobileParty2, false), 0, TooltipProperty.TooltipPropertyFlags.None);
							}
							propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
						}
						if (list2.Count <= 0)
						{
							goto IL_BCB;
						}
						propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.DefaultSeperator);
						using (List<MobileParty>.Enumerator enumerator = list2.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								MobileParty mobileParty3 = enumerator.Current;
								propertyBasedTooltipVM.AddProperty(mobileParty3.Name.ToString(), CampaignUIHelper.GetPartyNameplateText(mobileParty3, false), 0, TooltipProperty.TooltipPropertyFlags.None);
							}
							goto IL_BCB;
						}
					}
					string value4 = GameTexts.FindText("str_missing_info_indicator", null).ToString();
					propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_map_tooltip_parties", null).ToString(), value4, 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				IL_BCB:
				if (!CS$<>8__locals1.settlement.IsHideout && troopRoster2.Count > 0 && (flag2 || flag))
				{
					TooltipRefresherCollection.AddPartyTroopProperties(propertyBasedTooltipVM, troopRoster2, GameTexts.FindText("str_map_tooltip_prisoners", null), flag2, func2);
				}
				if (CS$<>8__locals1.settlement.IsFortification && CS$<>8__locals1.settlement.Town.InRebelliousState)
				{
					propertyBasedTooltipVM.AddProperty(string.Empty, GameTexts.FindText("str_settlement_rebellious_state", null).ToString(), -1, TooltipProperty.TooltipPropertyFlags.None);
				}
				propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
				if (!CS$<>8__locals1.settlement.IsHideout && !propertyBasedTooltipVM.IsExtended && (flag2 || flag))
				{
					GameTexts.SetVariable("EXTEND_KEY", propertyBasedTooltipVM.GetKeyText(TooltipRefresherCollection.ExtendKeyId));
					propertyBasedTooltipVM.AddProperty(string.Empty, GameTexts.FindText("str_map_tooltip_info", null).ToString(), -1, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000EF64 File Offset: 0x0000D164
		public static void RefreshMobilePartyTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			MobileParty mobileParty = args[0] as MobileParty;
			bool flag = (bool)args[1];
			bool flag2 = (bool)args[2];
			if (mobileParty != null)
			{
				if (FactionManager.IsAtWarAgainstFaction(mobileParty.MapFaction, PartyBase.MainParty.MapFaction))
				{
					propertyBasedTooltipVM.Mode = 3;
				}
				else if (mobileParty.MapFaction == PartyBase.MainParty.MapFaction || FactionManager.IsAlliedWithFaction(mobileParty.MapFaction, PartyBase.MainParty.MapFaction))
				{
					propertyBasedTooltipVM.Mode = 2;
				}
				else
				{
					propertyBasedTooltipVM.Mode = 1;
				}
				if (Game.Current.IsDevelopmentMode)
				{
					propertyBasedTooltipVM.AddProperty("", string.Concat(new object[]
					{
						mobileParty.Name,
						" (",
						mobileParty.Id,
						")"
					}), 1, TooltipProperty.TooltipPropertyFlags.Title);
				}
				else
				{
					propertyBasedTooltipVM.AddProperty("", mobileParty.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
				}
				bool isInspected = mobileParty.IsInspected;
				if (mobileParty.IsDisorganized)
				{
					TextObject hoursAndDaysTextFromHourValue = CampaignUIHelper.GetHoursAndDaysTextFromHourValue(MathF.Ceiling(mobileParty.DisorganizedUntilTime.RemainingHoursFromNow));
					TextObject textObject = new TextObject("{=BbLTwhsA}Disorganized for {REMAINING_TIME}", null);
					textObject.SetTextVariable("REMAINING_TIME", hoursAndDaysTextFromHourValue.ToString());
					propertyBasedTooltipVM.AddProperty("", textObject.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty("", "", -1, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (isInspected)
				{
					propertyBasedTooltipVM.AddProperty("", CampaignUIHelper.GetMobilePartyBehaviorText(mobileParty), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_owner", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
				if (mobileParty.LeaderHero != null && mobileParty.LeaderHero.Clan != mobileParty.MapFaction)
				{
					TextObject textObject2 = new TextObject("{=oUhd9YhP}{PARTY_LEADERS_FACTION}", null);
					textObject2.SetTextVariable("PARTY_LEADERS_FACTION", mobileParty.LeaderHero.Clan.Name);
					propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_clan", null).ToString(), textObject2.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (mobileParty.MapFaction != null)
				{
					TextObject textObject3 = new TextObject("{=s6koeapc}{MAP_FACTION}", null);
					TextObject textObject4 = textObject3;
					string tag = "MAP_FACTION";
					IFaction mapFaction = mobileParty.MapFaction;
					textObject4.SetTextVariable(tag, ((mapFaction != null) ? mapFaction.Name : null) ?? new TextObject("{=!}ERROR", null));
					propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_faction", null).ToString(), textObject3.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (isInspected)
				{
					propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_information", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
					propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_map_tooltip_speed", null).ToString(), CampaignUIHelper.FloatToString(mobileParty.Speed), 0, TooltipProperty.TooltipPropertyFlags.None);
					if (propertyBasedTooltipVM.IsExtended)
					{
						TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(mobileParty.CurrentNavigationFace);
						string definition = GameTexts.FindText("str_terrain", null).ToString();
						string id = "str_terrain_types";
						int num = (int)faceTerrainType;
						propertyBasedTooltipVM.AddProperty(definition, GameTexts.FindText(id, num.ToString()).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
				TroopRoster troopRoster = TroopRoster.CreateDummyTroopRoster();
				TroopRoster troopRoster2 = TroopRoster.CreateDummyTroopRoster();
				TroopRoster.CreateDummyTroopRoster();
				Func<TroopRoster> func = delegate()
				{
					TroopRoster troopRoster3 = TroopRoster.CreateDummyTroopRoster();
					for (int i = 0; i < mobileParty.MemberRoster.Count; i++)
					{
						TroopRosterElement elementCopyAtIndex = mobileParty.MemberRoster.GetElementCopyAtIndex(i);
						troopRoster3.AddToCounts(elementCopyAtIndex.Character, elementCopyAtIndex.Number, false, elementCopyAtIndex.WoundedNumber, 0, true, -1);
					}
					return troopRoster3;
				};
				Func<TroopRoster> func2 = delegate()
				{
					TroopRoster troopRoster3 = TroopRoster.CreateDummyTroopRoster();
					for (int i = 0; i < mobileParty.PrisonRoster.Count; i++)
					{
						TroopRosterElement elementCopyAtIndex = mobileParty.PrisonRoster.GetElementCopyAtIndex(i);
						troopRoster3.AddToCounts(elementCopyAtIndex.Character, elementCopyAtIndex.Number, false, elementCopyAtIndex.WoundedNumber, 0, true, -1);
					}
					return troopRoster3;
				};
				troopRoster = func();
				troopRoster2 = func2();
				if (troopRoster.Count > 0)
				{
					TooltipRefresherCollection.AddPartyTroopProperties(propertyBasedTooltipVM, troopRoster, GameTexts.FindText("str_map_tooltip_troops", null), flag || isInspected || !flag2, func);
				}
				if (troopRoster2.Count > 0 && (isInspected || flag))
				{
					TooltipRefresherCollection.AddPartyTroopProperties(propertyBasedTooltipVM, troopRoster2, GameTexts.FindText("str_map_tooltip_prisoners", null), isInspected || !flag2, func2);
				}
				propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
				if (!propertyBasedTooltipVM.IsExtended && (isInspected || flag))
				{
					GameTexts.SetVariable("EXTEND_KEY", propertyBasedTooltipVM.GetKeyText(TooltipRefresherCollection.ExtendKeyId));
					propertyBasedTooltipVM.AddProperty(string.Empty, GameTexts.FindText("str_map_tooltip_info", null).ToString(), -1, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (mobileParty != MobileParty.MainParty && !flag)
				{
					GameTexts.SetVariable("MODIFIER_KEY", propertyBasedTooltipVM.GetKeyText(TooltipRefresherCollection.FollowModifierKeyId));
					GameTexts.SetVariable("CLICK_KEY", propertyBasedTooltipVM.GetKeyText(TooltipRefresherCollection.MapClickKeyId));
					propertyBasedTooltipVM.AddProperty(string.Empty, GameTexts.FindText("str_map_follow_party_info", null).ToString(), -1, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000F458 File Offset: 0x0000D658
		public static void RefreshArmyTooltip(PropertyBasedTooltipVM propertyBasedTooltipVM, object[] args)
		{
			TooltipRefresherCollection.<>c__DisplayClass17_0 CS$<>8__locals1 = new TooltipRefresherCollection.<>c__DisplayClass17_0();
			CS$<>8__locals1.army = (args[0] as Army);
			bool flag = (bool)args[1];
			bool flag2 = (bool)args[2];
			MobileParty leaderParty = CS$<>8__locals1.army.LeaderParty;
			if (leaderParty != null)
			{
				if (FactionManager.IsAtWarAgainstFaction(leaderParty.MapFaction, PartyBase.MainParty.MapFaction))
				{
					propertyBasedTooltipVM.Mode = 3;
				}
				else if (CS$<>8__locals1.army.Kingdom == PartyBase.MainParty.MapFaction || FactionManager.IsAlliedWithFaction(leaderParty.MapFaction, PartyBase.MainParty.MapFaction))
				{
					propertyBasedTooltipVM.Mode = 2;
				}
				else
				{
					propertyBasedTooltipVM.Mode = 1;
				}
				propertyBasedTooltipVM.AddProperty("", CS$<>8__locals1.army.Name.ToString(), 0, TooltipProperty.TooltipPropertyFlags.Title);
				if (leaderParty.IsInspected || !flag2)
				{
					propertyBasedTooltipVM.AddProperty("", CampaignUIHelper.GetMobilePartyBehaviorText(leaderParty), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_owner", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
				if (CS$<>8__locals1.army.Kingdom != null)
				{
					TextObject textObject = new TextObject("{=s6koeapc}{MAP_FACTION}", null);
					TextObject textObject2 = textObject;
					string tag = "MAP_FACTION";
					Kingdom kingdom = CS$<>8__locals1.army.Kingdom;
					textObject2.SetTextVariable(tag, ((kingdom != null) ? kingdom.Name : null) ?? new TextObject("{=!}ERROR", null));
					propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_faction", null).ToString(), textObject.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
				if (leaderParty.IsInspected || !flag2)
				{
					propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_information", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
					propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
					propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_map_tooltip_speed", null).ToString(), CampaignUIHelper.FloatToString(leaderParty.Speed), 0, TooltipProperty.TooltipPropertyFlags.None);
					if (propertyBasedTooltipVM.IsExtended)
					{
						TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(leaderParty.CurrentNavigationFace);
						string definition = GameTexts.FindText("str_terrain", null).ToString();
						string id = "str_terrain_types";
						int num = (int)faceTerrainType;
						propertyBasedTooltipVM.AddProperty(definition, GameTexts.FindText(id, num.ToString()).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
				TroopRoster troopRoster = CS$<>8__locals1.<RefreshArmyTooltip>g__GetTempRoster|0();
				TroopRoster troopRoster2 = CS$<>8__locals1.<RefreshArmyTooltip>g__GetTempPrisonerRoster|1();
				if (troopRoster.Count > 0)
				{
					TooltipRefresherCollection.AddPartyTroopProperties(propertyBasedTooltipVM, troopRoster, GameTexts.FindText("str_map_tooltip_troops", null), flag || leaderParty.IsInspected, new Func<TroopRoster>(CS$<>8__locals1.<RefreshArmyTooltip>g__GetTempRoster|0));
				}
				if (troopRoster2.Count > 0 && (leaderParty.IsInspected || flag || !flag2))
				{
					TooltipRefresherCollection.AddPartyTroopProperties(propertyBasedTooltipVM, troopRoster2, GameTexts.FindText("str_map_tooltip_prisoners", null), leaderParty.IsInspected || !flag2, new Func<TroopRoster>(CS$<>8__locals1.<RefreshArmyTooltip>g__GetTempPrisonerRoster|1));
				}
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000F73C File Offset: 0x0000D93C
		private static void AddEncounterParties(PropertyBasedTooltipVM propertyBasedTooltipVM, MBReadOnlyList<MapEventParty> parties1, MBReadOnlyList<MapEventParty> parties2, bool isExtended)
		{
			propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.BattleMode);
			int num = 0;
			while (num < parties1.Count || num < parties2.Count)
			{
				MBTextManager.SetTextVariable("PARTY_1S_MEMBERS", "", false);
				MBTextManager.SetTextVariable("PARTY_2S_MEMBERS", "", false);
				if (num < parties1.Count)
				{
					MBTextManager.SetTextVariable("PARTY_1S_MEMBERS", parties1[num].Party.Name, false);
				}
				if (num < parties2.Count)
				{
					MBTextManager.SetTextVariable("PARTY_2S_MEMBERS", parties2[num].Party.Name, false);
				}
				propertyBasedTooltipVM.AddProperty(new TextObject("{=CExQ40Ux}{PARTY_1S_MEMBERS}   ", null).ToString(), new TextObject("{=OTaPfaJl}{PARTY_2S_MEMBERS}   ", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				num++;
			}
			if (parties1.Count > 0 && parties2.Count > 0)
			{
				PartyBase party = parties1[0].Party;
				if (((party != null) ? party.MapFaction : null) != null)
				{
					PartyBase party2 = parties2[0].Party;
					if (((party2 != null) ? party2.MapFaction : null) != null)
					{
						propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.DefaultSeperator);
						MBTextManager.SetTextVariable("PARTY_1S_MEMBERS", parties1[0].Party.MapFaction.Name, false);
						MBTextManager.SetTextVariable("PARTY_2S_MEMBERS", parties2[0].Party.MapFaction.Name, false);
						propertyBasedTooltipVM.AddProperty(new TextObject("{=CExQ40Ux}{PARTY_1S_MEMBERS}   ", null).ToString(), new TextObject("{=OTaPfaJl}{PARTY_2S_MEMBERS}   ", null).ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
			}
			int lastHeroIndex = 0;
			TroopRoster troopRoster = TroopRoster.CreateDummyTroopRoster();
			foreach (MapEventParty mapEventParty in parties1)
			{
				for (int i = 0; i < mapEventParty.Party.MemberRoster.Count; i++)
				{
					TroopRosterElement elementCopyAtIndex = mapEventParty.Party.MemberRoster.GetElementCopyAtIndex(i);
					if (elementCopyAtIndex.Character.IsHero)
					{
						troopRoster.AddToCounts(elementCopyAtIndex.Character, elementCopyAtIndex.Number, false, elementCopyAtIndex.WoundedNumber, 0, true, lastHeroIndex);
						int lastHeroIndex3 = lastHeroIndex;
						lastHeroIndex = lastHeroIndex3 + 1;
					}
					else
					{
						troopRoster.AddToCounts(elementCopyAtIndex.Character, elementCopyAtIndex.Number, false, elementCopyAtIndex.WoundedNumber, 0, true, -1);
					}
				}
			}
			lastHeroIndex = 0;
			TroopRoster troopRoster2 = TroopRoster.CreateDummyTroopRoster();
			foreach (MapEventParty mapEventParty2 in parties2)
			{
				for (int j = 0; j < mapEventParty2.Party.MemberRoster.Count; j++)
				{
					TroopRosterElement elementCopyAtIndex2 = mapEventParty2.Party.MemberRoster.GetElementCopyAtIndex(j);
					if (elementCopyAtIndex2.Character.IsHero)
					{
						troopRoster2.AddToCounts(elementCopyAtIndex2.Character, elementCopyAtIndex2.Number, false, elementCopyAtIndex2.WoundedNumber, 0, true, lastHeroIndex);
						int lastHeroIndex3 = lastHeroIndex;
						lastHeroIndex = lastHeroIndex3 + 1;
					}
					else
					{
						troopRoster2.AddToCounts(elementCopyAtIndex2.Character, elementCopyAtIndex2.Number, false, elementCopyAtIndex2.WoundedNumber, 0, true, -1);
					}
				}
			}
			Func<string> func = () => "";
			Func<string> func2 = () => "";
			if (troopRoster.Count > 0)
			{
				func = delegate()
				{
					TroopRoster troopRoster3 = TroopRoster.CreateDummyTroopRoster();
					lastHeroIndex = 0;
					foreach (MapEventParty mapEventParty3 in parties1)
					{
						for (int k = 0; k < mapEventParty3.Party.MemberRoster.Count; k++)
						{
							TroopRosterElement elementCopyAtIndex3 = mapEventParty3.Party.MemberRoster.GetElementCopyAtIndex(k);
							if (elementCopyAtIndex3.Character.IsHero)
							{
								troopRoster3.AddToCounts(elementCopyAtIndex3.Character, elementCopyAtIndex3.Number, false, elementCopyAtIndex3.WoundedNumber, 0, true, lastHeroIndex);
								int lastHeroIndex2 = lastHeroIndex;
								lastHeroIndex = lastHeroIndex2 + 1;
							}
							else
							{
								troopRoster3.AddToCounts(elementCopyAtIndex3.Character, elementCopyAtIndex3.Number, false, elementCopyAtIndex3.WoundedNumber, 0, true, -1);
							}
						}
					}
					TextObject textObject = new TextObject("{=QlbkxoSp} {TOOLTIP_TROOPS} ({PARTY_SIZE})", null);
					textObject.SetTextVariable("TOOLTIP_TROOPS", GameTexts.FindText("str_map_tooltip_troops", null));
					textObject.SetTextVariable("PARTY_SIZE", PartyBaseHelper.GetPartySizeText(troopRoster3.TotalManCount - troopRoster3.TotalWounded, troopRoster3.TotalWounded, true));
					return textObject.ToString();
				};
			}
			if (troopRoster2.Count > 0)
			{
				func2 = delegate()
				{
					TroopRoster troopRoster3 = TroopRoster.CreateDummyTroopRoster();
					lastHeroIndex = 0;
					foreach (MapEventParty mapEventParty3 in parties2)
					{
						for (int k = 0; k < mapEventParty3.Party.MemberRoster.Count; k++)
						{
							TroopRosterElement elementCopyAtIndex3 = mapEventParty3.Party.MemberRoster.GetElementCopyAtIndex(k);
							if (elementCopyAtIndex3.Character.IsHero)
							{
								troopRoster3.AddToCounts(elementCopyAtIndex3.Character, elementCopyAtIndex3.Number, false, elementCopyAtIndex3.WoundedNumber, 0, true, lastHeroIndex);
								int lastHeroIndex2 = lastHeroIndex;
								lastHeroIndex = lastHeroIndex2 + 1;
							}
							else
							{
								troopRoster3.AddToCounts(elementCopyAtIndex3.Character, elementCopyAtIndex3.Number, false, elementCopyAtIndex3.WoundedNumber, 0, true, -1);
							}
						}
					}
					TextObject textObject = new TextObject("{=QlbkxoSp} {TOOLTIP_TROOPS} ({PARTY_SIZE})", null);
					textObject.SetTextVariable("TOOLTIP_TROOPS", GameTexts.FindText("str_map_tooltip_troops", null));
					textObject.SetTextVariable("PARTY_SIZE", PartyBaseHelper.GetPartySizeText(troopRoster3.TotalManCount - troopRoster3.TotalWounded, troopRoster3.TotalWounded, true));
					return textObject.ToString();
				};
			}
			if (func().Length != 0 && func2().Length != 0)
			{
				propertyBasedTooltipVM.AddProperty("", "", -1, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty(func, func2, 0, TooltipProperty.TooltipPropertyFlags.None);
			}
			if (isExtended)
			{
				propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.DefaultSeperator);
				int num2 = 0;
				while (num2 < troopRoster.Count || num2 < troopRoster2.Count)
				{
					string blankString = new TextObject("{=!} ", null).ToString();
					Func<string> definition = () => blankString;
					Func<string> value = () => blankString;
					if (num2 < troopRoster.Count)
					{
						CharacterObject character = troopRoster.GetElementCopyAtIndex(num2).Character;
						definition = delegate()
						{
							lastHeroIndex = 0;
							foreach (MapEventParty mapEventParty3 in parties1)
							{
								for (int k = 0; k < mapEventParty3.Party.MemberRoster.Count; k++)
								{
									TroopRosterElement elementCopyAtIndex3 = mapEventParty3.Party.MemberRoster.GetElementCopyAtIndex(k);
									if (elementCopyAtIndex3.Character == character)
									{
										TextObject textObject;
										if (elementCopyAtIndex3.Character.IsHero)
										{
											textObject = new TextObject("{=W1tsTWZv} {PARTY_MEMBER.LINK} ({MEMBER_HEALTH}%)", null);
											textObject.SetTextVariable("MEMBER_HEALTH", elementCopyAtIndex3.Character.HeroObject.HitPoints * 100 / elementCopyAtIndex3.Character.MaxHitPoints());
										}
										else
										{
											textObject = new TextObject("{=vLaBJFGy} {PARTY_MEMBER.LINK} ({PARTY_SIZE})", null);
											textObject.SetTextVariable("PARTY_SIZE", PartyBaseHelper.GetPartySizeText(elementCopyAtIndex3.Number - elementCopyAtIndex3.WoundedNumber, elementCopyAtIndex3.WoundedNumber, true));
										}
										StringHelpers.SetCharacterProperties("PARTY_MEMBER", elementCopyAtIndex3.Character, textObject, false);
										return textObject.ToString();
									}
								}
							}
							return blankString;
						};
					}
					if (num2 < troopRoster2.Count)
					{
						CharacterObject character = troopRoster2.GetElementCopyAtIndex(num2).Character;
						value = delegate()
						{
							lastHeroIndex = 0;
							foreach (MapEventParty mapEventParty3 in parties2)
							{
								for (int k = 0; k < mapEventParty3.Party.MemberRoster.Count; k++)
								{
									TroopRosterElement elementCopyAtIndex3 = mapEventParty3.Party.MemberRoster.GetElementCopyAtIndex(k);
									if (character == elementCopyAtIndex3.Character)
									{
										TextObject textObject;
										if (character.IsHero)
										{
											textObject = new TextObject("{=PS02CqPu} {PARTY_MEMBER.LINK} (Health: {MEMBER_HEALTH}%)", null);
											textObject.SetTextVariable("MEMBER_HEALTH", elementCopyAtIndex3.Character.HeroObject.HitPoints * 100 / elementCopyAtIndex3.Character.MaxHitPoints());
										}
										else
										{
											textObject = new TextObject("{=vLaBJFGy} {PARTY_MEMBER.LINK} ({PARTY_SIZE})", null);
											textObject.SetTextVariable("PARTY_SIZE", PartyBaseHelper.GetPartySizeText(elementCopyAtIndex3.Number - elementCopyAtIndex3.WoundedNumber, elementCopyAtIndex3.WoundedNumber, true));
										}
										StringHelpers.SetCharacterProperties("PARTY_MEMBER", elementCopyAtIndex3.Character, textObject, false);
										return textObject.ToString();
									}
								}
							}
							return blankString;
						};
					}
					propertyBasedTooltipVM.AddProperty(definition, value, 0, TooltipProperty.TooltipPropertyFlags.None);
					num2++;
				}
			}
			propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.BattleModeOver);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		private static void AddPartyTroopProperties(PropertyBasedTooltipVM propertyBasedTooltipVM, TroopRoster troopRoster, TextObject title, bool isInspected, Func<TroopRoster> funcToDoBeforeLambda = null)
		{
			propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
			propertyBasedTooltipVM.AddProperty(title.ToString(), delegate()
			{
				TroopRoster troopRoster2 = (funcToDoBeforeLambda != null) ? funcToDoBeforeLambda() : troopRoster;
				int num = 0;
				int num2 = 0;
				for (int l = 0; l < troopRoster2.Count; l++)
				{
					TroopRosterElement elementCopyAtIndex3 = troopRoster2.GetElementCopyAtIndex(l);
					num += elementCopyAtIndex3.Number - elementCopyAtIndex3.WoundedNumber;
					num2 += elementCopyAtIndex3.WoundedNumber;
				}
				TextObject textObject3 = new TextObject("{=iXXTONWb} ({PARTY_SIZE})", null);
				textObject3.SetTextVariable("PARTY_SIZE", PartyBaseHelper.GetPartySizeText(num, num2, isInspected));
				return textObject3.ToString();
			}, 0, TooltipProperty.TooltipPropertyFlags.None);
			if (isInspected)
			{
				propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.RundownSeperator);
			}
			if (isInspected)
			{
				Dictionary<FormationClass, Tuple<int, int>> dictionary = new Dictionary<FormationClass, Tuple<int, int>>();
				for (int i = 0; i < troopRoster.Count; i++)
				{
					TroopRosterElement elementCopyAtIndex = troopRoster.GetElementCopyAtIndex(i);
					if (dictionary.ContainsKey(elementCopyAtIndex.Character.DefaultFormationClass))
					{
						Tuple<int, int> tuple = dictionary[elementCopyAtIndex.Character.DefaultFormationClass];
						dictionary[elementCopyAtIndex.Character.DefaultFormationClass] = new Tuple<int, int>(tuple.Item1 + elementCopyAtIndex.Number - elementCopyAtIndex.WoundedNumber, tuple.Item2 + elementCopyAtIndex.WoundedNumber);
					}
					else
					{
						dictionary.Add(elementCopyAtIndex.Character.DefaultFormationClass, new Tuple<int, int>(elementCopyAtIndex.Number - elementCopyAtIndex.WoundedNumber, elementCopyAtIndex.WoundedNumber));
					}
				}
				foreach (KeyValuePair<FormationClass, Tuple<int, int>> keyValuePair in from x in dictionary
				orderby x.Key
				select x)
				{
					TextObject textObject = new TextObject("{=Dqydb21E} {PARTY_SIZE}", null);
					textObject.SetTextVariable("PARTY_SIZE", PartyBaseHelper.GetPartySizeText(keyValuePair.Value.Item1, keyValuePair.Value.Item2, true));
					TextObject textObject2 = GameTexts.FindText("str_troop_type_name", keyValuePair.Key.GetName());
					propertyBasedTooltipVM.AddProperty(textObject2.ToString(), textObject.ToString(), 0, TooltipProperty.TooltipPropertyFlags.None);
				}
			}
			if (propertyBasedTooltipVM.IsExtended & isInspected)
			{
				propertyBasedTooltipVM.AddProperty(string.Empty, string.Empty, -1, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty(GameTexts.FindText("str_troop_types", null).ToString(), " ", 0, TooltipProperty.TooltipPropertyFlags.None);
				propertyBasedTooltipVM.AddProperty("", "", 0, TooltipProperty.TooltipPropertyFlags.DefaultSeperator);
				for (int j = 0; j < troopRoster.Count; j++)
				{
					TroopRosterElement elementCopyAtIndex2 = troopRoster.GetElementCopyAtIndex(j);
					if (elementCopyAtIndex2.Character.IsHero)
					{
						CharacterObject hero = elementCopyAtIndex2.Character;
						propertyBasedTooltipVM.AddProperty(elementCopyAtIndex2.Character.Name.ToString(), delegate()
						{
							TroopRoster troopRoster2 = (funcToDoBeforeLambda != null) ? funcToDoBeforeLambda() : troopRoster;
							int num = troopRoster2.FindIndexOfTroop(hero);
							if (num == -1)
							{
								return string.Empty;
							}
							TroopRosterElement elementCopyAtIndex3 = troopRoster2.GetElementCopyAtIndex(num);
							TextObject textObject3 = new TextObject("{=aE4ZRbB6} {HEALTH}%", null);
							textObject3.SetTextVariable("HEALTH", elementCopyAtIndex3.Character.HeroObject.HitPoints * 100 / elementCopyAtIndex3.Character.MaxHitPoints());
							return textObject3.ToString();
						}, 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
				for (int k = 0; k < troopRoster.Count; k++)
				{
					int index = k;
					CharacterObject character = troopRoster.GetElementCopyAtIndex(index).Character;
					if (!character.IsHero)
					{
						propertyBasedTooltipVM.AddProperty(character.Name.ToString(), delegate()
						{
							TroopRoster troopRoster2 = (funcToDoBeforeLambda != null) ? funcToDoBeforeLambda() : troopRoster;
							CharacterObject character;
							int num = troopRoster2.FindIndexOfTroop(character);
							if (num != -1)
							{
								if (num > troopRoster2.Count)
								{
									return string.Empty;
								}
								TroopRosterElement elementCopyAtIndex3 = troopRoster2.GetElementCopyAtIndex(num);
								if (elementCopyAtIndex3.Character == null)
								{
									return string.Empty;
								}
								character = elementCopyAtIndex3.Character;
								if (character != null && !character.IsHero)
								{
									TextObject textObject3 = new TextObject("{=QyVbwGLp}{PARTY_SIZE}", null);
									textObject3.SetTextVariable("PARTY_SIZE", PartyBaseHelper.GetPartySizeText(elementCopyAtIndex3.Number - elementCopyAtIndex3.WoundedNumber, elementCopyAtIndex3.WoundedNumber, true));
									return textObject3.ToString();
								}
							}
							return string.Empty;
						}, 0, TooltipProperty.TooltipPropertyFlags.None);
					}
				}
			}
		}

		// Token: 0x040000BF RID: 191
		private static readonly IEqualityComparer<ValueTuple<ItemCategory, int>> itemCategoryDistinctComparer = new CampaignUIHelper.ProductInputOutputEqualityComparer();

		// Token: 0x040000C0 RID: 192
		private static string ExtendKeyId = "ExtendModifier";

		// Token: 0x040000C1 RID: 193
		private static string FollowModifierKeyId = "FollowModifier";

		// Token: 0x040000C2 RID: 194
		private static string MapClickKeyId = "MapClick";
	}
}
