using System;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200002F RID: 47
	public sealed class WeakNativeObjectReference
	{
		// Token: 0x0600012E RID: 302 RVA: 0x000053D6 File Offset: 0x000035D6
		public WeakNativeObjectReference(NativeObject nativeObject)
		{
			if (nativeObject != null)
			{
				this._pointer = nativeObject.Pointer;
				this._constructor = (Func<NativeObject>)Managed.GetConstructorDelegateOfWeakReferenceClass(nativeObject.GetType());
				this._weakReferenceCache = new WeakReference(nativeObject);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00005418 File Offset: 0x00003618
		public void ManualInvalidate()
		{
			NativeObject nativeObject = (NativeObject)this._weakReferenceCache.Target;
			if (nativeObject != null)
			{
				nativeObject.ManualInvalidate();
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005448 File Offset: 0x00003648
		public NativeObject GetNativeObject()
		{
			if (!(this._pointer != UIntPtr.Zero))
			{
				return null;
			}
			NativeObject nativeObject = (NativeObject)this._weakReferenceCache.Target;
			if (nativeObject != null)
			{
				return nativeObject;
			}
			NativeObject nativeObject2 = this._constructor();
			nativeObject2.Construct(this._pointer);
			this._weakReferenceCache.Target = nativeObject2;
			return nativeObject2;
		}

		// Token: 0x0400006D RID: 109
		private readonly UIntPtr _pointer;

		// Token: 0x0400006E RID: 110
		private readonly Func<NativeObject> _constructor;

		// Token: 0x0400006F RID: 111
		private readonly WeakReference _weakReferenceCache;
	}
}
