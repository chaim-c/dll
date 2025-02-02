using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000054 RID: 84
	public class LocationItemSpawnHandler : MissionLogic
	{
		// Token: 0x0600034E RID: 846 RVA: 0x00015298 File Offset: 0x00013498
		public override void AfterStart()
		{
			if (CampaignMission.Current.Location != null && CampaignMission.Current.Location.SpecialItems.Count != 0)
			{
				this.SpawnSpecialItems();
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000152C4 File Offset: 0x000134C4
		private void SpawnSpecialItems()
		{
			this._spawnedEntities = new Dictionary<ItemObject, GameEntity>();
			List<GameEntity> list = base.Mission.Scene.FindEntitiesWithTag("sp_special_item").ToList<GameEntity>();
			foreach (ItemObject itemObject in CampaignMission.Current.Location.SpecialItems)
			{
				if (list.Count != 0)
				{
					MatrixFrame globalFrame = list[0].GetGlobalFrame();
					MissionWeapon missionWeapon = new MissionWeapon(itemObject, null, null);
					GameEntity value = base.Mission.SpawnWeaponWithNewEntity(ref missionWeapon, Mission.WeaponSpawnFlags.WithStaticPhysics, globalFrame);
					this._spawnedEntities.Add(itemObject, value);
					list.RemoveAt(0);
				}
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00015388 File Offset: 0x00013588
		public override void OnEntityRemoved(GameEntity entity)
		{
			if (this._spawnedEntities != null)
			{
				foreach (KeyValuePair<ItemObject, GameEntity> keyValuePair in this._spawnedEntities)
				{
					if (keyValuePair.Value == entity)
					{
						CampaignMission.Current.Location.SpecialItems.Remove(keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x0400019A RID: 410
		private Dictionary<ItemObject, GameEntity> _spawnedEntities;
	}
}
