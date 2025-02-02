using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000CE RID: 206
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct AttributeDataValueInternal : IGettable<AttributeDataValue>, ISettable<AttributeDataValue>, IDisposable
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0000AB70 File Offset: 0x00008D70
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0000AB98 File Offset: 0x00008D98
		public long? AsInt64
		{
			get
			{
				long? result;
				Helper.Get<long, AttributeType>(this.m_AsInt64, out result, this.m_ValueType, AttributeType.Int64);
				return result;
			}
			set
			{
				Helper.Set<long, AttributeType>(value, ref this.m_AsInt64, AttributeType.Int64, ref this.m_ValueType, this);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0000ABBC File Offset: 0x00008DBC
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		public double? AsDouble
		{
			get
			{
				double? result;
				Helper.Get<double, AttributeType>(this.m_AsDouble, out result, this.m_ValueType, AttributeType.Double);
				return result;
			}
			set
			{
				Helper.Set<double, AttributeType>(value, ref this.m_AsDouble, AttributeType.Double, ref this.m_ValueType, this);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0000AC08 File Offset: 0x00008E08
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0000AC30 File Offset: 0x00008E30
		public bool? AsBool
		{
			get
			{
				bool? result;
				Helper.Get<AttributeType>(this.m_AsBool, out result, this.m_ValueType, AttributeType.Boolean);
				return result;
			}
			set
			{
				Helper.Set<AttributeType>(value, ref this.m_AsBool, AttributeType.Boolean, ref this.m_ValueType, this);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0000AC54 File Offset: 0x00008E54
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0000AC7C File Offset: 0x00008E7C
		public Utf8String AsUtf8
		{
			get
			{
				Utf8String result;
				Helper.Get<AttributeType>(this.m_AsUtf8, out result, this.m_ValueType, AttributeType.String);
				return result;
			}
			set
			{
				Helper.Set<AttributeType>(value, ref this.m_AsUtf8, AttributeType.String, ref this.m_ValueType, this);
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0000AC9E File Offset: 0x00008E9E
		public void Set(ref AttributeDataValue other)
		{
			this.AsInt64 = other.AsInt64;
			this.AsDouble = other.AsDouble;
			this.AsBool = other.AsBool;
			this.AsUtf8 = other.AsUtf8;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0000ACD8 File Offset: 0x00008ED8
		public void Set(ref AttributeDataValue? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.AsInt64 = other.Value.AsInt64;
				this.AsDouble = other.Value.AsDouble;
				this.AsBool = other.Value.AsBool;
				this.AsUtf8 = other.Value.AsUtf8;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0000AD46 File Offset: 0x00008F46
		public void Dispose()
		{
			Helper.Dispose<AttributeType>(ref this.m_AsUtf8, this.m_ValueType, AttributeType.String);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0000AD5C File Offset: 0x00008F5C
		public void Get(out AttributeDataValue output)
		{
			output = default(AttributeDataValue);
			output.Set(ref this);
		}

		// Token: 0x04000362 RID: 866
		[FieldOffset(0)]
		private long m_AsInt64;

		// Token: 0x04000363 RID: 867
		[FieldOffset(0)]
		private double m_AsDouble;

		// Token: 0x04000364 RID: 868
		[FieldOffset(0)]
		private int m_AsBool;

		// Token: 0x04000365 RID: 869
		[FieldOffset(0)]
		private IntPtr m_AsUtf8;

		// Token: 0x04000366 RID: 870
		[FieldOffset(8)]
		private AttributeType m_ValueType;
	}
}
