using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000310 RID: 784
	[AttributeUsage(AttributeTargets.Field)]
	public class NotificationProperty : Attribute
	{
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x000A53D5 File Offset: 0x000A35D5
		// (set) Token: 0x06002A9F RID: 10911 RVA: 0x000A53DD File Offset: 0x000A35DD
		public string StringId { get; private set; }

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x000A53E6 File Offset: 0x000A35E6
		// (set) Token: 0x06002AA1 RID: 10913 RVA: 0x000A53EE File Offset: 0x000A35EE
		public string SoundIdOne { get; private set; }

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x000A53F7 File Offset: 0x000A35F7
		// (set) Token: 0x06002AA3 RID: 10915 RVA: 0x000A53FF File Offset: 0x000A35FF
		public string SoundIdTwo { get; private set; }

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000A5408 File Offset: 0x000A3608
		public NotificationProperty(string stringId, string soundIdOne, string soundIdTwo = "")
		{
			this.StringId = stringId;
			this.SoundIdOne = soundIdOne;
			this.SoundIdTwo = soundIdTwo;
		}
	}
}
