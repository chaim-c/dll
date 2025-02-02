using System;

namespace SandBox.View.Map
{
	// Token: 0x0200003F RID: 63
	public class MapConversationView : MapView
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00015455 File Offset: 0x00013655
		protected internal override void OnFinalize()
		{
			base.OnFinalize();
			this.ConversationMission.OnFinalize();
			this.ConversationMission = null;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0001546F File Offset: 0x0001366F
		protected void CreateConversationMission()
		{
			this.ConversationMission = new MapConversationMission();
		}

		// Token: 0x04000139 RID: 313
		public MapConversationMission ConversationMission;
	}
}
