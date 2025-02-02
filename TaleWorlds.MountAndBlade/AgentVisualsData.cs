using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002B6 RID: 694
	public class AgentVisualsData
	{
		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x000905D8 File Offset: 0x0008E7D8
		// (set) Token: 0x060025F4 RID: 9716 RVA: 0x000905E0 File Offset: 0x0008E7E0
		public MBActionSet ActionSetData { get; private set; }

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x000905E9 File Offset: 0x0008E7E9
		// (set) Token: 0x060025F6 RID: 9718 RVA: 0x000905F1 File Offset: 0x0008E7F1
		public MatrixFrame FrameData { get; private set; }

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x000905FA File Offset: 0x0008E7FA
		// (set) Token: 0x060025F8 RID: 9720 RVA: 0x00090602 File Offset: 0x0008E802
		public BodyProperties BodyPropertiesData { get; private set; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x0009060B File Offset: 0x0008E80B
		// (set) Token: 0x060025FA RID: 9722 RVA: 0x00090613 File Offset: 0x0008E813
		public Equipment EquipmentData { get; private set; }

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x0009061C File Offset: 0x0008E81C
		// (set) Token: 0x060025FC RID: 9724 RVA: 0x00090624 File Offset: 0x0008E824
		public int RightWieldedItemIndexData { get; private set; }

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x0009062D File Offset: 0x0008E82D
		// (set) Token: 0x060025FE RID: 9726 RVA: 0x00090635 File Offset: 0x0008E835
		public int LeftWieldedItemIndexData { get; private set; }

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x0009063E File Offset: 0x0008E83E
		// (set) Token: 0x06002600 RID: 9728 RVA: 0x00090646 File Offset: 0x0008E846
		public SkeletonType SkeletonTypeData { get; private set; }

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x0009064F File Offset: 0x0008E84F
		// (set) Token: 0x06002602 RID: 9730 RVA: 0x00090657 File Offset: 0x0008E857
		public Banner BannerData { get; private set; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x00090660 File Offset: 0x0008E860
		// (set) Token: 0x06002604 RID: 9732 RVA: 0x00090668 File Offset: 0x0008E868
		public GameEntity CachedWeaponSlot0Entity { get; private set; }

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x00090671 File Offset: 0x0008E871
		// (set) Token: 0x06002606 RID: 9734 RVA: 0x00090679 File Offset: 0x0008E879
		public GameEntity CachedWeaponSlot1Entity { get; private set; }

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x00090682 File Offset: 0x0008E882
		// (set) Token: 0x06002608 RID: 9736 RVA: 0x0009068A File Offset: 0x0008E88A
		public GameEntity CachedWeaponSlot2Entity { get; private set; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x00090693 File Offset: 0x0008E893
		// (set) Token: 0x0600260A RID: 9738 RVA: 0x0009069B File Offset: 0x0008E89B
		public GameEntity CachedWeaponSlot3Entity { get; private set; }

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000906A4 File Offset: 0x0008E8A4
		// (set) Token: 0x0600260C RID: 9740 RVA: 0x000906AC File Offset: 0x0008E8AC
		public GameEntity CachedWeaponSlot4Entity { get; private set; }

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x000906B5 File Offset: 0x0008E8B5
		// (set) Token: 0x0600260E RID: 9742 RVA: 0x000906BD File Offset: 0x0008E8BD
		public Scene SceneData { get; private set; }

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x000906C6 File Offset: 0x0008E8C6
		// (set) Token: 0x06002610 RID: 9744 RVA: 0x000906CE File Offset: 0x0008E8CE
		public Monster MonsterData { get; private set; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x000906D7 File Offset: 0x0008E8D7
		// (set) Token: 0x06002612 RID: 9746 RVA: 0x000906DF File Offset: 0x0008E8DF
		public bool PrepareImmediatelyData { get; private set; }

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002613 RID: 9747 RVA: 0x000906E8 File Offset: 0x0008E8E8
		// (set) Token: 0x06002614 RID: 9748 RVA: 0x000906F0 File Offset: 0x0008E8F0
		public bool UseScaledWeaponsData { get; private set; }

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002615 RID: 9749 RVA: 0x000906F9 File Offset: 0x0008E8F9
		// (set) Token: 0x06002616 RID: 9750 RVA: 0x00090701 File Offset: 0x0008E901
		public bool UseTranslucencyData { get; private set; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002617 RID: 9751 RVA: 0x0009070A File Offset: 0x0008E90A
		// (set) Token: 0x06002618 RID: 9752 RVA: 0x00090712 File Offset: 0x0008E912
		public bool UseTesselationData { get; private set; }

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x0009071B File Offset: 0x0008E91B
		// (set) Token: 0x0600261A RID: 9754 RVA: 0x00090723 File Offset: 0x0008E923
		public bool UseMorphAnimsData { get; private set; }

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x0009072C File Offset: 0x0008E92C
		// (set) Token: 0x0600261C RID: 9756 RVA: 0x00090734 File Offset: 0x0008E934
		public uint ClothColor1Data { get; private set; }

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600261D RID: 9757 RVA: 0x0009073D File Offset: 0x0008E93D
		// (set) Token: 0x0600261E RID: 9758 RVA: 0x00090745 File Offset: 0x0008E945
		public uint ClothColor2Data { get; private set; }

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x0009074E File Offset: 0x0008E94E
		// (set) Token: 0x06002620 RID: 9760 RVA: 0x00090756 File Offset: 0x0008E956
		public float ScaleData { get; private set; }

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002621 RID: 9761 RVA: 0x0009075F File Offset: 0x0008E95F
		// (set) Token: 0x06002622 RID: 9762 RVA: 0x00090767 File Offset: 0x0008E967
		public string CharacterObjectStringIdData { get; private set; }

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002623 RID: 9763 RVA: 0x00090770 File Offset: 0x0008E970
		// (set) Token: 0x06002624 RID: 9764 RVA: 0x00090778 File Offset: 0x0008E978
		public ActionIndexCache ActionCodeData { get; private set; } = ActionIndexCache.act_none;

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x00090781 File Offset: 0x0008E981
		// (set) Token: 0x06002626 RID: 9766 RVA: 0x00090789 File Offset: 0x0008E989
		public GameEntity EntityData { get; private set; }

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x00090792 File Offset: 0x0008E992
		// (set) Token: 0x06002628 RID: 9768 RVA: 0x0009079A File Offset: 0x0008E99A
		public bool HasClippingPlaneData { get; private set; }

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x000907A3 File Offset: 0x0008E9A3
		// (set) Token: 0x0600262A RID: 9770 RVA: 0x000907AB File Offset: 0x0008E9AB
		public string MountCreationKeyData { get; private set; }

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x000907B4 File Offset: 0x0008E9B4
		// (set) Token: 0x0600262C RID: 9772 RVA: 0x000907BC File Offset: 0x0008E9BC
		public bool AddColorRandomnessData { get; private set; }

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x000907C5 File Offset: 0x0008E9C5
		// (set) Token: 0x0600262E RID: 9774 RVA: 0x000907CD File Offset: 0x0008E9CD
		public int RaceData { get; private set; }

		// Token: 0x0600262F RID: 9775 RVA: 0x000907D8 File Offset: 0x0008E9D8
		public AgentVisualsData(AgentVisualsData agentVisualsData)
		{
			this.AgentVisuals = agentVisualsData.AgentVisuals;
			this.ActionSetData = agentVisualsData.ActionSetData;
			this.FrameData = agentVisualsData.FrameData;
			this.BodyPropertiesData = agentVisualsData.BodyPropertiesData;
			this.EquipmentData = agentVisualsData.EquipmentData;
			this.RightWieldedItemIndexData = agentVisualsData.RightWieldedItemIndexData;
			this.LeftWieldedItemIndexData = agentVisualsData.LeftWieldedItemIndexData;
			this.SkeletonTypeData = agentVisualsData.SkeletonTypeData;
			this.BannerData = agentVisualsData.BannerData;
			this.CachedWeaponSlot0Entity = agentVisualsData.CachedWeaponSlot0Entity;
			this.CachedWeaponSlot1Entity = agentVisualsData.CachedWeaponSlot1Entity;
			this.CachedWeaponSlot2Entity = agentVisualsData.CachedWeaponSlot2Entity;
			this.CachedWeaponSlot3Entity = agentVisualsData.CachedWeaponSlot3Entity;
			this.CachedWeaponSlot4Entity = agentVisualsData.CachedWeaponSlot4Entity;
			this.SceneData = agentVisualsData.SceneData;
			this.MonsterData = agentVisualsData.MonsterData;
			this.PrepareImmediatelyData = agentVisualsData.PrepareImmediatelyData;
			this.UseScaledWeaponsData = agentVisualsData.UseScaledWeaponsData;
			this.UseTranslucencyData = agentVisualsData.UseTranslucencyData;
			this.UseTesselationData = agentVisualsData.UseTesselationData;
			this.UseMorphAnimsData = agentVisualsData.UseMorphAnimsData;
			this.ClothColor1Data = agentVisualsData.ClothColor1Data;
			this.ClothColor2Data = agentVisualsData.ClothColor2Data;
			this.ScaleData = agentVisualsData.ScaleData;
			this.ActionCodeData = agentVisualsData.ActionCodeData;
			this.EntityData = agentVisualsData.EntityData;
			this.CharacterObjectStringIdData = agentVisualsData.CharacterObjectStringIdData;
			this.HasClippingPlaneData = agentVisualsData.HasClippingPlaneData;
			this.MountCreationKeyData = agentVisualsData.MountCreationKeyData;
			this.AddColorRandomnessData = agentVisualsData.AddColorRandomnessData;
			this.RaceData = agentVisualsData.RaceData;
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x0009096A File Offset: 0x0008EB6A
		public AgentVisualsData()
		{
			this.ClothColor1Data = uint.MaxValue;
			this.ClothColor2Data = uint.MaxValue;
			this.RightWieldedItemIndexData = -1;
			this.LeftWieldedItemIndexData = -1;
			this.ScaleData = 0f;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000909A4 File Offset: 0x0008EBA4
		public AgentVisualsData Equipment(Equipment equipment)
		{
			this.EquipmentData = equipment;
			return this;
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000909AE File Offset: 0x0008EBAE
		public AgentVisualsData BodyProperties(BodyProperties bodyProperties)
		{
			this.BodyPropertiesData = bodyProperties;
			return this;
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000909B8 File Offset: 0x0008EBB8
		public AgentVisualsData Frame(MatrixFrame frame)
		{
			this.FrameData = frame;
			return this;
		}

		// Token: 0x06002634 RID: 9780 RVA: 0x000909C2 File Offset: 0x0008EBC2
		public AgentVisualsData ActionSet(MBActionSet actionSet)
		{
			this.ActionSetData = actionSet;
			return this;
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x000909CC File Offset: 0x0008EBCC
		public AgentVisualsData Scene(Scene scene)
		{
			this.SceneData = scene;
			return this;
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x000909D6 File Offset: 0x0008EBD6
		public AgentVisualsData Monster(Monster monster)
		{
			this.MonsterData = monster;
			return this;
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000909E0 File Offset: 0x0008EBE0
		public AgentVisualsData PrepareImmediately(bool prepareImmediately)
		{
			this.PrepareImmediatelyData = prepareImmediately;
			return this;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000909EA File Offset: 0x0008EBEA
		public AgentVisualsData UseScaledWeapons(bool useScaledWeapons)
		{
			this.UseScaledWeaponsData = useScaledWeapons;
			return this;
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000909F4 File Offset: 0x0008EBF4
		public AgentVisualsData SkeletonType(SkeletonType skeletonType)
		{
			this.SkeletonTypeData = skeletonType;
			return this;
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000909FE File Offset: 0x0008EBFE
		public AgentVisualsData UseMorphAnims(bool useMorphAnims)
		{
			this.UseMorphAnimsData = useMorphAnims;
			return this;
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x00090A08 File Offset: 0x0008EC08
		public AgentVisualsData ClothColor1(uint clothColor1)
		{
			this.ClothColor1Data = clothColor1;
			return this;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x00090A12 File Offset: 0x0008EC12
		public AgentVisualsData ClothColor2(uint clothColor2)
		{
			this.ClothColor2Data = clothColor2;
			return this;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x00090A1C File Offset: 0x0008EC1C
		public AgentVisualsData Banner(Banner banner)
		{
			this.BannerData = banner;
			return this;
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x00090A26 File Offset: 0x0008EC26
		public AgentVisualsData Race(int race)
		{
			this.RaceData = race;
			return this;
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x00090A30 File Offset: 0x0008EC30
		public GameEntity GetCachedWeaponEntity(EquipmentIndex slotIndex)
		{
			switch (slotIndex)
			{
			case EquipmentIndex.WeaponItemBeginSlot:
				return this.CachedWeaponSlot0Entity;
			case EquipmentIndex.Weapon1:
				return this.CachedWeaponSlot1Entity;
			case EquipmentIndex.Weapon2:
				return this.CachedWeaponSlot2Entity;
			case EquipmentIndex.Weapon3:
				return this.CachedWeaponSlot3Entity;
			case EquipmentIndex.ExtraWeaponSlot:
				return this.CachedWeaponSlot4Entity;
			default:
				return null;
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x00090A80 File Offset: 0x0008EC80
		public AgentVisualsData CachedWeaponEntity(EquipmentIndex slotIndex, GameEntity cachedWeaponEntity)
		{
			switch (slotIndex)
			{
			case EquipmentIndex.WeaponItemBeginSlot:
				this.CachedWeaponSlot0Entity = cachedWeaponEntity;
				break;
			case EquipmentIndex.Weapon1:
				this.CachedWeaponSlot1Entity = cachedWeaponEntity;
				break;
			case EquipmentIndex.Weapon2:
				this.CachedWeaponSlot2Entity = cachedWeaponEntity;
				break;
			case EquipmentIndex.Weapon3:
				this.CachedWeaponSlot3Entity = cachedWeaponEntity;
				break;
			case EquipmentIndex.ExtraWeaponSlot:
				this.CachedWeaponSlot4Entity = cachedWeaponEntity;
				break;
			}
			return this;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x00090AD5 File Offset: 0x0008ECD5
		public AgentVisualsData Entity(GameEntity entity)
		{
			this.EntityData = entity;
			return this;
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x00090ADF File Offset: 0x0008ECDF
		public AgentVisualsData UseTranslucency(bool useTranslucency)
		{
			this.UseTranslucencyData = useTranslucency;
			return this;
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x00090AE9 File Offset: 0x0008ECE9
		public AgentVisualsData UseTesselation(bool useTesselation)
		{
			this.UseTesselationData = useTesselation;
			return this;
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00090AF3 File Offset: 0x0008ECF3
		public AgentVisualsData ActionCode(ActionIndexCache actionCode)
		{
			this.ActionCodeData = actionCode;
			return this;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00090AFD File Offset: 0x0008ECFD
		public AgentVisualsData RightWieldedItemIndex(int rightWieldedItemIndex)
		{
			this.RightWieldedItemIndexData = rightWieldedItemIndex;
			return this;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00090B07 File Offset: 0x0008ED07
		public AgentVisualsData LeftWieldedItemIndex(int leftWieldedItemIndex)
		{
			this.LeftWieldedItemIndexData = leftWieldedItemIndex;
			return this;
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00090B11 File Offset: 0x0008ED11
		public AgentVisualsData Scale(float scale)
		{
			this.ScaleData = scale;
			return this;
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00090B1B File Offset: 0x0008ED1B
		public AgentVisualsData CharacterObjectStringId(string characterObjectStringId)
		{
			this.CharacterObjectStringIdData = characterObjectStringId;
			return this;
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00090B25 File Offset: 0x0008ED25
		public AgentVisualsData HasClippingPlane(bool hasClippingPlane)
		{
			this.HasClippingPlaneData = hasClippingPlane;
			return this;
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x00090B2F File Offset: 0x0008ED2F
		public AgentVisualsData MountCreationKey(string mountCreationKey)
		{
			this.MountCreationKeyData = mountCreationKey;
			return this;
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00090B39 File Offset: 0x0008ED39
		public AgentVisualsData AddColorRandomness(bool addColorRandomness)
		{
			this.AddColorRandomnessData = addColorRandomness;
			return this;
		}

		// Token: 0x04000E1C RID: 3612
		public MBAgentVisuals AgentVisuals;
	}
}
