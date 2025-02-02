using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Bannerlord.BUTR.Shared.Utils
{
	// Token: 0x0200012B RID: 299
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class WrappedMethodInfo : MethodInfo
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x000175AD File Offset: 0x000157AD
		public WrappedMethodInfo(MethodInfo actualMethodInfo, object instance)
		{
			this._methodInfoImplementation = actualMethodInfo;
			this._instance = instance;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x000175C5 File Offset: 0x000157C5
		public override MethodAttributes Attributes
		{
			get
			{
				return this._methodInfoImplementation.Attributes;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x000175D2 File Offset: 0x000157D2
		public override CallingConventions CallingConvention
		{
			get
			{
				return this._methodInfoImplementation.CallingConvention;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x000175DF File Offset: 0x000157DF
		public override bool ContainsGenericParameters
		{
			get
			{
				return this._methodInfoImplementation.ContainsGenericParameters;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x000175EC File Offset: 0x000157EC
		public override IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this._methodInfoImplementation.CustomAttributes;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x000175F9 File Offset: 0x000157F9
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this._methodInfoImplementation.DeclaringType;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00017606 File Offset: 0x00015806
		public override bool IsGenericMethod
		{
			get
			{
				return this._methodInfoImplementation.IsGenericMethod;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00017613 File Offset: 0x00015813
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this._methodInfoImplementation.IsGenericMethodDefinition;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00017620 File Offset: 0x00015820
		public override bool IsSecurityCritical
		{
			get
			{
				return this._methodInfoImplementation.IsSecurityCritical;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001762D File Offset: 0x0001582D
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this._methodInfoImplementation.IsSecuritySafeCritical;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0001763A File Offset: 0x0001583A
		public override bool IsSecurityTransparent
		{
			get
			{
				return this._methodInfoImplementation.IsSecurityTransparent;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00017647 File Offset: 0x00015847
		public override MemberTypes MemberType
		{
			get
			{
				return this._methodInfoImplementation.MemberType;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x00017654 File Offset: 0x00015854
		public override int MetadataToken
		{
			get
			{
				return this._methodInfoImplementation.MetadataToken;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x00017661 File Offset: 0x00015861
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this._methodInfoImplementation.MethodHandle;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001766E File Offset: 0x0001586E
		public override MethodImplAttributes MethodImplementationFlags
		{
			get
			{
				return this._methodInfoImplementation.MethodImplementationFlags;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001767B File Offset: 0x0001587B
		public override Module Module
		{
			get
			{
				return this._methodInfoImplementation.Module;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00017688 File Offset: 0x00015888
		public override string Name
		{
			get
			{
				return this._methodInfoImplementation.Name;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00017695 File Offset: 0x00015895
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this._methodInfoImplementation.ReflectedType;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x000176A2 File Offset: 0x000158A2
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return this._methodInfoImplementation.ReturnParameter;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x000176AF File Offset: 0x000158AF
		public override Type ReturnType
		{
			get
			{
				return this._methodInfoImplementation.ReturnType;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x000176BC File Offset: 0x000158BC
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return this._methodInfoImplementation.ReturnTypeCustomAttributes;
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000176C9 File Offset: 0x000158C9
		public override Delegate CreateDelegate(Type delegateType)
		{
			return this._methodInfoImplementation.CreateDelegate(delegateType);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000176D7 File Offset: 0x000158D7
		public override Delegate CreateDelegate(Type delegateType, [Nullable(2)] object target)
		{
			return this._methodInfoImplementation.CreateDelegate(delegateType, target);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000176E6 File Offset: 0x000158E6
		public override MethodInfo GetBaseDefinition()
		{
			return this._methodInfoImplementation.GetBaseDefinition();
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000176F3 File Offset: 0x000158F3
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._methodInfoImplementation.GetCustomAttributes(inherit);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00017701 File Offset: 0x00015901
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._methodInfoImplementation.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00017710 File Offset: 0x00015910
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return this._methodInfoImplementation.GetCustomAttributesData();
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001771D File Offset: 0x0001591D
		public override Type[] GetGenericArguments()
		{
			return this._methodInfoImplementation.GetGenericArguments();
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001772A File Offset: 0x0001592A
		public override MethodInfo GetGenericMethodDefinition()
		{
			return this._methodInfoImplementation.GetGenericMethodDefinition();
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00017737 File Offset: 0x00015937
		[NullableContext(2)]
		public override MethodBody GetMethodBody()
		{
			return this._methodInfoImplementation.GetMethodBody();
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00017744 File Offset: 0x00015944
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this._methodInfoImplementation.GetMethodImplementationFlags();
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00017751 File Offset: 0x00015951
		public override ParameterInfo[] GetParameters()
		{
			return this._methodInfoImplementation.GetParameters();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001775E File Offset: 0x0001595E
		[NullableContext(2)]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this._methodInfoImplementation.Invoke(this._instance, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00017777 File Offset: 0x00015977
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._methodInfoImplementation.IsDefined(attributeType, inherit);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00017786 File Offset: 0x00015986
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return this._methodInfoImplementation.MakeGenericMethod(typeArguments);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00017794 File Offset: 0x00015994
		[NullableContext(2)]
		public override string ToString()
		{
			return this._methodInfoImplementation.ToString();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000177A4 File Offset: 0x000159A4
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (!true)
			{
			}
			WrappedMethodInfo proxy = obj as WrappedMethodInfo;
			bool result;
			if (proxy == null)
			{
				MethodInfo propertyInfo = obj as MethodInfo;
				if (propertyInfo == null)
				{
					result = this._methodInfoImplementation.Equals(obj);
				}
				else
				{
					result = this._methodInfoImplementation.Equals(propertyInfo);
				}
			}
			else
			{
				result = this._methodInfoImplementation.Equals(proxy._methodInfoImplementation);
			}
			if (!true)
			{
			}
			return result;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00017806 File Offset: 0x00015A06
		public override int GetHashCode()
		{
			return this._methodInfoImplementation.GetHashCode();
		}

		// Token: 0x0400022C RID: 556
		private readonly object _instance;

		// Token: 0x0400022D RID: 557
		private readonly MethodInfo _methodInfoImplementation;
	}
}
