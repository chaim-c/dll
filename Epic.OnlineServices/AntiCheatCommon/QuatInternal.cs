using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000605 RID: 1541
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QuatInternal : IGettable<Quat>, ISettable<Quat>, IDisposable
	{
		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06002795 RID: 10133 RVA: 0x0003B044 File Offset: 0x00039244
		// (set) Token: 0x06002796 RID: 10134 RVA: 0x0003B05C File Offset: 0x0003925C
		public float w
		{
			get
			{
				return this.m_w;
			}
			set
			{
				this.m_w = value;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x0003B068 File Offset: 0x00039268
		// (set) Token: 0x06002798 RID: 10136 RVA: 0x0003B080 File Offset: 0x00039280
		public float x
		{
			get
			{
				return this.m_x;
			}
			set
			{
				this.m_x = value;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x0003B08C File Offset: 0x0003928C
		// (set) Token: 0x0600279A RID: 10138 RVA: 0x0003B0A4 File Offset: 0x000392A4
		public float y
		{
			get
			{
				return this.m_y;
			}
			set
			{
				this.m_y = value;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x0003B0B0 File Offset: 0x000392B0
		// (set) Token: 0x0600279C RID: 10140 RVA: 0x0003B0C8 File Offset: 0x000392C8
		public float z
		{
			get
			{
				return this.m_z;
			}
			set
			{
				this.m_z = value;
			}
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x0003B0D2 File Offset: 0x000392D2
		public void Set(ref Quat other)
		{
			this.w = other.w;
			this.x = other.x;
			this.y = other.y;
			this.z = other.z;
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x0003B10C File Offset: 0x0003930C
		public void Set(ref Quat? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.w = other.Value.w;
				this.x = other.Value.x;
				this.y = other.Value.y;
				this.z = other.Value.z;
			}
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x0003B17A File Offset: 0x0003937A
		public void Dispose()
		{
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x0003B17D File Offset: 0x0003937D
		public void Get(out Quat output)
		{
			output = default(Quat);
			output.Set(ref this);
		}

		// Token: 0x040011CA RID: 4554
		private float m_w;

		// Token: 0x040011CB RID: 4555
		private float m_x;

		// Token: 0x040011CC RID: 4556
		private float m_y;

		// Token: 0x040011CD RID: 4557
		private float m_z;
	}
}
