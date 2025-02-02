using System;
using System.Runtime.Serialization;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x0200003C RID: 60
	public class ObjectHeaderLoadData
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00009DA2 File Offset: 0x00007FA2
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00009DAA File Offset: 0x00007FAA
		public int Id { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00009DB3 File Offset: 0x00007FB3
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00009DBB File Offset: 0x00007FBB
		public object LoadedObject { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00009DC4 File Offset: 0x00007FC4
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00009DCC File Offset: 0x00007FCC
		public object Target { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00009DD5 File Offset: 0x00007FD5
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00009DDD File Offset: 0x00007FDD
		public int PropertyCount { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00009DE6 File Offset: 0x00007FE6
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00009DEE File Offset: 0x00007FEE
		public int ChildStructCount { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00009DF7 File Offset: 0x00007FF7
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00009DFF File Offset: 0x00007FFF
		public TypeDefinition TypeDefinition { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00009E08 File Offset: 0x00008008
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00009E10 File Offset: 0x00008010
		public LoadContext Context { get; private set; }

		// Token: 0x06000226 RID: 550 RVA: 0x00009E19 File Offset: 0x00008019
		public ObjectHeaderLoadData(LoadContext context, int id)
		{
			this.Context = context;
			this.Id = id;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009E30 File Offset: 0x00008030
		public void InitialieReaders(SaveEntryFolder saveEntryFolder)
		{
			BinaryReader binaryReader = saveEntryFolder.GetEntry(new EntryId(-1, SaveEntryExtension.Basics)).GetBinaryReader();
			this._saveId = SaveId.ReadSaveIdFrom(binaryReader);
			this.PropertyCount = (int)binaryReader.ReadShort();
			this.ChildStructCount = (int)binaryReader.ReadShort();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009E74 File Offset: 0x00008074
		public void CreateObject()
		{
			this.TypeDefinition = (this.Context.DefinitionContext.TryGetTypeDefinition(this._saveId) as TypeDefinition);
			if (this.TypeDefinition != null)
			{
				Type type = this.TypeDefinition.Type;
				this.LoadedObject = FormatterServices.GetUninitializedObject(type);
				this.Target = this.LoadedObject;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009ECE File Offset: 0x000080CE
		public void AdvancedResolveObject(MetaData metaData, ObjectLoadData objectLoadData)
		{
			this.Target = this.TypeDefinition.AdvancedResolveObject(this.LoadedObject, metaData, objectLoadData);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009EE9 File Offset: 0x000080E9
		public void ResolveObject()
		{
			this.Target = this.TypeDefinition.ResolveObject(this.LoadedObject);
		}

		// Token: 0x040000AF RID: 175
		private SaveId _saveId;
	}
}
