﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Xml;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.PrefabSystem
{
	// Token: 0x02000018 RID: 24
	public static class WidgetExtensions
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00003160 File Offset: 0x00001360
		private static void GetObjectAndProperty(object parent, string name, int nameStartIndex, out object targetObject, out PropertyInfo targetPropertyInfo)
		{
			int num = name.IndexOf('.', nameStartIndex);
			PropertyInfo property = parent.GetType().GetProperty((num >= 0) ? name.Substring(nameStartIndex, num) : ((nameStartIndex > 0) ? name.Substring(nameStartIndex) : name), BindingFlags.Instance | BindingFlags.Public);
			if (!(property != null))
			{
				targetPropertyInfo = null;
				targetObject = null;
				return;
			}
			if (num < 0)
			{
				targetObject = parent;
				targetPropertyInfo = property;
				return;
			}
			WidgetExtensions.GetObjectAndProperty(property.GetGetMethod().Invoke(parent, new object[0]), name, num + 1, out targetObject, out targetPropertyInfo);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000031DC File Offset: 0x000013DC
		public static void SetWidgetAttributeFromString(object target, string name, string value, BrushFactory brushFactory, SpriteData spriteData, Dictionary<string, VisualDefinitionTemplate> visualDefinitionTemplates, Dictionary<string, ConstantDefinition> constants, Dictionary<string, WidgetAttributeTemplate> parameters, Dictionary<string, XmlElement> customElements, Dictionary<string, string> defaultParameters)
		{
			object obj;
			PropertyInfo propertyInfo;
			WidgetExtensions.GetObjectAndProperty(target, name, 0, out obj, out propertyInfo);
			if (propertyInfo != null)
			{
				if (propertyInfo.PropertyType == typeof(int))
				{
					int num = Convert.ToInt32(value);
					propertyInfo.GetSetMethod().Invoke(obj, new object[]
					{
						num
					});
					return;
				}
				if (propertyInfo.PropertyType == typeof(float))
				{
					float num2 = Convert.ToSingle(value, CultureInfo.InvariantCulture);
					propertyInfo.GetSetMethod().Invoke(obj, new object[]
					{
						num2
					});
					return;
				}
				if (propertyInfo.PropertyType == typeof(bool))
				{
					bool flag = value == "true";
					propertyInfo.GetSetMethod().Invoke(obj, new object[]
					{
						flag
					});
					return;
				}
				if (propertyInfo.PropertyType == typeof(string))
				{
					propertyInfo.GetSetMethod().Invoke(obj, new object[]
					{
						value
					});
					return;
				}
				if (propertyInfo.PropertyType == typeof(Brush))
				{
					if (brushFactory != null)
					{
						Brush brush = brushFactory.GetBrush(value);
						propertyInfo.GetSetMethod().Invoke(obj, new object[]
						{
							brush
						});
						return;
					}
				}
				else if (propertyInfo.PropertyType == typeof(Sprite))
				{
					if (spriteData != null)
					{
						Sprite sprite = spriteData.GetSprite(value);
						propertyInfo.GetSetMethod().Invoke(obj, new object[]
						{
							sprite
						});
						return;
					}
				}
				else
				{
					if (propertyInfo.PropertyType.IsEnum)
					{
						object obj2 = Enum.Parse(propertyInfo.PropertyType, value);
						propertyInfo.GetSetMethod().Invoke(obj, new object[]
						{
							obj2
						});
						return;
					}
					if (propertyInfo.PropertyType == typeof(Color))
					{
						Color color = Color.ConvertStringToColor(value);
						propertyInfo.GetSetMethod().Invoke(obj, new object[]
						{
							color
						});
						return;
					}
					if (propertyInfo.PropertyType == typeof(XmlElement))
					{
						if (customElements != null && customElements.ContainsKey(value))
						{
							XmlElement xmlElement = customElements[value];
							propertyInfo.GetSetMethod().Invoke(obj, new object[]
							{
								xmlElement
							});
							return;
						}
					}
					else if (typeof(Widget).IsAssignableFrom(propertyInfo.PropertyType))
					{
						Widget widget;
						if ((widget = (target as Widget)) != null)
						{
							BindingPath path = new BindingPath(value);
							Widget widget2 = widget.FindChild(path);
							propertyInfo.GetSetMethod().Invoke(obj, new object[]
							{
								widget2
							});
							return;
						}
					}
					else if (propertyInfo.PropertyType == typeof(VisualDefinition) && visualDefinitionTemplates != null)
					{
						VisualDefinition visualDefinition = visualDefinitionTemplates[value].CreateVisualDefinition(brushFactory, spriteData, visualDefinitionTemplates, constants, parameters, defaultParameters);
						propertyInfo.GetSetMethod().Invoke(obj, new object[]
						{
							visualDefinition
						});
					}
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000034C8 File Offset: 0x000016C8
		public static Type GetWidgetAttributeType(object target, string name)
		{
			object obj;
			PropertyInfo propertyInfo;
			WidgetExtensions.GetObjectAndProperty(target, name, 0, out obj, out propertyInfo);
			if (propertyInfo != null)
			{
				return propertyInfo.PropertyType;
			}
			return null;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000034F4 File Offset: 0x000016F4
		public static void SetWidgetAttribute(UIContext context, object target, string name, object value)
		{
			object obj;
			PropertyInfo propertyInfo;
			WidgetExtensions.GetObjectAndProperty(target, name, 0, out obj, out propertyInfo);
			if (propertyInfo != null)
			{
				object obj2 = WidgetExtensions.ConvertObject(context, value, propertyInfo.PropertyType);
				propertyInfo.GetSetMethod().Invoke(obj, new object[]
				{
					obj2
				});
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000353C File Offset: 0x0000173C
		private static object ConvertObject(UIContext context, object input, Type targetType)
		{
			object result = input;
			if (input != null && input.GetType() == typeof(string))
			{
				if (targetType == typeof(Sprite))
				{
					result = context.SpriteData.GetSprite((string)input);
				}
				else if (targetType == typeof(Brush))
				{
					result = context.GetBrush((string)input);
				}
				else if (targetType == typeof(int))
				{
					result = Convert.ToInt32(input);
				}
				else if (targetType == typeof(Color))
				{
					result = Color.ConvertStringToColor((string)input);
				}
			}
			return result;
		}
	}
}
