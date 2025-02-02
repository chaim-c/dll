using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000208 RID: 520
	public class BattleSpawnPathSelector
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x00064A60 File Offset: 0x00062C60
		// (set) Token: 0x06001CBD RID: 7357 RVA: 0x00064A68 File Offset: 0x00062C68
		public bool IsInitialized { get; private set; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x00064A71 File Offset: 0x00062C71
		public Path InitialPath
		{
			get
			{
				return this._initialPath;
			}
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x00064A79 File Offset: 0x00062C79
		public BattleSpawnPathSelector(Mission mission)
		{
			this.IsInitialized = false;
			this._initialPath = null;
			this._mission = mission;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00064A98 File Offset: 0x00062C98
		public void Initialize()
		{
			float num;
			bool flag;
			Path path = BattleSpawnPathSelector.FindBestInitialPath(this._mission, out num, out flag);
			if (path != null)
			{
				this._initialPath = path;
				this._battleSideSelectors = new BattleSideSpawnPathSelector[2];
				this._battleSideSelectors[0] = new BattleSideSpawnPathSelector(this._mission, path, num, flag);
				this._battleSideSelectors[1] = new BattleSideSpawnPathSelector(this._mission, path, MathF.Max(1f - num, 0f), !flag);
				this.IsInitialized = true;
				return;
			}
			this._initialPath = null;
			this.IsInitialized = false;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00064B28 File Offset: 0x00062D28
		public bool HasPath(Path path)
		{
			if (!this.IsInitialized)
			{
				Debug.FailedAssert("BattleSpawnPathSelector must be initialized.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\BattleSpawnPathSelector.cs", "HasPath", 63);
				return false;
			}
			BattleSideSpawnPathSelector battleSideSpawnPathSelector = this._battleSideSelectors[1];
			BattleSideSpawnPathSelector battleSideSpawnPathSelector2 = this._battleSideSelectors[0];
			return path != null && (this._initialPath.Pointer == path.Pointer || battleSideSpawnPathSelector.HasReinforcementPath(path) || battleSideSpawnPathSelector2.HasReinforcementPath(path));
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x00064B9C File Offset: 0x00062D9C
		public bool GetInitialPathDataOfSide(BattleSideEnum side, out SpawnPathData pathPathData)
		{
			if (!this.IsInitialized)
			{
				Debug.FailedAssert("BattleSpawnPathSelector must be initialized.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\BattleSpawnPathSelector.cs", "GetInitialPathDataOfSide", 77);
				pathPathData = SpawnPathData.Invalid;
				return false;
			}
			pathPathData = this._battleSideSelectors[(int)side].InitialSpawnPath;
			return pathPathData.IsValid;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00064BED File Offset: 0x00062DED
		public MBReadOnlyList<SpawnPathData> GetReinforcementPathsDataOfSide(BattleSideEnum side)
		{
			if (!this.IsInitialized)
			{
				Debug.FailedAssert("BattleSpawnPathSelector must be initialized.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Deployment\\BattleSpawnPathSelector.cs", "GetReinforcementPathsDataOfSide", 91);
				return null;
			}
			return this._battleSideSelectors[(int)side].ReinforcementPaths;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00064C1C File Offset: 0x00062E1C
		public static Path FindBestInitialPath(Mission mission, out float centerRatio, out bool isInverted)
		{
			centerRatio = 0f;
			isInverted = false;
			MBList<Path> allSpawnPaths = MBSceneUtilities.GetAllSpawnPaths(mission.Scene);
			if (allSpawnPaths.IsEmpty<Path>())
			{
				return null;
			}
			int num = 2;
			foreach (Path path in allSpawnPaths)
			{
				num = MathF.Max(path.NumberOfPoints, num);
			}
			Path path2 = null;
			if (mission.HasSceneMapPatch())
			{
				Path path3 = null;
				bool flag = false;
				float num2 = float.MinValue;
				MatrixFrame[] array = new MatrixFrame[num];
				Vec3 vec;
				mission.GetPatchSceneEncounterPosition(out vec);
				Vec2 asVec = vec.AsVec2;
				Vec2 vec2;
				mission.GetPatchSceneEncounterDirection(out vec2);
				foreach (Path path4 in allSpawnPaths)
				{
					if (path4.NumberOfPoints > 1)
					{
						path4.GetPoints(array);
						float num3 = 0f;
						for (int i = 1; i < path4.NumberOfPoints; i++)
						{
							Vec2 asVec2 = array[i - 1].origin.AsVec2;
							Vec2 v = (array[i].origin.AsVec2 - asVec2).Normalized();
							float num4 = vec2.DotProduct(v);
							float num5 = 1000f / (1f + asVec2.Distance(asVec));
							num3 += num5 * num4;
						}
						num3 /= (float)(path4.NumberOfPoints - 1);
						bool flag2 = false;
						if (num3 < 0f)
						{
							num3 = -num3;
							flag2 = true;
						}
						if (num3 >= num2)
						{
							path3 = path4;
							num2 = num3;
							flag = flag2;
						}
					}
				}
				if (path3 != null)
				{
					path3.GetPoints(array);
					float num6 = array[0].origin.AsVec2.DistanceSquared(asVec);
					float num7 = 0f;
					float num8 = 0f;
					for (int j = 1; j < path3.NumberOfPoints; j++)
					{
						float num9 = array[j].origin.AsVec2.DistanceSquared(asVec);
						num8 += path3.GetArcLength(j - 1);
						if (num9 < num6)
						{
							num6 = num9;
							num7 = num8;
						}
					}
					path2 = path3;
					centerRatio = num7 / path2.GetTotalLength();
					isInverted = flag;
				}
			}
			else
			{
				Path randomElement = allSpawnPaths.GetRandomElement<Path>();
				if (randomElement.NumberOfPoints > 1)
				{
					path2 = randomElement;
					centerRatio = 0.37f + MBRandom.RandomFloat * 0.26f;
					isInverted = false;
				}
			}
			return path2;
		}

		// Token: 0x04000941 RID: 2369
		private readonly Mission _mission;

		// Token: 0x04000942 RID: 2370
		private Path _initialPath;

		// Token: 0x04000943 RID: 2371
		private BattleSideSpawnPathSelector[] _battleSideSelectors;
	}
}
