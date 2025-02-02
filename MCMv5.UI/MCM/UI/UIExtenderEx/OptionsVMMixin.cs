using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Bannerlord.UIExtenderEx.Attributes;
using Bannerlord.UIExtenderEx.ViewModels;
using HarmonyLib;
using HarmonyLib.BUTR.Extensions;
using MCM.UI.GUI.ViewModels;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions;

namespace MCM.UI.UIExtenderEx
{
	// Token: 0x02000018 RID: 24
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	[ViewModelMixin]
	internal sealed class OptionsVMMixin : BaseViewModelMixin<OptionsVM>
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00003BE0 File Offset: 0x00001DE0
		static OptionsVMMixin()
		{
			Harmony harmony = new Harmony("bannerlord.mcm.ui.optionsvm");
			harmony.CreateReversePatcher(AccessTools2.Method(typeof(OptionsVM), "ExecuteCloseOptions", null, null, true), new HarmonyMethod(SymbolExtensions2.GetMethodInfo<OptionsVM>((OptionsVM x) => OptionsVMMixin.OriginalExecuteCloseOptions(x)))).Patch(HarmonyReversePatchType.Original);
			harmony.Patch(AccessTools2.Method(typeof(OptionsVM), "ExecuteCloseOptions", null, null, true), null, new HarmonyMethod(SymbolExtensions2.GetMethodInfo<OptionsVM>((OptionsVM x) => OptionsVMMixin.ExecuteCloseOptionsPostfix(x)), 300, null, null, null), null, null);
			harmony.Patch(AccessTools2.Method(typeof(OptionsVM), "RefreshValues", null, null, true), null, new HarmonyMethod(SymbolExtensions2.GetMethodInfo<OptionsVM>((OptionsVM x) => OptionsVMMixin.RefreshValuesPostfix(x)), 300, null, null, null), null, null);
			harmony.Patch(AccessTools2.PropertySetter(typeof(OptionsVM), "CategoryIndex", true), null, new HarmonyMethod(SymbolExtensions2.GetMethodInfo<OptionsVM>((OptionsVM x) => OptionsVMMixin.SetSelectedCategoryPostfix(x)), 300, null, null, null), null, null);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003E2C File Offset: 0x0000202C
		private static void ExecuteCloseOptionsPostfix(OptionsVM __instance)
		{
			WeakReference<OptionsVMMixin> weakReference = __instance.GetPropertyValue("MCMMixin") as WeakReference<OptionsVMMixin>;
			OptionsVMMixin mixin;
			bool flag = weakReference != null && weakReference.TryGetTarget(out mixin);
			if (flag)
			{
				if (mixin != null)
				{
					mixin.ExecuteCloseOptions();
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003E6C File Offset: 0x0000206C
		private static void RefreshValuesPostfix(OptionsVM __instance)
		{
			ModOptionsVM modOptions = __instance.GetPropertyValue("ModOptions") as ModOptionsVM;
			bool flag = modOptions != null;
			if (flag)
			{
				modOptions.RefreshValues();
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003E9C File Offset: 0x0000209C
		private static void SetSelectedCategoryPostfix(OptionsVM __instance)
		{
			WeakReference<OptionsVMMixin> weakReference = __instance.GetPropertyValue("MCMMixin") as WeakReference<OptionsVMMixin>;
			OptionsVMMixin mixin;
			bool flag = weakReference != null && weakReference.TryGetTarget(out mixin);
			if (flag)
			{
				mixin.ModOptionsSelected = (__instance.CategoryIndex == 4);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003EDF File Offset: 0x000020DF
		private static void OriginalExecuteCloseOptions(OptionsVM instance)
		{
			throw new NotImplementedException("It's a stub");
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003EEB File Offset: 0x000020EB
		[DataSourceProperty]
		public WeakReference<OptionsVMMixin> MCMMixin
		{
			get
			{
				return new WeakReference<OptionsVMMixin>(this);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003EF3 File Offset: 0x000020F3
		[DataSourceProperty]
		public ModOptionsVM ModOptions
		{
			get
			{
				return this._modOptions;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003EFB File Offset: 0x000020FB
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003F03 File Offset: 0x00002103
		[DataSourceProperty]
		public int DescriptionWidth
		{
			get
			{
				return this._descriptionWidth;
			}
			private set
			{
				base.SetField<int>(ref this._descriptionWidth, value, "DescriptionWidth");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003F18 File Offset: 0x00002118
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003F20 File Offset: 0x00002120
		[DataSourceProperty]
		public bool ModOptionsSelected
		{
			get
			{
				return this._modOptionsSelected;
			}
			set
			{
				bool flag = !base.SetField<bool>(ref this._modOptionsSelected, value, "ModOptionsSelected");
				if (!flag)
				{
					this._modOptions.IsDisabled = !value;
					this._modOptionsSelected = value;
					this.DescriptionWidth = (this.ModOptionsSelected ? 0 : 650);
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003F78 File Offset: 0x00002178
		public unsafe OptionsVMMixin(OptionsVM vm) : base(vm)
		{
			vm.PropertyChanged += this.OptionsVM_PropertyChanged;
			AccessTools.FieldRef<OptionsVM, List<ViewModel>> categories = OptionsVMMixin._categories;
			List<ViewModel> list = ((categories != null) ? (*categories(vm)) : null) ?? new List<ViewModel>();
			list.Insert(5, this._modOptions);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003FE2 File Offset: 0x000021E2
		private void OptionsVM_PropertyChanged([Nullable(2)] object sender, PropertyChangedEventArgs e)
		{
			this._modOptions.OnPropertyChanged(e.PropertyName);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003FF8 File Offset: 0x000021F8
		public override void OnFinalize()
		{
			bool flag = base.ViewModel != null;
			if (flag)
			{
				base.ViewModel.PropertyChanged -= this.OptionsVM_PropertyChanged;
			}
			base.OnFinalize();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004038 File Offset: 0x00002238
		[DataSourceMethod]
		public void ExecuteCloseOptions()
		{
			this.ModOptions.ExecuteCancelInternal(false, null);
			bool flag = base.ViewModel != null;
			if (flag)
			{
				try
				{
					OptionsVMMixin.OriginalExecuteCloseOptions(base.ViewModel);
				}
				catch
				{
				}
			}
			this.OnFinalize();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004094 File Offset: 0x00002294
		[DataSourceMethod]
		public void ExecuteDone()
		{
			this.ModOptions.ExecuteDoneInternal(false, delegate
			{
				bool flag = base.ViewModel != null;
				if (flag)
				{
					OptionsVMMixin.ExecuteDoneDelegate executeDoneMethod = OptionsVMMixin.ExecuteDoneMethod;
					if (executeDoneMethod != null)
					{
						executeDoneMethod(base.ViewModel);
					}
				}
			});
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000040B0 File Offset: 0x000022B0
		[DataSourceMethod]
		public void ExecuteCancel()
		{
			this.ModOptions.ExecuteCancelInternal(false, delegate
			{
				bool flag = base.ViewModel != null;
				if (flag)
				{
					OptionsVMMixin.ExecuteCancelDelegate executeCancelMethod = OptionsVMMixin.ExecuteCancelMethod;
					if (executeCancelMethod != null)
					{
						executeCancelMethod(base.ViewModel);
					}
				}
			});
		}

		// Token: 0x04000026 RID: 38
		[Nullable(2)]
		private static readonly OptionsVMMixin.ExecuteDoneDelegate ExecuteDoneMethod = AccessTools2.GetDelegate<OptionsVMMixin.ExecuteDoneDelegate>(typeof(OptionsVM), "ExecuteDone", null, null, true);

		// Token: 0x04000027 RID: 39
		[Nullable(2)]
		private static readonly OptionsVMMixin.ExecuteCancelDelegate ExecuteCancelMethod = AccessTools2.GetDelegate<OptionsVMMixin.ExecuteCancelDelegate>(typeof(OptionsVM), "ExecuteCancel", null, null, true);

		// Token: 0x04000028 RID: 40
		[Nullable(new byte[]
		{
			2,
			1,
			1,
			1
		})]
		private static readonly AccessTools.FieldRef<OptionsVM, List<ViewModel>> _categories = AccessTools2.FieldRefAccess<OptionsVM, List<ViewModel>>("_categories", true);

		// Token: 0x04000029 RID: 41
		private readonly ModOptionsVM _modOptions = new ModOptionsVM();

		// Token: 0x0400002A RID: 42
		private bool _modOptionsSelected;

		// Token: 0x0400002B RID: 43
		private int _descriptionWidth = 650;

		// Token: 0x02000086 RID: 134
		// (Invoke) Token: 0x060004E5 RID: 1253
		[NullableContext(0)]
		private delegate void ExecuteDoneDelegate(OptionsVM instance);

		// Token: 0x02000087 RID: 135
		// (Invoke) Token: 0x060004E9 RID: 1257
		[NullableContext(0)]
		private delegate void ExecuteCancelDelegate(OptionsVM instance);
	}
}
