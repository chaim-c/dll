using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200060F RID: 1551
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct Vec3fInternal : IGettable<Vec3f>, ISettable<Vec3f>, IDisposable
	{
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060027D6 RID: 10198 RVA: 0x0003B5C4 File Offset: 0x000397C4
		// (set) Token: 0x060027D7 RID: 10199 RVA: 0x0003B5DC File Offset: 0x000397DC
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

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x0003B5E8 File Offset: 0x000397E8
		// (set) Token: 0x060027D9 RID: 10201 RVA: 0x0003B600 File Offset: 0x00039800
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

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x0003B60C File Offset: 0x0003980C
		// (set) Token: 0x060027DB RID: 10203 RVA: 0x0003B624 File Offset: 0x00039824
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

		// Token: 0x060027DC RID: 10204 RVA: 0x0003B62E File Offset: 0x0003982E
		public void Set(ref Vec3f other)
		{
			this.x = other.x;
			this.y = other.y;
			this.z = other.z;
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x0003B658 File Offset: 0x00039858
		public void Set(ref Vec3f? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.x = other.Value.x;
				this.y = other.Value.y;
				this.z = other.Value.z;
			}
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x0003B6B1 File Offset: 0x000398B1
		public void Dispose()
		{
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x0003B6B4 File Offset: 0x000398B4
		public void Get(out Vec3f output)
		{
			output = default(Vec3f);
			output.Set(ref this);
		}

		// Token: 0x040011E9 RID: 4585
		private float m_x;

		// Token: 0x040011EA RID: 4586
		private float m_y;

		// Token: 0x040011EB RID: 4587
		private float m_z;
	}
}
