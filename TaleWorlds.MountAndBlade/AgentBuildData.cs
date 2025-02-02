using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020000F9 RID: 249
	public class AgentBuildData
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x000155D0 File Offset: 0x000137D0
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x000155D8 File Offset: 0x000137D8
		public AgentData AgentData { get; private set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x000155E1 File Offset: 0x000137E1
		public BasicCharacterObject AgentCharacter
		{
			get
			{
				return this.AgentData.AgentCharacter;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x000155EE File Offset: 0x000137EE
		public Monster AgentMonster
		{
			get
			{
				return this.AgentData.AgentMonster;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000155FB File Offset: 0x000137FB
		public Equipment AgentOverridenSpawnEquipment
		{
			get
			{
				return this.AgentData.AgentOverridenEquipment;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00015608 File Offset: 0x00013808
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x00015610 File Offset: 0x00013810
		public MissionEquipment AgentOverridenSpawnMissionEquipment { get; private set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00015619 File Offset: 0x00013819
		public int AgentEquipmentSeed
		{
			get
			{
				return this.AgentData.AgentEquipmentSeed;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00015626 File Offset: 0x00013826
		public bool AgentNoHorses
		{
			get
			{
				return this.AgentData.AgentNoHorses;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00015633 File Offset: 0x00013833
		public string AgentMountKey
		{
			get
			{
				return this.AgentData.AgentMountKey;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00015640 File Offset: 0x00013840
		public bool AgentNoWeapons
		{
			get
			{
				return this.AgentData.AgentNoWeapons;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0001564D File Offset: 0x0001384D
		public bool AgentNoArmor
		{
			get
			{
				return this.AgentData.AgentNoArmor;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0001565A File Offset: 0x0001385A
		public bool AgentFixedEquipment
		{
			get
			{
				return this.AgentData.AgentFixedEquipment;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00015667 File Offset: 0x00013867
		public bool AgentCivilianEquipment
		{
			get
			{
				return this.AgentData.AgentCivilianEquipment;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00015674 File Offset: 0x00013874
		public uint AgentClothingColor1
		{
			get
			{
				return this.AgentData.AgentClothingColor1;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00015681 File Offset: 0x00013881
		public uint AgentClothingColor2
		{
			get
			{
				return this.AgentData.AgentClothingColor2;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0001568E File Offset: 0x0001388E
		public bool BodyPropertiesOverriden
		{
			get
			{
				return this.AgentData.BodyPropertiesOverriden;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0001569B File Offset: 0x0001389B
		public BodyProperties AgentBodyProperties
		{
			get
			{
				return this.AgentData.AgentBodyProperties;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x000156A8 File Offset: 0x000138A8
		public bool AgeOverriden
		{
			get
			{
				return this.AgentData.AgeOverriden;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x000156B5 File Offset: 0x000138B5
		public int AgentAge
		{
			get
			{
				return this.AgentData.AgentAge;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x000156C2 File Offset: 0x000138C2
		public bool GenderOverriden
		{
			get
			{
				return this.AgentData.GenderOverriden;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x000156CF File Offset: 0x000138CF
		public bool AgentIsFemale
		{
			get
			{
				return this.AgentData.AgentIsFemale;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x000156DC File Offset: 0x000138DC
		public int AgentRace
		{
			get
			{
				return this.AgentData.AgentRace;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x000156E9 File Offset: 0x000138E9
		public IAgentOriginBase AgentOrigin
		{
			get
			{
				return this.AgentData.AgentOrigin;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x000156F6 File Offset: 0x000138F6
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x000156FE File Offset: 0x000138FE
		public Agent.ControllerType AgentController { get; private set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00015707 File Offset: 0x00013907
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x0001570F File Offset: 0x0001390F
		public Team AgentTeam { get; private set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x00015718 File Offset: 0x00013918
		// (set) Token: 0x06000B59 RID: 2905 RVA: 0x00015720 File Offset: 0x00013920
		public bool AgentIsReinforcement { get; private set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x00015729 File Offset: 0x00013929
		// (set) Token: 0x06000B5B RID: 2907 RVA: 0x00015731 File Offset: 0x00013931
		public bool AgentSpawnsIntoOwnFormation { get; private set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0001573A File Offset: 0x0001393A
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x00015742 File Offset: 0x00013942
		public bool AgentSpawnsUsingOwnTroopClass { get; private set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x0001574B File Offset: 0x0001394B
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x00015753 File Offset: 0x00013953
		public float MakeUnitStandOutDistance { get; private set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0001575C File Offset: 0x0001395C
		// (set) Token: 0x06000B61 RID: 2913 RVA: 0x00015764 File Offset: 0x00013964
		public Vec3? AgentInitialPosition { get; private set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0001576D File Offset: 0x0001396D
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00015775 File Offset: 0x00013975
		public Vec2? AgentInitialDirection { get; private set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0001577E File Offset: 0x0001397E
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x00015786 File Offset: 0x00013986
		public Formation AgentFormation { get; private set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0001578F File Offset: 0x0001398F
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x00015797 File Offset: 0x00013997
		public int AgentFormationTroopSpawnCount { get; private set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x000157A0 File Offset: 0x000139A0
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x000157A8 File Offset: 0x000139A8
		public int AgentFormationTroopSpawnIndex { get; private set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x000157B1 File Offset: 0x000139B1
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x000157B9 File Offset: 0x000139B9
		public MissionPeer AgentMissionPeer { get; private set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x000157C2 File Offset: 0x000139C2
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x000157CA File Offset: 0x000139CA
		public MissionPeer OwningAgentMissionPeer { get; private set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x000157D3 File Offset: 0x000139D3
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x000157DB File Offset: 0x000139DB
		public bool AgentIndexOverriden { get; private set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x000157E4 File Offset: 0x000139E4
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x000157EC File Offset: 0x000139EC
		public int AgentIndex { get; private set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x000157F5 File Offset: 0x000139F5
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x000157FD File Offset: 0x000139FD
		public bool AgentMountIndexOverriden { get; private set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B74 RID: 2932 RVA: 0x00015806 File Offset: 0x00013A06
		// (set) Token: 0x06000B75 RID: 2933 RVA: 0x0001580E File Offset: 0x00013A0E
		public int AgentMountIndex { get; private set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00015817 File Offset: 0x00013A17
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x0001581F File Offset: 0x00013A1F
		public int AgentVisualsIndex { get; private set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x00015828 File Offset: 0x00013A28
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x00015830 File Offset: 0x00013A30
		public Banner AgentBanner { get; private set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00015839 File Offset: 0x00013A39
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x00015841 File Offset: 0x00013A41
		public ItemObject AgentBannerItem { get; private set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0001584A File Offset: 0x00013A4A
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x00015852 File Offset: 0x00013A52
		public ItemObject AgentBannerReplacementWeaponItem { get; private set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0001585B File Offset: 0x00013A5B
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x00015863 File Offset: 0x00013A63
		public bool AgentCanSpawnOutsideOfMissionBoundary { get; private set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0001586C File Offset: 0x00013A6C
		public bool RandomizeColors
		{
			get
			{
				return this.AgentCharacter != null && !this.AgentCharacter.IsHero && this.AgentMissionPeer == null;
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0001588E File Offset: 0x00013A8E
		private AgentBuildData()
		{
			this.AgentController = Agent.ControllerType.AI;
			this.AgentTeam = TaleWorlds.MountAndBlade.Team.Invalid;
			this.AgentFormation = null;
			this.AgentMissionPeer = null;
			this.AgentFormationTroopSpawnIndex = -1;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000158BD File Offset: 0x00013ABD
		public AgentBuildData(AgentData agentData) : this()
		{
			this.AgentData = agentData;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000158CC File Offset: 0x00013ACC
		public AgentBuildData(IAgentOriginBase agentOrigin) : this()
		{
			this.AgentData = new AgentData(agentOrigin);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000158E0 File Offset: 0x00013AE0
		public AgentBuildData(BasicCharacterObject characterObject) : this()
		{
			this.AgentData = new AgentData(characterObject);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000158F4 File Offset: 0x00013AF4
		public AgentBuildData Character(BasicCharacterObject characterObject)
		{
			this.AgentData.Character(characterObject);
			return this;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00015904 File Offset: 0x00013B04
		public AgentBuildData Controller(Agent.ControllerType controller)
		{
			this.AgentController = controller;
			return this;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0001590E File Offset: 0x00013B0E
		public AgentBuildData Team(Team team)
		{
			this.AgentTeam = team;
			return this;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00015918 File Offset: 0x00013B18
		public AgentBuildData IsReinforcement(bool isReinforcement)
		{
			this.AgentIsReinforcement = isReinforcement;
			return this;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00015922 File Offset: 0x00013B22
		public AgentBuildData SpawnsIntoOwnFormation(bool spawnIntoOwnFormation)
		{
			this.AgentSpawnsIntoOwnFormation = spawnIntoOwnFormation;
			return this;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0001592C File Offset: 0x00013B2C
		public AgentBuildData SpawnsUsingOwnTroopClass(bool spawnUsingOwnTroopClass)
		{
			this.AgentSpawnsUsingOwnTroopClass = spawnUsingOwnTroopClass;
			return this;
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00015936 File Offset: 0x00013B36
		public AgentBuildData MakeUnitStandOutOfFormationDistance(float makeUnitStandOutDistance)
		{
			this.MakeUnitStandOutDistance = makeUnitStandOutDistance;
			return this;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00015940 File Offset: 0x00013B40
		public AgentBuildData InitialPosition(in Vec3 position)
		{
			this.AgentInitialPosition = new Vec3?(position);
			return this;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00015954 File Offset: 0x00013B54
		public AgentBuildData InitialDirection(in Vec2 direction)
		{
			this.AgentInitialDirection = new Vec2?(direction);
			return this;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00015968 File Offset: 0x00013B68
		public AgentBuildData InitialFrameFromSpawnPointEntity(GameEntity entity)
		{
			MatrixFrame globalFrame = entity.GetGlobalFrame();
			this.AgentInitialPosition = new Vec3?(globalFrame.origin);
			this.AgentInitialDirection = new Vec2?(globalFrame.rotation.f.AsVec2.Normalized());
			return this;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000159B2 File Offset: 0x00013BB2
		public AgentBuildData Formation(Formation formation)
		{
			this.AgentFormation = formation;
			return this;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000159BC File Offset: 0x00013BBC
		public AgentBuildData Monster(Monster monster)
		{
			this.AgentData.Monster(monster);
			return this;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000159CC File Offset: 0x00013BCC
		public AgentBuildData VisualsIndex(int index)
		{
			this.AgentVisualsIndex = index;
			return this;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000159D6 File Offset: 0x00013BD6
		public AgentBuildData Equipment(Equipment equipment)
		{
			this.AgentData.Equipment(equipment);
			return this;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000159E6 File Offset: 0x00013BE6
		public AgentBuildData MissionEquipment(MissionEquipment missionEquipment)
		{
			this.AgentOverridenSpawnMissionEquipment = missionEquipment;
			return this;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000159F0 File Offset: 0x00013BF0
		public AgentBuildData EquipmentSeed(int seed)
		{
			this.AgentData.EquipmentSeed(seed);
			return this;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00015A00 File Offset: 0x00013C00
		public AgentBuildData NoHorses(bool noHorses)
		{
			this.AgentData.NoHorses(noHorses);
			return this;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00015A10 File Offset: 0x00013C10
		public AgentBuildData NoWeapons(bool noWeapons)
		{
			this.AgentData.NoWeapons(noWeapons);
			return this;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00015A20 File Offset: 0x00013C20
		public AgentBuildData NoArmor(bool noArmor)
		{
			this.AgentData.NoArmor(noArmor);
			return this;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00015A30 File Offset: 0x00013C30
		public AgentBuildData FixedEquipment(bool fixedEquipment)
		{
			this.AgentData.FixedEquipment(fixedEquipment);
			return this;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00015A40 File Offset: 0x00013C40
		public AgentBuildData CivilianEquipment(bool civilianEquipment)
		{
			this.AgentData.CivilianEquipment(civilianEquipment);
			return this;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00015A50 File Offset: 0x00013C50
		public AgentBuildData ClothingColor1(uint color)
		{
			this.AgentData.ClothingColor1(color);
			return this;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00015A60 File Offset: 0x00013C60
		public AgentBuildData ClothingColor2(uint color)
		{
			this.AgentData.ClothingColor2(color);
			return this;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00015A70 File Offset: 0x00013C70
		public AgentBuildData MissionPeer(MissionPeer missionPeer)
		{
			this.AgentMissionPeer = missionPeer;
			return this;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00015A7A File Offset: 0x00013C7A
		public AgentBuildData OwningMissionPeer(MissionPeer missionPeer)
		{
			this.OwningAgentMissionPeer = missionPeer;
			return this;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00015A84 File Offset: 0x00013C84
		public AgentBuildData BodyProperties(BodyProperties bodyProperties)
		{
			this.AgentData.BodyProperties(bodyProperties);
			return this;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00015A94 File Offset: 0x00013C94
		public AgentBuildData Age(int age)
		{
			this.AgentData.Age(age);
			return this;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00015AA4 File Offset: 0x00013CA4
		public AgentBuildData TroopOrigin(IAgentOriginBase troopOrigin)
		{
			this.AgentData.TroopOrigin(troopOrigin);
			return this;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00015AB4 File Offset: 0x00013CB4
		public AgentBuildData IsFemale(bool isFemale)
		{
			this.AgentData.IsFemale(isFemale);
			return this;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00015AC4 File Offset: 0x00013CC4
		public AgentBuildData Race(int race)
		{
			this.AgentData.Race(race);
			return this;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00015AD4 File Offset: 0x00013CD4
		public AgentBuildData MountKey(string mountKey)
		{
			this.AgentData.MountKey(mountKey);
			return this;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00015AE4 File Offset: 0x00013CE4
		public AgentBuildData Index(int index)
		{
			this.AgentIndex = index;
			this.AgentIndexOverriden = true;
			return this;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00015AF5 File Offset: 0x00013CF5
		public AgentBuildData MountIndex(int mountIndex)
		{
			this.AgentMountIndex = mountIndex;
			this.AgentMountIndexOverriden = true;
			return this;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00015B06 File Offset: 0x00013D06
		public AgentBuildData Banner(Banner banner)
		{
			this.AgentBanner = banner;
			return this;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00015B10 File Offset: 0x00013D10
		public AgentBuildData BannerItem(ItemObject bannerItem)
		{
			this.AgentBannerItem = bannerItem;
			return this;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00015B1A File Offset: 0x00013D1A
		public AgentBuildData BannerReplacementWeaponItem(ItemObject weaponItem)
		{
			this.AgentBannerReplacementWeaponItem = weaponItem;
			return this;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00015B24 File Offset: 0x00013D24
		public AgentBuildData FormationTroopSpawnCount(int formationTroopCount)
		{
			this.AgentFormationTroopSpawnCount = formationTroopCount;
			return this;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00015B2E File Offset: 0x00013D2E
		public AgentBuildData FormationTroopSpawnIndex(int formationTroopIndex)
		{
			this.AgentFormationTroopSpawnIndex = formationTroopIndex;
			return this;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00015B38 File Offset: 0x00013D38
		public AgentBuildData CanSpawnOutsideOfMissionBoundary(bool canSpawn)
		{
			this.AgentCanSpawnOutsideOfMissionBoundary = canSpawn;
			return this;
		}
	}
}
