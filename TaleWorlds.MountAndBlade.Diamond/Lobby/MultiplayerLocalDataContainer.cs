using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby
{
	// Token: 0x02000178 RID: 376
	public abstract class MultiplayerLocalDataContainer<T> where T : MultiplayerLocalData
	{
		// Token: 0x06000A62 RID: 2658 RVA: 0x00011037 File Offset: 0x0000F237
		public MultiplayerLocalDataContainer()
		{
			this._operationQueue = new List<MultiplayerLocalDataContainer<T>.ContainerOperation>();
			this._dataList = new List<T>();
			this._saveDirectoryName = this.GetSaveDirectoryName();
			this._saveFileName = this.GetSaveFileName();
			this._isCacheDirty = true;
		}

		// Token: 0x06000A63 RID: 2659
		protected abstract string GetSaveDirectoryName();

		// Token: 0x06000A64 RID: 2660
		protected abstract string GetSaveFileName();

		// Token: 0x06000A65 RID: 2661 RVA: 0x00011074 File Offset: 0x0000F274
		public void AddEntry(T item)
		{
			List<MultiplayerLocalDataContainer<T>.ContainerOperation> operationQueue = this._operationQueue;
			lock (operationQueue)
			{
				MultiplayerLocalDataContainer<T>.ContainerOperation item2 = MultiplayerLocalDataContainer<T>.ContainerOperation.CreateAsAdd(item);
				this._operationQueue.Add(item2);
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000110C4 File Offset: 0x0000F2C4
		public void InsertEntry(T item, int index)
		{
			List<MultiplayerLocalDataContainer<T>.ContainerOperation> operationQueue = this._operationQueue;
			lock (operationQueue)
			{
				MultiplayerLocalDataContainer<T>.ContainerOperation item2 = MultiplayerLocalDataContainer<T>.ContainerOperation.CreateAsInsert(item, index);
				this._operationQueue.Add(item2);
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00011114 File Offset: 0x0000F314
		public void RemoveEntry(T item)
		{
			List<MultiplayerLocalDataContainer<T>.ContainerOperation> operationQueue = this._operationQueue;
			lock (operationQueue)
			{
				MultiplayerLocalDataContainer<T>.ContainerOperation item2 = MultiplayerLocalDataContainer<T>.ContainerOperation.CreateAsRemove(item);
				this._operationQueue.Add(item2);
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00011164 File Offset: 0x0000F364
		public MBReadOnlyList<T> GetEntries()
		{
			return new MBReadOnlyList<T>(this._dataList);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00011174 File Offset: 0x0000F374
		internal async Task Tick(float dt)
		{
			if (this._isCacheDirty)
			{
				await this.LoadFileAux();
				this._isCacheDirty = false;
			}
			while (this._operationQueue.Count > 0)
			{
				this.HandleOperation(this._operationQueue[0]);
				this._operationQueue.RemoveAt(0);
			}
			if (this._isFileDirty)
			{
				await this.SaveFileAux();
				this._isFileDirty = false;
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000111BC File Offset: 0x0000F3BC
		private void HandleOperation(MultiplayerLocalDataContainer<T>.ContainerOperation operation)
		{
			switch (operation.OperationType)
			{
			case MultiplayerLocalDataContainer<T>.OperationType.Add:
				this.AddEntryAux(operation.Item);
				return;
			case MultiplayerLocalDataContainer<T>.OperationType.Insert:
				this.InsertEntryAux(operation.Item, operation.Index);
				return;
			case MultiplayerLocalDataContainer<T>.OperationType.Remove:
				this.RemoveEntryAux(operation.Item);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00011210 File Offset: 0x0000F410
		private void AddEntryAux(T item)
		{
			bool flag;
			this.OnBeforeAddEntry(item, out flag);
			if (!flag)
			{
				return;
			}
			bool flag2 = false;
			using (List<T>.Enumerator enumerator = this._dataList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasSameContentWith(item))
					{
						flag2 = true;
						break;
					}
				}
			}
			if (!flag2)
			{
				this._dataList.Add(item);
			}
			else
			{
				Debug.FailedAssert("Item is already in container: " + item, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "AddEntryAux", 234);
			}
			this._isFileDirty = true;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000112BC File Offset: 0x0000F4BC
		private void InsertEntryAux(T item, int index)
		{
			bool flag;
			this.OnBeforeAddEntry(item, out flag);
			if (!flag)
			{
				return;
			}
			bool flag2 = false;
			using (List<T>.Enumerator enumerator = this._dataList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasSameContentWith(item))
					{
						flag2 = true;
						break;
					}
				}
			}
			if (!flag2)
			{
				if (index >= 0 && index < this._dataList.Count)
				{
					this._dataList.Insert(index, item);
				}
				else
				{
					this._dataList.Add(item);
				}
			}
			else
			{
				Debug.FailedAssert("Item is already in container: " + item, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "InsertEntryAux", 272);
			}
			this._isFileDirty = true;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0001138C File Offset: 0x0000F58C
		protected virtual void OnBeforeAddEntry(T item, out bool canAddEntry)
		{
			canAddEntry = true;
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00011394 File Offset: 0x0000F594
		private void RemoveEntryAux(T item)
		{
			bool flag;
			this.OnBeforeRemoveEntry(item, out flag);
			if (!flag)
			{
				return;
			}
			int count = this._dataList.Count;
			for (int i = this._dataList.Count - 1; i >= 0; i--)
			{
				if (this._dataList[i].HasSameContentWith(item))
				{
					this._dataList.Remove(item);
				}
			}
			if (count == this._dataList.Count)
			{
				Debug.FailedAssert("Item is not in container: " + item, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "RemoveEntryAux", 304);
			}
			this._isFileDirty = true;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00011436 File Offset: 0x0000F636
		protected virtual void OnBeforeRemoveEntry(T item, out bool canRemoveEntry)
		{
			canRemoveEntry = true;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0001143B File Offset: 0x0000F63B
		private PlatformFilePath GetDataFilePath()
		{
			return new PlatformFilePath(new PlatformDirectoryPath(PlatformFileType.User, this._saveDirectoryName), this._saveFileName);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00011454 File Offset: 0x0000F654
		private async Task SaveFileAux()
		{
			try
			{
				await FileHelper.SaveFileAsync(this.GetDataFilePath(), Common.SerializeObjectAsJson(this._dataList));
			}
			catch (Exception ex)
			{
				Debug.FailedAssert("An exception occured while trying to save " + base.GetType().Name + " data: " + ex.Message, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "SaveFileAux", 331);
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001149C File Offset: 0x0000F69C
		private async Task LoadFileAux()
		{
			PlatformFilePath oldFilePath = this.GetCompatibilityFilePath();
			if (FileHelper.FileExists(oldFilePath))
			{
				string text = await FileHelper.GetFileContentStringAsync(oldFilePath);
				FileHelper.DeleteFile(oldFilePath);
				if (!string.IsNullOrEmpty(text))
				{
					this._dataList.Clear();
					List<T> list = null;
					try
					{
						list = JsonConvert.DeserializeObject<List<T>>(text);
					}
					catch
					{
						try
						{
							list = this.DeserializeInCompatibilityMode(text);
						}
						catch
						{
							Debug.FailedAssert("Failed to load old data in compatibility mode", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "LoadFileAux", 362);
						}
					}
					if (list != null)
					{
						foreach (T item in list)
						{
							this._dataList.Add(item);
						}
					}
					this._isFileDirty = true;
					return;
				}
			}
			PlatformFilePath dataFilePath = this.GetDataFilePath();
			if (FileHelper.FileExists(dataFilePath))
			{
				string text2 = await FileHelper.GetFileContentStringAsync(dataFilePath);
				if (!string.IsNullOrEmpty(text2))
				{
					this._dataList.Clear();
					List<T> list2 = null;
					try
					{
						list2 = JsonConvert.DeserializeObject<List<T>>(text2);
					}
					catch
					{
						try
						{
							list2 = this.DeserializeInCompatibilityMode(text2);
							this._isFileDirty = true;
						}
						catch
						{
							Debug.FailedAssert("Failed to load file in compatibility mode", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.Diamond\\Lobby\\MultiplayerLocalDataManager.cs", "LoadFileAux", 403);
						}
					}
					if (list2 != null)
					{
						foreach (T item2 in list2)
						{
							this._dataList.Add(item2);
						}
					}
				}
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000114E1 File Offset: 0x0000F6E1
		protected virtual PlatformFilePath GetCompatibilityFilePath()
		{
			return new PlatformFilePath(new PlatformDirectoryPath(PlatformFileType.User, "DataOld"), "TmpData");
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000114F8 File Offset: 0x0000F6F8
		protected virtual List<T> DeserializeInCompatibilityMode(string serializedJson)
		{
			return null;
		}

		// Token: 0x04000502 RID: 1282
		private readonly string _saveDirectoryName;

		// Token: 0x04000503 RID: 1283
		private readonly string _saveFileName;

		// Token: 0x04000504 RID: 1284
		private readonly List<MultiplayerLocalDataContainer<T>.ContainerOperation> _operationQueue;

		// Token: 0x04000505 RID: 1285
		private readonly List<T> _dataList;

		// Token: 0x04000506 RID: 1286
		private bool _isFileDirty;

		// Token: 0x04000507 RID: 1287
		private bool _isCacheDirty;

		// Token: 0x020001E5 RID: 485
		private enum OperationType
		{
			// Token: 0x040006DF RID: 1759
			Add,
			// Token: 0x040006E0 RID: 1760
			Insert,
			// Token: 0x040006E1 RID: 1761
			Remove
		}

		// Token: 0x020001E6 RID: 486
		private struct ContainerOperation
		{
			// Token: 0x06000BB4 RID: 2996 RVA: 0x00017BC2 File Offset: 0x00015DC2
			private ContainerOperation(MultiplayerLocalDataContainer<T>.OperationType type, T item, int index)
			{
				this.OperationType = type;
				this.Item = item;
				this.Index = index;
			}

			// Token: 0x06000BB5 RID: 2997 RVA: 0x00017BD9 File Offset: 0x00015DD9
			public static MultiplayerLocalDataContainer<T>.ContainerOperation CreateAsAdd(T item)
			{
				return new MultiplayerLocalDataContainer<T>.ContainerOperation(MultiplayerLocalDataContainer<T>.OperationType.Add, item, -1);
			}

			// Token: 0x06000BB6 RID: 2998 RVA: 0x00017BE3 File Offset: 0x00015DE3
			public static MultiplayerLocalDataContainer<T>.ContainerOperation CreateAsRemove(T item)
			{
				return new MultiplayerLocalDataContainer<T>.ContainerOperation(MultiplayerLocalDataContainer<T>.OperationType.Remove, item, -1);
			}

			// Token: 0x06000BB7 RID: 2999 RVA: 0x00017BED File Offset: 0x00015DED
			public static MultiplayerLocalDataContainer<T>.ContainerOperation CreateAsInsert(T item, int index)
			{
				return new MultiplayerLocalDataContainer<T>.ContainerOperation(MultiplayerLocalDataContainer<T>.OperationType.Insert, item, index);
			}

			// Token: 0x040006E2 RID: 1762
			public readonly MultiplayerLocalDataContainer<T>.OperationType OperationType;

			// Token: 0x040006E3 RID: 1763
			public readonly T Item;

			// Token: 0x040006E4 RID: 1764
			public readonly int Index;
		}

		// Token: 0x020001E7 RID: 487
		private class ContainerOperationComparer : IComparer<MultiplayerLocalDataContainer<T>.ContainerOperation>
		{
			// Token: 0x06000BB8 RID: 3000 RVA: 0x00017BF7 File Offset: 0x00015DF7
			public int Compare(MultiplayerLocalDataContainer<T>.ContainerOperation x, MultiplayerLocalDataContainer<T>.ContainerOperation y)
			{
				return x.OperationType.CompareTo(y.OperationType);
			}
		}
	}
}
