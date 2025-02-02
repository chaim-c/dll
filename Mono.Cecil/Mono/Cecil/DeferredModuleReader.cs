using System;
using Mono.Cecil.PE;

namespace Mono.Cecil
{
	// Token: 0x02000062 RID: 98
	internal sealed class DeferredModuleReader : ModuleReader
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000D044 File Offset: 0x0000B244
		public DeferredModuleReader(Image image) : base(image, ReadingMode.Deferred)
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000D058 File Offset: 0x0000B258
		protected override void ReadModule()
		{
			this.module.Read<ModuleDefinition, ModuleDefinition>(this.module, delegate(ModuleDefinition module, MetadataReader reader)
			{
				base.ReadModuleManifest(reader);
				return module;
			});
		}
	}
}
