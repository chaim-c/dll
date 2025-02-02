using System;

namespace Mono.Cecil
{
	// Token: 0x020000A0 RID: 160
	public struct CustomAttributeArgument
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001786D File Offset: 0x00015A6D
		public TypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00017875 File Offset: 0x00015A75
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001787D File Offset: 0x00015A7D
		public CustomAttributeArgument(TypeReference type, object value)
		{
			Mixin.CheckType(type);
			this.type = type;
			this.value = value;
		}

		// Token: 0x04000418 RID: 1048
		private readonly TypeReference type;

		// Token: 0x04000419 RID: 1049
		private readonly object value;
	}
}
