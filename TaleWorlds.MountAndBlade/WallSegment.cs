using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects.Siege;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200034F RID: 847
	public class WallSegment : SynchedMissionObject, IPointDefendable, ICastleKeyPosition
	{
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x000BD6EC File Offset: 0x000BB8EC
		// (set) Token: 0x06002E4D RID: 11853 RVA: 0x000BD6F4 File Offset: 0x000BB8F4
		public TacticalPosition MiddlePosition { get; private set; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x000BD6FD File Offset: 0x000BB8FD
		// (set) Token: 0x06002E4F RID: 11855 RVA: 0x000BD705 File Offset: 0x000BB905
		public TacticalPosition WaitPosition { get; private set; }

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x000BD70E File Offset: 0x000BB90E
		// (set) Token: 0x06002E51 RID: 11857 RVA: 0x000BD716 File Offset: 0x000BB916
		public TacticalPosition AttackerWaitPosition { get; private set; }

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06002E52 RID: 11858 RVA: 0x000BD71F File Offset: 0x000BB91F
		// (set) Token: 0x06002E53 RID: 11859 RVA: 0x000BD727 File Offset: 0x000BB927
		public IPrimarySiegeWeapon AttackerSiegeWeapon { get; set; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x000BD730 File Offset: 0x000BB930
		// (set) Token: 0x06002E55 RID: 11861 RVA: 0x000BD738 File Offset: 0x000BB938
		public IEnumerable<DefencePoint> DefencePoints { get; protected set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x000BD741 File Offset: 0x000BB941
		// (set) Token: 0x06002E57 RID: 11863 RVA: 0x000BD749 File Offset: 0x000BB949
		public bool IsBreachedWall { get; private set; }

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000BD752 File Offset: 0x000BB952
		// (set) Token: 0x06002E59 RID: 11865 RVA: 0x000BD75A File Offset: 0x000BB95A
		public WorldFrame MiddleFrame { get; private set; }

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x000BD763 File Offset: 0x000BB963
		// (set) Token: 0x06002E5B RID: 11867 RVA: 0x000BD76B File Offset: 0x000BB96B
		public WorldFrame DefenseWaitFrame { get; private set; }

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06002E5C RID: 11868 RVA: 0x000BD774 File Offset: 0x000BB974
		// (set) Token: 0x06002E5D RID: 11869 RVA: 0x000BD77C File Offset: 0x000BB97C
		public WorldFrame AttackerWaitFrame { get; private set; } = WorldFrame.Invalid;

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x000BD785 File Offset: 0x000BB985
		// (set) Token: 0x06002E5F RID: 11871 RVA: 0x000BD78D File Offset: 0x000BB98D
		public FormationAI.BehaviorSide DefenseSide { get; private set; }

		// Token: 0x06002E60 RID: 11872 RVA: 0x000BD796 File Offset: 0x000BB996
		public Vec3 GetPosition()
		{
			return base.GameEntity.GlobalPosition;
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000BD7A4 File Offset: 0x000BB9A4
		public WallSegment()
		{
			this.AttackerSiegeWeapon = null;
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x000BD808 File Offset: 0x000BBA08
		protected internal override void OnInit()
		{
			base.OnInit();
			string sideTag = this.SideTag;
			if (!(sideTag == "left"))
			{
				if (!(sideTag == "middle"))
				{
					if (!(sideTag == "right"))
					{
						this.DefenseSide = FormationAI.BehaviorSide.BehaviorSideNotSet;
					}
					else
					{
						this.DefenseSide = FormationAI.BehaviorSide.Right;
					}
				}
				else
				{
					this.DefenseSide = FormationAI.BehaviorSide.Middle;
				}
			}
			else
			{
				this.DefenseSide = FormationAI.BehaviorSide.Left;
			}
			GameEntity gameEntity = base.GameEntity.GetChildren().FirstOrDefault((GameEntity ce) => ce.HasTag("solid_child"));
			List<GameEntity> list = new List<GameEntity>();
			List<GameEntity> list2 = new List<GameEntity>();
			if (gameEntity != null)
			{
				list = gameEntity.CollectChildrenEntitiesWithTag("middle_pos");
				list2 = gameEntity.CollectChildrenEntitiesWithTag("wait_pos");
			}
			else
			{
				list = base.GameEntity.CollectChildrenEntitiesWithTag("middle_pos");
				list2 = base.GameEntity.CollectChildrenEntitiesWithTag("wait_pos");
			}
			MatrixFrame globalFrame;
			if (list.Count > 0)
			{
				GameEntity gameEntity2 = list.FirstOrDefault<GameEntity>();
				this.MiddlePosition = gameEntity2.GetFirstScriptOfType<TacticalPosition>();
				globalFrame = gameEntity2.GetGlobalFrame();
			}
			else
			{
				globalFrame = base.GameEntity.GetGlobalFrame();
			}
			this.MiddleFrame = new WorldFrame(globalFrame.rotation, globalFrame.origin.ToWorldPosition());
			if (list2.Count > 0)
			{
				GameEntity gameEntity3 = list2.FirstOrDefault<GameEntity>();
				this.WaitPosition = gameEntity3.GetFirstScriptOfType<TacticalPosition>();
				globalFrame = gameEntity3.GetGlobalFrame();
				this.DefenseWaitFrame = new WorldFrame(globalFrame.rotation, globalFrame.origin.ToWorldPosition());
				return;
			}
			this.DefenseWaitFrame = this.MiddleFrame;
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000BD991 File Offset: 0x000BBB91
		protected internal override bool MovesEntity()
		{
			return false;
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000BD994 File Offset: 0x000BBB94
		public void OnChooseUsedWallSegment(bool isBroken)
		{
			GameEntity gameEntity = base.GameEntity.GetChildren().FirstOrDefault((GameEntity ce) => ce.HasTag("solid_child"));
			GameEntity gameEntity2 = base.GameEntity.GetChildren().FirstOrDefault((GameEntity ce) => ce.HasTag("broken_child"));
			Scene scene = base.GameEntity.Scene;
			if (isBroken)
			{
				gameEntity.GetFirstScriptOfType<WallSegment>().SetDisabledSynched();
				gameEntity2.GetFirstScriptOfType<WallSegment>().SetVisibleSynched(true, false);
				if (!GameNetwork.IsClientOrReplay)
				{
					if (this._properGroundOutsideNavmeshID > 0 && this._underDebrisOutsideNavmeshID > 0)
					{
						scene.SeparateFacesWithId(this._properGroundOutsideNavmeshID, this._underDebrisOutsideNavmeshID);
					}
					if (this._properGroundInsideNavmeshID > 0 && this._underDebrisInsideNavmeshID > 0)
					{
						scene.SeparateFacesWithId(this._properGroundInsideNavmeshID, this._underDebrisInsideNavmeshID);
					}
					if (this._underDebrisOutsideNavmeshID > 0)
					{
						scene.SetAbilityOfFacesWithId(this._underDebrisOutsideNavmeshID, false);
					}
					if (this._underDebrisInsideNavmeshID > 0)
					{
						scene.SetAbilityOfFacesWithId(this._underDebrisInsideNavmeshID, false);
					}
					if (this._underDebrisGenericNavmeshID > 0)
					{
						scene.SetAbilityOfFacesWithId(this._underDebrisGenericNavmeshID, false);
					}
					if (this._overDebrisOutsideNavmeshID > 0)
					{
						scene.SetAbilityOfFacesWithId(this._overDebrisOutsideNavmeshID, true);
						if (this._properGroundOutsideNavmeshID > 0)
						{
							scene.MergeFacesWithId(this._overDebrisOutsideNavmeshID, this._properGroundOutsideNavmeshID, 0);
						}
					}
					if (this._overDebrisInsideNavmeshID > 0)
					{
						scene.SetAbilityOfFacesWithId(this._overDebrisInsideNavmeshID, true);
						if (this._properGroundInsideNavmeshID > 0)
						{
							scene.MergeFacesWithId(this._overDebrisInsideNavmeshID, this._properGroundInsideNavmeshID, 1);
						}
					}
					if (this._overDebrisGenericNavmeshID > 0)
					{
						scene.SetAbilityOfFacesWithId(this._overDebrisGenericNavmeshID, true);
					}
					if (this._onSolidWallGenericNavmeshID > 0)
					{
						scene.SetAbilityOfFacesWithId(this._onSolidWallGenericNavmeshID, false);
					}
					foreach (StrategicArea strategicArea in from c in gameEntity.GetChildren()
					where c.HasScriptOfType<StrategicArea>()
					select c.GetFirstScriptOfType<StrategicArea>())
					{
						strategicArea.OnParentGameEntityVisibilityChanged(false);
					}
					foreach (StrategicArea strategicArea2 in from c in gameEntity2.GetChildren()
					where c.HasScriptOfType<StrategicArea>()
					select c.GetFirstScriptOfType<StrategicArea>())
					{
						strategicArea2.OnParentGameEntityVisibilityChanged(true);
					}
				}
				this.IsBreachedWall = true;
				List<GameEntity> list = gameEntity2.CollectChildrenEntitiesWithTag("middle_pos");
				if (list.Count > 0)
				{
					GameEntity gameEntity3 = list.FirstOrDefault<GameEntity>();
					this.MiddlePosition = gameEntity3.GetFirstScriptOfType<TacticalPosition>();
					MatrixFrame globalFrame = gameEntity3.GetGlobalFrame();
					this.MiddleFrame = new WorldFrame(globalFrame.rotation, globalFrame.origin.ToWorldPosition());
				}
				else
				{
					MBDebug.ShowWarning("Broken child of wall does not have middle position");
					MatrixFrame globalFrame2 = gameEntity2.GetGlobalFrame();
					this.MiddleFrame = new WorldFrame(globalFrame2.rotation, new WorldPosition(scene, UIntPtr.Zero, globalFrame2.origin, false));
				}
				List<GameEntity> list2 = gameEntity2.CollectChildrenEntitiesWithTag("wait_pos");
				if (list2.Count > 0)
				{
					GameEntity gameEntity4 = list2.FirstOrDefault<GameEntity>();
					this.WaitPosition = gameEntity4.GetFirstScriptOfType<TacticalPosition>();
					MatrixFrame globalFrame3 = gameEntity4.GetGlobalFrame();
					this.DefenseWaitFrame = new WorldFrame(globalFrame3.rotation, globalFrame3.origin.ToWorldPosition());
				}
				else
				{
					this.DefenseWaitFrame = this.MiddleFrame;
				}
				WallSegment firstScriptOfType = gameEntity.GetFirstScriptOfType<WallSegment>();
				if (firstScriptOfType != null)
				{
					firstScriptOfType.SetDisabledAndMakeInvisible(true);
				}
				GameEntity gameEntity5 = gameEntity2.CollectChildrenEntitiesWithTag("attacker_wait_pos").FirstOrDefault<GameEntity>();
				if (gameEntity5 != null)
				{
					MatrixFrame globalFrame4 = gameEntity5.GetGlobalFrame();
					this.AttackerWaitFrame = new WorldFrame(globalFrame4.rotation, globalFrame4.origin.ToWorldPosition());
					this.AttackerWaitPosition = gameEntity5.GetFirstScriptOfType<TacticalPosition>();
					return;
				}
			}
			else if (!GameNetwork.IsClientOrReplay)
			{
				gameEntity.GetFirstScriptOfType<WallSegment>().SetVisibleSynched(true, false);
				gameEntity2.GetFirstScriptOfType<WallSegment>().SetDisabledSynched();
				if (this._overDebrisOutsideNavmeshID > 0)
				{
					scene.SetAbilityOfFacesWithId(this._overDebrisOutsideNavmeshID, false);
				}
				if (this._overDebrisInsideNavmeshID > 0)
				{
					scene.SetAbilityOfFacesWithId(this._overDebrisInsideNavmeshID, false);
				}
				if (this._overDebrisGenericNavmeshID > 0)
				{
					scene.SetAbilityOfFacesWithId(this._overDebrisGenericNavmeshID, false);
				}
				foreach (StrategicArea strategicArea3 in from c in gameEntity.GetChildren()
				where c.HasScriptOfType<StrategicArea>()
				select c.GetFirstScriptOfType<StrategicArea>())
				{
					strategicArea3.OnParentGameEntityVisibilityChanged(true);
				}
				foreach (StrategicArea strategicArea4 in from c in gameEntity2.GetChildren()
				where c.HasScriptOfType<StrategicArea>()
				select c.GetFirstScriptOfType<StrategicArea>())
				{
					strategicArea4.OnParentGameEntityVisibilityChanged(false);
				}
			}
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000BDF2C File Offset: 0x000BC12C
		protected internal override void OnEditorValidate()
		{
			base.OnEditorValidate();
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000BDF34 File Offset: 0x000BC134
		protected internal override bool OnCheckForProblems()
		{
			bool result = base.OnCheckForProblems();
			if (!base.Scene.IsMultiplayerScene() && this.SideTag == "left")
			{
				List<GameEntity> list = new List<GameEntity>();
				base.Scene.GetEntities(ref list);
				int num = 0;
				foreach (GameEntity gameEntity in list)
				{
					if (base.GameEntity.GetUpgradeLevelOfEntity() == gameEntity.GetUpgradeLevelOfEntity() && gameEntity.GetFirstScriptOfType<SiegeLadderSpawner>() != null)
					{
						num++;
					}
				}
				if (num != 4)
				{
					MBEditor.AddEntityWarning(base.GameEntity, "The siege ladder count in the scene is not 4, for upgrade level " + base.GameEntity.GetUpgradeLevelOfEntity().ToString() + ". Current siege ladder count: " + num.ToString());
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04001375 RID: 4981
		private const string WaitPositionTag = "wait_pos";

		// Token: 0x04001376 RID: 4982
		private const string MiddlePositionTag = "middle_pos";

		// Token: 0x04001377 RID: 4983
		private const string AttackerWaitPositionTag = "attacker_wait_pos";

		// Token: 0x04001378 RID: 4984
		private const string SolidChildTag = "solid_child";

		// Token: 0x04001379 RID: 4985
		private const string BrokenChildTag = "broken_child";

		// Token: 0x0400137A RID: 4986
		[EditableScriptComponentVariable(true)]
		private int _properGroundOutsideNavmeshID = -1;

		// Token: 0x0400137B RID: 4987
		[EditableScriptComponentVariable(true)]
		private int _properGroundInsideNavmeshID = -1;

		// Token: 0x0400137C RID: 4988
		[EditableScriptComponentVariable(true)]
		private int _underDebrisOutsideNavmeshID = -1;

		// Token: 0x0400137D RID: 4989
		[EditableScriptComponentVariable(true)]
		private int _underDebrisInsideNavmeshID = -1;

		// Token: 0x0400137E RID: 4990
		[EditableScriptComponentVariable(true)]
		private int _overDebrisOutsideNavmeshID = -1;

		// Token: 0x0400137F RID: 4991
		[EditableScriptComponentVariable(true)]
		private int _overDebrisInsideNavmeshID = -1;

		// Token: 0x04001380 RID: 4992
		[EditableScriptComponentVariable(true)]
		private int _underDebrisGenericNavmeshID = -1;

		// Token: 0x04001381 RID: 4993
		[EditableScriptComponentVariable(true)]
		private int _overDebrisGenericNavmeshID = -1;

		// Token: 0x04001382 RID: 4994
		[EditableScriptComponentVariable(true)]
		private int _onSolidWallGenericNavmeshID = -1;

		// Token: 0x04001388 RID: 5000
		public string SideTag;
	}
}
