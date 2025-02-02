using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.Core.ViewModelCollection.Tutorial
{
	// Token: 0x02000010 RID: 16
	public class TutorialNotificationElementChangeEvent : EventBase
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003502 File Offset: 0x00001702
		// (set) Token: 0x060000CA RID: 202 RVA: 0x0000350A File Offset: 0x0000170A
		public string NewNotificationElementID { get; private set; }

		// Token: 0x060000CB RID: 203 RVA: 0x00003513 File Offset: 0x00001713
		public TutorialNotificationElementChangeEvent(string newNotificationElementID)
		{
			this.NewNotificationElementID = (newNotificationElementID ?? string.Empty);
		}
	}
}
