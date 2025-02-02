using System;
using System.Reflection;
using TaleWorlds.Library;

namespace TaleWorlds.SaveSystem.Definition
{
	// Token: 0x02000065 RID: 101
	public class PropertyDefinition : MemberDefinition
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000C3D0 File Offset: 0x0000A5D0
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		public PropertyInfo PropertyInfo { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000C3E1 File Offset: 0x0000A5E1
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000C3E9 File Offset: 0x0000A5E9
		public SaveablePropertyAttribute SaveablePropertyAttribute { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000C3F2 File Offset: 0x0000A5F2
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x0000C3FA File Offset: 0x0000A5FA
		public MethodInfo GetMethod { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000C403 File Offset: 0x0000A603
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000C40B File Offset: 0x0000A60B
		public MethodInfo SetMethod { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000C414 File Offset: 0x0000A614
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000C41C File Offset: 0x0000A61C
		public GetPropertyValueDelegate GetPropertyValueMethod { get; private set; }

		// Token: 0x060002FE RID: 766 RVA: 0x0000C428 File Offset: 0x0000A628
		public PropertyDefinition(PropertyInfo propertyInfo, MemberTypeId id) : base(propertyInfo, id)
		{
			this.PropertyInfo = propertyInfo;
			this.SaveablePropertyAttribute = propertyInfo.GetCustomAttribute<SaveablePropertyAttribute>();
			this.SetMethod = this.PropertyInfo.GetSetMethod(true);
			if (this.SetMethod == null && this.PropertyInfo.DeclaringType != null)
			{
				PropertyInfo property = this.PropertyInfo.DeclaringType.GetProperty(this.PropertyInfo.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (property != null)
				{
					this.SetMethod = property.GetSetMethod(true);
				}
			}
			if (this.SetMethod == null)
			{
				Debug.FailedAssert(string.Concat(new string[]
				{
					"Property ",
					this.PropertyInfo.Name,
					" at Type ",
					this.PropertyInfo.DeclaringType.FullName,
					" does not have setter method."
				}), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.SaveSystem\\Definition\\PropertyDefinition.cs", ".ctor", 39);
				throw new Exception(string.Concat(new string[]
				{
					"Property ",
					this.PropertyInfo.Name,
					" at Type ",
					this.PropertyInfo.DeclaringType.FullName,
					" does not have setter method."
				}));
			}
			this.GetMethod = this.PropertyInfo.GetGetMethod(true);
			if (this.GetMethod == null && this.PropertyInfo.DeclaringType != null)
			{
				PropertyInfo property2 = this.PropertyInfo.DeclaringType.GetProperty(this.PropertyInfo.Name);
				if (property2 != null)
				{
					this.GetMethod = property2.GetGetMethod(true);
				}
			}
			if (this.GetMethod == null)
			{
				throw new Exception(string.Concat(new string[]
				{
					"Property ",
					this.PropertyInfo.Name,
					" at Type ",
					this.PropertyInfo.DeclaringType.FullName,
					" does not have getter method."
				}));
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C624 File Offset: 0x0000A824
		public override Type GetMemberType()
		{
			return this.PropertyInfo.PropertyType;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000C634 File Offset: 0x0000A834
		public override object GetValue(object target)
		{
			object result;
			if (this.GetPropertyValueMethod != null)
			{
				result = this.GetPropertyValueMethod(target);
			}
			else
			{
				result = this.GetMethod.Invoke(target, new object[0]);
			}
			return result;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000C66C File Offset: 0x0000A86C
		public void InitializeForAutoGeneration(GetPropertyValueDelegate getPropertyValueMethod)
		{
			this.GetPropertyValueMethod = getPropertyValueMethod;
		}
	}
}
