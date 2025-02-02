using System;
using System.Collections.Generic;
using System.Text;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000023 RID: 35
	public static class GauntletExtensions
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000E33C File Offset: 0x0000C53C
		public static void SetGlobalAlphaRecursively(this Widget widget, float alphaFactor)
		{
			widget.SetAlpha(alphaFactor);
			List<Widget> children = widget.Children;
			for (int i = 0; i < children.Count; i++)
			{
				children[i].SetGlobalAlphaRecursively(alphaFactor);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000E378 File Offset: 0x0000C578
		public static void SetAlpha(this Widget widget, float alphaFactor)
		{
			BrushWidget brushWidget;
			if ((brushWidget = (widget as BrushWidget)) != null)
			{
				brushWidget.Brush.GlobalAlphaFactor = alphaFactor;
			}
			TextureWidget textureWidget;
			if ((textureWidget = (widget as TextureWidget)) != null)
			{
				textureWidget.Brush.GlobalAlphaFactor = alphaFactor;
			}
			widget.AlphaFactor = alphaFactor;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		public static void RegisterBrushStatesOfWidget(this Widget widget)
		{
			BrushWidget brushWidget;
			if ((brushWidget = (widget as BrushWidget)) != null)
			{
				foreach (Style style in brushWidget.ReadOnlyBrush.Styles)
				{
					if (!widget.ContainsState(style.Name))
					{
						widget.AddState(style.Name);
					}
				}
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000E430 File Offset: 0x0000C630
		public static string GetFullIDPath(this Widget widget)
		{
			StringBuilder stringBuilder = new StringBuilder(string.IsNullOrEmpty(widget.Id) ? widget.GetType().Name : widget.Id);
			for (Widget parentWidget = widget.ParentWidget; parentWidget != null; parentWidget = parentWidget.ParentWidget)
			{
				stringBuilder.Insert(0, (string.IsNullOrEmpty(parentWidget.Id) ? parentWidget.GetType().Name : parentWidget.Id) + "\\");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000E4B0 File Offset: 0x0000C6B0
		public static void ApplyActionForThisAndAllChildren(this Widget widget, Action<Widget> action)
		{
			action(widget);
			List<Widget> children = widget.Children;
			for (int i = 0; i < children.Count; i++)
			{
				children[i].ApplyActionForThisAndAllChildren(action);
			}
		}
	}
}
