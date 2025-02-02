using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.ViewModelCollection.Quests;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay
{
	// Token: 0x020000A8 RID: 168
	public class GameMenuPartyItemVM : ViewModel
	{
		// Token: 0x060010AF RID: 4271 RVA: 0x00041D7F File Offset: 0x0003FF7F
		public GameMenuPartyItemVM()
		{
			this.Visual = new ImageIdentifierVM(ImageIdentifierType.Null);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00041DA4 File Offset: 0x0003FFA4
		public GameMenuPartyItemVM(Action<GameMenuPartyItemVM> onSetAsContextMenuActiveItem, Settlement settlement)
		{
			this._onSetAsContextMenuActiveItem = onSetAsContextMenuActiveItem;
			this.Settlement = settlement;
			SettlementComponent settlementComponent = settlement.SettlementComponent;
			this.SettlementPath = ((settlementComponent == null) ? "placeholder" : (settlementComponent.BackgroundMeshName + "_t"));
			this.Visual = new ImageIdentifierVM(ImageIdentifierType.Null);
			this.NameText = settlement.Name.ToString();
			this.PartySize = -1;
			this.PartyWoundedSize = -1;
			this.PartySizeLbl = "";
			this.IsPlayer = false;
			this.IsAlly = false;
			this.IsEnemy = false;
			this.Quests = new MBBindingList<QuestMarkerVM>();
			this.RefreshProperties();
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00041E58 File Offset: 0x00040058
		public GameMenuPartyItemVM(Action<GameMenuPartyItemVM> onSetAsContextMenuActiveItem, PartyBase item, bool canShowQuest)
		{
			this._onSetAsContextMenuActiveItem = onSetAsContextMenuActiveItem;
			this.Party = item;
			CharacterObject visualPartyLeader = PartyBaseHelper.GetVisualPartyLeader(this.Party);
			if (visualPartyLeader != null)
			{
				CharacterCode characterCode = CampaignUIHelper.GetCharacterCode(visualPartyLeader, false);
				this.Visual = new ImageIdentifierVM(characterCode);
			}
			else
			{
				this.Visual = new ImageIdentifierVM(ImageIdentifierType.Null);
			}
			this.Quests = new MBBindingList<QuestMarkerVM>();
			this._canShowQuest = canShowQuest;
			this.RefreshProperties();
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00041ED4 File Offset: 0x000400D4
		public GameMenuPartyItemVM(Action<GameMenuPartyItemVM> onSetAsContextMenuActiveItem, CharacterObject character, bool useCivilianEquipment)
		{
			this._onSetAsContextMenuActiveItem = onSetAsContextMenuActiveItem;
			this.Character = character;
			this._useCivilianEquipment = useCivilianEquipment;
			CharacterCode characterCode = CampaignUIHelper.GetCharacterCode(character, useCivilianEquipment);
			this.Visual = new ImageIdentifierVM(characterCode);
			Hero heroObject = this.Character.HeroObject;
			this.Banner_9 = (((heroObject != null && heroObject.IsLord) || (this.Character.IsHero && this.Character.HeroObject.Clan == Clan.PlayerClan && character.HeroObject.IsLord)) ? new ImageIdentifierVM(BannerCode.CreateFrom(this.Character.HeroObject.ClanBanner), true) : new ImageIdentifierVM(ImageIdentifierType.Null));
			this.NameText = this.Character.Name.ToString();
			this.PartySize = -1;
			this.PartyWoundedSize = -1;
			this.PartySizeLbl = "";
			this.IsPlayer = character.IsPlayerCharacter;
			this.Quests = new MBBindingList<QuestMarkerVM>();
			this.RefreshProperties();
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00041FE6 File Offset: 0x000401E6
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.RefreshProperties();
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00041FF4 File Offset: 0x000401F4
		public void ExecuteSetAsContextMenuItem()
		{
			Action<GameMenuPartyItemVM> onSetAsContextMenuActiveItem = this._onSetAsContextMenuActiveItem;
			if (onSetAsContextMenuActiveItem == null)
			{
				return;
			}
			onSetAsContextMenuActiveItem(this);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00042008 File Offset: 0x00040208
		public void ExecuteOpenEncyclopedia()
		{
			if (this.Character != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.Character.EncyclopediaLink);
				return;
			}
			if (this.Party != null)
			{
				if (this.Party.LeaderHero != null)
				{
					Campaign.Current.EncyclopediaManager.GoToLink(this.Party.LeaderHero.EncyclopediaLink);
					return;
				}
				if (this.Party.Owner != null)
				{
					Campaign.Current.EncyclopediaManager.GoToLink(this.Party.Owner.EncyclopediaLink);
					return;
				}
				CharacterObject visualPartyLeader = CampaignUIHelper.GetVisualPartyLeader(this.Party);
				if (visualPartyLeader != null)
				{
					Campaign.Current.EncyclopediaManager.GoToLink(visualPartyLeader.EncyclopediaLink);
				}
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000420BE File Offset: 0x000402BE
		public void ExecuteCloseTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000420C8 File Offset: 0x000402C8
		public void ExecuteOpenTooltip()
		{
			PartyBase party = this.Party;
			if (((party != null) ? party.MobileParty : null) != null)
			{
				InformationManager.ShowTooltip(typeof(MobileParty), new object[]
				{
					this.Party.MobileParty,
					true,
					false
				});
				return;
			}
			if (this.Settlement != null)
			{
				InformationManager.ShowTooltip(typeof(Settlement), new object[]
				{
					this.Settlement,
					true
				});
				return;
			}
			InformationManager.ShowTooltip(typeof(Hero), new object[]
			{
				this.Character.HeroObject,
				true
			});
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0004217C File Offset: 0x0004037C
		public void RefreshProperties()
		{
			if (this.Party != null)
			{
				this.PartyWoundedSize = this.Party.NumberOfAllMembers - this.Party.NumberOfHealthyMembers;
				this.PartySize = this.Party.NumberOfHealthyMembers;
				this.PartySizeLbl = this.Party.NumberOfHealthyMembers.ToString();
				this.Relation = HeroVM.GetRelation(this.Party.LeaderHero);
				this.LocationText = " ";
				TextObject name = this.Party.Name;
				if (this.Party.IsMobile)
				{
					name = this.Party.MobileParty.Name;
					if (this.Party.MobileParty.Position2D.DistanceSquared(MobileParty.MainParty.Position2D) > 9f)
					{
						if (this.Party.MobileParty.MapEvent == null)
						{
							GameTexts.SetVariable("LEFT", GameTexts.FindText("str_distance_to_army_leader", null));
							GameTexts.SetVariable("RIGHT", CampaignUIHelper.GetPartyDistanceByTimeText((float)((int)Campaign.Current.Models.MapDistanceModel.GetDistance(this.Party.MobileParty, MobileParty.MainParty)), this.Party.MobileParty.Speed));
							this.LocationText = GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
						}
						else
						{
							TextObject variable = GameTexts.FindText("str_at_map_event", null);
							TextObject textObject = new TextObject("{=zawBaxl5}Distance : {DISTANCE}", null);
							textObject.SetTextVariable("DISTANCE", variable);
							this.LocationText = textObject.ToString();
						}
					}
					this.DescriptionText = this.GetPartyDescriptionTextFromValues();
					this.IsMergedWithArmy = true;
					if (this.Party.MobileParty.Army != null)
					{
						this.IsMergedWithArmy = this.Party.MobileParty.Army.DoesLeaderPartyAndAttachedPartiesContain(this.Party.MobileParty);
					}
				}
				this.NameText = name.ToString();
				this.ProfessionText = " ";
			}
			else if (this.Character != null)
			{
				this.Relation = HeroVM.GetRelation(this.Character.HeroObject);
				Hero heroObject = this.Character.HeroObject;
				this.IsCharacterInPrison = (heroObject != null && heroObject.IsPrisoner);
				GameTexts.SetVariable("PROFESSION", HeroHelper.GetCharacterTypeName(this.Character.HeroObject));
				string variableName = "LOCATION";
				Hero heroObject2 = this.Character.HeroObject;
				GameTexts.SetVariable(variableName, (((heroObject2 != null) ? heroObject2.CurrentSettlement : null) != null) ? this.Character.HeroObject.CurrentSettlement.Name.ToString() : "");
				Hero heroObject3 = this.Character.HeroObject;
				this.DescriptionText = ((heroObject3 != null && !heroObject3.IsSpecial) ? GameTexts.FindText("str_character_in_town", null).ToString() : string.Empty);
				string variableName2 = "LOCATION";
				LocationComplex locationComplex = LocationComplex.Current;
				TextObject textObject2;
				if (locationComplex == null)
				{
					textObject2 = null;
				}
				else
				{
					Location locationOfCharacter = locationComplex.GetLocationOfCharacter(this.Character.HeroObject);
					textObject2 = ((locationOfCharacter != null) ? locationOfCharacter.Name : null);
				}
				GameTexts.SetVariable(variableName2, textObject2 ?? TextObject.Empty);
				this.LocationText = GameTexts.FindText("str_location_colon", null).ToString();
				GameTexts.SetVariable("PROFESSION", HeroHelper.GetCharacterTypeName(this.Character.HeroObject));
				this.ProfessionText = GameTexts.FindText("str_profession_colon", null).ToString();
				if (this.Character.IsHero && this.Character.HeroObject.IsNotable)
				{
					GameTexts.SetVariable("POWER", Campaign.Current.Models.NotablePowerModel.GetPowerRankName(this.Character.HeroObject).ToString());
					this.PowerText = GameTexts.FindText("str_power_colon", null).ToString();
				}
				this.NameText = this.Character.Name.ToString();
			}
			this.RefreshQuestStatus();
			this.RefreshRelationStatus();
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00042550 File Offset: 0x00040750
		public void RefreshQuestStatus()
		{
			this.Quests.Clear();
			PartyBase party = this.Party;
			Hero hero;
			if ((hero = ((party != null) ? party.LeaderHero : null)) == null)
			{
				CharacterObject character = this.Character;
				hero = ((character != null) ? character.HeroObject : null);
			}
			Hero hero2 = hero;
			if (hero2 != null)
			{
				GameMenuPartyItemVM.<>c__DisplayClass16_0 CS$<>8__locals1 = new GameMenuPartyItemVM.<>c__DisplayClass16_0();
				CS$<>8__locals1.questTypes = CampaignUIHelper.GetQuestStateOfHero(hero2);
				int k;
				int i;
				for (i = 0; i < CS$<>8__locals1.questTypes.Count; i = k + 1)
				{
					if (!this.Quests.Any((QuestMarkerVM q) => q.QuestMarkerType == (int)CS$<>8__locals1.questTypes[i].Item1))
					{
						this.Quests.Add(new QuestMarkerVM(CS$<>8__locals1.questTypes[i].Item1, CS$<>8__locals1.questTypes[i].Item2, CS$<>8__locals1.questTypes[i].Item3));
					}
					k = i;
				}
			}
			else
			{
				PartyBase party2 = this.Party;
				if (((party2 != null) ? party2.MobileParty : null) != null)
				{
					List<QuestBase> questsRelatedToParty = CampaignUIHelper.GetQuestsRelatedToParty(this.Party.MobileParty);
					for (int j = 0; j < questsRelatedToParty.Count; j++)
					{
						TextObject questHintText = (questsRelatedToParty[j].JournalEntries.Count > 0) ? questsRelatedToParty[j].JournalEntries[0].LogText : TextObject.Empty;
						CampaignUIHelper.IssueQuestFlags issueQuestFlag;
						if (hero2 != null && questsRelatedToParty[j].QuestGiver == hero2)
						{
							issueQuestFlag = (questsRelatedToParty[j].IsSpecialQuest ? CampaignUIHelper.IssueQuestFlags.ActiveStoryQuest : CampaignUIHelper.IssueQuestFlags.ActiveIssue);
						}
						else
						{
							issueQuestFlag = (questsRelatedToParty[j].IsSpecialQuest ? CampaignUIHelper.IssueQuestFlags.TrackedStoryQuest : CampaignUIHelper.IssueQuestFlags.TrackedIssue);
						}
						this.Quests.Add(new QuestMarkerVM(issueQuestFlag, questsRelatedToParty[j].Title, questHintText));
					}
				}
			}
			this.Quests.Sort(new GameMenuPartyItemVM.QuestMarkerComparer());
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00042760 File Offset: 0x00040960
		private void RefreshRelationStatus()
		{
			this.IsEnemy = false;
			this.IsAlly = false;
			this.IsNeutral = false;
			IFaction faction = null;
			bool flag = false;
			if (this.Character != null)
			{
				this.IsPlayer = this.Character.IsPlayerCharacter;
				flag = (this.Character.IsHero && this.Character.HeroObject.IsNotable);
				IFaction faction2;
				if (!this.IsPlayer)
				{
					CharacterObject character = this.Character;
					faction2 = ((character != null) ? character.HeroObject.MapFaction : null);
				}
				else
				{
					faction2 = null;
				}
				faction = faction2;
			}
			else if (this.Party != null)
			{
				bool isPlayer;
				if (this.Party.IsMobile)
				{
					MobileParty mobileParty = this.Party.MobileParty;
					isPlayer = (mobileParty != null && mobileParty.IsMainParty);
				}
				else
				{
					isPlayer = false;
				}
				this.IsPlayer = isPlayer;
				flag = false;
				IFaction faction3;
				if (!this.IsPlayer)
				{
					PartyBase party = this.Party;
					if (party == null)
					{
						faction3 = null;
					}
					else
					{
						MobileParty mobileParty2 = party.MobileParty;
						faction3 = ((mobileParty2 != null) ? mobileParty2.MapFaction : null);
					}
				}
				else
				{
					faction3 = null;
				}
				faction = faction3;
			}
			if (this.IsPlayer || faction == null || flag)
			{
				this.IsNeutral = true;
				return;
			}
			if (FactionManager.IsAtWarAgainstFaction(faction, Hero.MainHero.MapFaction))
			{
				this.IsEnemy = true;
				return;
			}
			if (FactionManager.IsAlliedWithFaction(faction, Hero.MainHero.MapFaction))
			{
				this.IsAlly = true;
				return;
			}
			this.IsNeutral = true;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00042898 File Offset: 0x00040A98
		public void RefreshVisual()
		{
			if (this.Visual.IsEmpty)
			{
				if (this.Character != null)
				{
					CharacterCode characterCode = CampaignUIHelper.GetCharacterCode(this.Character, this._useCivilianEquipment);
					this.Visual = new ImageIdentifierVM(characterCode);
					return;
				}
				if (this.Party != null)
				{
					CharacterObject visualPartyLeader = PartyBaseHelper.GetVisualPartyLeader(this.Party);
					if (visualPartyLeader != null)
					{
						CharacterCode characterCode2 = CampaignUIHelper.GetCharacterCode(visualPartyLeader, false);
						this.Visual = new ImageIdentifierVM(characterCode2);
						return;
					}
					this.Visual = new ImageIdentifierVM(ImageIdentifierType.Null);
				}
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00042914 File Offset: 0x00040B14
		public string GetPartyDescriptionTextFromValues()
		{
			GameTexts.SetVariable("newline", "\n");
			string content = (this.Party.MobileParty.CurrentSettlement != null && this.Party.MobileParty.MapEvent == null) ? "" : CampaignUIHelper.GetMobilePartyBehaviorText(this.Party.MobileParty);
			GameTexts.SetVariable("LEFT", GameTexts.FindText("str_food", null).ToString());
			GameTexts.SetVariable("RIGHT", this.Party.MobileParty.Food);
			string content2 = GameTexts.FindText("str_LEFT_colon_RIGHT", null).ToString();
			GameTexts.SetVariable("LEFT", GameTexts.FindText("str_map_tooltip_speed", null).ToString());
			GameTexts.SetVariable("RIGHT", this.Party.MobileParty.Speed.ToString("F"));
			string content3 = GameTexts.FindText("str_LEFT_colon_RIGHT", null).ToString();
			GameTexts.SetVariable("LEFT", GameTexts.FindText("str_seeing_range", null).ToString());
			GameTexts.SetVariable("RIGHT", this.Party.MobileParty.SeeingRange);
			string content4 = GameTexts.FindText("str_LEFT_colon_RIGHT", null).ToString();
			GameTexts.SetVariable("STR1", content);
			GameTexts.SetVariable("STR2", content2);
			string content5 = GameTexts.FindText("str_string_newline_string", null).ToString();
			GameTexts.SetVariable("STR1", content5);
			GameTexts.SetVariable("STR2", content3);
			content5 = GameTexts.FindText("str_string_newline_string", null).ToString();
			GameTexts.SetVariable("STR1", content5);
			GameTexts.SetVariable("STR2", content4);
			return GameTexts.FindText("str_string_newline_string", null).ToString();
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00042AC1 File Offset: 0x00040CC1
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x00042AC9 File Offset: 0x00040CC9
		[DataSourceProperty]
		public int Relation
		{
			get
			{
				return this._relation;
			}
			set
			{
				if (value != this._relation)
				{
					this._relation = value;
					base.OnPropertyChangedWithValue(value, "Relation");
				}
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00042AE7 File Offset: 0x00040CE7
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x00042AEF File Offset: 0x00040CEF
		[DataSourceProperty]
		public MBBindingList<QuestMarkerVM> Quests
		{
			get
			{
				return this._quests;
			}
			set
			{
				if (value != this._quests)
				{
					this._quests = value;
					base.OnPropertyChangedWithValue<MBBindingList<QuestMarkerVM>>(value, "Quests");
				}
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00042B0D File Offset: 0x00040D0D
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x00042B15 File Offset: 0x00040D15
		[DataSourceProperty]
		public bool IsHighlightEnabled
		{
			get
			{
				return this._isHighlightEnabled;
			}
			set
			{
				if (value != this._isHighlightEnabled)
				{
					this._isHighlightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsHighlightEnabled");
				}
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00042B33 File Offset: 0x00040D33
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x00042B3B File Offset: 0x00040D3B
		[DataSourceProperty]
		public bool IsCharacterInPrison
		{
			get
			{
				return this._isCharacterInPrison;
			}
			set
			{
				if (value != this._isCharacterInPrison)
				{
					this._isCharacterInPrison = value;
					base.OnPropertyChangedWithValue(value, "IsCharacterInPrison");
				}
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x00042B59 File Offset: 0x00040D59
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x00042B61 File Offset: 0x00040D61
		[DataSourceProperty]
		public bool IsIdle
		{
			get
			{
				return this._isIdle;
			}
			set
			{
				if (value != this._isIdle)
				{
					this._isIdle = value;
					base.OnPropertyChangedWithValue(value, "IsIdle");
				}
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x00042B7F File Offset: 0x00040D7F
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x00042B87 File Offset: 0x00040D87
		[DataSourceProperty]
		public bool IsPlayer
		{
			get
			{
				return this._isPlayer;
			}
			set
			{
				if (value != this._isPlayer)
				{
					this._isPlayer = value;
					base.OnPropertyChanged("IsPlayerParty");
				}
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x00042BA4 File Offset: 0x00040DA4
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x00042BAC File Offset: 0x00040DAC
		[DataSourceProperty]
		public bool IsEnemy
		{
			get
			{
				return this._isEnemy;
			}
			set
			{
				if (value != this._isEnemy)
				{
					this._isEnemy = value;
					base.OnPropertyChangedWithValue(value, "IsEnemy");
				}
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00042BCA File Offset: 0x00040DCA
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x00042BD2 File Offset: 0x00040DD2
		[DataSourceProperty]
		public bool IsAlly
		{
			get
			{
				return this._isAlly;
			}
			set
			{
				if (value != this._isAlly)
				{
					this._isAlly = value;
					base.OnPropertyChangedWithValue(value, "IsAlly");
				}
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x00042BF0 File Offset: 0x00040DF0
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x00042BF8 File Offset: 0x00040DF8
		[DataSourceProperty]
		public bool IsNeutral
		{
			get
			{
				return this._isNeutral;
			}
			set
			{
				if (value != this._isNeutral)
				{
					this._isNeutral = value;
					base.OnPropertyChangedWithValue(value, "IsNeutral");
				}
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00042C16 File Offset: 0x00040E16
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x00042C1E File Offset: 0x00040E1E
		[DataSourceProperty]
		public bool IsMergedWithArmy
		{
			get
			{
				return this._isMergedWithArmy;
			}
			set
			{
				if (value != this._isMergedWithArmy)
				{
					this._isMergedWithArmy = value;
					base.OnPropertyChangedWithValue(value, "IsMergedWithArmy");
				}
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00042C3C File Offset: 0x00040E3C
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x00042C44 File Offset: 0x00040E44
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00042C67 File Offset: 0x00040E67
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x00042C6F File Offset: 0x00040E6F
		[DataSourceProperty]
		public string SettlementPath
		{
			get
			{
				return this._settlementPath;
			}
			set
			{
				if (value != this._settlementPath)
				{
					this._settlementPath = value;
					base.OnPropertyChangedWithValue<string>(value, "SettlementPath");
				}
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x00042C92 File Offset: 0x00040E92
		// (set) Token: 0x060010D6 RID: 4310 RVA: 0x00042C9A File Offset: 0x00040E9A
		[DataSourceProperty]
		public string LocationText
		{
			get
			{
				return this._locationText;
			}
			set
			{
				if (value != this._locationText)
				{
					this._locationText = value;
					base.OnPropertyChangedWithValue<string>(value, "LocationText");
				}
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00042CBD File Offset: 0x00040EBD
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x00042CC5 File Offset: 0x00040EC5
		[DataSourceProperty]
		public string PowerText
		{
			get
			{
				return this._powerText;
			}
			set
			{
				if (value != this._powerText)
				{
					this._powerText = value;
					base.OnPropertyChangedWithValue<string>(value, "PowerText");
				}
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00042CE8 File Offset: 0x00040EE8
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x00042CF0 File Offset: 0x00040EF0
		[DataSourceProperty]
		public string DescriptionText
		{
			get
			{
				return this._descriptionText;
			}
			set
			{
				if (value != this._descriptionText)
				{
					this._descriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "DescriptionText");
				}
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00042D13 File Offset: 0x00040F13
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x00042D1B File Offset: 0x00040F1B
		[DataSourceProperty]
		public string ProfessionText
		{
			get
			{
				return this._professionText;
			}
			set
			{
				if (value != this._professionText)
				{
					this._professionText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProfessionText");
				}
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x00042D3E File Offset: 0x00040F3E
		// (set) Token: 0x060010DE RID: 4318 RVA: 0x00042D46 File Offset: 0x00040F46
		[DataSourceProperty]
		public ImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00042D64 File Offset: 0x00040F64
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x00042D6C File Offset: 0x00040F6C
		[DataSourceProperty]
		public ImageIdentifierVM Banner_9
		{
			get
			{
				return this._banner_9;
			}
			set
			{
				if (value != this._banner_9)
				{
					this._banner_9 = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Banner_9");
				}
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00042D8A File Offset: 0x00040F8A
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x00042D92 File Offset: 0x00040F92
		[DataSourceProperty]
		public int PartySize
		{
			get
			{
				return this._partySize;
			}
			set
			{
				if (value != this._partySize)
				{
					this._partySize = value;
					base.OnPropertyChangedWithValue(value, "PartySize");
				}
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00042DB0 File Offset: 0x00040FB0
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x00042DB8 File Offset: 0x00040FB8
		[DataSourceProperty]
		public int PartyWoundedSize
		{
			get
			{
				return this._partyWoundedSize;
			}
			set
			{
				if (value != this._partySize)
				{
					this._partyWoundedSize = value;
					base.OnPropertyChangedWithValue(value, "PartyWoundedSize");
				}
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00042DD6 File Offset: 0x00040FD6
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x00042DDE File Offset: 0x00040FDE
		[DataSourceProperty]
		public string PartySizeLbl
		{
			get
			{
				return this._partySizeLbl;
			}
			set
			{
				if (value != this._partySizeLbl)
				{
					this._partySizeLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "PartySizeLbl");
				}
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00042E01 File Offset: 0x00041001
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x00042E09 File Offset: 0x00041009
		[DataSourceProperty]
		public bool IsLeader
		{
			get
			{
				return this._isLeader;
			}
			set
			{
				if (value != this._isLeader)
				{
					this._isLeader = value;
					base.OnPropertyChangedWithValue(value, "IsLeader");
				}
			}
		}

		// Token: 0x040007BD RID: 1981
		public CharacterObject Character;

		// Token: 0x040007BE RID: 1982
		public PartyBase Party;

		// Token: 0x040007BF RID: 1983
		public Settlement Settlement;

		// Token: 0x040007C0 RID: 1984
		private readonly bool _canShowQuest = true;

		// Token: 0x040007C1 RID: 1985
		private readonly bool _useCivilianEquipment;

		// Token: 0x040007C2 RID: 1986
		private readonly Action<GameMenuPartyItemVM> _onSetAsContextMenuActiveItem;

		// Token: 0x040007C3 RID: 1987
		private MBBindingList<QuestMarkerVM> _quests;

		// Token: 0x040007C4 RID: 1988
		private int _partySize;

		// Token: 0x040007C5 RID: 1989
		private int _partyWoundedSize;

		// Token: 0x040007C6 RID: 1990
		private int _relation = -101;

		// Token: 0x040007C7 RID: 1991
		private ImageIdentifierVM _visual;

		// Token: 0x040007C8 RID: 1992
		private ImageIdentifierVM _banner_9;

		// Token: 0x040007C9 RID: 1993
		private string _settlementPath;

		// Token: 0x040007CA RID: 1994
		private string _partySizeLbl;

		// Token: 0x040007CB RID: 1995
		private string _nameText;

		// Token: 0x040007CC RID: 1996
		private string _locationText;

		// Token: 0x040007CD RID: 1997
		private string _descriptionText;

		// Token: 0x040007CE RID: 1998
		private string _professionText;

		// Token: 0x040007CF RID: 1999
		private string _powerText;

		// Token: 0x040007D0 RID: 2000
		private bool _isIdle;

		// Token: 0x040007D1 RID: 2001
		private bool _isPlayer;

		// Token: 0x040007D2 RID: 2002
		private bool _isEnemy;

		// Token: 0x040007D3 RID: 2003
		private bool _isAlly;

		// Token: 0x040007D4 RID: 2004
		private bool _isNeutral;

		// Token: 0x040007D5 RID: 2005
		private bool _isHighlightEnabled;

		// Token: 0x040007D6 RID: 2006
		private bool _isLeader;

		// Token: 0x040007D7 RID: 2007
		private bool _isMergedWithArmy;

		// Token: 0x040007D8 RID: 2008
		private bool _isCharacterInPrison;

		// Token: 0x020001EE RID: 494
		private class QuestMarkerComparer : IComparer<QuestMarkerVM>
		{
			// Token: 0x060021CC RID: 8652 RVA: 0x0007461C File Offset: 0x0007281C
			public int Compare(QuestMarkerVM x, QuestMarkerVM y)
			{
				return x.QuestMarkerType.CompareTo(y.QuestMarkerType);
			}
		}
	}
}
