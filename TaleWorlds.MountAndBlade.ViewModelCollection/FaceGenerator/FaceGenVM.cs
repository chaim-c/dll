using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.FaceGenerator
{
	// Token: 0x0200006E RID: 110
	public class FaceGenVM : ViewModel
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00020F1C File Offset: 0x0001F11C
		private bool _isAgeAvailable
		{
			get
			{
				return this._openedFromMultiplayer || this._showDebugValues;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00020F2E File Offset: 0x0001F12E
		private bool _isWeightAvailable
		{
			get
			{
				return !this._openedFromMultiplayer || this._showDebugValues;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00020F40 File Offset: 0x0001F140
		private bool _isBuildAvailable
		{
			get
			{
				return !this._openedFromMultiplayer || this._showDebugValues;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00020F52 File Offset: 0x0001F152
		private bool _isRaceAvailable
		{
			get
			{
				return (FaceGen.GetRaceCount() > 1 && !this._openedFromMultiplayer) || this._showDebugValues;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00020F6C File Offset: 0x0001F16C
		public void SetFaceGenerationParams(FaceGenerationParams faceGenerationParams)
		{
			this._faceGenerationParams = faceGenerationParams;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00020F78 File Offset: 0x0001F178
		public FaceGenVM(BodyGenerator bodyGenerator, IFaceGeneratorHandler faceGeneratorScreen, Action<float> onHeightChanged, Action onAgeChanged, TextObject affirmitiveText, TextObject negativeText, int currentStageIndex, int totalStagesCount, int furthestIndex, Action<int> goToIndex, bool canChangeGender, bool openedFromMultiplayer, IFaceGeneratorCustomFilter filter)
		{
			this._bodyGenerator = bodyGenerator;
			this._faceGeneratorScreen = faceGeneratorScreen;
			this._showDebugValues = FaceGen.ShowDebugValues;
			this._affirmitiveText = affirmitiveText;
			this._negativeText = negativeText;
			this._openedFromMultiplayer = openedFromMultiplayer;
			this._filter = filter;
			this.CanChangeGender = (canChangeGender || this._showDebugValues);
			this._onHeightChanged = onHeightChanged;
			this._onAgeChanged = onAgeChanged;
			this._goToIndex = goToIndex;
			this.TotalStageCount = totalStagesCount;
			this.CurrentStageIndex = currentStageIndex;
			this.FurthestIndex = furthestIndex;
			this.CameraControlKeys = new MBBindingList<InputKeyItemVM>();
			this.BodyProperties = new MBBindingList<FaceGenPropertyVM>();
			this.FaceProperties = new MBBindingList<FaceGenPropertyVM>();
			this.EyesProperties = new MBBindingList<FaceGenPropertyVM>();
			this.NoseProperties = new MBBindingList<FaceGenPropertyVM>();
			this.MouthProperties = new MBBindingList<FaceGenPropertyVM>();
			this.HairProperties = new MBBindingList<FaceGenPropertyVM>();
			this.TaintProperties = new MBBindingList<FaceGenPropertyVM>();
			this._tabProperties = new Dictionary<FaceGenVM.FaceGenTabs, MBBindingList<FaceGenPropertyVM>>
			{
				{
					FaceGenVM.FaceGenTabs.Body,
					this.BodyProperties
				},
				{
					FaceGenVM.FaceGenTabs.Face,
					this.FaceProperties
				},
				{
					FaceGenVM.FaceGenTabs.Eyes,
					this.EyesProperties
				},
				{
					FaceGenVM.FaceGenTabs.Nose,
					this.NoseProperties
				},
				{
					FaceGenVM.FaceGenTabs.Mouth,
					this.MouthProperties
				},
				{
					FaceGenVM.FaceGenTabs.Hair,
					this.HairProperties
				},
				{
					FaceGenVM.FaceGenTabs.Taint,
					this.TaintProperties
				}
			};
			this.TaintTypes = new MBBindingList<FacegenListItemVM>();
			this.BeardTypes = new MBBindingList<FacegenListItemVM>();
			this.HairTypes = new MBBindingList<FacegenListItemVM>();
			this._tab = -1;
			this.IsDressed = false;
			this.genderBasedSelectedValues = new FaceGenVM.GenderBasedSelectedValue[2];
			this.genderBasedSelectedValues[0].Reset();
			this.genderBasedSelectedValues[1].Reset();
			this._undoCommands = new List<FaceGenVM.UndoRedoKey>(100);
			this._redoCommands = new List<FaceGenVM.UndoRedoKey>(100);
			this.IsUndoEnabled = (this._undoCommands.Count > 0);
			this.IsRedoEnabled = (this._redoCommands.Count > 0);
			this.CanChangeRace = this._isRaceAvailable;
			this.RefreshValues();
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000211D8 File Offset: 0x0001F3D8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.BodyHint = new HintViewModel(GameTexts.FindText("str_body", null), null);
			this.FaceHint = new HintViewModel(GameTexts.FindText("str_face", null), null);
			this.EyesHint = new HintViewModel(GameTexts.FindText("str_eyes", null), null);
			this.NoseHint = new HintViewModel(GameTexts.FindText("str_nose", null), null);
			this.HairHint = new HintViewModel(GameTexts.FindText("str_hair", null), null);
			this.TaintHint = new HintViewModel(GameTexts.FindText("str_face_gen_markings", null), null);
			this.MouthHint = new HintViewModel(GameTexts.FindText("str_mouth", null), null);
			this.RedoHint = new HintViewModel(GameTexts.FindText("str_redo", null), null);
			this.UndoHint = new HintViewModel(GameTexts.FindText("str_undo", null), null);
			this.RandomizeHint = new HintViewModel(GameTexts.FindText("str_randomize", null), null);
			this.RandomizeAllHint = new HintViewModel(GameTexts.FindText("str_randomize_all", null), null);
			this.ResetHint = new HintViewModel(GameTexts.FindText("str_reset", null), null);
			this.ResetAllHint = new HintViewModel(GameTexts.FindText("str_reset_all", null), null);
			this.ClothHint = new HintViewModel(GameTexts.FindText("str_face_gen_change_cloth", null), null);
			this.FlipHairLbl = new TextObject("{=74PKmRWJ}Flip Hair", null).ToString();
			this.SkinColorLbl = GameTexts.FindText("sf_facegen_skin_color", null).ToString();
			this.GenderLbl = GameTexts.FindText("sf_facegen_gender", null).ToString();
			this.RaceLbl = GameTexts.FindText("sf_facegen_race", null).ToString();
			this.Title = GameTexts.FindText("sf_facegen_title", null).ToString();
			this.DoneBtnLbl = this._affirmitiveText.ToString();
			this.CancelBtnLbl = this._negativeText.ToString();
			FacegenListItemVM selectedTaintType = this._selectedTaintType;
			if (selectedTaintType != null)
			{
				selectedTaintType.RefreshValues();
			}
			FacegenListItemVM selectedBeardType = this._selectedBeardType;
			if (selectedBeardType != null)
			{
				selectedBeardType.RefreshValues();
			}
			FacegenListItemVM selectedHairType = this._selectedHairType;
			if (selectedHairType != null)
			{
				selectedHairType.RefreshValues();
			}
			this._bodyProperties.ApplyActionOnAllItems(delegate(FaceGenPropertyVM x)
			{
				x.RefreshValues();
			});
			this._faceProperties.ApplyActionOnAllItems(delegate(FaceGenPropertyVM x)
			{
				x.RefreshValues();
			});
			this._eyesProperties.ApplyActionOnAllItems(delegate(FaceGenPropertyVM x)
			{
				x.RefreshValues();
			});
			this._noseProperties.ApplyActionOnAllItems(delegate(FaceGenPropertyVM x)
			{
				x.RefreshValues();
			});
			this._mouthProperties.ApplyActionOnAllItems(delegate(FaceGenPropertyVM x)
			{
				x.RefreshValues();
			});
			this._hairProperties.ApplyActionOnAllItems(delegate(FaceGenPropertyVM x)
			{
				x.RefreshValues();
			});
			this._taintProperties.ApplyActionOnAllItems(delegate(FaceGenPropertyVM x)
			{
				x.RefreshValues();
			});
			this._taintTypes.ApplyActionOnAllItems(delegate(FacegenListItemVM x)
			{
				x.RefreshValues();
			});
			this._beardTypes.ApplyActionOnAllItems(delegate(FacegenListItemVM x)
			{
				x.RefreshValues();
			});
			this._hairTypes.ApplyActionOnAllItems(delegate(FacegenListItemVM x)
			{
				x.RefreshValues();
			});
			FaceGenPropertyVM soundPreset = this._soundPreset;
			if (soundPreset != null)
			{
				soundPreset.RefreshValues();
			}
			FaceGenPropertyVM faceTypes = this._faceTypes;
			if (faceTypes != null)
			{
				faceTypes.RefreshValues();
			}
			FaceGenPropertyVM teethTypes = this._teethTypes;
			if (teethTypes != null)
			{
				teethTypes.RefreshValues();
			}
			FaceGenPropertyVM eyebrowTypes = this._eyebrowTypes;
			if (eyebrowTypes != null)
			{
				eyebrowTypes.RefreshValues();
			}
			SelectorVM<SelectorItemVM> skinColorSelector = this._skinColorSelector;
			if (skinColorSelector != null)
			{
				skinColorSelector.RefreshValues();
			}
			SelectorVM<SelectorItemVM> hairColorSelector = this._hairColorSelector;
			if (hairColorSelector != null)
			{
				hairColorSelector.RefreshValues();
			}
			SelectorVM<SelectorItemVM> tattooColorSelector = this._tattooColorSelector;
			if (tattooColorSelector != null)
			{
				tattooColorSelector.RefreshValues();
			}
			SelectorVM<SelectorItemVM> raceSelector = this._raceSelector;
			if (raceSelector == null)
			{
				return;
			}
			raceSelector.RefreshValues();
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002161C File Offset: 0x0001F81C
		private void FilterCategories()
		{
			FaceGeneratorStage[] availableStages = this._filter.GetAvailableStages();
			this.IsBodyEnabled = availableStages.Contains(FaceGeneratorStage.Body);
			this.IsFaceEnabled = availableStages.Contains(FaceGeneratorStage.Face);
			this.IsEyesEnabled = availableStages.Contains(FaceGeneratorStage.Eyes);
			this.IsNoseEnabled = availableStages.Contains(FaceGeneratorStage.Nose);
			this.IsMouthEnabled = availableStages.Contains(FaceGeneratorStage.Mouth);
			this.IsHairEnabled = availableStages.Contains(FaceGeneratorStage.Hair);
			this.IsTaintEnabled = availableStages.Contains(FaceGeneratorStage.Taint);
			this.Tab = (int)availableStages.FirstOrDefault<FaceGeneratorStage>();
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002169C File Offset: 0x0001F89C
		private void SetColorCodes()
		{
			this._skinColors = MBBodyProperties.GetSkinColorGradientPoints(this._selectedRace, this.SelectedGender, (int)this._bodyGenerator.Character.Age);
			this._hairColors = MBBodyProperties.GetHairColorGradientPoints(this._selectedRace, this.SelectedGender, (int)this._bodyGenerator.Character.Age);
			this._tattooColors = MBBodyProperties.GetTatooColorGradientPoints(this._selectedRace, this.SelectedGender, (int)this._bodyGenerator.Character.Age);
			this.SkinColorSelector = new SelectorVM<SelectorItemVM>(this._skinColors.Select(delegate(uint t)
			{
				t %= 4278190080U;
				return "#" + Convert.ToString((long)((ulong)t), 16).PadLeft(6, '0').ToUpper() + "FF";
			}).ToList<string>(), MathF.Round(this._faceGenerationParams.CurrentSkinColorOffset * (float)(this._skinColors.Count - 1)), new Action<SelectorVM<SelectorItemVM>>(this.OnSelectSkinColor));
			this.HairColorSelector = new SelectorVM<SelectorItemVM>(this._hairColors.Select(delegate(uint t)
			{
				t %= 4278190080U;
				return "#" + Convert.ToString((long)((ulong)t), 16).PadLeft(6, '0').ToUpper() + "FF";
			}).ToList<string>(), MathF.Round(this._faceGenerationParams.CurrentHairColorOffset * (float)(this._hairColors.Count - 1)), new Action<SelectorVM<SelectorItemVM>>(this.OnSelectHairColor));
			this.TattooColorSelector = new SelectorVM<SelectorItemVM>(this._tattooColors.Select(delegate(uint t)
			{
				t %= 4278190080U;
				return "#" + Convert.ToString((long)((ulong)t), 16).PadLeft(6, '0').ToUpper() + "FF";
			}).ToList<string>(), MathF.Round(this._faceGenerationParams.CurrentFaceTattooColorOffset1 * (float)(this._tattooColors.Count - 1)), new Action<SelectorVM<SelectorItemVM>>(this.OnSelectTattooColor));
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00021850 File Offset: 0x0001FA50
		private void OnSelectSkinColor(SelectorVM<SelectorItemVM> s)
		{
			this.AddCommand();
			this._faceGenerationParams.CurrentSkinColorOffset = (float)s.SelectedIndex / (float)(this._skinColors.Count - 1);
			this.UpdateFace();
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002187F File Offset: 0x0001FA7F
		private void OnSelectTattooColor(SelectorVM<SelectorItemVM> s)
		{
			this.AddCommand();
			this._faceGenerationParams.CurrentFaceTattooColorOffset1 = (float)s.SelectedIndex / (float)(this._tattooColors.Count - 1);
			this.UpdateFace();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000218AE File Offset: 0x0001FAAE
		private void OnSelectHairColor(SelectorVM<SelectorItemVM> s)
		{
			this.AddCommand();
			this._faceGenerationParams.CurrentHairColorOffset = (float)s.SelectedIndex / (float)(this._hairColors.Count - 1);
			this.UpdateFace();
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000218DD File Offset: 0x0001FADD
		private void OnSelectRace(SelectorVM<SelectorItemVM> s)
		{
			this.AddCommand();
			this._selectedRace = s.SelectedIndex;
			if (this._initialRace == -1)
			{
				this._initialRace = this._selectedRace;
			}
			this.UpdateRaceAndGenderBasedResources();
			this.Refresh(true);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00021913 File Offset: 0x0001FB13
		private void OnHeightChanged()
		{
			Action<float> onHeightChanged = this._onHeightChanged;
			if (onHeightChanged == null)
			{
				return;
			}
			FaceGenPropertyVM heightSlider = this._heightSlider;
			onHeightChanged((heightSlider != null) ? heightSlider.Value : 0f);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002193B File Offset: 0x0001FB3B
		private void OnAgeChanged()
		{
			Action onAgeChanged = this._onAgeChanged;
			if (onAgeChanged == null)
			{
				return;
			}
			onAgeChanged();
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00021950 File Offset: 0x0001FB50
		private void SetTabAvailabilities()
		{
			this._tabAvailabilities = new MBList<bool>
			{
				this.IsBodyEnabled,
				this.IsFaceEnabled,
				this.IsEyesEnabled,
				this.IsNoseEnabled,
				this.IsMouthEnabled,
				this.IsHairEnabled,
				this.IsTaintEnabled
			};
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x000219BC File Offset: 0x0001FBBC
		public void OnTabClicked(int index)
		{
			this.Tab = index;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x000219C8 File Offset: 0x0001FBC8
		public void SelectPreviousTab()
		{
			int num = (this.Tab == 0) ? 6 : (this.Tab - 1);
			while (!this._tabAvailabilities[num] && num != this.Tab)
			{
				num = ((num == 0) ? 6 : (num - 1));
			}
			this.Tab = num;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00021A14 File Offset: 0x0001FC14
		public void SelectNextTab()
		{
			int num = (this.Tab + 1) % 7;
			while (!this._tabAvailabilities[num] && num != this.Tab)
			{
				num = (num + 1) % 7;
			}
			this.Tab = num;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00021A54 File Offset: 0x0001FC54
		public void Refresh(bool clearProperties)
		{
			if (!this._characterRefreshEnabled)
			{
				return;
			}
			this._characterRefreshEnabled = false;
			base.OnPropertyChanged("FlipHairCb");
			this._selectedRace = this._faceGenerationParams.CurrentRace;
			this._selectedGender = this._faceGenerationParams.CurrentGender;
			this.SetColorCodes();
			int num = 0;
			MBBodyProperties.GetParamsMax(this._selectedRace, this.SelectedGender, (int)this._faceGenerationParams.CurrentAge, ref num, ref this.beardNum, ref this.faceTextureNum, ref this.mouthTextureNum, ref this.faceTattooNum, ref this._newSoundPresetSize, ref this.eyebrowTextureNum, ref this._scale);
			this.HairNum = num;
			MBBodyProperties.GetZeroProbabilities(this._selectedRace, this.SelectedGender, this._faceGenerationParams.CurrentAge, ref this._faceGenerationParams.TattooZeroProbability);
			if (clearProperties)
			{
				foreach (KeyValuePair<FaceGenVM.FaceGenTabs, MBBindingList<FaceGenPropertyVM>> keyValuePair in this._tabProperties)
				{
					keyValuePair.Value.Clear();
				}
			}
			int keyTimePoint = 0;
			int keyTimePoint2 = 0;
			int keyTimePoint3 = 0;
			int keyTimePoint4 = 0;
			if (clearProperties)
			{
				int faceGenInstancesLength = MBBodyProperties.GetFaceGenInstancesLength(this._faceGenerationParams.CurrentRace, this._faceGenerationParams.CurrentGender, (int)this._faceGenerationParams.CurrentAge);
				for (int i = 0; i < faceGenInstancesLength; i++)
				{
					DeformKeyData deformKeyData = MBBodyProperties.GetDeformKeyData(i, this._faceGenerationParams.CurrentRace, this._faceGenerationParams.CurrentGender, (int)this._faceGenerationParams.CurrentAge);
					TextObject textObject = new TextObject("{=bsiRNJtk}{NAME}:", null);
					textObject.SetTextVariable("NAME", GameTexts.FindText("str_facegen_skin", deformKeyData.Id));
					if (GameTexts.FindText("str_facegen_skin", deformKeyData.Id).ToString().Contains("exist"))
					{
						Debug.FailedAssert(deformKeyData.Id + " id name doesn't exist", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\FaceGenerator\\FaceGenVM.cs", "Refresh", 441);
					}
					if (deformKeyData.Id == "weight")
					{
						keyTimePoint2 = deformKeyData.KeyTimePoint;
					}
					else if (deformKeyData.Id == "build")
					{
						keyTimePoint4 = deformKeyData.KeyTimePoint;
					}
					else if (deformKeyData.Id == "height")
					{
						keyTimePoint = deformKeyData.KeyTimePoint;
					}
					else if (deformKeyData.Id == "age")
					{
						keyTimePoint3 = deformKeyData.KeyTimePoint;
					}
					else
					{
						FaceGenPropertyVM item = new FaceGenPropertyVM(i, 0.0, 1.0, textObject, deformKeyData.KeyTimePoint, deformKeyData.GroupId, (double)this._faceGenerationParams.KeyWeights[i], new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
						if (deformKeyData.GroupId > -1 && deformKeyData.GroupId < 7)
						{
							this._tabProperties[(FaceGenVM.FaceGenTabs)deformKeyData.GroupId].Add(item);
						}
					}
				}
			}
			if (this._filter != null)
			{
				this.FilterCategories();
			}
			else if (this._tab == -1)
			{
				this.IsBodyEnabled = true;
				this.IsFaceEnabled = true;
				this.IsEyesEnabled = true;
				this.IsNoseEnabled = true;
				this.IsMouthEnabled = true;
				this.IsHairEnabled = true;
				this.IsTaintEnabled = true;
				this.Tab = 0;
			}
			this.SetTabAvailabilities();
			if (clearProperties)
			{
				FaceGenPropertyVM item = new FaceGenPropertyVM(-19, 0.0, 1.0, new TextObject("{=G6hYIR5k}Voice Pitch:", null), -19, 0, (double)this._faceGenerationParams.VoicePitch, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
				this._tabProperties[FaceGenVM.FaceGenTabs.Body].Add(item);
				this._heightSlider = new FaceGenPropertyVM(-16, (double)(this._openedFromMultiplayer ? 0.25f : 0f), (double)(this._openedFromMultiplayer ? 0.75f : 1f), new TextObject("{=cLJdeUWz}Height:", null), keyTimePoint, 0, (double)((this._heightSlider == null) ? this._faceGenerationParams.HeightMultiplier : this._heightSlider.Value), new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
				this._tabProperties[FaceGenVM.FaceGenTabs.Body].Add(this._heightSlider);
				this.UpdateVoiceIndiciesFromCurrentParameters();
				if (this._isAgeAvailable)
				{
					double min = (double)(this._openedFromMultiplayer ? 25 : 3);
					item = new FaceGenPropertyVM(-11, min, 128.0, new TextObject("{=H1emUb6k}Age:", null), keyTimePoint3, 0, (double)this._faceGenerationParams.CurrentAge, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
					this._tabProperties[FaceGenVM.FaceGenTabs.Body].Add(item);
				}
				if (this._isWeightAvailable)
				{
					item = new FaceGenPropertyVM(-17, 0.0, 1.0, new TextObject("{=zBld61ck}Weight:", null), keyTimePoint2, 0, (double)this._faceGenerationParams.CurrentWeight, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
					this._tabProperties[FaceGenVM.FaceGenTabs.Body].Add(item);
				}
				if (this._isBuildAvailable)
				{
					item = new FaceGenPropertyVM(-18, 0.0, 1.0, new TextObject("{=EUAKPHek}Build:", null), keyTimePoint4, 0, (double)this._faceGenerationParams.CurrentBuild, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
					this._tabProperties[FaceGenVM.FaceGenTabs.Body].Add(item);
				}
				item = new FaceGenPropertyVM(-12, 0.0, 1.0, new TextObject("{=qXxpITdc}Eye Color:", null), -12, 2, (double)this._faceGenerationParams.CurrentEyeColorOffset, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
				this._tabProperties[FaceGenVM.FaceGenTabs.Eyes].Add(item);
				this.RaceSelector = new SelectorVM<SelectorItemVM>(FaceGen.GetRaceNames(), this._selectedRace, new Action<SelectorVM<SelectorItemVM>>(this.OnSelectRace));
			}
			this.UpdateRaceAndGenderBasedResources();
			if (!this._initialValuesSet)
			{
				this._initialSelectedTaintType = this._faceGenerationParams.CurrentFaceTattoo;
				this._initialSelectedBeardType = this._faceGenerationParams.CurrentBeard;
				this._initialSelectedHairType = this._faceGenerationParams.CurrentHair;
				this._initialSelectedHairColor = this._faceGenerationParams.CurrentHairColorOffset;
				this._initialSelectedSkinColor = this._faceGenerationParams.CurrentSkinColorOffset;
				this._initialSelectedTaintColor = this._faceGenerationParams.CurrentFaceTattooColorOffset1;
				this._initialRace = this._selectedRace;
				this._initialGender = this.SelectedGender;
				this._initialValuesSet = true;
			}
			this._characterRefreshEnabled = true;
			this.UpdateFace();
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00022170 File Offset: 0x00020370
		private void UpdateRaceAndGenderBasedResources()
		{
			int num = 0;
			MBBodyProperties.GetParamsMax(this._selectedRace, this.SelectedGender, (int)this._faceGenerationParams.CurrentAge, ref num, ref this.beardNum, ref this.faceTextureNum, ref this.mouthTextureNum, ref this.faceTattooNum, ref this._newSoundPresetSize, ref this.eyebrowTextureNum, ref this._scale);
			this.HairNum = num;
			int[] source = Enumerable.Range(0, num).ToArray<int>();
			int[] source2 = Enumerable.Range(0, this.beardNum).ToArray<int>();
			if (this._filter != null)
			{
				source = this._filter.GetHaircutIndices(this._bodyGenerator.Character);
				source2 = this._filter.GetFacialHairIndices(this._bodyGenerator.Character);
			}
			this.BeardTypes.Clear();
			for (int i = 0; i < this.beardNum; i++)
			{
				if (source2.Contains(i) || i == this._faceGenerationParams.CurrentBeard)
				{
					FacegenListItemVM item = new FacegenListItemVM("FaceGen\\Beard\\img" + i, i, new Action<FacegenListItemVM, bool>(this.SetSelectedBeardType));
					this.BeardTypes.Add(item);
				}
			}
			string text = (this._selectedGender == 1) ? "Female" : "Male";
			this.HairTypes.Clear();
			for (int j = 0; j < num; j++)
			{
				if (source.Contains(j) || j == this._faceGenerationParams.CurrentHair)
				{
					FacegenListItemVM item2 = new FacegenListItemVM(string.Concat(new object[]
					{
						"FaceGen\\Hair\\",
						text,
						"\\img",
						j
					}), j, new Action<FacegenListItemVM, bool>(this.SetSelectedHairType));
					this.HairTypes.Add(item2);
				}
			}
			this.TaintTypes.Clear();
			for (int k = 0; k < this.faceTattooNum; k++)
			{
				FacegenListItemVM item3 = new FacegenListItemVM(string.Concat(new object[]
				{
					"FaceGen\\Tattoo\\",
					text,
					"\\img",
					k
				}), k, new Action<FacegenListItemVM, bool>(this.SetSelectedTattooType));
				this.TaintTypes.Add(item3);
			}
			this.UpdateFace(-20, (float)this._selectedRace, true, true);
			this.UpdateFace(-1, (float)this._selectedGender, true, true);
			if (this.BeardTypes.Count > 0)
			{
				this.SetSelectedBeardType(this._faceGenerationParams.CurrentBeard, false);
			}
			this.SetSelectedHairType(this._faceGenerationParams.CurrentHair, false);
			if (this.TaintTypes.Count > 0)
			{
				this.SetSelectedTattooType(this.TaintTypes[this._faceGenerationParams.CurrentFaceTattoo], false);
			}
			this.UpdateVoiceIndiciesFromCurrentParameters();
			if (!this._openedFromMultiplayer)
			{
				this._faceGenerationParams.CurrentVoice = this.GetVoiceRealIndex(0);
			}
			this._faceGenerationParams.CurrentFaceTexture = MBMath.ClampInt(this._faceGenerationParams.CurrentFaceTexture, 0, this.faceTextureNum - 1);
			this.FaceTypes = new FaceGenPropertyVM(-3, 0.0, (double)(this.faceTextureNum - 1), new TextObject("{=DmaP2qaR}Skin Type", null), -3, 1, (double)this._faceGenerationParams.CurrentFaceTexture, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
			this._faceGenerationParams.CurrentMouthTexture = MBMath.ClampInt(this._faceGenerationParams.CurrentMouthTexture, 0, this.mouthTextureNum - 1);
			this.TeethTypes = new FaceGenPropertyVM(-14, 0.0, (double)(this.mouthTextureNum - 1), new TextObject("{=l2CNxPXG}Teeth Type", null), -14, 4, (double)this._faceGenerationParams.CurrentMouthTexture, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
			this._faceGenerationParams.CurrentEyebrow = MBMath.ClampInt(this._faceGenerationParams.CurrentEyebrow, 0, this.eyebrowTextureNum - 1);
			this.EyebrowTypes = new FaceGenPropertyVM(-15, 0.0, (double)(this.eyebrowTextureNum - 1), new TextObject("{=bIcFZT6L}Eyebrow Type", null), -15, 4, (double)this._faceGenerationParams.CurrentEyebrow, new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x000225B8 File Offset: 0x000207B8
		private void UpdateVoiceIndiciesFromCurrentParameters()
		{
			this._isVoiceTypeUsableForOnlyNpc = MBBodyProperties.GetVoiceTypeUsableForPlayerData(this._faceGenerationParams.CurrentRace, this.SelectedGender, (float)((int)this._faceGenerationParams.CurrentAge), this._newSoundPresetSize);
			int num = 0;
			for (int i = 0; i < this._isVoiceTypeUsableForOnlyNpc.Count; i++)
			{
				if (!this._isVoiceTypeUsableForOnlyNpc[i])
				{
					num++;
				}
			}
			this.SoundPreset = new FaceGenPropertyVM(-9, 0.0, (double)(num - 1), new TextObject("{=macpKFaG}Voice", null), -9, 0, (double)this.GetVoiceUIIndex(), new Action<int, float, bool, bool>(this.UpdateFace), new Action(this.AddCommand), new Action(this.ResetSliderPrevValues), true, false);
			Debug.Print("Called GetVoiceTypeUsableForPlayerData", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00022689 File Offset: 0x00020889
		private void UpdateFace()
		{
			if (this._characterRefreshEnabled)
			{
				this._bodyGenerator.RefreshFace(this._faceGenerationParams, this.IsDressed);
				this._faceGeneratorScreen.RefreshCharacterEntity();
			}
			this.SaveGenderBasedSelectedValues();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000226BC File Offset: 0x000208BC
		private void UpdateFace(int keyNo, float value, bool calledFromInit, bool isNeedRefresh = true)
		{
			if (this._enforceConstraints)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (keyNo > -1)
			{
				this._faceGenerationParams.KeyWeights[keyNo] = value;
				this._enforceConstraints = MBBodyProperties.EnforceConstraints(ref this._faceGenerationParams);
				flag3 = (this._enforceConstraints && !calledFromInit);
			}
			else
			{
				switch (keyNo)
				{
				case -20:
					this.RestoreRaceGenderBasedSelectedValues();
					this._faceGenerationParams.SetRaceGenderAndAdjustParams((int)value, this.SelectedGender, (int)this._faceGenerationParams.CurrentAge);
					goto IL_2CB;
				case -19:
					this._faceGenerationParams.VoicePitch = value;
					goto IL_2CB;
				case -18:
					this._faceGenerationParams.CurrentBuild = value;
					this._enforceConstraints = MBBodyProperties.EnforceConstraints(ref this._faceGenerationParams);
					flag3 = (this._enforceConstraints && !calledFromInit);
					goto IL_2CB;
				case -17:
					this._faceGenerationParams.CurrentWeight = value;
					this._enforceConstraints = MBBodyProperties.EnforceConstraints(ref this._faceGenerationParams);
					flag3 = (this._enforceConstraints && !calledFromInit);
					goto IL_2CB;
				case -16:
					this._faceGenerationParams.HeightMultiplier = (this._openedFromMultiplayer ? MathF.Clamp(value, 0.25f, 0.75f) : MathF.Clamp(value, 0f, 1f));
					this._enforceConstraints = MBBodyProperties.EnforceConstraints(ref this._faceGenerationParams);
					flag3 = (this._enforceConstraints && !calledFromInit);
					flag2 = true;
					goto IL_2CB;
				case -15:
					this._faceGenerationParams.CurrentEyebrow = (int)value;
					goto IL_2CB;
				case -14:
					this._faceGenerationParams.CurrentMouthTexture = (int)value;
					goto IL_2CB;
				case -12:
					this._faceGenerationParams.CurrentEyeColorOffset = value;
					goto IL_2CB;
				case -11:
				{
					this._faceGenerationParams.CurrentAge = value;
					this._enforceConstraints = MBBodyProperties.EnforceConstraints(ref this._faceGenerationParams);
					flag3 = (this._enforceConstraints && !calledFromInit);
					flag = true;
					flag2 = true;
					BodyMeshMaturityType maturityTypeWithAge = FaceGen.GetMaturityTypeWithAge(this._faceGenerationParams.CurrentAge);
					if (this._latestMaturityType != maturityTypeWithAge)
					{
						this.UpdateVoiceIndiciesFromCurrentParameters();
						this._latestMaturityType = maturityTypeWithAge;
						goto IL_2CB;
					}
					goto IL_2CB;
				}
				case -10:
					this._faceGenerationParams.CurrentFaceTattoo = (int)value;
					goto IL_2CB;
				case -9:
					this._faceGenerationParams.CurrentVoice = this.GetVoiceRealIndex((int)value);
					goto IL_2CB;
				case -7:
					this._faceGenerationParams.CurrentBeard = (int)value;
					goto IL_2CB;
				case -6:
					this._faceGenerationParams.CurrentHair = (int)value;
					goto IL_2CB;
				case -3:
					this._faceGenerationParams.CurrentFaceTexture = (int)value;
					goto IL_2CB;
				case -1:
					this.RestoreRaceGenderBasedSelectedValues();
					this._faceGenerationParams.SetRaceGenderAndAdjustParams(this._faceGenerationParams.CurrentRace, (int)value, (int)this._faceGenerationParams.CurrentAge);
					goto IL_2CB;
				}
				MBDebug.ShowWarning("Unknown preset!");
			}
			IL_2CB:
			if (flag3)
			{
				this.UpdateFacegen();
			}
			if (isNeedRefresh)
			{
				this.UpdateFace();
			}
			else
			{
				this.SaveGenderBasedSelectedValues();
			}
			if (!calledFromInit && !this._isRandomizing && keyNo < 0)
			{
				if (keyNo != -14)
				{
					if (keyNo == -9)
					{
						this._faceGeneratorScreen.MakeVoice(this._faceGenerationParams.CurrentVoice, this._faceGenerationParams.VoicePitch);
					}
				}
				else
				{
					this._faceGeneratorScreen.SetFacialAnimation("facegen_teeth", false);
				}
			}
			this._enforceConstraints = false;
			if (flag)
			{
				this.OnAgeChanged();
			}
			if (flag2)
			{
				this.OnHeightChanged();
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00022A1C File Offset: 0x00020C1C
		private void RestoreRaceGenderBasedSelectedValues()
		{
			if (this.genderBasedSelectedValues[this.SelectedGender].FaceTexture > -1)
			{
				this._faceGenerationParams.CurrentFaceTexture = this.genderBasedSelectedValues[this.SelectedGender].FaceTexture;
			}
			if (this.genderBasedSelectedValues[this.SelectedGender].Hair > -1)
			{
				this._faceGenerationParams.CurrentHair = this.genderBasedSelectedValues[this.SelectedGender].Hair;
			}
			if (this.genderBasedSelectedValues[this.SelectedGender].Beard > -1)
			{
				this._faceGenerationParams.CurrentBeard = this.genderBasedSelectedValues[this.SelectedGender].Beard;
			}
			if (this.genderBasedSelectedValues[this.SelectedGender].Tattoo > -1)
			{
				this._faceGenerationParams.CurrentFaceTattoo = this.genderBasedSelectedValues[this.SelectedGender].Tattoo;
			}
			if (this.genderBasedSelectedValues[this.SelectedGender].SoundPreset > -1)
			{
				this._faceGenerationParams.CurrentVoice = this.genderBasedSelectedValues[this.SelectedGender].SoundPreset;
			}
			if (this.genderBasedSelectedValues[this.SelectedGender].MouthTexture > -1)
			{
				this._faceGenerationParams.CurrentMouthTexture = this.genderBasedSelectedValues[this.SelectedGender].MouthTexture;
			}
			if (this.genderBasedSelectedValues[this.SelectedGender].EyebrowTexture > -1)
			{
				this._faceGenerationParams.CurrentEyebrow = this.genderBasedSelectedValues[this.SelectedGender].EyebrowTexture;
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00022BC0 File Offset: 0x00020DC0
		private void SaveGenderBasedSelectedValues()
		{
			this.genderBasedSelectedValues[this.SelectedGender].FaceTexture = this._faceGenerationParams.CurrentFaceTexture;
			this.genderBasedSelectedValues[this.SelectedGender].Hair = this._faceGenerationParams.CurrentHair;
			this.genderBasedSelectedValues[this.SelectedGender].Beard = this._faceGenerationParams.CurrentBeard;
			this.genderBasedSelectedValues[this.SelectedGender].Tattoo = this._faceGenerationParams.CurrentFaceTattoo;
			this.genderBasedSelectedValues[this.SelectedGender].SoundPreset = this._faceGenerationParams.CurrentVoice;
			this.genderBasedSelectedValues[this.SelectedGender].MouthTexture = this._faceGenerationParams.CurrentMouthTexture;
			this.genderBasedSelectedValues[this.SelectedGender].EyebrowTexture = this._faceGenerationParams.CurrentEyebrow;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00022CB4 File Offset: 0x00020EB4
		private int GetVoiceUIIndex()
		{
			int num = 0;
			for (int i = 0; i < this._faceGenerationParams.CurrentVoice; i++)
			{
				if (!this._isVoiceTypeUsableForOnlyNpc[i])
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00022CEC File Offset: 0x00020EEC
		private int GetVoiceRealIndex(int UIValue)
		{
			int num = 0;
			for (int i = 0; i < this._newSoundPresetSize; i++)
			{
				if (!this._isVoiceTypeUsableForOnlyNpc[i])
				{
					if (num == UIValue)
					{
						return i;
					}
					num++;
				}
			}
			Debug.FailedAssert("Cannot calculate voice index", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\FaceGenerator\\FaceGenVM.cs", "GetVoiceRealIndex", 927);
			return -1;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00022D3E File Offset: 0x00020F3E
		public void ExecuteHearCurrentVoiceSample()
		{
			this._faceGeneratorScreen.MakeVoice(this._faceGenerationParams.CurrentVoice, this._faceGenerationParams.VoicePitch);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00022D64 File Offset: 0x00020F64
		public void ExecuteReset()
		{
			string titleText = GameTexts.FindText("str_reset", null).ToString();
			string text = new TextObject("{=hiKTvBgF}Are you sure want to reset changes done in this tab? Your changes will be lost.", null).ToString();
			InformationManager.ShowInquiry(new InquiryData(titleText, text, true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(this.Reset), null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00022DDC File Offset: 0x00020FDC
		private void Reset()
		{
			if (this.Tab <= -1 || this.Tab >= 7)
			{
				Debug.FailedAssert("Calling Reset on invalid tab!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\FaceGenerator\\FaceGenVM.cs", "Reset", 948);
				this.Tab = this._tabAvailabilities.IndexOf(true);
			}
			this.AddCommand();
			this._characterRefreshEnabled = false;
			bool flag = this._initialRace != this.RaceSelector.SelectedIndex;
			switch (this.Tab)
			{
			case 0:
				this.SelectedGender = this._initialGender;
				this.RaceSelector.SelectedIndex = this._initialRace;
				this.SoundPreset.Reset();
				this.SkinColorSelector.SelectedIndex = MathF.Round(this._initialSelectedSkinColor * (float)(this._skinColors.Count - 1));
				break;
			case 1:
				this.FaceTypes.Reset();
				break;
			case 2:
				this.EyebrowTypes.Reset();
				break;
			case 4:
				this.TeethTypes.Reset();
				break;
			case 5:
			{
				FacegenListItemVM item = (this._selectedGender == 1) ? this.BeardTypes.FirstOrDefault<FacegenListItemVM>() : this.BeardTypes.FirstOrDefault((FacegenListItemVM b) => b.Index == this._initialSelectedBeardType);
				this.SetSelectedBeardType(item, false);
				if (this._initialSelectedHairType > this.HairTypes.Count - 1)
				{
					this.SetSelectedHairType(this.HairTypes[this.HairTypes.Count - 1], false);
				}
				else
				{
					this.SetSelectedHairType(this.HairTypes[this._initialSelectedHairType], false);
				}
				this.HairColorSelector.SelectedIndex = MathF.Round(this._initialSelectedHairColor * (float)(this._hairColors.Count - 1));
				break;
			}
			case 6:
				if (this._initialSelectedTaintType > this.TaintTypes.Count - 1)
				{
					this.SetSelectedTattooType(this.TaintTypes[this.TaintTypes.Count - 1], false);
				}
				else
				{
					this.SetSelectedTattooType(this.TaintTypes[this._initialSelectedTaintType], false);
				}
				this.TattooColorSelector.SelectedIndex = MathF.Round(this._initialSelectedTaintColor * (float)(this._tattooColors.Count - 1));
				break;
			}
			foreach (FaceGenPropertyVM faceGenPropertyVM in this._tabProperties[(FaceGenVM.FaceGenTabs)this.Tab])
			{
				if (faceGenPropertyVM.TabID == this.Tab)
				{
					faceGenPropertyVM.Reset();
				}
			}
			this._characterRefreshEnabled = true;
			if (flag)
			{
				this.Refresh(true);
			}
			this.UpdateFace();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0002308C File Offset: 0x0002128C
		private void ResetAll()
		{
			this.SelectedGender = this._initialGender;
			this._raceSelector.SelectedIndex = this._initialRace;
			foreach (KeyValuePair<FaceGenVM.FaceGenTabs, MBBindingList<FaceGenPropertyVM>> keyValuePair in this._tabProperties)
			{
				foreach (FaceGenPropertyVM faceGenPropertyVM in keyValuePair.Value)
				{
					faceGenPropertyVM.Reset();
				}
			}
			this.FaceTypes.Reset();
			this.SoundPreset.Reset();
			this.TeethTypes.Reset();
			this.EyebrowTypes.Reset();
			this._faceGenerationParams = this._bodyGenerator.InitBodyGenerator(this.IsDressed);
			this._undoCommands.Clear();
			this._redoCommands.Clear();
			this.IsUndoEnabled = false;
			this.IsRedoEnabled = false;
			this._characterRefreshEnabled = true;
			this.Refresh(FaceGen.UpdateDeformKeys);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x000231A8 File Offset: 0x000213A8
		public void ExecuteResetAll()
		{
			string titleText = GameTexts.FindText("str_reset_all", null).ToString();
			string text = new TextObject("{=1hnq3Kb1}Are you sure want to reset all properties? Your changes will be lost.", null).ToString();
			InformationManager.ShowInquiry(new InquiryData(titleText, text, true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), new Action(this.ResetAll), null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00023220 File Offset: 0x00021420
		public void ExecuteRandomize()
		{
			if (this.Tab <= -1 || this.Tab >= 7)
			{
				Debug.FailedAssert("Calling ExecuteRandomize on invalid tab!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\FaceGenerator\\FaceGenVM.cs", "ExecuteRandomize", 1064);
				this.Tab = this._tabAvailabilities.IndexOf(true);
			}
			this.AddCommand();
			this._characterRefreshEnabled = false;
			this._isRandomizing = true;
			foreach (FaceGenPropertyVM faceGenPropertyVM in this._tabProperties[(FaceGenVM.FaceGenTabs)this.Tab])
			{
				faceGenPropertyVM.Randomize();
			}
			switch (this.Tab)
			{
			case 0:
				this.SkinColorSelector.SelectedIndex = MBRandom.RandomInt(this._skinColors.Count);
				break;
			case 1:
				this.FaceTypes.Value = (float)MBRandom.RandomInt((int)this.FaceTypes.Max + 1);
				break;
			case 2:
				this.EyebrowTypes.Value = (float)MBRandom.RandomInt((int)this.EyebrowTypes.Max + 1);
				break;
			case 4:
				this.TeethTypes.Value = (float)MBRandom.RandomInt((int)this.TeethTypes.Max + 1);
				break;
			case 5:
				this.SetSelectedBeardType(this.BeardTypes[MBRandom.RandomInt(this.BeardTypes.Count)], false);
				this.SetSelectedHairType(this.HairTypes[MBRandom.RandomInt(this.HairTypes.Count)], false);
				this.HairColorSelector.SelectedIndex = MBRandom.RandomInt(this._hairColors.Count);
				break;
			case 6:
				this.SetSelectedTattooType(this.TaintTypes[MBRandom.RandomInt(this.TaintTypes.Count)], false);
				this.TattooColorSelector.SelectedIndex = MBRandom.RandomInt(this._tattooColors.Count);
				break;
			}
			this._characterRefreshEnabled = true;
			this._isRandomizing = false;
			this.UpdateFace();
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00023430 File Offset: 0x00021630
		public void ExecuteRandomizeAll()
		{
			this.AddCommand();
			this._characterRefreshEnabled = false;
			this._isRandomizing = true;
			foreach (KeyValuePair<FaceGenVM.FaceGenTabs, MBBindingList<FaceGenPropertyVM>> keyValuePair in this._tabProperties)
			{
				foreach (FaceGenPropertyVM faceGenPropertyVM in keyValuePair.Value)
				{
					faceGenPropertyVM.Randomize();
				}
			}
			this.FaceTypes.Value = (float)MBRandom.RandomInt((int)this.FaceTypes.Max + 1);
			if (this.BeardTypes.Count > 0)
			{
				this.SetSelectedBeardType(this.BeardTypes[MBRandom.RandomInt(this.BeardTypes.Count)], false);
			}
			if (this.HairTypes.Count > 0)
			{
				this.SetSelectedHairType(this.HairTypes[MBRandom.RandomInt(this.HairTypes.Count)], false);
			}
			this.EyebrowTypes.Value = (float)MBRandom.RandomInt((int)this.EyebrowTypes.Max + 1);
			this.TeethTypes.Value = (float)MBRandom.RandomInt((int)this.TeethTypes.Max + 1);
			if (this.TaintTypes.Count > 0)
			{
				if (MBRandom.RandomFloat < this._faceGenerationParams.TattooZeroProbability)
				{
					this.SetSelectedTattooType(this.TaintTypes[0], false);
				}
				else
				{
					this.SetSelectedTattooType(this.TaintTypes[MBRandom.RandomInt(1, this.TaintTypes.Count)], false);
				}
			}
			this.TattooColorSelector.SelectedIndex = MBRandom.RandomInt(this._tattooColors.Count);
			this.HairColorSelector.SelectedIndex = MBRandom.RandomInt(this._hairColors.Count);
			this.SkinColorSelector.SelectedIndex = MBRandom.RandomInt(this._skinColors.Count);
			this._characterRefreshEnabled = true;
			this.UpdateFace();
			this._isRandomizing = false;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00023644 File Offset: 0x00021844
		public void ExecuteCancel()
		{
			this._faceGeneratorScreen.Cancel();
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00023651 File Offset: 0x00021851
		public void ExecuteDone()
		{
			this._faceGeneratorScreen.Done();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00023660 File Offset: 0x00021860
		public void ExecuteRedo()
		{
			if (this._redoCommands.Count > 0)
			{
				int index = this._redoCommands.Count - 1;
				BodyProperties bodyProperties = this._redoCommands[index].BodyProperties;
				int gender = this._redoCommands[index].Gender;
				int race = this._redoCommands[index].Race;
				this._redoCommands.RemoveAt(index);
				this._undoCommands.Add(new FaceGenVM.UndoRedoKey(this._faceGenerationParams.CurrentGender, this._faceGenerationParams.CurrentRace, this._bodyGenerator.CurrentBodyProperties));
				this.IsRedoEnabled = (this._redoCommands.Count > 0);
				this.IsUndoEnabled = (this._undoCommands.Count > 0);
				this._characterRefreshEnabled = false;
				this.SetBodyProperties(bodyProperties, false, race, gender, false);
				this._characterRefreshEnabled = true;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00023740 File Offset: 0x00021940
		public void ExecuteUndo()
		{
			if (this._undoCommands.Count > 0)
			{
				int index = this._undoCommands.Count - 1;
				BodyProperties bodyProperties = this._undoCommands[index].BodyProperties;
				int gender = this._undoCommands[index].Gender;
				int race = this._undoCommands[index].Race;
				this._undoCommands.RemoveAt(index);
				this._redoCommands.Add(new FaceGenVM.UndoRedoKey(this._faceGenerationParams.CurrentGender, this._faceGenerationParams.CurrentRace, this._bodyGenerator.CurrentBodyProperties));
				this.IsRedoEnabled = (this._redoCommands.Count > 0);
				this.IsUndoEnabled = (this._undoCommands.Count > 0);
				this._characterRefreshEnabled = false;
				this.SetBodyProperties(bodyProperties, false, race, gender, false);
				this._characterRefreshEnabled = true;
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00023820 File Offset: 0x00021A20
		public void ExecuteChangeClothing()
		{
			if (this.IsDressed)
			{
				this._faceGeneratorScreen.UndressCharacterEntity();
				this.IsDressed = false;
				return;
			}
			this._faceGeneratorScreen.DressCharacterEntity();
			this.IsDressed = true;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00023850 File Offset: 0x00021A50
		public void AddCommand()
		{
			if (this._characterRefreshEnabled)
			{
				if (this._undoCommands.Count + 1 == this._undoCommands.Capacity)
				{
					this._undoCommands.RemoveAt(0);
				}
				this._undoCommands.Add(new FaceGenVM.UndoRedoKey(this._faceGenerationParams.CurrentGender, this._faceGenerationParams.CurrentRace, this._bodyGenerator.CurrentBodyProperties));
				this._redoCommands.Clear();
				this.IsRedoEnabled = (this._redoCommands.Count > 0);
				this.IsUndoEnabled = (this._undoCommands.Count > 0);
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x000238F2 File Offset: 0x00021AF2
		private void UpdateTitle()
		{
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000238F4 File Offset: 0x00021AF4
		private void ExecuteGoToIndex(int index)
		{
			this._goToIndex(index);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00023904 File Offset: 0x00021B04
		public void SetBodyProperties(BodyProperties bodyProperties, bool ignoreDebugValues, int race = 0, int gender = -1, bool recordChange = false)
		{
			this._characterRefreshEnabled = false;
			bool flag = false;
			if (gender == -1)
			{
				this._faceGenerationParams.CurrentGender = this._selectedGender;
			}
			else
			{
				this._faceGenerationParams.CurrentGender = gender;
			}
			if (this._isRaceAvailable)
			{
				flag = (this._faceGenerationParams.CurrentRace != race);
				this._faceGenerationParams.CurrentRace = race;
			}
			if (this._openedFromMultiplayer)
			{
				bodyProperties = bodyProperties.ClampForMultiplayer();
			}
			float age = this._isAgeAvailable ? bodyProperties.Age : this._bodyGenerator.CurrentBodyProperties.Age;
			float weight = this._isWeightAvailable ? bodyProperties.Weight : this._bodyGenerator.CurrentBodyProperties.Weight;
			float build = this._isWeightAvailable ? bodyProperties.Build : this._bodyGenerator.CurrentBodyProperties.Build;
			bodyProperties = new BodyProperties(new DynamicBodyProperties(age, weight, build), bodyProperties.StaticProperties);
			this._bodyGenerator.CurrentBodyProperties = bodyProperties;
			MBBodyProperties.GetParamsFromKey(ref this._faceGenerationParams, bodyProperties, this.IsDressed && this._bodyGenerator.Character.Equipment.EarsAreHidden, this.IsDressed && this._bodyGenerator.Character.Equipment.MouthIsHidden);
			if (flag)
			{
				this._characterRefreshEnabled = true;
				this.Refresh(true);
			}
			else
			{
				this.UpdateFacegen();
				this._characterRefreshEnabled = true;
				this.UpdateFace();
			}
			if (recordChange)
			{
				this._characterRefreshEnabled = true;
				this.AddCommand();
			}
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00023A84 File Offset: 0x00021C84
		private void ResetSliderPrevValues()
		{
			foreach (MBBindingList<FaceGenPropertyVM> mbbindingList in this._tabProperties.Values)
			{
				foreach (FaceGenPropertyVM faceGenPropertyVM in mbbindingList)
				{
					faceGenPropertyVM.PrevValue = -1.0;
				}
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00023B10 File Offset: 0x00021D10
		public void UpdateFacegen()
		{
			foreach (MBBindingList<FaceGenPropertyVM> mbbindingList in this._tabProperties.Values)
			{
				foreach (FaceGenPropertyVM faceGenPropertyVM in mbbindingList)
				{
					if (faceGenPropertyVM.KeyNo < 0)
					{
						switch (faceGenPropertyVM.KeyNo)
						{
						case -19:
							faceGenPropertyVM.Value = this._faceGenerationParams.VoicePitch;
							break;
						case -18:
							faceGenPropertyVM.Value = this._faceGenerationParams.CurrentBuild;
							break;
						case -17:
							faceGenPropertyVM.Value = this._faceGenerationParams.CurrentWeight;
							break;
						case -16:
							faceGenPropertyVM.Value = (this._openedFromMultiplayer ? MathF.Clamp(this._faceGenerationParams.HeightMultiplier, 0.25f, 0.75f) : MathF.Clamp(this._faceGenerationParams.HeightMultiplier, 0f, 1f));
							break;
						case -12:
							faceGenPropertyVM.Value = this._faceGenerationParams.CurrentEyeColorOffset;
							break;
						case -11:
							faceGenPropertyVM.Value = this._faceGenerationParams.CurrentAge;
							break;
						}
					}
					else
					{
						faceGenPropertyVM.Value = this._faceGenerationParams.KeyWeights[faceGenPropertyVM.KeyNo];
					}
					faceGenPropertyVM.PrevValue = -1.0;
				}
			}
			this.SelectedGender = this._faceGenerationParams.CurrentGender;
			this.SoundPreset.Value = (float)this.GetVoiceUIIndex();
			this.FaceTypes.Value = (float)this._faceGenerationParams.CurrentFaceTexture;
			this.EyebrowTypes.Value = (float)this._faceGenerationParams.CurrentEyebrow;
			this.TeethTypes.Value = (float)this._faceGenerationParams.CurrentMouthTexture;
			if (this.TaintTypes.Count > this._faceGenerationParams.CurrentFaceTattoo)
			{
				this.SetSelectedTattooType(this.TaintTypes[this._faceGenerationParams.CurrentFaceTattoo], false);
			}
			if (this.BeardTypes.Count > this._faceGenerationParams.CurrentBeard)
			{
				this.SetSelectedBeardType(this.BeardTypes[this._faceGenerationParams.CurrentBeard], false);
			}
			if (this.HairTypes.Count > this._faceGenerationParams.CurrentHair)
			{
				this.SetSelectedHairType(this.HairTypes[this._faceGenerationParams.CurrentHair], false);
			}
			this.SkinColorSelector.SelectedIndex = MathF.Round(this._faceGenerationParams.CurrentSkinColorOffset * (float)(this._skinColors.Count - 1));
			this.HairColorSelector.SelectedIndex = MathF.Round(this._faceGenerationParams.CurrentHairColorOffset * (float)(this._hairColors.Count - 1));
			this.TattooColorSelector.SelectedIndex = MathF.Round(this._faceGenerationParams.CurrentFaceTattooColorOffset1 * (float)(this._tattooColors.Count - 1));
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00023E50 File Offset: 0x00022050
		private void SetSelectedHairType(FacegenListItemVM item, bool addCommand)
		{
			if (this._selectedHairType != null)
			{
				this._selectedHairType.IsSelected = false;
			}
			this._selectedHairType = item;
			this._selectedHairType.IsSelected = true;
			this._faceGenerationParams.CurrentHair = item.Index;
			if (!addCommand)
			{
				return;
			}
			this.AddCommand();
			this.UpdateFace(-6, (float)item.Index, false, true);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00023EB0 File Offset: 0x000220B0
		private void SetSelectedHairType(int index, bool addCommand)
		{
			foreach (FacegenListItemVM facegenListItemVM in this.HairTypes)
			{
				if (facegenListItemVM.Index == index)
				{
					this.SetSelectedHairType(facegenListItemVM, addCommand);
					break;
				}
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00023F0C File Offset: 0x0002210C
		private void SetSelectedTattooType(FacegenListItemVM item, bool addCommand)
		{
			if (this._selectedTaintType != null)
			{
				this._selectedTaintType.IsSelected = false;
			}
			this._selectedTaintType = item;
			this._selectedTaintType.IsSelected = true;
			this._faceGenerationParams.CurrentFaceTattoo = item.Index;
			if (!addCommand)
			{
				return;
			}
			this.AddCommand();
			this.UpdateFace(-10, (float)item.Index, false, true);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00023F6C File Offset: 0x0002216C
		private void SetSelectedBeardType(FacegenListItemVM item, bool addCommand)
		{
			if (this._selectedBeardType != null)
			{
				this._selectedBeardType.IsSelected = false;
			}
			this._selectedBeardType = item;
			this._selectedBeardType.IsSelected = true;
			this._faceGenerationParams.CurrentBeard = item.Index;
			if (!addCommand)
			{
				return;
			}
			this.AddCommand();
			this.UpdateFace(-7, (float)item.Index, false, true);
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00023FCC File Offset: 0x000221CC
		private void SetSelectedBeardType(int index, bool addCommand)
		{
			foreach (FacegenListItemVM facegenListItemVM in this.BeardTypes)
			{
				if (facegenListItemVM.Index == index)
				{
					this.SetSelectedBeardType(facegenListItemVM, addCommand);
					break;
				}
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00024028 File Offset: 0x00022228
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey != null)
			{
				cancelInputKey.OnFinalize();
			}
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey != null)
			{
				doneInputKey.OnFinalize();
			}
			InputKeyItemVM previousTabInputKey = this.PreviousTabInputKey;
			if (previousTabInputKey != null)
			{
				previousTabInputKey.OnFinalize();
			}
			InputKeyItemVM nextTabInputKey = this.NextTabInputKey;
			if (nextTabInputKey != null)
			{
				nextTabInputKey.OnFinalize();
			}
			for (int i = 0; i < this.CameraControlKeys.Count; i++)
			{
				this.CameraControlKeys[i].OnFinalize();
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000240A6 File Offset: 0x000222A6
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x000240B5 File Offset: 0x000222B5
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000240C4 File Offset: 0x000222C4
		public void SetPreviousTabInputKey(HotKey hotKey)
		{
			this.PreviousTabInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000240D3 File Offset: 0x000222D3
		public void SetNextTabInputKey(HotKey hotKey)
		{
			this.NextTabInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000240E4 File Offset: 0x000222E4
		public void AddCameraControlInputKey(HotKey hotKey)
		{
			InputKeyItemVM item = InputKeyItemVM.CreateFromHotKey(hotKey, true);
			this.CameraControlKeys.Add(item);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00024108 File Offset: 0x00022308
		public void AddCameraControlInputKey(GameKey gameKey)
		{
			InputKeyItemVM item = InputKeyItemVM.CreateFromGameKey(gameKey, true);
			this.CameraControlKeys.Add(item);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x0002412C File Offset: 0x0002232C
		public void AddCameraControlInputKey(GameAxisKey gameAxisKey)
		{
			TextObject forcedName = Module.CurrentModule.GlobalTextManager.FindText("str_key_name", typeof(FaceGenHotkeyCategory).Name + "_" + gameAxisKey.Id);
			InputKeyItemVM item = InputKeyItemVM.CreateFromForcedID(gameAxisKey.AxisKey.ToString(), forcedName, true);
			this.CameraControlKeys.Add(item);
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0002418C File Offset: 0x0002238C
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x00024194 File Offset: 0x00022394
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000241B2 File Offset: 0x000223B2
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x000241BA File Offset: 0x000223BA
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x000241D8 File Offset: 0x000223D8
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x000241E0 File Offset: 0x000223E0
		[DataSourceProperty]
		public InputKeyItemVM PreviousTabInputKey
		{
			get
			{
				return this._previousTabInputKey;
			}
			set
			{
				if (value != this._previousTabInputKey)
				{
					this._previousTabInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "PreviousTabInputKey");
				}
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x000241FE File Offset: 0x000223FE
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x00024206 File Offset: 0x00022406
		[DataSourceProperty]
		public InputKeyItemVM NextTabInputKey
		{
			get
			{
				return this._nextTabInputKey;
			}
			set
			{
				if (value != this._nextTabInputKey)
				{
					this._nextTabInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "NextTabInputKey");
				}
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00024224 File Offset: 0x00022424
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x0002422C File Offset: 0x0002242C
		[DataSourceProperty]
		public MBBindingList<InputKeyItemVM> CameraControlKeys
		{
			get
			{
				return this._cameraControlKeys;
			}
			set
			{
				if (value != this._cameraControlKeys)
				{
					this._cameraControlKeys = value;
					base.OnPropertyChangedWithValue<MBBindingList<InputKeyItemVM>>(value, "CameraControlKeys");
				}
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0002424A File Offset: 0x0002244A
		[DataSourceProperty]
		public bool AreAllTabsEnabled
		{
			get
			{
				return this.IsBodyEnabled && this.IsFaceEnabled && this.IsEyesEnabled && this.IsNoseEnabled && this.IsMouthEnabled && this.IsHairEnabled && this.IsTaintEnabled;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00024284 File Offset: 0x00022484
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x0002428C File Offset: 0x0002248C
		[DataSourceProperty]
		public bool IsBodyEnabled
		{
			get
			{
				return this._isBodyEnabled;
			}
			set
			{
				if (value != this._isBodyEnabled)
				{
					this._isBodyEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsBodyEnabled");
				}
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x000242AA File Offset: 0x000224AA
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x000242B2 File Offset: 0x000224B2
		[DataSourceProperty]
		public bool IsFaceEnabled
		{
			get
			{
				return this._isFaceEnabled;
			}
			set
			{
				if (value != this._isFaceEnabled)
				{
					this._isFaceEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsFaceEnabled");
				}
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x000242D0 File Offset: 0x000224D0
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x000242D8 File Offset: 0x000224D8
		[DataSourceProperty]
		public bool IsEyesEnabled
		{
			get
			{
				return this._isEyesEnabled;
			}
			set
			{
				if (value != this._isEyesEnabled)
				{
					this._isEyesEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEyesEnabled");
				}
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x000242F6 File Offset: 0x000224F6
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x000242FE File Offset: 0x000224FE
		[DataSourceProperty]
		public bool IsNoseEnabled
		{
			get
			{
				return this._isNoseEnabled;
			}
			set
			{
				if (value != this._isNoseEnabled)
				{
					this._isNoseEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsNoseEnabled");
				}
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002431C File Offset: 0x0002251C
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00024324 File Offset: 0x00022524
		[DataSourceProperty]
		public bool IsMouthEnabled
		{
			get
			{
				return this._isMouthEnabled;
			}
			set
			{
				if (value != this._isMouthEnabled)
				{
					this._isMouthEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsMouthEnabled");
				}
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00024342 File Offset: 0x00022542
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0002434A File Offset: 0x0002254A
		[DataSourceProperty]
		public bool IsHairEnabled
		{
			get
			{
				return this._isHairEnabled;
			}
			set
			{
				if (value != this._isHairEnabled)
				{
					this._isHairEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsHairEnabled");
				}
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00024368 File Offset: 0x00022568
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x00024370 File Offset: 0x00022570
		[DataSourceProperty]
		public bool IsTaintEnabled
		{
			get
			{
				return this._isTaintEnabled;
			}
			set
			{
				if (value != this._isTaintEnabled)
				{
					this._isTaintEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsTaintEnabled");
				}
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0002438E File Offset: 0x0002258E
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x00024396 File Offset: 0x00022596
		[DataSourceProperty]
		public string FlipHairLbl
		{
			get
			{
				return this._flipHairLbl;
			}
			set
			{
				if (value != this._flipHairLbl)
				{
					this._flipHairLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "FlipHairLbl");
				}
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x000243B9 File Offset: 0x000225B9
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x000243C1 File Offset: 0x000225C1
		[DataSourceProperty]
		public string SkinColorLbl
		{
			get
			{
				return this._skinColorLbl;
			}
			set
			{
				if (value != this._skinColorLbl)
				{
					this._skinColorLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "SkinColorLbl");
				}
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x000243E4 File Offset: 0x000225E4
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x000243EC File Offset: 0x000225EC
		[DataSourceProperty]
		public string RaceLbl
		{
			get
			{
				return this._raceLbl;
			}
			set
			{
				if (value != this._raceLbl)
				{
					this._raceLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "RaceLbl");
				}
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0002440F File Offset: 0x0002260F
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00024417 File Offset: 0x00022617
		[DataSourceProperty]
		public string GenderLbl
		{
			get
			{
				return this._genderLbl;
			}
			set
			{
				if (value != this._genderLbl)
				{
					this._genderLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "GenderLbl");
				}
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0002443A File Offset: 0x0002263A
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x00024442 File Offset: 0x00022642
		[DataSourceProperty]
		public string CancelBtnLbl
		{
			get
			{
				return this._cancelBtnLbl;
			}
			set
			{
				if (value != this._cancelBtnLbl)
				{
					this._cancelBtnLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "CancelBtnLbl");
				}
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00024465 File Offset: 0x00022665
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x0002446D File Offset: 0x0002266D
		[DataSourceProperty]
		public string DoneBtnLbl
		{
			get
			{
				return this._doneBtnLbl;
			}
			set
			{
				if (value != this._doneBtnLbl)
				{
					this._doneBtnLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneBtnLbl");
				}
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00024490 File Offset: 0x00022690
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x00024498 File Offset: 0x00022698
		[DataSourceProperty]
		public HintViewModel BodyHint
		{
			get
			{
				return this._bodyHint;
			}
			set
			{
				if (value != this._bodyHint)
				{
					this._bodyHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "BodyHint");
				}
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x000244B6 File Offset: 0x000226B6
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x000244BE File Offset: 0x000226BE
		[DataSourceProperty]
		public HintViewModel FaceHint
		{
			get
			{
				return this._faceHint;
			}
			set
			{
				if (value != this._faceHint)
				{
					this._faceHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "FaceHint");
				}
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x000244DC File Offset: 0x000226DC
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x000244E4 File Offset: 0x000226E4
		[DataSourceProperty]
		public HintViewModel EyesHint
		{
			get
			{
				return this._eyesHint;
			}
			set
			{
				if (value != this._eyesHint)
				{
					this._eyesHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "EyesHint");
				}
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x00024502 File Offset: 0x00022702
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x0002450A File Offset: 0x0002270A
		[DataSourceProperty]
		public HintViewModel NoseHint
		{
			get
			{
				return this._noseHint;
			}
			set
			{
				if (value != this._noseHint)
				{
					this._noseHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "NoseHint");
				}
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00024528 File Offset: 0x00022728
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00024530 File Offset: 0x00022730
		[DataSourceProperty]
		public HintViewModel HairHint
		{
			get
			{
				return this._hairHint;
			}
			set
			{
				if (value != this._hairHint)
				{
					this._hairHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "HairHint");
				}
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0002454E File Offset: 0x0002274E
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00024556 File Offset: 0x00022756
		[DataSourceProperty]
		public HintViewModel TaintHint
		{
			get
			{
				return this._taintHint;
			}
			set
			{
				if (value != this._taintHint)
				{
					this._taintHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "TaintHint");
				}
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00024574 File Offset: 0x00022774
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x0002457C File Offset: 0x0002277C
		[DataSourceProperty]
		public HintViewModel MouthHint
		{
			get
			{
				return this._mouthHint;
			}
			set
			{
				if (value != this._mouthHint)
				{
					this._mouthHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "MouthHint");
				}
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0002459A File Offset: 0x0002279A
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x000245A2 File Offset: 0x000227A2
		[DataSourceProperty]
		public HintViewModel RedoHint
		{
			get
			{
				return this._redoHint;
			}
			set
			{
				if (value != this._redoHint)
				{
					this._redoHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RedoHint");
				}
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x000245C0 File Offset: 0x000227C0
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x000245C8 File Offset: 0x000227C8
		[DataSourceProperty]
		public HintViewModel UndoHint
		{
			get
			{
				return this._undoHint;
			}
			set
			{
				if (value != this._undoHint)
				{
					this._undoHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "UndoHint");
				}
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x000245E6 File Offset: 0x000227E6
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x000245EE File Offset: 0x000227EE
		[DataSourceProperty]
		public HintViewModel RandomizeHint
		{
			get
			{
				return this._randomizeHint;
			}
			set
			{
				if (value != this._randomizeHint)
				{
					this._randomizeHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RandomizeHint");
				}
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0002460C File Offset: 0x0002280C
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x00024614 File Offset: 0x00022814
		[DataSourceProperty]
		public HintViewModel RandomizeAllHint
		{
			get
			{
				return this._randomizeAllHint;
			}
			set
			{
				if (value != this._randomizeAllHint)
				{
					this._randomizeAllHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "RandomizeAllHint");
				}
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00024632 File Offset: 0x00022832
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0002463A File Offset: 0x0002283A
		[DataSourceProperty]
		public HintViewModel ResetHint
		{
			get
			{
				return this._resetHint;
			}
			set
			{
				if (value != this._resetHint)
				{
					this._resetHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ResetHint");
				}
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00024658 File Offset: 0x00022858
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00024660 File Offset: 0x00022860
		[DataSourceProperty]
		public HintViewModel ResetAllHint
		{
			get
			{
				return this._resetAllHint;
			}
			set
			{
				if (value != this._resetAllHint)
				{
					this._resetAllHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ResetAllHint");
				}
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0002467E File Offset: 0x0002287E
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00024686 File Offset: 0x00022886
		[DataSourceProperty]
		public HintViewModel ClothHint
		{
			get
			{
				return this._clothHint;
			}
			set
			{
				if (value != this._clothHint)
				{
					this._clothHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ClothHint");
				}
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000246A4 File Offset: 0x000228A4
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x000246AC File Offset: 0x000228AC
		[DataSourceProperty]
		public int HairNum
		{
			get
			{
				return this.hairNum;
			}
			set
			{
				if (value != this.hairNum)
				{
					this.hairNum = value;
					base.OnPropertyChangedWithValue(value, "HairNum");
				}
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x000246CA File Offset: 0x000228CA
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x000246D2 File Offset: 0x000228D2
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> SkinColorSelector
		{
			get
			{
				return this._skinColorSelector;
			}
			set
			{
				if (value != this._skinColorSelector)
				{
					this._skinColorSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "SkinColorSelector");
				}
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x000246F0 File Offset: 0x000228F0
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x000246F8 File Offset: 0x000228F8
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> HairColorSelector
		{
			get
			{
				return this._hairColorSelector;
			}
			set
			{
				if (value != this._hairColorSelector)
				{
					this._hairColorSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "HairColorSelector");
				}
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00024716 File Offset: 0x00022916
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x0002471E File Offset: 0x0002291E
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> TattooColorSelector
		{
			get
			{
				return this._tattooColorSelector;
			}
			set
			{
				if (value != this._tattooColorSelector)
				{
					this._tattooColorSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "TattooColorSelector");
				}
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0002473C File Offset: 0x0002293C
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x00024744 File Offset: 0x00022944
		[DataSourceProperty]
		public SelectorVM<SelectorItemVM> RaceSelector
		{
			get
			{
				return this._raceSelector;
			}
			set
			{
				if (value != this._raceSelector)
				{
					this._raceSelector = value;
					base.OnPropertyChangedWithValue<SelectorVM<SelectorItemVM>>(value, "RaceSelector");
				}
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00024762 File Offset: 0x00022962
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x0002476C File Offset: 0x0002296C
		[DataSourceProperty]
		public int Tab
		{
			get
			{
				return this._tab;
			}
			set
			{
				if (this._tab != value)
				{
					this._tab = value;
					base.OnPropertyChangedWithValue(value, "Tab");
					if (this._tab <= -1 || this._tab >= 7)
					{
						Debug.FailedAssert("Tab has been set to an invalid value!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\FaceGenerator\\FaceGenVM.cs", "Tab", 2214);
						this._tab = this._tabAvailabilities.IndexOf(true);
					}
				}
				switch (value)
				{
				case 0:
					this._faceGeneratorScreen.ChangeToBodyCamera();
					break;
				case 1:
				case 6:
					this._faceGeneratorScreen.ChangeToFaceCamera();
					break;
				case 2:
					this._faceGeneratorScreen.ChangeToEyeCamera();
					break;
				case 3:
					this._faceGeneratorScreen.ChangeToNoseCamera();
					break;
				case 4:
					this._faceGeneratorScreen.ChangeToMouthCamera();
					break;
				case 5:
					this._faceGeneratorScreen.ChangeToHairCamera();
					break;
				}
				this.UpdateTitle();
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00024848 File Offset: 0x00022A48
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x00024850 File Offset: 0x00022A50
		[DataSourceProperty]
		public int SelectedGender
		{
			get
			{
				return this._selectedGender;
			}
			set
			{
				if (this._initialGender == -1)
				{
					this._initialGender = value;
				}
				if (this._selectedGender != value)
				{
					this.AddCommand();
					this._selectedGender = value;
					this.UpdateRaceAndGenderBasedResources();
					this.Refresh(FaceGen.UpdateDeformKeys);
					base.OnPropertyChangedWithValue(value, "SelectedGender");
					base.OnPropertyChanged("IsFemale");
				}
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x000248AB File Offset: 0x00022AAB
		[DataSourceProperty]
		public bool IsFemale
		{
			get
			{
				return this.SelectedGender != 0;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x000248B6 File Offset: 0x00022AB6
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x000248BE File Offset: 0x00022ABE
		[DataSourceProperty]
		public MBBindingList<FaceGenPropertyVM> BodyProperties
		{
			get
			{
				return this._bodyProperties;
			}
			set
			{
				if (value != this._bodyProperties)
				{
					this._bodyProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<FaceGenPropertyVM>>(value, "BodyProperties");
				}
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x000248DC File Offset: 0x00022ADC
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x000248E4 File Offset: 0x00022AE4
		[DataSourceProperty]
		public bool CanChangeGender
		{
			get
			{
				return this._canChangeGender;
			}
			set
			{
				if (value != this._canChangeGender)
				{
					this._canChangeGender = value;
					base.OnPropertyChangedWithValue(value, "CanChangeGender");
				}
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00024902 File Offset: 0x00022B02
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0002490A File Offset: 0x00022B0A
		[DataSourceProperty]
		public bool CanChangeRace
		{
			get
			{
				return this._canChangeRace;
			}
			set
			{
				if (value != this._canChangeRace)
				{
					this._canChangeRace = value;
					base.OnPropertyChangedWithValue(value, "CanChangeRace");
				}
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00024928 File Offset: 0x00022B28
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x00024930 File Offset: 0x00022B30
		[DataSourceProperty]
		public bool IsUndoEnabled
		{
			get
			{
				return this._isUndoEnabled;
			}
			set
			{
				if (value != this._isUndoEnabled)
				{
					this._isUndoEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsUndoEnabled");
				}
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0002494E File Offset: 0x00022B4E
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x00024956 File Offset: 0x00022B56
		[DataSourceProperty]
		public bool IsRedoEnabled
		{
			get
			{
				return this._isRedoEnabled;
			}
			set
			{
				if (value != this._isRedoEnabled)
				{
					this._isRedoEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsRedoEnabled");
				}
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00024974 File Offset: 0x00022B74
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x0002497C File Offset: 0x00022B7C
		[DataSourceProperty]
		public MBBindingList<FaceGenPropertyVM> FaceProperties
		{
			get
			{
				return this._faceProperties;
			}
			set
			{
				if (value != this._faceProperties)
				{
					this._faceProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<FaceGenPropertyVM>>(value, "FaceProperties");
				}
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000930 RID: 2352 RVA: 0x0002499A File Offset: 0x00022B9A
		// (set) Token: 0x06000931 RID: 2353 RVA: 0x000249A2 File Offset: 0x00022BA2
		[DataSourceProperty]
		public MBBindingList<FaceGenPropertyVM> EyesProperties
		{
			get
			{
				return this._eyesProperties;
			}
			set
			{
				if (value != this._eyesProperties)
				{
					this._eyesProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<FaceGenPropertyVM>>(value, "EyesProperties");
				}
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000932 RID: 2354 RVA: 0x000249C0 File Offset: 0x00022BC0
		// (set) Token: 0x06000933 RID: 2355 RVA: 0x000249C8 File Offset: 0x00022BC8
		[DataSourceProperty]
		public MBBindingList<FaceGenPropertyVM> NoseProperties
		{
			get
			{
				return this._noseProperties;
			}
			set
			{
				if (value != this._noseProperties)
				{
					this._noseProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<FaceGenPropertyVM>>(value, "NoseProperties");
				}
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x000249E6 File Offset: 0x00022BE6
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x000249EE File Offset: 0x00022BEE
		[DataSourceProperty]
		public MBBindingList<FaceGenPropertyVM> MouthProperties
		{
			get
			{
				return this._mouthProperties;
			}
			set
			{
				if (value != this._mouthProperties)
				{
					this._mouthProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<FaceGenPropertyVM>>(value, "MouthProperties");
				}
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000936 RID: 2358 RVA: 0x00024A0C File Offset: 0x00022C0C
		// (set) Token: 0x06000937 RID: 2359 RVA: 0x00024A14 File Offset: 0x00022C14
		[DataSourceProperty]
		public MBBindingList<FaceGenPropertyVM> HairProperties
		{
			get
			{
				return this._hairProperties;
			}
			set
			{
				if (value != this._hairProperties)
				{
					this._hairProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<FaceGenPropertyVM>>(value, "HairProperties");
				}
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x00024A32 File Offset: 0x00022C32
		// (set) Token: 0x06000939 RID: 2361 RVA: 0x00024A3A File Offset: 0x00022C3A
		[DataSourceProperty]
		public MBBindingList<FaceGenPropertyVM> TaintProperties
		{
			get
			{
				return this._taintProperties;
			}
			set
			{
				if (value != this._taintProperties)
				{
					this._taintProperties = value;
					base.OnPropertyChangedWithValue<MBBindingList<FaceGenPropertyVM>>(value, "TaintProperties");
				}
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600093A RID: 2362 RVA: 0x00024A58 File Offset: 0x00022C58
		// (set) Token: 0x0600093B RID: 2363 RVA: 0x00024A60 File Offset: 0x00022C60
		[DataSourceProperty]
		public MBBindingList<FacegenListItemVM> TaintTypes
		{
			get
			{
				return this._taintTypes;
			}
			set
			{
				if (value != this._taintTypes)
				{
					this._taintTypes = value;
					base.OnPropertyChangedWithValue<MBBindingList<FacegenListItemVM>>(value, "TaintTypes");
				}
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600093C RID: 2364 RVA: 0x00024A7E File Offset: 0x00022C7E
		// (set) Token: 0x0600093D RID: 2365 RVA: 0x00024A86 File Offset: 0x00022C86
		[DataSourceProperty]
		public MBBindingList<FacegenListItemVM> BeardTypes
		{
			get
			{
				return this._beardTypes;
			}
			set
			{
				if (value != this._beardTypes)
				{
					this._beardTypes = value;
					base.OnPropertyChangedWithValue<MBBindingList<FacegenListItemVM>>(value, "BeardTypes");
				}
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600093E RID: 2366 RVA: 0x00024AA4 File Offset: 0x00022CA4
		// (set) Token: 0x0600093F RID: 2367 RVA: 0x00024AAC File Offset: 0x00022CAC
		[DataSourceProperty]
		public MBBindingList<FacegenListItemVM> HairTypes
		{
			get
			{
				return this._hairTypes;
			}
			set
			{
				if (value != this._hairTypes)
				{
					this._hairTypes = value;
					base.OnPropertyChangedWithValue<MBBindingList<FacegenListItemVM>>(value, "HairTypes");
				}
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000940 RID: 2368 RVA: 0x00024ACA File Offset: 0x00022CCA
		// (set) Token: 0x06000941 RID: 2369 RVA: 0x00024AD2 File Offset: 0x00022CD2
		[DataSourceProperty]
		public FaceGenPropertyVM SoundPreset
		{
			get
			{
				return this._soundPreset;
			}
			set
			{
				if (value != this._soundPreset)
				{
					this._soundPreset = value;
					base.OnPropertyChangedWithValue<FaceGenPropertyVM>(value, "SoundPreset");
				}
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00024AF0 File Offset: 0x00022CF0
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x00024AF8 File Offset: 0x00022CF8
		[DataSourceProperty]
		public FaceGenPropertyVM EyebrowTypes
		{
			get
			{
				return this._eyebrowTypes;
			}
			set
			{
				if (value != this._eyebrowTypes)
				{
					this._eyebrowTypes = value;
					base.OnPropertyChangedWithValue<FaceGenPropertyVM>(value, "EyebrowTypes");
				}
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00024B16 File Offset: 0x00022D16
		// (set) Token: 0x06000945 RID: 2373 RVA: 0x00024B1E File Offset: 0x00022D1E
		[DataSourceProperty]
		public FaceGenPropertyVM TeethTypes
		{
			get
			{
				return this._teethTypes;
			}
			set
			{
				if (value != this._teethTypes)
				{
					this._teethTypes = value;
					base.OnPropertyChangedWithValue<FaceGenPropertyVM>(value, "TeethTypes");
				}
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00024B3C File Offset: 0x00022D3C
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x00024B49 File Offset: 0x00022D49
		[DataSourceProperty]
		public bool FlipHairCb
		{
			get
			{
				return this._faceGenerationParams.IsHairFlipped;
			}
			set
			{
				if (value != this._faceGenerationParams.IsHairFlipped)
				{
					this._faceGenerationParams.IsHairFlipped = value;
					base.OnPropertyChangedWithValue(value, "FlipHairCb");
					this.UpdateFace();
				}
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00024B77 File Offset: 0x00022D77
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x00024B7F File Offset: 0x00022D7F
		[DataSourceProperty]
		public bool IsDressed
		{
			get
			{
				return this._isDressed;
			}
			set
			{
				if (value != this._isDressed)
				{
					this._isDressed = value;
					base.OnPropertyChangedWithValue(value, "IsDressed");
				}
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x00024B9D File Offset: 0x00022D9D
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x00024BA5 File Offset: 0x00022DA5
		[DataSourceProperty]
		public bool CharacterGamepadControlsEnabled
		{
			get
			{
				return this._characterGamepadControlsEnabled;
			}
			set
			{
				if (value != this._characterGamepadControlsEnabled)
				{
					this._characterGamepadControlsEnabled = value;
					base.OnPropertyChangedWithValue(value, "CharacterGamepadControlsEnabled");
				}
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00024BC3 File Offset: 0x00022DC3
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x00024BCB File Offset: 0x00022DCB
		[DataSourceProperty]
		public FaceGenPropertyVM FaceTypes
		{
			get
			{
				return this._faceTypes;
			}
			set
			{
				if (value != this._faceTypes)
				{
					this._faceTypes = value;
					base.OnPropertyChangedWithValue<FaceGenPropertyVM>(value, "FaceTypes");
				}
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00024BE9 File Offset: 0x00022DE9
		// (set) Token: 0x0600094F RID: 2383 RVA: 0x00024BF1 File Offset: 0x00022DF1
		[DataSourceProperty]
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				if (value != this._title)
				{
					this._title = value;
					base.OnPropertyChangedWithValue<string>(value, "Title");
				}
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00024C14 File Offset: 0x00022E14
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00024C1C File Offset: 0x00022E1C
		[DataSourceProperty]
		public int TotalStageCount
		{
			get
			{
				return this._totalStageCount;
			}
			set
			{
				if (value != this._totalStageCount)
				{
					this._totalStageCount = value;
					base.OnPropertyChangedWithValue(value, "TotalStageCount");
				}
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x00024C3A File Offset: 0x00022E3A
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x00024C42 File Offset: 0x00022E42
		[DataSourceProperty]
		public int CurrentStageIndex
		{
			get
			{
				return this._currentStageIndex;
			}
			set
			{
				if (value != this._currentStageIndex)
				{
					this._currentStageIndex = value;
					base.OnPropertyChangedWithValue(value, "CurrentStageIndex");
				}
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000954 RID: 2388 RVA: 0x00024C60 File Offset: 0x00022E60
		// (set) Token: 0x06000955 RID: 2389 RVA: 0x00024C68 File Offset: 0x00022E68
		[DataSourceProperty]
		public int FurthestIndex
		{
			get
			{
				return this._furthestIndex;
			}
			set
			{
				if (value != this._furthestIndex)
				{
					this._furthestIndex = value;
					base.OnPropertyChangedWithValue(value, "FurthestIndex");
				}
			}
		}

		// Token: 0x04000404 RID: 1028
		private const float MultiplayerHeightSliderMinValue = 0.25f;

		// Token: 0x04000405 RID: 1029
		private const float MultiplayerHeightSliderMaxValue = 0.75f;

		// Token: 0x04000406 RID: 1030
		private readonly IFaceGeneratorHandler _faceGeneratorScreen;

		// Token: 0x04000407 RID: 1031
		private bool _characterRefreshEnabled = true;

		// Token: 0x04000408 RID: 1032
		private bool _initialValuesSet;

		// Token: 0x04000409 RID: 1033
		private readonly BodyGenerator _bodyGenerator;

		// Token: 0x0400040A RID: 1034
		private readonly TextObject _affirmitiveText;

		// Token: 0x0400040B RID: 1035
		private readonly TextObject _negativeText;

		// Token: 0x0400040C RID: 1036
		private FaceGenerationParams _faceGenerationParams = FaceGenerationParams.Create();

		// Token: 0x0400040D RID: 1037
		private List<FaceGenVM.UndoRedoKey> _undoCommands;

		// Token: 0x0400040E RID: 1038
		private List<FaceGenVM.UndoRedoKey> _redoCommands;

		// Token: 0x0400040F RID: 1039
		private List<bool> _isVoiceTypeUsableForOnlyNpc;

		// Token: 0x04000410 RID: 1040
		private MBReadOnlyList<bool> _tabAvailabilities;

		// Token: 0x04000411 RID: 1041
		private Action<float> _onHeightChanged;

		// Token: 0x04000412 RID: 1042
		private Action _onAgeChanged;

		// Token: 0x04000413 RID: 1043
		private int _initialRace = -1;

		// Token: 0x04000414 RID: 1044
		private int _initialGender = -1;

		// Token: 0x04000415 RID: 1045
		private BodyMeshMaturityType _latestMaturityType;

		// Token: 0x04000416 RID: 1046
		private bool _isRandomizing;

		// Token: 0x04000417 RID: 1047
		private readonly Action<int> _goToIndex;

		// Token: 0x04000418 RID: 1048
		private FaceGenVM.GenderBasedSelectedValue[] genderBasedSelectedValues;

		// Token: 0x04000419 RID: 1049
		private readonly Dictionary<FaceGenVM.FaceGenTabs, MBBindingList<FaceGenPropertyVM>> _tabProperties;

		// Token: 0x0400041A RID: 1050
		private List<uint> _skinColors;

		// Token: 0x0400041B RID: 1051
		private List<uint> _hairColors;

		// Token: 0x0400041C RID: 1052
		private List<uint> _tattooColors;

		// Token: 0x0400041D RID: 1053
		private readonly bool _showDebugValues;

		// Token: 0x0400041E RID: 1054
		private readonly bool _openedFromMultiplayer;

		// Token: 0x0400041F RID: 1055
		private bool _enforceConstraints;

		// Token: 0x04000420 RID: 1056
		private IFaceGeneratorCustomFilter _filter;

		// Token: 0x04000421 RID: 1057
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000422 RID: 1058
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000423 RID: 1059
		private InputKeyItemVM _previousTabInputKey;

		// Token: 0x04000424 RID: 1060
		private InputKeyItemVM _nextTabInputKey;

		// Token: 0x04000425 RID: 1061
		private MBBindingList<InputKeyItemVM> _cameraControlKeys;

		// Token: 0x04000426 RID: 1062
		private bool _isBodyEnabled;

		// Token: 0x04000427 RID: 1063
		private bool _isFaceEnabled;

		// Token: 0x04000428 RID: 1064
		private bool _isEyesEnabled;

		// Token: 0x04000429 RID: 1065
		private bool _isNoseEnabled;

		// Token: 0x0400042A RID: 1066
		private bool _isMouthEnabled;

		// Token: 0x0400042B RID: 1067
		private bool _isHairEnabled;

		// Token: 0x0400042C RID: 1068
		private bool _isTaintEnabled;

		// Token: 0x0400042D RID: 1069
		private string _cancelBtnLbl;

		// Token: 0x0400042E RID: 1070
		private string _doneBtnLbl;

		// Token: 0x0400042F RID: 1071
		private int _initialSelectedTaintType;

		// Token: 0x04000430 RID: 1072
		private int _initialSelectedHairType;

		// Token: 0x04000431 RID: 1073
		private int _initialSelectedBeardType;

		// Token: 0x04000432 RID: 1074
		private float _initialSelectedSkinColor;

		// Token: 0x04000433 RID: 1075
		private float _initialSelectedHairColor;

		// Token: 0x04000434 RID: 1076
		private float _initialSelectedTaintColor;

		// Token: 0x04000435 RID: 1077
		private string _flipHairLbl;

		// Token: 0x04000436 RID: 1078
		private string _skinColorLbl;

		// Token: 0x04000437 RID: 1079
		private string _raceLbl;

		// Token: 0x04000438 RID: 1080
		private string _genderLbl;

		// Token: 0x04000439 RID: 1081
		private FaceGenPropertyVM _heightSlider;

		// Token: 0x0400043A RID: 1082
		private HintViewModel _bodyHint;

		// Token: 0x0400043B RID: 1083
		private HintViewModel _faceHint;

		// Token: 0x0400043C RID: 1084
		private HintViewModel _eyesHint;

		// Token: 0x0400043D RID: 1085
		private HintViewModel _noseHint;

		// Token: 0x0400043E RID: 1086
		private HintViewModel _hairHint;

		// Token: 0x0400043F RID: 1087
		private HintViewModel _taintHint;

		// Token: 0x04000440 RID: 1088
		private HintViewModel _mouthHint;

		// Token: 0x04000441 RID: 1089
		private HintViewModel _redoHint;

		// Token: 0x04000442 RID: 1090
		private HintViewModel _undoHint;

		// Token: 0x04000443 RID: 1091
		private HintViewModel _randomizeHint;

		// Token: 0x04000444 RID: 1092
		private HintViewModel _randomizeAllHint;

		// Token: 0x04000445 RID: 1093
		private HintViewModel _resetHint;

		// Token: 0x04000446 RID: 1094
		private HintViewModel _resetAllHint;

		// Token: 0x04000447 RID: 1095
		private HintViewModel _clothHint;

		// Token: 0x04000448 RID: 1096
		private int hairNum;

		// Token: 0x04000449 RID: 1097
		private int beardNum;

		// Token: 0x0400044A RID: 1098
		private int faceTextureNum;

		// Token: 0x0400044B RID: 1099
		private int mouthTextureNum;

		// Token: 0x0400044C RID: 1100
		private int eyebrowTextureNum;

		// Token: 0x0400044D RID: 1101
		private int faceTattooNum;

		// Token: 0x0400044E RID: 1102
		private int _newSoundPresetSize;

		// Token: 0x0400044F RID: 1103
		private float _scale = 1f;

		// Token: 0x04000450 RID: 1104
		private int _tab = -1;

		// Token: 0x04000451 RID: 1105
		private int _selectedRace = -1;

		// Token: 0x04000452 RID: 1106
		private int _selectedGender = -1;

		// Token: 0x04000453 RID: 1107
		private bool _canChangeGender;

		// Token: 0x04000454 RID: 1108
		private bool _canChangeRace;

		// Token: 0x04000455 RID: 1109
		private bool _isDressed;

		// Token: 0x04000456 RID: 1110
		private bool _characterGamepadControlsEnabled;

		// Token: 0x04000457 RID: 1111
		private bool _isUndoEnabled;

		// Token: 0x04000458 RID: 1112
		private bool _isRedoEnabled;

		// Token: 0x04000459 RID: 1113
		private MBBindingList<FaceGenPropertyVM> _bodyProperties;

		// Token: 0x0400045A RID: 1114
		private MBBindingList<FaceGenPropertyVM> _faceProperties;

		// Token: 0x0400045B RID: 1115
		private MBBindingList<FaceGenPropertyVM> _eyesProperties;

		// Token: 0x0400045C RID: 1116
		private MBBindingList<FaceGenPropertyVM> _noseProperties;

		// Token: 0x0400045D RID: 1117
		private MBBindingList<FaceGenPropertyVM> _mouthProperties;

		// Token: 0x0400045E RID: 1118
		private MBBindingList<FaceGenPropertyVM> _hairProperties;

		// Token: 0x0400045F RID: 1119
		private MBBindingList<FaceGenPropertyVM> _taintProperties;

		// Token: 0x04000460 RID: 1120
		private MBBindingList<FacegenListItemVM> _taintTypes;

		// Token: 0x04000461 RID: 1121
		private MBBindingList<FacegenListItemVM> _beardTypes;

		// Token: 0x04000462 RID: 1122
		private MBBindingList<FacegenListItemVM> _hairTypes;

		// Token: 0x04000463 RID: 1123
		private FaceGenPropertyVM _soundPreset;

		// Token: 0x04000464 RID: 1124
		private FaceGenPropertyVM _faceTypes;

		// Token: 0x04000465 RID: 1125
		private FaceGenPropertyVM _teethTypes;

		// Token: 0x04000466 RID: 1126
		private FaceGenPropertyVM _eyebrowTypes;

		// Token: 0x04000467 RID: 1127
		private SelectorVM<SelectorItemVM> _skinColorSelector;

		// Token: 0x04000468 RID: 1128
		private SelectorVM<SelectorItemVM> _hairColorSelector;

		// Token: 0x04000469 RID: 1129
		private SelectorVM<SelectorItemVM> _tattooColorSelector;

		// Token: 0x0400046A RID: 1130
		private SelectorVM<SelectorItemVM> _raceSelector;

		// Token: 0x0400046B RID: 1131
		private FacegenListItemVM _selectedTaintType;

		// Token: 0x0400046C RID: 1132
		private FacegenListItemVM _selectedBeardType;

		// Token: 0x0400046D RID: 1133
		private FacegenListItemVM _selectedHairType;

		// Token: 0x0400046E RID: 1134
		private string _title = "";

		// Token: 0x0400046F RID: 1135
		private int _totalStageCount = -1;

		// Token: 0x04000470 RID: 1136
		private int _currentStageIndex = -1;

		// Token: 0x04000471 RID: 1137
		private int _furthestIndex = -1;

		// Token: 0x020000EF RID: 239
		public enum FaceGenTabs
		{
			// Token: 0x04000676 RID: 1654
			None = -1,
			// Token: 0x04000677 RID: 1655
			Body,
			// Token: 0x04000678 RID: 1656
			Face,
			// Token: 0x04000679 RID: 1657
			Eyes,
			// Token: 0x0400067A RID: 1658
			Nose,
			// Token: 0x0400067B RID: 1659
			Mouth,
			// Token: 0x0400067C RID: 1660
			Hair,
			// Token: 0x0400067D RID: 1661
			Taint,
			// Token: 0x0400067E RID: 1662
			NumOfFaceGenTabs
		}

		// Token: 0x020000F0 RID: 240
		public enum Presets
		{
			// Token: 0x04000680 RID: 1664
			Gender = -1,
			// Token: 0x04000681 RID: 1665
			FacePresets = -2,
			// Token: 0x04000682 RID: 1666
			FaceType = -3,
			// Token: 0x04000683 RID: 1667
			EyePresets = -4,
			// Token: 0x04000684 RID: 1668
			HairBeardPreset = -5,
			// Token: 0x04000685 RID: 1669
			HairType = -6,
			// Token: 0x04000686 RID: 1670
			BeardType = -7,
			// Token: 0x04000687 RID: 1671
			TaintPresets = -8,
			// Token: 0x04000688 RID: 1672
			SoundPresets = -9,
			// Token: 0x04000689 RID: 1673
			TaintType = -10,
			// Token: 0x0400068A RID: 1674
			Age = -11,
			// Token: 0x0400068B RID: 1675
			EyeColor = -12,
			// Token: 0x0400068C RID: 1676
			HairAndBeardColor = -13,
			// Token: 0x0400068D RID: 1677
			TeethType = -14,
			// Token: 0x0400068E RID: 1678
			EyebrowType = -15,
			// Token: 0x0400068F RID: 1679
			Scale = -16,
			// Token: 0x04000690 RID: 1680
			Weight = -17,
			// Token: 0x04000691 RID: 1681
			Build = -18,
			// Token: 0x04000692 RID: 1682
			Pitch = -19,
			// Token: 0x04000693 RID: 1683
			Race = -20
		}

		// Token: 0x020000F1 RID: 241
		public struct GenderBasedSelectedValue
		{
			// Token: 0x06000C01 RID: 3073 RVA: 0x00029AD0 File Offset: 0x00027CD0
			public void Reset()
			{
				this.Hair = -1;
				this.Beard = -1;
				this.FaceTexture = -1;
				this.MouthTexture = -1;
				this.Tattoo = -1;
				this.SoundPreset = -1;
				this.EyebrowTexture = -1;
			}

			// Token: 0x04000694 RID: 1684
			public int Hair;

			// Token: 0x04000695 RID: 1685
			public int Beard;

			// Token: 0x04000696 RID: 1686
			public int FaceTexture;

			// Token: 0x04000697 RID: 1687
			public int MouthTexture;

			// Token: 0x04000698 RID: 1688
			public int Tattoo;

			// Token: 0x04000699 RID: 1689
			public int SoundPreset;

			// Token: 0x0400069A RID: 1690
			public int EyebrowTexture;
		}

		// Token: 0x020000F2 RID: 242
		private struct UndoRedoKey
		{
			// Token: 0x06000C02 RID: 3074 RVA: 0x00029B03 File Offset: 0x00027D03
			public UndoRedoKey(int gender, int race, BodyProperties bodyProperties)
			{
				this.Gender = gender;
				this.Race = race;
				this.BodyProperties = bodyProperties;
			}

			// Token: 0x0400069B RID: 1691
			public readonly int Gender;

			// Token: 0x0400069C RID: 1692
			public readonly int Race;

			// Token: 0x0400069D RID: 1693
			public readonly BodyProperties BodyProperties;
		}
	}
}
