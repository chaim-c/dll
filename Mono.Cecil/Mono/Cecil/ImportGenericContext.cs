using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000099 RID: 153
	internal struct ImportGenericContext
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001610A File Offset: 0x0001430A
		public bool IsEmpty
		{
			get
			{
				return this.stack == null;
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00016115 File Offset: 0x00014315
		public ImportGenericContext(IGenericParameterProvider provider)
		{
			this.stack = null;
			this.Push(provider);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00016128 File Offset: 0x00014328
		public void Push(IGenericParameterProvider provider)
		{
			if (this.stack == null)
			{
				this.stack = new Collection<IGenericParameterProvider>(1)
				{
					provider
				};
				return;
			}
			this.stack.Add(provider);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001615F File Offset: 0x0001435F
		public void Pop()
		{
			this.stack.RemoveAt(this.stack.Count - 1);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001617C File Offset: 0x0001437C
		public TypeReference MethodParameter(string method, int position)
		{
			for (int i = this.stack.Count - 1; i >= 0; i--)
			{
				MethodReference methodReference = this.stack[i] as MethodReference;
				if (methodReference != null && !(method != methodReference.Name))
				{
					return methodReference.GenericParameters[position];
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000161D8 File Offset: 0x000143D8
		public TypeReference TypeParameter(string type, int position)
		{
			for (int i = this.stack.Count - 1; i >= 0; i--)
			{
				TypeReference typeReference = ImportGenericContext.GenericTypeFor(this.stack[i]);
				if (!(typeReference.FullName != type))
				{
					return typeReference.GenericParameters[position];
				}
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00016230 File Offset: 0x00014430
		private static TypeReference GenericTypeFor(IGenericParameterProvider context)
		{
			TypeReference typeReference = context as TypeReference;
			if (typeReference != null)
			{
				return typeReference.GetElementType();
			}
			MethodReference methodReference = context as MethodReference;
			if (methodReference != null)
			{
				return methodReference.DeclaringType.GetElementType();
			}
			throw new InvalidOperationException();
		}

		// Token: 0x040003F3 RID: 1011
		private Collection<IGenericParameterProvider> stack;
	}
}
