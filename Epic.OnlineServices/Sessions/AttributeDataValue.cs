using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000CD RID: 205
	public struct AttributeDataValue
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0000A944 File Offset: 0x00008B44
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0000A96C File Offset: 0x00008B6C
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

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0000A984 File Offset: 0x00008B84
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x0000A9AC File Offset: 0x00008BAC
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

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0000A9C4 File Offset: 0x00008BC4
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0000A9EC File Offset: 0x00008BEC
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

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0000AA04 File Offset: 0x00008C04
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0000AA2C File Offset: 0x00008C2C
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

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0000AA44 File Offset: 0x00008C44
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0000AA5C File Offset: 0x00008C5C
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

		// Token: 0x06000710 RID: 1808 RVA: 0x0000AA68 File Offset: 0x00008C68
		public static implicit operator AttributeDataValue(long value)
		{
			return new AttributeDataValue
			{
				AsInt64 = new long?(value)
			};
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0000AA94 File Offset: 0x00008C94
		public static implicit operator AttributeDataValue(double value)
		{
			return new AttributeDataValue
			{
				AsDouble = new double?(value)
			};
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		public static implicit operator AttributeDataValue(bool value)
		{
			return new AttributeDataValue
			{
				AsBool = new bool?(value)
			};
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000AAEC File Offset: 0x00008CEC
		public static implicit operator AttributeDataValue(Utf8String value)
		{
			return new AttributeDataValue
			{
				AsUtf8 = value
			};
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000AB10 File Offset: 0x00008D10
		public static implicit operator AttributeDataValue(string value)
		{
			return new AttributeDataValue
			{
				AsUtf8 = value
			};
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000AB39 File Offset: 0x00008D39
		internal void Set(ref AttributeDataValueInternal other)
		{
			this.AsInt64 = other.AsInt64;
			this.AsDouble = other.AsDouble;
			this.AsBool = other.AsBool;
			this.AsUtf8 = other.AsUtf8;
		}

		// Token: 0x0400035D RID: 861
		private long? m_AsInt64;

		// Token: 0x0400035E RID: 862
		private double? m_AsDouble;

		// Token: 0x0400035F RID: 863
		private bool? m_AsBool;

		// Token: 0x04000360 RID: 864
		private Utf8String m_AsUtf8;

		// Token: 0x04000361 RID: 865
		private AttributeType m_ValueType;
	}
}
