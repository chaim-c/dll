using System;
using System.Collections.Generic;
using SandBox.Objects;
using SandBox.Objects.AreaMarkers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Missions.NameMarker
{
	// Token: 0x02000026 RID: 38
	public class MissionNameMarkerTargetVM : ViewModel
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000E7D6 File Offset: 0x0000C9D6
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000E7DE File Offset: 0x0000C9DE
		public bool IsAdditionalTargetAgent { get; private set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000E7E7 File Offset: 0x0000C9E7
		public bool IsMovingTarget { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000E7EF File Offset: 0x0000C9EF
		public Agent TargetAgent { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000E7F7 File Offset: 0x0000C9F7
		public Alley TargetAlley { get; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000E7FF File Offset: 0x0000C9FF
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000E807 File Offset: 0x0000CA07
		public PassageUsePoint TargetPassageUsePoint { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000E810 File Offset: 0x0000CA10
		public Vec3 WorldPosition
		{
			get
			{
				return this._getPosition();
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000E820 File Offset: 0x0000CA20
		public MissionNameMarkerTargetVM(CommonAreaMarker commonAreaMarker)
		{
			this.IsMovingTarget = 0;
			this.NameType = "Passage";
			this.IconType = "common_area";
			this.Quests = new MBBindingList<QuestMarkerVM>();
			this.TargetAlley = Hero.MainHero.CurrentSettlement.Alleys[commonAreaMarker.AreaIndex - 1];
			this.UpdateAlleyStatus();
			this._getPosition = (() => commonAreaMarker.GetPosition());
			this._getMarkerObjectName = (() => commonAreaMarker.GetName().ToString());
			CampaignEvents.AlleyOwnerChanged.AddNonSerializedListener(this, new Action<Alley, Hero, Hero>(this.OnAlleyOwnerChanged));
			this.RefreshValues();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000E938 File Offset: 0x0000CB38
		public MissionNameMarkerTargetVM(WorkshopType workshopType, Vec3 signPosition)
		{
			this.IsMovingTarget = 0;
			this.NameType = "Passage";
			this.IconType = workshopType.StringId;
			this.Quests = new MBBindingList<QuestMarkerVM>();
			this._getPosition = (() => signPosition);
			this._getMarkerObjectName = (() => workshopType.Name.ToString());
			this.RefreshValues();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000EA18 File Offset: 0x0000CC18
		public MissionNameMarkerTargetVM(PassageUsePoint passageUsePoint)
		{
			this.TargetPassageUsePoint = passageUsePoint;
			this.IsMovingTarget = 0;
			this.NameType = "Passage";
			this.IconType = passageUsePoint.ToLocation.StringId;
			this.Quests = new MBBindingList<QuestMarkerVM>();
			this._getPosition = (() => passageUsePoint.GameEntity.GlobalPosition);
			this._getMarkerObjectName = (() => passageUsePoint.ToLocation.Name.ToString());
			this.RefreshValues();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000EB04 File Offset: 0x0000CD04
		public MissionNameMarkerTargetVM(Agent agent, bool isAdditionalTargetAgent)
		{
			this.IsMovingTarget = 1;
			this.TargetAgent = agent;
			this.NameType = "Normal";
			this.IconType = "character";
			this.IsAdditionalTargetAgent = isAdditionalTargetAgent;
			this.Quests = new MBBindingList<QuestMarkerVM>();
			CharacterObject characterObject = (CharacterObject)agent.Character;
			if (characterObject != null)
			{
				Hero heroObject = characterObject.HeroObject;
				if (heroObject != null && heroObject.IsLord)
				{
					this.IconType = "noble";
					this.NameType = "Noble";
					if (FactionManager.IsAtWarAgainstFaction(characterObject.HeroObject.MapFaction, Hero.MainHero.MapFaction))
					{
						this.NameType = "Enemy";
						this.IsEnemy = true;
					}
					else if (FactionManager.IsAlliedWithFaction(characterObject.HeroObject.MapFaction, Hero.MainHero.MapFaction))
					{
						this.NameType = "Friendly";
						this.IsFriendly = true;
					}
				}
				if (characterObject.HeroObject != null && characterObject.HeroObject.IsPrisoner)
				{
					this.IconType = "prisoner";
				}
				if (agent.IsHuman && agent != Agent.Main && !this.IsAdditionalTargetAgent)
				{
					this.UpdateQuestStatus();
				}
				CharacterObject characterObject2 = characterObject;
				Settlement currentSettlement = Settlement.CurrentSettlement;
				object obj;
				if (currentSettlement == null)
				{
					obj = null;
				}
				else
				{
					CultureObject culture = currentSettlement.Culture;
					obj = ((culture != null) ? culture.Barber : null);
				}
				if (characterObject2 == obj)
				{
					this.IconType = "barber";
				}
				else
				{
					CharacterObject characterObject3 = characterObject;
					Settlement currentSettlement2 = Settlement.CurrentSettlement;
					object obj2;
					if (currentSettlement2 == null)
					{
						obj2 = null;
					}
					else
					{
						CultureObject culture2 = currentSettlement2.Culture;
						obj2 = ((culture2 != null) ? culture2.Blacksmith : null);
					}
					if (characterObject3 == obj2)
					{
						this.IconType = "blacksmith";
					}
					else
					{
						CharacterObject characterObject4 = characterObject;
						Settlement currentSettlement3 = Settlement.CurrentSettlement;
						object obj3;
						if (currentSettlement3 == null)
						{
							obj3 = null;
						}
						else
						{
							CultureObject culture3 = currentSettlement3.Culture;
							obj3 = ((culture3 != null) ? culture3.TavernGamehost : null);
						}
						if (characterObject4 == obj3)
						{
							this.IconType = "game_host";
						}
						else if (characterObject.StringId == "sp_hermit")
						{
							this.IconType = "hermit";
						}
					}
				}
			}
			this._getPosition = delegate()
			{
				Vec3 position = agent.Position;
				position.z = agent.GetEyeGlobalPosition().Z;
				return position;
			};
			this._getMarkerObjectName = (() => agent.Name);
			this.RefreshValues();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public MissionNameMarkerTargetVM(Vec3 position, string name, string iconType)
		{
			this.NameType = "Passage";
			this.IconType = iconType;
			this.Quests = new MBBindingList<QuestMarkerVM>();
			this._getPosition = (() => position);
			this._getMarkerObjectName = (() => name);
			this.RefreshValues();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000EE42 File Offset: 0x0000D042
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Name = this._getMarkerObjectName();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000EE5B File Offset: 0x0000D05B
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.AlleyOwnerChanged.ClearListeners(this);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000EE6E File Offset: 0x0000D06E
		private void OnAlleyOwnerChanged(Alley alley, Hero newOwner, Hero oldOwner)
		{
			if (this.TargetAlley == alley && (newOwner == Hero.MainHero || oldOwner == Hero.MainHero))
			{
				this.UpdateAlleyStatus();
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000EE90 File Offset: 0x0000D090
		private void UpdateAlleyStatus()
		{
			if (this.TargetAlley != null)
			{
				Hero owner = this.TargetAlley.Owner;
				if (owner != null)
				{
					if (owner == Hero.MainHero)
					{
						this.NameType = "Friendly";
						this.IsFriendly = true;
						this.IsEnemy = false;
						return;
					}
					this.NameType = "Passage";
					this.IsFriendly = false;
					this.IsEnemy = true;
					return;
				}
				else
				{
					this.NameType = "Normal";
					this.IsFriendly = false;
					this.IsEnemy = false;
				}
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000EF0C File Offset: 0x0000D10C
		public void UpdateQuestStatus(SandBoxUIHelper.IssueQuestFlags issueQuestFlags)
		{
			SandBoxUIHelper.IssueQuestFlags[] issueQuestFlagsValues = SandBoxUIHelper.IssueQuestFlagsValues;
			for (int i = 0; i < issueQuestFlagsValues.Length; i++)
			{
				SandBoxUIHelper.IssueQuestFlags questFlag = issueQuestFlagsValues[i];
				if (questFlag != SandBoxUIHelper.IssueQuestFlags.None && (issueQuestFlags & questFlag) != SandBoxUIHelper.IssueQuestFlags.None && this.Quests.AllQ((QuestMarkerVM q) => q.IssueQuestFlag != questFlag))
				{
					this.Quests.Add(new QuestMarkerVM(questFlag));
					if ((questFlag & SandBoxUIHelper.IssueQuestFlags.ActiveIssue) != SandBoxUIHelper.IssueQuestFlags.None && (questFlag & SandBoxUIHelper.IssueQuestFlags.AvailableIssue) != SandBoxUIHelper.IssueQuestFlags.None && (questFlag & SandBoxUIHelper.IssueQuestFlags.TrackedIssue) != SandBoxUIHelper.IssueQuestFlags.None)
					{
						this.IsTracked = true;
					}
					else if ((questFlag & SandBoxUIHelper.IssueQuestFlags.ActiveIssue) != SandBoxUIHelper.IssueQuestFlags.None && (questFlag & SandBoxUIHelper.IssueQuestFlags.ActiveStoryQuest) != SandBoxUIHelper.IssueQuestFlags.None && (questFlag & SandBoxUIHelper.IssueQuestFlags.TrackedStoryQuest) != SandBoxUIHelper.IssueQuestFlags.None)
					{
						this.IsQuestMainStory = true;
					}
				}
			}
			this.Quests.Sort(new MissionNameMarkerTargetVM.QuestMarkerComparer());
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000EFE4 File Offset: 0x0000D1E4
		public void UpdateQuestStatus()
		{
			this.Quests.Clear();
			SandBoxUIHelper.IssueQuestFlags issueQuestFlags = SandBoxUIHelper.IssueQuestFlags.None;
			Agent targetAgent = this.TargetAgent;
			CharacterObject characterObject = (CharacterObject)((targetAgent != null) ? targetAgent.Character : null);
			Hero hero = (characterObject != null) ? characterObject.HeroObject : null;
			if (hero != null)
			{
				List<ValueTuple<SandBoxUIHelper.IssueQuestFlags, TextObject, TextObject>> questStateOfHero = SandBoxUIHelper.GetQuestStateOfHero(hero);
				for (int i = 0; i < questStateOfHero.Count; i++)
				{
					issueQuestFlags |= questStateOfHero[i].Item1;
				}
			}
			if (this.TargetAgent != null)
			{
				CharacterObject characterObject2 = this.TargetAgent.Character as CharacterObject;
				Hero hero2;
				if (characterObject2 == null)
				{
					hero2 = null;
				}
				else
				{
					Hero heroObject = characterObject2.HeroObject;
					if (heroObject == null)
					{
						hero2 = null;
					}
					else
					{
						Clan clan = heroObject.Clan;
						hero2 = ((clan != null) ? clan.Leader : null);
					}
				}
				if (hero2 != Hero.MainHero)
				{
					Settlement currentSettlement = Settlement.CurrentSettlement;
					bool flag;
					if (currentSettlement == null)
					{
						flag = false;
					}
					else
					{
						LocationComplex locationComplex = currentSettlement.LocationComplex;
						bool? flag2;
						if (locationComplex == null)
						{
							flag2 = null;
						}
						else
						{
							LocationCharacter locationCharacter = locationComplex.FindCharacter(this.TargetAgent);
							flag2 = ((locationCharacter != null) ? new bool?(locationCharacter.IsVisualTracked) : null);
						}
						bool? flag3 = flag2;
						bool flag4 = true;
						flag = (flag3.GetValueOrDefault() == flag4 & flag3 != null);
					}
					if (flag)
					{
						issueQuestFlags |= SandBoxUIHelper.IssueQuestFlags.TrackedIssue;
					}
				}
			}
			foreach (SandBoxUIHelper.IssueQuestFlags issueQuestFlags2 in SandBoxUIHelper.IssueQuestFlagsValues)
			{
				if (issueQuestFlags2 != SandBoxUIHelper.IssueQuestFlags.None && (issueQuestFlags & issueQuestFlags2) != SandBoxUIHelper.IssueQuestFlags.None)
				{
					this.Quests.Add(new QuestMarkerVM(issueQuestFlags2));
					if ((issueQuestFlags2 & SandBoxUIHelper.IssueQuestFlags.ActiveIssue) != SandBoxUIHelper.IssueQuestFlags.None && (issueQuestFlags2 & SandBoxUIHelper.IssueQuestFlags.AvailableIssue) != SandBoxUIHelper.IssueQuestFlags.None && (issueQuestFlags2 & SandBoxUIHelper.IssueQuestFlags.TrackedIssue) != SandBoxUIHelper.IssueQuestFlags.None)
					{
						this.IsTracked = true;
					}
					else if ((issueQuestFlags2 & SandBoxUIHelper.IssueQuestFlags.ActiveIssue) != SandBoxUIHelper.IssueQuestFlags.None && (issueQuestFlags2 & SandBoxUIHelper.IssueQuestFlags.ActiveStoryQuest) != SandBoxUIHelper.IssueQuestFlags.None && (issueQuestFlags2 & SandBoxUIHelper.IssueQuestFlags.TrackedStoryQuest) != SandBoxUIHelper.IssueQuestFlags.None)
					{
						this.IsQuestMainStory = true;
					}
				}
			}
			this.Quests.Sort(new MissionNameMarkerTargetVM.QuestMarkerComparer());
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0000F178 File Offset: 0x0000D378
		// (set) Token: 0x0600030D RID: 781 RVA: 0x0000F180 File Offset: 0x0000D380
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600030E RID: 782 RVA: 0x0000F19E File Offset: 0x0000D39E
		// (set) Token: 0x0600030F RID: 783 RVA: 0x0000F1A6 File Offset: 0x0000D3A6
		[DataSourceProperty]
		public Vec2 ScreenPosition
		{
			get
			{
				return this._screenPosition;
			}
			set
			{
				if (value.x != this._screenPosition.x || value.y != this._screenPosition.y)
				{
					this._screenPosition = value;
					base.OnPropertyChangedWithValue(value, "ScreenPosition");
				}
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000310 RID: 784 RVA: 0x0000F1E1 File Offset: 0x0000D3E1
		// (set) Token: 0x06000311 RID: 785 RVA: 0x0000F1E9 File Offset: 0x0000D3E9
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000F20C File Offset: 0x0000D40C
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0000F214 File Offset: 0x0000D414
		[DataSourceProperty]
		public string IconType
		{
			get
			{
				return this._iconType;
			}
			set
			{
				if (value != this._iconType)
				{
					this._iconType = value;
					base.OnPropertyChangedWithValue<string>(value, "IconType");
				}
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000F237 File Offset: 0x0000D437
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0000F23F File Offset: 0x0000D43F
		[DataSourceProperty]
		public string NameType
		{
			get
			{
				return this._nameType;
			}
			set
			{
				if (value != this._nameType)
				{
					this._nameType = value;
					base.OnPropertyChangedWithValue<string>(value, "NameType");
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000F262 File Offset: 0x0000D462
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000F26A File Offset: 0x0000D46A
		[DataSourceProperty]
		public int Distance
		{
			get
			{
				return this._distance;
			}
			set
			{
				if (value != this._distance)
				{
					this._distance = value;
					base.OnPropertyChangedWithValue(value, "Distance");
				}
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000F288 File Offset: 0x0000D488
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000F290 File Offset: 0x0000D490
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

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000F2AE File Offset: 0x0000D4AE
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000F2B6 File Offset: 0x0000D4B6
		[DataSourceProperty]
		public bool IsTracked
		{
			get
			{
				return this._isTracked;
			}
			set
			{
				if (value != this._isTracked)
				{
					this._isTracked = value;
					base.OnPropertyChangedWithValue(value, "IsTracked");
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		[DataSourceProperty]
		public bool IsQuestMainStory
		{
			get
			{
				return this._isQuestMainStory;
			}
			set
			{
				if (value != this._isQuestMainStory)
				{
					this._isQuestMainStory = value;
					base.OnPropertyChangedWithValue(value, "IsQuestMainStory");
				}
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000F2FA File Offset: 0x0000D4FA
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000F302 File Offset: 0x0000D502
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

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000F320 File Offset: 0x0000D520
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000F328 File Offset: 0x0000D528
		[DataSourceProperty]
		public bool IsFriendly
		{
			get
			{
				return this._isFriendly;
			}
			set
			{
				if (value != this._isFriendly)
				{
					this._isFriendly = value;
					base.OnPropertyChangedWithValue(value, "IsFriendly");
				}
			}
		}

		// Token: 0x0400017F RID: 383
		public const string NameTypeNeutral = "Normal";

		// Token: 0x04000180 RID: 384
		public const string NameTypeFriendly = "Friendly";

		// Token: 0x04000181 RID: 385
		public const string NameTypeEnemy = "Enemy";

		// Token: 0x04000182 RID: 386
		public const string NameTypeNoble = "Noble";

		// Token: 0x04000183 RID: 387
		public const string NameTypePassage = "Passage";

		// Token: 0x04000184 RID: 388
		public const string NameTypeEnemyPassage = "Passage";

		// Token: 0x04000185 RID: 389
		public const string IconTypeCommonArea = "common_area";

		// Token: 0x04000186 RID: 390
		public const string IconTypeCharacter = "character";

		// Token: 0x04000187 RID: 391
		public const string IconTypePrisoner = "prisoner";

		// Token: 0x04000188 RID: 392
		public const string IconTypeNoble = "noble";

		// Token: 0x04000189 RID: 393
		public const string IconTypeBarber = "barber";

		// Token: 0x0400018A RID: 394
		public const string IconTypeBlacksmith = "blacksmith";

		// Token: 0x0400018B RID: 395
		public const string IconTypeGameHost = "game_host";

		// Token: 0x0400018C RID: 396
		public const string IconTypeHermit = "hermit";

		// Token: 0x0400018D RID: 397
		private Func<Vec3> _getPosition = () => Vec3.Zero;

		// Token: 0x0400018E RID: 398
		private Func<string> _getMarkerObjectName = () => string.Empty;

		// Token: 0x04000194 RID: 404
		private MBBindingList<QuestMarkerVM> _quests;

		// Token: 0x04000195 RID: 405
		private Vec2 _screenPosition;

		// Token: 0x04000196 RID: 406
		private int _distance;

		// Token: 0x04000197 RID: 407
		private string _name;

		// Token: 0x04000198 RID: 408
		private string _iconType = string.Empty;

		// Token: 0x04000199 RID: 409
		private string _nameType = string.Empty;

		// Token: 0x0400019A RID: 410
		private bool _isEnabled;

		// Token: 0x0400019B RID: 411
		private bool _isTracked;

		// Token: 0x0400019C RID: 412
		private bool _isQuestMainStory;

		// Token: 0x0400019D RID: 413
		private bool _isEnemy;

		// Token: 0x0400019E RID: 414
		private bool _isFriendly;

		// Token: 0x02000084 RID: 132
		private class QuestMarkerComparer : IComparer<QuestMarkerVM>
		{
			// Token: 0x06000551 RID: 1361 RVA: 0x00014C30 File Offset: 0x00012E30
			public int Compare(QuestMarkerVM x, QuestMarkerVM y)
			{
				return x.QuestMarkerType.CompareTo(y.QuestMarkerType);
			}
		}
	}
}
