using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000252 RID: 594
	public abstract class MissionObject : ScriptComponentBehavior
	{
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001FD6 RID: 8150 RVA: 0x000710C2 File Offset: 0x0006F2C2
		private Mission Mission
		{
			get
			{
				return Mission.Current;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x000710C9 File Offset: 0x0006F2C9
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x000710D1 File Offset: 0x0006F2D1
		public MissionObjectId Id { get; set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x000710DA File Offset: 0x0006F2DA
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x000710E2 File Offset: 0x0006F2E2
		public bool IsDisabled { get; private set; }

		// Token: 0x06001FDB RID: 8155 RVA: 0x000710EC File Offset: 0x0006F2EC
		public MissionObject()
		{
			MissionObjectId id = new MissionObjectId(-1, false);
			this.Id = id;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0007111C File Offset: 0x0006F31C
		public virtual void SetAbilityOfFaces(bool enabled)
		{
			if (this.DynamicNavmeshIdStart > 0)
			{
				base.GameEntity.Scene.SetAbilityOfFacesWithId(this.DynamicNavmeshIdStart + 1, enabled);
				base.GameEntity.Scene.SetAbilityOfFacesWithId(this.DynamicNavmeshIdStart + 2, enabled);
				base.GameEntity.Scene.SetAbilityOfFacesWithId(this.DynamicNavmeshIdStart + 3, enabled);
				base.GameEntity.Scene.SetAbilityOfFacesWithId(this.DynamicNavmeshIdStart + 4, enabled);
				base.GameEntity.Scene.SetAbilityOfFacesWithId(this.DynamicNavmeshIdStart + 5, enabled);
				base.GameEntity.Scene.SetAbilityOfFacesWithId(this.DynamicNavmeshIdStart + 6, enabled);
				base.GameEntity.Scene.SetAbilityOfFacesWithId(this.DynamicNavmeshIdStart + 7, enabled);
			}
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x000711E4 File Offset: 0x0006F3E4
		protected internal override void OnInit()
		{
			base.OnInit();
			if (!GameNetwork.IsClientOrReplay)
			{
				this.AttachDynamicNavmeshToEntity();
				this.SetAbilityOfFaces(base.GameEntity != null && base.GameEntity.IsVisibleIncludeParents());
			}
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0007121C File Offset: 0x0006F41C
		protected virtual void AttachDynamicNavmeshToEntity()
		{
			if (this.NavMeshPrefabName.Length > 0)
			{
				this.DynamicNavmeshIdStart = Mission.Current.GetNextDynamicNavMeshIdStart();
				base.GameEntity.Scene.ImportNavigationMeshPrefab(this.NavMeshPrefabName, this.DynamicNavmeshIdStart);
				this.GetEntityToAttachNavMeshFaces().AttachNavigationMeshFaces(this.DynamicNavmeshIdStart + 1, false, false, false);
				this.GetEntityToAttachNavMeshFaces().AttachNavigationMeshFaces(this.DynamicNavmeshIdStart + 2, true, false, false);
				this.GetEntityToAttachNavMeshFaces().AttachNavigationMeshFaces(this.DynamicNavmeshIdStart + 3, true, false, false);
				this.GetEntityToAttachNavMeshFaces().AttachNavigationMeshFaces(this.DynamicNavmeshIdStart + 4, false, true, false);
				this.SetAbilityOfFaces(base.GameEntity != null && base.GameEntity.GetPhysicsState());
			}
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x000712E0 File Offset: 0x0006F4E0
		protected virtual GameEntity GetEntityToAttachNavMeshFaces()
		{
			return base.GameEntity;
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x000712E8 File Offset: 0x0006F4E8
		protected internal override bool OnCheckForProblems()
		{
			base.OnCheckForProblems();
			bool result = false;
			List<GameEntity> list = new List<GameEntity>();
			list.Add(base.GameEntity);
			base.GameEntity.GetChildrenRecursive(ref list);
			bool flag = false;
			foreach (GameEntity gameEntity in list)
			{
				flag = (flag || (gameEntity.HasPhysicsDefinitionWithoutFlags(1) && !gameEntity.PhysicsDescBodyFlag.HasAnyFlag(BodyFlags.CommonCollisionExcludeFlagsForMissile)));
			}
			Vec3 scaleVector = base.GameEntity.GetGlobalFrame().rotation.GetScaleVector();
			bool flag2 = MathF.Abs(scaleVector.x - scaleVector.y) >= 0.01f || MathF.Abs(scaleVector.x - scaleVector.z) >= 0.01f;
			if (flag && flag2)
			{
				MBEditor.AddEntityWarning(base.GameEntity, "Mission object has non-uniform scale and physics object. This is not supported because any attached focusable item to this mesh will not work within this configuration.");
				result = true;
			}
			return result;
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x000713F0 File Offset: 0x0006F5F0
		protected internal override void OnPreInit()
		{
			base.OnPreInit();
			if (this.Mission != null)
			{
				int id = -1;
				bool createdAtRuntime;
				if (this.Mission.IsLoadingFinished)
				{
					createdAtRuntime = true;
					if (!GameNetwork.IsClientOrReplay)
					{
						id = this.Mission.GetFreeRuntimeMissionObjectId();
					}
				}
				else
				{
					createdAtRuntime = false;
					id = this.Mission.GetFreeSceneMissionObjectId();
				}
				this.Id = new MissionObjectId(id, createdAtRuntime);
				this.Mission.AddActiveMissionObject(this);
			}
			base.GameEntity.SetAsReplayEntity();
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x00071464 File Offset: 0x0006F664
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x00071485 File Offset: 0x0006F685
		protected internal virtual void OnMissionReset()
		{
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x00071487 File Offset: 0x0006F687
		public virtual void AfterMissionStart()
		{
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x00071489 File Offset: 0x0006F689
		public virtual void OnMissionEnded()
		{
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0007148B File Offset: 0x0006F68B
		protected internal virtual bool OnHit(Agent attackerAgent, int damage, Vec3 impactPosition, Vec3 impactDirection, in MissionWeapon weapon, ScriptComponentBehavior attackerScriptComponentBehavior, out bool reportDamage)
		{
			reportDamage = false;
			return false;
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x00071494 File Offset: 0x0006F694
		public void SetDisabled(bool isParentObject = false)
		{
			if (!GameNetwork.IsClientOrReplay)
			{
				this.SetAbilityOfFaces(false);
			}
			if (isParentObject && base.GameEntity != null)
			{
				List<GameEntity> source = new List<GameEntity>();
				base.GameEntity.GetChildrenRecursive(ref source);
				foreach (MissionObject missionObject in from sc in source.SelectMany((GameEntity ac) => ac.GetScriptComponents())
				where sc is MissionObject
				select sc as MissionObject)
				{
					missionObject.SetDisabled(false);
				}
			}
			Mission.Current.DeactivateMissionObject(this);
			this.IsDisabled = true;
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x00071590 File Offset: 0x0006F790
		public void SetDisabledAndMakeInvisible(bool isParentObject = false)
		{
			if (isParentObject && base.GameEntity != null)
			{
				List<GameEntity> source = new List<GameEntity>();
				base.GameEntity.GetChildrenRecursive(ref source);
				foreach (MissionObject missionObject in from sc in source.SelectMany((GameEntity ac) => ac.GetScriptComponents())
				where sc is MissionObject
				select sc as MissionObject)
				{
					missionObject.SetDisabledAndMakeInvisible(false);
				}
			}
			Mission.Current.DeactivateMissionObject(this);
			this.IsDisabled = true;
			if (base.GameEntity != null)
			{
				base.GameEntity.SetVisibilityExcludeParents(false);
				base.SetScriptComponentToTick(this.GetTickRequirement());
			}
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x000716A4 File Offset: 0x0006F8A4
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			if (!GameNetwork.IsClientOrReplay)
			{
				this.SetAbilityOfFaces(false);
			}
			if (this.Mission != null)
			{
				this.Mission.OnMissionObjectRemoved(this, removeReason);
			}
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x000716D1 File Offset: 0x0006F8D1
		public virtual void OnEndMission()
		{
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x000716D3 File Offset: 0x0006F8D3
		public bool CreatedAtRuntime
		{
			get
			{
				return this.Id.CreatedAtRuntime;
			}
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x000716E0 File Offset: 0x0006F8E0
		protected internal override bool MovesEntity()
		{
			return true;
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x000716E3 File Offset: 0x0006F8E3
		public virtual void AddStuckMissile(GameEntity missileEntity)
		{
			base.GameEntity.AddChild(missileEntity, false);
		}

		// Token: 0x04000BE3 RID: 3043
		public const int MaxNavMeshPerDynamicObject = 10;

		// Token: 0x04000BE6 RID: 3046
		[EditableScriptComponentVariable(true)]
		protected string NavMeshPrefabName = "";

		// Token: 0x04000BE7 RID: 3047
		protected int DynamicNavmeshIdStart;

		// Token: 0x0200050A RID: 1290
		protected enum DynamicNavmeshLocalIds
		{
			// Token: 0x04001BDC RID: 7132
			Inside = 1,
			// Token: 0x04001BDD RID: 7133
			Enter,
			// Token: 0x04001BDE RID: 7134
			Exit,
			// Token: 0x04001BDF RID: 7135
			Blocker,
			// Token: 0x04001BE0 RID: 7136
			Extra1,
			// Token: 0x04001BE1 RID: 7137
			Extra2,
			// Token: 0x04001BE2 RID: 7138
			Extra3,
			// Token: 0x04001BE3 RID: 7139
			Reserved1,
			// Token: 0x04001BE4 RID: 7140
			Reserved2,
			// Token: 0x04001BE5 RID: 7141
			Count
		}
	}
}
