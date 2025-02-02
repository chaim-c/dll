using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x02000037 RID: 55
	internal class LoadCallbackInitializator
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000914A File Offset: 0x0000734A
		public LoadCallbackInitializator(LoadData loadData, ObjectHeaderLoadData[] objectHeaderLoadDatas, int objectCount)
		{
			this._loadData = loadData;
			this._objectHeaderLoadDatas = objectHeaderLoadDatas;
			this._objectCount = objectCount;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00009168 File Offset: 0x00007368
		public void InitializeObjects()
		{
			using (new PerformanceTestBlock("LoadContext::Callbacks"))
			{
				for (int i = 0; i < this._objectCount; i++)
				{
					ObjectHeaderLoadData objectHeaderLoadData = this._objectHeaderLoadDatas[i];
					if (objectHeaderLoadData.Target != null)
					{
						TypeDefinition typeDefinition = objectHeaderLoadData.TypeDefinition;
						IEnumerable<MethodInfo> enumerable = (typeDefinition != null) ? typeDefinition.InitializationCallbacks : null;
						if (enumerable != null)
						{
							foreach (MethodInfo methodInfo in enumerable)
							{
								ParameterInfo[] parameters = methodInfo.GetParameters();
								if (parameters.Length > 1 && parameters[1].ParameterType == typeof(ObjectLoadData))
								{
									ObjectLoadData objectLoadData = LoadContext.CreateLoadData(this._loadData, i, objectHeaderLoadData);
									methodInfo.Invoke(objectHeaderLoadData.Target, new object[]
									{
										this._loadData.MetaData,
										objectLoadData
									});
								}
								else if (parameters.Length == 1)
								{
									methodInfo.Invoke(objectHeaderLoadData.Target, new object[]
									{
										this._loadData.MetaData
									});
								}
								else
								{
									methodInfo.Invoke(objectHeaderLoadData.Target, null);
								}
							}
						}
					}
				}
			}
			GC.Collect();
			this.AfterInitializeObjects();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000092DC File Offset: 0x000074DC
		public void AfterInitializeObjects()
		{
			using (new PerformanceTestBlock("LoadContext::AfterCallbacks"))
			{
				for (int i = 0; i < this._objectCount; i++)
				{
					ObjectHeaderLoadData objectHeaderLoadData = this._objectHeaderLoadDatas[i];
					if (objectHeaderLoadData.Target != null)
					{
						TypeDefinition typeDefinition = objectHeaderLoadData.TypeDefinition;
						IEnumerable<MethodInfo> enumerable = (typeDefinition != null) ? typeDefinition.LateInitializationCallbacks : null;
						if (enumerable != null)
						{
							foreach (MethodInfo methodInfo in enumerable)
							{
								ParameterInfo[] parameters = methodInfo.GetParameters();
								if (parameters.Length > 1 && parameters[1].ParameterType == typeof(ObjectLoadData))
								{
									ObjectLoadData objectLoadData = LoadContext.CreateLoadData(this._loadData, i, objectHeaderLoadData);
									methodInfo.Invoke(objectHeaderLoadData.Target, new object[]
									{
										this._loadData.MetaData,
										objectLoadData
									});
								}
								else if (parameters.Length == 1)
								{
									methodInfo.Invoke(objectHeaderLoadData.Target, new object[]
									{
										this._loadData.MetaData
									});
								}
								else
								{
									methodInfo.Invoke(objectHeaderLoadData.Target, null);
								}
							}
						}
					}
				}
			}
			GC.Collect();
		}

		// Token: 0x04000095 RID: 149
		private ObjectHeaderLoadData[] _objectHeaderLoadDatas;

		// Token: 0x04000096 RID: 150
		private int _objectCount;

		// Token: 0x04000097 RID: 151
		private LoadData _loadData;
	}
}
