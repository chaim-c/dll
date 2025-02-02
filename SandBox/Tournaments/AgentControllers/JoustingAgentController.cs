using System;
using SandBox.Tournaments.MissionLogics;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Tournaments.AgentControllers
{
	// Token: 0x02000033 RID: 51
	public class JoustingAgentController : AgentController
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000BAD1 File Offset: 0x00009CD1
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000BAD9 File Offset: 0x00009CD9
		public JoustingAgentController.JoustingAgentState State
		{
			get
			{
				return this._state;
			}
			set
			{
				if (value != this._state)
				{
					this._state = value;
					this.JoustingMissionController.OnJoustingAgentStateChanged(base.Owner, value);
				}
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000BAFD File Offset: 0x00009CFD
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000BB05 File Offset: 0x00009D05
		public TournamentJoustingMissionController JoustingMissionController { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x0000BB10 File Offset: 0x00009D10
		public Agent Opponent
		{
			get
			{
				if (this._opponentAgent == null)
				{
					foreach (Agent agent in base.Mission.Agents)
					{
						if (agent.IsHuman && agent != base.Owner)
						{
							this._opponentAgent = agent;
						}
					}
				}
				return this._opponentAgent;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000BB88 File Offset: 0x00009D88
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x0000BB90 File Offset: 0x00009D90
		public bool PrepareEquipmentsAfterDismount { get; private set; }

		// Token: 0x060001BA RID: 442 RVA: 0x0000BB99 File Offset: 0x00009D99
		public override void OnInitialize()
		{
			this.JoustingMissionController = base.Mission.GetMissionBehavior<TournamentJoustingMissionController>();
			this._state = JoustingAgentController.JoustingAgentState.WaitingOpponent;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000BBB3 File Offset: 0x00009DB3
		public void UpdateState()
		{
			if (base.Owner.Character.IsPlayerCharacter)
			{
				this.UpdateMainAgentState();
				return;
			}
			this.UpdateAIAgentState();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		private void UpdateMainAgentState()
		{
			JoustingAgentController controller = this.Opponent.GetController<JoustingAgentController>();
			bool flag = this.JoustingMissionController.CornerStartList[this.CurrentCornerIndex].CheckPointWithOrientedBoundingBox(base.Owner.Position) && !this.JoustingMissionController.RegionBoxList[this.CurrentCornerIndex].CheckPointWithOrientedBoundingBox(base.Owner.Position);
			switch (this.State)
			{
			case JoustingAgentController.JoustingAgentState.GoToStartPosition:
				if (flag)
				{
					this.State = JoustingAgentController.JoustingAgentState.WaitInStartPosition;
					return;
				}
				break;
			case JoustingAgentController.JoustingAgentState.WaitInStartPosition:
				if (!flag)
				{
					this.State = JoustingAgentController.JoustingAgentState.GoToStartPosition;
					return;
				}
				if (base.Owner.GetCurrentVelocity().LengthSquared < 0.0025000002f)
				{
					this.State = JoustingAgentController.JoustingAgentState.WaitingOpponent;
					return;
				}
				break;
			case JoustingAgentController.JoustingAgentState.WaitingOpponent:
				if (!flag)
				{
					this.State = JoustingAgentController.JoustingAgentState.GoToStartPosition;
					return;
				}
				if (controller.State == JoustingAgentController.JoustingAgentState.WaitingOpponent || controller.State == JoustingAgentController.JoustingAgentState.Ready)
				{
					this.State = JoustingAgentController.JoustingAgentState.Ready;
					return;
				}
				break;
			case JoustingAgentController.JoustingAgentState.Ready:
				if (this.JoustingMissionController.IsAgentInTheTrack(base.Owner, true) && base.Owner.GetCurrentVelocity().LengthSquared > 0.0025000002f)
				{
					this.State = JoustingAgentController.JoustingAgentState.Riding;
					return;
				}
				if (controller.State == JoustingAgentController.JoustingAgentState.GoToStartPosition)
				{
					this.State = JoustingAgentController.JoustingAgentState.WaitingOpponent;
					return;
				}
				if (!this.JoustingMissionController.CornerStartList[this.CurrentCornerIndex].CheckPointWithOrientedBoundingBox(base.Owner.Position))
				{
					this.State = JoustingAgentController.JoustingAgentState.GoToStartPosition;
					return;
				}
				break;
			case JoustingAgentController.JoustingAgentState.StartRiding:
				break;
			case JoustingAgentController.JoustingAgentState.Riding:
				if (this.JoustingMissionController.IsAgentInTheTrack(base.Owner, false))
				{
					this.State = JoustingAgentController.JoustingAgentState.RidingAtWrongSide;
				}
				if (this.JoustingMissionController.RegionExitBoxList[this.CurrentCornerIndex].CheckPointWithOrientedBoundingBox(base.Owner.Position))
				{
					this.State = JoustingAgentController.JoustingAgentState.GoToStartPosition;
					this.CurrentCornerIndex = 1 - this.CurrentCornerIndex;
					return;
				}
				break;
			case JoustingAgentController.JoustingAgentState.RidingAtWrongSide:
				if (this.JoustingMissionController.IsAgentInTheTrack(base.Owner, true))
				{
					this.State = JoustingAgentController.JoustingAgentState.Riding;
					return;
				}
				if (this.JoustingMissionController.CornerStartList[1 - this.CurrentCornerIndex].CheckPointWithOrientedBoundingBox(base.Owner.Position))
				{
					this.State = JoustingAgentController.JoustingAgentState.GoToStartPosition;
					this.CurrentCornerIndex = 1 - this.CurrentCornerIndex;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000BE04 File Offset: 0x0000A004
		private void UpdateAIAgentState()
		{
			if (this.Opponent != null && this.Opponent.IsActive())
			{
				JoustingAgentController controller = this.Opponent.GetController<JoustingAgentController>();
				switch (this.State)
				{
				case JoustingAgentController.JoustingAgentState.GoingToBackStart:
					if (base.Owner.Position.Distance(this.JoustingMissionController.CornerBackStartList[this.CurrentCornerIndex].origin) < 3f && base.Owner.GetCurrentVelocity().LengthSquared < 0.0025000002f)
					{
						this.CurrentCornerIndex = 1 - this.CurrentCornerIndex;
						MatrixFrame globalFrame = this.JoustingMissionController.CornerStartList[this.CurrentCornerIndex].GetGlobalFrame();
						WorldPosition worldPosition = new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, globalFrame.origin, false);
						base.Owner.SetScriptedPositionAndDirection(ref worldPosition, globalFrame.rotation.f.AsVec2.RotationInRadians, false, Agent.AIScriptedFrameFlags.None);
						this.State = JoustingAgentController.JoustingAgentState.GoToStartPosition;
						return;
					}
					break;
				case JoustingAgentController.JoustingAgentState.GoToStartPosition:
					if (this.JoustingMissionController.CornerStartList[this.CurrentCornerIndex].CheckPointWithOrientedBoundingBox(base.Owner.Position) && base.Owner.GetCurrentVelocity().LengthSquared < 0.0025000002f)
					{
						this.State = JoustingAgentController.JoustingAgentState.WaitingOpponent;
						return;
					}
					break;
				case JoustingAgentController.JoustingAgentState.WaitInStartPosition:
					break;
				case JoustingAgentController.JoustingAgentState.WaitingOpponent:
					if (controller.State == JoustingAgentController.JoustingAgentState.WaitingOpponent || controller.State == JoustingAgentController.JoustingAgentState.Ready)
					{
						this.State = JoustingAgentController.JoustingAgentState.Ready;
						return;
					}
					break;
				case JoustingAgentController.JoustingAgentState.Ready:
					if (controller.State == JoustingAgentController.JoustingAgentState.Riding)
					{
						this.State = JoustingAgentController.JoustingAgentState.StartRiding;
						WorldPosition worldPosition2 = new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this.JoustingMissionController.CornerMiddleList[this.CurrentCornerIndex].origin, false);
						base.Owner.SetScriptedPosition(ref worldPosition2, false, Agent.AIScriptedFrameFlags.NeverSlowDown);
						return;
					}
					if (controller.State == JoustingAgentController.JoustingAgentState.Ready)
					{
						WorldPosition worldPosition3 = new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this.JoustingMissionController.CornerStartList[this.CurrentCornerIndex].GetGlobalFrame().origin, false);
						base.Owner.SetScriptedPosition(ref worldPosition3, false, Agent.AIScriptedFrameFlags.NeverSlowDown);
						return;
					}
					this.State = JoustingAgentController.JoustingAgentState.WaitingOpponent;
					return;
				case JoustingAgentController.JoustingAgentState.StartRiding:
					if (base.Owner.Position.Distance(this.JoustingMissionController.CornerMiddleList[this.CurrentCornerIndex].origin) < 3f)
					{
						WorldPosition worldPosition4 = new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this.JoustingMissionController.CornerFinishList[this.CurrentCornerIndex].origin, false);
						base.Owner.SetScriptedPosition(ref worldPosition4, false, Agent.AIScriptedFrameFlags.NeverSlowDown);
						this.State = JoustingAgentController.JoustingAgentState.Riding;
						return;
					}
					break;
				case JoustingAgentController.JoustingAgentState.Riding:
					if (base.Owner.Position.Distance(this.JoustingMissionController.CornerFinishList[this.CurrentCornerIndex].origin) < 3f)
					{
						WorldPosition worldPosition5 = new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this.JoustingMissionController.CornerBackStartList[this.CurrentCornerIndex].origin, false);
						base.Owner.SetScriptedPosition(ref worldPosition5, false, Agent.AIScriptedFrameFlags.None);
						this.State = JoustingAgentController.JoustingAgentState.GoingToBackStart;
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000C148 File Offset: 0x0000A348
		public void PrepareAgentToSwordDuel()
		{
			if (base.Owner.MountAgent != null)
			{
				base.Owner.Controller = Agent.ControllerType.AI;
				WorldPosition worldPosition = this.Opponent.GetWorldPosition();
				base.Owner.SetScriptedPosition(ref worldPosition, false, Agent.AIScriptedFrameFlags.GoWithoutMount);
				this.PrepareEquipmentsAfterDismount = true;
				return;
			}
			this.PrepareEquipmentsForSwordDuel();
			base.Owner.DisableScriptedMovement();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000C1A3 File Offset: 0x0000A3A3
		public void PrepareEquipmentsForSwordDuel()
		{
			this.AddEquipmentsForSwordDuel();
			base.Owner.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
			this.PrepareEquipmentsAfterDismount = false;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		private void AddEquipmentsForSwordDuel()
		{
			base.Owner.DropItem(EquipmentIndex.WeaponItemBeginSlot, WeaponClass.Undefined);
			ItemObject @object = Game.Current.ObjectManager.GetObject<ItemObject>("wooden_sword_t1");
			ItemModifier itemModifier = null;
			IAgentOriginBase origin = base.Owner.Origin;
			MissionWeapon missionWeapon = new MissionWeapon(@object, itemModifier, (origin != null) ? origin.Banner : null);
			base.Owner.EquipWeaponWithNewEntity(EquipmentIndex.Weapon2, ref missionWeapon);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000C21B File Offset: 0x0000A41B
		public bool IsRiding()
		{
			return this.State == JoustingAgentController.JoustingAgentState.StartRiding || this.State == JoustingAgentController.JoustingAgentState.Riding;
		}

		// Token: 0x040000A0 RID: 160
		private JoustingAgentController.JoustingAgentState _state;

		// Token: 0x040000A2 RID: 162
		public int CurrentCornerIndex;

		// Token: 0x040000A3 RID: 163
		private const float MaxDistance = 3f;

		// Token: 0x040000A4 RID: 164
		public int Score;

		// Token: 0x040000A5 RID: 165
		private Agent _opponentAgent;

		// Token: 0x02000116 RID: 278
		public enum JoustingAgentState
		{
			// Token: 0x040004E9 RID: 1257
			GoingToBackStart,
			// Token: 0x040004EA RID: 1258
			GoToStartPosition,
			// Token: 0x040004EB RID: 1259
			WaitInStartPosition,
			// Token: 0x040004EC RID: 1260
			WaitingOpponent,
			// Token: 0x040004ED RID: 1261
			Ready,
			// Token: 0x040004EE RID: 1262
			StartRiding,
			// Token: 0x040004EF RID: 1263
			Riding,
			// Token: 0x040004F0 RID: 1264
			RidingAtWrongSide,
			// Token: 0x040004F1 RID: 1265
			SwordDuel
		}
	}
}
