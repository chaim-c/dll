using System;
using System.Collections.Generic;

namespace TaleWorlds.Engine
{
	// Token: 0x0200004D RID: 77
	public class Highlights
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x00004EC9 File Offset: 0x000030C9
		public static void Initialize()
		{
			EngineApplicationInterface.IHighlights.Initialize();
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00004ED5 File Offset: 0x000030D5
		public static void OpenGroup(string id)
		{
			EngineApplicationInterface.IHighlights.OpenGroup(id);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00004EE2 File Offset: 0x000030E2
		public static void CloseGroup(string id, bool destroy = false)
		{
			EngineApplicationInterface.IHighlights.CloseGroup(id, destroy);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00004EF0 File Offset: 0x000030F0
		public static void SaveScreenshot(string highlightId, string groupId)
		{
			EngineApplicationInterface.IHighlights.SaveScreenshot(highlightId, groupId);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00004EFE File Offset: 0x000030FE
		public static void SaveVideo(string highlightId, string groupId, int startDelta, int endDelta)
		{
			EngineApplicationInterface.IHighlights.SaveVideo(highlightId, groupId, startDelta, endDelta);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00004F10 File Offset: 0x00003110
		public static void OpenSummary(List<string> groups)
		{
			string groups2 = string.Join("::", groups);
			EngineApplicationInterface.IHighlights.OpenSummary(groups2);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00004F34 File Offset: 0x00003134
		public static void AddHighlight(string id, string name)
		{
			EngineApplicationInterface.IHighlights.AddHighlight(id, name);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00004F42 File Offset: 0x00003142
		public static void RemoveHighlight(string id)
		{
			EngineApplicationInterface.IHighlights.RemoveHighlight(id);
		}

		// Token: 0x020000B6 RID: 182
		public enum Significance
		{
			// Token: 0x04000380 RID: 896
			None,
			// Token: 0x04000381 RID: 897
			ExtremelyBad,
			// Token: 0x04000382 RID: 898
			VeryBad,
			// Token: 0x04000383 RID: 899
			Bad = 4,
			// Token: 0x04000384 RID: 900
			Neutral = 16,
			// Token: 0x04000385 RID: 901
			Good = 256,
			// Token: 0x04000386 RID: 902
			VeryGood = 512,
			// Token: 0x04000387 RID: 903
			ExtremelyGoods = 1024,
			// Token: 0x04000388 RID: 904
			Max = 2048
		}

		// Token: 0x020000B7 RID: 183
		public enum Type
		{
			// Token: 0x0400038A RID: 906
			None,
			// Token: 0x0400038B RID: 907
			Milestone,
			// Token: 0x0400038C RID: 908
			Achievement,
			// Token: 0x0400038D RID: 909
			Incident = 4,
			// Token: 0x0400038E RID: 910
			StateChange = 8,
			// Token: 0x0400038F RID: 911
			Unannounced = 16,
			// Token: 0x04000390 RID: 912
			Max = 32
		}
	}
}
