﻿using System;
using System.ComponentModel;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;
using TaleWorlds.MountAndBlade.ViewModelCollection.FaceGenerator;

namespace TaleWorlds.MountAndBlade.GauntletUI.AutoGenerated1
{
	// Token: 0x02000019 RID: 25
	public class FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_19_FaceGenGrid__DependendPrefab : ListPanel
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x0002C3CB File Offset: 0x0002A5CB
		public FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_19_FaceGenGrid__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0002C3D4 File Offset: 0x0002A5D4
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new ScrollbarWidget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_0_0 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_0);
			this._widget_0_1 = new ImageWidget(base.Context);
			this._widget_0.AddChild(this._widget_0_1);
			this._widget_1 = new ScrollablePanel(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_1_0 = new Widget(base.Context);
			this._widget_1.AddChild(this._widget_1_0);
			this._widget_1_0_0 = new NavigatableGridWidget(base.Context);
			this._widget_1_0.AddChild(this._widget_1_0_0);
			this._widget_1_1 = new Widget(base.Context);
			this._widget_1.AddChild(this._widget_1_1);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0002C4D8 File Offset: 0x0002A6D8
		public void SetIds()
		{
			this._widget_0.Id = "VerticalScrollbar";
			this._widget_0_1.Id = "VerticalScrollbarHandle";
			this._widget_1_0.Id = "ClipRect";
			this._widget_1_0_0.Id = "Grid";
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0002C528 File Offset: 0x0002A728
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.StretchToParent;
			base.StackLayout.LayoutMethod = LayoutMethod.HorizontalRightToLeft;
			base.DoNotUseCustomScaleAndChildren = true;
			this._widget_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.SuggestedWidth = 8f;
			this._widget_0.HorizontalAlignment = HorizontalAlignment.Right;
			this._widget_0.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0.MarginTop = 15f;
			this._widget_0.MarginBottom = 15f;
			this._widget_0.AlignmentAxis = AlignmentAxis.Vertical;
			this._widget_0.Handle = this._widget_0_1;
			this._widget_0.MaxValue = 100f;
			this._widget_0.MinValue = 0f;
			this._widget_0_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0_0.SuggestedWidth = 4f;
			this._widget_0_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0_0.Sprite = base.Context.SpriteData.GetSprite("BlankWhiteSquare_9");
			this._widget_0_0.Color = new Color(0.3529412f, 0.2509804f, 0.2f, 1f);
			this._widget_0_0.AlphaFactor = 0.2f;
			this._widget_0_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_1.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_1.SuggestedHeight = 10f;
			this._widget_0_1.SuggestedWidth = 8f;
			this._widget_0_1.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0_1.Brush = base.Context.GetBrush("FaceGen.Scrollbar.Handle");
			this._widget_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.AutoHideScrollBars = true;
			this._widget_1.ClipRect = this._widget_1_0;
			this._widget_1.InnerPanel = this._widget_1_0_0;
			this._widget_1.VerticalScrollbar = this._widget_0;
			this._widget_1_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_0.ClipContents = true;
			this._widget_1_0_0.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_1_0_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1_0_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_1_0_0.ColumnCount = 5;
			this._widget_1_0_0.DefaultCellHeight = 112f;
			this._widget_1_0_0.DefaultCellWidth = 112f;
			this._widget_1_0_0.MinIndex = 1001;
			this._widget_1_0_0.MaxIndex = 2000;
			this._widget_1_1.DoNotAcceptEvents = true;
			this._widget_1_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_1_1.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_1_1.SuggestedWidth = 576f;
			this._widget_1_1.SuggestedHeight = 57f;
			this._widget_1_1.HorizontalAlignment = HorizontalAlignment.Right;
			this._widget_1_1.PositionXOffset = 13f;
			this._widget_1_1.VerticalAlignment = VerticalAlignment.Bottom;
			this._widget_1_1.Sprite = base.Context.SpriteData.GetSprite("General\\CharacterCreation\\character_creation_scroll_gradient");
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0002C854 File Offset: 0x0002AA54
		public void DestroyDataSource()
		{
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				if (this._datasource_Root_TaintTypes != null)
				{
					this._datasource_Root_TaintTypes.ListChanged -= this.OnList_datasource_Root_TaintTypesChanged;
					for (int i = this._widget_1_0_0.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_1_0_0.GetChild(i);
						((FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate)child).OnBeforeRemovedChild(child);
						((FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate)this._widget_1_0_0.GetChild(i)).DestroyDataSource();
					}
					this._datasource_Root_TaintTypes = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0002C9AF File Offset: 0x0002ABAF
		public void SetDataSource(FaceGenVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0002C9B8 File Offset: 0x0002ABB8
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0002C9C6 File Offset: 0x0002ABC6
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0002C9D4 File Offset: 0x0002ABD4
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0002C9E2 File Offset: 0x0002ABE2
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0002C9F0 File Offset: 0x0002ABF0
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0002C9FE File Offset: 0x0002ABFE
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002CA0C File Offset: 0x0002AC0C
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0002CA1A File Offset: 0x0002AC1A
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0002CA28 File Offset: 0x0002AC28
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0002CA36 File Offset: 0x0002AC36
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "TaintTypes")
			{
				this.RefreshDataSource_datasource_Root_TaintTypes(this._datasource_Root.TaintTypes);
				return;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0002CA58 File Offset: 0x0002AC58
		public void OnList_datasource_Root_TaintTypesChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				for (int i = this._widget_1_0_0.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_1_0_0.GetChild(i);
					((FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_1_0_0.GetChild(i);
					((FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate)child2).SetDataSource(null);
					this._widget_1_0_0.RemoveChild(child2);
				}
				return;
			case ListChangedType.Sorted:
				for (int j = 0; j < this._datasource_Root_TaintTypes.Count; j++)
				{
					FacegenListItemVM bindingObject = this._datasource_Root_TaintTypes[j];
					this._widget_1_0_0.FindChild((Widget widget) => widget.GetComponent<GeneratedWidgetData>().Data == bindingObject).SetSiblingIndex(j, false);
				}
				return;
			case ListChangedType.ItemAdded:
			{
				FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate = new FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate(base.Context);
				GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate);
				FacegenListItemVM facegenListItemVM = this._datasource_Root_TaintTypes[e.NewIndex];
				generatedWidgetData.Data = facegenListItemVM;
				faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.AddComponent(generatedWidgetData);
				this._widget_1_0_0.AddChildAtIndex(faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate, e.NewIndex);
				faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.CreateWidgets();
				faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.SetIds();
				faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.SetAttributes();
				faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.SetDataSource(facegenListItemVM);
				return;
			}
			case ListChangedType.ItemBeforeDeleted:
			{
				Widget child3 = this._widget_1_0_0.GetChild(e.NewIndex);
				((FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate)child3).OnBeforeRemovedChild(child3);
				return;
			}
			case ListChangedType.ItemDeleted:
			{
				Widget child4 = this._widget_1_0_0.GetChild(e.NewIndex);
				((FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate)child4).SetDataSource(null);
				this._widget_1_0_0.RemoveChild(child4);
				break;
			}
			case ListChangedType.ItemChanged:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0002CBF4 File Offset: 0x0002ADF4
		private void RefreshDataSource_datasource_Root(FaceGenVM newDataSource)
		{
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				if (this._datasource_Root_TaintTypes != null)
				{
					this._datasource_Root_TaintTypes.ListChanged -= this.OnList_datasource_Root_TaintTypesChanged;
					for (int i = this._widget_1_0_0.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_1_0_0.GetChild(i);
						((FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate)child).OnBeforeRemovedChild(child);
						Widget child2 = this._widget_1_0_0.GetChild(i);
						((FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate)child2).SetDataSource(null);
						this._widget_1_0_0.RemoveChild(child2);
					}
					this._datasource_Root_TaintTypes = null;
				}
				this._datasource_Root = null;
			}
			this._datasource_Root = newDataSource;
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				this._datasource_Root_TaintTypes = this._datasource_Root.TaintTypes;
				if (this._datasource_Root_TaintTypes != null)
				{
					this._datasource_Root_TaintTypes.ListChanged += this.OnList_datasource_Root_TaintTypesChanged;
					for (int j = 0; j < this._datasource_Root_TaintTypes.Count; j++)
					{
						FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate = new FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate(base.Context);
						GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate);
						FacegenListItemVM facegenListItemVM = this._datasource_Root_TaintTypes[j];
						generatedWidgetData.Data = facegenListItemVM;
						faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.AddComponent(generatedWidgetData);
						this._widget_1_0_0.AddChildAtIndex(faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate, j);
						faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.CreateWidgets();
						faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.SetIds();
						faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.SetAttributes();
						faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.SetDataSource(facegenListItemVM);
					}
				}
			}
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002CEEC File Offset: 0x0002B0EC
		private void RefreshDataSource_datasource_Root_TaintTypes(MBBindingList<FacegenListItemVM> newDataSource)
		{
			if (this._datasource_Root_TaintTypes != null)
			{
				this._datasource_Root_TaintTypes.ListChanged -= this.OnList_datasource_Root_TaintTypesChanged;
				for (int i = this._widget_1_0_0.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_1_0_0.GetChild(i);
					((FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_1_0_0.GetChild(i);
					((FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate)child2).SetDataSource(null);
					this._widget_1_0_0.RemoveChild(child2);
				}
				this._datasource_Root_TaintTypes = null;
			}
			this._datasource_Root_TaintTypes = newDataSource;
			this._datasource_Root_TaintTypes = this._datasource_Root.TaintTypes;
			if (this._datasource_Root_TaintTypes != null)
			{
				this._datasource_Root_TaintTypes.ListChanged += this.OnList_datasource_Root_TaintTypesChanged;
				for (int j = 0; j < this._datasource_Root_TaintTypes.Count; j++)
				{
					FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate = new FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate(base.Context);
					GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate);
					FacegenListItemVM facegenListItemVM = this._datasource_Root_TaintTypes[j];
					generatedWidgetData.Data = facegenListItemVM;
					faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.AddComponent(generatedWidgetData);
					this._widget_1_0_0.AddChildAtIndex(faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate, j);
					faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.CreateWidgets();
					faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.SetIds();
					faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.SetAttributes();
					faceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_26_ItemTemplate.SetDataSource(facegenListItemVM);
				}
			}
		}

		// Token: 0x0400015F RID: 351
		private ListPanel _widget;

		// Token: 0x04000160 RID: 352
		private ScrollbarWidget _widget_0;

		// Token: 0x04000161 RID: 353
		private Widget _widget_0_0;

		// Token: 0x04000162 RID: 354
		private ImageWidget _widget_0_1;

		// Token: 0x04000163 RID: 355
		private ScrollablePanel _widget_1;

		// Token: 0x04000164 RID: 356
		private Widget _widget_1_0;

		// Token: 0x04000165 RID: 357
		private NavigatableGridWidget _widget_1_0_0;

		// Token: 0x04000166 RID: 358
		private Widget _widget_1_1;

		// Token: 0x04000167 RID: 359
		private FaceGenVM _datasource_Root;

		// Token: 0x04000168 RID: 360
		private MBBindingList<FacegenListItemVM> _datasource_Root_TaintTypes;
	}
}
