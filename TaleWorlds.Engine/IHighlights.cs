using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000037 RID: 55
	[ApplicationInterfaceBase]
	internal interface IHighlights
	{
		// Token: 0x0600048A RID: 1162
		[EngineMethod("initialize", false)]
		void Initialize();

		// Token: 0x0600048B RID: 1163
		[EngineMethod("open_group", false)]
		void OpenGroup(string id);

		// Token: 0x0600048C RID: 1164
		[EngineMethod("close_group", false)]
		void CloseGroup(string id, bool destroy = false);

		// Token: 0x0600048D RID: 1165
		[EngineMethod("save_screenshot", false)]
		void SaveScreenshot(string highlightId, string groupId);

		// Token: 0x0600048E RID: 1166
		[EngineMethod("save_video", false)]
		void SaveVideo(string highlightId, string groupId, int startDelta, int endDelta);

		// Token: 0x0600048F RID: 1167
		[EngineMethod("open_summary", false)]
		void OpenSummary(string groups);

		// Token: 0x06000490 RID: 1168
		[EngineMethod("add_highlight", false)]
		void AddHighlight(string id, string name);

		// Token: 0x06000491 RID: 1169
		[EngineMethod("remove_highlight", false)]
		void RemoveHighlight(string id);
	}
}
