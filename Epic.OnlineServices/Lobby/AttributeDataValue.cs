using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000334 RID: 820
	public struct AttributeDataValue
	{
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x00020020 File Offset: 0x0001E220
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x00020048 File Offset: 0x0001E248
		public long? AsInt64
		{
			get
			{
				long? result;
				Helper.Get<long?, AttributeType>(this.m_AsInt64, out result, this.m_ValueType, AttributeType.Int64);
				return result;
			}
			set
			{
				Helper.Set<long?, AttributeType>(value, ref this.m_AsInt64, AttributeType.Int64, ref this.m_ValueType, null);
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x00020060 File Offset: 0x0001E260
		// (set) Token: 0x0600159D RID: 5533 RVA: 0x00020088 File Offset: 0x0001E288
		public double? AsDouble
		{
			get
			{
				double? result;
				Helper.Get<double?, AttributeType>(this.m_AsDouble, out result, this.m_ValueType, AttributeType.Double);
				return result;
			}
			set
			{
				Helper.Set<double?, AttributeType>(value, ref this.m_AsDouble, AttributeType.Double, ref this.m_ValueType, null);
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x000200A0 File Offset: 0x0001E2A0
		// (set) Token: 0x0600159F RID: 5535 RVA: 0x000200C8 File Offset: 0x0001E2C8
		public bool? AsBool
		{
			get
			{
				bool? result;
				Helper.Get<bool?, AttributeType>(this.m_AsBool, out result, this.m_ValueType, AttributeType.Boolean);
				return result;
			}
			set
			{
				Helper.Set<bool?, AttributeType>(value, ref this.m_AsBool, AttributeType.Boolean, ref this.m_ValueType, null);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000200E0 File Offset: 0x0001E2E0
		// (set) Token: 0x060015A1 RID: 5537 RVA: 0x00020108 File Offset: 0x0001E308
		public Utf8String AsUtf8
		{
			get
			{
				Utf8String result;
				Helper.Get<Utf8String, AttributeType>(this.m_AsUtf8, out result, this.m_ValueType, AttributeType.String);
				return result;
			}
			set
			{
				Helper.Set<Utf8String, AttributeType>(value, ref this.m_AsUtf8, AttributeType.String, ref this.m_ValueType, null);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00020120 File Offset: 0x0001E320
		// (set) Token: 0x060015A3 RID: 5539 RVA: 0x00020138 File Offset: 0x0001E338
		public AttributeType ValueType
		{
			get
			{
				return this.m_ValueType;
			}
			private set
			{
				this.m_ValueType = value;
			}
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00020144 File Offset: 0x0001E344
		public static implicit operator AttributeDataValue(long value)
		{
			return new AttributeDataValue
			{
				AsInt64 = new long?(value)
			};
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00020170 File Offset: 0x0001E370
		public static implicit operator AttributeDataValue(double value)
		{
			return new AttributeDataValue
			{
				AsDouble = new double?(value)
			};
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0002019C File Offset: 0x0001E39C
		public static implicit operator AttributeDataValue(bool value)
		{
			return new AttributeDataValue
			{
				AsBool = new bool?(value)
			};
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x000201C8 File Offset: 0x0001E3C8
		public static implicit operator AttributeDataValue(Utf8String value)
		{
			return new AttributeDataValue
			{
				AsUtf8 = value
			};
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000201EC File Offset: 0x0001E3EC
		public static implicit operator AttributeDataValue(string value)
		{
			return new AttributeDataValue
			{
				AsUtf8 = value
			};
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00020215 File Offset: 0x0001E415
		internal void Set(ref AttributeDataValueInternal other)
		{
			this.AsInt64 = other.AsInt64;
			this.AsDouble = other.AsDouble;
			this.AsBool = other.AsBool;
			this.AsUtf8 = other.AsUtf8;
		}

		// Token: 0x040009CB RID: 2507
		private long? m_AsInt64;

		// Token: 0x040009CC RID: 2508
		private double? m_AsDouble;

		// Token: 0x040009CD RID: 2509
		private bool? m_AsBool;

		// Token: 0x040009CE RID: 2510
		private Utf8String m_AsUtf8;

		// Token: 0x040009CF RID: 2511
		private AttributeType m_ValueType;
	}
}
