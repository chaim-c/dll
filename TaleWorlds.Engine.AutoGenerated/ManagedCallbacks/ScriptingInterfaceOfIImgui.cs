using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000014 RID: 20
	internal class ScriptingInterfaceOfIImgui : IImgui
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000F990 File Offset: 0x0000DB90
		public void Begin(string text)
		{
			byte[] array = null;
			if (text != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(text);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(text, 0, text.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIImgui.call_BeginDelegate(array);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000F9EA File Offset: 0x0000DBEA
		public void BeginMainThreadScope()
		{
			ScriptingInterfaceOfIImgui.call_BeginMainThreadScopeDelegate();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		public void BeginWithCloseButton(string text, ref bool is_open)
		{
			byte[] array = null;
			if (text != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(text);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(text, 0, text.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIImgui.call_BeginWithCloseButtonDelegate(array, ref is_open);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000FA54 File Offset: 0x0000DC54
		public bool Button(string text)
		{
			byte[] array = null;
			if (text != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(text);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(text, 0, text.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_ButtonDelegate(array);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		public bool Checkbox(string text, ref bool is_checked)
		{
			byte[] array = null;
			if (text != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(text);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(text, 0, text.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_CheckboxDelegate(array, ref is_checked);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000FB0C File Offset: 0x0000DD0C
		public bool CollapsingHeader(string label)
		{
			byte[] array = null;
			if (label != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(label);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(label, 0, label.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_CollapsingHeaderDelegate(array);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000FB68 File Offset: 0x0000DD68
		public void Columns(int count, string id, bool border)
		{
			byte[] array = null;
			if (id != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(id);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(id, 0, id.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIImgui.call_ColumnsDelegate(count, array, border);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
		public bool Combo(string label, ref int selectedIndex, string items)
		{
			byte[] array = null;
			if (label != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(label);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(label, 0, label.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (items != null)
			{
				int byteCount2 = ScriptingInterfaceOfIImgui._utf8.GetByteCount(items);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(items, 0, items.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_ComboDelegate(array, ref selectedIndex, array2);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000FC62 File Offset: 0x0000DE62
		public void End()
		{
			ScriptingInterfaceOfIImgui.call_EndDelegate();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000FC6E File Offset: 0x0000DE6E
		public void EndMainThreadScope()
		{
			ScriptingInterfaceOfIImgui.call_EndMainThreadScopeDelegate();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		public bool InputFloat(string label, ref float val, float step, float stepFast, int decimalPrecision)
		{
			byte[] array = null;
			if (label != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(label);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(label, 0, label.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_InputFloatDelegate(array, ref val, step, stepFast, decimalPrecision);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000FCDC File Offset: 0x0000DEDC
		public bool InputFloat2(string label, ref float val0, ref float val1, int decimalPrecision)
		{
			byte[] array = null;
			if (label != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(label);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(label, 0, label.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_InputFloat2Delegate(array, ref val0, ref val1, decimalPrecision);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000FD3C File Offset: 0x0000DF3C
		public bool InputFloat3(string label, ref float val0, ref float val1, ref float val2, int decimalPrecision)
		{
			byte[] array = null;
			if (label != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(label);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(label, 0, label.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_InputFloat3Delegate(array, ref val0, ref val1, ref val2, decimalPrecision);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000FD9C File Offset: 0x0000DF9C
		public bool InputFloat4(string label, ref float val0, ref float val1, ref float val2, ref float val3, int decimalPrecision)
		{
			byte[] array = null;
			if (label != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(label);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(label, 0, label.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_InputFloat4Delegate(array, ref val0, ref val1, ref val2, ref val3, decimalPrecision);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000FE00 File Offset: 0x0000E000
		public bool InputInt(string label, ref int value)
		{
			byte[] array = null;
			if (label != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(label);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(label, 0, label.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_InputIntDelegate(array, ref value);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000FE5B File Offset: 0x0000E05B
		public bool IsItemHovered()
		{
			return ScriptingInterfaceOfIImgui.call_IsItemHoveredDelegate();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000FE67 File Offset: 0x0000E067
		public void NewFrame()
		{
			ScriptingInterfaceOfIImgui.call_NewFrameDelegate();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000FE73 File Offset: 0x0000E073
		public void NewLine()
		{
			ScriptingInterfaceOfIImgui.call_NewLineDelegate();
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000FE7F File Offset: 0x0000E07F
		public void NextColumn()
		{
			ScriptingInterfaceOfIImgui.call_NextColumnDelegate();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000FE8C File Offset: 0x0000E08C
		public void PlotLines(string name, float[] values, int valuesCount, int valuesOffset, string overlayText, float minScale, float maxScale, float graphWidth, float graphHeight, int stride)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			PinnedArrayData<float> pinnedArrayData = new PinnedArrayData<float>(values, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			byte[] array2 = null;
			if (overlayText != null)
			{
				int byteCount2 = ScriptingInterfaceOfIImgui._utf8.GetByteCount(overlayText);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(overlayText, 0, overlayText.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIImgui.call_PlotLinesDelegate(array, pointer, valuesCount, valuesOffset, array2, minScale, maxScale, graphWidth, graphHeight, stride);
			pinnedArrayData.Dispose();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000FF5B File Offset: 0x0000E15B
		public void PopStyleColor()
		{
			ScriptingInterfaceOfIImgui.call_PopStyleColorDelegate();
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000FF67 File Offset: 0x0000E167
		public void ProgressBar(float value)
		{
			ScriptingInterfaceOfIImgui.call_ProgressBarDelegate(value);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000FF74 File Offset: 0x0000E174
		public void PushStyleColor(int style, ref Vec3 color)
		{
			ScriptingInterfaceOfIImgui.call_PushStyleColorDelegate(style, ref color);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000FF84 File Offset: 0x0000E184
		public bool RadioButton(string label, bool active)
		{
			byte[] array = null;
			if (label != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(label);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(label, 0, label.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_RadioButtonDelegate(array, active);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000FFDF File Offset: 0x0000E1DF
		public void Render()
		{
			ScriptingInterfaceOfIImgui.call_RenderDelegate();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000FFEB File Offset: 0x0000E1EB
		public void SameLine(float posX, float spacingWidth)
		{
			ScriptingInterfaceOfIImgui.call_SameLineDelegate(posX, spacingWidth);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000FFF9 File Offset: 0x0000E1F9
		public void Separator()
		{
			ScriptingInterfaceOfIImgui.call_SeparatorDelegate();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00010008 File Offset: 0x0000E208
		public void SetTooltip(string label)
		{
			byte[] array = null;
			if (label != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(label);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(label, 0, label.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIImgui.call_SetTooltipDelegate(array);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00010064 File Offset: 0x0000E264
		public bool SliderFloat(string label, ref float value, float min, float max)
		{
			byte[] array = null;
			if (label != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(label);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(label, 0, label.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_SliderFloatDelegate(array, ref value, min, max);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000100C4 File Offset: 0x0000E2C4
		public bool SmallButton(string label)
		{
			byte[] array = null;
			if (label != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(label);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(label, 0, label.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_SmallButtonDelegate(array);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00010120 File Offset: 0x0000E320
		public void Text(string text)
		{
			byte[] array = null;
			if (text != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(text);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(text, 0, text.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIImgui.call_TextDelegate(array);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0001017C File Offset: 0x0000E37C
		public bool TreeNode(string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIImgui._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIImgui._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIImgui.call_TreeNodeDelegate(array);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000101D6 File Offset: 0x0000E3D6
		public void TreePop()
		{
			ScriptingInterfaceOfIImgui.call_TreePopDelegate();
		}

		// Token: 0x04000167 RID: 359
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000168 RID: 360
		public static ScriptingInterfaceOfIImgui.BeginDelegate call_BeginDelegate;

		// Token: 0x04000169 RID: 361
		public static ScriptingInterfaceOfIImgui.BeginMainThreadScopeDelegate call_BeginMainThreadScopeDelegate;

		// Token: 0x0400016A RID: 362
		public static ScriptingInterfaceOfIImgui.BeginWithCloseButtonDelegate call_BeginWithCloseButtonDelegate;

		// Token: 0x0400016B RID: 363
		public static ScriptingInterfaceOfIImgui.ButtonDelegate call_ButtonDelegate;

		// Token: 0x0400016C RID: 364
		public static ScriptingInterfaceOfIImgui.CheckboxDelegate call_CheckboxDelegate;

		// Token: 0x0400016D RID: 365
		public static ScriptingInterfaceOfIImgui.CollapsingHeaderDelegate call_CollapsingHeaderDelegate;

		// Token: 0x0400016E RID: 366
		public static ScriptingInterfaceOfIImgui.ColumnsDelegate call_ColumnsDelegate;

		// Token: 0x0400016F RID: 367
		public static ScriptingInterfaceOfIImgui.ComboDelegate call_ComboDelegate;

		// Token: 0x04000170 RID: 368
		public static ScriptingInterfaceOfIImgui.EndDelegate call_EndDelegate;

		// Token: 0x04000171 RID: 369
		public static ScriptingInterfaceOfIImgui.EndMainThreadScopeDelegate call_EndMainThreadScopeDelegate;

		// Token: 0x04000172 RID: 370
		public static ScriptingInterfaceOfIImgui.InputFloatDelegate call_InputFloatDelegate;

		// Token: 0x04000173 RID: 371
		public static ScriptingInterfaceOfIImgui.InputFloat2Delegate call_InputFloat2Delegate;

		// Token: 0x04000174 RID: 372
		public static ScriptingInterfaceOfIImgui.InputFloat3Delegate call_InputFloat3Delegate;

		// Token: 0x04000175 RID: 373
		public static ScriptingInterfaceOfIImgui.InputFloat4Delegate call_InputFloat4Delegate;

		// Token: 0x04000176 RID: 374
		public static ScriptingInterfaceOfIImgui.InputIntDelegate call_InputIntDelegate;

		// Token: 0x04000177 RID: 375
		public static ScriptingInterfaceOfIImgui.IsItemHoveredDelegate call_IsItemHoveredDelegate;

		// Token: 0x04000178 RID: 376
		public static ScriptingInterfaceOfIImgui.NewFrameDelegate call_NewFrameDelegate;

		// Token: 0x04000179 RID: 377
		public static ScriptingInterfaceOfIImgui.NewLineDelegate call_NewLineDelegate;

		// Token: 0x0400017A RID: 378
		public static ScriptingInterfaceOfIImgui.NextColumnDelegate call_NextColumnDelegate;

		// Token: 0x0400017B RID: 379
		public static ScriptingInterfaceOfIImgui.PlotLinesDelegate call_PlotLinesDelegate;

		// Token: 0x0400017C RID: 380
		public static ScriptingInterfaceOfIImgui.PopStyleColorDelegate call_PopStyleColorDelegate;

		// Token: 0x0400017D RID: 381
		public static ScriptingInterfaceOfIImgui.ProgressBarDelegate call_ProgressBarDelegate;

		// Token: 0x0400017E RID: 382
		public static ScriptingInterfaceOfIImgui.PushStyleColorDelegate call_PushStyleColorDelegate;

		// Token: 0x0400017F RID: 383
		public static ScriptingInterfaceOfIImgui.RadioButtonDelegate call_RadioButtonDelegate;

		// Token: 0x04000180 RID: 384
		public static ScriptingInterfaceOfIImgui.RenderDelegate call_RenderDelegate;

		// Token: 0x04000181 RID: 385
		public static ScriptingInterfaceOfIImgui.SameLineDelegate call_SameLineDelegate;

		// Token: 0x04000182 RID: 386
		public static ScriptingInterfaceOfIImgui.SeparatorDelegate call_SeparatorDelegate;

		// Token: 0x04000183 RID: 387
		public static ScriptingInterfaceOfIImgui.SetTooltipDelegate call_SetTooltipDelegate;

		// Token: 0x04000184 RID: 388
		public static ScriptingInterfaceOfIImgui.SliderFloatDelegate call_SliderFloatDelegate;

		// Token: 0x04000185 RID: 389
		public static ScriptingInterfaceOfIImgui.SmallButtonDelegate call_SmallButtonDelegate;

		// Token: 0x04000186 RID: 390
		public static ScriptingInterfaceOfIImgui.TextDelegate call_TextDelegate;

		// Token: 0x04000187 RID: 391
		public static ScriptingInterfaceOfIImgui.TreeNodeDelegate call_TreeNodeDelegate;

		// Token: 0x04000188 RID: 392
		public static ScriptingInterfaceOfIImgui.TreePopDelegate call_TreePopDelegate;

		// Token: 0x020001CD RID: 461
		// (Invoke) Token: 0x06000C37 RID: 3127
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void BeginDelegate(byte[] text);

		// Token: 0x020001CE RID: 462
		// (Invoke) Token: 0x06000C3B RID: 3131
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void BeginMainThreadScopeDelegate();

		// Token: 0x020001CF RID: 463
		// (Invoke) Token: 0x06000C3F RID: 3135
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void BeginWithCloseButtonDelegate(byte[] text, [MarshalAs(UnmanagedType.U1)] ref bool is_open);

		// Token: 0x020001D0 RID: 464
		// (Invoke) Token: 0x06000C43 RID: 3139
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ButtonDelegate(byte[] text);

		// Token: 0x020001D1 RID: 465
		// (Invoke) Token: 0x06000C47 RID: 3143
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CheckboxDelegate(byte[] text, [MarshalAs(UnmanagedType.U1)] ref bool is_checked);

		// Token: 0x020001D2 RID: 466
		// (Invoke) Token: 0x06000C4B RID: 3147
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CollapsingHeaderDelegate(byte[] label);

		// Token: 0x020001D3 RID: 467
		// (Invoke) Token: 0x06000C4F RID: 3151
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ColumnsDelegate(int count, byte[] id, [MarshalAs(UnmanagedType.U1)] bool border);

		// Token: 0x020001D4 RID: 468
		// (Invoke) Token: 0x06000C53 RID: 3155
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ComboDelegate(byte[] label, ref int selectedIndex, byte[] items);

		// Token: 0x020001D5 RID: 469
		// (Invoke) Token: 0x06000C57 RID: 3159
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EndDelegate();

		// Token: 0x020001D6 RID: 470
		// (Invoke) Token: 0x06000C5B RID: 3163
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EndMainThreadScopeDelegate();

		// Token: 0x020001D7 RID: 471
		// (Invoke) Token: 0x06000C5F RID: 3167
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool InputFloatDelegate(byte[] label, ref float val, float step, float stepFast, int decimalPrecision);

		// Token: 0x020001D8 RID: 472
		// (Invoke) Token: 0x06000C63 RID: 3171
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool InputFloat2Delegate(byte[] label, ref float val0, ref float val1, int decimalPrecision);

		// Token: 0x020001D9 RID: 473
		// (Invoke) Token: 0x06000C67 RID: 3175
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool InputFloat3Delegate(byte[] label, ref float val0, ref float val1, ref float val2, int decimalPrecision);

		// Token: 0x020001DA RID: 474
		// (Invoke) Token: 0x06000C6B RID: 3179
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool InputFloat4Delegate(byte[] label, ref float val0, ref float val1, ref float val2, ref float val3, int decimalPrecision);

		// Token: 0x020001DB RID: 475
		// (Invoke) Token: 0x06000C6F RID: 3183
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool InputIntDelegate(byte[] label, ref int value);

		// Token: 0x020001DC RID: 476
		// (Invoke) Token: 0x06000C73 RID: 3187
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsItemHoveredDelegate();

		// Token: 0x020001DD RID: 477
		// (Invoke) Token: 0x06000C77 RID: 3191
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void NewFrameDelegate();

		// Token: 0x020001DE RID: 478
		// (Invoke) Token: 0x06000C7B RID: 3195
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void NewLineDelegate();

		// Token: 0x020001DF RID: 479
		// (Invoke) Token: 0x06000C7F RID: 3199
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void NextColumnDelegate();

		// Token: 0x020001E0 RID: 480
		// (Invoke) Token: 0x06000C83 RID: 3203
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PlotLinesDelegate(byte[] name, IntPtr values, int valuesCount, int valuesOffset, byte[] overlayText, float minScale, float maxScale, float graphWidth, float graphHeight, int stride);

		// Token: 0x020001E1 RID: 481
		// (Invoke) Token: 0x06000C87 RID: 3207
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PopStyleColorDelegate();

		// Token: 0x020001E2 RID: 482
		// (Invoke) Token: 0x06000C8B RID: 3211
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ProgressBarDelegate(float value);

		// Token: 0x020001E3 RID: 483
		// (Invoke) Token: 0x06000C8F RID: 3215
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PushStyleColorDelegate(int style, ref Vec3 color);

		// Token: 0x020001E4 RID: 484
		// (Invoke) Token: 0x06000C93 RID: 3219
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool RadioButtonDelegate(byte[] label, [MarshalAs(UnmanagedType.U1)] bool active);

		// Token: 0x020001E5 RID: 485
		// (Invoke) Token: 0x06000C97 RID: 3223
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDelegate();

		// Token: 0x020001E6 RID: 486
		// (Invoke) Token: 0x06000C9B RID: 3227
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SameLineDelegate(float posX, float spacingWidth);

		// Token: 0x020001E7 RID: 487
		// (Invoke) Token: 0x06000C9F RID: 3231
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SeparatorDelegate();

		// Token: 0x020001E8 RID: 488
		// (Invoke) Token: 0x06000CA3 RID: 3235
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTooltipDelegate(byte[] label);

		// Token: 0x020001E9 RID: 489
		// (Invoke) Token: 0x06000CA7 RID: 3239
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool SliderFloatDelegate(byte[] label, ref float value, float min, float max);

		// Token: 0x020001EA RID: 490
		// (Invoke) Token: 0x06000CAB RID: 3243
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool SmallButtonDelegate(byte[] label);

		// Token: 0x020001EB RID: 491
		// (Invoke) Token: 0x06000CAF RID: 3247
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TextDelegate(byte[] text);

		// Token: 0x020001EC RID: 492
		// (Invoke) Token: 0x06000CB3 RID: 3251
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool TreeNodeDelegate(byte[] name);

		// Token: 0x020001ED RID: 493
		// (Invoke) Token: 0x06000CB7 RID: 3255
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TreePopDelegate();
	}
}
