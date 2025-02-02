using System;
using System.Collections.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.Core.ViewModelCollection.Information
{
	// Token: 0x02000013 RID: 19
	public class BasicTooltipViewModel : ViewModel
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00003C0C File Offset: 0x00001E0C
		public BasicTooltipViewModel(Func<string> hintTextDelegate)
		{
			this._hintProperty = hintTextDelegate;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003C1B File Offset: 0x00001E1B
		public BasicTooltipViewModel(Func<List<TooltipProperty>> tooltipPropertiesDelegate)
		{
			this._tooltipProperties = tooltipPropertiesDelegate;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003C2A File Offset: 0x00001E2A
		public BasicTooltipViewModel(Action preBuiltTooltipCallback)
		{
			this._preBuiltTooltipCallback = preBuiltTooltipCallback;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003C39 File Offset: 0x00001E39
		public BasicTooltipViewModel()
		{
			this._hintProperty = null;
			this._tooltipProperties = null;
			this._preBuiltTooltipCallback = null;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003C56 File Offset: 0x00001E56
		public void SetToolipCallback(Func<List<TooltipProperty>> tooltipPropertiesDelegate)
		{
			this._tooltipProperties = tooltipPropertiesDelegate;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003C5F File Offset: 0x00001E5F
		public void SetGenericTooltipCallback(Action preBuiltTooltipCallback)
		{
			this._preBuiltTooltipCallback = preBuiltTooltipCallback;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003C68 File Offset: 0x00001E68
		public void SetHintCallback(Func<string> hintProperty)
		{
			this._hintProperty = hintProperty;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003C74 File Offset: 0x00001E74
		public void ExecuteBeginHint()
		{
			if (this._hintProperty == null && this._tooltipProperties == null && this._preBuiltTooltipCallback == null)
			{
				return;
			}
			if (this._hintProperty != null)
			{
				Func<List<TooltipProperty>> tooltipProperties = this._tooltipProperties;
			}
			if (this._tooltipProperties != null)
			{
				InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[]
				{
					this._tooltipProperties()
				});
				return;
			}
			if (this._hintProperty != null)
			{
				string text = this._hintProperty();
				if (!string.IsNullOrEmpty(text))
				{
					MBInformationManager.ShowHint(text);
					return;
				}
			}
			else if (this._preBuiltTooltipCallback != null)
			{
				this._preBuiltTooltipCallback();
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003D0B File Offset: 0x00001F0B
		public void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x0400005D RID: 93
		private Func<string> _hintProperty;

		// Token: 0x0400005E RID: 94
		private Func<List<TooltipProperty>> _tooltipProperties;

		// Token: 0x0400005F RID: 95
		private Action _preBuiltTooltipCallback;
	}
}
