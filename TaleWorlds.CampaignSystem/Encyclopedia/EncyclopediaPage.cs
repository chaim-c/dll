using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.Encyclopedia
{
	// Token: 0x02000159 RID: 345
	public abstract class EncyclopediaPage
	{
		// Token: 0x0600188C RID: 6284
		protected abstract IEnumerable<EncyclopediaListItem> InitializeListItems();

		// Token: 0x0600188D RID: 6285
		protected abstract IEnumerable<EncyclopediaFilterGroup> InitializeFilterItems();

		// Token: 0x0600188E RID: 6286
		protected abstract IEnumerable<EncyclopediaSortController> InitializeSortControllers();

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x0007CEB9 File Offset: 0x0007B0B9
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x0007CEC1 File Offset: 0x0007B0C1
		public int HomePageOrderIndex { get; protected set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x0007CECA File Offset: 0x0007B0CA
		public EncyclopediaPage Parent { get; }

		// Token: 0x06001892 RID: 6290 RVA: 0x0007CED4 File Offset: 0x0007B0D4
		public EncyclopediaPage()
		{
			this._filters = this.InitializeFilterItems();
			this._items = this.InitializeListItems();
			this._sortControllers = new List<EncyclopediaSortController>
			{
				new EncyclopediaSortController(new TextObject("{=koX9okuG}None", null), new EncyclopediaListItemNameComparer())
			};
			((List<EncyclopediaSortController>)this._sortControllers).AddRange(this.InitializeSortControllers());
			foreach (object obj in base.GetType().GetCustomAttributesSafe(typeof(EncyclopediaModel), true))
			{
				if (obj is EncyclopediaModel)
				{
					this._identifierTypes = (obj as EncyclopediaModel).PageTargetTypes;
					break;
				}
			}
			this._identifiers = new Dictionary<Type, string>();
			foreach (Type type in this._identifierTypes)
			{
				if (Game.Current.ObjectManager.HasType(type))
				{
					this._identifiers.Add(type, Game.Current.ObjectManager.FindRegisteredClassPrefix(type));
				}
				else
				{
					string text = type.Name.ToString();
					if (text == "Clan")
					{
						text = "Faction";
					}
					this._identifiers.Add(type, text);
				}
			}
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0007D009 File Offset: 0x0007B209
		public bool HasIdentifierType(Type identifierType)
		{
			return this._identifierTypes.Contains(identifierType);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0007D017 File Offset: 0x0007B217
		internal bool HasIdentifier(string identifier)
		{
			return this._identifiers.ContainsValue(identifier);
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0007D025 File Offset: 0x0007B225
		public string GetIdentifier(Type identifierType)
		{
			if (this._identifiers.ContainsKey(identifierType))
			{
				return this._identifiers[identifierType];
			}
			return "";
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0007D047 File Offset: 0x0007B247
		public string[] GetIdentifierNames()
		{
			return this._identifiers.Values.ToArray<string>();
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0007D05C File Offset: 0x0007B25C
		public bool IsFiltered(object o)
		{
			using (IEnumerator<EncyclopediaFilterGroup> enumerator = this.GetFilterItems().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Predicate(o))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0007D0B8 File Offset: 0x0007B2B8
		public virtual string GetViewFullyQualifiedName()
		{
			return "";
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0007D0BF File Offset: 0x0007B2BF
		public virtual string GetStringID()
		{
			return "";
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0007D0C6 File Offset: 0x0007B2C6
		public virtual TextObject GetName()
		{
			return TextObject.Empty;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x0007D0CD File Offset: 0x0007B2CD
		public virtual MBObjectBase GetObject(string typeName, string stringID)
		{
			return MBObjectManager.Instance.GetObject(typeName, stringID);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0007D0DB File Offset: 0x0007B2DB
		public virtual bool IsValidEncyclopediaItem(object o)
		{
			return false;
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x0007D0DE File Offset: 0x0007B2DE
		public virtual TextObject GetDescriptionText()
		{
			return TextObject.Empty;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x0007D0E5 File Offset: 0x0007B2E5
		public IEnumerable<EncyclopediaListItem> GetListItems()
		{
			return this._items;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0007D0ED File Offset: 0x0007B2ED
		public IEnumerable<EncyclopediaFilterGroup> GetFilterItems()
		{
			return this._filters;
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x0007D0F5 File Offset: 0x0007B2F5
		public IEnumerable<EncyclopediaSortController> GetSortControllers()
		{
			return this._sortControllers;
		}

		// Token: 0x040008A2 RID: 2210
		private readonly Type[] _identifierTypes;

		// Token: 0x040008A3 RID: 2211
		private readonly Dictionary<Type, string> _identifiers;

		// Token: 0x040008A4 RID: 2212
		private IEnumerable<EncyclopediaFilterGroup> _filters;

		// Token: 0x040008A5 RID: 2213
		private IEnumerable<EncyclopediaListItem> _items;

		// Token: 0x040008A6 RID: 2214
		private IEnumerable<EncyclopediaSortController> _sortControllers;
	}
}
