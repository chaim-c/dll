using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace TaleWorlds.Library
{
	// Token: 0x0200006D RID: 109
	public class NavigationPath : ISerializable
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000C556 File Offset: 0x0000A756
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0000C55E File Offset: 0x0000A75E
		public Vec2[] PathPoints { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000C567 File Offset: 0x0000A767
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000C56F File Offset: 0x0000A76F
		[CachedData]
		public int Size { get; set; }

		// Token: 0x060003CA RID: 970 RVA: 0x0000C578 File Offset: 0x0000A778
		public NavigationPath()
		{
			this.PathPoints = new Vec2[128];
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000C590 File Offset: 0x0000A790
		protected NavigationPath(SerializationInfo info, StreamingContext context)
		{
			this.PathPoints = new Vec2[128];
			this.Size = info.GetInt32("s");
			for (int i = 0; i < this.Size; i++)
			{
				float single = info.GetSingle("x" + i);
				float single2 = info.GetSingle("y" + i);
				this.PathPoints[i] = new Vec2(single, single2);
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000C618 File Offset: 0x0000A818
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("s", this.Size);
			for (int i = 0; i < this.Size; i++)
			{
				info.AddValue("x" + i, this.PathPoints[i].x);
				info.AddValue("y" + i, this.PathPoints[i].y);
			}
		}

		// Token: 0x1700005D RID: 93
		public Vec2 this[int i]
		{
			get
			{
				return this.PathPoints[i];
			}
		}

		// Token: 0x0400011B RID: 283
		private const int PathSize = 128;
	}
}
