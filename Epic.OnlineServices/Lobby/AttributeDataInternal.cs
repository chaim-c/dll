using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000333 RID: 819
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AttributeDataInternal : IGettable<AttributeData>, ISettable<AttributeData>, IDisposable
	{
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0001FF1C File Offset: 0x0001E11C
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x0001FF3D File Offset: 0x0001E13D
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

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x0001FF50 File Offset: 0x0001E150
		// (set) Token: 0x06001595 RID: 5525 RVA: 0x0001FF71 File Offset: 0x0001E171
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

		// Token: 0x06001596 RID: 5526 RVA: 0x0001FF82 File Offset: 0x0001E182
		public void Set(ref AttributeData other)
		{
			this.m_ApiVersion = 1;
			this.Key = other.Key;
			this.Value = other.Value;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0001FFA8 File Offset: 0x0001E1A8
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

		// Token: 0x06001598 RID: 5528 RVA: 0x0001FFF3 File Offset: 0x0001E1F3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Key);
			Helper.Dispose<AttributeDataValueInternal>(ref this.m_Value);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0002000E File Offset: 0x0001E20E
		public void Get(out AttributeData output)
		{
			output = default(AttributeData);
			output.Set(ref this);
		}

		// Token: 0x040009C8 RID: 2504
		private int m_ApiVersion;

		// Token: 0x040009C9 RID: 2505
		private IntPtr m_Key;

		// Token: 0x040009CA RID: 2506
		private AttributeDataValueInternal m_Value;
	}
}
