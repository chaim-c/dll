using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200013C RID: 316
	public static class MBExtensions
	{
		// Token: 0x06000F19 RID: 3865 RVA: 0x0002A9A0 File Offset: 0x00028BA0
		private static Vec2 GetGlobalOrganicDirectionAux(ColumnFormation columnFormation, int depthCount = -1)
		{
			IEnumerable<Agent> unitsAtVanguardFile = columnFormation.GetUnitsAtVanguardFile<Agent>();
			Vec2 vec = Vec2.Zero;
			int num = 0;
			Agent agent = null;
			foreach (Agent agent2 in unitsAtVanguardFile)
			{
				if (agent != null)
				{
					Vec2 v = (agent.Position - agent2.Position).AsVec2.Normalized();
					vec += v;
					num++;
				}
				agent = agent2;
				if (depthCount > 0 && num >= depthCount)
				{
					break;
				}
			}
			if (num == 0)
			{
				return Vec2.Invalid;
			}
			return vec * (1f / (float)num);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0002AA4C File Offset: 0x00028C4C
		public static Vec2 GetGlobalOrganicDirection(this ColumnFormation columnFormation)
		{
			return MBExtensions.GetGlobalOrganicDirectionAux(columnFormation, -1);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0002AA55 File Offset: 0x00028C55
		public static Vec2 GetGlobalHeadDirection(this ColumnFormation columnFormation)
		{
			return MBExtensions.GetGlobalOrganicDirectionAux(columnFormation, 3);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0002AA5E File Offset: 0x00028C5E
		public static IEnumerable<T> FindAllWithType<T>(this IEnumerable<GameEntity> entities) where T : ScriptComponentBehavior
		{
			return entities.SelectMany((GameEntity e) => e.GetScriptComponents<T>());
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0002AA88 File Offset: 0x00028C88
		public static IEnumerable<T> FindAllWithType<T>(this IEnumerable<MissionObject> missionObjects) where T : MissionObject
		{
			return from e in missionObjects
			where e != null && e is T
			select e as T;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		public static List<GameEntity> FindAllWithCompatibleType(this IEnumerable<GameEntity> sceneProps, params Type[] types)
		{
			List<GameEntity> list = new List<GameEntity>();
			foreach (GameEntity gameEntity in sceneProps)
			{
				foreach (ScriptComponentBehavior scriptComponentBehavior in gameEntity.GetScriptComponents())
				{
					Type type = scriptComponentBehavior.GetType();
					for (int i = 0; i < types.Length; i++)
					{
						if (types[i].IsAssignableFrom(type))
						{
							list.Add(gameEntity);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0002AB90 File Offset: 0x00028D90
		public static List<MissionObject> FindAllWithCompatibleType(this IEnumerable<MissionObject> missionObjects, params Type[] types)
		{
			List<MissionObject> list = new List<MissionObject>();
			foreach (MissionObject missionObject in missionObjects)
			{
				if (missionObject != null)
				{
					Type type = missionObject.GetType();
					for (int i = 0; i < types.Length; i++)
					{
						if (types[i].IsAssignableFrom(type))
						{
							list.Add(missionObject);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0002AC0C File Offset: 0x00028E0C
		private static void CollectObjectsAux<T>(GameEntity entity, MBList<T> list) where T : ScriptComponentBehavior
		{
			IEnumerable<T> scriptComponents = entity.GetScriptComponents<T>();
			list.AddRange(scriptComponents);
			foreach (GameEntity entity2 in entity.GetChildren())
			{
				MBExtensions.CollectObjectsAux<T>(entity2, list);
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0002AC68 File Offset: 0x00028E68
		public static MBList<T> CollectObjects<T>(this GameEntity entity) where T : ScriptComponentBehavior
		{
			MBList<T> mblist = new MBList<T>();
			MBExtensions.CollectObjectsAux<T>(entity, mblist);
			return mblist;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0002AC84 File Offset: 0x00028E84
		public static List<T> CollectObjectsWithTag<T>(this GameEntity entity, string tag) where T : ScriptComponentBehavior
		{
			List<T> list = new List<T>();
			foreach (GameEntity gameEntity in entity.GetChildren())
			{
				if (gameEntity.HasTag(tag))
				{
					IEnumerable<T> scriptComponents = gameEntity.GetScriptComponents<T>();
					list.AddRange(scriptComponents);
				}
				if (gameEntity.ChildCount > 0)
				{
					list.AddRange(gameEntity.CollectObjectsWithTag(tag));
				}
			}
			return list;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0002AD00 File Offset: 0x00028F00
		public static List<GameEntity> CollectChildrenEntitiesWithTag(this GameEntity entity, string tag)
		{
			List<GameEntity> list = new List<GameEntity>();
			foreach (GameEntity gameEntity in entity.GetChildren())
			{
				if (gameEntity.HasTag(tag))
				{
					list.Add(gameEntity);
				}
				if (gameEntity.ChildCount > 0)
				{
					list.AddRange(gameEntity.CollectChildrenEntitiesWithTag(tag));
				}
			}
			return list;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0002AD74 File Offset: 0x00028F74
		public static GameEntity GetFirstChildEntityWithTag(this GameEntity entity, string tag)
		{
			foreach (GameEntity gameEntity in entity.GetChildren())
			{
				if (gameEntity.HasTag(tag))
				{
					return gameEntity;
				}
			}
			return null;
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0002ADCC File Offset: 0x00028FCC
		public static T GetFirstScriptInFamilyDescending<T>(this GameEntity entity) where T : ScriptComponentBehavior
		{
			T t = entity.GetFirstScriptOfType<T>();
			if (t != null)
			{
				return t;
			}
			foreach (GameEntity entity2 in entity.GetChildren())
			{
				t = entity2.GetFirstScriptInFamilyDescending<T>();
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0002AE40 File Offset: 0x00029040
		public static bool HasParentOfType(this GameEntity e, Type t)
		{
			for (;;)
			{
				e = e.Parent;
				if (e.GetScriptComponents().Any((ScriptComponentBehavior sc) => sc.GetType() == t))
				{
					break;
				}
				if (!(e != null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0002AE88 File Offset: 0x00029088
		public static TSource ElementAtOrValue<TSource>(this IEnumerable<TSource> source, int index, TSource value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (index >= 0)
			{
				IList<TSource> list = source as IList<TSource>;
				if (list == null)
				{
					using (IEnumerator<TSource> enumerator = source.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (index == 0)
							{
								return enumerator.Current;
							}
							index--;
						}
					}
					return value;
				}
				if (index < list.Count)
				{
					return list[index];
				}
			}
			return value;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0002AF04 File Offset: 0x00029104
		public static bool IsOpponentOf(this BattleSideEnum s, BattleSideEnum side)
		{
			return (s == BattleSideEnum.Attacker && side == BattleSideEnum.Defender) || (s == BattleSideEnum.Defender && side == BattleSideEnum.Attacker);
		}
	}
}
