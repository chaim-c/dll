using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003AF RID: 943
	public class CaravanBattleMissionHandler : MissionLogic
	{
		// Token: 0x060032BB RID: 12987 RVA: 0x000D25C8 File Offset: 0x000D07C8
		public CaravanBattleMissionHandler(int unitCount, bool isCamelCulture, bool isCaravan)
		{
			this._unitCount = unitCount;
			this._isCamelCulture = isCamelCulture;
			this._isCaravan = isCaravan;
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000D2668 File Offset: 0x000D0868
		public override void AfterStart()
		{
			base.AfterStart();
			float battleSizeOffset = Mission.GetBattleSizeOffset((int)((float)this._unitCount * 1.5f), base.Mission.GetInitialSpawnPath());
			WorldFrame battleSideInitialSpawnPathFrame = base.Mission.GetBattleSideInitialSpawnPathFrame(BattleSideEnum.Defender, battleSizeOffset);
			this._entity = GameEntity.Instantiate(Mission.Current.Scene, this._isCaravan ? "caravan_scattered_goods_prop" : "villager_scattered_goods_prop", new MatrixFrame(battleSideInitialSpawnPathFrame.Rotation, battleSideInitialSpawnPathFrame.Origin.GetGroundVec3()));
			this._entity.SetMobility(GameEntity.Mobility.dynamic);
			foreach (GameEntity gameEntity in this._entity.GetChildren())
			{
				float z;
				Vec3 u;
				Mission.Current.Scene.GetTerrainHeightAndNormal(gameEntity.GlobalPosition.AsVec2, out z, out u);
				MatrixFrame globalFrame = gameEntity.GetGlobalFrame();
				globalFrame.origin.z = z;
				globalFrame.rotation.u = u;
				globalFrame.rotation.Orthonormalize();
				gameEntity.SetGlobalFrame(globalFrame);
			}
			IEnumerable<GameEntity> enumerable = from c in this._entity.GetChildren()
			where c.HasTag("caravan_animal_spawn")
			select c;
			int num = (int)((float)enumerable.Count<GameEntity>() * 0.4f);
			foreach (GameEntity gameEntity2 in enumerable)
			{
				MatrixFrame globalFrame2 = gameEntity2.GetGlobalFrame();
				string objectName;
				if (this._isCamelCulture)
				{
					if (num > 0)
					{
						int num2 = MBRandom.RandomInt(this._camelMountableHarnesses.Length);
						objectName = this._camelMountableHarnesses[num2];
					}
					else
					{
						int num3 = MBRandom.RandomInt(this._camelLoadHarnesses.Length);
						objectName = this._camelLoadHarnesses[num3];
					}
				}
				else if (num > 0)
				{
					int num4 = MBRandom.RandomInt(this._muleMountableHarnesses.Length);
					objectName = this._muleMountableHarnesses[num4];
				}
				else
				{
					int num5 = MBRandom.RandomInt(this._muleLoadHarnesses.Length);
					objectName = this._muleLoadHarnesses[num5];
				}
				ItemRosterElement itemRosterElement = new ItemRosterElement(Game.Current.ObjectManager.GetObject<ItemObject>(objectName), 0, null);
				ItemRosterElement itemRosterElement2 = this._isCamelCulture ? ((num-- > 0) ? new ItemRosterElement(Game.Current.ObjectManager.GetObject<ItemObject>("pack_camel"), 0, null) : new ItemRosterElement(Game.Current.ObjectManager.GetObject<ItemObject>("pack_camel_unmountable"), 0, null)) : ((num-- > 0) ? new ItemRosterElement(Game.Current.ObjectManager.GetObject<ItemObject>("mule"), 0, null) : new ItemRosterElement(Game.Current.ObjectManager.GetObject<ItemObject>("mule_unmountable"), 0, null));
				Mission mission = Mission.Current;
				ItemRosterElement rosterElement = itemRosterElement2;
				ItemRosterElement harnessRosterElement = itemRosterElement;
				Vec2 vec = globalFrame2.rotation.f.AsVec2;
				vec = vec.Normalized();
				Agent agent = mission.SpawnMonster(rosterElement, harnessRosterElement, globalFrame2.origin, vec, -1);
				agent.SetAgentFlags(agent.GetAgentFlags() & ~AgentFlag.CanWander);
			}
			TacticalPosition firstScriptInFamilyDescending = this._entity.GetFirstScriptInFamilyDescending<TacticalPosition>();
			if (firstScriptInFamilyDescending != null)
			{
				foreach (Team team in Mission.Current.Teams)
				{
					team.TeamAI.TacticalPositions.Add(firstScriptInFamilyDescending);
				}
			}
		}

		// Token: 0x040015FB RID: 5627
		private GameEntity _entity;

		// Token: 0x040015FC RID: 5628
		private int _unitCount;

		// Token: 0x040015FD RID: 5629
		private bool _isCamelCulture;

		// Token: 0x040015FE RID: 5630
		private bool _isCaravan;

		// Token: 0x040015FF RID: 5631
		private readonly string[] _camelLoadHarnesses = new string[]
		{
			"camel_saddle_a",
			"camel_saddle_b"
		};

		// Token: 0x04001600 RID: 5632
		private readonly string[] _camelMountableHarnesses = new string[]
		{
			"camel_saddle"
		};

		// Token: 0x04001601 RID: 5633
		private readonly string[] _muleLoadHarnesses = new string[]
		{
			"mule_load_a",
			"mule_load_b",
			"mule_load_c"
		};

		// Token: 0x04001602 RID: 5634
		private readonly string[] _muleMountableHarnesses = new string[]
		{
			"aseran_village_harness",
			"steppe_fur_harness",
			"steppe_harness"
		};

		// Token: 0x04001603 RID: 5635
		private const string CaravanPrefabName = "caravan_scattered_goods_prop";

		// Token: 0x04001604 RID: 5636
		private const string VillagerGoodsPrefabName = "villager_scattered_goods_prop";
	}
}
