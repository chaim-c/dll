using System;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200001F RID: 31
	[DataContract]
	[JsonConverter(typeof(PeerIdJsonConverter))]
	[Serializable]
	public struct PeerId
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002D93 File Offset: 0x00000F93
		public bool IsValid
		{
			get
			{
				return this._chunk1 != 0UL || this._chunk2 != 0UL || this._chunk3 != 0UL || this._chunk4 > 0UL;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002DBC File Offset: 0x00000FBC
		public PeerId(Guid guid)
		{
			byte[] value = guid.ToByteArray();
			this._chunk1 = 0UL;
			this._chunk2 = 0UL;
			this._chunk3 = BitConverter.ToUInt64(value, 0);
			this._chunk4 = BitConverter.ToUInt64(value, 8);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002DFB File Offset: 0x00000FFB
		public PeerId(byte[] data)
		{
			this._chunk1 = BitConverter.ToUInt64(data, 0);
			this._chunk2 = BitConverter.ToUInt64(data, 8);
			this._chunk3 = BitConverter.ToUInt64(data, 16);
			this._chunk4 = BitConverter.ToUInt64(data, 24);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002E34 File Offset: 0x00001034
		public PeerId(string peerIdAsString)
		{
			int num = peerIdAsString.Length * 2;
			byte[] array = new byte[(num < 32) ? 32 : num];
			Encoding.Unicode.GetBytes(peerIdAsString, 0, peerIdAsString.Length, array, 0);
			this._chunk1 = BitConverter.ToUInt64(array, 0);
			this._chunk2 = BitConverter.ToUInt64(array, 8);
			this._chunk3 = BitConverter.ToUInt64(array, 16);
			this._chunk4 = BitConverter.ToUInt64(array, 24);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002EA5 File Offset: 0x000010A5
		public PeerId(ulong chunk1, ulong chunk2, ulong chunk3, ulong chunk4)
		{
			this._chunk1 = chunk1;
			this._chunk2 = chunk2;
			this._chunk3 = chunk3;
			this._chunk4 = chunk4;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002EC4 File Offset: 0x000010C4
		public byte[] ToByteArray()
		{
			byte[] array = new byte[32];
			byte[] bytes = BitConverter.GetBytes(this._chunk1);
			byte[] bytes2 = BitConverter.GetBytes(this._chunk2);
			byte[] bytes3 = BitConverter.GetBytes(this._chunk3);
			byte[] bytes4 = BitConverter.GetBytes(this._chunk4);
			for (int i = 0; i < 8; i++)
			{
				array[i] = bytes[i];
				array[8 + i] = bytes2[i];
				array[16 + i] = bytes3[i];
				array[24 + i] = bytes4[i];
			}
			return array;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002F44 File Offset: 0x00001144
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this._chunk1,
				".",
				this._chunk2,
				".",
				this._chunk3,
				" .",
				this._chunk4
			});
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002FAC File Offset: 0x000011AC
		public static PeerId FromString(string peerIdAsString)
		{
			string[] array = peerIdAsString.Split(new char[]
			{
				'.'
			});
			return new PeerId(ulong.Parse(array[0]), ulong.Parse(array[1]), ulong.Parse(array[2]), ulong.Parse(array[3]));
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002FF0 File Offset: 0x000011F0
		public static bool operator ==(PeerId a, PeerId b)
		{
			return a._chunk1 == b._chunk1 && a._chunk2 == b._chunk2 && a._chunk3 == b._chunk3 && a._chunk4 == b._chunk4;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000302C File Offset: 0x0000122C
		public static bool operator !=(PeerId a, PeerId b)
		{
			return a._chunk1 != b._chunk1 || a._chunk2 != b._chunk2 || a._chunk3 != b._chunk3 || a._chunk4 != b._chunk4;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000306C File Offset: 0x0000126C
		public override bool Equals(object o)
		{
			if (o != null && o is PeerId)
			{
				PeerId peerId = (PeerId)o;
				return this._chunk1 == peerId._chunk1 && this._chunk2 == peerId._chunk2 && this._chunk3 == peerId._chunk3 && this._chunk4 == peerId._chunk4;
			}
			return false;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000030C8 File Offset: 0x000012C8
		public override int GetHashCode()
		{
			int hashCode = this._chunk1.GetHashCode();
			int hashCode2 = this._chunk2.GetHashCode();
			int hashCode3 = this._chunk3.GetHashCode();
			int hashCode4 = this._chunk4.GetHashCode();
			return hashCode ^ hashCode2 ^ hashCode3 ^ hashCode4;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003116 File Offset: 0x00001316
		public static PeerId Empty
		{
			get
			{
				return new PeerId(0UL, 0UL, 0UL, 0UL);
			}
		}

		// Token: 0x04000028 RID: 40
		[DataMember]
		private readonly ulong _chunk1;

		// Token: 0x04000029 RID: 41
		[DataMember]
		private readonly ulong _chunk2;

		// Token: 0x0400002A RID: 42
		[DataMember]
		private readonly ulong _chunk3;

		// Token: 0x0400002B RID: 43
		[DataMember]
		private readonly ulong _chunk4;
	}
}
