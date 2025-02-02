using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace MCM.LightInject
{
	// Token: 0x020000F4 RID: 244
	[ExcludeFromCodeCoverage]
	internal class DynamicMethod
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x000122C4 File Offset: 0x000104C4
		public DynamicMethod(Type returnType, Type[] parameterTypes)
		{
			this.returnType = returnType;
			this.parameterTypes = parameterTypes;
			Func<Type, ParameterExpression> selector;
			if ((selector = DynamicMethod.<>O.<0>__Parameter) == null)
			{
				selector = (DynamicMethod.<>O.<0>__Parameter = new Func<Type, ParameterExpression>(Expression.Parameter));
			}
			this.parameters = parameterTypes.Select(selector).ToArray<ParameterExpression>();
			this.generator = new ILGenerator(this.parameters);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00012324 File Offset: 0x00010524
		public Delegate CreateDelegate(Type delegateType)
		{
			LambdaExpression lambda = Expression.Lambda(delegateType, this.generator.CurrentExpression, this.parameters);
			return lambda.Compile();
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00012354 File Offset: 0x00010554
		public Delegate CreateDelegate(Type delegateType, object target)
		{
			Type delegateTypeWithTargetParameter = Expression.GetDelegateType(this.parameterTypes.Concat(new Type[]
			{
				this.returnType
			}).ToArray<Type>());
			LambdaExpression lambdaWithTargetParameter = Expression.Lambda(delegateTypeWithTargetParameter, this.generator.CurrentExpression, true, this.parameters);
			Expression[] arguments = new Expression[]
			{
				Expression.Constant(target)
			}.Concat(this.parameters.Cast<Expression>().Skip(1)).ToArray<Expression>();
			InvocationExpression invokeExpression = Expression.Invoke(lambdaWithTargetParameter, arguments);
			LambdaExpression lambda = Expression.Lambda(delegateType, invokeExpression, this.parameters.Skip(1));
			return lambda.Compile();
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000123F8 File Offset: 0x000105F8
		public ILGenerator GetILGenerator()
		{
			return this.generator;
		}

		// Token: 0x040001A3 RID: 419
		private readonly Type returnType;

		// Token: 0x040001A4 RID: 420
		private readonly Type[] parameterTypes;

		// Token: 0x040001A5 RID: 421
		private readonly ParameterExpression[] parameters;

		// Token: 0x040001A6 RID: 422
		private readonly ILGenerator generator;

		// Token: 0x0200021D RID: 541
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040004BA RID: 1210
			public static Func<Type, ParameterExpression> <0>__Parameter;
		}
	}
}
