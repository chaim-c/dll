using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Save
{
	// Token: 0x0200002D RID: 45
	public class SaveContext : ISaveContext
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007807 File Offset: 0x00005A07
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000780F File Offset: 0x00005A0F
		public object RootObject { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007818 File Offset: 0x00005A18
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00007820 File Offset: 0x00005A20
		public GameData SaveData { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007829 File Offset: 0x00005A29
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00007831 File Offset: 0x00005A31
		public DefinitionContext DefinitionContext { get; private set; }

		// Token: 0x06000198 RID: 408 RVA: 0x0000783A File Offset: 0x00005A3A
		public static SaveContext.SaveStatistics GetStatistics()
		{
			return new SaveContext.SaveStatistics(SaveContext._typeStatistics, SaveContext._containerStatistics);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000784B File Offset: 0x00005A4B
		public static bool EnableSaveStatistics
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007850 File Offset: 0x00005A50
		public SaveContext(DefinitionContext definitionContext)
		{
			this.DefinitionContext = definitionContext;
			this._childObjects = new List<object>(131072);
			this._idsOfChildObjects = new Dictionary<object, int>(131072);
			this._strings = new List<string>(131072);
			this._idsOfStrings = new Dictionary<string, int>(131072);
			this._childContainers = new List<object>(131072);
			this._idsOfChildContainers = new Dictionary<object, int>(131072);
			this._temporaryCollectedObjects = new List<object>(4096);
			this._locker = new object();
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000078E8 File Offset: 0x00005AE8
		private void CollectObjects()
		{
			using (new PerformanceTestBlock("SaveContext::CollectObjects"))
			{
				this._objectsToIterate = new Queue<object>(1024);
				this._objectsToIterate.Enqueue(this.RootObject);
				while (this._objectsToIterate.Count > 0)
				{
					object obj = this._objectsToIterate.Dequeue();
					ContainerType containerType;
					if (obj.GetType().IsContainer(out containerType))
					{
						this.CollectContainerObjects(containerType, obj);
					}
					else
					{
						this.CollectObjects(obj);
					}
				}
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000797C File Offset: 0x00005B7C
		private void CollectContainerObjects(ContainerType containerType, object parent)
		{
			if (!this._idsOfChildContainers.ContainsKey(parent))
			{
				int count = this._childContainers.Count;
				this._childContainers.Add(parent);
				this._idsOfChildContainers.Add(parent, count);
				Type type = parent.GetType();
				ContainerDefinition containerDefinition = this.DefinitionContext.GetContainerDefinition(type);
				if (containerDefinition == null)
				{
					string message = "Cant find definition for " + type.FullName;
					Debug.Print(message, 0, Debug.DebugColor.Red, 17592186044416UL);
					Debug.FailedAssert(message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.SaveSystem\\Save\\SaveContext.cs", "CollectContainerObjects", 154);
				}
				ContainerSaveData.GetChildObjects(this, containerDefinition, containerType, parent, this._temporaryCollectedObjects);
				for (int i = 0; i < this._temporaryCollectedObjects.Count; i++)
				{
					object obj = this._temporaryCollectedObjects[i];
					if (obj != null)
					{
						this._objectsToIterate.Enqueue(obj);
					}
				}
				this._temporaryCollectedObjects.Clear();
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007A5C File Offset: 0x00005C5C
		private void CollectObjects(object parent)
		{
			if (!this._idsOfChildObjects.ContainsKey(parent))
			{
				int count = this._childObjects.Count;
				this._childObjects.Add(parent);
				this._idsOfChildObjects.Add(parent, count);
				Type type = parent.GetType();
				TypeDefinition classDefinition = this.DefinitionContext.GetClassDefinition(type);
				if (classDefinition == null)
				{
					throw new Exception("Could not find type definition of type: " + type);
				}
				ObjectSaveData.GetChildObjects(this, classDefinition, parent, this._temporaryCollectedObjects);
				for (int i = 0; i < this._temporaryCollectedObjects.Count; i++)
				{
					object obj = this._temporaryCollectedObjects[i];
					if (obj != null)
					{
						this._objectsToIterate.Enqueue(obj);
					}
				}
				this._temporaryCollectedObjects.Clear();
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007B18 File Offset: 0x00005D18
		public void AddStrings(List<string> texts)
		{
			object locker = this._locker;
			lock (locker)
			{
				for (int i = 0; i < texts.Count; i++)
				{
					string text = texts[i];
					if (text != null && !this._idsOfStrings.ContainsKey(text))
					{
						int count = this._strings.Count;
						this._idsOfStrings.Add(text, count);
						this._strings.Add(text);
					}
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007BA4 File Offset: 0x00005DA4
		public int AddOrGetStringId(string text)
		{
			int num = -1;
			if (text == null)
			{
				num = -1;
			}
			else
			{
				object locker = this._locker;
				lock (locker)
				{
					int num2;
					if (this._idsOfStrings.TryGetValue(text, out num2))
					{
						num = num2;
					}
					else
					{
						num = this._strings.Count;
						this._idsOfStrings.Add(text, num);
						this._strings.Add(text);
					}
				}
			}
			return num;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00007C24 File Offset: 0x00005E24
		public int GetObjectId(object target)
		{
			int result;
			if (!this._idsOfChildObjects.TryGetValue(target, out result))
			{
				Debug.Print(string.Format("SAVE ERROR. Cant find {0} with type {1}", target, target.GetType()), 0, Debug.DebugColor.White, 17592186044416UL);
				Debug.FailedAssert("SAVE ERROR. Cant find target object on save", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.SaveSystem\\Save\\SaveContext.cs", "GetObjectId", 261);
			}
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007C7D File Offset: 0x00005E7D
		public int GetContainerId(object target)
		{
			return this._idsOfChildContainers[target];
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00007C8C File Offset: 0x00005E8C
		public int GetStringId(string target)
		{
			if (target == null)
			{
				return -1;
			}
			int result = -1;
			object locker = this._locker;
			lock (locker)
			{
				result = this._idsOfStrings[target];
			}
			return result;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007CDC File Offset: 0x00005EDC
		private static void SaveStringTo(SaveEntryFolder stringsFolder, int id, string value)
		{
			BinaryWriter binaryWriter = BinaryWriterFactory.GetBinaryWriter();
			binaryWriter.WriteString(value);
			stringsFolder.CreateEntry(new EntryId(id, SaveEntryExtension.Txt)).FillFrom(binaryWriter);
			BinaryWriterFactory.ReleaseBinaryWriter(binaryWriter);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007D10 File Offset: 0x00005F10
		public bool Save(object target, MetaData metaData, out string errorMessage)
		{
			errorMessage = "";
			bool result = false;
			if (SaveContext.EnableSaveStatistics)
			{
				SaveContext._typeStatistics = new Dictionary<string, ValueTuple<int, int, int, long>>();
				SaveContext._containerStatistics = new Dictionary<string, ValueTuple<int, int, int, int, long>>();
			}
			try
			{
				this.RootObject = target;
				using (new PerformanceTestBlock("SaveContext::Save"))
				{
					BinaryWriterFactory.Initialize();
					this.CollectObjects();
					ArchiveConcurrentSerializer headerSerializer = new ArchiveConcurrentSerializer();
					byte[][] objectData = new byte[this._childObjects.Count][];
					using (new PerformanceTestBlock("SaveContext::Saving Objects"))
					{
						if (!SaveContext.EnableSaveStatistics)
						{
							TWParallel.For(0, this._childObjects.Count, delegate(int startInclusive, int endExclusive)
							{
								for (int l = startInclusive; l < endExclusive; l++)
								{
									this.SaveSingleObject(headerSerializer, objectData, l);
								}
							}, 16);
						}
						else
						{
							for (int i = 0; i < this._childObjects.Count; i++)
							{
								this.SaveSingleObject(headerSerializer, objectData, i);
							}
						}
					}
					byte[][] containerData = new byte[this._childContainers.Count][];
					using (new PerformanceTestBlock("SaveContext::Saving Containers"))
					{
						if (!SaveContext.EnableSaveStatistics)
						{
							TWParallel.For(0, this._childContainers.Count, delegate(int startInclusive, int endExclusive)
							{
								for (int l = startInclusive; l < endExclusive; l++)
								{
									this.SaveSingleContainer(headerSerializer, containerData, l);
								}
							}, 16);
						}
						else
						{
							for (int j = 0; j < this._childContainers.Count; j++)
							{
								this.SaveSingleContainer(headerSerializer, containerData, j);
							}
						}
					}
					SaveEntryFolder saveEntryFolder = SaveEntryFolder.CreateRootFolder();
					BinaryWriter binaryWriter = BinaryWriterFactory.GetBinaryWriter();
					binaryWriter.WriteInt(this._idsOfChildObjects.Count);
					binaryWriter.WriteInt(this._strings.Count);
					binaryWriter.WriteInt(this._idsOfChildContainers.Count);
					saveEntryFolder.CreateEntry(new EntryId(-1, SaveEntryExtension.Config)).FillFrom(binaryWriter);
					headerSerializer.SerializeFolderConcurrent(saveEntryFolder);
					BinaryWriterFactory.ReleaseBinaryWriter(binaryWriter);
					ArchiveSerializer archiveSerializer = new ArchiveSerializer();
					SaveEntryFolder saveEntryFolder2 = SaveEntryFolder.CreateRootFolder();
					SaveEntryFolder stringsFolder = archiveSerializer.CreateFolder(saveEntryFolder2, new FolderId(-1, SaveFolderExtension.Strings), this._strings.Count);
					for (int k = 0; k < this._strings.Count; k++)
					{
						string value = this._strings[k];
						SaveContext.SaveStringTo(stringsFolder, k, value);
					}
					archiveSerializer.SerializeFolder(saveEntryFolder2);
					byte[] header = headerSerializer.FinalizeAndGetBinaryDataConcurrent();
					byte[] strings = archiveSerializer.FinalizeAndGetBinaryData();
					this.SaveData = new GameData(header, strings, objectData, containerData);
					BinaryWriterFactory.Release();
				}
				result = true;
			}
			catch (Exception ex)
			{
				errorMessage = "SaveContext Error\n";
				errorMessage += ex.Message;
				result = false;
			}
			return result;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008024 File Offset: 0x00006224
		private void SaveSingleObject(ArchiveConcurrentSerializer headerSerializer, byte[][] objectData, int id)
		{
			object target = this._childObjects[id];
			ArchiveSerializer archiveSerializer = new ArchiveSerializer();
			SaveEntryFolder saveEntryFolder = SaveEntryFolder.CreateRootFolder();
			SaveEntryFolder saveEntryFolder2 = SaveEntryFolder.CreateRootFolder();
			ObjectSaveData objectSaveData = new ObjectSaveData(this, id, target, true);
			objectSaveData.CollectStructs();
			objectSaveData.CollectMembers();
			objectSaveData.CollectStrings();
			objectSaveData.SaveHeaderTo(saveEntryFolder2, headerSerializer);
			objectSaveData.SaveTo(saveEntryFolder, archiveSerializer);
			headerSerializer.SerializeFolderConcurrent(saveEntryFolder2);
			archiveSerializer.SerializeFolder(saveEntryFolder);
			byte[] array = archiveSerializer.FinalizeAndGetBinaryData();
			objectData[id] = array;
			if (SaveContext.EnableSaveStatistics)
			{
				string name = objectSaveData.Type.Name;
				int num = array.Length;
				ValueTuple<int, int, int, long> valueTuple;
				if (SaveContext._typeStatistics.TryGetValue(name, out valueTuple))
				{
					SaveContext._typeStatistics[name] = new ValueTuple<int, int, int, long>(valueTuple.Item1 + 1, valueTuple.Item2, valueTuple.Item3, valueTuple.Item4 + (long)num);
					return;
				}
				SaveContext._typeStatistics[name] = new ValueTuple<int, int, int, long>(1, objectSaveData.FieldCount, objectSaveData.PropertyCount, (long)num);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008120 File Offset: 0x00006320
		private void SaveSingleContainer(ArchiveConcurrentSerializer headerSerializer, byte[][] containerData, int id)
		{
			object obj = this._childContainers[id];
			ArchiveSerializer archiveSerializer = new ArchiveSerializer();
			SaveEntryFolder saveEntryFolder = SaveEntryFolder.CreateRootFolder();
			SaveEntryFolder saveEntryFolder2 = SaveEntryFolder.CreateRootFolder();
			ContainerType containerType;
			obj.GetType().IsContainer(out containerType);
			ContainerSaveData containerSaveData = new ContainerSaveData(this, id, obj, containerType);
			containerSaveData.CollectChildren();
			containerSaveData.CollectStructs();
			containerSaveData.CollectMembers();
			containerSaveData.CollectStrings();
			containerSaveData.SaveHeaderTo(saveEntryFolder2, headerSerializer);
			containerSaveData.SaveTo(saveEntryFolder, archiveSerializer);
			headerSerializer.SerializeFolderConcurrent(saveEntryFolder2);
			archiveSerializer.SerializeFolder(saveEntryFolder);
			byte[] array = archiveSerializer.FinalizeAndGetBinaryData();
			containerData[id] = array;
			if (SaveContext.EnableSaveStatistics)
			{
				string containerName = this.GetContainerName(containerSaveData.Type);
				long num = (long)array.Length;
				ValueTuple<int, int, int, int, long> valueTuple;
				if (SaveContext._containerStatistics.TryGetValue(containerName, out valueTuple))
				{
					SaveContext._containerStatistics[containerName] = new ValueTuple<int, int, int, int, long>(valueTuple.Item1 + 1, valueTuple.Item2 + containerSaveData.GetElementCount(), valueTuple.Item3, valueTuple.Item4, SaveContext._containerStatistics[containerName].Item5 + num);
					return;
				}
				SaveContext._containerStatistics[containerName] = new ValueTuple<int, int, int, int, long>(1, containerSaveData.GetElementCount(), containerSaveData.ElementFieldCount, containerSaveData.ElementPropertyCount, num);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008254 File Offset: 0x00006454
		private string GetContainerName(Type t)
		{
			string text = t.Name;
			foreach (Type type in t.GetGenericArguments())
			{
				if (t.IsContainer())
				{
					text += this.GetContainerName(type);
				}
				else
				{
					text = text + type.Name + ".";
				}
			}
			return text.TrimEnd(new char[]
			{
				'.'
			});
		}

		// Token: 0x0400006F RID: 111
		private List<object> _childObjects;

		// Token: 0x04000070 RID: 112
		private Dictionary<object, int> _idsOfChildObjects;

		// Token: 0x04000071 RID: 113
		private List<object> _childContainers;

		// Token: 0x04000072 RID: 114
		private Dictionary<object, int> _idsOfChildContainers;

		// Token: 0x04000073 RID: 115
		private List<string> _strings;

		// Token: 0x04000074 RID: 116
		private Dictionary<string, int> _idsOfStrings;

		// Token: 0x04000075 RID: 117
		private List<object> _temporaryCollectedObjects;

		// Token: 0x04000077 RID: 119
		private object _locker;

		// Token: 0x04000079 RID: 121
		private static Dictionary<string, ValueTuple<int, int, int, long>> _typeStatistics;

		// Token: 0x0400007A RID: 122
		private static Dictionary<string, ValueTuple<int, int, int, int, long>> _containerStatistics;

		// Token: 0x0400007B RID: 123
		private Queue<object> _objectsToIterate;

		// Token: 0x02000073 RID: 115
		public struct SaveStatistics
		{
			// Token: 0x06000380 RID: 896 RVA: 0x0000F587 File Offset: 0x0000D787
			public SaveStatistics(Dictionary<string, ValueTuple<int, int, int, long>> typeStatistics, Dictionary<string, ValueTuple<int, int, int, int, long>> containerStatistics)
			{
				this._typeStatistics = typeStatistics;
				this._containerStatistics = containerStatistics;
			}

			// Token: 0x06000381 RID: 897 RVA: 0x0000F598 File Offset: 0x0000D798
			public ValueTuple<int, int, int, long> GetObjectCounts(string key)
			{
				if (this._typeStatistics.ContainsKey(key))
				{
					return this._typeStatistics[key];
				}
				return default(ValueTuple<int, int, int, long>);
			}

			// Token: 0x06000382 RID: 898 RVA: 0x0000F5C9 File Offset: 0x0000D7C9
			public ValueTuple<int, int, int, int, long> GetContainerCounts(string key)
			{
				return this._containerStatistics[key];
			}

			// Token: 0x06000383 RID: 899 RVA: 0x0000F5D7 File Offset: 0x0000D7D7
			public long GetContainerSize(string key)
			{
				return this._containerStatistics[key].Item5;
			}

			// Token: 0x06000384 RID: 900 RVA: 0x0000F5EA File Offset: 0x0000D7EA
			public List<string> GetTypeKeys()
			{
				return this._typeStatistics.Keys.ToList<string>();
			}

			// Token: 0x06000385 RID: 901 RVA: 0x0000F5FC File Offset: 0x0000D7FC
			public List<string> GetContainerKeys()
			{
				return this._containerStatistics.Keys.ToList<string>();
			}

			// Token: 0x04000134 RID: 308
			private Dictionary<string, ValueTuple<int, int, int, long>> _typeStatistics;

			// Token: 0x04000135 RID: 309
			private Dictionary<string, ValueTuple<int, int, int, int, long>> _containerStatistics;
		}
	}
}
