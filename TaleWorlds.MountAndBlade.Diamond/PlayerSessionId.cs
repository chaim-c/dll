using System;
using TaleWorlds.Diamond;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014A RID: 330
	[Serializable]
	public struct PlayerSessionId
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0000D252 File Offset: 0x0000B452
		public Guid Guid
		{
			get
			{
				return this._guid;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0000D25A File Offset: 0x0000B45A
		public SessionKey SessionKey
		{
			get
			{
				return new SessionKey(this._guid);
			}
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0000D267 File Offset: 0x0000B467
		public PlayerSessionId(Guid guid)
		{
			this._guid = guid;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0000D270 File Offset: 0x0000B470
		public PlayerSessionId(SessionKey sessionKey)
		{
			this._guid = sessionKey.Guid;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0000D27F File Offset: 0x0000B47F
		public PlayerSessionId(byte[] b)
		{
			this._guid = new Guid(b);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0000D28D File Offset: 0x0000B48D
		public PlayerSessionId(string g)
		{
			this._guid = new Guid(g);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0000D29B File Offset: 0x0000B49B
		public static PlayerSessionId NewGuid()
		{
			return new PlayerSessionId(Guid.NewGuid());
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0000D2A7 File Offset: 0x0000B4A7
		public override string ToString()
		{
			return this._guid.ToString();
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0000D2BA File Offset: 0x0000B4BA
		public byte[] ToByteArray()
		{
			return this._guid.ToByteArray();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0000D2C7 File Offset: 0x0000B4C7
		public static bool operator ==(PlayerSessionId a, PlayerSessionId b)
		{
			return a._guid == b._guid;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0000D2DA File Offset: 0x0000B4DA
		public static bool operator !=(PlayerSessionId a, PlayerSessionId b)
		{
			return a._guid != b._guid;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
		public override bool Equals(object o)
		{
			return o != null && o is PlayerSessionId && this._guid.Equals(((PlayerSessionId)o).Guid);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0000D323 File Offset: 0x0000B523
		public override int GetHashCode()
		{
			return this._guid.GetHashCode();
		}

		// Token: 0x040003A9 RID: 937
		private Guid _guid;
	}
}
