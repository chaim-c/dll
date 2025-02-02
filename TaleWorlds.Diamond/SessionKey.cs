using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000024 RID: 36
	[DataContract]
	[Serializable]
	public struct SessionKey
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003246 File Offset: 0x00001446
		public Guid Guid
		{
			get
			{
				return this._guid;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000324E File Offset: 0x0000144E
		public SessionKey(Guid guid)
		{
			this._guid = guid;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003257 File Offset: 0x00001457
		public SessionKey(byte[] b)
		{
			this._guid = new Guid(b);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003265 File Offset: 0x00001465
		public SessionKey(string g)
		{
			this._guid = new Guid(g);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003273 File Offset: 0x00001473
		public static SessionKey NewGuid()
		{
			return new SessionKey(Guid.NewGuid());
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003280 File Offset: 0x00001480
		public override string ToString()
		{
			return this._guid.ToString();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000032A4 File Offset: 0x000014A4
		public byte[] ToByteArray()
		{
			return this._guid.ToByteArray();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000032BF File Offset: 0x000014BF
		public static bool operator ==(SessionKey a, SessionKey b)
		{
			return a._guid == b._guid;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000032D2 File Offset: 0x000014D2
		public static bool operator !=(SessionKey a, SessionKey b)
		{
			return a._guid != b._guid;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000032E8 File Offset: 0x000014E8
		public override bool Equals(object o)
		{
			if (o != null && o is SessionKey)
			{
				SessionKey sessionKey = (SessionKey)o;
				return this._guid.Equals(sessionKey.Guid);
			}
			return false;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003320 File Offset: 0x00001520
		public override int GetHashCode()
		{
			return this._guid.GetHashCode();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003341 File Offset: 0x00001541
		public static SessionKey Empty
		{
			get
			{
				return new SessionKey(Guid.Empty);
			}
		}

		// Token: 0x04000030 RID: 48
		[DataMember]
		private readonly Guid _guid;
	}
}
