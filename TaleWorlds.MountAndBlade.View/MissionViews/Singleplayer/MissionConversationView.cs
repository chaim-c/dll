using System;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x02000066 RID: 102
	public class MissionConversationView : MissionView
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00022950 File Offset: 0x00020B50
		public static MissionConversationView Current
		{
			get
			{
				return Mission.Current.GetMissionBehavior<MissionConversationView>();
			}
		}
	}
}
