using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.Logger;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.GameFeatures;
using Newtonsoft.Json;

namespace MCM.Implementation
{
	// Token: 0x02000022 RID: 34
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class JsonSettingsPreset : ISettingsPreset
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00004638 File Offset: 0x00002838
		[return: Nullable(2)]
		public static string GetPresetId(string content)
		{
			string result;
			try
			{
				JsonSettingsPreset.PresetContainerDefinition container = JsonConvert.DeserializeObject<JsonSettingsPreset.PresetContainerDefinition>(content);
				bool flag = container == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = container.Id;
				}
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000467C File Offset: 0x0000287C
		[return: Nullable(2)]
		public static JsonSettingsPreset FromFile(BaseSettings settings, GameFile file)
		{
			return JsonSettingsPreset.FromFile(settings.Id, file, new Func<BaseSettings>(settings.CreateNew));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004698 File Offset: 0x00002898
		[return: Nullable(2)]
		public static JsonSettingsPreset FromFile(string settingsId, GameFile file, Func<BaseSettings> getNewSettings)
		{
			IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
			bool flag = fileSystemProvider == null;
			JsonSettingsPreset result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] data = fileSystemProvider.ReadData(file);
				bool flag2 = data == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					string content = Encoding.UTF8.GetString(data);
					JsonSettingsPreset.PresetContainerDefinition container = JsonConvert.DeserializeObject<JsonSettingsPreset.PresetContainerDefinition>(content);
					bool flag3 = container == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = new JsonSettingsPreset(settingsId, container.Id, container.Name, file, getNewSettings);
					}
				}
			}
			return result;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004714 File Offset: 0x00002914
		public string SettingsId { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000471C File Offset: 0x0000291C
		public string Id { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004724 File Offset: 0x00002924
		public string Name { get; }

		// Token: 0x060000BE RID: 190 RVA: 0x0000472C File Offset: 0x0000292C
		public JsonSettingsPreset(BaseSettings settings, string id, string name, GameFile file) : this(settings.Id, id, name, file, new Func<BaseSettings>(settings.CreateNew))
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004750 File Offset: 0x00002950
		public JsonSettingsPreset(string settingsId, string id, string name, GameFile file, Func<BaseSettings> getNewSettings)
		{
			this.SettingsId = settingsId;
			this.Id = id;
			this.Name = name;
			this._file = file;
			this._getNewSettings = getNewSettings;
			IBUTRLogger<JsonSettingsPreset> logger = GenericServiceProvider.GetService<IBUTRLogger<JsonSettingsPreset>>() ?? new DefaultBUTRLogger<JsonSettingsPreset>();
			this._jsonSerializerSettings = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				Converters = 
				{
					new BaseSettingsJsonConverter(logger, new Action<string, object>(this.AddSerializationProperty), new Action(this.ClearSerializationProperties)),
					new DropdownJsonConverter(logger, new Func<string, object>(this.GetSerializationProperty))
				}
			};
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000480C File Offset: 0x00002A0C
		public BaseSettings LoadPreset()
		{
			BaseSettings presetBase = this._getNewSettings();
			IFileSystemProvider fileSystemProvider = GenericServiceProvider.GetService<IFileSystemProvider>();
			bool flag = fileSystemProvider == null;
			BaseSettings result;
			if (flag)
			{
				result = presetBase;
			}
			else
			{
				byte[] data = fileSystemProvider.ReadData(this._file);
				bool flag2 = data == null;
				if (flag2)
				{
					this.SavePreset(presetBase);
					result = presetBase;
				}
				else
				{
					string content = Encoding.UTF8.GetString(data);
					object @lock = this._lock;
					lock (@lock)
					{
						try
						{
							JsonSettingsPreset.PresetContainer container = new JsonSettingsPreset.PresetContainer
							{
								Id = this.Id,
								Name = this.Name,
								Settings = presetBase
							};
							JsonConvert.PopulateObject(content, container, this._jsonSerializerSettings);
						}
						catch (Exception)
						{
						}
					}
					result = presetBase;
				}
			}
			return result;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004900 File Offset: 0x00002B00
		public bool SavePreset(BaseSettings settings)
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
				try
				{
					JsonSettingsPreset.PresetContainer container = new JsonSettingsPreset.PresetContainer
					{
						Id = this.Id,
						Name = this.Name,
						Settings = settings
					};
					string content = JsonConvert.SerializeObject(container, this._jsonSerializerSettings);
					fileSystemProvider.WriteData(this._file, Encoding.UTF8.GetBytes(content));
					result = true;
				}
				catch (Exception)
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004990 File Offset: 0x00002B90
		[return: Nullable(2)]
		private object GetSerializationProperty(string path)
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

		// Token: 0x060000C3 RID: 195 RVA: 0x000049E4 File Offset: 0x00002BE4
		private void AddSerializationProperty(string path, [Nullable(2)] object value)
		{
			this._existingObjects.Add(path, value);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000049F5 File Offset: 0x00002BF5
		private void ClearSerializationProperties()
		{
			this._existingObjects.Clear();
		}

		// Token: 0x04000038 RID: 56
		private readonly GameFile _file;

		// Token: 0x04000039 RID: 57
		private readonly Func<BaseSettings> _getNewSettings;

		// Token: 0x0400003A RID: 58
		private readonly JsonSerializerSettings _jsonSerializerSettings;

		// Token: 0x0400003B RID: 59
		private readonly object _lock = new object();

		// Token: 0x0400003C RID: 60
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private readonly Dictionary<string, object> _existingObjects = new Dictionary<string, object>();

		// Token: 0x02000171 RID: 369
		[Nullable(0)]
		private class PresetContainerDefinition
		{
			// Token: 0x1700020A RID: 522
			// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00021CD8 File Offset: 0x0001FED8
			// (set) Token: 0x06000A03 RID: 2563 RVA: 0x00021CE0 File Offset: 0x0001FEE0
			[JsonProperty(Order = 1)]
			public string Id { get; set; } = string.Empty;

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00021CE9 File Offset: 0x0001FEE9
			// (set) Token: 0x06000A05 RID: 2565 RVA: 0x00021CF1 File Offset: 0x0001FEF1
			[JsonProperty(Order = 2)]
			public string Name { get; set; } = string.Empty;
		}

		// Token: 0x02000172 RID: 370
		[NullableContext(2)]
		[Nullable(0)]
		private sealed class PresetContainer : JsonSettingsPreset.PresetContainerDefinition
		{
			// Token: 0x1700020C RID: 524
			// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00021D19 File Offset: 0x0001FF19
			// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00021D21 File Offset: 0x0001FF21
			[JsonProperty(Order = 3)]
			public BaseSettings Settings { get; set; }
		}
	}
}
