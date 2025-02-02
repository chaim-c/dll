using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using MCM.UI.Exceptions;

namespace MCM.UI.Functionality.Injectors
{
	// Token: 0x02000026 RID: 38
	public abstract class ResourceInjector
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00007E9C File Offset: 0x0000609C
		[NullableContext(1)]
		protected static XmlDocument Load(string embedPath)
		{
			XmlDocument result;
			using (Stream stream = typeof(DefaultResourceInjector).Assembly.GetManifestResourceStream(embedPath))
			{
				bool flag = stream == null;
				if (flag)
				{
					throw new MCMUIEmbedResourceNotFoundException("Could not find embed resource '" + embedPath + "'!");
				}
				using (XmlReader xmlReader = XmlReader.Create(stream, new XmlReaderSettings
				{
					IgnoreComments = true
				}))
				{
					XmlDocument doc = new XmlDocument();
					doc.Load(xmlReader);
					result = doc;
				}
			}
			return result;
		}

		// Token: 0x0600017C RID: 380
		public abstract void Inject();
	}
}
