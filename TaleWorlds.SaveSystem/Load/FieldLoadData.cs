using System;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x02000036 RID: 54
	internal class FieldLoadData : MemberLoadData
	{
		// Token: 0x060001EE RID: 494 RVA: 0x000090C2 File Offset: 0x000072C2
		public FieldLoadData(ObjectLoadData objectLoadData, IReader reader) : base(objectLoadData, reader)
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000090CC File Offset: 0x000072CC
		public void FillObject()
		{
			FieldDefinition fieldDefinitionWithId;
			if (base.ObjectLoadData.TypeDefinition == null || (fieldDefinitionWithId = base.ObjectLoadData.TypeDefinition.GetFieldDefinitionWithId(base.MemberSaveId)) == null)
			{
				return;
			}
			FieldInfo fieldInfo = fieldDefinitionWithId.FieldInfo;
			object target = base.ObjectLoadData.Target;
			object dataToUse = base.GetDataToUse();
			if (dataToUse != null && !fieldInfo.FieldType.IsInstanceOfType(dataToUse) && !LoadContext.TryConvertType(dataToUse.GetType(), fieldInfo.FieldType, ref dataToUse))
			{
				return;
			}
			fieldInfo.SetValue(target, dataToUse);
		}
	}
}
