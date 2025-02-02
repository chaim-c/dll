using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200000E RID: 14
	public class RichTextTag
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000047AE File Offset: 0x000029AE
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000047B6 File Offset: 0x000029B6
		public string Name { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000047BF File Offset: 0x000029BF
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000047C7 File Offset: 0x000029C7
		public RichTextTagType Type { get; set; }

		// Token: 0x06000085 RID: 133 RVA: 0x000047D0 File Offset: 0x000029D0
		public RichTextTag(string name)
		{
			this.Name = name;
			this._attributes = new Dictionary<string, string>();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000047EA File Offset: 0x000029EA
		public void AddAtrribute(string key, string value)
		{
			this._attributes.Add(key, value);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000047F9 File Offset: 0x000029F9
		public string GetAttribute(string key)
		{
			if (this._attributes.ContainsKey(key))
			{
				return this._attributes[key];
			}
			return "";
		}

		// Token: 0x04000054 RID: 84
		private Dictionary<string, string> _attributes;
	}
}
