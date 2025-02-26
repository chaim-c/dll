﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay;
using TaleWorlds.Core;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x0200009A RID: 154
	public class SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_2_ItemTemplate : Widget
	{
		// Token: 0x060025EB RID: 9707 RVA: 0x0011E6AF File Offset: 0x0011C8AF
		public SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_2_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x0011E6B8 File Offset: 0x0011C8B8
		private VisualDefinition CreateVisualDefinitionTopPanel()
		{
			VisualDefinition visualDefinition = new VisualDefinition("TopPanel", 0.45f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = -6f
			});
			visualDefinition.AddVisualState(new VisualState("Disabled")
			{
				PositionYOffset = -400f
			});
			return visualDefinition;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x0011E714 File Offset: 0x0011C914
		private VisualDefinition CreateVisualDefinitionCharacterPartyExtension()
		{
			VisualDefinition visualDefinition = new VisualDefinition("CharacterPartyExtension", 0.9f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 78f
			});
			visualDefinition.AddVisualState(new VisualState("Disabled")
			{
				PositionYOffset = -400f
			});
			return visualDefinition;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x0011E770 File Offset: 0x0011C970
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new ImageIdentifierWidget(base.Context);
			this._widget.AddChild(this._widget_0);
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x0011E79B File Offset: 0x0011C99B
		public void SetIds()
		{
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x0011E7A0 File Offset: 0x0011C9A0
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedWidth = 45f;
			base.SuggestedHeight = 30f;
			base.VerticalAlignment = VerticalAlignment.Bottom;
			base.MarginLeft = 3f;
			base.MarginRight = 3f;
			base.MarginTop = 4f;
			base.MarginBottom = 15f;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x0011E81C File Offset: 0x0011CA1C
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
				if (this._datasource_Root_Visual != null)
				{
					this._datasource_Root_Visual.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
					this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
					this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
					this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
					this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
					this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
					this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
					this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
					this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
					this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
					this._datasource_Root_Visual = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x0011EABA File Offset: 0x0011CCBA
		public void SetDataSource(GameMenuPartyItemVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x0011EAC3 File Offset: 0x0011CCC3
		private void PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x0011EACC File Offset: 0x0011CCCC
		private void boolPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025F5 RID: 9717 RVA: 0x0011EAD5 File Offset: 0x0011CCD5
		private void floatPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x0011EADE File Offset: 0x0011CCDE
		private void Vec2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x0011EAE7 File Offset: 0x0011CCE7
		private void Vector2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x0011EAF0 File Offset: 0x0011CCF0
		private void doublePropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x0011EAF9 File Offset: 0x0011CCF9
		private void intPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x0011EB02 File Offset: 0x0011CD02
		private void uintPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x0011EB0B File Offset: 0x0011CD0B
		private void ColorPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x0011EB14 File Offset: 0x0011CD14
		private void HandleWidgetPropertyChangeOf_widget_0(string propertyName)
		{
			if (propertyName == "AdditionalArgs")
			{
				return;
			}
			if (propertyName == "ImageId")
			{
				return;
			}
			propertyName == "ImageTypeCode";
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x0011EB3E File Offset: 0x0011CD3E
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x0011EB4C File Offset: 0x0011CD4C
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x0011EB5A File Offset: 0x0011CD5A
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x0011EB68 File Offset: 0x0011CD68
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x0011EB76 File Offset: 0x0011CD76
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x0011EB84 File Offset: 0x0011CD84
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x0011EB92 File Offset: 0x0011CD92
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x0011EBA0 File Offset: 0x0011CDA0
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x0011EBAE File Offset: 0x0011CDAE
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x0011EBBC File Offset: 0x0011CDBC
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Visual")
			{
				this.RefreshDataSource_datasource_Root_Visual(this._datasource_Root.Visual);
				return;
			}
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x0011EBDD File Offset: 0x0011CDDD
		private void ViewModelPropertyChangedListenerOf_datasource_Root_Visual(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x0011EBEB File Offset: 0x0011CDEB
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x0011EBF9 File Offset: 0x0011CDF9
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x0011EC07 File Offset: 0x0011CE07
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x0011EC15 File Offset: 0x0011CE15
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x0011EC23 File Offset: 0x0011CE23
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x0011EC31 File Offset: 0x0011CE31
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x0011EC3F File Offset: 0x0011CE3F
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x0011EC4D File Offset: 0x0011CE4D
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x0011EC5C File Offset: 0x0011CE5C
		private void HandleViewModelPropertyChangeOf_datasource_Root_Visual(string propertyName)
		{
			if (propertyName == "AdditionalArgs")
			{
				this._widget_0.AdditionalArgs = this._datasource_Root_Visual.AdditionalArgs;
				return;
			}
			if (propertyName == "Id")
			{
				this._widget_0.ImageId = this._datasource_Root_Visual.Id;
				return;
			}
			if (propertyName == "ImageTypeCode")
			{
				this._widget_0.ImageTypeCode = this._datasource_Root_Visual.ImageTypeCode;
				return;
			}
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x0011ECD8 File Offset: 0x0011CED8
		private void RefreshDataSource_datasource_Root(GameMenuPartyItemVM newDataSource)
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
				if (this._datasource_Root_Visual != null)
				{
					this._datasource_Root_Visual.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
					this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
					this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
					this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
					this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
					this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
					this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
					this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
					this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
					this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
					this._datasource_Root_Visual = null;
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
				this._datasource_Root_Visual = this._datasource_Root.Visual;
				if (this._datasource_Root_Visual != null)
				{
					this._datasource_Root_Visual.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
					this._widget_0.AdditionalArgs = this._datasource_Root_Visual.AdditionalArgs;
					this._widget_0.ImageId = this._datasource_Root_Visual.Id;
					this._widget_0.ImageTypeCode = this._datasource_Root_Visual.ImageTypeCode;
					this._widget_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0;
					this._widget_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0;
					this._widget_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0;
					this._widget_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0;
					this._widget_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0;
					this._widget_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0;
					this._widget_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0;
					this._widget_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0;
					this._widget_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0;
				}
			}
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x0011F254 File Offset: 0x0011D454
		private void RefreshDataSource_datasource_Root_Visual(ImageIdentifierVM newDataSource)
		{
			if (this._datasource_Root_Visual != null)
			{
				this._datasource_Root_Visual.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
				this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
				this._datasource_Root_Visual = null;
			}
			this._datasource_Root_Visual = newDataSource;
			this._datasource_Root_Visual = this._datasource_Root.Visual;
			if (this._datasource_Root_Visual != null)
			{
				this._datasource_Root_Visual.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
				this._widget_0.AdditionalArgs = this._datasource_Root_Visual.AdditionalArgs;
				this._widget_0.ImageId = this._datasource_Root_Visual.Id;
				this._widget_0.ImageTypeCode = this._datasource_Root_Visual.ImageTypeCode;
				this._widget_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0;
			}
		}

		// Token: 0x040007B0 RID: 1968
		private Widget _widget;

		// Token: 0x040007B1 RID: 1969
		private ImageIdentifierWidget _widget_0;

		// Token: 0x040007B2 RID: 1970
		private GameMenuPartyItemVM _datasource_Root;

		// Token: 0x040007B3 RID: 1971
		private ImageIdentifierVM _datasource_Root_Visual;
	}
}
