using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TaleWorlds.Library
{
	// Token: 0x0200009F RID: 159
	public abstract class ViewModel : IViewModel, INotifyPropertyChanged
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060005C5 RID: 1477 RVA: 0x0001308A File Offset: 0x0001128A
		// (remove) Token: 0x060005C6 RID: 1478 RVA: 0x000130AB File Offset: 0x000112AB
		public event PropertyChangedEventHandler PropertyChanged
		{
			add
			{
				if (this._eventHandlers == null)
				{
					this._eventHandlers = new List<PropertyChangedEventHandler>();
				}
				this._eventHandlers.Add(value);
			}
			remove
			{
				if (this._eventHandlers != null)
				{
					this._eventHandlers.Remove(value);
				}
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060005C7 RID: 1479 RVA: 0x000130C2 File Offset: 0x000112C2
		// (remove) Token: 0x060005C8 RID: 1480 RVA: 0x000130E3 File Offset: 0x000112E3
		public event PropertyChangedWithValueEventHandler PropertyChangedWithValue
		{
			add
			{
				if (this._eventHandlersWithValue == null)
				{
					this._eventHandlersWithValue = new List<PropertyChangedWithValueEventHandler>();
				}
				this._eventHandlersWithValue.Add(value);
			}
			remove
			{
				if (this._eventHandlersWithValue != null)
				{
					this._eventHandlersWithValue.Remove(value);
				}
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060005C9 RID: 1481 RVA: 0x000130FA File Offset: 0x000112FA
		// (remove) Token: 0x060005CA RID: 1482 RVA: 0x0001311B File Offset: 0x0001131B
		public event PropertyChangedWithBoolValueEventHandler PropertyChangedWithBoolValue
		{
			add
			{
				if (this._eventHandlersWithBoolValue == null)
				{
					this._eventHandlersWithBoolValue = new List<PropertyChangedWithBoolValueEventHandler>();
				}
				this._eventHandlersWithBoolValue.Add(value);
			}
			remove
			{
				if (this._eventHandlersWithBoolValue != null)
				{
					this._eventHandlersWithBoolValue.Remove(value);
				}
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060005CB RID: 1483 RVA: 0x00013132 File Offset: 0x00011332
		// (remove) Token: 0x060005CC RID: 1484 RVA: 0x00013153 File Offset: 0x00011353
		public event PropertyChangedWithIntValueEventHandler PropertyChangedWithIntValue
		{
			add
			{
				if (this._eventHandlersWithIntValue == null)
				{
					this._eventHandlersWithIntValue = new List<PropertyChangedWithIntValueEventHandler>();
				}
				this._eventHandlersWithIntValue.Add(value);
			}
			remove
			{
				if (this._eventHandlersWithIntValue != null)
				{
					this._eventHandlersWithIntValue.Remove(value);
				}
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060005CD RID: 1485 RVA: 0x0001316A File Offset: 0x0001136A
		// (remove) Token: 0x060005CE RID: 1486 RVA: 0x0001318B File Offset: 0x0001138B
		public event PropertyChangedWithFloatValueEventHandler PropertyChangedWithFloatValue
		{
			add
			{
				if (this._eventHandlersWithFloatValue == null)
				{
					this._eventHandlersWithFloatValue = new List<PropertyChangedWithFloatValueEventHandler>();
				}
				this._eventHandlersWithFloatValue.Add(value);
			}
			remove
			{
				if (this._eventHandlersWithFloatValue != null)
				{
					this._eventHandlersWithFloatValue.Remove(value);
				}
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060005CF RID: 1487 RVA: 0x000131A2 File Offset: 0x000113A2
		// (remove) Token: 0x060005D0 RID: 1488 RVA: 0x000131C3 File Offset: 0x000113C3
		public event PropertyChangedWithUIntValueEventHandler PropertyChangedWithUIntValue
		{
			add
			{
				if (this._eventHandlersWithUIntValue == null)
				{
					this._eventHandlersWithUIntValue = new List<PropertyChangedWithUIntValueEventHandler>();
				}
				this._eventHandlersWithUIntValue.Add(value);
			}
			remove
			{
				if (this._eventHandlersWithUIntValue != null)
				{
					this._eventHandlersWithUIntValue.Remove(value);
				}
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060005D1 RID: 1489 RVA: 0x000131DA File Offset: 0x000113DA
		// (remove) Token: 0x060005D2 RID: 1490 RVA: 0x000131FB File Offset: 0x000113FB
		public event PropertyChangedWithColorValueEventHandler PropertyChangedWithColorValue
		{
			add
			{
				if (this._eventHandlersWithColorValue == null)
				{
					this._eventHandlersWithColorValue = new List<PropertyChangedWithColorValueEventHandler>();
				}
				this._eventHandlersWithColorValue.Add(value);
			}
			remove
			{
				if (this._eventHandlersWithColorValue != null)
				{
					this._eventHandlersWithColorValue.Remove(value);
				}
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060005D3 RID: 1491 RVA: 0x00013212 File Offset: 0x00011412
		// (remove) Token: 0x060005D4 RID: 1492 RVA: 0x00013233 File Offset: 0x00011433
		public event PropertyChangedWithDoubleValueEventHandler PropertyChangedWithDoubleValue
		{
			add
			{
				if (this._eventHandlersWithDoubleValue == null)
				{
					this._eventHandlersWithDoubleValue = new List<PropertyChangedWithDoubleValueEventHandler>();
				}
				this._eventHandlersWithDoubleValue.Add(value);
			}
			remove
			{
				if (this._eventHandlersWithDoubleValue != null)
				{
					this._eventHandlersWithDoubleValue.Remove(value);
				}
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060005D5 RID: 1493 RVA: 0x0001324A File Offset: 0x0001144A
		// (remove) Token: 0x060005D6 RID: 1494 RVA: 0x0001326B File Offset: 0x0001146B
		public event PropertyChangedWithVec2ValueEventHandler PropertyChangedWithVec2Value
		{
			add
			{
				if (this._eventHandlersWithVec2Value == null)
				{
					this._eventHandlersWithVec2Value = new List<PropertyChangedWithVec2ValueEventHandler>();
				}
				this._eventHandlersWithVec2Value.Add(value);
			}
			remove
			{
				if (this._eventHandlersWithVec2Value != null)
				{
					this._eventHandlersWithVec2Value.Remove(value);
				}
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00013284 File Offset: 0x00011484
		protected ViewModel()
		{
			this._type = base.GetType();
			ViewModel.DataSourceTypeBindingPropertiesCollection dataSourceTypeBindingPropertiesCollection;
			ViewModel._cachedViewModelProperties.TryGetValue(this._type, out dataSourceTypeBindingPropertiesCollection);
			if (dataSourceTypeBindingPropertiesCollection == null)
			{
				this._propertiesAndMethods = ViewModel.GetPropertiesOfType(this._type);
				ViewModel._cachedViewModelProperties.Add(this._type, this._propertiesAndMethods);
				return;
			}
			this._propertiesAndMethods = dataSourceTypeBindingPropertiesCollection;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000132E8 File Offset: 0x000114E8
		private PropertyInfo GetProperty(string name)
		{
			PropertyInfo result;
			if (this._propertiesAndMethods != null && this._propertiesAndMethods.Properties.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00013315 File Offset: 0x00011515
		protected bool SetField<T>(ref T field, T value, string propertyName)
		{
			if (EqualityComparer<T>.Default.Equals(field, value))
			{
				return false;
			}
			field = value;
			this.OnPropertyChanged(propertyName);
			return true;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001333C File Offset: 0x0001153C
		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (this._eventHandlers != null)
			{
				for (int i = 0; i < this._eventHandlers.Count; i++)
				{
					PropertyChangedEventHandler propertyChangedEventHandler = this._eventHandlers[i];
					PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
					propertyChangedEventHandler(this, e);
				}
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00013384 File Offset: 0x00011584
		public void OnPropertyChangedWithValue<T>(T value, [CallerMemberName] string propertyName = null) where T : class
		{
			if (this._eventHandlersWithValue != null)
			{
				for (int i = 0; i < this._eventHandlersWithValue.Count; i++)
				{
					PropertyChangedWithValueEventHandler propertyChangedWithValueEventHandler = this._eventHandlersWithValue[i];
					PropertyChangedWithValueEventArgs e = new PropertyChangedWithValueEventArgs(propertyName, value);
					propertyChangedWithValueEventHandler(this, e);
				}
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000133D0 File Offset: 0x000115D0
		public void OnPropertyChangedWithValue(bool value, [CallerMemberName] string propertyName = null)
		{
			if (this._eventHandlersWithBoolValue != null)
			{
				for (int i = 0; i < this._eventHandlersWithBoolValue.Count; i++)
				{
					PropertyChangedWithBoolValueEventHandler propertyChangedWithBoolValueEventHandler = this._eventHandlersWithBoolValue[i];
					PropertyChangedWithBoolValueEventArgs e = new PropertyChangedWithBoolValueEventArgs(propertyName, value);
					propertyChangedWithBoolValueEventHandler(this, e);
				}
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00013418 File Offset: 0x00011618
		public void OnPropertyChangedWithValue(int value, [CallerMemberName] string propertyName = null)
		{
			if (this._eventHandlersWithIntValue != null)
			{
				for (int i = 0; i < this._eventHandlersWithIntValue.Count; i++)
				{
					PropertyChangedWithIntValueEventHandler propertyChangedWithIntValueEventHandler = this._eventHandlersWithIntValue[i];
					PropertyChangedWithIntValueEventArgs e = new PropertyChangedWithIntValueEventArgs(propertyName, value);
					propertyChangedWithIntValueEventHandler(this, e);
				}
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00013460 File Offset: 0x00011660
		public void OnPropertyChangedWithValue(float value, [CallerMemberName] string propertyName = null)
		{
			if (this._eventHandlersWithFloatValue != null)
			{
				for (int i = 0; i < this._eventHandlersWithFloatValue.Count; i++)
				{
					PropertyChangedWithFloatValueEventHandler propertyChangedWithFloatValueEventHandler = this._eventHandlersWithFloatValue[i];
					PropertyChangedWithFloatValueEventArgs e = new PropertyChangedWithFloatValueEventArgs(propertyName, value);
					propertyChangedWithFloatValueEventHandler(this, e);
				}
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000134A8 File Offset: 0x000116A8
		public void OnPropertyChangedWithValue(uint value, [CallerMemberName] string propertyName = null)
		{
			if (this._eventHandlersWithUIntValue != null)
			{
				for (int i = 0; i < this._eventHandlersWithUIntValue.Count; i++)
				{
					PropertyChangedWithUIntValueEventHandler propertyChangedWithUIntValueEventHandler = this._eventHandlersWithUIntValue[i];
					PropertyChangedWithUIntValueEventArgs e = new PropertyChangedWithUIntValueEventArgs(propertyName, value);
					propertyChangedWithUIntValueEventHandler(this, e);
				}
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000134F0 File Offset: 0x000116F0
		public void OnPropertyChangedWithValue(Color value, [CallerMemberName] string propertyName = null)
		{
			if (this._eventHandlersWithColorValue != null)
			{
				for (int i = 0; i < this._eventHandlersWithColorValue.Count; i++)
				{
					PropertyChangedWithColorValueEventHandler propertyChangedWithColorValueEventHandler = this._eventHandlersWithColorValue[i];
					PropertyChangedWithColorValueEventArgs e = new PropertyChangedWithColorValueEventArgs(propertyName, value);
					propertyChangedWithColorValueEventHandler(this, e);
				}
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00013538 File Offset: 0x00011738
		public void OnPropertyChangedWithValue(double value, [CallerMemberName] string propertyName = null)
		{
			if (this._eventHandlersWithDoubleValue != null)
			{
				for (int i = 0; i < this._eventHandlersWithDoubleValue.Count; i++)
				{
					PropertyChangedWithDoubleValueEventHandler propertyChangedWithDoubleValueEventHandler = this._eventHandlersWithDoubleValue[i];
					PropertyChangedWithDoubleValueEventArgs e = new PropertyChangedWithDoubleValueEventArgs(propertyName, value);
					propertyChangedWithDoubleValueEventHandler(this, e);
				}
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00013580 File Offset: 0x00011780
		public void OnPropertyChangedWithValue(Vec2 value, [CallerMemberName] string propertyName = null)
		{
			if (this._eventHandlersWithVec2Value != null)
			{
				for (int i = 0; i < this._eventHandlersWithVec2Value.Count; i++)
				{
					PropertyChangedWithVec2ValueEventHandler propertyChangedWithVec2ValueEventHandler = this._eventHandlersWithVec2Value[i];
					PropertyChangedWithVec2ValueEventArgs e = new PropertyChangedWithVec2ValueEventArgs(propertyName, value);
					propertyChangedWithVec2ValueEventHandler(this, e);
				}
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000135C6 File Offset: 0x000117C6
		public object GetViewModelAtPath(BindingPath path, bool isList)
		{
			return this.GetViewModelAtPath(path);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000135D0 File Offset: 0x000117D0
		public object GetViewModelAtPath(BindingPath path)
		{
			BindingPath subPath = path.SubPath;
			if (subPath != null)
			{
				PropertyInfo property = this.GetProperty(subPath.FirstNode);
				if (property != null)
				{
					object obj = property.GetGetMethod().InvokeWithLog(this, null);
					ViewModel viewModel;
					if ((viewModel = (obj as ViewModel)) != null)
					{
						return viewModel.GetViewModelAtPath(subPath);
					}
					if (obj is IMBBindingList)
					{
						return ViewModel.GetChildAtPath(obj as IMBBindingList, subPath);
					}
				}
				return null;
			}
			return this;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001363C File Offset: 0x0001183C
		private static object GetChildAtPath(IMBBindingList bindingList, BindingPath path)
		{
			BindingPath subPath = path.SubPath;
			if (subPath == null)
			{
				return bindingList;
			}
			if (bindingList.Count > 0)
			{
				int num = Convert.ToInt32(subPath.FirstNode);
				if (num >= 0 && num < bindingList.Count)
				{
					object obj = bindingList[num];
					if (obj is ViewModel)
					{
						return (obj as ViewModel).GetViewModelAtPath(subPath);
					}
					if (obj is IMBBindingList)
					{
						return ViewModel.GetChildAtPath(obj as IMBBindingList, subPath);
					}
				}
			}
			return null;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000136B0 File Offset: 0x000118B0
		public object GetPropertyValue(string name, PropertyTypeFeeder propertyTypeFeeder)
		{
			return this.GetPropertyValue(name);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000136BC File Offset: 0x000118BC
		public object GetPropertyValue(string name)
		{
			PropertyInfo property = this.GetProperty(name);
			object result = null;
			if (property != null)
			{
				result = property.GetGetMethod().InvokeWithLog(this, null);
			}
			return result;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000136EC File Offset: 0x000118EC
		public Type GetPropertyType(string name)
		{
			PropertyInfo property = this.GetProperty(name);
			if (property != null)
			{
				return property.PropertyType;
			}
			return null;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00013714 File Offset: 0x00011914
		public void SetPropertyValue(string name, object value)
		{
			PropertyInfo property = this.GetProperty(name);
			if (property != null)
			{
				MethodInfo setMethod = property.GetSetMethod();
				if (setMethod == null)
				{
					return;
				}
				setMethod.InvokeWithLog(this, new object[]
				{
					value
				});
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001374E File Offset: 0x0001194E
		public virtual void OnFinalize()
		{
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00013750 File Offset: 0x00011950
		public void ExecuteCommand(string commandName, object[] parameters)
		{
			MethodInfo methodInfo;
			MethodInfo methodInfo2;
			if (this._propertiesAndMethods != null && this._propertiesAndMethods.Methods.TryGetValue(commandName, out methodInfo))
			{
				methodInfo2 = methodInfo;
			}
			else
			{
				methodInfo2 = this._type.GetMethod(commandName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			}
			if (methodInfo2 != null)
			{
				if (methodInfo2.GetParameters().Length == parameters.Length)
				{
					object[] array = new object[parameters.Length];
					ParameterInfo[] parameters2 = methodInfo2.GetParameters();
					for (int i = 0; i < parameters.Length; i++)
					{
						object obj = parameters[i];
						Type parameterType = parameters2[i].ParameterType;
						array[i] = obj;
						if (obj is string && parameterType != typeof(string))
						{
							object obj2 = ViewModel.ConvertValueTo((string)obj, parameterType);
							array[i] = obj2;
						}
					}
					methodInfo2.InvokeWithLog(this, array);
					return;
				}
				if (methodInfo2.GetParameters().Length == 0)
				{
					methodInfo2.InvokeWithLog(this, null);
				}
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00013830 File Offset: 0x00011A30
		private static object ConvertValueTo(string value, Type parameterType)
		{
			object result = null;
			if (parameterType == typeof(string))
			{
				result = value;
			}
			else if (parameterType == typeof(int))
			{
				result = Convert.ToInt32(value);
			}
			else if (parameterType == typeof(float))
			{
				result = Convert.ToSingle(value);
			}
			return result;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00013894 File Offset: 0x00011A94
		public virtual void RefreshValues()
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00013898 File Offset: 0x00011A98
		public static void CollectPropertiesAndMethods()
		{
			Assembly[] viewModelAssemblies = ViewModel.GetViewModelAssemblies();
			for (int i = 0; i < viewModelAssemblies.Length; i++)
			{
				List<Type> typesSafe = viewModelAssemblies[i].GetTypesSafe(null);
				for (int j = 0; j < typesSafe.Count; j++)
				{
					Type type = typesSafe[j];
					if (typeof(IViewModel).IsAssignableFrom(type) && typeof(IViewModel) != type)
					{
						ViewModel.DataSourceTypeBindingPropertiesCollection propertiesOfType = ViewModel.GetPropertiesOfType(type);
						ViewModel._cachedViewModelProperties[type] = propertiesOfType;
					}
				}
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001391C File Offset: 0x00011B1C
		private static Assembly[] GetViewModelAssemblies()
		{
			List<Assembly> list = new List<Assembly>();
			Assembly assembly = typeof(ViewModel).Assembly;
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			list.Add(assembly);
			foreach (Assembly assembly2 in assemblies)
			{
				if (assembly2 != assembly)
				{
					AssemblyName[] referencedAssemblies = assembly2.GetReferencedAssemblies();
					for (int j = 0; j < referencedAssemblies.Length; j++)
					{
						if (referencedAssemblies[j].ToString() == assembly.GetName().ToString())
						{
							list.Add(assembly2);
							break;
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000139B8 File Offset: 0x00011BB8
		private static ViewModel.DataSourceTypeBindingPropertiesCollection GetPropertiesOfType(Type t)
		{
			string name = t.Name;
			Dictionary<string, PropertyInfo> dictionary = new Dictionary<string, PropertyInfo>();
			Dictionary<string, MethodInfo> dictionary2 = new Dictionary<string, MethodInfo>();
			foreach (PropertyInfo propertyInfo in t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				dictionary.Add(propertyInfo.Name, propertyInfo);
			}
			foreach (MethodInfo methodInfo in t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (!dictionary2.ContainsKey(methodInfo.Name))
				{
					dictionary2.Add(methodInfo.Name, methodInfo);
				}
			}
			return new ViewModel.DataSourceTypeBindingPropertiesCollection(dictionary, dictionary2);
		}

		// Token: 0x040001A7 RID: 423
		public static bool UIDebugMode;

		// Token: 0x040001A8 RID: 424
		private List<PropertyChangedEventHandler> _eventHandlers;

		// Token: 0x040001A9 RID: 425
		private List<PropertyChangedWithValueEventHandler> _eventHandlersWithValue;

		// Token: 0x040001AA RID: 426
		private List<PropertyChangedWithBoolValueEventHandler> _eventHandlersWithBoolValue;

		// Token: 0x040001AB RID: 427
		private List<PropertyChangedWithIntValueEventHandler> _eventHandlersWithIntValue;

		// Token: 0x040001AC RID: 428
		private List<PropertyChangedWithFloatValueEventHandler> _eventHandlersWithFloatValue;

		// Token: 0x040001AD RID: 429
		private List<PropertyChangedWithUIntValueEventHandler> _eventHandlersWithUIntValue;

		// Token: 0x040001AE RID: 430
		private List<PropertyChangedWithColorValueEventHandler> _eventHandlersWithColorValue;

		// Token: 0x040001AF RID: 431
		private List<PropertyChangedWithDoubleValueEventHandler> _eventHandlersWithDoubleValue;

		// Token: 0x040001B0 RID: 432
		private List<PropertyChangedWithVec2ValueEventHandler> _eventHandlersWithVec2Value;

		// Token: 0x040001B1 RID: 433
		private Type _type;

		// Token: 0x040001B2 RID: 434
		private ViewModel.DataSourceTypeBindingPropertiesCollection _propertiesAndMethods;

		// Token: 0x040001B3 RID: 435
		private static Dictionary<Type, ViewModel.DataSourceTypeBindingPropertiesCollection> _cachedViewModelProperties = new Dictionary<Type, ViewModel.DataSourceTypeBindingPropertiesCollection>();

		// Token: 0x020000E5 RID: 229
		public interface IViewModelGetterInterface
		{
			// Token: 0x06000745 RID: 1861
			bool IsValueSynced(string name);

			// Token: 0x06000746 RID: 1862
			Type GetPropertyType(string name);

			// Token: 0x06000747 RID: 1863
			object GetPropertyValue(string name);

			// Token: 0x06000748 RID: 1864
			void OnFinalize();
		}

		// Token: 0x020000E6 RID: 230
		public interface IViewModelSetterInterface
		{
			// Token: 0x06000749 RID: 1865
			void SetPropertyValue(string name, object value);

			// Token: 0x0600074A RID: 1866
			void OnFinalize();
		}

		// Token: 0x020000E7 RID: 231
		private class DataSourceTypeBindingPropertiesCollection
		{
			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x0600074B RID: 1867 RVA: 0x00016D37 File Offset: 0x00014F37
			// (set) Token: 0x0600074C RID: 1868 RVA: 0x00016D3F File Offset: 0x00014F3F
			public Dictionary<string, PropertyInfo> Properties { get; set; }

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x0600074D RID: 1869 RVA: 0x00016D48 File Offset: 0x00014F48
			// (set) Token: 0x0600074E RID: 1870 RVA: 0x00016D50 File Offset: 0x00014F50
			public Dictionary<string, MethodInfo> Methods { get; set; }

			// Token: 0x0600074F RID: 1871 RVA: 0x00016D59 File Offset: 0x00014F59
			public DataSourceTypeBindingPropertiesCollection(Dictionary<string, PropertyInfo> properties, Dictionary<string, MethodInfo> methods)
			{
				this.Properties = properties;
				this.Methods = methods;
			}
		}
	}
}
