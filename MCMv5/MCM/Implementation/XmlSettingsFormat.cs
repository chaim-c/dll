using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.Logger;
using MCM.Abstractions.Base;
using MCM.Abstractions.GameFeatures;
using MCM.Common;
using Newtonsoft.Json;

namespace MCM.Implementation
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class XmlSettingsFormat : BaseJsonSettingsFormat
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000043FB File Offset: 0x000025FB
		public override IEnumerable<string> FormatTypes
		{
			get
			{
				return new string[]
				{
					"xml"
				};
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000440B File Offset: 0x0000260B
		public XmlSettingsFormat(IBUTRLogger<XmlSettingsFormat> logger) : base(logger)
		{
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004418 File Offset: 0x00002618
		public override bool Save(BaseSettings settings, GameDirectory directory, string filename)
		{
			IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
			bool flag = fileSystemProvider == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				GameFile file = fileSystemProvider.GetOrCreateFile(directory, filename + ".xml");
				bool flag2 = file == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					string content = base.SaveJson(settings);
					string value = content;
					IWrapper wrapper = settings as IWrapper;
					string name;
					if (wrapper != null)
					{
						object obj = wrapper.Object;
						if (obj != null)
						{
							name = obj.GetType().Name;
							goto IL_7F;
						}
					}
					name = settings.GetType().Name;
					IL_7F:
					XmlDocument xmlDocument = JsonConvert.DeserializeXmlNode(value, name);
					bool flag3 = xmlDocument == null;
					if (flag3)
					{
						result = false;
					}
					else
					{
						using (MemoryStream ms = new MemoryStream())
						{
							using (StreamWriter writer = new StreamWriter(ms))
							{
								xmlDocument.Save(writer);
								try
								{
									result = fileSystemProvider.WriteData(file, ms.ToArray());
								}
								catch (Exception)
								{
									result = false;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004530 File Offset: 0x00002730
		public override BaseSettings Load(BaseSettings settings, GameDirectory directory, string filename)
		{
			IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
			bool flag = fileSystemProvider == null;
			BaseSettings result;
			if (flag)
			{
				result = settings;
			}
			else
			{
				GameFile file = fileSystemProvider.GetFile(directory, filename + ".xml");
				bool flag2 = file == null;
				if (flag2)
				{
					result = settings;
				}
				else
				{
					byte[] data = fileSystemProvider.ReadData(file);
					bool flag3 = data == null;
					if (flag3)
					{
						this.Save(settings, directory, filename);
						result = settings;
					}
					else
					{
						XmlDocument xmlDocument = new XmlDocument();
						using (MemoryStream ms = new MemoryStream(data))
						{
							xmlDocument.Load(ms);
							XmlElement root = xmlDocument[settings.GetType().Name];
							bool flag4 = root == null;
							if (flag4)
							{
								this.Save(settings, directory, filename);
								result = settings;
							}
							else
							{
								string content = JsonConvert.SerializeXmlNode(root, Formatting.None, true);
								bool flag5 = !base.TryLoadFromJson(ref settings, content);
								if (flag5)
								{
									this.Save(settings, directory, filename);
								}
								result = settings;
							}
						}
					}
				}
			}
			return result;
		}
	}
}
