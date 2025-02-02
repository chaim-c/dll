using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200017A RID: 378
	public class ArcherPosition
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001354 RID: 4948 RVA: 0x00049328 File Offset: 0x00047528
		public GameEntity Entity { get; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x00049330 File Offset: 0x00047530
		public TacticalPosition TacticalArcherPosition { get; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001356 RID: 4950 RVA: 0x00049338 File Offset: 0x00047538
		// (set) Token: 0x06001357 RID: 4951 RVA: 0x00049340 File Offset: 0x00047540
		public int ConnectedSides
		{
			get
			{
				return this._connectedSides;
			}
			private set
			{
				this._connectedSides = value;
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00049349 File Offset: 0x00047549
		public Formation GetLastAssignedFormation(int teamIndex)
		{
			if (teamIndex >= 0)
			{
				return this._lastAssignedFormations[teamIndex];
			}
			return null;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0004935C File Offset: 0x0004755C
		public ArcherPosition(GameEntity _entity, SiegeQuerySystem siegeQuerySystem, BattleSideEnum battleSide)
		{
			this.Entity = _entity;
			this.TacticalArcherPosition = this.Entity.GetFirstScriptOfType<TacticalPosition>();
			this._siegeQuerySystem = siegeQuerySystem;
			this.DetermineArcherPositionSide(battleSide);
			this._lastAssignedFormations = new Formation[Mission.Current.Teams.Count];
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x000493AF File Offset: 0x000475AF
		private static int ConvertToBinaryPow(int pow)
		{
			return 1 << pow;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000493B7 File Offset: 0x000475B7
		public bool IsArcherPositionRelatedToSide(FormationAI.BehaviorSide side)
		{
			return (ArcherPosition.ConvertToBinaryPow((int)side) & this.ConnectedSides) != 0;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000493C9 File Offset: 0x000475C9
		public FormationAI.BehaviorSide GetArcherPositionClosestSide()
		{
			return this._closestSide;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000493D1 File Offset: 0x000475D1
		public void OnDeploymentFinished(SiegeQuerySystem siegeQuerySystem, BattleSideEnum battleSide)
		{
			this._siegeQuerySystem = siegeQuerySystem;
			this.DetermineArcherPositionSide(battleSide);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000493E4 File Offset: 0x000475E4
		private void DetermineArcherPositionSide(BattleSideEnum battleSide)
		{
			this.ConnectedSides = 0;
			if (this.TacticalArcherPosition != null)
			{
				int tacticalPositionSide = (int)this.TacticalArcherPosition.TacticalPositionSide;
				if (tacticalPositionSide < 3)
				{
					this._closestSide = this.TacticalArcherPosition.TacticalPositionSide;
					this.ConnectedSides = ArcherPosition.ConvertToBinaryPow(tacticalPositionSide);
				}
			}
			if (this.ConnectedSides == 0)
			{
				if (battleSide == BattleSideEnum.Defender)
				{
					ArcherPosition.CalculateArcherPositionSideUsingDefenderLanes(this._siegeQuerySystem, this.Entity.GlobalPosition, out this._closestSide, out this._connectedSides);
					return;
				}
				ArcherPosition.CalculateArcherPositionSideUsingAttackerRegions(this._siegeQuerySystem, this.Entity.GlobalPosition, out this._closestSide, out this._connectedSides);
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00049480 File Offset: 0x00047680
		private static void CalculateArcherPositionSideUsingAttackerRegions(SiegeQuerySystem siegeQuerySystem, Vec3 position, out FormationAI.BehaviorSide _closestSide, out int ConnectedSides)
		{
			float num = position.DistanceSquared(siegeQuerySystem.LeftAttackerOrigin);
			float num2 = position.DistanceSquared(siegeQuerySystem.MiddleAttackerOrigin);
			float num3 = position.DistanceSquared(siegeQuerySystem.RightAttackerOrigin);
			FormationAI.BehaviorSide behaviorSide;
			if (num < num2 && num < num3)
			{
				behaviorSide = FormationAI.BehaviorSide.Left;
			}
			else if (num3 < num2)
			{
				behaviorSide = FormationAI.BehaviorSide.Right;
			}
			else
			{
				behaviorSide = FormationAI.BehaviorSide.Middle;
			}
			_closestSide = behaviorSide;
			ConnectedSides = ArcherPosition.ConvertToBinaryPow((int)behaviorSide);
			Vec2 vec = position.AsVec2 - siegeQuerySystem.LeftDefenderOrigin.AsVec2;
			if (vec.DotProduct(siegeQuerySystem.LeftToMidDir) >= 0f && vec.DotProduct(siegeQuerySystem.LeftToMidDir.RightVec()) >= 0f)
			{
				ConnectedSides |= ArcherPosition.ConvertToBinaryPow(0);
			}
			else
			{
				vec = position.AsVec2 - siegeQuerySystem.MidDefenderOrigin.AsVec2;
				if (vec.DotProduct(siegeQuerySystem.MidToLeftDir) >= 0f && vec.DotProduct(siegeQuerySystem.MidToLeftDir.RightVec()) >= 0f)
				{
					ConnectedSides |= ArcherPosition.ConvertToBinaryPow(0);
				}
			}
			vec = position.AsVec2 - siegeQuerySystem.MidDefenderOrigin.AsVec2;
			if (vec.DotProduct(siegeQuerySystem.LeftToMidDir) >= 0f && vec.DotProduct(siegeQuerySystem.LeftToMidDir.LeftVec()) >= 0f)
			{
				ConnectedSides |= ArcherPosition.ConvertToBinaryPow(1);
			}
			else
			{
				vec = position.AsVec2 - siegeQuerySystem.RightDefenderOrigin.AsVec2;
				if (vec.DotProduct(siegeQuerySystem.RightToMidDir) >= 0f && vec.DotProduct(siegeQuerySystem.RightToMidDir.RightVec()) >= 0f)
				{
					ConnectedSides |= ArcherPosition.ConvertToBinaryPow(1);
				}
			}
			vec = position.AsVec2 - siegeQuerySystem.RightDefenderOrigin.AsVec2;
			if (vec.DotProduct(siegeQuerySystem.MidToRightDir) >= 0f && vec.DotProduct(siegeQuerySystem.MidToRightDir.LeftVec()) >= 0f)
			{
				ConnectedSides |= ArcherPosition.ConvertToBinaryPow(2);
				return;
			}
			vec = position.AsVec2 - siegeQuerySystem.RightDefenderOrigin.AsVec2;
			if (vec.DotProduct(siegeQuerySystem.RightToMidDir) >= 0f && vec.DotProduct(siegeQuerySystem.RightToMidDir.LeftVec()) >= 0f)
			{
				ConnectedSides |= ArcherPosition.ConvertToBinaryPow(2);
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000496F8 File Offset: 0x000478F8
		private static void CalculateArcherPositionSideUsingDefenderLanes(SiegeQuerySystem siegeQuerySystem, Vec3 position, out FormationAI.BehaviorSide _closestSide, out int ConnectedSides)
		{
			float num = position.DistanceSquared(siegeQuerySystem.LeftDefenderOrigin);
			float num2 = position.DistanceSquared(siegeQuerySystem.MidDefenderOrigin);
			float num3 = position.DistanceSquared(siegeQuerySystem.RightDefenderOrigin);
			FormationAI.BehaviorSide behaviorSide;
			if (num < num2 && num < num3)
			{
				behaviorSide = FormationAI.BehaviorSide.Left;
			}
			else if (num3 < num2)
			{
				behaviorSide = FormationAI.BehaviorSide.Right;
			}
			else
			{
				behaviorSide = FormationAI.BehaviorSide.Middle;
			}
			FormationAI.BehaviorSide behaviorSide2 = FormationAI.BehaviorSide.BehaviorSideNotSet;
			switch (behaviorSide)
			{
			case FormationAI.BehaviorSide.Left:
				if ((position.AsVec2 - siegeQuerySystem.LeftDefenderOrigin.AsVec2).Normalized().DotProduct(siegeQuerySystem.DefenderLeftToDefenderMidDir) > 0f)
				{
					behaviorSide2 = FormationAI.BehaviorSide.Middle;
				}
				break;
			case FormationAI.BehaviorSide.Middle:
				if ((position.AsVec2 - siegeQuerySystem.MidDefenderOrigin.AsVec2).Normalized().DotProduct(siegeQuerySystem.DefenderMidToDefenderRightDir) > 0f)
				{
					behaviorSide2 = FormationAI.BehaviorSide.Right;
				}
				else
				{
					behaviorSide2 = FormationAI.BehaviorSide.Left;
				}
				break;
			case FormationAI.BehaviorSide.Right:
				if ((position.AsVec2 - siegeQuerySystem.RightDefenderOrigin.AsVec2).Normalized().DotProduct(siegeQuerySystem.DefenderMidToDefenderRightDir) < 0f)
				{
					behaviorSide2 = FormationAI.BehaviorSide.Middle;
				}
				break;
			}
			_closestSide = behaviorSide;
			ConnectedSides = ArcherPosition.ConvertToBinaryPow((int)behaviorSide);
			if (behaviorSide2 != FormationAI.BehaviorSide.BehaviorSideNotSet)
			{
				ConnectedSides |= ArcherPosition.ConvertToBinaryPow((int)behaviorSide2);
			}
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00049843 File Offset: 0x00047A43
		public void SetLastAssignedFormation(int teamIndex, Formation formation)
		{
			if (teamIndex >= 0)
			{
				this._lastAssignedFormations[teamIndex] = formation;
			}
		}

		// Token: 0x0400055C RID: 1372
		private FormationAI.BehaviorSide _closestSide;

		// Token: 0x0400055D RID: 1373
		private int _connectedSides;

		// Token: 0x0400055E RID: 1374
		private SiegeQuerySystem _siegeQuerySystem;

		// Token: 0x0400055F RID: 1375
		private readonly Formation[] _lastAssignedFormations;
	}
}
