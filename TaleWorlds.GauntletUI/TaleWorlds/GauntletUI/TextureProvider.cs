using System;
using System.Collections.Generic;
using System.Reflection;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200002F RID: 47
	public abstract class TextureProvider
	{
		// Token: 0x06000336 RID: 822 RVA: 0x0000E70D File Offset: 0x0000C90D
		public virtual void SetTargetSize(int width, int height)
		{
		}

		// Token: 0x06000337 RID: 823
		public abstract Texture GetTexture(TwoDimensionContext twoDimensionContext, string name);

		// Token: 0x06000338 RID: 824 RVA: 0x0000E70F File Offset: 0x0000C90F
		public virtual void Tick(float dt)
		{
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000E711 File Offset: 0x0000C911
		public virtual void Clear(bool clearNextFrame)
		{
			this._getGetMethodCache.Clear();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000E720 File Offset: 0x0000C920
		public void SetProperty(string name, object value)
		{
			PropertyInfo property = base.GetType().GetProperty(name);
			if (property != null)
			{
				property.GetSetMethod().Invoke(this, new object[]
				{
					value
				});
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000E75C File Offset: 0x0000C95C
		public object GetProperty(string name)
		{
			MethodInfo methodInfo;
			if (this._getGetMethodCache.TryGetValue(name, out methodInfo))
			{
				return methodInfo.Invoke(this, null);
			}
			PropertyInfo property = base.GetType().GetProperty(name);
			if (property != null)
			{
				MethodInfo getMethod = property.GetGetMethod();
				this._getGetMethodCache.Add(name, getMethod);
				return getMethod.Invoke(this, null);
			}
			return null;
		}

		// Token: 0x0400018F RID: 399
		private Dictionary<string, MethodInfo> _getGetMethodCache = new Dictionary<string, MethodInfo>();
	}
}
