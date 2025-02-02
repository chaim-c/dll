using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem.Load;
using TaleWorlds.SaveSystem.Resolvers;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x0200006A RID: 106
	public class TypeDefinition : TypeDefinitionBase
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000E4CD File Offset: 0x0000C6CD
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000E4D5 File Offset: 0x0000C6D5
		public List<MemberDefinition> MemberDefinitions { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000E4DE File Offset: 0x0000C6DE
		public IEnumerable<MethodInfo> InitializationCallbacks
		{
			get
			{
				return this._initializationCallbacks;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000E4E6 File Offset: 0x0000C6E6
		public IEnumerable<MethodInfo> LateInitializationCallbacks
		{
			get
			{
				return this._lateInitializationCallbacks;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000E4EE File Offset: 0x0000C6EE
		public IEnumerable<string> Errors
		{
			get
			{
				return this._errors.AsReadOnly();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000E4FB File Offset: 0x0000C6FB
		public bool IsClassDefinition
		{
			get
			{
				return this._isClass;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000E503 File Offset: 0x0000C703
		// (set) Token: 0x06000333 RID: 819 RVA: 0x0000E50B File Offset: 0x0000C70B
		public List<CustomField> CustomFields { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000E514 File Offset: 0x0000C714
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000E51C File Offset: 0x0000C71C
		public CollectObjectsDelegate CollectObjectsMethod { get; private set; }

		// Token: 0x06000336 RID: 822 RVA: 0x0000E528 File Offset: 0x0000C728
		public TypeDefinition(Type type, SaveId saveId, IObjectResolver objectResolver) : base(type, saveId)
		{
			this._isClass = base.Type.IsClass;
			this._errors = new List<string>();
			this._properties = new Dictionary<MemberTypeId, PropertyDefinition>();
			this._fields = new Dictionary<MemberTypeId, FieldDefinition>();
			this.MemberDefinitions = new List<MemberDefinition>();
			this.CustomFields = new List<CustomField>();
			this._initializationCallbacks = new List<MethodInfo>();
			this._lateInitializationCallbacks = new List<MethodInfo>();
			this._objectResolver = objectResolver;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000E5A2 File Offset: 0x0000C7A2
		public TypeDefinition(Type type, int saveId, IObjectResolver objectResolver) : this(type, new TypeSaveId(saveId), objectResolver)
		{
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000E5B2 File Offset: 0x0000C7B2
		public bool CheckIfRequiresAdvancedResolving(object originalObject)
		{
			return this._objectResolver != null && this._objectResolver.CheckIfRequiresAdvancedResolving(originalObject);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000E5CA File Offset: 0x0000C7CA
		public object ResolveObject(object originalObject)
		{
			if (this._objectResolver != null)
			{
				return this._objectResolver.ResolveObject(originalObject);
			}
			return originalObject;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E5E2 File Offset: 0x0000C7E2
		public object AdvancedResolveObject(object originalObject, MetaData metaData, ObjectLoadData objectLoadData)
		{
			if (this._objectResolver != null)
			{
				return this._objectResolver.AdvancedResolveObject(originalObject, metaData, objectLoadData);
			}
			return originalObject;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		public void CollectInitializationCallbacks()
		{
			Type type = base.Type;
			while (type != typeof(object))
			{
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.DeclaringType == type)
					{
						if (methodInfo.GetCustomAttributesSafe(typeof(LoadInitializationCallback)).ToArray<Attribute>().Length != 0 && !this._initializationCallbacks.Contains(methodInfo))
						{
							this._initializationCallbacks.Insert(0, methodInfo);
						}
						if (methodInfo.GetCustomAttributesSafe(typeof(LateLoadInitializationCallback)).ToArray<Attribute>().Length != 0 && !this._lateInitializationCallbacks.Contains(methodInfo))
						{
							this._lateInitializationCallbacks.Insert(0, methodInfo);
						}
					}
				}
				type = type.BaseType;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
		public void CollectProperties()
		{
			foreach (PropertyInfo propertyInfo in base.Type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				Attribute[] array = propertyInfo.GetCustomAttributesSafe(typeof(SaveablePropertyAttribute)).ToArray<Attribute>();
				if (array.Length != 0)
				{
					SaveablePropertyAttribute saveablePropertyAttribute = (SaveablePropertyAttribute)array[0];
					byte classLevel = TypeDefinitionBase.GetClassLevel(propertyInfo.DeclaringType);
					MemberTypeId memberTypeId = new MemberTypeId(classLevel, saveablePropertyAttribute.LocalSaveId);
					PropertyDefinition propertyDefinition = new PropertyDefinition(propertyInfo, memberTypeId);
					if (this._properties.ContainsKey(memberTypeId))
					{
						this._errors.Add(string.Concat(new object[]
						{
							"SaveId ",
							memberTypeId,
							" of property ",
							propertyDefinition.PropertyInfo.Name,
							" is already defined in type ",
							base.Type.FullName
						}));
					}
					else
					{
						this._properties.Add(memberTypeId, propertyDefinition);
						this.MemberDefinitions.Add(propertyDefinition);
					}
				}
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000E7C7 File Offset: 0x0000C9C7
		private static IEnumerable<FieldInfo> GetFieldsOfType(Type type)
		{
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (!fieldInfo.IsPrivate)
				{
					yield return fieldInfo;
				}
			}
			FieldInfo[] array = null;
			Type typeToCheck = type;
			while (typeToCheck != typeof(object))
			{
				FieldInfo[] fields2 = typeToCheck.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
				foreach (FieldInfo fieldInfo2 in fields2)
				{
					if (fieldInfo2.IsPrivate)
					{
						yield return fieldInfo2;
					}
				}
				array = null;
				typeToCheck = typeToCheck.BaseType;
			}
			yield break;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
		public void CollectFields()
		{
			foreach (FieldInfo fieldInfo in TypeDefinition.GetFieldsOfType(base.Type).ToArray<FieldInfo>())
			{
				Attribute[] array2 = fieldInfo.GetCustomAttributesSafe(typeof(SaveableFieldAttribute)).ToArray<Attribute>();
				if (array2.Length != 0)
				{
					SaveableFieldAttribute saveableFieldAttribute = (SaveableFieldAttribute)array2[0];
					byte classLevel = TypeDefinitionBase.GetClassLevel(fieldInfo.DeclaringType);
					MemberTypeId memberTypeId = new MemberTypeId(classLevel, saveableFieldAttribute.LocalSaveId);
					FieldDefinition fieldDefinition = new FieldDefinition(fieldInfo, memberTypeId);
					if (this._fields.ContainsKey(memberTypeId))
					{
						this._errors.Add(string.Concat(new object[]
						{
							"SaveId ",
							memberTypeId,
							" of field ",
							fieldDefinition.FieldInfo,
							" is already defined in type ",
							base.Type.FullName
						}));
					}
					else
					{
						this._fields.Add(memberTypeId, fieldDefinition);
						this.MemberDefinitions.Add(fieldDefinition);
					}
				}
			}
			foreach (CustomField customField in this.CustomFields)
			{
				string name = customField.Name;
				short saveId = customField.SaveId;
				FieldInfo field = base.Type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				byte classLevel2 = TypeDefinitionBase.GetClassLevel(field.DeclaringType);
				MemberTypeId memberTypeId2 = new MemberTypeId(classLevel2, saveId);
				FieldDefinition fieldDefinition2 = new FieldDefinition(field, memberTypeId2);
				if (this._fields.ContainsKey(memberTypeId2))
				{
					this._errors.Add(string.Concat(new object[]
					{
						"SaveId ",
						memberTypeId2,
						" of field ",
						fieldDefinition2.FieldInfo,
						" is already defined in type ",
						base.Type.FullName
					}));
				}
				else
				{
					this._fields.Add(memberTypeId2, fieldDefinition2);
					this.MemberDefinitions.Add(fieldDefinition2);
				}
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000E9DC File Offset: 0x0000CBDC
		public void AddCustomField(string fieldName, short saveId)
		{
			this.CustomFields.Add(new CustomField(fieldName, saveId));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		public PropertyDefinition GetPropertyDefinitionWithId(MemberTypeId id)
		{
			PropertyDefinition result;
			this._properties.TryGetValue(id, out result);
			return result;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000EA10 File Offset: 0x0000CC10
		public FieldDefinition GetFieldDefinitionWithId(MemberTypeId id)
		{
			FieldDefinition result;
			this._fields.TryGetValue(id, out result);
			return result;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000EA2D File Offset: 0x0000CC2D
		public Dictionary<MemberTypeId, PropertyDefinition>.ValueCollection PropertyDefinitions
		{
			get
			{
				return this._properties.Values;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000EA3A File Offset: 0x0000CC3A
		public Dictionary<MemberTypeId, FieldDefinition>.ValueCollection FieldDefinitions
		{
			get
			{
				return this._fields.Values;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000EA47 File Offset: 0x0000CC47
		public void InitializeForAutoGeneration(CollectObjectsDelegate collectObjectsDelegate)
		{
			this.CollectObjectsMethod = collectObjectsDelegate;
		}

		// Token: 0x040000FF RID: 255
		private Dictionary<MemberTypeId, PropertyDefinition> _properties;

		// Token: 0x04000100 RID: 256
		private Dictionary<MemberTypeId, FieldDefinition> _fields;

		// Token: 0x04000102 RID: 258
		private List<string> _errors;

		// Token: 0x04000103 RID: 259
		private List<MethodInfo> _initializationCallbacks;

		// Token: 0x04000104 RID: 260
		private List<MethodInfo> _lateInitializationCallbacks;

		// Token: 0x04000105 RID: 261
		private bool _isClass;

		// Token: 0x04000106 RID: 262
		private readonly IObjectResolver _objectResolver;
	}
}
