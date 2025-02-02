using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Information
{
	// Token: 0x02000019 RID: 25
	public class PropertyBasedTooltipVM : TooltipBaseVM
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00004423 File Offset: 0x00002623
		public PropertyBasedTooltipVM(Type invokedType, object[] invokedArgs) : base(invokedType, invokedArgs)
		{
			this.TooltipPropertyList = new MBBindingList<TooltipProperty>();
			this._isPeriodicRefreshEnabled = true;
			this._periodicRefreshDelay = 2f;
			this.Refresh();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004450 File Offset: 0x00002650
		protected override void OnFinalizeInternal()
		{
			base.IsActive = false;
			this.TooltipPropertyList.Clear();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004464 File Offset: 0x00002664
		public static void AddKeyType(string keyID, Func<string> getKeyText)
		{
			PropertyBasedTooltipVM._keyTextGetters.Add(keyID, getKeyText);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004472 File Offset: 0x00002672
		public string GetKeyText(string keyID)
		{
			if (PropertyBasedTooltipVM._keyTextGetters.ContainsKey(keyID))
			{
				return PropertyBasedTooltipVM._keyTextGetters[keyID]();
			}
			return "";
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004498 File Offset: 0x00002698
		protected override void OnPeriodicRefresh()
		{
			base.OnPeriodicRefresh();
			foreach (TooltipProperty tooltipProperty in this.TooltipPropertyList)
			{
				tooltipProperty.RefreshDefinition();
				tooltipProperty.RefreshValue();
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000044F0 File Offset: 0x000026F0
		protected override void OnIsExtendedChanged()
		{
			if (base.IsActive)
			{
				base.IsActive = false;
				this.TooltipPropertyList.Clear();
				this.Refresh();
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004512 File Offset: 0x00002712
		private void Refresh()
		{
			base.InvokeRefreshData<PropertyBasedTooltipVM>(this);
			if (this.TooltipPropertyList.Count > 0)
			{
				base.IsActive = true;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004530 File Offset: 0x00002730
		public static void RefreshGenericPropertyBasedTooltip(PropertyBasedTooltipVM propertyBasedTooltip, object[] args)
		{
			IEnumerable<TooltipProperty> source = args[0] as List<TooltipProperty>;
			propertyBasedTooltip.Mode = 0;
			Func<TooltipProperty, bool> <>9__0;
			Func<TooltipProperty, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((TooltipProperty p) => (p.OnlyShowWhenExtended && propertyBasedTooltip.IsExtended) || (!p.OnlyShowWhenExtended && p.OnlyShowWhenNotExtended && !propertyBasedTooltip.IsExtended) || (!p.OnlyShowWhenExtended && !p.OnlyShowWhenNotExtended)));
			}
			foreach (TooltipProperty property in source.Where(predicate))
			{
				propertyBasedTooltip.AddPropertyDuplicate(property);
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000045C4 File Offset: 0x000027C4
		public void AddProperty(string definition, string value, int textHeight = 0, TooltipProperty.TooltipPropertyFlags propertyFlags = TooltipProperty.TooltipPropertyFlags.None)
		{
			TooltipProperty item = new TooltipProperty(definition, value, textHeight, false, propertyFlags);
			this.TooltipPropertyList.Add(item);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000045EC File Offset: 0x000027EC
		public void AddModifierProperty(string definition, int modifierValue, int textHeight = 0, TooltipProperty.TooltipPropertyFlags propertyFlags = TooltipProperty.TooltipPropertyFlags.None)
		{
			string value = (modifierValue > 0) ? ("+" + modifierValue.ToString()) : modifierValue.ToString();
			TooltipProperty item = new TooltipProperty(definition, value, textHeight, false, propertyFlags);
			this.TooltipPropertyList.Add(item);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004630 File Offset: 0x00002830
		public void AddProperty(string definition, Func<string> value, int textHeight = 0, TooltipProperty.TooltipPropertyFlags propertyFlags = TooltipProperty.TooltipPropertyFlags.None)
		{
			TooltipProperty item = new TooltipProperty(definition, value, textHeight, false, propertyFlags);
			this.TooltipPropertyList.Add(item);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004658 File Offset: 0x00002858
		public void AddProperty(Func<string> definition, Func<string> value, int textHeight = 0, TooltipProperty.TooltipPropertyFlags propertyFlags = TooltipProperty.TooltipPropertyFlags.None)
		{
			TooltipProperty item = new TooltipProperty(definition, value, textHeight, false, propertyFlags);
			this.TooltipPropertyList.Add(item);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004680 File Offset: 0x00002880
		public void AddColoredProperty(string definition, string value, Color color, int textHeight = 0, TooltipProperty.TooltipPropertyFlags propertyFlags = TooltipProperty.TooltipPropertyFlags.None)
		{
			if (color == Colors.Black)
			{
				this.AddProperty(definition, value, textHeight, TooltipProperty.TooltipPropertyFlags.None);
				return;
			}
			TooltipProperty item = new TooltipProperty(definition, value, textHeight, color, false, propertyFlags);
			this.TooltipPropertyList.Add(item);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000046C0 File Offset: 0x000028C0
		public void AddColoredProperty(string definition, Func<string> value, Color color, int textHeight = 0, TooltipProperty.TooltipPropertyFlags propertyFlags = TooltipProperty.TooltipPropertyFlags.None)
		{
			if (color == Colors.Black)
			{
				this.AddProperty(definition, value, textHeight, TooltipProperty.TooltipPropertyFlags.None);
				return;
			}
			TooltipProperty item = new TooltipProperty(definition, value, textHeight, color, false, propertyFlags);
			this.TooltipPropertyList.Add(item);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004700 File Offset: 0x00002900
		public void AddColoredProperty(Func<string> definition, Func<string> value, Color color, int textHeight = 0, TooltipProperty.TooltipPropertyFlags propertyFlags = TooltipProperty.TooltipPropertyFlags.None)
		{
			if (color == Colors.Black)
			{
				this.AddProperty(definition, value, textHeight, TooltipProperty.TooltipPropertyFlags.None);
				return;
			}
			TooltipProperty item = new TooltipProperty(definition, value, textHeight, color, false, propertyFlags);
			this.TooltipPropertyList.Add(item);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004740 File Offset: 0x00002940
		private void AddPropertyDuplicate(TooltipProperty property)
		{
			TooltipProperty item = new TooltipProperty(property);
			this.TooltipPropertyList.Add(item);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00004760 File Offset: 0x00002960
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00004768 File Offset: 0x00002968
		[DataSourceProperty]
		public MBBindingList<TooltipProperty> TooltipPropertyList
		{
			get
			{
				return this._tooltipPropertyList;
			}
			set
			{
				if (value != this._tooltipPropertyList)
				{
					this._tooltipPropertyList = value;
					base.OnPropertyChangedWithValue<MBBindingList<TooltipProperty>>(value, "TooltipPropertyList");
				}
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004786 File Offset: 0x00002986
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000478E File Offset: 0x0000298E
		[DataSourceProperty]
		public int Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				if (value != this._mode)
				{
					this._mode = value;
					base.OnPropertyChangedWithValue(value, "Mode");
				}
			}
		}

		// Token: 0x0400007A RID: 122
		private static Dictionary<string, Func<string>> _keyTextGetters = new Dictionary<string, Func<string>>();

		// Token: 0x0400007B RID: 123
		private MBBindingList<TooltipProperty> _tooltipPropertyList;

		// Token: 0x0400007C RID: 124
		private int _mode;

		// Token: 0x0200002C RID: 44
		public enum TooltipMode
		{
			// Token: 0x040000CE RID: 206
			DefaultGame,
			// Token: 0x040000CF RID: 207
			DefaultCampaign,
			// Token: 0x040000D0 RID: 208
			Ally,
			// Token: 0x040000D1 RID: 209
			Enemy,
			// Token: 0x040000D2 RID: 210
			War
		}
	}
}
