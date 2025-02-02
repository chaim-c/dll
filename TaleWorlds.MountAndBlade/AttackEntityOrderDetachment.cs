using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200017B RID: 379
	public class AttackEntityOrderDetachment : IDetachment
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x00049852 File Offset: 0x00047A52
		public MBReadOnlyList<Formation> UserFormations
		{
			get
			{
				return this._userFormations;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0004985A File Offset: 0x00047A5A
		public bool IsLoose
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0004985D File Offset: 0x00047A5D
		public bool IsActive
		{
			get
			{
				return this._targetEntityDestructableComponent != null && !this._targetEntityDestructableComponent.IsDestroyed;
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00049878 File Offset: 0x00047A78
		public AttackEntityOrderDetachment(GameEntity targetEntity)
		{
			this._targetEntity = targetEntity;
			this._targetEntityDestructableComponent = this._targetEntity.GetFirstScriptOfType<DestructableComponent>();
			this._surroundEntity = (this._targetEntity.GetFirstScriptOfType<CastleGate>() == null);
			this._agents = new List<Agent>();
			this._userFormations = new MBList<Formation>();
			MatrixFrame globalFrame = this._targetEntity.GetGlobalFrame();
			this._frame = new WorldFrame(globalFrame.rotation, new WorldPosition(targetEntity.GetScenePointer(), UIntPtr.Zero, globalFrame.origin, false));
			this._frame.Rotation.Orthonormalize();
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00049911 File Offset: 0x00047B11
		public Vec3 GetPosition()
		{
			return this._frame.Origin.GetGroundVec3();
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00049924 File Offset: 0x00047B24
		public void AddAgent(Agent agent, int slotIndex)
		{
			this._agents.Add(agent);
			agent.SetScriptedTargetEntityAndPosition(this._targetEntity, new WorldPosition(agent.Mission.Scene, UIntPtr.Zero, this._targetEntity.GlobalPosition, false), this._surroundEntity ? Agent.AISpecialCombatModeFlags.SurroundAttackEntity : Agent.AISpecialCombatModeFlags.None, false);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00049977 File Offset: 0x00047B77
		public void AddAgentAtSlotIndex(Agent agent, int slotIndex)
		{
			this.AddAgent(agent, slotIndex);
			Formation formation = agent.Formation;
			if (formation != null)
			{
				formation.DetachUnit(agent, true);
			}
			agent.Detachment = this;
			agent.DetachmentWeight = 1f;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000499A6 File Offset: 0x00047BA6
		void IDetachment.FormationStartUsing(Formation formation)
		{
			this._userFormations.Add(formation);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000499B4 File Offset: 0x00047BB4
		void IDetachment.FormationStopUsing(Formation formation)
		{
			this._userFormations.Remove(formation);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000499C3 File Offset: 0x00047BC3
		public bool IsUsedByFormation(Formation formation)
		{
			return this._userFormations.Contains(formation);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x000499D1 File Offset: 0x00047BD1
		Agent IDetachment.GetMovingAgentAtSlotIndex(int slotIndex)
		{
			return null;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000499D4 File Offset: 0x00047BD4
		void IDetachment.GetSlotIndexWeightTuples(List<ValueTuple<int, float>> slotIndexWeightTuples)
		{
			for (int i = this._agents.Count; i < 8; i++)
			{
				slotIndexWeightTuples.Add(new ValueTuple<int, float>(i, AttackEntityOrderDetachment.CalculateWeight(i)));
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00049A09 File Offset: 0x00047C09
		bool IDetachment.IsSlotAtIndexAvailableForAgent(int slotIndex, Agent agent)
		{
			return slotIndex < 8 && slotIndex >= this._agents.Count && agent.CanBeAssignedForScriptedMovement() && !this.IsAgentOnInconvenientNavmesh(agent);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00049A34 File Offset: 0x00047C34
		private bool IsAgentOnInconvenientNavmesh(Agent agent)
		{
			if (Mission.Current.MissionTeamAIType != Mission.MissionTeamAITypeEnum.Siege)
			{
				return false;
			}
			int currentNavigationFaceId = agent.GetCurrentNavigationFaceId();
			TeamAISiegeComponent teamAISiegeComponent;
			if ((teamAISiegeComponent = (agent.Team.TeamAI as TeamAISiegeComponent)) != null)
			{
				foreach (int num in teamAISiegeComponent.DifficultNavmeshIDs)
				{
					if (currentNavigationFaceId == num)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00049AB8 File Offset: 0x00047CB8
		bool IDetachment.IsAgentEligible(Agent agent)
		{
			return true;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00049ABB File Offset: 0x00047CBB
		void IDetachment.UnmarkDetachment()
		{
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00049ABD File Offset: 0x00047CBD
		bool IDetachment.IsDetachmentRecentlyEvaluated()
		{
			return false;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00049AC0 File Offset: 0x00047CC0
		void IDetachment.MarkSlotAtIndex(int slotIndex)
		{
			Debug.FailedAssert("This should never have been called because this detachment does not seek to replace moving agents.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\TeamAI\\AttackEntityOrderDetachment.cs", "MarkSlotAtIndex", 149);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00049ADB File Offset: 0x00047CDB
		bool IDetachment.IsAgentUsingOrInterested(Agent agent)
		{
			return this._agents.Contains(agent);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00049AEC File Offset: 0x00047CEC
		void IDetachment.OnFormationLeave(Formation formation)
		{
			for (int i = this._agents.Count - 1; i >= 0; i--)
			{
				Agent agent = this._agents[i];
				if (agent.Formation == formation && !agent.IsPlayerControlled)
				{
					((IDetachment)this).RemoveAgent(agent);
					formation.AttachUnit(agent);
				}
			}
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00049B3D File Offset: 0x00047D3D
		public bool IsStandingPointAvailableForAgent(Agent agent)
		{
			return this._agents.Count < 8;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00049B50 File Offset: 0x00047D50
		public List<float> GetTemplateCostsOfAgent(Agent candidate, List<float> oldValue)
		{
			WorldPosition worldPosition = candidate.GetWorldPosition();
			WorldPosition origin = this._frame.Origin;
			origin.SetVec2(origin.AsVec2 + this._frame.Rotation.f.AsVec2.Normalized() * 0.7f);
			float num2;
			float num = Mission.Current.Scene.GetPathDistanceBetweenPositions(ref worldPosition, ref origin, candidate.Monster.BodyCapsuleRadius, out num2) ? num2 : float.MaxValue;
			num *= MissionGameModels.Current.AgentStatCalculateModel.GetDetachmentCostMultiplierOfAgent(candidate, this);
			List<float> list = oldValue ?? new List<float>(8);
			list.Clear();
			for (int i = 0; i < 8; i++)
			{
				list.Add(num);
			}
			return list;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00049C18 File Offset: 0x00047E18
		float IDetachment.GetExactCostOfAgentAtSlot(Agent candidate, int slotIndex)
		{
			Debug.FailedAssert("This should never have been called because this detachment does not seek to replace moving agents.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\TeamAI\\AttackEntityOrderDetachment.cs", "GetExactCostOfAgentAtSlot", 205);
			return 0f;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00049C38 File Offset: 0x00047E38
		public float GetTemplateWeightOfAgent(Agent candidate)
		{
			WorldPosition worldPosition = candidate.GetWorldPosition();
			WorldPosition origin = this._frame.Origin;
			float result;
			if (!Mission.Current.Scene.GetPathDistanceBetweenPositions(ref worldPosition, ref origin, candidate.Monster.BodyCapsuleRadius, out result))
			{
				return float.MaxValue;
			}
			return result;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00049C84 File Offset: 0x00047E84
		public float? GetWeightOfAgentAtNextSlot(List<Agent> newAgents, out Agent match)
		{
			float? weightOfNextSlot = this.GetWeightOfNextSlot(newAgents[0].Team.Side);
			if (this._agents.Count < 8)
			{
				Vec3 position = this._targetEntity.GlobalPosition;
				match = newAgents.MinBy((Agent a) => a.Position.DistanceSquared(position));
				return weightOfNextSlot;
			}
			match = null;
			return null;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00049CF0 File Offset: 0x00047EF0
		public float? GetWeightOfAgentAtNextSlot(List<ValueTuple<Agent, float>> agentTemplateScores, out Agent match)
		{
			float? weight = this.GetWeightOfNextSlot(agentTemplateScores[0].Item1.Team.Side);
			if (this._agents.Count < 8)
			{
				IEnumerable<ValueTuple<Agent, float>> source = agentTemplateScores.Where(delegate(ValueTuple<Agent, float> a)
				{
					Agent item = a.Item1;
					if (item.IsDetachedFromFormation)
					{
						float detachmentWeight = item.DetachmentWeight;
						float? num = weight * 0.4f;
						return detachmentWeight < num.GetValueOrDefault() & num != null;
					}
					return true;
				});
				if (source.Any<ValueTuple<Agent, float>>())
				{
					match = source.MinBy((ValueTuple<Agent, float> a) => a.Item2).Item1;
					return weight;
				}
			}
			match = null;
			return null;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x00049D90 File Offset: 0x00047F90
		public float? GetWeightOfAgentAtOccupiedSlot(Agent detachedAgent, List<Agent> newAgents, out Agent match)
		{
			float weightOfOccupiedSlot = this.GetWeightOfOccupiedSlot(detachedAgent);
			Vec3 position = this._targetEntity.GlobalPosition;
			match = newAgents.MinBy((Agent a) => a.Position.DistanceSquared(position));
			return new float?(weightOfOccupiedSlot * 0.5f);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00049DDA File Offset: 0x00047FDA
		public void RemoveAgent(Agent agent)
		{
			this._agents.Remove(agent);
			agent.DisableScriptedMovement();
			agent.DisableScriptedCombatMovement();
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00049DF5 File Offset: 0x00047FF5
		public int GetNumberOfUsableSlots()
		{
			return 8;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00049DF8 File Offset: 0x00047FF8
		public WorldFrame? GetAgentFrame(Agent agent)
		{
			return null;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00049E0E File Offset: 0x0004800E
		private static float CalculateWeight(int index)
		{
			return 1f + (1f - (float)index / 8f);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00049E24 File Offset: 0x00048024
		public float? GetWeightOfNextSlot(BattleSideEnum side)
		{
			if (this._agents.Count < 8)
			{
				return new float?(AttackEntityOrderDetachment.CalculateWeight(this._agents.Count));
			}
			return null;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00049E5E File Offset: 0x0004805E
		public float GetWeightOfOccupiedSlot(Agent agent)
		{
			return AttackEntityOrderDetachment.CalculateWeight(this._agents.IndexOf(agent));
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00049E71 File Offset: 0x00048071
		float IDetachment.GetDetachmentWeight(BattleSideEnum side)
		{
			if (this._agents.Count < 8)
			{
				return (float)(8 - this._agents.Count) * 1f / 8f;
			}
			return float.MinValue;
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00049EA1 File Offset: 0x000480A1
		void IDetachment.ResetEvaluation()
		{
			this._isEvaluated = false;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00049EAA File Offset: 0x000480AA
		bool IDetachment.IsEvaluated()
		{
			return this._isEvaluated;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00049EB2 File Offset: 0x000480B2
		void IDetachment.SetAsEvaluated()
		{
			this._isEvaluated = true;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00049EBB File Offset: 0x000480BB
		float IDetachment.GetDetachmentWeightFromCache()
		{
			return this._cachedDetachmentWeight;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00049EC3 File Offset: 0x000480C3
		float IDetachment.ComputeAndCacheDetachmentWeight(BattleSideEnum side)
		{
			this._cachedDetachmentWeight = ((IDetachment)this).GetDetachmentWeight(side);
			return this._cachedDetachmentWeight;
		}

		// Token: 0x04000560 RID: 1376
		private const int Capacity = 8;

		// Token: 0x04000561 RID: 1377
		private readonly List<Agent> _agents;

		// Token: 0x04000562 RID: 1378
		private readonly MBList<Formation> _userFormations;

		// Token: 0x04000563 RID: 1379
		private WorldFrame _frame;

		// Token: 0x04000564 RID: 1380
		private readonly GameEntity _targetEntity;

		// Token: 0x04000565 RID: 1381
		private readonly DestructableComponent _targetEntityDestructableComponent;

		// Token: 0x04000566 RID: 1382
		private readonly bool _surroundEntity;

		// Token: 0x04000567 RID: 1383
		private bool _isEvaluated;

		// Token: 0x04000568 RID: 1384
		private float _cachedDetachmentWeight;
	}
}
