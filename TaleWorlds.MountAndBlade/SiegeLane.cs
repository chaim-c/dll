using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200017C RID: 380
	public class SiegeLane
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x00049ED8 File Offset: 0x000480D8
		// (set) Token: 0x0600138A RID: 5002 RVA: 0x00049EE0 File Offset: 0x000480E0
		public SiegeLane.LaneStateEnum LaneState { get; private set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x00049EE9 File Offset: 0x000480E9
		public FormationAI.BehaviorSide LaneSide { get; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x00049EF1 File Offset: 0x000480F1
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x00049EF9 File Offset: 0x000480F9
		public List<IPrimarySiegeWeapon> PrimarySiegeWeapons { get; private set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00049F02 File Offset: 0x00048102
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x00049F0A File Offset: 0x0004810A
		public bool IsOpen { get; private set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00049F13 File Offset: 0x00048113
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x00049F1B File Offset: 0x0004811B
		public bool IsBreach { get; private set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00049F24 File Offset: 0x00048124
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x00049F2C File Offset: 0x0004812C
		public bool HasGate { get; private set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00049F35 File Offset: 0x00048135
		// (set) Token: 0x06001395 RID: 5013 RVA: 0x00049F3D File Offset: 0x0004813D
		public List<ICastleKeyPosition> DefensePoints { get; private set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00049F46 File Offset: 0x00048146
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x00049F4E File Offset: 0x0004814E
		public WorldPosition DefenderOrigin { get; private set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x00049F57 File Offset: 0x00048157
		// (set) Token: 0x06001399 RID: 5017 RVA: 0x00049F5F File Offset: 0x0004815F
		public WorldPosition AttackerOrigin { get; private set; }

		// Token: 0x0600139A RID: 5018 RVA: 0x00049F68 File Offset: 0x00048168
		public SiegeLane(FormationAI.BehaviorSide laneSide, SiegeQuerySystem siegeQuerySystem)
		{
			this.LaneSide = laneSide;
			this.IsOpen = false;
			this.PrimarySiegeWeapons = new List<IPrimarySiegeWeapon>();
			this.DefensePoints = new List<ICastleKeyPosition>();
			this.IsBreach = false;
			this._siegeQuerySystem = siegeQuerySystem;
			this._lastAssignedFormations = new Formation[Mission.Current.Teams.Count];
			this.HasGate = false;
			this.LaneState = SiegeLane.LaneStateEnum.Active;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00049FD8 File Offset: 0x000481D8
		public bool CalculateIsLaneUnusable()
		{
			if (this.IsOpen)
			{
				return false;
			}
			if (this.HasGate)
			{
				for (int i = 0; i < this.DefensePoints.Count; i++)
				{
					CastleGate castleGate;
					if ((castleGate = (this.DefensePoints[i] as CastleGate)) != null && castleGate.IsGateOpen && castleGate.GameEntity.HasTag("outer_gate"))
					{
						return false;
					}
				}
			}
			for (int j = 0; j < this.PrimarySiegeWeapons.Count; j++)
			{
				IPrimarySiegeWeapon primarySiegeWeapon = this.PrimarySiegeWeapons[j];
				UsableMachine usableMachine;
				SiegeTower siegeTower;
				BatteringRam batteringRam;
				if (((usableMachine = (primarySiegeWeapon as UsableMachine)) == null || usableMachine.GameEntity != null) && ((siegeTower = (primarySiegeWeapon as SiegeTower)) == null || !siegeTower.IsDestroyed) && (primarySiegeWeapon.HasCompletedAction() || (batteringRam = (primarySiegeWeapon as BatteringRam)) == null || !batteringRam.IsDestroyed))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004A0AE File Offset: 0x000482AE
		public Formation GetLastAssignedFormation(int teamIndex)
		{
			if (teamIndex >= 0)
			{
				return this._lastAssignedFormations[teamIndex];
			}
			return null;
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004A0BE File Offset: 0x000482BE
		public void SetLaneState(SiegeLane.LaneStateEnum newLaneState)
		{
			this.LaneState = newLaneState;
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0004A0C7 File Offset: 0x000482C7
		public void SetLastAssignedFormation(int teamIndex, Formation formation)
		{
			if (teamIndex >= 0)
			{
				this._lastAssignedFormations[teamIndex] = formation;
			}
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004A0D6 File Offset: 0x000482D6
		public void SetSiegeQuerySystem(SiegeQuerySystem siegeQuerySystem)
		{
			this._siegeQuerySystem = siegeQuerySystem;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0004A0E0 File Offset: 0x000482E0
		public float CalculateLaneCapacity()
		{
			bool flag = false;
			for (int i = 0; i < this.DefensePoints.Count; i++)
			{
				WallSegment wallSegment;
				if ((wallSegment = (this.DefensePoints[i] as WallSegment)) != null && wallSegment.IsBreachedWall)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				return 60f;
			}
			if (this.HasGate)
			{
				bool flag2 = true;
				for (int j = 0; j < this.DefensePoints.Count; j++)
				{
					CastleGate castleGate;
					if ((castleGate = (this.DefensePoints[j] as CastleGate)) != null && !castleGate.IsGateOpen)
					{
						flag2 = false;
						break;
					}
				}
				if (flag2)
				{
					return 60f;
				}
			}
			float num = 0f;
			for (int k = 0; k < this.PrimarySiegeWeapons.Count; k++)
			{
				SiegeWeapon siegeWeapon = this.PrimarySiegeWeapons[k] as SiegeWeapon;
				if (!siegeWeapon.IsDeactivated && !siegeWeapon.IsDestroyed)
				{
					num += this.PrimarySiegeWeapons[k].SiegeWeaponPriority;
				}
			}
			return num;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0004A1E0 File Offset: 0x000483E0
		public SiegeLane.LaneDefenseStates GetDefenseState()
		{
			switch (this.LaneState)
			{
			case SiegeLane.LaneStateEnum.Safe:
			case SiegeLane.LaneStateEnum.Unused:
				return SiegeLane.LaneDefenseStates.Empty;
			case SiegeLane.LaneStateEnum.Used:
			case SiegeLane.LaneStateEnum.Abandoned:
				return SiegeLane.LaneDefenseStates.Token;
			case SiegeLane.LaneStateEnum.Active:
			case SiegeLane.LaneStateEnum.Contested:
			case SiegeLane.LaneStateEnum.Conceited:
				return SiegeLane.LaneDefenseStates.Full;
			default:
				return SiegeLane.LaneDefenseStates.Full;
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004A220 File Offset: 0x00048420
		private bool IsPowerBehindLane()
		{
			switch (this.LaneSide)
			{
			case FormationAI.BehaviorSide.Left:
				return this._siegeQuerySystem.LeftRegionMemberCount >= 30;
			case FormationAI.BehaviorSide.Middle:
				return this._siegeQuerySystem.MiddleRegionMemberCount >= 30;
			case FormationAI.BehaviorSide.Right:
				return this._siegeQuerySystem.RightRegionMemberCount >= 30;
			default:
				MBDebug.ShowWarning("Lane without side");
				return false;
			}
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004A28C File Offset: 0x0004848C
		public bool IsUnderAttack()
		{
			switch (this.LaneSide)
			{
			case FormationAI.BehaviorSide.Left:
				return this._siegeQuerySystem.LeftCloseAttackerCount >= 15;
			case FormationAI.BehaviorSide.Middle:
				return this._siegeQuerySystem.MiddleCloseAttackerCount >= 15;
			case FormationAI.BehaviorSide.Right:
				return this._siegeQuerySystem.RightCloseAttackerCount >= 15;
			default:
				MBDebug.ShowWarning("Lane without side");
				return false;
			}
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0004A2F8 File Offset: 0x000484F8
		public bool IsDefended()
		{
			switch (this.LaneSide)
			{
			case FormationAI.BehaviorSide.Left:
				return this._siegeQuerySystem.LeftDefenderCount >= 15;
			case FormationAI.BehaviorSide.Middle:
				return this._siegeQuerySystem.MiddleDefenderCount >= 15;
			case FormationAI.BehaviorSide.Right:
				return this._siegeQuerySystem.RightDefenderCount >= 15;
			default:
				MBDebug.ShowWarning("Lane without side");
				return false;
			}
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0004A364 File Offset: 0x00048564
		public void DetermineLaneState()
		{
			if (this.LaneState != SiegeLane.LaneStateEnum.Conceited || this.IsDefended())
			{
				if (this.CalculateIsLaneUnusable())
				{
					this.LaneState = SiegeLane.LaneStateEnum.Safe;
				}
				else if (Mission.Current.IsTeleportingAgents)
				{
					this.LaneState = SiegeLane.LaneStateEnum.Active;
				}
				else if (!this.IsOpen)
				{
					bool flag = true;
					foreach (IPrimarySiegeWeapon primarySiegeWeapon in this.PrimarySiegeWeapons)
					{
						if (!(primarySiegeWeapon is IMoveableSiegeWeapon) || primarySiegeWeapon.HasCompletedAction() || ((SiegeWeapon)primarySiegeWeapon).IsUsed)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						this.LaneState = SiegeLane.LaneStateEnum.Unused;
					}
					else
					{
						this.LaneState = ((!this.IsPowerBehindLane()) ? SiegeLane.LaneStateEnum.Used : SiegeLane.LaneStateEnum.Active);
					}
				}
				else if (!this.IsPowerBehindLane())
				{
					this.LaneState = SiegeLane.LaneStateEnum.Abandoned;
				}
				else
				{
					this.LaneState = ((!this.IsUnderAttack() || this.IsDefended()) ? SiegeLane.LaneStateEnum.Contested : SiegeLane.LaneStateEnum.Conceited);
				}
				if (this.HasGate && this.LaneState < SiegeLane.LaneStateEnum.Active && TeamAISiegeComponent.QuerySystem.InsideAttackerCount >= 15)
				{
					this.LaneState = SiegeLane.LaneStateEnum.Active;
				}
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0004A48C File Offset: 0x0004868C
		public WorldPosition GetCurrentAttackerPosition()
		{
			if (this.IsBreach)
			{
				return this.DefenderOrigin;
			}
			if (this._attackerMovableWeapon != null)
			{
				return this._attackerMovableWeapon.WaitFrame.origin.ToWorldPosition();
			}
			return this.AttackerOrigin;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0004A4C4 File Offset: 0x000486C4
		public void DetermineOrigins()
		{
			this._attackerMovableWeapon = null;
			if (this.IsBreach)
			{
				WallSegment wallSegment = this.DefensePoints.FirstOrDefault((ICastleKeyPosition dp) => dp is WallSegment && (dp as WallSegment).IsBreachedWall) as WallSegment;
				this.DefenderOrigin = wallSegment.MiddleFrame.Origin;
				this.AttackerOrigin = wallSegment.AttackerWaitFrame.Origin;
				return;
			}
			this.HasGate = this.DefensePoints.Any((ICastleKeyPosition dp) => dp is CastleGate);
			IEnumerable<IPrimarySiegeWeapon> enumerable;
			if (this.PrimarySiegeWeapons.Count != 0)
			{
				IEnumerable<IPrimarySiegeWeapon> primarySiegeWeapons = this.PrimarySiegeWeapons;
				enumerable = primarySiegeWeapons;
			}
			else
			{
				enumerable = from sw in Mission.Current.MissionObjects.FindAllWithType<SiegeWeapon>().Where(delegate(SiegeWeapon sw)
				{
					IPrimarySiegeWeapon primarySiegeWeapon;
					return (primarySiegeWeapon = (sw as IPrimarySiegeWeapon)) != null && primarySiegeWeapon.WeaponSide == this.LaneSide;
				})
				select sw as IPrimarySiegeWeapon;
			}
			IEnumerable<IPrimarySiegeWeapon> source = enumerable;
			IMoveableSiegeWeapon moveableSiegeWeapon;
			if ((moveableSiegeWeapon = (source.FirstOrDefault((IPrimarySiegeWeapon psw) => psw is IMoveableSiegeWeapon) as IMoveableSiegeWeapon)) != null)
			{
				this._attackerMovableWeapon = (moveableSiegeWeapon as SiegeWeapon);
				this.DefenderOrigin = ((moveableSiegeWeapon as IPrimarySiegeWeapon).TargetCastlePosition as ICastleKeyPosition).MiddleFrame.Origin;
				this.AttackerOrigin = moveableSiegeWeapon.GetInitialFrame().origin.ToWorldPosition();
				return;
			}
			SiegeLadder siegeLadder = source.FirstOrDefault((IPrimarySiegeWeapon psw) => psw is SiegeLadder) as SiegeLadder;
			this.DefenderOrigin = (siegeLadder.TargetCastlePosition as ICastleKeyPosition).MiddleFrame.Origin;
			this.AttackerOrigin = siegeLadder.InitialWaitPosition.GetGlobalFrame().origin.ToWorldPosition();
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0004A694 File Offset: 0x00048894
		public void RefreshLane()
		{
			for (int i = this.PrimarySiegeWeapons.Count - 1; i >= 0; i--)
			{
				SiegeWeapon siegeWeapon;
				if ((siegeWeapon = (this.PrimarySiegeWeapons[i] as SiegeWeapon)) != null && siegeWeapon.IsDisabled)
				{
					this.PrimarySiegeWeapons.RemoveAt(i);
				}
			}
			bool flag = false;
			for (int j = 0; j < this.DefensePoints.Count; j++)
			{
				WallSegment wallSegment;
				if ((wallSegment = (this.DefensePoints[j] as WallSegment)) != null && wallSegment.IsBreachedWall)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				this.IsOpen = true;
				this.IsBreach = true;
				return;
			}
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			for (int k = 0; k < this.DefensePoints.Count; k++)
			{
				ICastleKeyPosition castleKeyPosition = this.DefensePoints[k];
				CastleGate castleGate;
				if (flag4 && (castleGate = (castleKeyPosition as CastleGate)) != null)
				{
					flag2 = true;
					flag3 = true;
					if (!castleGate.IsDestroyed && castleGate.State != CastleGate.GateState.Open)
					{
						flag4 = false;
						break;
					}
				}
				else if (!flag3 && !(castleKeyPosition is WallSegment))
				{
					flag3 = true;
				}
			}
			bool flag5 = false;
			if (!flag3)
			{
				for (int l = 0; l < this.PrimarySiegeWeapons.Count; l++)
				{
					IPrimarySiegeWeapon primarySiegeWeapon = this.PrimarySiegeWeapons[l];
					if (primarySiegeWeapon.HasCompletedAction() && !(primarySiegeWeapon as UsableMachine).IsDestroyed)
					{
						flag5 = true;
						break;
					}
				}
			}
			this.IsOpen = ((flag2 && flag4) || flag5);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0004A7F8 File Offset: 0x000489F8
		public void SetPrimarySiegeWeapons(List<IPrimarySiegeWeapon> primarySiegeWeapons)
		{
			this.PrimarySiegeWeapons = primarySiegeWeapons;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0004A804 File Offset: 0x00048A04
		public void SetDefensePoints(List<ICastleKeyPosition> defensePoints)
		{
			this.DefensePoints = defensePoints;
			foreach (ICastleKeyPosition castleKeyPosition in from psw in this.PrimarySiegeWeapons
			select psw.TargetCastlePosition as ICastleKeyPosition)
			{
				if (castleKeyPosition != null && !this.DefensePoints.Contains(castleKeyPosition))
				{
					this.DefensePoints.Add(castleKeyPosition);
				}
			}
		}

		// Token: 0x04000569 RID: 1385
		private readonly Formation[] _lastAssignedFormations;

		// Token: 0x0400056A RID: 1386
		private SiegeQuerySystem _siegeQuerySystem;

		// Token: 0x0400056B RID: 1387
		private SiegeWeapon _attackerMovableWeapon;

		// Token: 0x020004A9 RID: 1193
		public enum LaneStateEnum
		{
			// Token: 0x04001A86 RID: 6790
			Safe,
			// Token: 0x04001A87 RID: 6791
			Unused,
			// Token: 0x04001A88 RID: 6792
			Used,
			// Token: 0x04001A89 RID: 6793
			Active,
			// Token: 0x04001A8A RID: 6794
			Abandoned,
			// Token: 0x04001A8B RID: 6795
			Contested,
			// Token: 0x04001A8C RID: 6796
			Conceited
		}

		// Token: 0x020004AA RID: 1194
		public enum LaneDefenseStates
		{
			// Token: 0x04001A8E RID: 6798
			Empty,
			// Token: 0x04001A8F RID: 6799
			Token,
			// Token: 0x04001A90 RID: 6800
			Full
		}
	}
}
