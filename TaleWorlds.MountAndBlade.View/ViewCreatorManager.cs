using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View
{
	// Token: 0x0200001B RID: 27
	public static class ViewCreatorManager
	{
		// Token: 0x060000BD RID: 189 RVA: 0x000070DC File Offset: 0x000052DC
		static ViewCreatorManager()
		{
			Assembly[] viewAssemblies = ViewCreatorManager.GetViewAssemblies();
			Assembly assembly = typeof(ViewCreatorModule).Assembly;
			ViewCreatorManager.CheckAssemblyScreens(assembly);
			Assembly[] array = viewAssemblies;
			for (int i = 0; i < array.Length; i++)
			{
				ViewCreatorManager.CheckAssemblyScreens(array[i]);
			}
			ViewCreatorManager.CollectDefaults(assembly);
			array = viewAssemblies;
			for (int i = 0; i < array.Length; i++)
			{
				ViewCreatorManager.CollectDefaults(array[i]);
			}
			array = viewAssemblies;
			for (int i = 0; i < array.Length; i++)
			{
				ViewCreatorManager.CheckOverridenViews(array[i]);
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007174 File Offset: 0x00005374
		private static void CheckAssemblyScreens(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypesSafe(null))
			{
				object[] customAttributesSafe = type.GetCustomAttributesSafe(typeof(ViewCreatorModule), false);
				if (customAttributesSafe != null && customAttributesSafe.Length == 1 && customAttributesSafe[0] is ViewCreatorModule)
				{
					foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
					{
						ViewMethod viewMethod = methodInfo.GetCustomAttributesSafe(typeof(ViewMethod), false)[0] as ViewMethod;
						if (viewMethod != null)
						{
							ViewCreatorManager._viewCreators.Add(viewMethod.Name, methodInfo);
						}
					}
				}
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007240 File Offset: 0x00005440
		private static Assembly[] GetViewAssemblies()
		{
			List<Assembly> list = new List<Assembly>();
			Assembly assembly = typeof(ViewCreatorModule).Assembly;
			foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
			{
				AssemblyName[] referencedAssemblies = assembly2.GetReferencedAssemblies();
				for (int j = 0; j < referencedAssemblies.Length; j++)
				{
					if (referencedAssemblies[j].ToString() == assembly.GetName().ToString())
					{
						list.Add(assembly2);
						break;
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000072CC File Offset: 0x000054CC
		internal static IEnumerable<MissionBehavior> CreateDefaultMissionBehaviors(Mission mission)
		{
			List<MissionBehavior> list = new List<MissionBehavior>();
			foreach (KeyValuePair<Type, Type> keyValuePair in ViewCreatorManager._defaultTypes)
			{
				if (!keyValuePair.Value.IsAbstract)
				{
					MissionBehavior item = Activator.CreateInstance(keyValuePair.Value) as MissionBehavior;
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00007348 File Offset: 0x00005548
		internal static IEnumerable<MissionBehavior> CollectMissionBehaviors(string missionName, Mission mission, IEnumerable<MissionBehavior> behaviors)
		{
			List<MissionBehavior> list = new List<MissionBehavior>();
			if (ViewCreatorManager._viewCreators.ContainsKey(missionName))
			{
				MissionBehavior[] collection = ViewCreatorManager._viewCreators[missionName].Invoke(null, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, new object[]
				{
					mission
				}, null) as MissionBehavior[];
				list.AddRange(collection);
			}
			return behaviors.Concat(list);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000739C File Offset: 0x0000559C
		public static ScreenBase CreateScreenView<T>() where T : ScreenBase, new()
		{
			if (ViewCreatorManager._actualViewTypes.ContainsKey(typeof(T)))
			{
				return Activator.CreateInstance(ViewCreatorManager._actualViewTypes[typeof(T)]) as ScreenBase;
			}
			return Activator.CreateInstance<T>();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000073E8 File Offset: 0x000055E8
		public static ScreenBase CreateScreenView<T>(params object[] parameters) where T : ScreenBase
		{
			Type type = typeof(T);
			if (ViewCreatorManager._actualViewTypes.ContainsKey(typeof(T)))
			{
				type = ViewCreatorManager._actualViewTypes[typeof(T)];
			}
			return Activator.CreateInstance(type, parameters) as ScreenBase;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007438 File Offset: 0x00005638
		public static MissionView CreateMissionView<T>(bool isNetwork = false, Mission mission = null, params object[] parameters) where T : MissionView, new()
		{
			if (ViewCreatorManager._actualViewTypes.ContainsKey(typeof(T)))
			{
				return Activator.CreateInstance(ViewCreatorManager._actualViewTypes[typeof(T)], parameters) as MissionView;
			}
			return Activator.CreateInstance<T>();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007488 File Offset: 0x00005688
		public static MissionView CreateMissionViewWithArgs<T>(params object[] parameters) where T : MissionView
		{
			Type type = typeof(T);
			if (ViewCreatorManager._actualViewTypes.ContainsKey(typeof(T)))
			{
				type = ViewCreatorManager._actualViewTypes[typeof(T)];
			}
			return Activator.CreateInstance(type, parameters) as MissionView;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000074D8 File Offset: 0x000056D8
		private static void CheckOverridenViews(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypesSafe(null))
			{
				if (typeof(MissionView).IsAssignableFrom(type) || typeof(ScreenBase).IsAssignableFrom(type))
				{
					object[] customAttributesSafe = type.GetCustomAttributesSafe(typeof(OverrideView), false);
					if (customAttributesSafe != null && customAttributesSafe.Length == 1)
					{
						OverrideView overrideView = customAttributesSafe[0] as OverrideView;
						if (overrideView != null)
						{
							ViewCreatorManager._actualViewTypes[overrideView.BaseType] = type;
							if (ViewCreatorManager._defaultTypes.ContainsKey(overrideView.BaseType))
							{
								ViewCreatorManager._defaultTypes[overrideView.BaseType] = type;
							}
						}
					}
				}
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000075AC File Offset: 0x000057AC
		private static void CollectDefaults(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypesSafe(null))
			{
				if (typeof(MissionBehavior).IsAssignableFrom(type) && type.GetCustomAttributesSafe(typeof(DefaultView), false).Length == 1)
				{
					ViewCreatorManager._defaultTypes.Add(type, type);
				}
			}
		}

		// Token: 0x04000042 RID: 66
		private static Dictionary<string, MethodInfo> _viewCreators = new Dictionary<string, MethodInfo>();

		// Token: 0x04000043 RID: 67
		private static Dictionary<Type, Type> _actualViewTypes = new Dictionary<Type, Type>();

		// Token: 0x04000044 RID: 68
		private static Dictionary<Type, Type> _defaultTypes = new Dictionary<Type, Type>();
	}
}
