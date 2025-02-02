using System;

namespace Mono.Cecil
{
	// Token: 0x020000B2 RID: 178
	public sealed class ArrayMarshalInfo : MarshalInfo
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001872D File Offset: 0x0001692D
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x00018735 File Offset: 0x00016935
		public NativeType ElementType
		{
			get
			{
				return this.element_type;
			}
			set
			{
				this.element_type = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001873E File Offset: 0x0001693E
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x00018746 File Offset: 0x00016946
		public int SizeParameterIndex
		{
			get
			{
				return this.size_parameter_index;
			}
			set
			{
				this.size_parameter_index = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0001874F File Offset: 0x0001694F
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x00018757 File Offset: 0x00016957
		public int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00018760 File Offset: 0x00016960
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x00018768 File Offset: 0x00016968
		public int SizeParameterMultiplier
		{
			get
			{
				return this.size_parameter_multiplier;
			}
			set
			{
				this.size_parameter_multiplier = value;
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00018771 File Offset: 0x00016971
		public ArrayMarshalInfo() : base(NativeType.Array)
		{
			this.element_type = NativeType.None;
			this.size_parameter_index = -1;
			this.size = -1;
			this.size_parameter_multiplier = -1;
		}

		// Token: 0x04000448 RID: 1096
		internal NativeType element_type;

		// Token: 0x04000449 RID: 1097
		internal int size_parameter_index;

		// Token: 0x0400044A RID: 1098
		internal int size;

		// Token: 0x0400044B RID: 1099
		internal int size_parameter_multiplier;
	}
}
