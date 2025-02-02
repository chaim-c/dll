using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x0200000C RID: 12
	public sealed class ExceptionHandler
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000451A File Offset: 0x0000271A
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00004522 File Offset: 0x00002722
		public Instruction TryStart
		{
			get
			{
				return this.try_start;
			}
			set
			{
				this.try_start = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000452B File Offset: 0x0000272B
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00004533 File Offset: 0x00002733
		public Instruction TryEnd
		{
			get
			{
				return this.try_end;
			}
			set
			{
				this.try_end = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000453C File Offset: 0x0000273C
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00004544 File Offset: 0x00002744
		public Instruction FilterStart
		{
			get
			{
				return this.filter_start;
			}
			set
			{
				this.filter_start = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000454D File Offset: 0x0000274D
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00004555 File Offset: 0x00002755
		public Instruction HandlerStart
		{
			get
			{
				return this.handler_start;
			}
			set
			{
				this.handler_start = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000455E File Offset: 0x0000275E
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00004566 File Offset: 0x00002766
		public Instruction HandlerEnd
		{
			get
			{
				return this.handler_end;
			}
			set
			{
				this.handler_end = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000456F File Offset: 0x0000276F
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00004577 File Offset: 0x00002777
		public TypeReference CatchType
		{
			get
			{
				return this.catch_type;
			}
			set
			{
				this.catch_type = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004580 File Offset: 0x00002780
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004588 File Offset: 0x00002788
		public ExceptionHandlerType HandlerType
		{
			get
			{
				return this.handler_type;
			}
			set
			{
				this.handler_type = value;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004591 File Offset: 0x00002791
		public ExceptionHandler(ExceptionHandlerType handlerType)
		{
			this.handler_type = handlerType;
		}

		// Token: 0x0400010E RID: 270
		private Instruction try_start;

		// Token: 0x0400010F RID: 271
		private Instruction try_end;

		// Token: 0x04000110 RID: 272
		private Instruction filter_start;

		// Token: 0x04000111 RID: 273
		private Instruction handler_start;

		// Token: 0x04000112 RID: 274
		private Instruction handler_end;

		// Token: 0x04000113 RID: 275
		private TypeReference catch_type;

		// Token: 0x04000114 RID: 276
		private ExceptionHandlerType handler_type;
	}
}
