using System;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200002E RID: 46
	public class AudioProperty
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		[Editor(false)]
		public string AudioName { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000E6BD File Offset: 0x0000C8BD
		// (set) Token: 0x06000331 RID: 817 RVA: 0x0000E6C5 File Offset: 0x0000C8C5
		[Editor(false)]
		public bool Delay { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000E6CE File Offset: 0x0000C8CE
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000E6D6 File Offset: 0x0000C8D6
		[Editor(false)]
		public float DelaySeconds { get; set; }

		// Token: 0x06000334 RID: 820 RVA: 0x0000E6DF File Offset: 0x0000C8DF
		public void FillFrom(AudioProperty audioProperty)
		{
			this.AudioName = audioProperty.AudioName;
			this.Delay = audioProperty.Delay;
			this.DelaySeconds = audioProperty.DelaySeconds;
		}
	}
}
