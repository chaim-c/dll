using System;
using System.Numerics;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000008 RID: 8
	public class RichTextPart
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000044F0 File Offset: 0x000026F0
		// (set) Token: 0x06000062 RID: 98 RVA: 0x000044F8 File Offset: 0x000026F8
		public string Style { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004501 File Offset: 0x00002701
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00004509 File Offset: 0x00002709
		internal TextMeshGenerator TextMeshGenerator { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004512 File Offset: 0x00002712
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000451A File Offset: 0x0000271A
		public DrawObject2D DrawObject2D { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004523 File Offset: 0x00002723
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000452B File Offset: 0x0000272B
		public Font DefaultFont { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004534 File Offset: 0x00002734
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000453C File Offset: 0x0000273C
		public float WordWidth { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004545 File Offset: 0x00002745
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000454D File Offset: 0x0000274D
		public Vector2 PartPosition { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004556 File Offset: 0x00002756
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000455E File Offset: 0x0000275E
		public Sprite Sprite { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004567 File Offset: 0x00002767
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000456F File Offset: 0x0000276F
		public Vector2 SpritePosition { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004578 File Offset: 0x00002778
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00004580 File Offset: 0x00002780
		public RichTextPartType Type { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004589 File Offset: 0x00002789
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00004591 File Offset: 0x00002791
		public float Extend { get; set; }

		// Token: 0x06000075 RID: 117 RVA: 0x0000459A File Offset: 0x0000279A
		internal RichTextPart()
		{
			this.Style = "Default";
		}
	}
}
