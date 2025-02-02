using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C0 RID: 704
	public abstract class SpawnFrameBehaviorBase
	{
		// Token: 0x060026AF RID: 9903 RVA: 0x00092195 File Offset: 0x00090395
		public virtual void Initialize()
		{
			this.SpawnPoints = Mission.Current.Scene.FindEntitiesWithTag("spawnpoint");
		}

		// Token: 0x060026B0 RID: 9904
		public abstract MatrixFrame GetSpawnFrame(Team team, bool hasMount, bool isInitialSpawn);

		// Token: 0x060026B1 RID: 9905 RVA: 0x000921B4 File Offset: 0x000903B4
		protected MatrixFrame GetSpawnFrameFromSpawnPoints(IList<GameEntity> spawnPointsList, Team team, bool hasMount)
		{
			float num = float.MinValue;
			int index = -1;
			for (int i = 0; i < spawnPointsList.Count; i++)
			{
				float num2 = MBRandom.RandomFloat * 0.2f;
				float num3 = 0f;
				if (hasMount && spawnPointsList[i].HasTag("exclude_mounted"))
				{
					num2 -= 1000f;
				}
				if (!hasMount && spawnPointsList[i].HasTag("exclude_footmen"))
				{
					num2 -= 1000f;
				}
				SpawnFrameBehaviorBase.WeightCache weightCache = SpawnFrameBehaviorBase.WeightCache.CreateDecreasingCache();
				SpawnFrameBehaviorBase.WeightCache weightCache2 = SpawnFrameBehaviorBase.WeightCache.CreateDecreasingCache();
				foreach (Agent agent in Mission.Current.Agents)
				{
					if (!agent.IsMount)
					{
						float length = (agent.Position - spawnPointsList[i].GlobalPosition).Length;
						float num5;
						if (team == null || agent.Team.IsEnemyOf(team))
						{
							float num4 = 3.75f - length * 0.125f;
							num5 = MathF.Tanh(num4 * num4) * -2f + 3.1f - length * 0.0125f - 1f / ((length + 0.0001f) * 0.05f);
						}
						else
						{
							float num6 = 1.8f - length * 0.1f;
							num5 = -MathF.Tanh(num6 * num6) + 1.7f - length * 0.01f - 1f / ((length + 0.0001f) * 0.1f);
						}
						float num8;
						if (num5 >= 0f)
						{
							float num7;
							if (weightCache.CheckAndInsertNewValueIfLower(num5, out num7))
							{
								num3 -= num7;
							}
						}
						else if (weightCache2.CheckAndInsertNewValueIfLower(num5, out num8))
						{
							num3 -= num8;
						}
					}
				}
				if (num3 > 0f)
				{
					num3 /= (float)Mission.Current.Agents.Count;
				}
				num2 += num3;
				if (num2 > num)
				{
					num = num2;
					index = i;
				}
			}
			MatrixFrame globalFrame = spawnPointsList[index].GetGlobalFrame();
			globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			return globalFrame;
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000923D8 File Offset: 0x000905D8
		public void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
		}

		// Token: 0x04000E62 RID: 3682
		private const string ExcludeMountedTag = "exclude_mounted";

		// Token: 0x04000E63 RID: 3683
		private const string ExcludeFootmenTag = "exclude_footmen";

		// Token: 0x04000E64 RID: 3684
		protected const string SpawnPointTag = "spawnpoint";

		// Token: 0x04000E65 RID: 3685
		public IEnumerable<GameEntity> SpawnPoints;

		// Token: 0x02000582 RID: 1410
		private struct WeightCache
		{
			// Token: 0x170009AF RID: 2479
			private float this[int index]
			{
				get
				{
					switch (index)
					{
					case 0:
						return this._value1;
					case 1:
						return this._value2;
					case 2:
						return this._value3;
					default:
						throw new ArgumentOutOfRangeException();
					}
				}
				set
				{
					switch (index)
					{
					case 0:
						this._value1 = value;
						return;
					case 1:
						this._value2 = value;
						return;
					case 2:
						this._value3 = value;
						return;
					default:
						return;
					}
				}
			}

			// Token: 0x06003A13 RID: 14867 RVA: 0x000E637D File Offset: 0x000E457D
			private WeightCache(float value1, float value2, float value3)
			{
				this._value1 = value1;
				this._value2 = value2;
				this._value3 = value3;
			}

			// Token: 0x06003A14 RID: 14868 RVA: 0x000E6394 File Offset: 0x000E4594
			public static SpawnFrameBehaviorBase.WeightCache CreateDecreasingCache()
			{
				return new SpawnFrameBehaviorBase.WeightCache(float.NaN, float.NaN, float.NaN);
			}

			// Token: 0x06003A15 RID: 14869 RVA: 0x000E63AC File Offset: 0x000E45AC
			public bool CheckAndInsertNewValueIfLower(float value, out float valueDifference)
			{
				int index = 0;
				for (int i = 1; i < 3; i++)
				{
					if (this[i] > this[index])
					{
						index = i;
					}
				}
				if (float.IsNaN(this[index]) || value < this[index])
				{
					valueDifference = (float.IsNaN(this[index]) ? MathF.Abs(value) : (this[index] - value));
					this[index] = value;
					return true;
				}
				valueDifference = float.NaN;
				return false;
			}

			// Token: 0x04001D71 RID: 7537
			private const int Length = 3;

			// Token: 0x04001D72 RID: 7538
			private float _value1;

			// Token: 0x04001D73 RID: 7539
			private float _value2;

			// Token: 0x04001D74 RID: 7540
			private float _value3;
		}
	}
}
