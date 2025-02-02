using System;
using System.Collections.Generic;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.View.Tableaus
{
	// Token: 0x02000029 RID: 41
	public class ThumbnailCache
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000F4EB File Offset: 0x0000D6EB
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000F4F3 File Offset: 0x0000D6F3
		public int Count { get; private set; }

		// Token: 0x060001B4 RID: 436 RVA: 0x0000F4FC File Offset: 0x0000D6FC
		public ThumbnailCache(int capacity, ThumbnailCreatorView thumbnailCreatorView)
		{
			this._capacity = capacity;
			this._map = new Dictionary<string, ThumbnailCacheNode>(capacity);
			this._thumbnailCreatorView = thumbnailCreatorView;
			this.Count = 0;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000F530 File Offset: 0x0000D730
		public bool GetValue(string key, out Texture texture)
		{
			texture = null;
			ThumbnailCacheNode thumbnailCacheNode;
			if (this._map.TryGetValue(key, out thumbnailCacheNode))
			{
				thumbnailCacheNode.FrameNo = Utilities.EngineFrameNo;
				texture = thumbnailCacheNode.Value;
				return true;
			}
			return false;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000F568 File Offset: 0x0000D768
		public bool AddReference(string key)
		{
			ThumbnailCacheNode thumbnailCacheNode;
			if (this._map.TryGetValue(key, out thumbnailCacheNode))
			{
				thumbnailCacheNode.ReferenceCount++;
				return true;
			}
			return false;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000F598 File Offset: 0x0000D798
		public bool MarkForDeletion(string key)
		{
			ThumbnailCacheNode thumbnailCacheNode;
			if (this._map.TryGetValue(key, out thumbnailCacheNode))
			{
				thumbnailCacheNode.ReferenceCount--;
				return true;
			}
			return false;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		public void ForceDelete(string key)
		{
			ThumbnailCacheNode thumbnailCacheNode;
			if (this._map.TryGetValue(key, out thumbnailCacheNode))
			{
				thumbnailCacheNode.ReferenceCount = 0;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000F5EC File Offset: 0x0000D7EC
		public void Tick()
		{
			if (this.Count > this._capacity)
			{
				List<ThumbnailCacheNode> list = new List<ThumbnailCacheNode>();
				foreach (KeyValuePair<string, ThumbnailCacheNode> keyValuePair in this._map)
				{
					if (keyValuePair.Value.ReferenceCount == 0)
					{
						list.Add(keyValuePair.Value);
					}
				}
				list.Sort(this._nodeComparer);
				int num = 0;
				while (this.Count > this._capacity && num < list.Count)
				{
					this._map.Remove(list[num].Key);
					this._thumbnailCreatorView.CancelRequest(list[num].Key);
					if (list[num].Value != null)
					{
						list[num].Value.Release();
					}
					num++;
					int count = this.Count;
					this.Count = count - 1;
				}
				list.RemoveRange(0, num);
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000F708 File Offset: 0x0000D908
		public void Add(string key, Texture value)
		{
			ThumbnailCacheNode thumbnailCacheNode;
			if (this._map.TryGetValue(key, out thumbnailCacheNode))
			{
				thumbnailCacheNode.Value = value;
				thumbnailCacheNode.FrameNo = Utilities.EngineFrameNo;
				return;
			}
			ThumbnailCacheNode value2 = new ThumbnailCacheNode(key, value, Utilities.EngineFrameNo);
			this._map[key] = value2;
			int count = this.Count;
			this.Count = count + 1;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000F764 File Offset: 0x0000D964
		public int GetTotalMemorySize()
		{
			int num = 0;
			foreach (ThumbnailCacheNode thumbnailCacheNode in this._map.Values)
			{
				num += thumbnailCacheNode.Value.MemorySize;
			}
			return num;
		}

		// Token: 0x04000129 RID: 297
		private int _capacity;

		// Token: 0x0400012A RID: 298
		private ThumbnailCreatorView _thumbnailCreatorView;

		// Token: 0x0400012B RID: 299
		private Dictionary<string, ThumbnailCacheNode> _map;

		// Token: 0x0400012C RID: 300
		private NodeComparer _nodeComparer = new NodeComparer();
	}
}
