using System;
using System.Collections.Generic;

namespace TaleWorlds.Library.EventSystem
{
	// Token: 0x020000B3 RID: 179
	public class DictionaryByType
	{
		// Token: 0x06000677 RID: 1655 RVA: 0x000149E8 File Offset: 0x00012BE8
		public void Add<T>(Action<T> value)
		{
			object obj;
			if (!this._eventsByType.TryGetValue(typeof(T), out obj))
			{
				obj = new List<Action<T>>();
				this._eventsByType[typeof(T)] = obj;
			}
			((List<Action<T>>)obj).Add(value);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00014A38 File Offset: 0x00012C38
		public void Remove<T>(Action<T> value)
		{
			object obj;
			if (this._eventsByType.TryGetValue(typeof(T), out obj))
			{
				List<Action<T>> list = (List<Action<T>>)obj;
				list.Remove(value);
				this._eventsByType[typeof(T)] = list;
				return;
			}
			Debug.FailedAssert("Event: " + typeof(T).Name + " were not registered in the first place", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\EventSystem\\EventManager.cs", "Remove", 106);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00014AB4 File Offset: 0x00012CB4
		public void InvokeActions<T>(T item)
		{
			object obj;
			if (this._eventsByType.TryGetValue(typeof(T), out obj))
			{
				foreach (Action<T> action in ((List<Action<T>>)obj))
				{
					action(item);
				}
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00014B20 File Offset: 0x00012D20
		public List<Action<T>> Get<T>()
		{
			return (List<Action<T>>)this._eventsByType[typeof(T)];
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00014B3C File Offset: 0x00012D3C
		public bool TryGet<T>(out List<Action<T>> value)
		{
			object obj;
			if (this._eventsByType.TryGetValue(typeof(T), out obj))
			{
				value = (List<Action<T>>)obj;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00014B70 File Offset: 0x00012D70
		public IDictionary<Type, object> GetClone()
		{
			return new Dictionary<Type, object>(this._eventsByType);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00014B7D File Offset: 0x00012D7D
		public void Clear()
		{
			this._eventsByType.Clear();
		}

		// Token: 0x040001EE RID: 494
		private readonly IDictionary<Type, object> _eventsByType = new Dictionary<Type, object>();
	}
}
