using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200038A RID: 906
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationAddAttributeOptionsInternal : ISettable<LobbyModificationAddAttributeOptions>, IDisposable
	{
		// Token: 0x170006E8 RID: 1768
		// (set) Token: 0x06001839 RID: 6201 RVA: 0x00024D40 File Offset: 0x00022F40
		public AttributeData? Attribute
		{
			set
			{
				Helper.Set<AttributeData, AttributeDataInternal>(ref value, ref this.m_Attribute);
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x00024D51 File Offset: 0x00022F51
		public LobbyAttributeVisibility Visibility
		{
			set
			{
				this.m_Visibility = value;
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00024D5B File Offset: 0x00022F5B
		public void Set(ref LobbyModificationAddAttributeOptions other)
		{
			this.m_ApiVersion = 1;
			this.Attribute = other.Attribute;
			this.Visibility = other.Visibility;
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00024D80 File Offset: 0x00022F80
		public void Set(ref LobbyModificationAddAttributeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Attribute = other.Value.Attribute;
				this.Visibility = other.Value.Visibility;
			}
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00024DCB File Offset: 0x00022FCB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Attribute);
		}

		// Token: 0x04000B12 RID: 2834
		private int m_ApiVersion;

		// Token: 0x04000B13 RID: 2835
		private IntPtr m_Attribute;

		// Token: 0x04000B14 RID: 2836
		private LobbyAttributeVisibility m_Visibility;
	}
}
