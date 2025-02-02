using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002FB RID: 763
	public sealed class ModsInterface : Handle
	{
		// Token: 0x06001497 RID: 5271 RVA: 0x0001E8F4 File Offset: 0x0001CAF4
		public ModsInterface()
		{
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0001E8FE File Offset: 0x0001CAFE
		public ModsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0001E90C File Offset: 0x0001CB0C
		public Result CopyModInfo(ref CopyModInfoOptions options, out ModInfo? outEnumeratedMods)
		{
			CopyModInfoOptionsInternal copyModInfoOptionsInternal = default(CopyModInfoOptionsInternal);
			copyModInfoOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Mods_CopyModInfo(base.InnerHandle, ref copyModInfoOptionsInternal, ref zero);
			Helper.Dispose<CopyModInfoOptionsInternal>(ref copyModInfoOptionsInternal);
			Helper.Get<ModInfoInternal, ModInfo>(zero, out outEnumeratedMods);
			bool flag = outEnumeratedMods != null;
			if (flag)
			{
				Bindings.EOS_Mods_ModInfo_Release(zero);
			}
			return result;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0001E96C File Offset: 0x0001CB6C
		public void EnumerateMods(ref EnumerateModsOptions options, object clientData, OnEnumerateModsCallback completionDelegate)
		{
			EnumerateModsOptionsInternal enumerateModsOptionsInternal = default(EnumerateModsOptionsInternal);
			enumerateModsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnEnumerateModsCallbackInternal onEnumerateModsCallbackInternal = new OnEnumerateModsCallbackInternal(ModsInterface.OnEnumerateModsCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onEnumerateModsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Mods_EnumerateMods(base.InnerHandle, ref enumerateModsOptionsInternal, zero, onEnumerateModsCallbackInternal);
			Helper.Dispose<EnumerateModsOptionsInternal>(ref enumerateModsOptionsInternal);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0001E9C8 File Offset: 0x0001CBC8
		public void InstallMod(ref InstallModOptions options, object clientData, OnInstallModCallback completionDelegate)
		{
			InstallModOptionsInternal installModOptionsInternal = default(InstallModOptionsInternal);
			installModOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnInstallModCallbackInternal onInstallModCallbackInternal = new OnInstallModCallbackInternal(ModsInterface.OnInstallModCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onInstallModCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Mods_InstallMod(base.InnerHandle, ref installModOptionsInternal, zero, onInstallModCallbackInternal);
			Helper.Dispose<InstallModOptionsInternal>(ref installModOptionsInternal);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0001EA24 File Offset: 0x0001CC24
		public void UninstallMod(ref UninstallModOptions options, object clientData, OnUninstallModCallback completionDelegate)
		{
			UninstallModOptionsInternal uninstallModOptionsInternal = default(UninstallModOptionsInternal);
			uninstallModOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUninstallModCallbackInternal onUninstallModCallbackInternal = new OnUninstallModCallbackInternal(ModsInterface.OnUninstallModCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUninstallModCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Mods_UninstallMod(base.InnerHandle, ref uninstallModOptionsInternal, zero, onUninstallModCallbackInternal);
			Helper.Dispose<UninstallModOptionsInternal>(ref uninstallModOptionsInternal);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0001EA80 File Offset: 0x0001CC80
		public void UpdateMod(ref UpdateModOptions options, object clientData, OnUpdateModCallback completionDelegate)
		{
			UpdateModOptionsInternal updateModOptionsInternal = default(UpdateModOptionsInternal);
			updateModOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUpdateModCallbackInternal onUpdateModCallbackInternal = new OnUpdateModCallbackInternal(ModsInterface.OnUpdateModCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUpdateModCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Mods_UpdateMod(base.InnerHandle, ref updateModOptionsInternal, zero, onUpdateModCallbackInternal);
			Helper.Dispose<UpdateModOptionsInternal>(ref updateModOptionsInternal);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0001EADC File Offset: 0x0001CCDC
		[MonoPInvokeCallback(typeof(OnEnumerateModsCallbackInternal))]
		internal static void OnEnumerateModsCallbackInternalImplementation(ref EnumerateModsCallbackInfoInternal data)
		{
			OnEnumerateModsCallback onEnumerateModsCallback;
			EnumerateModsCallbackInfo enumerateModsCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<EnumerateModsCallbackInfoInternal, OnEnumerateModsCallback, EnumerateModsCallbackInfo>(ref data, out onEnumerateModsCallback, out enumerateModsCallbackInfo);
			if (flag)
			{
				onEnumerateModsCallback(ref enumerateModsCallbackInfo);
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0001EB04 File Offset: 0x0001CD04
		[MonoPInvokeCallback(typeof(OnInstallModCallbackInternal))]
		internal static void OnInstallModCallbackInternalImplementation(ref InstallModCallbackInfoInternal data)
		{
			OnInstallModCallback onInstallModCallback;
			InstallModCallbackInfo installModCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<InstallModCallbackInfoInternal, OnInstallModCallback, InstallModCallbackInfo>(ref data, out onInstallModCallback, out installModCallbackInfo);
			if (flag)
			{
				onInstallModCallback(ref installModCallbackInfo);
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0001EB2C File Offset: 0x0001CD2C
		[MonoPInvokeCallback(typeof(OnUninstallModCallbackInternal))]
		internal static void OnUninstallModCallbackInternalImplementation(ref UninstallModCallbackInfoInternal data)
		{
			OnUninstallModCallback onUninstallModCallback;
			UninstallModCallbackInfo uninstallModCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UninstallModCallbackInfoInternal, OnUninstallModCallback, UninstallModCallbackInfo>(ref data, out onUninstallModCallback, out uninstallModCallbackInfo);
			if (flag)
			{
				onUninstallModCallback(ref uninstallModCallbackInfo);
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0001EB54 File Offset: 0x0001CD54
		[MonoPInvokeCallback(typeof(OnUpdateModCallbackInternal))]
		internal static void OnUpdateModCallbackInternalImplementation(ref UpdateModCallbackInfoInternal data)
		{
			OnUpdateModCallback onUpdateModCallback;
			UpdateModCallbackInfo updateModCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UpdateModCallbackInfoInternal, OnUpdateModCallback, UpdateModCallbackInfo>(ref data, out onUpdateModCallback, out updateModCallbackInfo);
			if (flag)
			{
				onUpdateModCallback(ref updateModCallbackInfo);
			}
		}

		// Token: 0x0400093F RID: 2367
		public const int CopymodinfoApiLatest = 1;

		// Token: 0x04000940 RID: 2368
		public const int EnumeratemodsApiLatest = 1;

		// Token: 0x04000941 RID: 2369
		public const int InstallmodApiLatest = 1;

		// Token: 0x04000942 RID: 2370
		public const int ModIdentifierApiLatest = 1;

		// Token: 0x04000943 RID: 2371
		public const int ModinfoApiLatest = 1;

		// Token: 0x04000944 RID: 2372
		public const int UninstallmodApiLatest = 1;

		// Token: 0x04000945 RID: 2373
		public const int UpdatemodApiLatest = 1;
	}
}
