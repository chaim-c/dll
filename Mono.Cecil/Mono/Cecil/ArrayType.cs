using System;
using System.Text;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000055 RID: 85
	public sealed class ArrayType : TypeSpecification
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000C014 File Offset: 0x0000A214
		public Collection<ArrayDimension> Dimensions
		{
			get
			{
				if (this.dimensions != null)
				{
					return this.dimensions;
				}
				this.dimensions = new Collection<ArrayDimension>();
				this.dimensions.Add(default(ArrayDimension));
				return this.dimensions;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000C055 File Offset: 0x0000A255
		public int Rank
		{
			get
			{
				if (this.dimensions != null)
				{
					return this.dimensions.Count;
				}
				return 1;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000C06C File Offset: 0x0000A26C
		public bool IsVector
		{
			get
			{
				return this.dimensions == null || (this.dimensions.Count <= 1 && !this.dimensions[0].IsSized);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000C0AA File Offset: 0x0000A2AA
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000C0AD File Offset: 0x0000A2AD
		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000C0B4 File Offset: 0x0000A2B4
		public override string Name
		{
			get
			{
				return base.Name + this.Suffix;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000C0C7 File Offset: 0x0000A2C7
		public override string FullName
		{
			get
			{
				return base.FullName + this.Suffix;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000C0DC File Offset: 0x0000A2DC
		private string Suffix
		{
			get
			{
				if (this.IsVector)
				{
					return "[]";
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("[");
				for (int i = 0; i < this.dimensions.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(this.dimensions[i].ToString());
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000C162 File Offset: 0x0000A362
		public override bool IsArray
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000C165 File Offset: 0x0000A365
		public ArrayType(TypeReference type) : base(type)
		{
			Mixin.CheckType(type);
			this.etype = Mono.Cecil.Metadata.ElementType.Array;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000C17C File Offset: 0x0000A37C
		public ArrayType(TypeReference type, int rank) : this(type)
		{
			Mixin.CheckType(type);
			if (rank == 1)
			{
				return;
			}
			this.dimensions = new Collection<ArrayDimension>(rank);
			for (int i = 0; i < rank; i++)
			{
				this.dimensions.Add(default(ArrayDimension));
			}
			this.etype = Mono.Cecil.Metadata.ElementType.Array;
		}

		// Token: 0x04000383 RID: 899
		private Collection<ArrayDimension> dimensions;
	}
}
