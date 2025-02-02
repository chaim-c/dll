using System;
using System.Collections.Generic;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000ED RID: 237
	internal sealed class TypeDefinitionCollection : Collection<TypeDefinition>
	{
		// Token: 0x06000976 RID: 2422 RVA: 0x0001D84B File Offset: 0x0001BA4B
		internal TypeDefinitionCollection(ModuleDefinition container)
		{
			this.container = container;
			this.name_cache = new Dictionary<Row<string, string>, TypeDefinition>(new RowEqualityComparer());
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001D86A File Offset: 0x0001BA6A
		internal TypeDefinitionCollection(ModuleDefinition container, int capacity) : base(capacity)
		{
			this.container = container;
			this.name_cache = new Dictionary<Row<string, string>, TypeDefinition>(capacity, new RowEqualityComparer());
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0001D88B File Offset: 0x0001BA8B
		protected override void OnAdd(TypeDefinition item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001D894 File Offset: 0x0001BA94
		protected override void OnSet(TypeDefinition item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001D89D File Offset: 0x0001BA9D
		protected override void OnInsert(TypeDefinition item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001D8A6 File Offset: 0x0001BAA6
		protected override void OnRemove(TypeDefinition item, int index)
		{
			this.Detach(item);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0001D8B0 File Offset: 0x0001BAB0
		protected override void OnClear()
		{
			foreach (TypeDefinition type in this)
			{
				this.Detach(type);
			}
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0001D900 File Offset: 0x0001BB00
		private void Attach(TypeDefinition type)
		{
			if (type.Module != null && type.Module != this.container)
			{
				throw new ArgumentException("Type already attached");
			}
			type.module = this.container;
			type.scope = this.container;
			this.name_cache[new Row<string, string>(type.Namespace, type.Name)] = type;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001D963 File Offset: 0x0001BB63
		private void Detach(TypeDefinition type)
		{
			type.module = null;
			type.scope = null;
			this.name_cache.Remove(new Row<string, string>(type.Namespace, type.Name));
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001D990 File Offset: 0x0001BB90
		public TypeDefinition GetType(string fullname)
		{
			string @namespace;
			string name;
			TypeParser.SplitFullName(fullname, out @namespace, out name);
			return this.GetType(@namespace, name);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001D9B0 File Offset: 0x0001BBB0
		public TypeDefinition GetType(string @namespace, string name)
		{
			TypeDefinition result;
			if (this.name_cache.TryGetValue(new Row<string, string>(@namespace, name), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x040005E5 RID: 1509
		private readonly ModuleDefinition container;

		// Token: 0x040005E6 RID: 1510
		private readonly Dictionary<Row<string, string>, TypeDefinition> name_cache;
	}
}
