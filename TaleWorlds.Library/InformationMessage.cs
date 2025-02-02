using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000039 RID: 57
	public class InformationMessage
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007245 File Offset: 0x00005445
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000724D File Offset: 0x0000544D
		public string Information { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00007256 File Offset: 0x00005456
		// (set) Token: 0x060001DF RID: 479 RVA: 0x0000725E File Offset: 0x0000545E
		public string Detail { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00007267 File Offset: 0x00005467
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x0000726F File Offset: 0x0000546F
		public Color Color { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007278 File Offset: 0x00005478
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00007280 File Offset: 0x00005480
		public string SoundEventPath { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00007289 File Offset: 0x00005489
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00007291 File Offset: 0x00005491
		public string Category { get; set; }

		// Token: 0x060001E6 RID: 486 RVA: 0x0000729A File Offset: 0x0000549A
		public InformationMessage(string information)
		{
			this.Information = information;
			this.Color = Color.White;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000072B4 File Offset: 0x000054B4
		public InformationMessage(string information, Color color)
		{
			this.Information = information;
			this.Color = color;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000072CA File Offset: 0x000054CA
		public InformationMessage(string information, Color color, string category)
		{
			this.Information = information;
			this.Color = color;
			this.Category = category;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000072E7 File Offset: 0x000054E7
		public InformationMessage(string information, string soundEventPath)
		{
			this.Information = information;
			this.SoundEventPath = soundEventPath;
			this.Color = Color.White;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007308 File Offset: 0x00005508
		public InformationMessage()
		{
			this.Information = "";
		}
	}
}
