using System;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x0200003E RID: 62
	internal class PropertyLoadData : MemberLoadData
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000A518 File Offset: 0x00008718
		public PropertyLoadData(ObjectLoadData objectLoadData, IReader reader) : base(objectLoadData, reader)
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A524 File Offset: 0x00008724
		public void FillObject()
		{
			PropertyDefinition propertyDefinitionWithId;
			if (base.ObjectLoadData.TypeDefinition == null || (propertyDefinitionWithId = base.ObjectLoadData.TypeDefinition.GetPropertyDefinitionWithId(base.MemberSaveId)) == null)
			{
				return;
			}
			MethodInfo setMethod = propertyDefinitionWithId.SetMethod;
			object target = base.ObjectLoadData.Target;
			object dataToUse = base.GetDataToUse();
			if (dataToUse != null && !propertyDefinitionWithId.PropertyInfo.PropertyType.IsInstanceOfType(dataToUse) && !LoadContext.TryConvertType(dataToUse.GetType(), propertyDefinitionWithId.PropertyInfo.PropertyType, ref dataToUse))
			{
				return;
			}
			setMethod.Invoke(target, new object[]
			{
				dataToUse
			});
		}
	}
}
