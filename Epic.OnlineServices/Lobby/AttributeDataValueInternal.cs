using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000335 RID: 821
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct AttributeDataValueInternal : IGettable<AttributeDataValue>, ISettable<AttributeDataValue>, IDisposable
	{
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x0002024C File Offset: 0x0001E44C
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x00020274 File Offset: 0x0001E474
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

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00020298 File Offset: 0x0001E498
		// (set) Token: 0x060015AD RID: 5549 RVA: 0x000202C0 File Offset: 0x0001E4C0
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

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x000202E4 File Offset: 0x0001E4E4
		// (set) Token: 0x060015AF RID: 5551 RVA: 0x0002030C File Offset: 0x0001E50C
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

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00020330 File Offset: 0x0001E530
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x00020358 File Offset: 0x0001E558
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

		// Token: 0x060015B2 RID: 5554 RVA: 0x0002037A File Offset: 0x0001E57A
		public void Set(ref AttributeDataValue other)
		{
			this.AsInt64 = other.AsInt64;
			this.AsDouble = other.AsDouble;
			this.AsBool = other.AsBool;
			this.AsUtf8 = other.AsUtf8;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000203B4 File Offset: 0x0001E5B4
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

		// Token: 0x060015B4 RID: 5556 RVA: 0x00020422 File Offset: 0x0001E622
		public void Dispose()
		{
			Helper.Dispose<AttributeType>(ref this.m_AsUtf8, this.m_ValueType, AttributeType.String);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00020438 File Offset: 0x0001E638
		public void Get(out AttributeDataValue output)
		{
			output = default(AttributeDataValue);
			output.Set(ref this);
		}

		// Token: 0x040009D0 RID: 2512
		[FieldOffset(0)]
		private long m_AsInt64;

		// Token: 0x040009D1 RID: 2513
		[FieldOffset(0)]
		private double m_AsDouble;

		// Token: 0x040009D2 RID: 2514
		[FieldOffset(0)]
		private int m_AsBool;

		// Token: 0x040009D3 RID: 2515
		[FieldOffset(0)]
		private IntPtr m_AsUtf8;

		// Token: 0x040009D4 RID: 2516
		[FieldOffset(8)]
		private AttributeType m_ValueType;
	}
}
