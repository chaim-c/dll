using System;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Save
{
	// Token: 0x02000030 RID: 48
	internal abstract class VariableSaveData
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000084EC File Offset: 0x000066EC
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000084F4 File Offset: 0x000066F4
		public ISaveContext Context { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000084FD File Offset: 0x000066FD
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00008505 File Offset: 0x00006705
		public SavedMemberType MemberType { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000850E File Offset: 0x0000670E
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00008516 File Offset: 0x00006716
		public object Value { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000851F File Offset: 0x0000671F
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00008527 File Offset: 0x00006727
		public MemberTypeId MemberSaveId { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00008530 File Offset: 0x00006730
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x00008538 File Offset: 0x00006738
		public TypeDefinitionBase TypeDefinition { get; private set; }

		// Token: 0x060001C2 RID: 450 RVA: 0x00008541 File Offset: 0x00006741
		protected VariableSaveData(ISaveContext context)
		{
			this.Context = context;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008550 File Offset: 0x00006750
		protected void InitializeDataAsNullObject(MemberTypeId memberSaveId)
		{
			this.MemberSaveId = memberSaveId;
			this.MemberType = SavedMemberType.Object;
			this.Value = -1;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000856C File Offset: 0x0000676C
		protected void InitializeDataAsCustomStruct(MemberTypeId memberSaveId, int structId, TypeDefinitionBase typeDefinition)
		{
			this.MemberSaveId = memberSaveId;
			this.MemberType = SavedMemberType.CustomStruct;
			this.Value = structId;
			this.TypeDefinition = typeDefinition;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008590 File Offset: 0x00006790
		protected void InitializeData(MemberTypeId memberSaveId, Type memberType, TypeDefinitionBase definition, object data)
		{
			this.MemberSaveId = memberSaveId;
			this.TypeDefinition = definition;
			TypeDefinition typeDefinition = this.TypeDefinition as TypeDefinition;
			if (this.TypeDefinition is ContainerDefinition)
			{
				int num = -1;
				if (data != null)
				{
					num = this.Context.GetContainerId(data);
				}
				this.MemberType = SavedMemberType.Container;
				this.Value = num;
			}
			else if (typeof(string) == memberType)
			{
				this.MemberType = SavedMemberType.String;
				this.Value = data;
			}
			else if ((typeDefinition != null && typeDefinition.IsClassDefinition) || this.TypeDefinition is InterfaceDefinition || (this.TypeDefinition == null && memberType.IsInterface))
			{
				int num2 = -1;
				if (data != null)
				{
					num2 = this.Context.GetObjectId(data);
				}
				this.MemberType = SavedMemberType.Object;
				this.Value = num2;
			}
			else if (this.TypeDefinition is EnumDefinition)
			{
				this.MemberType = SavedMemberType.Enum;
				this.Value = data;
			}
			else if (this.TypeDefinition is BasicTypeDefinition)
			{
				this.MemberType = SavedMemberType.BasicType;
				this.Value = data;
			}
			else
			{
				this.MemberType = SavedMemberType.CustomStruct;
				this.Value = data;
			}
			if (this.TypeDefinition == null && !memberType.IsInterface)
			{
				string message = string.Format("Cant find definition for: {0}. Save id: {1}", memberType.Name, this.MemberSaveId);
				Debug.Print(message, 0, Debug.DebugColor.Red, 17592186044416UL);
				Debug.FailedAssert(message, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.SaveSystem\\Save\\VariableSaveData.cs", "InitializeData", 97);
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008700 File Offset: 0x00006900
		public void SaveTo(IWriter writer)
		{
			writer.WriteByte((byte)this.MemberType);
			writer.WriteByte(this.MemberSaveId.TypeLevel);
			writer.WriteShort(this.MemberSaveId.LocalSaveId);
			if (this.MemberType == SavedMemberType.Object)
			{
				writer.WriteInt((int)this.Value);
				return;
			}
			if (this.MemberType == SavedMemberType.Container)
			{
				writer.WriteInt((int)this.Value);
				return;
			}
			if (this.MemberType == SavedMemberType.String)
			{
				int stringId = this.Context.GetStringId((string)this.Value);
				writer.WriteInt(stringId);
				return;
			}
			if (this.MemberType == SavedMemberType.Enum)
			{
				this.TypeDefinition.SaveId.WriteTo(writer);
				writer.WriteString(this.Value.ToString());
				return;
			}
			if (this.MemberType == SavedMemberType.BasicType)
			{
				this.TypeDefinition.SaveId.WriteTo(writer);
				((BasicTypeDefinition)this.TypeDefinition).Serializer.Serialize(writer, this.Value);
				return;
			}
			if (this.MemberType == SavedMemberType.CustomStruct)
			{
				writer.WriteInt((int)this.Value);
			}
		}
	}
}
