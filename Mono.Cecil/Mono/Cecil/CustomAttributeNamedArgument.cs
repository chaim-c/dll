using System;

namespace Mono.Cecil
{
	// Token: 0x020000A1 RID: 161
	public struct CustomAttributeNamedArgument
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00017893 File Offset: 0x00015A93
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001789B File Offset: 0x00015A9B
		public CustomAttributeArgument Argument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000178A3 File Offset: 0x00015AA3
		public CustomAttributeNamedArgument(string name, CustomAttributeArgument argument)
		{
			Mixin.CheckName(name);
			this.name = name;
			this.argument = argument;
		}

		// Token: 0x0400041A RID: 1050
		private readonly string name;

		// Token: 0x0400041B RID: 1051
		private readonly CustomAttributeArgument argument;
	}
}
