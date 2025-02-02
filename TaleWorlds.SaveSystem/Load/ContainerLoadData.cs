using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x02000034 RID: 52
	internal class ContainerLoadData
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00008991 File Offset: 0x00006B91
		public int Id
		{
			get
			{
				return this.ContainerHeaderLoadData.Id;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000899E File Offset: 0x00006B9E
		public object Target
		{
			get
			{
				return this.ContainerHeaderLoadData.Target;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000089AB File Offset: 0x00006BAB
		public LoadContext Context
		{
			get
			{
				return this.ContainerHeaderLoadData.Context;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000089B8 File Offset: 0x00006BB8
		public ContainerDefinition TypeDefinition
		{
			get
			{
				return this.ContainerHeaderLoadData.TypeDefinition;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000089C5 File Offset: 0x00006BC5
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000089CD File Offset: 0x00006BCD
		public ContainerHeaderLoadData ContainerHeaderLoadData { get; private set; }

		// Token: 0x060001E3 RID: 483 RVA: 0x000089D8 File Offset: 0x00006BD8
		public ContainerLoadData(ContainerHeaderLoadData headerLoadData)
		{
			this.ContainerHeaderLoadData = headerLoadData;
			this._childStructs = new Dictionary<int, ObjectLoadData>();
			this._saveId = headerLoadData.SaveId;
			this._containerType = headerLoadData.ContainerType;
			this._elementCount = headerLoadData.ElementCount;
			this._keys = new ElementLoadData[this._elementCount];
			this._values = new ElementLoadData[this._elementCount];
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008A44 File Offset: 0x00006C44
		private FolderId[] GetChildStructNames(SaveEntryFolder saveEntryFolder)
		{
			List<FolderId> list = new List<FolderId>();
			foreach (SaveEntryFolder saveEntryFolder2 in saveEntryFolder.ChildFolders)
			{
				if (saveEntryFolder2.FolderId.Extension == SaveFolderExtension.Struct && !list.Contains(saveEntryFolder2.FolderId))
				{
					list.Add(saveEntryFolder2.FolderId);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008AC8 File Offset: 0x00006CC8
		public void InitializeReaders(SaveEntryFolder saveEntryFolder)
		{
			foreach (FolderId folderId in this.GetChildStructNames(saveEntryFolder))
			{
				int localId = folderId.LocalId;
				ObjectLoadData value = new ObjectLoadData(this.Context, localId);
				this._childStructs.Add(localId, value);
			}
			for (int j = 0; j < this._elementCount; j++)
			{
				BinaryReader binaryReader = saveEntryFolder.GetEntry(new EntryId(j, SaveEntryExtension.Value)).GetBinaryReader();
				ElementLoadData elementLoadData = new ElementLoadData(this, binaryReader);
				this._values[j] = elementLoadData;
				if (this._containerType == ContainerType.Dictionary)
				{
					BinaryReader binaryReader2 = saveEntryFolder.GetEntry(new EntryId(j, SaveEntryExtension.Key)).GetBinaryReader();
					ElementLoadData elementLoadData2 = new ElementLoadData(this, binaryReader2);
					this._keys[j] = elementLoadData2;
				}
			}
			foreach (KeyValuePair<int, ObjectLoadData> keyValuePair in this._childStructs)
			{
				int key = keyValuePair.Key;
				ObjectLoadData value2 = keyValuePair.Value;
				SaveEntryFolder childFolder = saveEntryFolder.GetChildFolder(new FolderId(key, SaveFolderExtension.Struct));
				value2.InitializeReaders(childFolder);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008BF4 File Offset: 0x00006DF4
		public void FillCreatedObject()
		{
			foreach (ObjectLoadData objectLoadData in this._childStructs.Values)
			{
				objectLoadData.CreateStruct();
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008C4C File Offset: 0x00006E4C
		public void Read()
		{
			for (int i = 0; i < this._elementCount; i++)
			{
				this._values[i].Read();
				if (this._containerType == ContainerType.Dictionary)
				{
					this._keys[i].Read();
				}
			}
			foreach (ObjectLoadData objectLoadData in this._childStructs.Values)
			{
				objectLoadData.Read();
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008CD8 File Offset: 0x00006ED8
		private static Assembly GetAssemblyByName(string name)
		{
			return AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault((Assembly assembly) => assembly.GetName().FullName == name);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008D10 File Offset: 0x00006F10
		public void FillObject()
		{
			foreach (ObjectLoadData objectLoadData in this._childStructs.Values)
			{
				objectLoadData.FillObject();
			}
			for (int i = 0; i < this._elementCount; i++)
			{
				if (this._containerType == ContainerType.List || this._containerType == ContainerType.CustomList || this._containerType == ContainerType.CustomReadOnlyList)
				{
					IList list = (IList)this.Target;
					ElementLoadData elementLoadData = this._values[i];
					if (elementLoadData.SavedMemberType == SavedMemberType.CustomStruct)
					{
						int key = (int)elementLoadData.Data;
						ObjectLoadData objectLoadData2;
						object customStructData;
						if (this._childStructs.TryGetValue(key, out objectLoadData2))
						{
							customStructData = objectLoadData2.Target;
						}
						else
						{
							customStructData = ContainerLoadData.GetDefaultObject(this._saveId, this.Context, false);
						}
						elementLoadData.SetCustomStructData(customStructData);
					}
					object dataToUse = elementLoadData.GetDataToUse();
					if (list != null)
					{
						list.Add(dataToUse);
					}
				}
				else if (this._containerType == ContainerType.Dictionary)
				{
					IDictionary dictionary = (IDictionary)this.Target;
					ElementLoadData elementLoadData2 = this._keys[i];
					ElementLoadData elementLoadData3 = this._values[i];
					if (elementLoadData2.SavedMemberType == SavedMemberType.CustomStruct)
					{
						int key2 = (int)elementLoadData2.Data;
						ObjectLoadData objectLoadData3;
						object customStructData2;
						if (this._childStructs.TryGetValue(key2, out objectLoadData3))
						{
							customStructData2 = objectLoadData3.Target;
						}
						else
						{
							customStructData2 = ContainerLoadData.GetDefaultObject(this._saveId, this.Context, false);
						}
						elementLoadData2.SetCustomStructData(customStructData2);
					}
					if (elementLoadData3.SavedMemberType == SavedMemberType.CustomStruct)
					{
						int key3 = (int)elementLoadData3.Data;
						ObjectLoadData objectLoadData4;
						object customStructData3;
						if (this._childStructs.TryGetValue(key3, out objectLoadData4))
						{
							customStructData3 = objectLoadData4.Target;
						}
						else
						{
							customStructData3 = ContainerLoadData.GetDefaultObject(this._saveId, this.Context, true);
						}
						elementLoadData3.SetCustomStructData(customStructData3);
					}
					object dataToUse2 = elementLoadData2.GetDataToUse();
					object dataToUse3 = elementLoadData3.GetDataToUse();
					if (dictionary != null && dataToUse2 != null)
					{
						dictionary.Add(dataToUse2, dataToUse3);
					}
				}
				else if (this._containerType == ContainerType.Array)
				{
					Array array = (Array)this.Target;
					ElementLoadData elementLoadData4 = this._values[i];
					if (elementLoadData4.SavedMemberType == SavedMemberType.CustomStruct)
					{
						int key4 = (int)elementLoadData4.Data;
						ObjectLoadData objectLoadData5;
						object customStructData4;
						if (this._childStructs.TryGetValue(key4, out objectLoadData5))
						{
							customStructData4 = objectLoadData5.Target;
						}
						else
						{
							customStructData4 = ContainerLoadData.GetDefaultObject(this._saveId, this.Context, false);
						}
						elementLoadData4.SetCustomStructData(customStructData4);
					}
					object dataToUse4 = elementLoadData4.GetDataToUse();
					array.SetValue(dataToUse4, i);
				}
				else if (this._containerType == ContainerType.Queue)
				{
					ICollection collection = (ICollection)this.Target;
					ElementLoadData elementLoadData5 = this._values[i];
					if (elementLoadData5.SavedMemberType == SavedMemberType.CustomStruct)
					{
						int key5 = (int)elementLoadData5.Data;
						ObjectLoadData objectLoadData6;
						object customStructData5;
						if (this._childStructs.TryGetValue(key5, out objectLoadData6))
						{
							customStructData5 = objectLoadData6.Target;
						}
						else
						{
							customStructData5 = ContainerLoadData.GetDefaultObject(this._saveId, this.Context, false);
						}
						elementLoadData5.SetCustomStructData(customStructData5);
					}
					object dataToUse5 = elementLoadData5.GetDataToUse();
					collection.GetType().GetMethod("Enqueue").Invoke(collection, new object[]
					{
						dataToUse5
					});
				}
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000904C File Offset: 0x0000724C
		private static object GetDefaultObject(SaveId saveId, LoadContext context, bool getValueId = false)
		{
			ContainerSaveId containerSaveId = (ContainerSaveId)saveId;
			TypeDefinitionBase typeDefinitionBase;
			if (getValueId)
			{
				typeDefinitionBase = context.DefinitionContext.TryGetTypeDefinition(containerSaveId.ValueId);
			}
			else
			{
				typeDefinitionBase = context.DefinitionContext.TryGetTypeDefinition(containerSaveId.KeyId);
			}
			return Activator.CreateInstance(((StructDefinition)typeDefinitionBase).Type);
		}

		// Token: 0x0400008D RID: 141
		private SaveId _saveId;

		// Token: 0x0400008E RID: 142
		private int _elementCount;

		// Token: 0x0400008F RID: 143
		private ContainerType _containerType;

		// Token: 0x04000090 RID: 144
		private ElementLoadData[] _keys;

		// Token: 0x04000091 RID: 145
		private ElementLoadData[] _values;

		// Token: 0x04000092 RID: 146
		private Dictionary<int, ObjectLoadData> _childStructs;
	}
}
