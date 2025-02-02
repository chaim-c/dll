using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Save
{
	// Token: 0x0200002B RID: 43
	internal class ObjectSaveData
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006F57 File Offset: 0x00005157
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00006F5F File Offset: 0x0000515F
		public int ObjectId { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00006F68 File Offset: 0x00005168
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00006F70 File Offset: 0x00005170
		public ISaveContext Context { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00006F79 File Offset: 0x00005179
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00006F81 File Offset: 0x00005181
		public object Target { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00006F8A File Offset: 0x0000518A
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00006F92 File Offset: 0x00005192
		public Type Type { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00006F9B File Offset: 0x0000519B
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00006FA3 File Offset: 0x000051A3
		public bool IsClass { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00006FAC File Offset: 0x000051AC
		internal int PropertyCount
		{
			get
			{
				return this._propertyValues.Count;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00006FB9 File Offset: 0x000051B9
		internal int FieldCount
		{
			get
			{
				return this._fieldValues.Count;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006FC8 File Offset: 0x000051C8
		public ObjectSaveData(ISaveContext context, int objectId, object target, bool isClass)
		{
			this.ObjectId = objectId;
			this.Context = context;
			this.Target = target;
			this.IsClass = isClass;
			this._stringMembers = new List<MemberSaveData>();
			this.Type = target.GetType();
			if (this.IsClass)
			{
				this._typeDefinition = context.DefinitionContext.GetClassDefinition(this.Type);
			}
			else
			{
				this._typeDefinition = context.DefinitionContext.GetStructDefinition(this.Type);
			}
			this._childStructs = new Dictionary<MemberDefinition, ObjectSaveData>(3);
			this._propertyValues = new Dictionary<PropertyInfo, PropertySaveData>(this._typeDefinition.PropertyDefinitions.Count);
			this._fieldValues = new Dictionary<FieldInfo, FieldSaveData>(this._typeDefinition.FieldDefinitions.Count);
			if (this._typeDefinition == null)
			{
				throw new Exception("Could not find type definition of type: " + this.Type);
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000070A8 File Offset: 0x000052A8
		public void CollectMembers()
		{
			for (int i = 0; i < this._typeDefinition.MemberDefinitions.Count; i++)
			{
				MemberDefinition memberDefinition = this._typeDefinition.MemberDefinitions[i];
				MemberSaveData memberSaveData = null;
				if (memberDefinition is PropertyDefinition)
				{
					PropertyDefinition propertyDefinition = (PropertyDefinition)memberDefinition;
					PropertyInfo propertyInfo = propertyDefinition.PropertyInfo;
					MemberTypeId id = propertyDefinition.Id;
					PropertySaveData propertySaveData = new PropertySaveData(this, propertyDefinition, id);
					this._propertyValues.Add(propertyInfo, propertySaveData);
					memberSaveData = propertySaveData;
				}
				else if (memberDefinition is FieldDefinition)
				{
					FieldDefinition fieldDefinition = (FieldDefinition)memberDefinition;
					FieldInfo fieldInfo = fieldDefinition.FieldInfo;
					MemberTypeId id2 = fieldDefinition.Id;
					FieldSaveData fieldSaveData = new FieldSaveData(this, fieldDefinition, id2);
					this._fieldValues.Add(fieldInfo, fieldSaveData);
					memberSaveData = fieldSaveData;
				}
				Type memberType = memberDefinition.GetMemberType();
				TypeDefinitionBase typeDefinition = this.Context.DefinitionContext.GetTypeDefinition(memberType);
				TypeDefinition typeDefinition2 = typeDefinition as TypeDefinition;
				if (typeDefinition2 != null && !typeDefinition2.IsClassDefinition)
				{
					ObjectSaveData objectSaveData = this._childStructs[memberDefinition];
					memberSaveData.InitializeAsCustomStruct(objectSaveData.ObjectId);
				}
				else
				{
					memberSaveData.Initialize(typeDefinition);
				}
				if (memberSaveData.MemberType == SavedMemberType.String)
				{
					this._stringMembers.Add(memberSaveData);
				}
			}
			foreach (ObjectSaveData objectSaveData2 in this._childStructs.Values)
			{
				objectSaveData2.CollectMembers();
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007220 File Offset: 0x00005420
		public void CollectStringsInto(List<string> collection)
		{
			for (int i = 0; i < this._stringMembers.Count; i++)
			{
				string item = (string)this._stringMembers[i].Value;
				collection.Add(item);
			}
			foreach (ObjectSaveData objectSaveData in this._childStructs.Values)
			{
				objectSaveData.CollectStringsInto(collection);
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000072AC File Offset: 0x000054AC
		public void CollectStrings()
		{
			for (int i = 0; i < this._stringMembers.Count; i++)
			{
				string text = (string)this._stringMembers[i].Value;
				this.Context.AddOrGetStringId(text);
			}
			foreach (ObjectSaveData objectSaveData in this._childStructs.Values)
			{
				objectSaveData.CollectStrings();
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000733C File Offset: 0x0000553C
		public void CollectStructs()
		{
			int num = 0;
			for (int i = 0; i < this._typeDefinition.MemberDefinitions.Count; i++)
			{
				MemberDefinition memberDefinition = this._typeDefinition.MemberDefinitions[i];
				Type memberType = memberDefinition.GetMemberType();
				if (this.Context.DefinitionContext.GetStructDefinition(memberType) != null)
				{
					object value = memberDefinition.GetValue(this.Target);
					ObjectSaveData value2 = new ObjectSaveData(this.Context, num, value, false);
					this._childStructs.Add(memberDefinition, value2);
					num++;
				}
			}
			foreach (ObjectSaveData objectSaveData in this._childStructs.Values)
			{
				objectSaveData.CollectStructs();
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000740C File Offset: 0x0000560C
		public void SaveHeaderTo(SaveEntryFolder parentFolder, IArchiveContext archiveContext)
		{
			SaveFolderExtension extension = this.IsClass ? SaveFolderExtension.Object : SaveFolderExtension.Struct;
			SaveEntryFolder saveEntryFolder = archiveContext.CreateFolder(parentFolder, new FolderId(this.ObjectId, extension), 1);
			BinaryWriter binaryWriter = BinaryWriterFactory.GetBinaryWriter();
			this._typeDefinition.SaveId.WriteTo(binaryWriter);
			binaryWriter.WriteShort((short)this._propertyValues.Count);
			binaryWriter.WriteShort((short)this._childStructs.Count);
			saveEntryFolder.CreateEntry(new EntryId(-1, SaveEntryExtension.Basics)).FillFrom(binaryWriter);
			BinaryWriterFactory.ReleaseBinaryWriter(binaryWriter);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007490 File Offset: 0x00005690
		public void SaveTo(SaveEntryFolder parentFolder, IArchiveContext archiveContext)
		{
			SaveFolderExtension extension = this.IsClass ? SaveFolderExtension.Object : SaveFolderExtension.Struct;
			int entryCount = 1 + this._fieldValues.Values.Count + this._propertyValues.Values.Count;
			SaveEntryFolder saveEntryFolder = archiveContext.CreateFolder(parentFolder, new FolderId(this.ObjectId, extension), entryCount);
			BinaryWriter binaryWriter = BinaryWriterFactory.GetBinaryWriter();
			this._typeDefinition.SaveId.WriteTo(binaryWriter);
			binaryWriter.WriteShort((short)this._propertyValues.Count);
			binaryWriter.WriteShort((short)this._childStructs.Count);
			saveEntryFolder.CreateEntry(new EntryId(-1, SaveEntryExtension.Basics)).FillFrom(binaryWriter);
			BinaryWriterFactory.ReleaseBinaryWriter(binaryWriter);
			int num = 0;
			foreach (VariableSaveData variableSaveData in this._fieldValues.Values)
			{
				BinaryWriter binaryWriter2 = BinaryWriterFactory.GetBinaryWriter();
				variableSaveData.SaveTo(binaryWriter2);
				saveEntryFolder.CreateEntry(new EntryId(num, SaveEntryExtension.Field)).FillFrom(binaryWriter2);
				BinaryWriterFactory.ReleaseBinaryWriter(binaryWriter2);
				num++;
			}
			int num2 = 0;
			foreach (VariableSaveData variableSaveData2 in this._propertyValues.Values)
			{
				BinaryWriter binaryWriter3 = BinaryWriterFactory.GetBinaryWriter();
				variableSaveData2.SaveTo(binaryWriter3);
				saveEntryFolder.CreateEntry(new EntryId(num2, SaveEntryExtension.Property)).FillFrom(binaryWriter3);
				BinaryWriterFactory.ReleaseBinaryWriter(binaryWriter3);
				num2++;
			}
			foreach (ObjectSaveData objectSaveData in this._childStructs.Values)
			{
				objectSaveData.SaveTo(saveEntryFolder, archiveContext);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007668 File Offset: 0x00005868
		internal static void GetChildObjectFrom(ISaveContext context, object target, MemberDefinition memberDefinition, List<object> collectedObjects)
		{
			Type memberType = memberDefinition.GetMemberType();
			if (memberType.IsClass || memberType.IsInterface)
			{
				if (memberType != typeof(string))
				{
					object value = memberDefinition.GetValue(target);
					if (value != null)
					{
						collectedObjects.Add(value);
						return;
					}
				}
			}
			else
			{
				TypeDefinition structDefinition = context.DefinitionContext.GetStructDefinition(memberType);
				if (structDefinition != null)
				{
					object value2 = memberDefinition.GetValue(target);
					for (int i = 0; i < structDefinition.MemberDefinitions.Count; i++)
					{
						MemberDefinition memberDefinition2 = structDefinition.MemberDefinitions[i];
						ObjectSaveData.GetChildObjectFrom(context, value2, memberDefinition2, collectedObjects);
					}
				}
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000076FC File Offset: 0x000058FC
		public IEnumerable<object> GetChildObjects()
		{
			List<object> list = new List<object>();
			ObjectSaveData.GetChildObjects(this.Context, this._typeDefinition, this.Target, list);
			return list;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007728 File Offset: 0x00005928
		public static void GetChildObjects(ISaveContext context, TypeDefinition typeDefinition, object target, List<object> collectedObjects)
		{
			if (typeDefinition.CollectObjectsMethod != null)
			{
				typeDefinition.CollectObjectsMethod(target, collectedObjects);
				return;
			}
			for (int i = 0; i < typeDefinition.MemberDefinitions.Count; i++)
			{
				MemberDefinition memberDefinition = typeDefinition.MemberDefinitions[i];
				ObjectSaveData.GetChildObjectFrom(context, target, memberDefinition, collectedObjects);
			}
		}

		// Token: 0x04000066 RID: 102
		private Dictionary<PropertyInfo, PropertySaveData> _propertyValues;

		// Token: 0x04000067 RID: 103
		private Dictionary<FieldInfo, FieldSaveData> _fieldValues;

		// Token: 0x04000068 RID: 104
		private List<MemberSaveData> _stringMembers;

		// Token: 0x04000069 RID: 105
		private TypeDefinition _typeDefinition;

		// Token: 0x0400006A RID: 106
		private Dictionary<MemberDefinition, ObjectSaveData> _childStructs;
	}
}
