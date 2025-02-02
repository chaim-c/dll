using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x0200003D RID: 61
	public class ObjectLoadData
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00009F02 File Offset: 0x00008102
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00009F0A File Offset: 0x0000810A
		public int Id { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00009F13 File Offset: 0x00008113
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00009F1B File Offset: 0x0000811B
		public object Target { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009F24 File Offset: 0x00008124
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00009F2C File Offset: 0x0000812C
		public LoadContext Context { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00009F35 File Offset: 0x00008135
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00009F3D File Offset: 0x0000813D
		public TypeDefinition TypeDefinition { get; private set; }

		// Token: 0x06000233 RID: 563 RVA: 0x00009F48 File Offset: 0x00008148
		public object GetDataBySaveId(int localSaveId)
		{
			MemberLoadData memberLoadData = this._memberValues.SingleOrDefault((MemberLoadData value) => (int)value.MemberSaveId.LocalSaveId == localSaveId);
			if (memberLoadData != null)
			{
				return memberLoadData.GetDataToUse();
			}
			return null;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009F88 File Offset: 0x00008188
		public object GetMemberValueBySaveId(int localSaveId)
		{
			MemberLoadData memberLoadData = this._memberValues.SingleOrDefault((MemberLoadData value) => (int)value.MemberSaveId.LocalSaveId == localSaveId);
			if (memberLoadData == null)
			{
				return null;
			}
			return memberLoadData.GetDataToUse();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009FC4 File Offset: 0x000081C4
		public object GetFieldValueBySaveId(int localSaveId)
		{
			FieldLoadData fieldLoadData = this._fieldValues.SingleOrDefault((FieldLoadData value) => (int)value.MemberSaveId.LocalSaveId == localSaveId);
			if (fieldLoadData == null)
			{
				return null;
			}
			return fieldLoadData.GetDataToUse();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A000 File Offset: 0x00008200
		public object GetPropertyValueBySaveId(int localSaveId)
		{
			PropertyLoadData propertyLoadData = this._propertyValues.SingleOrDefault((PropertyLoadData value) => (int)value.MemberSaveId.LocalSaveId == localSaveId);
			if (propertyLoadData == null)
			{
				return null;
			}
			return propertyLoadData.GetDataToUse();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A03C File Offset: 0x0000823C
		public bool HasMember(int localSaveId)
		{
			return this._memberValues.Any((MemberLoadData x) => (int)x.MemberSaveId.LocalSaveId == localSaveId);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A070 File Offset: 0x00008270
		public ObjectLoadData(LoadContext context, int id)
		{
			this.Context = context;
			this.Id = id;
			this._propertyValues = new List<PropertyLoadData>();
			this._fieldValues = new List<FieldLoadData>();
			this._memberValues = new List<MemberLoadData>();
			this._childStructs = new List<ObjectLoadData>();
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A0C0 File Offset: 0x000082C0
		public ObjectLoadData(ObjectHeaderLoadData headerLoadData)
		{
			this.Id = headerLoadData.Id;
			this.Target = headerLoadData.Target;
			this.Context = headerLoadData.Context;
			this.TypeDefinition = headerLoadData.TypeDefinition;
			this._propertyValues = new List<PropertyLoadData>();
			this._fieldValues = new List<FieldLoadData>();
			this._memberValues = new List<MemberLoadData>();
			this._childStructs = new List<ObjectLoadData>();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A130 File Offset: 0x00008330
		public void InitializeReaders(SaveEntryFolder saveEntryFolder)
		{
			BinaryReader binaryReader = saveEntryFolder.GetEntry(new EntryId(-1, SaveEntryExtension.Basics)).GetBinaryReader();
			this._saveId = SaveId.ReadSaveIdFrom(binaryReader);
			this._propertyCount = binaryReader.ReadShort();
			this._childStructCount = binaryReader.ReadShort();
			for (int i = 0; i < (int)this._childStructCount; i++)
			{
				ObjectLoadData item = new ObjectLoadData(this.Context, i);
				this._childStructs.Add(item);
			}
			foreach (SaveEntry saveEntry in saveEntryFolder.ChildEntries)
			{
				if (saveEntry.Id.Extension == SaveEntryExtension.Property)
				{
					BinaryReader binaryReader2 = saveEntry.GetBinaryReader();
					PropertyLoadData item2 = new PropertyLoadData(this, binaryReader2);
					this._propertyValues.Add(item2);
					this._memberValues.Add(item2);
				}
				else if (saveEntry.Id.Extension == SaveEntryExtension.Field)
				{
					BinaryReader binaryReader3 = saveEntry.GetBinaryReader();
					FieldLoadData item3 = new FieldLoadData(this, binaryReader3);
					this._fieldValues.Add(item3);
					this._memberValues.Add(item3);
				}
			}
			for (int j = 0; j < (int)this._childStructCount; j++)
			{
				ObjectLoadData objectLoadData = this._childStructs[j];
				SaveEntryFolder childFolder = saveEntryFolder.GetChildFolder(new FolderId(j, SaveFolderExtension.Struct));
				objectLoadData.InitializeReaders(childFolder);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public void CreateStruct()
		{
			this.TypeDefinition = (this.Context.DefinitionContext.TryGetTypeDefinition(this._saveId) as TypeDefinition);
			if (this.TypeDefinition != null)
			{
				Type type = this.TypeDefinition.Type;
				this.Target = FormatterServices.GetUninitializedObject(type);
			}
			foreach (ObjectLoadData objectLoadData in this._childStructs)
			{
				objectLoadData.CreateStruct();
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A334 File Offset: 0x00008534
		public void FillCreatedObject()
		{
			foreach (ObjectLoadData objectLoadData in this._childStructs)
			{
				objectLoadData.CreateStruct();
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A384 File Offset: 0x00008584
		public void Read()
		{
			foreach (ObjectLoadData objectLoadData in this._childStructs)
			{
				objectLoadData.Read();
			}
			foreach (MemberLoadData memberLoadData in this._memberValues)
			{
				memberLoadData.Read();
				if (memberLoadData.SavedMemberType == SavedMemberType.CustomStruct)
				{
					int index = (int)memberLoadData.Data;
					object target = this._childStructs[index].Target;
					memberLoadData.SetCustomStructData(target);
				}
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A448 File Offset: 0x00008648
		public void FillObject()
		{
			foreach (ObjectLoadData objectLoadData in this._childStructs)
			{
				objectLoadData.FillObject();
			}
			foreach (FieldLoadData fieldLoadData in this._fieldValues)
			{
				fieldLoadData.FillObject();
			}
			foreach (PropertyLoadData propertyLoadData in this._propertyValues)
			{
				propertyLoadData.FillObject();
			}
		}

		// Token: 0x040000B4 RID: 180
		private short _propertyCount;

		// Token: 0x040000B5 RID: 181
		private List<PropertyLoadData> _propertyValues;

		// Token: 0x040000B6 RID: 182
		private List<FieldLoadData> _fieldValues;

		// Token: 0x040000B7 RID: 183
		private List<MemberLoadData> _memberValues;

		// Token: 0x040000B8 RID: 184
		private SaveId _saveId;

		// Token: 0x040000B9 RID: 185
		private List<ObjectLoadData> _childStructs;

		// Token: 0x040000BA RID: 186
		private short _childStructCount;
	}
}
