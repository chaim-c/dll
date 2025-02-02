using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000027 RID: 39
	public abstract class VariableReference
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000765B File Offset: 0x0000585B
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00007663 File Offset: 0x00005863
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000766C File Offset: 0x0000586C
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00007674 File Offset: 0x00005874
		public TypeReference VariableType
		{
			get
			{
				return this.variable_type;
			}
			set
			{
				this.variable_type = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000767D File Offset: 0x0000587D
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007685 File Offset: 0x00005885
		internal VariableReference(TypeReference variable_type) : this(string.Empty, variable_type)
		{
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007693 File Offset: 0x00005893
		internal VariableReference(string name, TypeReference variable_type)
		{
			this.name = name;
			this.variable_type = variable_type;
		}

		// Token: 0x06000177 RID: 375
		public abstract VariableDefinition Resolve();

		// Token: 0x06000178 RID: 376 RVA: 0x000076B0 File Offset: 0x000058B0
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.name))
			{
				return this.name;
			}
			if (this.index >= 0)
			{
				return "V_" + this.index;
			}
			return string.Empty;
		}

		// Token: 0x04000274 RID: 628
		private string name;

		// Token: 0x04000275 RID: 629
		internal int index = -1;

		// Token: 0x04000276 RID: 630
		protected TypeReference variable_type;
	}
}
