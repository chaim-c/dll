using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using Newtonsoft.Json;

namespace MCM.Abstractions.Utils
{
	// Token: 0x02000069 RID: 105
	internal static class SerializationUtils
	{
		// Token: 0x0600026D RID: 621 RVA: 0x000099C8 File Offset: 0x00007BC8
		[NullableContext(2)]
		public static T DeserializeXml<T>([Nullable(1)] Stream xmlStream)
		{
			T result;
			using (StreamReader reader = new StreamReader(xmlStream))
			{
				string xml = reader.ReadToEnd();
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(xml);
				string json = JsonConvert.SerializeXmlNode(doc);
				result = ((json != null) ? JsonConvert.DeserializeObject<T>(json) : default(T));
			}
			return result;
		}
	}
}
