using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000331 RID: 817
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AttributeInternal : IGettable<Attribute>, ISettable<Attribute>, IDisposable
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0001FDF4 File Offset: 0x0001DFF4
		// (set) Token: 0x06001586 RID: 5510 RVA: 0x0001FE15 File Offset: 0x0001E015
		public AttributeData? Data
		{
			get
			{
				AttributeData? result;
				Helper.Get<AttributeDataInternal, AttributeData>(this.m_Data, out result);
				return result;
			}
			set
			{
				Helper.Set<AttributeData, AttributeDataInternal>(ref value, ref this.m_Data);
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0001FE28 File Offset: 0x0001E028
		// (set) Token: 0x06001588 RID: 5512 RVA: 0x0001FE40 File Offset: 0x0001E040
		public LobbyAttributeVisibility Visibility
		{
			get
			{
				return this.m_Visibility;
			}
			set
			{
				this.m_Visibility = value;
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0001FE4A File Offset: 0x0001E04A
		public void Set(ref Attribute other)
		{
			this.m_ApiVersion = 1;
			this.Data = other.Data;
			this.Visibility = other.Visibility;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x0001FE70 File Offset: 0x0001E070
		public void Set(ref Attribute? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Data = other.Value.Data;
				this.Visibility = other.Value.Visibility;
			}
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0001FEBB File Offset: 0x0001E0BB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Data);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x0001FECA File Offset: 0x0001E0CA
		public void Get(out Attribute output)
		{
			output = default(Attribute);
			output.Set(ref this);
		}

		// Token: 0x040009C3 RID: 2499
		private int m_ApiVersion;

		// Token: 0x040009C4 RID: 2500
		private IntPtr m_Data;

		// Token: 0x040009C5 RID: 2501
		private LobbyAttributeVisibility m_Visibility;
	}
}
