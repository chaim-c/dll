using System;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x0200003F RID: 63
	internal abstract class VariableLoadData
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000A5B6 File Offset: 0x000087B6
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000A5BE File Offset: 0x000087BE
		public LoadContext Context { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A5C7 File Offset: 0x000087C7
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000A5CF File Offset: 0x000087CF
		public MemberTypeId MemberSaveId { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000A5D8 File Offset: 0x000087D8
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000A5E0 File Offset: 0x000087E0
		public SavedMemberType SavedMemberType { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000A5E9 File Offset: 0x000087E9
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000A5F1 File Offset: 0x000087F1
		public object Data { get; private set; }

		// Token: 0x06000249 RID: 585 RVA: 0x0000A5FA File Offset: 0x000087FA
		protected VariableLoadData(LoadContext context, IReader reader)
		{
			this.Context = context;
			this._reader = reader;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000A610 File Offset: 0x00008810
		public void Read()
		{
			this.SavedMemberType = (SavedMemberType)this._reader.ReadByte();
			this.MemberSaveId = new MemberTypeId
			{
				TypeLevel = this._reader.ReadByte(),
				LocalSaveId = this._reader.ReadShort()
			};
			if (this.SavedMemberType == SavedMemberType.Object)
			{
				this.Data = this._reader.ReadInt();
				return;
			}
			if (this.SavedMemberType == SavedMemberType.Container)
			{
				this.Data = this._reader.ReadInt();
				return;
			}
			if (this.SavedMemberType == SavedMemberType.String)
			{
				this.Data = this._reader.ReadInt();
				return;
			}
			if (this.SavedMemberType == SavedMemberType.Enum)
			{
				this._saveId = SaveId.ReadSaveIdFrom(this._reader);
				this._typeDefinition = this.Context.DefinitionContext.TryGetTypeDefinition(this._saveId);
				string text = this._reader.ReadString();
				EnumDefinition enumDefinition = (EnumDefinition)this._typeDefinition;
				if (((enumDefinition != null) ? enumDefinition.Resolver : null) != null)
				{
					this.Data = enumDefinition.Resolver.ResolveObject(text);
					return;
				}
				this.Data = text;
				return;
			}
			else
			{
				if (this.SavedMemberType == SavedMemberType.BasicType)
				{
					this._saveId = SaveId.ReadSaveIdFrom(this._reader);
					this._typeDefinition = this.Context.DefinitionContext.TryGetTypeDefinition(this._saveId);
					BasicTypeDefinition basicTypeDefinition = (BasicTypeDefinition)this._typeDefinition;
					this.Data = basicTypeDefinition.Serializer.Deserialize(this._reader);
					return;
				}
				if (this.SavedMemberType == SavedMemberType.CustomStruct)
				{
					this.Data = this._reader.ReadInt();
				}
				return;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000A7B0 File Offset: 0x000089B0
		public void SetCustomStructData(object customStructObject)
		{
			this._customStructObject = customStructObject;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000A7BC File Offset: 0x000089BC
		public object GetDataToUse()
		{
			object result = null;
			if (this.SavedMemberType == SavedMemberType.Object)
			{
				ObjectHeaderLoadData objectWithId = this.Context.GetObjectWithId((int)this.Data);
				if (objectWithId != null)
				{
					result = objectWithId.Target;
				}
			}
			else if (this.SavedMemberType == SavedMemberType.Container)
			{
				ContainerHeaderLoadData containerWithId = this.Context.GetContainerWithId((int)this.Data);
				if (containerWithId != null)
				{
					result = containerWithId.Target;
				}
			}
			else if (this.SavedMemberType == SavedMemberType.String)
			{
				int id = (int)this.Data;
				result = this.Context.GetStringWithId(id);
			}
			else if (this.SavedMemberType == SavedMemberType.Enum)
			{
				if (this._typeDefinition == null)
				{
					result = (string)this.Data;
				}
				else
				{
					Type type = this._typeDefinition.Type;
					if (Enum.IsDefined(type, this.Data))
					{
						result = Enum.Parse(type, (string)this.Data);
					}
				}
			}
			else if (this.SavedMemberType == SavedMemberType.BasicType)
			{
				result = this.Data;
			}
			else if (this.SavedMemberType == SavedMemberType.CustomStruct)
			{
				result = this._customStructObject;
			}
			return result;
		}

		// Token: 0x040000BB RID: 187
		private IReader _reader;

		// Token: 0x040000C0 RID: 192
		private TypeDefinitionBase _typeDefinition;

		// Token: 0x040000C1 RID: 193
		private SaveId _saveId;

		// Token: 0x040000C2 RID: 194
		private object _customStructObject;
	}
}
