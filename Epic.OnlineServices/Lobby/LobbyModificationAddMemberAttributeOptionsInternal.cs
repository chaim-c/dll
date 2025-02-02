using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200038C RID: 908
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationAddMemberAttributeOptionsInternal : ISettable<LobbyModificationAddMemberAttributeOptions>, IDisposable
	{
		// Token: 0x170006EC RID: 1772
		// (set) Token: 0x06001842 RID: 6210 RVA: 0x00024DFC File Offset: 0x00022FFC
		public AttributeData? Attribute
		{
			set
			{
				Helper.Set<AttributeData, AttributeDataInternal>(ref value, ref this.m_Attribute);
			}
		}

		// Token: 0x170006ED RID: 1773
		// (set) Token: 0x06001843 RID: 6211 RVA: 0x00024E0D File Offset: 0x0002300D
		public LobbyAttributeVisibility Visibility
		{
			set
			{
				this.m_Visibility = value;
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00024E17 File Offset: 0x00023017
		public void Set(ref LobbyModificationAddMemberAttributeOptions other)
		{
			this.m_ApiVersion = 1;
			this.Attribute = other.Attribute;
			this.Visibility = other.Visibility;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00024E3C File Offset: 0x0002303C
		public void Set(ref LobbyModificationAddMemberAttributeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Attribute = other.Value.Attribute;
				this.Visibility = other.Value.Visibility;
			}
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00024E87 File Offset: 0x00023087
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Attribute);
		}

		// Token: 0x04000B17 RID: 2839
		private int m_ApiVersion;

		// Token: 0x04000B18 RID: 2840
		private IntPtr m_Attribute;

		// Token: 0x04000B19 RID: 2841
		private LobbyAttributeVisibility m_Visibility;
	}
}
