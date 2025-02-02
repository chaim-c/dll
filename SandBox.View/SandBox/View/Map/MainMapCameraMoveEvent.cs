using System;
using TaleWorlds.Library.EventSystem;

namespace SandBox.View.Map
{
	// Token: 0x0200004E RID: 78
	public class MainMapCameraMoveEvent : EventBase
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0001C4EE File Offset: 0x0001A6EE
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0001C4F6 File Offset: 0x0001A6F6
		public bool RotationChanged { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0001C4FF File Offset: 0x0001A6FF
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0001C507 File Offset: 0x0001A707
		public bool PositionChanged { get; private set; }

		// Token: 0x06000371 RID: 881 RVA: 0x0001C510 File Offset: 0x0001A710
		public MainMapCameraMoveEvent(bool rotationChanged, bool positionChanged)
		{
			this.RotationChanged = rotationChanged;
			this.PositionChanged = positionChanged;
		}
	}
}
