using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000CC RID: 204
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AttributeDataInternal : IGettable<AttributeData>, ISettable<AttributeData>, IDisposable
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0000A840 File Offset: 0x00008A40
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x0000A861 File Offset: 0x00008A61
		public Utf8String Key
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Key, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Key);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0000A874 File Offset: 0x00008A74
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0000A895 File Offset: 0x00008A95
		public AttributeDataValue Value
		{
			get
			{
				AttributeDataValue result;
				Helper.Get<AttributeDataValueInternal, AttributeDataValue>(ref this.m_Value, out result);
				return result;
			}
			set
			{
				Helper.Set<AttributeDataValue, AttributeDataValueInternal>(ref value, ref this.m_Value);
			}
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0000A8A6 File Offset: 0x00008AA6
		public void Set(ref AttributeData other)
		{
			this.m_ApiVersion = 1;
			this.Key = other.Key;
			this.Value = other.Value;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000A8CC File Offset: 0x00008ACC
		public void Set(ref AttributeData? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Value.Key;
				this.Value = other.Value.Value;
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0000A917 File Offset: 0x00008B17
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Key);
			Helper.Dispose<AttributeDataValueInternal>(ref this.m_Value);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000A932 File Offset: 0x00008B32
		public void Get(out AttributeData output)
		{
			output = default(AttributeData);
			output.Set(ref this);
		}

		// Token: 0x0400035A RID: 858
		private int m_ApiVersion;

		// Token: 0x0400035B RID: 859
		private IntPtr m_Key;

		// Token: 0x0400035C RID: 860
		private AttributeDataValueInternal m_Value;
	}
}
