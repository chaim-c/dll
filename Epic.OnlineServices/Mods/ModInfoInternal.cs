using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002FA RID: 762
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ModInfoInternal : IGettable<ModInfo>, ISettable<ModInfo>, IDisposable
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0001E800 File Offset: 0x0001CA00
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x0001E827 File Offset: 0x0001CA27
		public ModIdentifier[] Mods
		{
			get
			{
				ModIdentifier[] result;
				Helper.Get<ModIdentifierInternal, ModIdentifier>(this.m_Mods, out result, this.m_ModsCount);
				return result;
			}
			set
			{
				Helper.Set<ModIdentifier, ModIdentifierInternal>(ref value, ref this.m_Mods, out this.m_ModsCount);
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0001E840 File Offset: 0x0001CA40
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x0001E858 File Offset: 0x0001CA58
		public ModEnumerationType Type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0001E862 File Offset: 0x0001CA62
		public void Set(ref ModInfo other)
		{
			this.m_ApiVersion = 1;
			this.Mods = other.Mods;
			this.Type = other.Type;
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0001E888 File Offset: 0x0001CA88
		public void Set(ref ModInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Mods = other.Value.Mods;
				this.Type = other.Value.Type;
			}
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0001E8D3 File Offset: 0x0001CAD3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Mods);
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0001E8E2 File Offset: 0x0001CAE2
		public void Get(out ModInfo output)
		{
			output = default(ModInfo);
			output.Set(ref this);
		}

		// Token: 0x0400093B RID: 2363
		private int m_ApiVersion;

		// Token: 0x0400093C RID: 2364
		private int m_ModsCount;

		// Token: 0x0400093D RID: 2365
		private IntPtr m_Mods;

		// Token: 0x0400093E RID: 2366
		private ModEnumerationType m_Type;
	}
}
