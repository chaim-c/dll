﻿using System;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x02000033 RID: 51
	public class ContainerHeaderLoadData
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00008816 File Offset: 0x00006A16
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000881E File Offset: 0x00006A1E
		public int Id { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00008827 File Offset: 0x00006A27
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000882F File Offset: 0x00006A2F
		public object Target { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00008838 File Offset: 0x00006A38
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00008840 File Offset: 0x00006A40
		public LoadContext Context { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00008849 File Offset: 0x00006A49
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00008851 File Offset: 0x00006A51
		public ContainerDefinition TypeDefinition { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000885A File Offset: 0x00006A5A
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00008862 File Offset: 0x00006A62
		public SaveId SaveId { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000886B File Offset: 0x00006A6B
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00008873 File Offset: 0x00006A73
		public int ElementCount { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000887C File Offset: 0x00006A7C
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00008884 File Offset: 0x00006A84
		public ContainerType ContainerType { get; private set; }

		// Token: 0x060001D9 RID: 473 RVA: 0x0000888D File Offset: 0x00006A8D
		public ContainerHeaderLoadData(LoadContext context, int id)
		{
			this.Context = context;
			this.Id = id;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000088A3 File Offset: 0x00006AA3
		public bool GetObjectTypeDefinition()
		{
			this.TypeDefinition = (this.Context.DefinitionContext.TryGetTypeDefinition(this.SaveId) as ContainerDefinition);
			return this.TypeDefinition != null;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000088D0 File Offset: 0x00006AD0
		public void CreateObject()
		{
			Type type = this.TypeDefinition.Type;
			if (this.ContainerType == ContainerType.Array)
			{
				this.Target = Activator.CreateInstance(type, new object[]
				{
					this.ElementCount
				});
				return;
			}
			if (this.ContainerType == ContainerType.List)
			{
				this.Target = Activator.CreateInstance(typeof(MBList<>).MakeGenericType(type.GetGenericArguments()));
				return;
			}
			this.Target = Activator.CreateInstance(type);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000894C File Offset: 0x00006B4C
		public void InitialieReaders(SaveEntryFolder saveEntryFolder)
		{
			BinaryReader binaryReader = saveEntryFolder.GetEntry(new EntryId(-1, SaveEntryExtension.Object)).GetBinaryReader();
			this.SaveId = SaveId.ReadSaveIdFrom(binaryReader);
			this.ContainerType = (ContainerType)binaryReader.ReadByte();
			this.ElementCount = binaryReader.ReadInt();
		}
	}
}
