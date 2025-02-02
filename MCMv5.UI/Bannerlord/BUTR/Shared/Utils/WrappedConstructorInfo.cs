using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Bannerlord.BUTR.Shared.Utils
{
	// Token: 0x0200003D RID: 61
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class WrappedConstructorInfo : ConstructorInfo
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000981F File Offset: 0x00007A1F
		public WrappedConstructorInfo(ConstructorInfo actualConstructorInfo, object[] args)
		{
			this._constructorInfoImplementation = actualConstructorInfo;
			this._args = args;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00009837 File Offset: 0x00007A37
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._constructorInfoImplementation.GetCustomAttributes(inherit);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00009845 File Offset: 0x00007A45
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._constructorInfoImplementation.IsDefined(attributeType, inherit);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009854 File Offset: 0x00007A54
		public override ParameterInfo[] GetParameters()
		{
			return this._constructorInfoImplementation.GetParameters();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009861 File Offset: 0x00007A61
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this._constructorInfoImplementation.GetMethodImplementationFlags();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000986E File Offset: 0x00007A6E
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this._constructorInfoImplementation.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00009882 File Offset: 0x00007A82
		public override string Name
		{
			get
			{
				return this._constructorInfoImplementation.Name;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000988F File Offset: 0x00007A8F
		[Nullable(2)]
		public override Type DeclaringType
		{
			[NullableContext(2)]
			get
			{
				return this._constructorInfoImplementation.DeclaringType;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000989C File Offset: 0x00007A9C
		[Nullable(2)]
		public override Type ReflectedType
		{
			[NullableContext(2)]
			get
			{
				return this._constructorInfoImplementation.ReflectedType;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000221 RID: 545 RVA: 0x000098A9 File Offset: 0x00007AA9
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this._constructorInfoImplementation.MethodHandle;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000222 RID: 546 RVA: 0x000098B6 File Offset: 0x00007AB6
		public override MethodAttributes Attributes
		{
			get
			{
				return this._constructorInfoImplementation.Attributes;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000098C3 File Offset: 0x00007AC3
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._constructorInfoImplementation.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000098D2 File Offset: 0x00007AD2
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this._constructorInfoImplementation.Invoke(invokeAttr, binder, this._args, culture);
		}

		// Token: 0x0400009D RID: 157
		private readonly object[] _args;

		// Token: 0x0400009E RID: 158
		private readonly ConstructorInfo _constructorInfoImplementation;
	}
}
