using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002F8 RID: 760
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ModIdentifierInternal : IGettable<ModIdentifier>, ISettable<ModIdentifier>, IDisposable
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0001E588 File Offset: 0x0001C788
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x0001E5A9 File Offset: 0x0001C7A9
		public Utf8String NamespaceId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_NamespaceId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_NamespaceId);
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x0001E5BC File Offset: 0x0001C7BC
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x0001E5DD File Offset: 0x0001C7DD
		public Utf8String ItemId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ItemId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ItemId);
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
		// (set) Token: 0x06001481 RID: 5249 RVA: 0x0001E611 File Offset: 0x0001C811
		public Utf8String ArtifactId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ArtifactId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ArtifactId);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0001E624 File Offset: 0x0001C824
		// (set) Token: 0x06001483 RID: 5251 RVA: 0x0001E645 File Offset: 0x0001C845
		public Utf8String Title
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Title, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Title);
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x0001E658 File Offset: 0x0001C858
		// (set) Token: 0x06001485 RID: 5253 RVA: 0x0001E679 File Offset: 0x0001C879
		public Utf8String Version
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Version, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Version);
			}
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0001E68C File Offset: 0x0001C88C
		public void Set(ref ModIdentifier other)
		{
			this.m_ApiVersion = 1;
			this.NamespaceId = other.NamespaceId;
			this.ItemId = other.ItemId;
			this.ArtifactId = other.ArtifactId;
			this.Title = other.Title;
			this.Version = other.Version;
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0001E6E4 File Offset: 0x0001C8E4
		public void Set(ref ModIdentifier? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.NamespaceId = other.Value.NamespaceId;
				this.ItemId = other.Value.ItemId;
				this.ArtifactId = other.Value.ArtifactId;
				this.Title = other.Value.Title;
				this.Version = other.Value.Version;
			}
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0001E76E File Offset: 0x0001C96E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_NamespaceId);
			Helper.Dispose(ref this.m_ItemId);
			Helper.Dispose(ref this.m_ArtifactId);
			Helper.Dispose(ref this.m_Title);
			Helper.Dispose(ref this.m_Version);
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0001E7AD File Offset: 0x0001C9AD
		public void Get(out ModIdentifier output)
		{
			output = default(ModIdentifier);
			output.Set(ref this);
		}

		// Token: 0x04000933 RID: 2355
		private int m_ApiVersion;

		// Token: 0x04000934 RID: 2356
		private IntPtr m_NamespaceId;

		// Token: 0x04000935 RID: 2357
		private IntPtr m_ItemId;

		// Token: 0x04000936 RID: 2358
		private IntPtr m_ArtifactId;

		// Token: 0x04000937 RID: 2359
		private IntPtr m_Title;

		// Token: 0x04000938 RID: 2360
		private IntPtr m_Version;
	}
}
