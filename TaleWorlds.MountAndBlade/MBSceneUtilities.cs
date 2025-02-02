using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200024B RID: 587
	public static class MBSceneUtilities
	{
		// Token: 0x06001F7A RID: 8058 RVA: 0x0006F898 File Offset: 0x0006DA98
		public static MBList<Path> GetAllSpawnPaths(Scene scene)
		{
			MBList<Path> mblist = new MBList<Path>();
			for (int i = 0; i < 32; i++)
			{
				string name = "spawn_path_" + i.ToString("D2");
				Path pathWithName = scene.GetPathWithName(name);
				if (pathWithName != null && pathWithName.NumberOfPoints > 1)
				{
					mblist.Add(pathWithName);
				}
			}
			return mblist;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0006F8F4 File Offset: 0x0006DAF4
		public static List<Vec2> GetSceneBoundaryPoints(Scene scene, out string boundaryName)
		{
			List<Vec2> list = new List<Vec2>();
			int softBoundaryVertexCount = scene.GetSoftBoundaryVertexCount();
			if (softBoundaryVertexCount >= 3)
			{
				boundaryName = "walk_area";
				for (int i = 0; i < softBoundaryVertexCount; i++)
				{
					Vec2 softBoundaryVertex = scene.GetSoftBoundaryVertex(i);
					list.Add(softBoundaryVertex);
				}
			}
			else
			{
				boundaryName = "scene_boundary";
				Vec3 vec;
				Vec3 vec2;
				scene.GetBoundingBox(out vec, out vec2);
				float num = MathF.Min(2f, vec2.x - vec.x);
				float num2 = MathF.Min(2f, vec2.y - vec.y);
				List<Vec2> collection = new List<Vec2>
				{
					new Vec2(vec.x + num, vec.y + num2),
					new Vec2(vec2.x - num, vec.y + num2),
					new Vec2(vec2.x - num, vec2.y - num2),
					new Vec2(vec.x + num, vec2.y - num2)
				};
				list.AddRange(collection);
			}
			return list;
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x0006FA0C File Offset: 0x0006DC0C
		[return: TupleElementNames(new string[]
		{
			"tag",
			"boundaryPoints",
			"insideAllowance"
		})]
		public static List<ValueTuple<string, List<Vec2>, bool>> GetDeploymentBoundaries(BattleSideEnum battleSide)
		{
			IEnumerable<GameEntity> enumerable = Mission.Current.Scene.FindEntitiesWithTagExpression("deployment_castle_boundary(_\\d+)*");
			List<ValueTuple<string, List<GameEntity>>> list = new List<ValueTuple<string, List<GameEntity>>>();
			foreach (GameEntity gameEntity in enumerable)
			{
				if (gameEntity.HasTag(battleSide.ToString()))
				{
					string[] tags = gameEntity.Tags;
					for (int i = 0; i < tags.Length; i++)
					{
						string tag = tags[i];
						if (tag.Contains("deployment_castle_boundary"))
						{
							ValueTuple<string, List<GameEntity>> valueTuple = list.FirstOrDefault(([TupleElementNames(new string[]
							{
								"tag",
								"boundaryEntities"
							})] ValueTuple<string, List<GameEntity>> tuple) => tuple.Item1.Equals(tag));
							if (valueTuple.Item1 == null)
							{
								valueTuple = new ValueTuple<string, List<GameEntity>>(tag, new List<GameEntity>());
								list.Add(valueTuple);
							}
							valueTuple.Item2.Add(gameEntity);
							break;
						}
					}
				}
			}
			List<ValueTuple<string, List<Vec2>, bool>> list2 = new List<ValueTuple<string, List<Vec2>, bool>>();
			foreach (ValueTuple<string, List<GameEntity>> valueTuple2 in list)
			{
				string item = valueTuple2.Item1;
				bool item2 = !valueTuple2.Item2.Any((GameEntity e) => e.HasTag("out"));
				List<Vec2> item3 = (from bp in valueTuple2.Item2
				select bp.GlobalPosition.AsVec2).ToList<Vec2>();
				MBSceneUtilities.RadialSortBoundary(ref item3);
				list2.Add(new ValueTuple<string, List<Vec2>, bool>(item, item3, item2));
			}
			return list2;
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x0006FBD0 File Offset: 0x0006DDD0
		public static void ProjectPositionToDeploymentBoundaries(BattleSideEnum side, ref WorldPosition position)
		{
			Mission mission = Mission.Current;
			IMissionDeploymentPlan deploymentPlan = mission.DeploymentPlan;
			if (deploymentPlan.HasDeploymentBoundaries(side))
			{
				IMissionDeploymentPlan missionDeploymentPlan = deploymentPlan;
				Vec2 asVec = position.AsVec2;
				if (!missionDeploymentPlan.IsPositionInsideDeploymentBoundaries(side, asVec))
				{
					IMissionDeploymentPlan missionDeploymentPlan2 = deploymentPlan;
					asVec = position.AsVec2;
					Vec2 closestDeploymentBoundaryPosition = missionDeploymentPlan2.GetClosestDeploymentBoundaryPosition(side, asVec, true, position.GetGroundZ());
					position = new WorldPosition(mission.Scene, new Vec3(closestDeploymentBoundaryPosition, position.GetGroundZ(), -1f));
				}
			}
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x0006FC44 File Offset: 0x0006DE44
		public static void FindConvexHull(ref List<Vec2> boundary)
		{
			Vec2[] array = boundary.ToArray();
			int num = 0;
			MBAPI.IMBMission.FindConvexHull(array, boundary.Count, ref num);
			boundary = array.ToList<Vec2>();
			boundary.RemoveRange(num, boundary.Count - num);
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x0006FC88 File Offset: 0x0006DE88
		public static void RadialSortBoundary(ref List<Vec2> boundary)
		{
			MBSceneUtilities.<>c__DisplayClass12_0 CS$<>8__locals1 = new MBSceneUtilities.<>c__DisplayClass12_0();
			if (boundary.Count == 0)
			{
				return;
			}
			CS$<>8__locals1.boundaryCenter = Vec2.Zero;
			foreach (Vec2 v in boundary)
			{
				CS$<>8__locals1.boundaryCenter += v;
			}
			MBSceneUtilities.<>c__DisplayClass12_0 CS$<>8__locals2 = CS$<>8__locals1;
			CS$<>8__locals2.boundaryCenter.x = CS$<>8__locals2.boundaryCenter.x / (float)boundary.Count;
			MBSceneUtilities.<>c__DisplayClass12_0 CS$<>8__locals3 = CS$<>8__locals1;
			CS$<>8__locals3.boundaryCenter.y = CS$<>8__locals3.boundaryCenter.y / (float)boundary.Count;
			boundary = (from b in boundary
			orderby (b - CS$<>8__locals1.boundaryCenter).RotationInRadians
			select b).ToList<Vec2>();
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x0006FD48 File Offset: 0x0006DF48
		public static bool IsPointInsideBoundaries(in Vec2 point, List<Vec2> boundaries, float acceptanceThreshold = 0.05f)
		{
			if (boundaries.Count <= 2)
			{
				return false;
			}
			acceptanceThreshold = MathF.Max(0f, acceptanceThreshold);
			bool result = true;
			for (int i = 0; i < boundaries.Count; i++)
			{
				Vec2 v = boundaries[i];
				Vec2 vec = boundaries[(i + 1) % boundaries.Count] - v;
				Vec2 vec2 = point - v;
				if (vec.x * vec2.y - vec.y * vec2.x < 0f)
				{
					vec.Normalize();
					Vec2 v2 = vec2.DotProduct(vec) * vec;
					if ((vec2 - v2).LengthSquared > acceptanceThreshold * acceptanceThreshold)
					{
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x0006FE0C File Offset: 0x0006E00C
		public static float FindClosestPointToBoundaries(in Vec2 position, List<Vec2> boundaries, out Vec2 closestPoint)
		{
			closestPoint = position;
			float num = float.MaxValue;
			for (int i = 0; i < boundaries.Count; i++)
			{
				Vec2 segmentA = boundaries[i];
				Vec2 segmentB = boundaries[(i + 1) % boundaries.Count];
				Vec2 vec;
				float closestPointOnLineSegment = MBMath.GetClosestPointOnLineSegment(position, segmentA, segmentB, out vec);
				if (closestPointOnLineSegment <= num)
				{
					num = closestPointOnLineSegment;
					closestPoint = vec;
				}
			}
			return num;
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x0006FE78 File Offset: 0x0006E078
		public static float FindClosestPointWithNavMeshToBoundaries(in Vec2 position, float positionZ, List<Vec2> boundaries, out Vec2 closestPoint)
		{
			closestPoint = position;
			float num = float.MaxValue;
			for (int i = 0; i < boundaries.Count; i++)
			{
				Vec2 vec = boundaries[i];
				Vec2 vec2 = boundaries[(i + 1) % boundaries.Count];
				Vec2 vec3;
				float num2 = MBMath.GetClosestPointOnLineSegment(position, vec, vec2, out vec3);
				if (num2 <= num)
				{
					Vec2 v = (vec2 - vec).Normalized() * 1f;
					WorldPosition worldPosition = new WorldPosition(Mission.Current.Scene, new Vec3(vec3, positionZ, -1f));
					int num3 = 0;
					while (worldPosition.GetNavMesh() == UIntPtr.Zero && num3 < 30)
					{
						Vec2 v2 = (float)((num3 / 2 + 1) * ((num3++ % 2 == 0) ? 1 : -1)) * v;
						Vec2 vec4 = vec3 + v2;
						if (vec4.X > MathF.Min(vec.X, vec2.X) && vec4.X < MathF.Max(vec.X, vec2.X) && vec4.Y > MathF.Min(vec.Y, vec2.Y) && vec4.Y < MathF.Max(vec.Y, vec2.Y))
						{
							worldPosition.SetVec2(vec3 + v2);
						}
					}
					bool flag = worldPosition.GetNavMesh() != UIntPtr.Zero;
					if (flag)
					{
						num2 = worldPosition.AsVec2.Distance(position);
					}
					if (num2 <= num)
					{
						num = num2;
						closestPoint = (flag ? worldPosition.AsVec2 : vec3);
					}
				}
			}
			return num;
		}

		// Token: 0x04000BA3 RID: 2979
		public const int MaxNumberOfSpawnPaths = 32;

		// Token: 0x04000BA4 RID: 2980
		public const string SpawnPathPrefix = "spawn_path_";

		// Token: 0x04000BA5 RID: 2981
		public const string SoftBorderVertexTag = "walk_area_vertex";

		// Token: 0x04000BA6 RID: 2982
		public const string SoftBoundaryName = "walk_area";

		// Token: 0x04000BA7 RID: 2983
		public const string SceneBoundaryName = "scene_boundary";

		// Token: 0x04000BA8 RID: 2984
		private const string DeploymentBoundaryTag = "deployment_castle_boundary";

		// Token: 0x04000BA9 RID: 2985
		private const string DeploymentBoundaryTagExpression = "deployment_castle_boundary(_\\d+)*";
	}
}
