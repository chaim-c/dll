using System;
using System.Collections.Generic;
using System.Globalization;

namespace TaleWorlds.Library
{
	// Token: 0x0200006F RID: 111
	public class ParameterContainer
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x0000C7C0 File Offset: 0x0000A9C0
		public ParameterContainer()
		{
			this._parameters = new Dictionary<string, string>();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000C7D3 File Offset: 0x0000A9D3
		public void AddParameter(string key, string value, bool overwriteIfExists)
		{
			if (this._parameters.ContainsKey(key))
			{
				if (overwriteIfExists)
				{
					this._parameters[key] = value;
					return;
				}
			}
			else
			{
				this._parameters.Add(key, value);
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000C804 File Offset: 0x0000AA04
		public void AddParameterConcurrent(string key, string value, bool overwriteIfExists)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(this._parameters);
			if (dictionary.ContainsKey(key))
			{
				if (overwriteIfExists)
				{
					dictionary[key] = value;
				}
			}
			else
			{
				dictionary.Add(key, value);
			}
			this._parameters = dictionary;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000C844 File Offset: 0x0000AA44
		public void AddParametersConcurrent(IEnumerable<KeyValuePair<string, string>> parameters, bool overwriteIfExists)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(this._parameters);
			foreach (KeyValuePair<string, string> keyValuePair in parameters)
			{
				if (dictionary.ContainsKey(keyValuePair.Key))
				{
					if (overwriteIfExists)
					{
						dictionary[keyValuePair.Key] = keyValuePair.Value;
					}
				}
				else
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			this._parameters = dictionary;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		public void ClearParameters()
		{
			this._parameters = new Dictionary<string, string>();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000C8E1 File Offset: 0x0000AAE1
		public bool TryGetParameter(string key, out string outValue)
		{
			return this._parameters.TryGetValue(key, out outValue);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
		public bool TryGetParameterAsBool(string key, out bool outValue)
		{
			outValue = false;
			string a;
			if (this.TryGetParameter(key, out a))
			{
				outValue = (a == "true" || a == "True");
				return true;
			}
			return false;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000C92C File Offset: 0x0000AB2C
		public bool TryGetParameterAsInt(string key, out int outValue)
		{
			outValue = 0;
			string value;
			if (this.TryGetParameter(key, out value))
			{
				outValue = Convert.ToInt32(value);
				return true;
			}
			return false;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000C954 File Offset: 0x0000AB54
		public bool TryGetParameterAsUInt16(string key, out ushort outValue)
		{
			outValue = 0;
			string value;
			if (this.TryGetParameter(key, out value))
			{
				outValue = Convert.ToUInt16(value);
				return true;
			}
			return false;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000C97C File Offset: 0x0000AB7C
		public bool TryGetParameterAsFloat(string key, out float outValue)
		{
			outValue = 0f;
			string value;
			if (this.TryGetParameter(key, out value))
			{
				outValue = Convert.ToSingle(value, CultureInfo.InvariantCulture);
				return true;
			}
			return false;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		public bool TryGetParameterAsByte(string key, out byte outValue)
		{
			outValue = 0;
			string value;
			if (this.TryGetParameter(key, out value))
			{
				outValue = Convert.ToByte(value);
				return true;
			}
			return false;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		public bool TryGetParameterAsSByte(string key, out sbyte outValue)
		{
			outValue = 0;
			string value;
			if (this.TryGetParameter(key, out value))
			{
				outValue = Convert.ToSByte(value);
				return true;
			}
			return false;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		public bool TryGetParameterAsVec3(string key, out Vec3 outValue)
		{
			outValue = default(Vec3);
			string text;
			if (this.TryGetParameter(key, out text))
			{
				string[] array = text.Split(new char[]
				{
					';'
				});
				float x = Convert.ToSingle(array[0], CultureInfo.InvariantCulture);
				float y = Convert.ToSingle(array[1], CultureInfo.InvariantCulture);
				float z = Convert.ToSingle(array[2], CultureInfo.InvariantCulture);
				outValue = new Vec3(x, y, z, -1f);
				return true;
			}
			return false;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		public bool TryGetParameterAsVec2(string key, out Vec2 outValue)
		{
			outValue = default(Vec2);
			string text;
			if (this.TryGetParameter(key, out text))
			{
				string[] array = text.Split(new char[]
				{
					';'
				});
				float a = Convert.ToSingle(array[0], CultureInfo.InvariantCulture);
				float b = Convert.ToSingle(array[1], CultureInfo.InvariantCulture);
				outValue = new Vec2(a, b);
				return true;
			}
			return false;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000CAC7 File Offset: 0x0000ACC7
		public string GetParameter(string key)
		{
			return this._parameters[key];
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000CAD5 File Offset: 0x0000ACD5
		public IEnumerable<KeyValuePair<string, string>> Iterator
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000CAE0 File Offset: 0x0000ACE0
		public ParameterContainer Clone()
		{
			ParameterContainer parameterContainer = new ParameterContainer();
			foreach (KeyValuePair<string, string> keyValuePair in this._parameters)
			{
				parameterContainer._parameters.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return parameterContainer;
		}

		// Token: 0x0400011F RID: 287
		private Dictionary<string, string> _parameters;
	}
}
