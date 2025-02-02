using System;
using TaleWorlds.SaveSystem.Load;

namespace TaleWorlds.SaveSystem.Resolvers
{
	// Token: 0x02000032 RID: 50
	public interface IObjectResolver
	{
		// Token: 0x060001C8 RID: 456
		bool CheckIfRequiresAdvancedResolving(object originalObject);

		// Token: 0x060001C9 RID: 457
		object ResolveObject(object originalObject);

		// Token: 0x060001CA RID: 458
		object AdvancedResolveObject(object originalObject, MetaData metaData, ObjectLoadData objectLoadData);
	}
}
