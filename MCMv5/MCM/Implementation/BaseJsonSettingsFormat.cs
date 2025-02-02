using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.Logger;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.GameFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCM.Implementation
{
	// Token: 0x0200001F RID: 31
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class BaseJsonSettingsFormat : ISettingsFormat
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00004014 File Offset: 0x00002214
		protected virtual JsonSerializerSettings JsonSerializerSettings { get; }

		// Token: 0x060000A7 RID: 167 RVA: 0x0000401C File Offset: 0x0000221C
		protected BaseJsonSettingsFormat(IBUTRLogger logger)
		{
			this.Logger = logger;
			this.JsonSerializerSettings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				Converters = 
				{
					new BaseSettingsJsonConverter(logger, new Action<string, object>(this.AddSerializationProperty), new Action(this.ClearSerializationProperties)),
					new DropdownJsonConverter(logger, new Func<string, object>(this.GetSerializationProperty))
				}
			};
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000040BD File Offset: 0x000022BD
		public virtual IEnumerable<string> FormatTypes { get; } = new string[]
		{
			"json"
		};

		// Token: 0x060000A9 RID: 169 RVA: 0x000040C8 File Offset: 0x000022C8
		public string SaveJson(BaseSettings settings)
		{
			return JsonConvert.SerializeObject(settings, this.JsonSerializerSettings);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000040E8 File Offset: 0x000022E8
		public BaseSettings LoadFromJson(BaseSettings settings, string content)
		{
			this.TryLoadFromJson(ref settings, content);
			return settings;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004108 File Offset: 0x00002308
		protected bool TryLoadFromJson(ref BaseSettings settings, string content)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				try
				{
					JObject jo;
					bool flag2 = !BaseJsonSettingsFormat.TryParse(content, out jo);
					if (flag2)
					{
						return false;
					}
					using (JsonReader reader = jo.CreateReader())
					{
						JsonSerializer serializer = JsonSerializer.CreateDefault(this.JsonSerializerSettings);
						Type settingsType = settings.GetType();
						BaseSettingsJsonConverter settingsConverter = this.JsonSerializerSettings.Converters.OfType<BaseSettingsJsonConverter>().FirstOrDefault<BaseSettingsJsonConverter>();
						bool flag3 = settingsConverter != null && settingsConverter.CanConvert(settingsType);
						if (flag3)
						{
							settingsConverter.ReadJson(reader, settingsType, settings, serializer);
						}
					}
				}
				catch (JsonSerializationException)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000041F0 File Offset: 0x000023F0
		public virtual bool Save(BaseSettings settings, GameDirectory directory, string filename)
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
				GameFile file = fileSystemProvider.GetOrCreateFile(directory, filename + ".json");
				bool flag2 = file == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					string content = this.SaveJson(settings);
					try
					{
						result = fileSystemProvider.WriteData(file, Encoding.UTF8.GetBytes(content));
					}
					catch (Exception)
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004274 File Offset: 0x00002474
		public virtual BaseSettings Load(BaseSettings settings, GameDirectory directory, string filename)
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
				GameFile file = fileSystemProvider.GetFile(directory, filename + ".json");
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
						try
						{
							bool flag4 = !this.TryLoadFromJson(ref settings, Encoding.UTF8.GetString(data));
							if (flag4)
							{
								this.Save(settings, directory, filename);
							}
						}
						catch (Exception)
						{
						}
						result = settings;
					}
				}
			}
			return result;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000432C File Offset: 0x0000252C
		[return: Nullable(2)]
		protected object GetSerializationProperty(string path)
		{
			object @lock = this._lock;
			object result;
			lock (@lock)
			{
				object value;
				result = (this._existingObjects.TryGetValue(path, out value) ? value : null);
			}
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004380 File Offset: 0x00002580
		protected void AddSerializationProperty(string path, [Nullable(2)] object value)
		{
			this._existingObjects.Add(path, value);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004391 File Offset: 0x00002591
		protected void ClearSerializationProperties()
		{
			this._existingObjects.Clear();
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000043A0 File Offset: 0x000025A0
		private static bool TryParse(string content, [Nullable(2)] [NotNullWhen(true)] out JObject jObject)
		{
			bool result;
			try
			{
				jObject = JObject.Parse(content);
				result = true;
			}
			catch (JsonReaderException)
			{
				jObject = null;
				result = false;
			}
			return result;
		}

		// Token: 0x04000030 RID: 48
		private readonly object _lock = new object();

		// Token: 0x04000031 RID: 49
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		protected readonly Dictionary<string, object> _existingObjects = new Dictionary<string, object>();

		// Token: 0x04000032 RID: 50
		protected readonly IBUTRLogger Logger;
	}
}
