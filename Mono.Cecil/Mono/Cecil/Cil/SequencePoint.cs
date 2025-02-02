using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x0200001C RID: 28
	public sealed class SequencePoint
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00007334 File Offset: 0x00005534
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000733C File Offset: 0x0000553C
		public int StartLine
		{
			get
			{
				return this.start_line;
			}
			set
			{
				this.start_line = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00007345 File Offset: 0x00005545
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000734D File Offset: 0x0000554D
		public int StartColumn
		{
			get
			{
				return this.start_column;
			}
			set
			{
				this.start_column = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00007356 File Offset: 0x00005556
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000735E File Offset: 0x0000555E
		public int EndLine
		{
			get
			{
				return this.end_line;
			}
			set
			{
				this.end_line = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00007367 File Offset: 0x00005567
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000736F File Offset: 0x0000556F
		public int EndColumn
		{
			get
			{
				return this.end_column;
			}
			set
			{
				this.end_column = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00007378 File Offset: 0x00005578
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00007380 File Offset: 0x00005580
		public Document Document
		{
			get
			{
				return this.document;
			}
			set
			{
				this.document = value;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007389 File Offset: 0x00005589
		public SequencePoint(Document document)
		{
			this.document = document;
		}

		// Token: 0x04000258 RID: 600
		private Document document;

		// Token: 0x04000259 RID: 601
		private int start_line;

		// Token: 0x0400025A RID: 602
		private int start_column;

		// Token: 0x0400025B RID: 603
		private int end_line;

		// Token: 0x0400025C RID: 604
		private int end_column;
	}
}
