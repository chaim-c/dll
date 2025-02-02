using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection
{
	// Token: 0x02000009 RID: 9
	public class PhotoModeVM : ViewModel
	{
		// Token: 0x0600009E RID: 158 RVA: 0x000040DC File Offset: 0x000022DC
		public PhotoModeVM(Scene missionScene, Func<bool> getVignetteOn, Func<bool> getHideAgentsOn)
		{
			this._missionScene = missionScene;
			this.Keys = new MBBindingList<InputKeyItemVM>();
			float currentValue = 0f;
			float currentValue2 = 0f;
			float currentValue3 = 0f;
			float currentValue4 = 0f;
			bool flag = false;
			float num = 65f;
			missionScene.SetPhotoModeFov(num);
			this._missionScene.GetPhotoModeFocus(ref currentValue, ref currentValue2, ref currentValue3, ref currentValue4, ref flag);
			this.FocusEndValueOption = new PhotoModeValueOptionVM(new TextObject("{=eeJcVeQG}Focus End", null), 0f, 1000f, currentValue3, new Action<float>(this.OnFocusEndValueChange));
			this.FocusStartValueOption = new PhotoModeValueOptionVM(new TextObject("{=j5pLIV91}Focus Start", null), 0f, 100f, currentValue2, new Action<float>(this.OnFocusStartValueChange));
			this.FocusValueOption = new PhotoModeValueOptionVM(new TextObject("{=photomodefocus}Focus", null), 0f, 100f, currentValue, new Action<float>(this.OnFocusValueChange));
			this.ExposureOption = new PhotoModeValueOptionVM(new TextObject("{=iPx4jep6}Exposure", null), -5f, 5f, currentValue4, new Action<float>(this.OnExposureValueChange));
			this.VerticalFovOption = new PhotoModeValueOptionVM(new TextObject("{=7XtICVeZ}Field of View", null), 2f, 140f, num, new Action<float>(this.OnVerticalFovValueChange));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004222 File Offset: 0x00002422
		private void OnFocusValueChange(float newFocusValue)
		{
			this._missionScene.SetPhotoModeFocus(this.FocusStartValueOption.CurrentValue, this.FocusEndValueOption.CurrentValue, newFocusValue, this.ExposureOption.CurrentValue);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004251 File Offset: 0x00002451
		private void OnFocusStartValueChange(float newFocusStartValue)
		{
			this._missionScene.SetPhotoModeFocus(newFocusStartValue, this.FocusEndValueOption.CurrentValue, this.FocusValueOption.CurrentValue, this.ExposureOption.CurrentValue);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004280 File Offset: 0x00002480
		private void OnFocusEndValueChange(float newFocusEndValue)
		{
			this._missionScene.SetPhotoModeFocus(this.FocusStartValueOption.CurrentValue, newFocusEndValue, this.FocusValueOption.CurrentValue, this.ExposureOption.CurrentValue);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000042AF File Offset: 0x000024AF
		private void OnExposureValueChange(float newExposureValue)
		{
			this._missionScene.SetPhotoModeFocus(this.FocusStartValueOption.CurrentValue, this.FocusEndValueOption.CurrentValue, this.FocusValueOption.CurrentValue, newExposureValue);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000042DE File Offset: 0x000024DE
		private void OnVerticalFovValueChange(float newVerticalFov)
		{
			this._missionScene.SetPhotoModeFov(newVerticalFov);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000042EC File Offset: 0x000024EC
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Keys.ApplyActionOnAllItems(delegate(InputKeyItemVM k)
			{
				k.RefreshValues();
			});
			this.FocusEndValueOption.RefreshValues();
			this.FocusStartValueOption.RefreshValues();
			this.FocusValueOption.RefreshValues();
			this.ExposureOption.RefreshValues();
			this.VerticalFovOption.RefreshValues();
			List<string> list = new List<string>();
			foreach (string variation in this._missionScene.GetAllColorGradeNames().Split(new string[]
			{
				"*/*"
			}, StringSplitOptions.RemoveEmptyEntries))
			{
				string item = GameTexts.FindText("str_photo_mode_color_grade", variation).ToString();
				list.Add(item);
			}
			if (list.Count == 0)
			{
				list.Add("Photo Mode Not Active");
			}
			this.ColorGradeSelector = new SelectorVM<SelectorItemVM>(list, this._missionScene.GetSceneColorGradeIndex(), new Action<SelectorVM<SelectorItemVM>>(this.OnColorGradeSelectionChanged));
			List<string> list2 = new List<string>();
			foreach (string variation2 in this._missionScene.GetAllFilterNames().Split(new string[]
			{
				"*/*"
			}, StringSplitOptions.RemoveEmptyEntries))
			{
				string item2 = GameTexts.FindText("str_photo_mode_overlay", variation2).ToString();
				list2.Add(item2);
			}
			if (list2.Count == 0)
			{
				list.Add("Photo Mode Not Active");
			}
			this.OverlaySelector = new SelectorVM<SelectorItemVM>(list2, this._missionScene.GetSceneFilterIndex(), new Action<SelectorVM<SelectorItemVM>>(this.OnOverlaySelectionChanged));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004474 File Offset: 0x00002674
		public void AddKey(GameKey key)
		{
			this.Keys.Add(InputKeyItemVM.CreateFromGameKey(key, false));
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004488 File Offset: 0x00002688
		public void AddHotkey(HotKey hotkey)
		{
			this.Keys.Add(InputKeyItemVM.CreateFromHotKey(hotkey, false));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000449C File Offset: 0x0000269C
		public void AddHotkeyWithForcedName(HotKey hotkey, TextObject forcedName)
		{
			this.Keys.Add(InputKeyItemVM.CreateFromHotKeyWithForcedName(hotkey, forcedName, false));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000044B1 File Offset: 0x000026B1
		public void AddCustomKey(string keyID, TextObject forcedName)
		{
			this.Keys.Add(InputKeyItemVM.CreateFromForcedID(keyID, forcedName, false));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000044C8 File Offset: 0x000026C8
		public override void OnFinalize()
		{
			base.OnFinalize();
			foreach (InputKeyItemVM inputKeyItemVM in this.Keys)
			{
				inputKeyItemVM.OnFinalize();
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004518 File Offset: 0x00002718
		private void OnColorGradeSelectionChanged(SelectorVM<SelectorItemVM> obj)
		{
			if (this._missionScene.GetSceneColorGradeIndex() != obj.SelectedIndex)
			{
				this._missionScene.SetSceneColorGradeIndex(obj.SelectedIndex);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004540 File Offset: 0x00002740
		private void OnOverlaySelectionChanged(SelectorVM<SelectorItemVM> obj)
		{
			if (this._missionScene.GetSceneFilterIndex() != obj.SelectedIndex)
			{
				int num = this._missionScene.SetSceneFilterIndex(obj.SelectedIndex);
				if (num >= 0)
				{
					this.ColorGradeSelector.SelectedIndex = num;
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004584 File Offset: 0x00002784
		public void Reset()
		{
			this.ColorGradeSelector.SelectedIndex = 0;
			this.OverlaySelector.SelectedIndex = 0;
			this.FocusValueOption.CurrentValue = 0f;
			this.FocusStartValueOption.CurrentValue = 0f;
			this.FocusEndValueOption.CurrentValue = 0f;
			this.ExposureOption.CurrentValue = 0f;
			this.VerticalFovOption.CurrentValue = 65f;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000045F9 File Offset: 0x000027F9
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004601 File Offset: 0x00002801
		[DataSourceProperty]
		public MBBindingList<InputKeyItemVM> Keys
		{
			get
			{
				return this._keys;
			}
			set
			{
				if (value != this._keys)
				{
					this._keys = value;
					base.OnPropertyChangedWithValue<MBBindingList<InputKeyItemVM>>(value, "Keys");
				}
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000461F File Offset: 0x0000281F
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004627 File Offset: 0x00002827
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> ColorGradeSelector
		{
			get
			{
				return this._colorGradeSelector;
			}
			set
			{
				if (value != this._colorGradeSelector)
				{
					this._colorGradeSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "ColorGradeSelector");
				}
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004645 File Offset: 0x00002845
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000464D File Offset: 0x0000284D
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> OverlaySelector
		{
			get
			{
				return this._overlaySelector;
			}
			set
			{
				if (value != this._overlaySelector)
				{
					this._overlaySelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "OverlaySelector");
				}
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000466B File Offset: 0x0000286B
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00004673 File Offset: 0x00002873
		[DataSourceProperty]
		public PhotoModeValueOptionVM FocusEndValueOption
		{
			get
			{
				return this._focusEndValueOption;
			}
			set
			{
				if (value != this._focusEndValueOption)
				{
					this._focusEndValueOption = value;
					base.OnPropertyChangedWithValue<PhotoModeValueOptionVM>(value, "FocusEndValueOption");
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00004691 File Offset: 0x00002891
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004699 File Offset: 0x00002899
		[DataSourceProperty]
		public PhotoModeValueOptionVM FocusStartValueOption
		{
			get
			{
				return this._focusStartValueOption;
			}
			set
			{
				if (value != this._focusStartValueOption)
				{
					this._focusStartValueOption = value;
					base.OnPropertyChangedWithValue<PhotoModeValueOptionVM>(value, "FocusStartValueOption");
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000046B7 File Offset: 0x000028B7
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000046BF File Offset: 0x000028BF
		[DataSourceProperty]
		public PhotoModeValueOptionVM FocusValueOption
		{
			get
			{
				return this._focusValueOption;
			}
			set
			{
				if (value != this._focusValueOption)
				{
					this._focusValueOption = value;
					base.OnPropertyChangedWithValue<PhotoModeValueOptionVM>(value, "FocusValueOption");
				}
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000046DD File Offset: 0x000028DD
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000046E5 File Offset: 0x000028E5
		[DataSourceProperty]
		public PhotoModeValueOptionVM ExposureOption
		{
			get
			{
				return this._exposureOption;
			}
			set
			{
				if (value != this._exposureOption)
				{
					this._exposureOption = value;
					base.OnPropertyChangedWithValue<PhotoModeValueOptionVM>(value, "ExposureOption");
				}
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004703 File Offset: 0x00002903
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000470B File Offset: 0x0000290B
		[DataSourceProperty]
		public PhotoModeValueOptionVM VerticalFovOption
		{
			get
			{
				return this._verticalFovOption;
			}
			set
			{
				if (value != this._verticalFovOption)
				{
					this._verticalFovOption = value;
					base.OnPropertyChangedWithValue<PhotoModeValueOptionVM>(value, "VerticalFovOption");
				}
			}
		}

		// Token: 0x04000043 RID: 67
		private readonly Scene _missionScene;

		// Token: 0x04000044 RID: 68
		private SelectorVM<SelectorItemVM> _colorGradeSelector;

		// Token: 0x04000045 RID: 69
		private SelectorVM<SelectorItemVM> _overlaySelector;

		// Token: 0x04000046 RID: 70
		private MBBindingList<InputKeyItemVM> _keys;

		// Token: 0x04000047 RID: 71
		private PhotoModeValueOptionVM _focusEndValueOption;

		// Token: 0x04000048 RID: 72
		private PhotoModeValueOptionVM _focusStartValueOption;

		// Token: 0x04000049 RID: 73
		private PhotoModeValueOptionVM _focusValueOption;

		// Token: 0x0400004A RID: 74
		private PhotoModeValueOptionVM _exposureOption;

		// Token: 0x0400004B RID: 75
		private PhotoModeValueOptionVM _verticalFovOption;
	}
}
