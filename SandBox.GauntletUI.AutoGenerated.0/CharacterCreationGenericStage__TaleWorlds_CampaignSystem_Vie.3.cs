﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x02000017 RID: 23
	public class CharacterCreationGenericStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationGenericStageVM_Dependency_2_ItemTemplate : ButtonWidget
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00037D6A File Offset: 0x00035F6A
		public CharacterCreationGenericStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationGenericStageVM_Dependency_2_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00037D73 File Offset: 0x00035F73
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new TextWidget(base.Context);
			this._widget.AddChild(this._widget_0);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00037D9E File Offset: 0x00035F9E
		public void SetIds()
		{
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00037DA0 File Offset: 0x00035FA0
		public void SetAttributes()
		{
			base.DoNotPassEventsToChildren = true;
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			base.MarginBottom = 8f;
			base.Brush = base.Context.GetBrush("FaceGen.Generic.Button");
			base.MinHeight = 10f;
			base.UpdateChildrenStates = true;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.MarginLeft = 10f;
			this._widget_0.MarginRight = 10f;
			this._widget_0.MarginTop = 7f;
			this._widget_0.MarginBottom = 7f;
			this._widget_0.Brush = base.Context.GetBrush("Generic.Button.Text");
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00037E68 File Offset: 0x00036068
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
				this._widget.EventFire -= this.EventListenerOf_widget;
				this._widget.PropertyChanged -= this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged -= this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget;
				this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
				this._datasource_Root = null;
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0003810B File Offset: 0x0003630B
		public void SetDataSource(CharacterCreationOptionVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00038114 File Offset: 0x00036314
		private void EventListenerOf_widget(Widget widget, string commandName, object[] args)
		{
			if (commandName == "Click")
			{
				this._datasource_Root.ExecuteAction();
			}
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0003812E File Offset: 0x0003632E
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00038137 File Offset: 0x00036337
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00038140 File Offset: 0x00036340
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00038149 File Offset: 0x00036349
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00038152 File Offset: 0x00036352
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0003815B File Offset: 0x0003635B
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00038164 File Offset: 0x00036364
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0003816D File Offset: 0x0003636D
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00038176 File Offset: 0x00036376
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0003817F File Offset: 0x0003637F
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "IsSelected")
			{
				this._datasource_Root.IsSelected = this._widget.IsSelected;
				return;
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000381A5 File Offset: 0x000363A5
		private void PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000381AE File Offset: 0x000363AE
		private void boolPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000381B7 File Offset: 0x000363B7
		private void floatPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x000381C0 File Offset: 0x000363C0
		private void Vec2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000381C9 File Offset: 0x000363C9
		private void Vector2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000381D2 File Offset: 0x000363D2
		private void doublePropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000381DB File Offset: 0x000363DB
		private void intPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x000381E4 File Offset: 0x000363E4
		private void uintPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x000381ED File Offset: 0x000363ED
		private void ColorPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000381F6 File Offset: 0x000363F6
		private void HandleWidgetPropertyChangeOf_widget_0(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.ActionText = this._widget_0.Text;
				return;
			}
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0003821C File Offset: 0x0003641C
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0003822A File Offset: 0x0003642A
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00038238 File Offset: 0x00036438
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00038246 File Offset: 0x00036446
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00038254 File Offset: 0x00036454
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00038262 File Offset: 0x00036462
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00038270 File Offset: 0x00036470
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0003827E File Offset: 0x0003647E
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0003828C File Offset: 0x0003648C
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0003829C File Offset: 0x0003649C
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "IsSelected")
			{
				this._widget.IsSelected = this._datasource_Root.IsSelected;
				return;
			}
			if (propertyName == "ActionText")
			{
				this._widget_0.Text = this._datasource_Root.ActionText;
				return;
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000382F4 File Offset: 0x000364F4
		private void RefreshDataSource_datasource_Root(CharacterCreationOptionVM newDataSource)
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
				this._widget.EventFire -= this.EventListenerOf_widget;
				this._widget.PropertyChanged -= this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged -= this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget;
				this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
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
				this._widget.IsSelected = this._datasource_Root.IsSelected;
				this._widget.EventFire += this.EventListenerOf_widget;
				this._widget.PropertyChanged += this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged += this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget;
				this._widget_0.Text = this._datasource_Root.ActionText;
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

		// Token: 0x0400017B RID: 379
		private ButtonWidget _widget;

		// Token: 0x0400017C RID: 380
		private TextWidget _widget_0;

		// Token: 0x0400017D RID: 381
		private CharacterCreationOptionVM _datasource_Root;
	}
}
