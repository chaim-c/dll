using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Load
{
	// Token: 0x02000038 RID: 56
	public class LoadContext
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000944C File Offset: 0x0000764C
		public static bool EnableLoadStatistics
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000944F File Offset: 0x0000764F
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00009457 File Offset: 0x00007657
		public object RootObject { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00009460 File Offset: 0x00007660
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00009468 File Offset: 0x00007668
		public DefinitionContext DefinitionContext { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00009471 File Offset: 0x00007671
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00009479 File Offset: 0x00007679
		public ISaveDriver Driver { get; private set; }

		// Token: 0x060001FA RID: 506 RVA: 0x00009482 File Offset: 0x00007682
		public LoadContext(DefinitionContext definitionContext, ISaveDriver driver)
		{
			this.DefinitionContext = definitionContext;
			this._objectHeaderLoadDatas = null;
			this._containerHeaderLoadDatas = null;
			this._strings = null;
			this.Driver = driver;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000094B0 File Offset: 0x000076B0
		internal static ObjectLoadData CreateLoadData(LoadData loadData, int i, ObjectHeaderLoadData header)
		{
			ArchiveDeserializer archiveDeserializer = new ArchiveDeserializer();
			archiveDeserializer.LoadFrom(loadData.GameData.ObjectData[i]);
			SaveEntryFolder rootFolder = archiveDeserializer.RootFolder;
			ObjectLoadData objectLoadData = new ObjectLoadData(header);
			SaveEntryFolder childFolder = rootFolder.GetChildFolder(new FolderId(i, SaveFolderExtension.Object));
			objectLoadData.InitializeReaders(childFolder);
			objectLoadData.FillCreatedObject();
			objectLoadData.Read();
			objectLoadData.FillObject();
			return objectLoadData;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00009508 File Offset: 0x00007708
		public bool Load(LoadData loadData, bool loadAsLateInitialize)
		{
			bool result = false;
			try
			{
				using (new PerformanceTestBlock("LoadContext::Load Headers"))
				{
					using (new PerformanceTestBlock("LoadContext::Load And Create Header"))
					{
						ArchiveDeserializer archiveDeserializer = new ArchiveDeserializer();
						archiveDeserializer.LoadFrom(loadData.GameData.Header);
						SaveEntryFolder headerRootFolder = archiveDeserializer.RootFolder;
						BinaryReader binaryReader = headerRootFolder.GetEntry(new EntryId(-1, SaveEntryExtension.Config)).GetBinaryReader();
						this._objectCount = binaryReader.ReadInt();
						this._stringCount = binaryReader.ReadInt();
						this._containerCount = binaryReader.ReadInt();
						this._objectHeaderLoadDatas = new ObjectHeaderLoadData[this._objectCount];
						this._containerHeaderLoadDatas = new ContainerHeaderLoadData[this._containerCount];
						this._strings = new string[this._stringCount];
						if (LoadContext.EnableLoadStatistics)
						{
							for (int i = 0; i < this._objectCount; i++)
							{
								ObjectHeaderLoadData objectHeaderLoadData = new ObjectHeaderLoadData(this, i);
								SaveEntryFolder childFolder = headerRootFolder.GetChildFolder(new FolderId(i, SaveFolderExtension.Object));
								objectHeaderLoadData.InitialieReaders(childFolder);
								this._objectHeaderLoadDatas[i] = objectHeaderLoadData;
							}
							for (int j = 0; j < this._containerCount; j++)
							{
								ContainerHeaderLoadData containerHeaderLoadData = new ContainerHeaderLoadData(this, j);
								SaveEntryFolder childFolder2 = headerRootFolder.GetChildFolder(new FolderId(j, SaveFolderExtension.Container));
								containerHeaderLoadData.InitialieReaders(childFolder2);
								this._containerHeaderLoadDatas[j] = containerHeaderLoadData;
							}
						}
						else
						{
							TWParallel.For(0, this._objectCount, delegate(int startInclusive, int endExclusive)
							{
								for (int num2 = startInclusive; num2 < endExclusive; num2++)
								{
									ObjectHeaderLoadData objectHeaderLoadData5 = new ObjectHeaderLoadData(this, num2);
									SaveEntryFolder childFolder4 = headerRootFolder.GetChildFolder(new FolderId(num2, SaveFolderExtension.Object));
									objectHeaderLoadData5.InitialieReaders(childFolder4);
									this._objectHeaderLoadDatas[num2] = objectHeaderLoadData5;
								}
							}, 16);
							TWParallel.For(0, this._containerCount, delegate(int startInclusive, int endExclusive)
							{
								for (int num2 = startInclusive; num2 < endExclusive; num2++)
								{
									ContainerHeaderLoadData containerHeaderLoadData3 = new ContainerHeaderLoadData(this, num2);
									SaveEntryFolder childFolder4 = headerRootFolder.GetChildFolder(new FolderId(num2, SaveFolderExtension.Container));
									containerHeaderLoadData3.InitialieReaders(childFolder4);
									this._containerHeaderLoadDatas[num2] = containerHeaderLoadData3;
								}
							}, 16);
						}
					}
					using (new PerformanceTestBlock("LoadContext::Create Objects"))
					{
						foreach (ObjectHeaderLoadData objectHeaderLoadData2 in this._objectHeaderLoadDatas)
						{
							objectHeaderLoadData2.CreateObject();
							if (objectHeaderLoadData2.Id == 0)
							{
								this.RootObject = objectHeaderLoadData2.Target;
							}
						}
						foreach (ContainerHeaderLoadData containerHeaderLoadData2 in this._containerHeaderLoadDatas)
						{
							if (containerHeaderLoadData2.GetObjectTypeDefinition())
							{
								containerHeaderLoadData2.CreateObject();
							}
						}
					}
				}
				GC.Collect();
				using (new PerformanceTestBlock("LoadContext::Load Strings"))
				{
					ArchiveDeserializer archiveDeserializer2 = new ArchiveDeserializer();
					archiveDeserializer2.LoadFrom(loadData.GameData.Strings);
					for (int l = 0; l < this._stringCount; l++)
					{
						string text = LoadContext.LoadString(archiveDeserializer2, l);
						this._strings[l] = text;
					}
				}
				GC.Collect();
				using (new PerformanceTestBlock("LoadContext::Resolve Objects"))
				{
					for (int m = 0; m < this._objectHeaderLoadDatas.Length; m++)
					{
						ObjectHeaderLoadData objectHeaderLoadData3 = this._objectHeaderLoadDatas[m];
						TypeDefinition typeDefinition = objectHeaderLoadData3.TypeDefinition;
						if (typeDefinition != null)
						{
							object loadedObject = objectHeaderLoadData3.LoadedObject;
							if (typeDefinition.CheckIfRequiresAdvancedResolving(loadedObject))
							{
								ObjectLoadData objectLoadData = LoadContext.CreateLoadData(loadData, m, objectHeaderLoadData3);
								objectHeaderLoadData3.AdvancedResolveObject(loadData.MetaData, objectLoadData);
							}
							else
							{
								objectHeaderLoadData3.ResolveObject();
							}
						}
					}
				}
				GC.Collect();
				using (new PerformanceTestBlock("LoadContext::Load Object Datas"))
				{
					if (LoadContext.EnableLoadStatistics)
					{
						for (int n = 0; n < this._objectCount; n++)
						{
							ObjectHeaderLoadData objectHeaderLoadData4 = this._objectHeaderLoadDatas[n];
							if (objectHeaderLoadData4.Target == objectHeaderLoadData4.LoadedObject)
							{
								LoadContext.CreateLoadData(loadData, n, objectHeaderLoadData4);
							}
						}
					}
					else
					{
						TWParallel.For(0, this._objectCount, delegate(int startInclusive, int endExclusive)
						{
							for (int num2 = startInclusive; num2 < endExclusive; num2++)
							{
								ObjectHeaderLoadData objectHeaderLoadData5 = this._objectHeaderLoadDatas[num2];
								if (objectHeaderLoadData5.Target == objectHeaderLoadData5.LoadedObject)
								{
									LoadContext.CreateLoadData(loadData, num2, objectHeaderLoadData5);
								}
							}
						}, 16);
					}
				}
				using (new PerformanceTestBlock("LoadContext::Load Container Datas"))
				{
					if (LoadContext.EnableLoadStatistics)
					{
						for (int num = 0; num < this._containerCount; num++)
						{
							byte[] binaryArchive = loadData.GameData.ContainerData[num];
							ArchiveDeserializer archiveDeserializer3 = new ArchiveDeserializer();
							archiveDeserializer3.LoadFrom(binaryArchive);
							SaveEntryFolder rootFolder = archiveDeserializer3.RootFolder;
							ContainerLoadData containerLoadData = new ContainerLoadData(this._containerHeaderLoadDatas[num]);
							SaveEntryFolder childFolder3 = rootFolder.GetChildFolder(new FolderId(num, SaveFolderExtension.Container));
							containerLoadData.InitializeReaders(childFolder3);
							containerLoadData.FillCreatedObject();
							containerLoadData.Read();
							containerLoadData.FillObject();
						}
					}
					else
					{
						TWParallel.For(0, this._containerCount, delegate(int startInclusive, int endExclusive)
						{
							for (int num2 = startInclusive; num2 < endExclusive; num2++)
							{
								byte[] binaryArchive2 = loadData.GameData.ContainerData[num2];
								ArchiveDeserializer archiveDeserializer4 = new ArchiveDeserializer();
								archiveDeserializer4.LoadFrom(binaryArchive2);
								SaveEntryFolder rootFolder2 = archiveDeserializer4.RootFolder;
								ContainerLoadData containerLoadData2 = new ContainerLoadData(this._containerHeaderLoadDatas[num2]);
								SaveEntryFolder childFolder4 = rootFolder2.GetChildFolder(new FolderId(num2, SaveFolderExtension.Container));
								containerLoadData2.InitializeReaders(childFolder4);
								containerLoadData2.FillCreatedObject();
								containerLoadData2.Read();
								containerLoadData2.FillObject();
							}
						}, 16);
					}
				}
				GC.Collect();
				if (!loadAsLateInitialize)
				{
					this.CreateLoadCallbackInitializator(loadData).InitializeObjects();
				}
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Print(ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				result = false;
			}
			return result;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009A98 File Offset: 0x00007C98
		internal LoadCallbackInitializator CreateLoadCallbackInitializator(LoadData loadData)
		{
			return new LoadCallbackInitializator(loadData, this._objectHeaderLoadDatas, this._objectCount);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009AAC File Offset: 0x00007CAC
		private static string LoadString(ArchiveDeserializer saveArchive, int id)
		{
			return saveArchive.RootFolder.GetChildFolder(new FolderId(-1, SaveFolderExtension.Strings)).GetEntry(new EntryId(id, SaveEntryExtension.Txt)).GetBinaryReader().ReadString();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00009AD8 File Offset: 0x00007CD8
		public static bool TryConvertType(Type sourceType, Type targetType, ref object data)
		{
			if (LoadContext.<TryConvertType>g__isNum|25_2(sourceType) && LoadContext.<TryConvertType>g__isNum|25_2(targetType))
			{
				try
				{
					data = Convert.ChangeType(data, targetType);
					return true;
				}
				catch
				{
					return false;
				}
			}
			if (LoadContext.<TryConvertType>g__isNum|25_2(sourceType) && targetType == typeof(string))
			{
				if (LoadContext.<TryConvertType>g__isInt|25_0(sourceType))
				{
					data = Convert.ToInt64(data).ToString();
				}
				else if (LoadContext.<TryConvertType>g__isFloat|25_1(sourceType))
				{
					data = Convert.ToDouble(data).ToString(CultureInfo.InvariantCulture);
				}
				return true;
			}
			if (sourceType.IsGenericType && sourceType.GetGenericTypeDefinition() == typeof(List<>) && targetType.IsGenericType)
			{
				targetType.GetGenericTypeDefinition() == typeof(MBList<>);
			}
			return false;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009BB4 File Offset: 0x00007DB4
		public ObjectHeaderLoadData GetObjectWithId(int id)
		{
			ObjectHeaderLoadData result = null;
			if (id != -1)
			{
				result = this._objectHeaderLoadDatas[id];
			}
			return result;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00009BD4 File Offset: 0x00007DD4
		public ContainerHeaderLoadData GetContainerWithId(int id)
		{
			ContainerHeaderLoadData result = null;
			if (id != -1)
			{
				result = this._containerHeaderLoadDatas[id];
			}
			return result;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009BF4 File Offset: 0x00007DF4
		public string GetStringWithId(int id)
		{
			string result = null;
			if (id != -1)
			{
				result = this._strings[id];
			}
			return result;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009C14 File Offset: 0x00007E14
		[CompilerGenerated]
		internal static bool <TryConvertType>g__isInt|25_0(Type type)
		{
			return type == typeof(long) || type == typeof(int) || type == typeof(short) || type == typeof(ulong) || type == typeof(uint) || type == typeof(ushort);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009C8D File Offset: 0x00007E8D
		[CompilerGenerated]
		internal static bool <TryConvertType>g__isFloat|25_1(Type type)
		{
			return type == typeof(double) || type == typeof(float);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009CB3 File Offset: 0x00007EB3
		[CompilerGenerated]
		internal static bool <TryConvertType>g__isNum|25_2(Type type)
		{
			return LoadContext.<TryConvertType>g__isInt|25_0(type) || LoadContext.<TryConvertType>g__isFloat|25_1(type);
		}

		// Token: 0x04000099 RID: 153
		private int _objectCount;

		// Token: 0x0400009A RID: 154
		private int _stringCount;

		// Token: 0x0400009B RID: 155
		private int _containerCount;

		// Token: 0x0400009C RID: 156
		private ObjectHeaderLoadData[] _objectHeaderLoadDatas;

		// Token: 0x0400009D RID: 157
		private ContainerHeaderLoadData[] _containerHeaderLoadDatas;

		// Token: 0x0400009E RID: 158
		private string[] _strings;
	}
}
