using System;
using System.IO;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000052 RID: 82
	public class ItemCatalogController : MissionLogic
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00014B3B File Offset: 0x00012D3B
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00014B43 File Offset: 0x00012D43
		public MBReadOnlyList<ItemObject> AllItems { get; private set; }

		// Token: 0x06000343 RID: 835 RVA: 0x00014B4C File Offset: 0x00012D4C
		public ItemCatalogController()
		{
			this._campaign = Campaign.Current;
			this._game = Game.Current;
			this.timer = new Timer(base.Mission.CurrentTime, 1f, true);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00014B98 File Offset: 0x00012D98
		public override void AfterStart()
		{
			base.AfterStart();
			base.Mission.SetMissionMode(MissionMode.Battle, true);
			this.AllItems = Game.Current.ObjectManager.GetObjectTypeList<ItemObject>();
			if (!this._campaign.IsInitializedSinglePlayerReferences)
			{
				this._campaign.InitializeSinglePlayerReferences();
			}
			CharacterObject playerCharacter = CharacterObject.PlayerCharacter;
			MobileParty.MainParty.MemberRoster.AddToCounts(playerCharacter, 1, false, 0, 0, true, -1);
			if (!base.Mission.Teams.IsEmpty<Team>())
			{
				throw new MBIllegalValueException("Number of teams is not 0.");
			}
			base.Mission.Teams.Add(BattleSideEnum.Defender, 4284776512U, uint.MaxValue, null, true, false, true);
			base.Mission.Teams.Add(BattleSideEnum.Attacker, 4281877080U, uint.MaxValue, null, true, false, true);
			base.Mission.PlayerTeam = base.Mission.AttackerTeam;
			EquipmentElement value = playerCharacter.Equipment[0];
			EquipmentElement value2 = playerCharacter.Equipment[1];
			EquipmentElement value3 = playerCharacter.Equipment[2];
			EquipmentElement value4 = playerCharacter.Equipment[3];
			EquipmentElement value5 = playerCharacter.Equipment[4];
			playerCharacter.Equipment[0] = value;
			playerCharacter.Equipment[1] = value2;
			playerCharacter.Equipment[2] = value3;
			playerCharacter.Equipment[3] = value4;
			playerCharacter.Equipment[4] = value5;
			ItemObject item = this.AllItems[0];
			Equipment equipment = new Equipment();
			equipment.AddEquipmentToSlotWithoutAgent(this.GetEquipmentIndexOfItem(item), new EquipmentElement(this.AllItems[0], null, null, false));
			AgentBuildData agentBuildData = new AgentBuildData(playerCharacter);
			agentBuildData.Equipment(equipment);
			Mission mission = base.Mission;
			AgentBuildData agentBuildData2 = agentBuildData.Team(base.Mission.AttackerTeam);
			Vec3 vec = new Vec3(15f, 12f, 1f, -1f);
			this._playerAgent = mission.SpawnAgent(agentBuildData2.InitialPosition(vec).InitialDirection(Vec2.Forward).Controller(Agent.ControllerType.Player), false);
			this._playerAgent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
			this._playerAgent.Health = 10000f;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00014DB0 File Offset: 0x00012FB0
		private EquipmentIndex GetEquipmentIndexOfItem(ItemObject item)
		{
			if (item.ItemFlags.HasAnyFlag(ItemFlags.DropOnWeaponChange | ItemFlags.DropOnAnyAction))
			{
				return EquipmentIndex.ExtraWeaponSlot;
			}
			switch (item.ItemType)
			{
			case ItemObject.ItemTypeEnum.Horse:
				return EquipmentIndex.ArmorItemEndSlot;
			case ItemObject.ItemTypeEnum.OneHandedWeapon:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.TwoHandedWeapon:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Polearm:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Arrows:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Bolts:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Shield:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Bow:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Crossbow:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Thrown:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.HeadArmor:
				return EquipmentIndex.NumAllWeaponSlots;
			case ItemObject.ItemTypeEnum.BodyArmor:
				return EquipmentIndex.Body;
			case ItemObject.ItemTypeEnum.LegArmor:
				return EquipmentIndex.Leg;
			case ItemObject.ItemTypeEnum.HandArmor:
				return EquipmentIndex.Gloves;
			case ItemObject.ItemTypeEnum.Pistol:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Musket:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Bullets:
				return EquipmentIndex.WeaponItemBeginSlot;
			case ItemObject.ItemTypeEnum.Animal:
				return EquipmentIndex.ArmorItemEndSlot;
			case ItemObject.ItemTypeEnum.Cape:
				return EquipmentIndex.Cape;
			case ItemObject.ItemTypeEnum.HorseHarness:
				return EquipmentIndex.HorseHarness;
			}
			Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\MissionLogics\\ItemCatalogController.cs", "GetEquipmentIndexOfItem", 147);
			return EquipmentIndex.None;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00014E84 File Offset: 0x00013084
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this.timer.Check(base.Mission.CurrentTime))
			{
				if (!Directory.Exists("ItemCatalog"))
				{
					Directory.CreateDirectory("ItemCatalog");
				}
				ItemCatalogController.BeforeCatalogTickDelegate beforeCatalogTick = this.BeforeCatalogTick;
				if (beforeCatalogTick != null)
				{
					beforeCatalogTick(this.curItemIndex);
				}
				this.timer.Reset(base.Mission.CurrentTime);
				MatrixFrame matrixFrame = default(MatrixFrame);
				matrixFrame.origin = new Vec3(10000f, 10000f, 10000f, -1f);
				this._playerAgent.AgentVisuals.SetFrame(ref matrixFrame);
				this._playerAgent.TeleportToPosition(matrixFrame.origin);
				Blow b = new Blow(this._playerAgent.Index);
				b.DamageType = DamageTypes.Blunt;
				b.BaseMagnitude = 1E+09f;
				b.GlobalPosition = this._playerAgent.Position;
				this._playerAgent.Die(b, Agent.KillInfo.Backstabbed);
				this._playerAgent = null;
				for (int i = base.Mission.Agents.Count - 1; i >= 0; i--)
				{
					Agent agent = base.Mission.Agents[i];
					Blow b2 = new Blow(agent.Index)
					{
						DamageType = DamageTypes.Blunt,
						BaseMagnitude = 1E+09f,
						GlobalPosition = agent.Position
					};
					agent.TeleportToPosition(matrixFrame.origin);
					agent.Die(b2, Agent.KillInfo.Backstabbed);
				}
				ItemObject item = this.AllItems[this.curItemIndex];
				Equipment equipment = new Equipment();
				equipment.AddEquipmentToSlotWithoutAgent(this.GetEquipmentIndexOfItem(item), new EquipmentElement(item, null, null, false));
				AgentBuildData agentBuildData = new AgentBuildData(this._game.PlayerTroop);
				agentBuildData.Equipment(equipment);
				Mission mission = base.Mission;
				AgentBuildData agentBuildData2 = agentBuildData.Team(base.Mission.AttackerTeam);
				Vec3 vec = new Vec3(15f, 12f, 1f, -1f);
				this._playerAgent = mission.SpawnAgent(agentBuildData2.InitialPosition(vec).InitialDirection(Vec2.Forward).Controller(Agent.ControllerType.Player), false);
				this._playerAgent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
				this._playerAgent.Health = 10000f;
				Action afterCatalogTick = this.AfterCatalogTick;
				if (afterCatalogTick != null)
				{
					afterCatalogTick();
				}
				this.curItemIndex++;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000347 RID: 839 RVA: 0x000150EC File Offset: 0x000132EC
		// (remove) Token: 0x06000348 RID: 840 RVA: 0x00015124 File Offset: 0x00013324
		public event ItemCatalogController.BeforeCatalogTickDelegate BeforeCatalogTick;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000349 RID: 841 RVA: 0x0001515C File Offset: 0x0001335C
		// (remove) Token: 0x0600034A RID: 842 RVA: 0x00015194 File Offset: 0x00013394
		public event Action AfterCatalogTick;

		// Token: 0x04000190 RID: 400
		private Campaign _campaign;

		// Token: 0x04000191 RID: 401
		private Game _game;

		// Token: 0x04000192 RID: 402
		private Agent _playerAgent;

		// Token: 0x04000194 RID: 404
		private int curItemIndex = 1;

		// Token: 0x04000195 RID: 405
		private Timer timer;

		// Token: 0x02000132 RID: 306
		// (Invoke) Token: 0x06000BD9 RID: 3033
		public delegate void BeforeCatalogTickDelegate(int currentItemIndex);
	}
}
