using System;
using TaleWorlds.Library.EventSystem;

namespace SandBox.ViewModelCollection.Missions.NameMarker
{
	// Token: 0x02000028 RID: 40
	public class MissionNameMarkerToggleEvent : EventBase
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000FE0F File Offset: 0x0000E00F
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000FE17 File Offset: 0x0000E017
		public bool NewState { get; private set; }

		// Token: 0x0600033A RID: 826 RVA: 0x0000FE20 File Offset: 0x0000E020
		public MissionNameMarkerToggleEvent(bool newState)
		{
			this.NewState = newState;
		}
	}
}
