using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;
using TaleWorlds.SaveSystem.Load;
using TaleWorlds.SaveSystem.Save;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000022 RID: 34
	public static class SaveManager
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x000042D0 File Offset: 0x000024D0
		public static void InitializeGlobalDefinitionContext()
		{
			SaveManager._definitionContext = new DefinitionContext();
			SaveManager._definitionContext.FillWithCurrentTypes();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000042E8 File Offset: 0x000024E8
		public static List<Type> CheckSaveableTypes()
		{
			List<Type> list = new List<Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				foreach (Type type in assemblies[i].GetTypesSafe(null))
				{
					PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
					{
						Attribute[] array = fieldInfo.GetCustomAttributesSafe(typeof(SaveableFieldAttribute)).ToArray<Attribute>();
						if (array.Length != 0)
						{
							SaveableFieldAttribute saveableFieldAttribute = (SaveableFieldAttribute)array[0];
							Type fieldType = fieldInfo.FieldType;
							if (!SaveManager._definitionContext.HasDefinition(fieldType) && !list.Contains(fieldType) && !fieldType.IsInterface && fieldType.FullName != null)
							{
								list.Add(fieldType);
							}
						}
					}
					foreach (PropertyInfo propertyInfo in properties)
					{
						Attribute[] array3 = propertyInfo.GetCustomAttributesSafe(typeof(SaveablePropertyAttribute)).ToArray<Attribute>();
						if (array3.Length != 0)
						{
							SaveablePropertyAttribute saveablePropertyAttribute = (SaveablePropertyAttribute)array3[0];
							Type propertyType = propertyInfo.PropertyType;
							if (!SaveManager._definitionContext.HasDefinition(propertyType) && !list.Contains(propertyType) && !propertyType.IsInterface && propertyType.FullName != null)
							{
								list.Add(propertyType);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004480 File Offset: 0x00002680
		public static SaveOutput Save(object target, MetaData metaData, string saveName, ISaveDriver driver)
		{
			if (SaveManager._definitionContext == null)
			{
				SaveManager.InitializeGlobalDefinitionContext();
			}
			SaveOutput result = null;
			if (SaveManager._definitionContext.GotError)
			{
				List<SaveError> list = new List<SaveError>();
				foreach (string message in SaveManager._definitionContext.Errors)
				{
					list.Add(new SaveError(message));
				}
				result = SaveOutput.CreateFailed(list, SaveResult.GeneralFailure);
			}
			else
			{
				Debug.Print("------Saving with new context. Save name: " + saveName + "------", 0, Debug.DebugColor.White, 17592186044416UL);
				SaveContext saveContext;
				string message2;
				if ((saveContext = new SaveContext(SaveManager._definitionContext)).Save(target, metaData, out message2))
				{
					try
					{
						Task<SaveResultWithMessage> task = driver.Save(saveName, 1, metaData, saveContext.SaveData);
						if (task.IsCompleted)
						{
							if (task.Result.SaveResult == SaveResult.Success)
							{
								result = SaveOutput.CreateSuccessful(saveContext.SaveData);
							}
							else
							{
								result = SaveOutput.CreateFailed(new SaveError[]
								{
									new SaveError(task.Result.Message)
								}, task.Result.SaveResult);
							}
						}
						else
						{
							result = SaveOutput.CreateContinuing(task);
						}
						return result;
					}
					catch (Exception ex)
					{
						return SaveOutput.CreateFailed(new SaveError[]
						{
							new SaveError(ex.Message)
						}, SaveResult.GeneralFailure);
					}
				}
				result = SaveOutput.CreateFailed(new SaveError[]
				{
					new SaveError(message2)
				}, SaveResult.GeneralFailure);
			}
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000045F4 File Offset: 0x000027F4
		public static MetaData LoadMetaData(string saveName, ISaveDriver driver)
		{
			return driver.LoadMetaData(saveName);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000045FD File Offset: 0x000027FD
		public static LoadResult Load(string saveName, ISaveDriver driver)
		{
			return SaveManager.Load(saveName, driver, false);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004608 File Offset: 0x00002808
		public static LoadResult Load(string saveName, ISaveDriver driver, bool loadAsLateInitialize)
		{
			DefinitionContext definitionContext = new DefinitionContext();
			definitionContext.FillWithCurrentTypes();
			LoadContext loadContext = new LoadContext(definitionContext, driver);
			LoadData loadData = driver.Load(saveName);
			LoadResult result;
			if (loadContext.Load(loadData, loadAsLateInitialize))
			{
				LoadCallbackInitializator loadCallbackInitializator = null;
				if (loadAsLateInitialize)
				{
					loadCallbackInitializator = loadContext.CreateLoadCallbackInitializator(loadData);
				}
				result = LoadResult.CreateSuccessful(loadContext.RootObject, loadData.MetaData, loadCallbackInitializator);
			}
			else
			{
				result = LoadResult.CreateFailed(new LoadError[]
				{
					new LoadError("Not implemented")
				});
			}
			return result;
		}

		// Token: 0x04000050 RID: 80
		public const string SaveFileExtension = "sav";

		// Token: 0x04000051 RID: 81
		private const int CurrentVersion = 1;

		// Token: 0x04000052 RID: 82
		private static DefinitionContext _definitionContext;
	}
}
