using System;
using System.Collections;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Save
{
	// Token: 0x02000026 RID: 38
	internal class ContainerSaveData
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006400 File Offset: 0x00004600
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00006408 File Offset: 0x00004608
		public int ObjectId { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006411 File Offset: 0x00004611
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00006419 File Offset: 0x00004619
		public ISaveContext Context { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006422 File Offset: 0x00004622
		// (set) Token: 0x0600014A RID: 330 RVA: 0x0000642A File Offset: 0x0000462A
		public object Target { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006433 File Offset: 0x00004633
		// (set) Token: 0x0600014C RID: 332 RVA: 0x0000643B File Offset: 0x0000463B
		public Type Type { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00006444 File Offset: 0x00004644
		internal int ElementPropertyCount
		{
			get
			{
				if (this._childStructs.Count <= 0)
				{
					return 0;
				}
				return this._childStructs[0].PropertyCount;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006467 File Offset: 0x00004667
		internal int ElementFieldCount
		{
			get
			{
				if (this._childStructs.Count <= 0)
				{
					return 0;
				}
				return this._childStructs[0].FieldCount;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000648C File Offset: 0x0000468C
		public ContainerSaveData(ISaveContext context, int objectId, object target, ContainerType containerType)
		{
			this.ObjectId = objectId;
			this.Context = context;
			this.Target = target;
			this._containerType = containerType;
			this.Type = target.GetType();
			this._elementCount = this.GetElementCount();
			this._childStructs = new List<ObjectSaveData>();
			this._typeDefinition = context.DefinitionContext.GetContainerDefinition(this.Type);
			if (this._typeDefinition == null)
			{
				throw new Exception("Could not find type definition of container type: " + this.Type);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006514 File Offset: 0x00004714
		public void CollectChildren()
		{
			this._keys = new ElementSaveData[this._elementCount];
			this._values = new ElementSaveData[this._elementCount];
			if (this._containerType == ContainerType.Dictionary)
			{
				IDictionary dictionary = (IDictionary)this.Target;
				int num = 0;
				using (IDictionaryEnumerator enumerator = dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						object key = dictionaryEntry.Key;
						object value = dictionaryEntry.Value;
						ElementSaveData elementSaveData = new ElementSaveData(this, key, num);
						ElementSaveData elementSaveData2 = new ElementSaveData(this, value, this._elementCount + num);
						this._keys[num] = elementSaveData;
						this._values[num] = elementSaveData2;
						num++;
					}
					return;
				}
			}
			if (this._containerType == ContainerType.List || this._containerType == ContainerType.CustomList || this._containerType == ContainerType.CustomReadOnlyList)
			{
				IList list = (IList)this.Target;
				for (int i = 0; i < this._elementCount; i++)
				{
					object value2 = list[i];
					ElementSaveData elementSaveData3 = new ElementSaveData(this, value2, i);
					this._values[i] = elementSaveData3;
				}
				return;
			}
			if (this._containerType == ContainerType.Queue)
			{
				IEnumerable enumerable = (ICollection)this.Target;
				int num2 = 0;
				using (IEnumerator enumerator2 = enumerable.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object value3 = enumerator2.Current;
						ElementSaveData elementSaveData4 = new ElementSaveData(this, value3, num2);
						this._values[num2] = elementSaveData4;
						num2++;
					}
					return;
				}
			}
			if (this._containerType == ContainerType.Array)
			{
				Array array = (Array)this.Target;
				for (int j = 0; j < this._elementCount; j++)
				{
					object value4 = array.GetValue(j);
					ElementSaveData elementSaveData5 = new ElementSaveData(this, value4, j);
					this._values[j] = elementSaveData5;
				}
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006708 File Offset: 0x00004908
		public void SaveHeaderTo(SaveEntryFolder parentFolder, IArchiveContext archiveContext)
		{
			SaveEntryFolder saveEntryFolder = archiveContext.CreateFolder(parentFolder, new FolderId(this.ObjectId, SaveFolderExtension.Container), 1);
			BinaryWriter binaryWriter = BinaryWriterFactory.GetBinaryWriter();
			this._typeDefinition.SaveId.WriteTo(binaryWriter);
			binaryWriter.WriteByte((byte)this._containerType);
			binaryWriter.WriteInt(this.GetElementCount());
			saveEntryFolder.CreateEntry(new EntryId(-1, SaveEntryExtension.Object)).FillFrom(binaryWriter);
			BinaryWriterFactory.ReleaseBinaryWriter(binaryWriter);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006774 File Offset: 0x00004974
		public void SaveTo(SaveEntryFolder parentFolder, IArchiveContext archiveContext)
		{
			int entryCount = (this._containerType == ContainerType.Dictionary) ? (this._elementCount * 2) : this._elementCount;
			SaveEntryFolder saveEntryFolder = archiveContext.CreateFolder(parentFolder, new FolderId(this.ObjectId, SaveFolderExtension.Container), entryCount);
			for (int i = 0; i < this._elementCount; i++)
			{
				VariableSaveData variableSaveData = this._values[i];
				BinaryWriter binaryWriter = BinaryWriterFactory.GetBinaryWriter();
				variableSaveData.SaveTo(binaryWriter);
				saveEntryFolder.CreateEntry(new EntryId(i, SaveEntryExtension.Value)).FillFrom(binaryWriter);
				BinaryWriterFactory.ReleaseBinaryWriter(binaryWriter);
				if (this._containerType == ContainerType.Dictionary)
				{
					VariableSaveData variableSaveData2 = this._keys[i];
					BinaryWriter binaryWriter2 = BinaryWriterFactory.GetBinaryWriter();
					variableSaveData2.SaveTo(binaryWriter2);
					saveEntryFolder.CreateEntry(new EntryId(i, SaveEntryExtension.Key)).FillFrom(binaryWriter2);
					BinaryWriterFactory.ReleaseBinaryWriter(binaryWriter2);
				}
			}
			foreach (ObjectSaveData objectSaveData in this._childStructs)
			{
				TypeDefinition structDefinition = this.Context.DefinitionContext.GetStructDefinition(objectSaveData.Type);
				ISavedStruct savedStruct;
				if (structDefinition == null || !(structDefinition is StructDefinition) || (savedStruct = (objectSaveData.Target as ISavedStruct)) == null || !savedStruct.IsDefault())
				{
					objectSaveData.SaveTo(saveEntryFolder, archiveContext);
				}
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000068B4 File Offset: 0x00004AB4
		internal int GetElementCount()
		{
			if (this._containerType == ContainerType.List || this._containerType == ContainerType.CustomList || this._containerType == ContainerType.CustomReadOnlyList)
			{
				return ((IList)this.Target).Count;
			}
			if (this._containerType == ContainerType.Queue)
			{
				return ((ICollection)this.Target).Count;
			}
			if (this._containerType == ContainerType.Dictionary)
			{
				return ((IDictionary)this.Target).Count;
			}
			if (this._containerType == ContainerType.Array)
			{
				return ((Array)this.Target).GetLength(0);
			}
			return 0;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006940 File Offset: 0x00004B40
		public void CollectStrings()
		{
			foreach (object obj in this.GetChildString())
			{
				string text = (string)obj;
				this.Context.AddOrGetStringId(text);
			}
			foreach (ObjectSaveData objectSaveData in this._childStructs)
			{
				objectSaveData.CollectStrings();
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000069D8 File Offset: 0x00004BD8
		public void CollectStringsInto(List<string> collection)
		{
			foreach (object obj in this.GetChildString())
			{
				string item = (string)obj;
				collection.Add(item);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006A2C File Offset: 0x00004C2C
		public void CollectStructs()
		{
			foreach (ElementSaveData elementSaveData in this.GetChildElementSaveDatas())
			{
				if (elementSaveData.MemberType == SavedMemberType.CustomStruct)
				{
					object elementValue = elementSaveData.ElementValue;
					ObjectSaveData item = new ObjectSaveData(this.Context, elementSaveData.ElementIndex, elementValue, false);
					this._childStructs.Add(item);
				}
			}
			foreach (ObjectSaveData objectSaveData in this._childStructs)
			{
				objectSaveData.CollectStructs();
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006AE4 File Offset: 0x00004CE4
		public void CollectMembers()
		{
			foreach (ObjectSaveData objectSaveData in this._childStructs)
			{
				objectSaveData.CollectMembers();
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006B34 File Offset: 0x00004D34
		public IEnumerable<ElementSaveData> GetChildElementSaveDatas()
		{
			int num;
			for (int i = 0; i < this._elementCount; i = num + 1)
			{
				ElementSaveData elementSaveData = this._keys[i];
				ElementSaveData value = this._values[i];
				if (elementSaveData != null)
				{
					yield return elementSaveData;
				}
				if (value != null)
				{
					yield return value;
				}
				value = null;
				num = i;
			}
			yield break;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006B44 File Offset: 0x00004D44
		public IEnumerable<object> GetChildElements()
		{
			return ContainerSaveData.GetChildElements(this._containerType, this.Target);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006B57 File Offset: 0x00004D57
		public static IEnumerable<object> GetChildElements(ContainerType containerType, object target)
		{
			if (containerType == ContainerType.List || containerType == ContainerType.CustomList || containerType == ContainerType.CustomReadOnlyList)
			{
				IList list = (IList)target;
				int num;
				for (int i = 0; i < list.Count; i = num + 1)
				{
					object obj = list[i];
					if (obj != null)
					{
						yield return obj;
					}
					num = i;
				}
				list = null;
			}
			else if (containerType == ContainerType.Queue)
			{
				ICollection collection = (ICollection)target;
				foreach (object obj2 in collection)
				{
					if (obj2 != null)
					{
						yield return obj2;
					}
				}
				IEnumerator enumerator = null;
			}
			else if (containerType == ContainerType.Dictionary)
			{
				IDictionary dictionary = (IDictionary)target;
				foreach (object obj3 in dictionary)
				{
					DictionaryEntry entry = (DictionaryEntry)obj3;
					yield return entry.Key;
					object value = entry.Value;
					if (value != null)
					{
						yield return value;
					}
					entry = default(DictionaryEntry);
				}
				IDictionaryEnumerator dictionaryEnumerator = null;
			}
			else if (containerType == ContainerType.Array)
			{
				Array array = (Array)target;
				int num;
				for (int i = 0; i < array.Length; i = num + 1)
				{
					object value2 = array.GetValue(i);
					if (value2 != null)
					{
						yield return value2;
					}
					num = i;
				}
				array = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006B70 File Offset: 0x00004D70
		public IEnumerable<object> GetChildObjects(ISaveContext context)
		{
			List<object> list = new List<object>();
			ContainerSaveData.GetChildObjects(context, this._typeDefinition, this._containerType, this.Target, list);
			return list;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public static void GetChildObjects(ISaveContext context, ContainerDefinition containerDefinition, ContainerType containerType, object target, List<object> collectedObjects)
		{
			if (containerDefinition.CollectObjectsMethod != null)
			{
				if (!containerDefinition.HasNoChildObject)
				{
					containerDefinition.CollectObjectsMethod(target, collectedObjects);
					return;
				}
			}
			else
			{
				if (containerType == ContainerType.List || containerType == ContainerType.CustomList || containerType == ContainerType.CustomReadOnlyList)
				{
					IList list = (IList)target;
					for (int i = 0; i < list.Count; i++)
					{
						object obj = list[i];
						if (obj != null)
						{
							ContainerSaveData.ProcessChildObjectElement(obj, context, collectedObjects);
						}
					}
					return;
				}
				if (containerType == ContainerType.Queue)
				{
					using (IEnumerator enumerator = ((ICollection)target).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj2 = enumerator.Current;
							if (obj2 != null)
							{
								ContainerSaveData.ProcessChildObjectElement(obj2, context, collectedObjects);
							}
						}
						return;
					}
				}
				if (containerType == ContainerType.Dictionary)
				{
					using (IDictionaryEnumerator enumerator2 = ((IDictionary)target).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj3 = enumerator2.Current;
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
							ContainerSaveData.ProcessChildObjectElement(dictionaryEntry.Key, context, collectedObjects);
							object value = dictionaryEntry.Value;
							if (value != null)
							{
								ContainerSaveData.ProcessChildObjectElement(value, context, collectedObjects);
							}
						}
						return;
					}
				}
				if (containerType == ContainerType.Array)
				{
					Array array = (Array)target;
					for (int j = 0; j < array.Length; j++)
					{
						object value2 = array.GetValue(j);
						if (value2 != null)
						{
							ContainerSaveData.ProcessChildObjectElement(value2, context, collectedObjects);
						}
					}
					return;
				}
				foreach (object childElement in ContainerSaveData.GetChildElements(containerType, target))
				{
					ContainerSaveData.ProcessChildObjectElement(childElement, context, collectedObjects);
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006D58 File Offset: 0x00004F58
		private static void ProcessChildObjectElement(object childElement, ISaveContext context, List<object> collectedObjects)
		{
			Type type = childElement.GetType();
			bool isClass = type.IsClass;
			if (isClass && type != typeof(string))
			{
				collectedObjects.Add(childElement);
				return;
			}
			if (!isClass)
			{
				TypeDefinition structDefinition = context.DefinitionContext.GetStructDefinition(type);
				if (structDefinition != null)
				{
					if (structDefinition.CollectObjectsMethod != null)
					{
						structDefinition.CollectObjectsMethod(childElement, collectedObjects);
						return;
					}
					for (int i = 0; i < structDefinition.MemberDefinitions.Count; i++)
					{
						MemberDefinition memberDefinition = structDefinition.MemberDefinitions[i];
						ObjectSaveData.GetChildObjectFrom(context, childElement, memberDefinition, collectedObjects);
					}
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00006DE7 File Offset: 0x00004FE7
		private IEnumerable<object> GetChildString()
		{
			foreach (object obj in this.GetChildElements())
			{
				if (obj.GetType() == typeof(string))
				{
					yield return obj;
				}
			}
			IEnumerator<object> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000057 RID: 87
		private ContainerType _containerType;

		// Token: 0x04000058 RID: 88
		private ElementSaveData[] _keys;

		// Token: 0x04000059 RID: 89
		private ElementSaveData[] _values;

		// Token: 0x0400005A RID: 90
		private ContainerDefinition _typeDefinition;

		// Token: 0x0400005B RID: 91
		private int _elementCount;

		// Token: 0x0400005C RID: 92
		private List<ObjectSaveData> _childStructs;
	}
}
