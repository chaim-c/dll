using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.SaveSystem.Definition;
using TaleWorlds.SaveSystem.Resolvers;

namespace TaleWorlds.SaveSystem
{
	// Token: 0x02000015 RID: 21
	public abstract class SaveableTypeDefiner
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00003669 File Offset: 0x00001869
		protected SaveableTypeDefiner(int saveBaseId)
		{
			this._saveBaseId = saveBaseId;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003678 File Offset: 0x00001878
		internal void Initialize(DefinitionContext definitionContext)
		{
			this._definitionContext = definitionContext;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003681 File Offset: 0x00001881
		protected internal virtual void DefineBasicTypes()
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003683 File Offset: 0x00001883
		protected internal virtual void DefineClassTypes()
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003685 File Offset: 0x00001885
		protected internal virtual void DefineStructTypes()
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003687 File Offset: 0x00001887
		protected internal virtual void DefineInterfaceTypes()
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003689 File Offset: 0x00001889
		protected internal virtual void DefineEnumTypes()
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000368B File Offset: 0x0000188B
		protected internal virtual void DefineRootClassTypes()
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000368D File Offset: 0x0000188D
		protected internal virtual void DefineGenericClassDefinitions()
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000368F File Offset: 0x0000188F
		protected internal virtual void DefineGenericStructDefinitions()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003691 File Offset: 0x00001891
		protected internal virtual void DefineContainerDefinitions()
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003693 File Offset: 0x00001893
		protected void ConstructGenericClassDefinition(Type type)
		{
			this._definitionContext.ConstructGenericClassDefinition(type);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000036A2 File Offset: 0x000018A2
		protected void ConstructGenericStructDefinition(Type type)
		{
			this._definitionContext.ConstructGenericStructDefinition(type);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000036B4 File Offset: 0x000018B4
		protected void AddBasicTypeDefinition(Type type, int saveId, IBasicTypeSerializer serializer)
		{
			BasicTypeDefinition basicTypeDefinition = new BasicTypeDefinition(type, this._saveBaseId + saveId, serializer);
			this._definitionContext.AddBasicTypeDefinition(basicTypeDefinition);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000036E0 File Offset: 0x000018E0
		protected void AddClassDefinition(Type type, int saveId, IObjectResolver resolver = null)
		{
			TypeDefinition classDefinition = new TypeDefinition(type, this._saveBaseId + saveId, resolver);
			this._definitionContext.AddClassDefinition(classDefinition);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000370C File Offset: 0x0000190C
		protected void AddClassDefinitionWithCustomFields(Type type, int saveId, IEnumerable<Tuple<string, short>> fields, IObjectResolver resolver = null)
		{
			TypeDefinition typeDefinition = new TypeDefinition(type, this._saveBaseId + saveId, resolver);
			this._definitionContext.AddClassDefinition(typeDefinition);
			foreach (Tuple<string, short> tuple in fields)
			{
				typeDefinition.AddCustomField(tuple.Item1, tuple.Item2);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000377C File Offset: 0x0000197C
		protected void AddStructDefinitionWithCustomFields(Type type, int saveId, IEnumerable<Tuple<string, short>> fields, IObjectResolver resolver = null)
		{
			StructDefinition structDefinition = new StructDefinition(type, this._saveBaseId + saveId, resolver);
			this._definitionContext.AddStructDefinition(structDefinition);
			foreach (Tuple<string, short> tuple in fields)
			{
				structDefinition.AddCustomField(tuple.Item1, tuple.Item2);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000037EC File Offset: 0x000019EC
		protected void AddRootClassDefinition(Type type, int saveId, IObjectResolver resolver = null)
		{
			TypeDefinition rootClassDefinition = new TypeDefinition(type, this._saveBaseId + saveId, resolver);
			this._definitionContext.AddRootClassDefinition(rootClassDefinition);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003818 File Offset: 0x00001A18
		protected void AddStructDefinition(Type type, int saveId, IObjectResolver resolver = null)
		{
			StructDefinition structDefinition = new StructDefinition(type, this._saveBaseId + saveId, resolver);
			this._definitionContext.AddStructDefinition(structDefinition);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003844 File Offset: 0x00001A44
		protected void AddInterfaceDefinition(Type type, int saveId)
		{
			InterfaceDefinition interfaceDefinition = new InterfaceDefinition(type, this._saveBaseId + saveId);
			this._definitionContext.AddInterfaceDefinition(interfaceDefinition);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000386C File Offset: 0x00001A6C
		protected void AddEnumDefinition(Type type, int saveId, IEnumResolver enumResolver = null)
		{
			EnumDefinition enumDefinition = new EnumDefinition(type, this._saveBaseId + saveId, enumResolver);
			this._definitionContext.AddEnumDefinition(enumDefinition);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003898 File Offset: 0x00001A98
		protected void ConstructContainerDefinition(Type type)
		{
			if (!this._definitionContext.HasDefinition(type))
			{
				Assembly assembly = base.GetType().Assembly;
				this._definitionContext.ConstructContainerDefinition(type, assembly);
			}
		}

		// Token: 0x0400001B RID: 27
		private DefinitionContext _definitionContext;

		// Token: 0x0400001C RID: 28
		private readonly int _saveBaseId;
	}
}
