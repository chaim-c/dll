using System;
using System.Collections.Generic;
using System.Reflection;
using SandBox.View.Map;
using SandBox.View.Menu;
using SandBox.View.Missions;
using SandBox.View.Missions.Tournaments;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.ScreenSystem;

namespace SandBox.View
{
	// Token: 0x0200000B RID: 11
	public static class SandBoxViewCreator
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000042EC File Offset: 0x000024EC
		static SandBoxViewCreator()
		{
			Assembly[] viewAssemblies = SandBoxViewCreator.GetViewAssemblies();
			Assembly assembly = typeof(ViewCreatorModule).Assembly;
			Assembly[] array = viewAssemblies;
			for (int i = 0; i < array.Length; i++)
			{
				SandBoxViewCreator.CheckOverridenViews(array[i]);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004330 File Offset: 0x00002530
		private static void CheckOverridenViews(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypesSafe(null))
			{
				if (typeof(MapView).IsAssignableFrom(type) || typeof(MenuView).IsAssignableFrom(type) || typeof(MissionView).IsAssignableFrom(type) || typeof(ScreenBase).IsAssignableFrom(type))
				{
					object[] customAttributesSafe = type.GetCustomAttributesSafe(typeof(OverrideView), false);
					if (customAttributesSafe != null && customAttributesSafe.Length == 1)
					{
						OverrideView overrideView = customAttributesSafe[0] as OverrideView;
						if (overrideView != null)
						{
							if (!SandBoxViewCreator._actualViewTypes.ContainsKey(overrideView.BaseType))
							{
								SandBoxViewCreator._actualViewTypes.Add(overrideView.BaseType, type);
							}
							else
							{
								SandBoxViewCreator._actualViewTypes[overrideView.BaseType] = type;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004428 File Offset: 0x00002628
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

		// Token: 0x0600003D RID: 61 RVA: 0x000044B1 File Offset: 0x000026B1
		public static ScreenBase CreateSaveLoadScreen(bool isSaving)
		{
			return ViewCreatorManager.CreateScreenView<SaveLoadScreen>(new object[]
			{
				isSaving
			});
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000044C7 File Offset: 0x000026C7
		public static MissionView CreateMissionCraftingView()
		{
			return null;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000044CA File Offset: 0x000026CA
		public static MissionView CreateMissionNameMarkerUIHandler(Mission mission = null)
		{
			return ViewCreatorManager.CreateMissionView<MissionNameMarkerUIHandler>(mission != null, mission, Array.Empty<object>());
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000044DB File Offset: 0x000026DB
		public static MissionView CreateMissionConversationView(Mission mission)
		{
			return ViewCreatorManager.CreateMissionView<MissionConversationView>(true, mission, Array.Empty<object>());
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000044E9 File Offset: 0x000026E9
		public static MissionView CreateMissionBarterView()
		{
			return ViewCreatorManager.CreateMissionView<BarterView>(false, null, Array.Empty<object>());
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000044F7 File Offset: 0x000026F7
		public static MissionView CreateMissionTournamentView()
		{
			return ViewCreatorManager.CreateMissionView<MissionTournamentView>(false, null, Array.Empty<object>());
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004508 File Offset: 0x00002708
		public static MapView CreateMapView<T>(params object[] parameters) where T : MapView
		{
			Type type = typeof(T);
			if (SandBoxViewCreator._actualViewTypes.ContainsKey(typeof(T)))
			{
				type = SandBoxViewCreator._actualViewTypes[typeof(T)];
			}
			return Activator.CreateInstance(type, parameters) as MapView;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004558 File Offset: 0x00002758
		public static MenuView CreateMenuView<T>(params object[] parameters) where T : MenuView
		{
			Type type = typeof(T);
			if (SandBoxViewCreator._actualViewTypes.ContainsKey(typeof(T)))
			{
				type = SandBoxViewCreator._actualViewTypes[typeof(T)];
			}
			return Activator.CreateInstance(type, parameters) as MenuView;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000045A7 File Offset: 0x000027A7
		public static MissionView CreateBoardGameView()
		{
			return ViewCreatorManager.CreateMissionView<BoardGameView>(false, null, Array.Empty<object>());
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000045B5 File Offset: 0x000027B5
		public static MissionView CreateMissionAmbushDeploymentView()
		{
			return ViewCreatorManager.CreateMissionView<MissionAmbushDeploymentView>(false, null, Array.Empty<object>());
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000045C3 File Offset: 0x000027C3
		public static MissionView CreateMissionArenaPracticeFightView()
		{
			return ViewCreatorManager.CreateMissionView<MissionArenaPracticeFightView>(false, null, Array.Empty<object>());
		}

		// Token: 0x04000012 RID: 18
		private static Dictionary<Type, Type> _actualViewTypes = new Dictionary<Type, Type>();
	}
}
