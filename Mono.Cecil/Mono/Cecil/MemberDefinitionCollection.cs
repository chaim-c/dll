using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000DE RID: 222
	internal class MemberDefinitionCollection<T> : Collection<T> where T : IMemberDefinition
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0001B91B File Offset: 0x00019B1B
		internal MemberDefinitionCollection(TypeDefinition container)
		{
			this.container = container;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001B92A File Offset: 0x00019B2A
		internal MemberDefinitionCollection(TypeDefinition container, int capacity) : base(capacity)
		{
			this.container = container;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001B93A File Offset: 0x00019B3A
		protected override void OnAdd(T item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001B943 File Offset: 0x00019B43
		protected sealed override void OnSet(T item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001B94C File Offset: 0x00019B4C
		protected sealed override void OnInsert(T item, int index)
		{
			this.Attach(item);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001B955 File Offset: 0x00019B55
		protected sealed override void OnRemove(T item, int index)
		{
			MemberDefinitionCollection<T>.Detach(item);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001B960 File Offset: 0x00019B60
		protected sealed override void OnClear()
		{
			foreach (T element in this)
			{
				MemberDefinitionCollection<T>.Detach(element);
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001B9B0 File Offset: 0x00019BB0
		private void Attach(T element)
		{
			if (element.DeclaringType == this.container)
			{
				return;
			}
			if (element.DeclaringType != null)
			{
				throw new ArgumentException("Member already attached");
			}
			element.DeclaringType = this.container;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001BA00 File Offset: 0x00019C00
		private static void Detach(T element)
		{
			element.DeclaringType = null;
		}

		// Token: 0x04000555 RID: 1365
		private TypeDefinition container;
	}
}
