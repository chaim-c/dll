using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000650 RID: 1616
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InitializeOptionsInternal : ISettable<InitializeOptions>, IDisposable
	{
		// Token: 0x17000C51 RID: 3153
		// (set) Token: 0x0600293C RID: 10556 RVA: 0x0003D80B File Offset: 0x0003BA0B
		public IntPtr AllocateMemoryFunction
		{
			set
			{
				this.m_AllocateMemoryFunction = value;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (set) Token: 0x0600293D RID: 10557 RVA: 0x0003D815 File Offset: 0x0003BA15
		public IntPtr ReallocateMemoryFunction
		{
			set
			{
				this.m_ReallocateMemoryFunction = value;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (set) Token: 0x0600293E RID: 10558 RVA: 0x0003D81F File Offset: 0x0003BA1F
		public IntPtr ReleaseMemoryFunction
		{
			set
			{
				this.m_ReleaseMemoryFunction = value;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (set) Token: 0x0600293F RID: 10559 RVA: 0x0003D829 File Offset: 0x0003BA29
		public Utf8String ProductName
		{
			set
			{
				Helper.Set(value, ref this.m_ProductName);
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (set) Token: 0x06002940 RID: 10560 RVA: 0x0003D839 File Offset: 0x0003BA39
		public Utf8String ProductVersion
		{
			set
			{
				Helper.Set(value, ref this.m_ProductVersion);
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (set) Token: 0x06002941 RID: 10561 RVA: 0x0003D849 File Offset: 0x0003BA49
		public IntPtr SystemInitializeOptions
		{
			set
			{
				this.m_SystemInitializeOptions = value;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (set) Token: 0x06002942 RID: 10562 RVA: 0x0003D853 File Offset: 0x0003BA53
		public InitializeThreadAffinity? OverrideThreadAffinity
		{
			set
			{
				Helper.Set<InitializeThreadAffinity, InitializeThreadAffinityInternal>(ref value, ref this.m_OverrideThreadAffinity);
			}
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x0003D864 File Offset: 0x0003BA64
		public void Set(ref InitializeOptions other)
		{
			this.m_ApiVersion = 4;
			this.AllocateMemoryFunction = other.AllocateMemoryFunction;
			this.ReallocateMemoryFunction = other.ReallocateMemoryFunction;
			this.ReleaseMemoryFunction = other.ReleaseMemoryFunction;
			this.ProductName = other.ProductName;
			this.ProductVersion = other.ProductVersion;
			int[] from = new int[]
			{
				1,
				1
			};
			IntPtr zero = IntPtr.Zero;
			Helper.Set<int>(from, ref zero);
			this.m_Reserved = zero;
			this.SystemInitializeOptions = other.SystemInitializeOptions;
			this.OverrideThreadAffinity = other.OverrideThreadAffinity;
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x0003D8FC File Offset: 0x0003BAFC
		public void Set(ref InitializeOptions? other)
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
				int[] from = new int[]
				{
					1,
					1
				};
				IntPtr zero = IntPtr.Zero;
				Helper.Set<int>(from, ref zero);
				this.m_Reserved = zero;
				this.SystemInitializeOptions = other.Value.SystemInitializeOptions;
				this.OverrideThreadAffinity = other.Value.OverrideThreadAffinity;
			}
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x0003D9D8 File Offset: 0x0003BBD8
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

		// Token: 0x0400129D RID: 4765
		private int m_ApiVersion;

		// Token: 0x0400129E RID: 4766
		private IntPtr m_AllocateMemoryFunction;

		// Token: 0x0400129F RID: 4767
		private IntPtr m_ReallocateMemoryFunction;

		// Token: 0x040012A0 RID: 4768
		private IntPtr m_ReleaseMemoryFunction;

		// Token: 0x040012A1 RID: 4769
		private IntPtr m_ProductName;

		// Token: 0x040012A2 RID: 4770
		private IntPtr m_ProductVersion;

		// Token: 0x040012A3 RID: 4771
		private IntPtr m_Reserved;

		// Token: 0x040012A4 RID: 4772
		private IntPtr m_SystemInitializeOptions;

		// Token: 0x040012A5 RID: 4773
		private IntPtr m_OverrideThreadAffinity;
	}
}
