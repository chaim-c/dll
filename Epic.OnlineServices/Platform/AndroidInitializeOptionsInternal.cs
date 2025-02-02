using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000642 RID: 1602
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AndroidInitializeOptionsInternal : ISettable<AndroidInitializeOptions>, IDisposable
	{
		// Token: 0x17000C34 RID: 3124
		// (set) Token: 0x060028C2 RID: 10434 RVA: 0x0003C9A1 File Offset: 0x0003ABA1
		public IntPtr AllocateMemoryFunction
		{
			set
			{
				this.m_AllocateMemoryFunction = value;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (set) Token: 0x060028C3 RID: 10435 RVA: 0x0003C9AB File Offset: 0x0003ABAB
		public IntPtr ReallocateMemoryFunction
		{
			set
			{
				this.m_ReallocateMemoryFunction = value;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (set) Token: 0x060028C4 RID: 10436 RVA: 0x0003C9B5 File Offset: 0x0003ABB5
		public IntPtr ReleaseMemoryFunction
		{
			set
			{
				this.m_ReleaseMemoryFunction = value;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (set) Token: 0x060028C5 RID: 10437 RVA: 0x0003C9BF File Offset: 0x0003ABBF
		public Utf8String ProductName
		{
			set
			{
				Helper.Set(value, ref this.m_ProductName);
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (set) Token: 0x060028C6 RID: 10438 RVA: 0x0003C9CF File Offset: 0x0003ABCF
		public Utf8String ProductVersion
		{
			set
			{
				Helper.Set(value, ref this.m_ProductVersion);
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (set) Token: 0x060028C7 RID: 10439 RVA: 0x0003C9DF File Offset: 0x0003ABDF
		public IntPtr Reserved
		{
			set
			{
				this.m_Reserved = value;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (set) Token: 0x060028C8 RID: 10440 RVA: 0x0003C9E9 File Offset: 0x0003ABE9
		public AndroidInitializeOptionsSystemInitializeOptions? SystemInitializeOptions
		{
			set
			{
				Helper.Set<AndroidInitializeOptionsSystemInitializeOptions, AndroidInitializeOptionsSystemInitializeOptionsInternal>(ref value, ref this.m_SystemInitializeOptions);
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (set) Token: 0x060028C9 RID: 10441 RVA: 0x0003C9FA File Offset: 0x0003ABFA
		public InitializeThreadAffinity? OverrideThreadAffinity
		{
			set
			{
				Helper.Set<InitializeThreadAffinity, InitializeThreadAffinityInternal>(ref value, ref this.m_OverrideThreadAffinity);
			}
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0003CA0C File Offset: 0x0003AC0C
		public void Set(ref AndroidInitializeOptions other)
		{
			this.m_ApiVersion = 4;
			this.AllocateMemoryFunction = other.AllocateMemoryFunction;
			this.ReallocateMemoryFunction = other.ReallocateMemoryFunction;
			this.ReleaseMemoryFunction = other.ReleaseMemoryFunction;
			this.ProductName = other.ProductName;
			this.ProductVersion = other.ProductVersion;
			this.Reserved = other.Reserved;
			this.SystemInitializeOptions = other.SystemInitializeOptions;
			this.OverrideThreadAffinity = other.OverrideThreadAffinity;
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x0003CA8C File Offset: 0x0003AC8C
		public void Set(ref AndroidInitializeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 4;
				this.AllocateMemoryFunction = other.Value.AllocateMemoryFunction;
				this.ReallocateMemoryFunction = other.Value.ReallocateMemoryFunction;
				this.ReleaseMemoryFunction = other.Value.ReleaseMemoryFunction;
				this.ProductName = other.Value.ProductName;
				this.ProductVersion = other.Value.ProductVersion;
				this.Reserved = other.Value.Reserved;
				this.SystemInitializeOptions = other.Value.SystemInitializeOptions;
				this.OverrideThreadAffinity = other.Value.OverrideThreadAffinity;
			}
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0003CB58 File Offset: 0x0003AD58
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AllocateMemoryFunction);
			Helper.Dispose(ref this.m_ReallocateMemoryFunction);
			Helper.Dispose(ref this.m_ReleaseMemoryFunction);
			Helper.Dispose(ref this.m_ProductName);
			Helper.Dispose(ref this.m_ProductVersion);
			Helper.Dispose(ref this.m_Reserved);
			Helper.Dispose(ref this.m_SystemInitializeOptions);
			Helper.Dispose(ref this.m_OverrideThreadAffinity);
		}

		// Token: 0x04001263 RID: 4707
		private int m_ApiVersion;

		// Token: 0x04001264 RID: 4708
		private IntPtr m_AllocateMemoryFunction;

		// Token: 0x04001265 RID: 4709
		private IntPtr m_ReallocateMemoryFunction;

		// Token: 0x04001266 RID: 4710
		private IntPtr m_ReleaseMemoryFunction;

		// Token: 0x04001267 RID: 4711
		private IntPtr m_ProductName;

		// Token: 0x04001268 RID: 4712
		private IntPtr m_ProductVersion;

		// Token: 0x04001269 RID: 4713
		private IntPtr m_Reserved;

		// Token: 0x0400126A RID: 4714
		private IntPtr m_SystemInitializeOptions;

		// Token: 0x0400126B RID: 4715
		private IntPtr m_OverrideThreadAffinity;
	}
}
