using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003B1 RID: 945
	public class DebugObjectDestroyerMissionController : MissionLogic
	{
		// Token: 0x060032C0 RID: 12992 RVA: 0x000D2C78 File Offset: 0x000D0E78
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			Vec3 lastFinalRenderCameraPosition = base.Mission.Scene.LastFinalRenderCameraPosition;
			Vec3 vec = -base.Mission.Scene.LastFinalRenderCameraFrame.rotation.u;
			float f;
			GameEntity gameEntity;
			bool flag = Mission.Current.Scene.RayCastForClosestEntityOrTerrain(lastFinalRenderCameraPosition, lastFinalRenderCameraPosition + vec * 100f, out f, out gameEntity, 0.01f, BodyFlags.OnlyCollideWithRaycast);
			if (Input.DebugInput.IsShiftDown() && Agent.Main != null && gameEntity != null && !gameEntity.HasScriptOfType<DestructableComponent>())
			{
				foreach (MissionObject missionObject in from x in Mission.Current.ActiveMissionObjects
				where x is DestructableComponent
				select x)
				{
					DestructableComponent destructableComponent = (DestructableComponent)missionObject;
					if ((destructableComponent.GameEntity.GlobalPosition - Agent.Main.Position).Length < 5f)
					{
						gameEntity = destructableComponent.GameEntity;
					}
				}
			}
			GameEntity gameEntity2 = null;
			if (flag && (Input.DebugInput.IsKeyDown(InputKey.MiddleMouseButton) || Input.DebugInput.IsKeyReleased(InputKey.MiddleMouseButton)))
			{
				Vec3 v = lastFinalRenderCameraPosition + vec * f;
				if (gameEntity == null)
				{
					return;
				}
				bool flag2 = Input.DebugInput.IsKeyReleased(InputKey.MiddleMouseButton);
				int weaponKind = 0;
				if (flag2)
				{
					if (Input.DebugInput.IsAltDown())
					{
						weaponKind = (int)Game.Current.ObjectManager.GetObject<ItemObject>("boulder").Id.InternalValue;
					}
					else if (Input.DebugInput.IsControlDown())
					{
						weaponKind = (int)Game.Current.ObjectManager.GetObject<ItemObject>("pot").Id.InternalValue;
					}
					else
					{
						weaponKind = (int)Game.Current.ObjectManager.GetObject<ItemObject>("ballista_projectile").Id.InternalValue;
					}
				}
				GameEntity gameEntity3 = gameEntity;
				DestructableComponent destructableComponent2 = null;
				while (destructableComponent2 == null && gameEntity3 != null)
				{
					destructableComponent2 = gameEntity3.GetFirstScriptOfType<DestructableComponent>();
					gameEntity3 = gameEntity3.Parent;
				}
				if (destructableComponent2 != null && !destructableComponent2.IsDestroyed)
				{
					if (flag2)
					{
						if (Agent.Main != null)
						{
							DestructableComponent destructableComponent3 = destructableComponent2;
							Agent main = Agent.Main;
							int inflictedDamage = 400;
							Vec3 impactPosition = v - vec * 0.1f;
							Vec3 impactDirection = vec;
							MissionWeapon missionWeapon = new MissionWeapon(ItemObject.GetItemFromWeaponKind(weaponKind), null, null);
							destructableComponent3.TriggerOnHit(main, inflictedDamage, impactPosition, impactDirection, missionWeapon, null);
						}
					}
					else
					{
						gameEntity2 = destructableComponent2.GameEntity;
					}
				}
			}
			if (gameEntity2 != this._contouredEntity && this._contouredEntity != null)
			{
				this._contouredEntity.SetContourColor(null, true);
			}
			this._contouredEntity = gameEntity2;
			if (this._contouredEntity != null)
			{
				this._contouredEntity.SetContourColor(new uint?(4294967040U), true);
			}
		}

		// Token: 0x04001605 RID: 5637
		private GameEntity _contouredEntity;
	}
}
