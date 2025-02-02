using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002BE RID: 702
	public class FlagDominationSpawnFrameBehavior : SpawnFrameBehaviorBase
	{
		// Token: 0x060026A4 RID: 9892 RVA: 0x00091AA0 File Offset: 0x0008FCA0
		public override void Initialize()
		{
			base.Initialize();
			this._spawnPointsByTeam = new List<GameEntity>[2];
			this._spawnZonesByTeam = new List<GameEntity>[2];
			this._spawnPointsByTeam[1] = (from x in this.SpawnPoints
			where x.HasTag("attacker")
			select x).ToList<GameEntity>();
			this._spawnPointsByTeam[0] = (from x in this.SpawnPoints
			where x.HasTag("defender")
			select x).ToList<GameEntity>();
			this._spawnZonesByTeam[1] = (from sz in (from sp in this._spawnPointsByTeam[1]
			select sp.Parent).Distinct<GameEntity>()
			where sz != null
			select sz).ToList<GameEntity>();
			this._spawnZonesByTeam[0] = (from sz in (from sp in this._spawnPointsByTeam[0]
			select sp.Parent).Distinct<GameEntity>()
			where sz != null
			select sz).ToList<GameEntity>();
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x00091C00 File Offset: 0x0008FE00
		public override MatrixFrame GetSpawnFrame(Team team, bool hasMount, bool isInitialSpawn)
		{
			GameEntity bestZone = this.GetBestZone(team, isInitialSpawn);
			List<GameEntity> spawnPointList;
			if (bestZone != null)
			{
				spawnPointList = (from sp in this._spawnPointsByTeam[(int)team.Side]
				where sp.Parent == bestZone
				select sp).ToList<GameEntity>();
			}
			else
			{
				spawnPointList = this._spawnPointsByTeam[(int)team.Side].ToList<GameEntity>();
			}
			return this.GetBestSpawnPoint(spawnPointList, hasMount);
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x00091C70 File Offset: 0x0008FE70
		private GameEntity GetBestZone(Team team, bool isInitialSpawn)
		{
			if (this._spawnZonesByTeam[(int)team.Side].Count == 0)
			{
				return null;
			}
			if (isInitialSpawn)
			{
				return this._spawnZonesByTeam[(int)team.Side].Single((GameEntity sz) => sz.HasTag("starting"));
			}
			List<GameEntity> list = (from sz in this._spawnZonesByTeam[(int)team.Side]
			where !sz.HasTag("starting")
			select sz).ToList<GameEntity>();
			if (list.Count == 0)
			{
				return null;
			}
			float[] array = new float[list.Count];
			foreach (NetworkCommunicator networkPeer in GameNetwork.NetworkPeers)
			{
				MissionPeer component = networkPeer.GetComponent<MissionPeer>();
				if (((component != null) ? component.Team : null) != null && component.Team.Side != BattleSideEnum.None && component.ControlledAgent != null && component.ControlledAgent.IsActive())
				{
					for (int i = 0; i < list.Count; i++)
					{
						Vec3 globalPosition = list[i].GlobalPosition;
						if (component.Team != team)
						{
							array[i] -= 1f / (0.0001f + component.ControlledAgent.Position.Distance(globalPosition)) * 1f;
						}
						else
						{
							array[i] += 1f / (0.0001f + component.ControlledAgent.Position.Distance(globalPosition)) * 1.5f;
						}
					}
				}
			}
			int num = -1;
			for (int j = 0; j < array.Length; j++)
			{
				if (num < 0 || array[j] > array[num])
				{
					num = j;
				}
			}
			return list[num];
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x00091E78 File Offset: 0x00090078
		private MatrixFrame GetBestSpawnPoint(List<GameEntity> spawnPointList, bool hasMount)
		{
			float num = float.MinValue;
			int index = -1;
			for (int i = 0; i < spawnPointList.Count; i++)
			{
				float num2 = MBRandom.RandomFloat * 0.2f;
				AgentProximityMap.ProximityMapSearchStruct proximityMapSearchStruct = AgentProximityMap.BeginSearch(Mission.Current, spawnPointList[i].GlobalPosition.AsVec2, 2f, false);
				while (proximityMapSearchStruct.LastFoundAgent != null)
				{
					float num3 = proximityMapSearchStruct.LastFoundAgent.Position.DistanceSquared(spawnPointList[i].GlobalPosition);
					if (num3 < 4f)
					{
						float num4 = MathF.Sqrt(num3);
						num2 -= (2f - num4) * 5f;
					}
					AgentProximityMap.FindNext(Mission.Current, ref proximityMapSearchStruct);
				}
				if (hasMount && spawnPointList[i].HasTag("exclude_mounted"))
				{
					num2 -= 100f;
				}
				if (num2 > num)
				{
					num = num2;
					index = i;
				}
			}
			MatrixFrame globalFrame = spawnPointList[index].GetGlobalFrame();
			globalFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			return globalFrame;
		}

		// Token: 0x04000E59 RID: 3673
		private List<GameEntity>[] _spawnPointsByTeam;

		// Token: 0x04000E5A RID: 3674
		private List<GameEntity>[] _spawnZonesByTeam;
	}
}
