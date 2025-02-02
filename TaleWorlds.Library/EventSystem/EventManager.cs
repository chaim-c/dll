using System;
using System.Collections.Generic;

namespace TaleWorlds.Library.EventSystem
{
	// Token: 0x020000B2 RID: 178
	public class EventManager
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x0001492D File Offset: 0x00012B2D
		public EventManager()
		{
			this._eventsByType = new DictionaryByType();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00014940 File Offset: 0x00012B40
		public void RegisterEvent<T>(Action<T> eventObjType)
		{
			if (typeof(T).IsSubclassOf(typeof(EventBase)))
			{
				this._eventsByType.Add<T>(eventObjType);
				return;
			}
			Debug.FailedAssert("Events have to derived from EventSystemBase", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\EventSystem\\EventManager.cs", "RegisterEvent", 31);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00014980 File Offset: 0x00012B80
		public void UnregisterEvent<T>(Action<T> eventObjType)
		{
			if (typeof(T).IsSubclassOf(typeof(EventBase)))
			{
				this._eventsByType.Remove<T>(eventObjType);
				return;
			}
			Debug.FailedAssert("Events have to derived from EventSystemBase", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\EventSystem\\EventManager.cs", "UnregisterEvent", 48);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000149C0 File Offset: 0x00012BC0
		public void TriggerEvent<T>(T eventObj)
		{
			this._eventsByType.InvokeActions<T>(eventObj);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000149CE File Offset: 0x00012BCE
		public void Clear()
		{
			this._eventsByType.Clear();
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x000149DB File Offset: 0x00012BDB
		public IDictionary<Type, object> GetCloneOfEventDictionary()
		{
			return this._eventsByType.GetClone();
		}

		// Token: 0x040001ED RID: 493
		private readonly DictionaryByType _eventsByType;
	}
}
