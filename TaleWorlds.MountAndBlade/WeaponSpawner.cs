using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200037C RID: 892
	public class WeaponSpawner : ScriptComponentBehavior
	{
		// Token: 0x0600312C RID: 12588 RVA: 0x000CBB08 File Offset: 0x000C9D08
		public void SpawnWeapon()
		{
			base.OnPreInit();
			base.GameEntity.RemoveAllChildren();
			MissionWeapon missionWeapon = new MissionWeapon(MBObjectManager.Instance.GetObject<ItemObject>(base.GameEntity.Name), null, null);
			GameEntity gameEntity = Mission.Current.SpawnWeaponWithNewEntity(ref missionWeapon, Mission.WeaponSpawnFlags.WithPhysics, base.GameEntity.GetGlobalFrame());
			List<string> list = new List<string>();
			foreach (string text in base.GameEntity.Tags)
			{
				gameEntity.AddTag(text);
				list.Add(text);
			}
			for (int j = 0; j < list.Count; j++)
			{
				base.GameEntity.RemoveTag(list[j]);
			}
		}
	}
}
