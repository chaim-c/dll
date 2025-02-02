using System;
using System.Reflection;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000057 RID: 87
	public class ContainerDefinition : TypeDefinitionBase
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000AD9C File Offset: 0x00008F9C
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		public Assembly DefinedAssembly { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000ADAD File Offset: 0x00008FAD
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000ADB5 File Offset: 0x00008FB5
		public CollectObjectsDelegate CollectObjectsMethod { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000ADBE File Offset: 0x00008FBE
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000ADC6 File Offset: 0x00008FC6
		public bool HasNoChildObject { get; private set; }

		// Token: 0x06000297 RID: 663 RVA: 0x0000ADCF File Offset: 0x00008FCF
		public ContainerDefinition(Type type, ContainerSaveId saveId, Assembly definedAssembly) : base(type, saveId)
		{
			this.DefinedAssembly = definedAssembly;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		public void InitializeForAutoGeneration(CollectObjectsDelegate collectObjectsDelegate, bool hasNoChildObject)
		{
			this.CollectObjectsMethod = collectObjectsDelegate;
			this.HasNoChildObject = hasNoChildObject;
		}
	}
}
