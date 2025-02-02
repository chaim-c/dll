using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000207 RID: 519
	public class BattleSideSpawnPathSelector
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x000647AA File Offset: 0x000629AA
		public SpawnPathData InitialSpawnPath
		{
			get
			{
				return this._initialSpawnPath;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x000647B2 File Offset: 0x000629B2
		public MBReadOnlyList<SpawnPathData> ReinforcementPaths
		{
			get
			{
				return this._reinforcementSpawnPaths;
			}
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x000647BA File Offset: 0x000629BA
		public BattleSideSpawnPathSelector(Mission mission, Path initialPath, float initialPathCenterRatio, bool initialPathIsInverted)
		{
			this._mission = mission;
			this._initialSpawnPath = new SpawnPathData(initialPath, SpawnPathOrientation.PathCenter, initialPathCenterRatio, initialPathIsInverted);
			this._reinforcementSpawnPaths = new MBList<SpawnPathData>();
			this.FindReinforcementPaths();
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x000647EC File Offset: 0x000629EC
		public bool HasReinforcementPath(Path path)
		{
			return path != null && this._reinforcementSpawnPaths.Exists((SpawnPathData pathData) => pathData.Path.Pointer == path.Pointer);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00064830 File Offset: 0x00062A30
		private void FindReinforcementPaths()
		{
			this._reinforcementSpawnPaths.Clear();
			SpawnPathData spawnPathData = new SpawnPathData(this._initialSpawnPath.Path, SpawnPathOrientation.Local, 0f, this._initialSpawnPath.IsInverted);
			this._reinforcementSpawnPaths.Add(spawnPathData);
			MBList<Path> allSpawnPaths = MBSceneUtilities.GetAllSpawnPaths(this._mission.Scene);
			if (allSpawnPaths.Count == 0)
			{
				return;
			}
			bool flag = false;
			if (allSpawnPaths.Count > 1)
			{
				MatrixFrame[] array = new MatrixFrame[100];
				spawnPathData.Path.GetPoints(array);
				MatrixFrame matrixFrame = spawnPathData.IsInverted ? array[spawnPathData.Path.NumberOfPoints - 1] : array[0];
				SortedList<float, SpawnPathData> sortedList = new SortedList<float, SpawnPathData>();
				foreach (Path path in allSpawnPaths)
				{
					if (path.NumberOfPoints > 1 && path.Pointer != spawnPathData.Path.Pointer)
					{
						path.GetPoints(array);
						MatrixFrame matrixFrame2 = array[0];
						MatrixFrame matrixFrame3 = array[path.NumberOfPoints - 1];
						float key = matrixFrame2.origin.DistanceSquared(matrixFrame.origin);
						float key2 = matrixFrame3.origin.DistanceSquared(matrixFrame.origin);
						sortedList.Add(key, new SpawnPathData(path, SpawnPathOrientation.Local, 0f, false));
						sortedList.Add(key2, new SpawnPathData(path, SpawnPathOrientation.Local, 0f, true));
					}
					else
					{
						flag = (flag || spawnPathData.Path.Pointer == path.Pointer);
					}
				}
				int num = 0;
				using (IEnumerator<KeyValuePair<float, SpawnPathData>> enumerator2 = sortedList.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<float, SpawnPathData> keyValuePair = enumerator2.Current;
						this._reinforcementSpawnPaths.Add(keyValuePair.Value);
						num++;
						if ((float)num >= 2f)
						{
							break;
						}
					}
					return;
				}
			}
			flag = (spawnPathData.Path.Pointer == allSpawnPaths[0].Pointer);
		}

		// Token: 0x0400093C RID: 2364
		public const float MaxNeighborCount = 2f;

		// Token: 0x0400093D RID: 2365
		private readonly Mission _mission;

		// Token: 0x0400093E RID: 2366
		private readonly SpawnPathData _initialSpawnPath;

		// Token: 0x0400093F RID: 2367
		private readonly MBList<SpawnPathData> _reinforcementSpawnPaths;
	}
}
