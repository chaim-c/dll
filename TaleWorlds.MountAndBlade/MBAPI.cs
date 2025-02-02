using System;
using System.Collections.Generic;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200019D RID: 413
	public static class MBAPI
	{
		// Token: 0x06001565 RID: 5477 RVA: 0x0004FDB8 File Offset: 0x0004DFB8
		private static T GetObject<T>() where T : class
		{
			object obj;
			if (MBAPI._objects.TryGetValue(typeof(T).FullName, out obj))
			{
				return obj as T;
			}
			return default(T);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
		internal static void SetObjects(Dictionary<string, object> objects)
		{
			MBAPI._objects = objects;
			MBAPI.IMBTestRun = MBAPI.GetObject<IMBTestRun>();
			MBAPI.IMBActionSet = MBAPI.GetObject<IMBActionSet>();
			MBAPI.IMBAgent = MBAPI.GetObject<IMBAgent>();
			MBAPI.IMBAnimation = MBAPI.GetObject<IMBAnimation>();
			MBAPI.IMBDelegate = MBAPI.GetObject<IMBDelegate>();
			MBAPI.IMBItem = MBAPI.GetObject<IMBItem>();
			MBAPI.IMBEditor = MBAPI.GetObject<IMBEditor>();
			MBAPI.IMBMission = MBAPI.GetObject<IMBMission>();
			MBAPI.IMBMultiplayerData = MBAPI.GetObject<IMBMultiplayerData>();
			MBAPI.IMouseManager = MBAPI.GetObject<IMouseManager>();
			MBAPI.IMBNetwork = MBAPI.GetObject<IMBNetwork>();
			MBAPI.IMBPeer = MBAPI.GetObject<IMBPeer>();
			MBAPI.IMBSkeletonExtensions = MBAPI.GetObject<IMBSkeletonExtensions>();
			MBAPI.IMBGameEntityExtensions = MBAPI.GetObject<IMBGameEntityExtensions>();
			MBAPI.IMBScreen = MBAPI.GetObject<IMBScreen>();
			MBAPI.IMBSoundEvent = MBAPI.GetObject<IMBSoundEvent>();
			MBAPI.IMBVoiceManager = MBAPI.GetObject<IMBVoiceManager>();
			MBAPI.IMBTeam = MBAPI.GetObject<IMBTeam>();
			MBAPI.IMBWorld = MBAPI.GetObject<IMBWorld>();
			MBAPI.IInput = MBAPI.GetObject<IInput>();
			MBAPI.IMBMessageManager = MBAPI.GetObject<IMBMessageManager>();
			MBAPI.IMBWindowManager = MBAPI.GetObject<IMBWindowManager>();
			MBAPI.IMBDebugExtensions = MBAPI.GetObject<IMBDebugExtensions>();
			MBAPI.IMBGame = MBAPI.GetObject<IMBGame>();
			MBAPI.IMBFaceGen = MBAPI.GetObject<IMBFaceGen>();
			MBAPI.IMBMapScene = MBAPI.GetObject<IMBMapScene>();
			MBAPI.IMBBannerlordChecker = MBAPI.GetObject<IMBBannerlordChecker>();
			MBAPI.IMBAgentVisuals = MBAPI.GetObject<IMBAgentVisuals>();
			MBAPI.IMBBannerlordTableauManager = MBAPI.GetObject<IMBBannerlordTableauManager>();
			MBAPI.IMBBannerlordConfig = MBAPI.GetObject<IMBBannerlordConfig>();
		}

		// Token: 0x04000755 RID: 1877
		internal static IMBTestRun IMBTestRun;

		// Token: 0x04000756 RID: 1878
		internal static IMBActionSet IMBActionSet;

		// Token: 0x04000757 RID: 1879
		internal static IMBAgent IMBAgent;

		// Token: 0x04000758 RID: 1880
		internal static IMBAgentVisuals IMBAgentVisuals;

		// Token: 0x04000759 RID: 1881
		internal static IMBAnimation IMBAnimation;

		// Token: 0x0400075A RID: 1882
		internal static IMBDelegate IMBDelegate;

		// Token: 0x0400075B RID: 1883
		internal static IMBItem IMBItem;

		// Token: 0x0400075C RID: 1884
		internal static IMBEditor IMBEditor;

		// Token: 0x0400075D RID: 1885
		internal static IMBMission IMBMission;

		// Token: 0x0400075E RID: 1886
		internal static IMBMultiplayerData IMBMultiplayerData;

		// Token: 0x0400075F RID: 1887
		internal static IMouseManager IMouseManager;

		// Token: 0x04000760 RID: 1888
		internal static IMBNetwork IMBNetwork;

		// Token: 0x04000761 RID: 1889
		internal static IMBPeer IMBPeer;

		// Token: 0x04000762 RID: 1890
		internal static IMBSkeletonExtensions IMBSkeletonExtensions;

		// Token: 0x04000763 RID: 1891
		internal static IMBGameEntityExtensions IMBGameEntityExtensions;

		// Token: 0x04000764 RID: 1892
		internal static IMBScreen IMBScreen;

		// Token: 0x04000765 RID: 1893
		internal static IMBSoundEvent IMBSoundEvent;

		// Token: 0x04000766 RID: 1894
		internal static IMBVoiceManager IMBVoiceManager;

		// Token: 0x04000767 RID: 1895
		internal static IMBTeam IMBTeam;

		// Token: 0x04000768 RID: 1896
		internal static IMBWorld IMBWorld;

		// Token: 0x04000769 RID: 1897
		internal static IInput IInput;

		// Token: 0x0400076A RID: 1898
		internal static IMBMessageManager IMBMessageManager;

		// Token: 0x0400076B RID: 1899
		internal static IMBWindowManager IMBWindowManager;

		// Token: 0x0400076C RID: 1900
		internal static IMBDebugExtensions IMBDebugExtensions;

		// Token: 0x0400076D RID: 1901
		internal static IMBGame IMBGame;

		// Token: 0x0400076E RID: 1902
		internal static IMBFaceGen IMBFaceGen;

		// Token: 0x0400076F RID: 1903
		internal static IMBMapScene IMBMapScene;

		// Token: 0x04000770 RID: 1904
		internal static IMBBannerlordChecker IMBBannerlordChecker;

		// Token: 0x04000771 RID: 1905
		internal static IMBBannerlordTableauManager IMBBannerlordTableauManager;

		// Token: 0x04000772 RID: 1906
		internal static IMBBannerlordConfig IMBBannerlordConfig;

		// Token: 0x04000773 RID: 1907
		private static Dictionary<string, object> _objects;
	}
}
