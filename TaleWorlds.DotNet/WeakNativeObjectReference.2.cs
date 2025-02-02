using System;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000030 RID: 48
	public sealed class WeakNativeObjectReference<T> where T : NativeObject
	{
		// Token: 0x06000131 RID: 305 RVA: 0x000054AA File Offset: 0x000036AA
		public WeakNativeObjectReference(T nativeObject)
		{
			if (nativeObject != null)
			{
				this._pointer = nativeObject.Pointer;
				this._weakReferenceCache = new WeakReference<T>(nativeObject);
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000054E0 File Offset: 0x000036E0
		public void ManualInvalidate()
		{
			T t;
			if (this._weakReferenceCache.TryGetTarget(out t) && t != null)
			{
				t.ManualInvalidate();
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005518 File Offset: 0x00003718
		public NativeObject GetNativeObject()
		{
			if (!(this._pointer != UIntPtr.Zero))
			{
				return null;
			}
			T t;
			if (this._weakReferenceCache.TryGetTarget(out t) && t != null)
			{
				return t;
			}
			T t2 = (T)((object)Activator.CreateInstance(typeof(T), new object[]
			{
				this._pointer
			}));
			this._weakReferenceCache.SetTarget(t2);
			return t2;
		}

		// Token: 0x04000070 RID: 112
		private readonly UIntPtr _pointer;

		// Token: 0x04000071 RID: 113
		private WeakReference<T> _weakReferenceCache;
	}
}
