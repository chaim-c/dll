using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F8 RID: 760
	public class MultiplayerClassDivisions
	{
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x0009ED0B File Offset: 0x0009CF0B
		// (set) Token: 0x0600295C RID: 10588 RVA: 0x0009ED12 File Offset: 0x0009CF12
		public static List<MultiplayerClassDivisions.MPHeroClassGroup> MultiplayerHeroClassGroups { get; private set; }

		// Token: 0x0600295D RID: 10589 RVA: 0x0009ED1C File Offset: 0x0009CF1C
		public static IEnumerable<MultiplayerClassDivisions.MPHeroClass> GetMPHeroClasses(BasicCultureObject culture)
		{
			return from x in MBObjectManager.Instance.GetObjectTypeList<MultiplayerClassDivisions.MPHeroClass>()
			where x.Culture == culture
			select x;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x0009ED51 File Offset: 0x0009CF51
		public static MBReadOnlyList<MultiplayerClassDivisions.MPHeroClass> GetMPHeroClasses()
		{
			return MBObjectManager.Instance.GetObjectTypeList<MultiplayerClassDivisions.MPHeroClass>();
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x0009ED60 File Offset: 0x0009CF60
		public static MultiplayerClassDivisions.MPHeroClass GetMPHeroClassForCharacter(BasicCharacterObject character)
		{
			return MBObjectManager.Instance.GetObjectTypeList<MultiplayerClassDivisions.MPHeroClass>().FirstOrDefault((MultiplayerClassDivisions.MPHeroClass x) => x.HeroCharacter == character || x.TroopCharacter == character);
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x0009ED98 File Offset: 0x0009CF98
		public static List<List<IReadOnlyPerkObject>> GetAllPerksForHeroClass(MultiplayerClassDivisions.MPHeroClass heroClass, string forcedForGameMode = null)
		{
			List<List<IReadOnlyPerkObject>> list = new List<List<IReadOnlyPerkObject>>();
			for (int i = 0; i < 3; i++)
			{
				list.Add(heroClass.GetAllAvailablePerksForListIndex(i, forcedForGameMode).ToList<IReadOnlyPerkObject>());
			}
			return list;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x0009EDCC File Offset: 0x0009CFCC
		public static MultiplayerClassDivisions.MPHeroClass GetMPHeroClassForPeer(MissionPeer peer, bool skipTeamCheck = false)
		{
			Team team = peer.Team;
			if ((!skipTeamCheck && (team == null || team.Side == BattleSideEnum.None)) || (peer.SelectedTroopIndex < 0 && peer.ControlledAgent == null))
			{
				return null;
			}
			if (peer.ControlledAgent != null)
			{
				return MultiplayerClassDivisions.GetMPHeroClassForCharacter(peer.ControlledAgent.Character);
			}
			if (peer.SelectedTroopIndex >= 0)
			{
				return MultiplayerClassDivisions.GetMPHeroClasses(peer.Culture).ToList<MultiplayerClassDivisions.MPHeroClass>()[peer.SelectedTroopIndex];
			}
			Debug.FailedAssert("This should not be seen.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\Gameplay\\MultiplayerClassDivisions.cs", "GetMPHeroClassForPeer", 255);
			return null;
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x0009EE5C File Offset: 0x0009D05C
		public static TargetIconType GetMPHeroClassForFormation(Formation formation)
		{
			switch (formation.PhysicalClass)
			{
			case FormationClass.Infantry:
				return TargetIconType.Infantry_Light;
			case FormationClass.Ranged:
				return TargetIconType.Archer_Light;
			case FormationClass.Cavalry:
				return TargetIconType.Cavalry_Light;
			default:
				return TargetIconType.HorseArcher_Light;
			}
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x0009EE8C File Offset: 0x0009D08C
		public static List<List<IReadOnlyPerkObject>> GetAvailablePerksForPeer(MissionPeer missionPeer)
		{
			if (((missionPeer != null) ? missionPeer.Team : null) != null)
			{
				return MultiplayerClassDivisions.GetAllPerksForHeroClass(MultiplayerClassDivisions.GetMPHeroClassForPeer(missionPeer, false), null);
			}
			return new List<List<IReadOnlyPerkObject>>();
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x0009EEB0 File Offset: 0x0009D0B0
		public static void Initialize()
		{
			MultiplayerClassDivisions.MultiplayerHeroClassGroups = new List<MultiplayerClassDivisions.MPHeroClassGroup>
			{
				new MultiplayerClassDivisions.MPHeroClassGroup("Infantry"),
				new MultiplayerClassDivisions.MPHeroClassGroup("Ranged"),
				new MultiplayerClassDivisions.MPHeroClassGroup("Cavalry"),
				new MultiplayerClassDivisions.MPHeroClassGroup("HorseArcher")
			};
			MultiplayerClassDivisions.AvailableCultures = from x in MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>().ToArray()
			where x.IsMainCulture
			select x;
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x0009EF3F File Offset: 0x0009D13F
		public static void Release()
		{
			MultiplayerClassDivisions.MultiplayerHeroClassGroups.Clear();
			MultiplayerClassDivisions.AvailableCultures = null;
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x0009EF51 File Offset: 0x0009D151
		private static BasicCharacterObject GetMPCharacter(string stringId)
		{
			return MBObjectManager.Instance.GetObject<BasicCharacterObject>(stringId);
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x0009EF60 File Offset: 0x0009D160
		public static int GetMinimumTroopCost(BasicCultureObject culture = null)
		{
			MBReadOnlyList<MultiplayerClassDivisions.MPHeroClass> mpheroClasses = MultiplayerClassDivisions.GetMPHeroClasses();
			if (culture != null)
			{
				return (from c in mpheroClasses
				where c.Culture == culture
				select c).Min((MultiplayerClassDivisions.MPHeroClass troop) => troop.TroopCost);
			}
			return mpheroClasses.Min((MultiplayerClassDivisions.MPHeroClass troop) => troop.TroopCost);
		}

		// Token: 0x04000FF8 RID: 4088
		public static IEnumerable<BasicCultureObject> AvailableCultures;

		// Token: 0x020005B1 RID: 1457
		public class MPHeroClass : MBObjectBase
		{
			// Token: 0x170009B9 RID: 2489
			// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x000E73B3 File Offset: 0x000E55B3
			// (set) Token: 0x06003AA9 RID: 15017 RVA: 0x000E73BB File Offset: 0x000E55BB
			public BasicCharacterObject HeroCharacter { get; private set; }

			// Token: 0x170009BA RID: 2490
			// (get) Token: 0x06003AAA RID: 15018 RVA: 0x000E73C4 File Offset: 0x000E55C4
			// (set) Token: 0x06003AAB RID: 15019 RVA: 0x000E73CC File Offset: 0x000E55CC
			public BasicCharacterObject TroopCharacter { get; private set; }

			// Token: 0x170009BB RID: 2491
			// (get) Token: 0x06003AAC RID: 15020 RVA: 0x000E73D5 File Offset: 0x000E55D5
			// (set) Token: 0x06003AAD RID: 15021 RVA: 0x000E73DD File Offset: 0x000E55DD
			public BasicCharacterObject BannerBearerCharacter { get; private set; }

			// Token: 0x170009BC RID: 2492
			// (get) Token: 0x06003AAE RID: 15022 RVA: 0x000E73E6 File Offset: 0x000E55E6
			// (set) Token: 0x06003AAF RID: 15023 RVA: 0x000E73EE File Offset: 0x000E55EE
			public BasicCultureObject Culture { get; private set; }

			// Token: 0x170009BD RID: 2493
			// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x000E73F7 File Offset: 0x000E55F7
			// (set) Token: 0x06003AB1 RID: 15025 RVA: 0x000E73FF File Offset: 0x000E55FF
			public MultiplayerClassDivisions.MPHeroClassGroup ClassGroup { get; private set; }

			// Token: 0x170009BE RID: 2494
			// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x000E7408 File Offset: 0x000E5608
			// (set) Token: 0x06003AB3 RID: 15027 RVA: 0x000E7410 File Offset: 0x000E5610
			public string HeroIdleAnim { get; private set; }

			// Token: 0x170009BF RID: 2495
			// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x000E7419 File Offset: 0x000E5619
			// (set) Token: 0x06003AB5 RID: 15029 RVA: 0x000E7421 File Offset: 0x000E5621
			public string HeroMountIdleAnim { get; private set; }

			// Token: 0x170009C0 RID: 2496
			// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x000E742A File Offset: 0x000E562A
			// (set) Token: 0x06003AB7 RID: 15031 RVA: 0x000E7432 File Offset: 0x000E5632
			public string TroopIdleAnim { get; private set; }

			// Token: 0x170009C1 RID: 2497
			// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x000E743B File Offset: 0x000E563B
			// (set) Token: 0x06003AB9 RID: 15033 RVA: 0x000E7443 File Offset: 0x000E5643
			public string TroopMountIdleAnim { get; private set; }

			// Token: 0x170009C2 RID: 2498
			// (get) Token: 0x06003ABA RID: 15034 RVA: 0x000E744C File Offset: 0x000E564C
			// (set) Token: 0x06003ABB RID: 15035 RVA: 0x000E7454 File Offset: 0x000E5654
			public int ArmorValue { get; private set; }

			// Token: 0x170009C3 RID: 2499
			// (get) Token: 0x06003ABC RID: 15036 RVA: 0x000E745D File Offset: 0x000E565D
			// (set) Token: 0x06003ABD RID: 15037 RVA: 0x000E7465 File Offset: 0x000E5665
			public int Health { get; private set; }

			// Token: 0x170009C4 RID: 2500
			// (get) Token: 0x06003ABE RID: 15038 RVA: 0x000E746E File Offset: 0x000E566E
			// (set) Token: 0x06003ABF RID: 15039 RVA: 0x000E7476 File Offset: 0x000E5676
			public float HeroMovementSpeedMultiplier { get; private set; }

			// Token: 0x170009C5 RID: 2501
			// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x000E747F File Offset: 0x000E567F
			// (set) Token: 0x06003AC1 RID: 15041 RVA: 0x000E7487 File Offset: 0x000E5687
			public float HeroCombatMovementSpeedMultiplier { get; private set; }

			// Token: 0x170009C6 RID: 2502
			// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x000E7490 File Offset: 0x000E5690
			// (set) Token: 0x06003AC3 RID: 15043 RVA: 0x000E7498 File Offset: 0x000E5698
			public float HeroTopSpeedReachDuration { get; private set; }

			// Token: 0x170009C7 RID: 2503
			// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x000E74A1 File Offset: 0x000E56A1
			// (set) Token: 0x06003AC5 RID: 15045 RVA: 0x000E74A9 File Offset: 0x000E56A9
			public float TroopMovementSpeedMultiplier { get; private set; }

			// Token: 0x170009C8 RID: 2504
			// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x000E74B2 File Offset: 0x000E56B2
			// (set) Token: 0x06003AC7 RID: 15047 RVA: 0x000E74BA File Offset: 0x000E56BA
			public float TroopCombatMovementSpeedMultiplier { get; private set; }

			// Token: 0x170009C9 RID: 2505
			// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x000E74C3 File Offset: 0x000E56C3
			// (set) Token: 0x06003AC9 RID: 15049 RVA: 0x000E74CB File Offset: 0x000E56CB
			public float TroopTopSpeedReachDuration { get; private set; }

			// Token: 0x170009CA RID: 2506
			// (get) Token: 0x06003ACA RID: 15050 RVA: 0x000E74D4 File Offset: 0x000E56D4
			// (set) Token: 0x06003ACB RID: 15051 RVA: 0x000E74DC File Offset: 0x000E56DC
			public float TroopMultiplier { get; private set; }

			// Token: 0x170009CB RID: 2507
			// (get) Token: 0x06003ACC RID: 15052 RVA: 0x000E74E5 File Offset: 0x000E56E5
			// (set) Token: 0x06003ACD RID: 15053 RVA: 0x000E74ED File Offset: 0x000E56ED
			public int TroopCost { get; private set; }

			// Token: 0x170009CC RID: 2508
			// (get) Token: 0x06003ACE RID: 15054 RVA: 0x000E74F6 File Offset: 0x000E56F6
			// (set) Token: 0x06003ACF RID: 15055 RVA: 0x000E74FE File Offset: 0x000E56FE
			public int TroopCasualCost { get; private set; }

			// Token: 0x170009CD RID: 2509
			// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x000E7507 File Offset: 0x000E5707
			// (set) Token: 0x06003AD1 RID: 15057 RVA: 0x000E750F File Offset: 0x000E570F
			public int TroopBattleCost { get; private set; }

			// Token: 0x170009CE RID: 2510
			// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x000E7518 File Offset: 0x000E5718
			// (set) Token: 0x06003AD3 RID: 15059 RVA: 0x000E7520 File Offset: 0x000E5720
			public int MeleeAI { get; private set; }

			// Token: 0x170009CF RID: 2511
			// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x000E7529 File Offset: 0x000E5729
			// (set) Token: 0x06003AD5 RID: 15061 RVA: 0x000E7531 File Offset: 0x000E5731
			public int RangedAI { get; private set; }

			// Token: 0x170009D0 RID: 2512
			// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x000E753A File Offset: 0x000E573A
			// (set) Token: 0x06003AD7 RID: 15063 RVA: 0x000E7542 File Offset: 0x000E5742
			public TextObject HeroInformation { get; private set; }

			// Token: 0x170009D1 RID: 2513
			// (get) Token: 0x06003AD8 RID: 15064 RVA: 0x000E754B File Offset: 0x000E574B
			// (set) Token: 0x06003AD9 RID: 15065 RVA: 0x000E7553 File Offset: 0x000E5753
			public TextObject TroopInformation { get; private set; }

			// Token: 0x170009D2 RID: 2514
			// (get) Token: 0x06003ADA RID: 15066 RVA: 0x000E755C File Offset: 0x000E575C
			// (set) Token: 0x06003ADB RID: 15067 RVA: 0x000E7564 File Offset: 0x000E5764
			public TargetIconType IconType { get; private set; }

			// Token: 0x170009D3 RID: 2515
			// (get) Token: 0x06003ADC RID: 15068 RVA: 0x000E756D File Offset: 0x000E576D
			public TextObject HeroName
			{
				get
				{
					return this.HeroCharacter.Name;
				}
			}

			// Token: 0x170009D4 RID: 2516
			// (get) Token: 0x06003ADD RID: 15069 RVA: 0x000E757A File Offset: 0x000E577A
			public TextObject TroopName
			{
				get
				{
					return this.TroopCharacter.Name;
				}
			}

			// Token: 0x06003ADE RID: 15070 RVA: 0x000E7587 File Offset: 0x000E5787
			public override bool Equals(object obj)
			{
				return obj is MultiplayerClassDivisions.MPHeroClass && ((MultiplayerClassDivisions.MPHeroClass)obj).StringId.Equals(base.StringId);
			}

			// Token: 0x06003ADF RID: 15071 RVA: 0x000E75A9 File Offset: 0x000E57A9
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06003AE0 RID: 15072 RVA: 0x000E75B4 File Offset: 0x000E57B4
			public List<IReadOnlyPerkObject> GetAllAvailablePerksForListIndex(int index, string forcedForGameMode = null)
			{
				string value = forcedForGameMode ?? MultiplayerOptions.OptionType.GameType.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
				List<IReadOnlyPerkObject> list = new List<IReadOnlyPerkObject>();
				foreach (IReadOnlyPerkObject readOnlyPerkObject in this._perks)
				{
					foreach (string text in readOnlyPerkObject.GameModes)
					{
						if ((text.Equals(value, StringComparison.InvariantCultureIgnoreCase) || text.Equals("all", StringComparison.InvariantCultureIgnoreCase)) && readOnlyPerkObject.PerkListIndex == index)
						{
							list.Add(readOnlyPerkObject);
							break;
						}
					}
				}
				return list;
			}

			// Token: 0x06003AE1 RID: 15073 RVA: 0x000E7680 File Offset: 0x000E5880
			public override void Deserialize(MBObjectManager objectManager, XmlNode node)
			{
				base.Deserialize(objectManager, node);
				this.HeroCharacter = MultiplayerClassDivisions.GetMPCharacter(node.Attributes["hero"].Value);
				this.TroopCharacter = MultiplayerClassDivisions.GetMPCharacter(node.Attributes["troop"].Value);
				XmlAttribute xmlAttribute = node.Attributes["banner_bearer"];
				string text = (xmlAttribute != null) ? xmlAttribute.Value : null;
				if (text != null)
				{
					this.BannerBearerCharacter = MultiplayerClassDivisions.GetMPCharacter(text);
				}
				XmlAttribute xmlAttribute2 = node.Attributes["hero_idle_anim"];
				this.HeroIdleAnim = ((xmlAttribute2 != null) ? xmlAttribute2.Value : null);
				XmlAttribute xmlAttribute3 = node.Attributes["hero_mount_idle_anim"];
				this.HeroMountIdleAnim = ((xmlAttribute3 != null) ? xmlAttribute3.Value : null);
				XmlAttribute xmlAttribute4 = node.Attributes["troop_idle_anim"];
				this.TroopIdleAnim = ((xmlAttribute4 != null) ? xmlAttribute4.Value : null);
				XmlAttribute xmlAttribute5 = node.Attributes["troop_mount_idle_anim"];
				this.TroopMountIdleAnim = ((xmlAttribute5 != null) ? xmlAttribute5.Value : null);
				this.Culture = this.HeroCharacter.Culture;
				this.ClassGroup = new MultiplayerClassDivisions.MPHeroClassGroup(this.HeroCharacter.DefaultFormationClass.GetName());
				this.TroopMultiplier = (float)Convert.ToDouble(node.Attributes["multiplier"].Value);
				this.TroopCost = Convert.ToInt32(node.Attributes["cost"].Value);
				this.ArmorValue = Convert.ToInt32(node.Attributes["armor"].Value);
				XmlAttribute xmlAttribute6 = node.Attributes["casual_cost"];
				XmlAttribute xmlAttribute7 = node.Attributes["battle_cost"];
				this.TroopCasualCost = ((xmlAttribute6 != null) ? Convert.ToInt32(node.Attributes["casual_cost"].Value) : this.TroopCost);
				this.TroopBattleCost = ((xmlAttribute7 != null) ? Convert.ToInt32(node.Attributes["battle_cost"].Value) : this.TroopCost);
				this.Health = 100;
				this.MeleeAI = 50;
				this.RangedAI = 50;
				XmlNode xmlNode = node.Attributes["hitpoints"];
				if (xmlNode != null)
				{
					this.Health = Convert.ToInt32(xmlNode.Value);
				}
				this.HeroMovementSpeedMultiplier = (float)Convert.ToDouble(node.Attributes["movement_speed"].Value);
				this.HeroCombatMovementSpeedMultiplier = (float)Convert.ToDouble(node.Attributes["combat_movement_speed"].Value);
				this.HeroTopSpeedReachDuration = (float)Convert.ToDouble(node.Attributes["acceleration"].Value);
				XmlAttribute xmlAttribute8 = node.Attributes["troop_movement_speed"];
				XmlAttribute xmlAttribute9 = node.Attributes["troop_combat_movement_speed"];
				XmlAttribute xmlAttribute10 = node.Attributes["troop_acceleration"];
				this.TroopMovementSpeedMultiplier = ((xmlAttribute8 != null) ? ((float)Convert.ToDouble(xmlAttribute8.Value)) : this.HeroMovementSpeedMultiplier);
				this.TroopCombatMovementSpeedMultiplier = ((xmlAttribute9 != null) ? ((float)Convert.ToDouble(xmlAttribute9.Value)) : this.HeroCombatMovementSpeedMultiplier);
				this.TroopTopSpeedReachDuration = ((xmlAttribute10 != null) ? ((float)Convert.ToDouble(xmlAttribute10.Value)) : this.HeroTopSpeedReachDuration);
				this.MeleeAI = Convert.ToInt32(node.Attributes["melee_ai"].Value);
				this.RangedAI = Convert.ToInt32(node.Attributes["ranged_ai"].Value);
				TargetIconType iconType;
				if (Enum.TryParse<TargetIconType>(node.Attributes["icon"].Value, true, out iconType))
				{
					this.IconType = iconType;
				}
				foreach (object obj in node.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					if (xmlNode2.NodeType != XmlNodeType.Comment && xmlNode2.Name == "Perks")
					{
						this._perks = new List<IReadOnlyPerkObject>();
						foreach (object obj2 in xmlNode2.ChildNodes)
						{
							XmlNode xmlNode3 = (XmlNode)obj2;
							if (xmlNode3.NodeType != XmlNodeType.Comment)
							{
								this._perks.Add(MPPerkObject.Deserialize(xmlNode3));
							}
						}
					}
				}
			}

			// Token: 0x06003AE2 RID: 15074 RVA: 0x000E7B0C File Offset: 0x000E5D0C
			public bool IsTroopCharacter(BasicCharacterObject character)
			{
				return this.TroopCharacter == character;
			}

			// Token: 0x04001E21 RID: 7713
			private List<IReadOnlyPerkObject> _perks = new List<IReadOnlyPerkObject>();
		}

		// Token: 0x020005B2 RID: 1458
		public class MPHeroClassGroup
		{
			// Token: 0x06003AE4 RID: 15076 RVA: 0x000E7B2A File Offset: 0x000E5D2A
			public MPHeroClassGroup(string stringId)
			{
				this.StringId = stringId;
				this.Name = GameTexts.FindText("str_troop_type_name", this.StringId);
			}

			// Token: 0x06003AE5 RID: 15077 RVA: 0x000E7B4F File Offset: 0x000E5D4F
			public override bool Equals(object obj)
			{
				return obj is MultiplayerClassDivisions.MPHeroClassGroup && ((MultiplayerClassDivisions.MPHeroClassGroup)obj).StringId.Equals(this.StringId);
			}

			// Token: 0x06003AE6 RID: 15078 RVA: 0x000E7B71 File Offset: 0x000E5D71
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x04001E22 RID: 7714
			public readonly string StringId;

			// Token: 0x04001E23 RID: 7715
			public readonly TextObject Name;
		}
	}
}
