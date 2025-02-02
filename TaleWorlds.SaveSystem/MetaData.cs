using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000013 RID: 19
	public class MetaData
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002C89 File Offset: 0x00000E89
		public void Add(string key, string value)
		{
			this._list.Add(key, value);
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002C98 File Offset: 0x00000E98
		[JsonIgnore]
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002CA5 File Offset: 0x00000EA5
		public bool TryGetValue(string key, out string value)
		{
			return this._list.TryGetValue(key, out value);
		}

		// Token: 0x1700000E RID: 14
		public string this[string key]
		{
			get
			{
				if (!this._list.ContainsKey(key))
				{
					return null;
				}
				return this._list[key];
			}
			set
			{
				this._list[key] = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002CE1 File Offset: 0x00000EE1
		[JsonIgnore]
		public Dictionary<string, string>.KeyCollection Keys
		{
			get
			{
				return this._list.Keys;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public void Serialize(Stream stream)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this));
			stream.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
			stream.Write(bytes, 0, bytes.Length);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D2C File Offset: 0x00000F2C
		public static MetaData Deserialize(Stream stream)
		{
			MetaData result;
			try
			{
				byte[] array = new byte[4];
				stream.Read(array, 0, 4);
				int num = BitConverter.ToInt32(array, 0);
				byte[] array2 = new byte[num];
				stream.Read(array2, 0, num);
				result = JsonConvert.DeserializeObject<MetaData>(Encoding.UTF8.GetString(array2));
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0400001A RID: 26
		[JsonProperty("List")]
		private Dictionary<string, string> _list = new Dictionary<string, string>();
	}
}
