using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TaleWorlds.MountAndBlade.Launcher.Library
{
	// Token: 0x02000012 RID: 18
	public class ResultData
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000042E2 File Offset: 0x000024E2
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000042EA File Offset: 0x000024EA
		public string Errors { get; set; } = "";

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000042F3 File Offset: 0x000024F3
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000042FB File Offset: 0x000024FB
		public List<DLLResult> DLLs { get; set; } = new List<DLLResult>();

		// Token: 0x06000099 RID: 153 RVA: 0x00004304 File Offset: 0x00002504
		public void AddDLLResult(string dllName, bool isSafe, string information)
		{
			this.DLLs.Add(new DLLResult(dllName, isSafe, information));
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000431C File Offset: 0x0000251C
		public override string ToString()
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ResultData));
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				xmlSerializer.Serialize(stringWriter, this);
				result = stringWriter.ToString();
			}
			return result;
		}
	}
}
