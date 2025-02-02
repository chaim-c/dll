using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using TaleWorlds.Diamond.InnerProcess;
using TaleWorlds.Library;
using TaleWorlds.Library.Http;
using TaleWorlds.ServiceDiscovery.Client;

namespace TaleWorlds.Diamond.ClientApplication
{
	// Token: 0x0200005D RID: 93
	public class DiamondClientApplication
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00006240 File Offset: 0x00004440
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00006248 File Offset: 0x00004448
		public ApplicationVersion ApplicationVersion { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00006251 File Offset: 0x00004451
		public ParameterContainer Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00006259 File Offset: 0x00004459
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00006261 File Offset: 0x00004461
		public IReadOnlyDictionary<string, string> ProxyAddressMap { get; private set; }

		// Token: 0x06000233 RID: 563 RVA: 0x0000626C File Offset: 0x0000446C
		public DiamondClientApplication(ApplicationVersion applicationVersion, ParameterContainer parameters)
		{
			this.ApplicationVersion = applicationVersion;
			this._parameters = parameters;
			this._clientApplicationObjects = new Dictionary<string, DiamondClientApplicationObject>();
			this._clientObjects = new Dictionary<string, IClient>();
			this._sessionlessClientObjects = new Dictionary<string, ISessionlessClient>();
			this.ProxyAddressMap = new Dictionary<string, string>();
			ServicePointManager.DefaultConnectionLimit = 1000;
			ServicePointManager.Expect100Continue = false;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000062C9 File Offset: 0x000044C9
		public DiamondClientApplication(ApplicationVersion applicationVersion) : this(applicationVersion, new ParameterContainer())
		{
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000062D8 File Offset: 0x000044D8
		public object GetObject(string name)
		{
			DiamondClientApplicationObject result;
			this._clientApplicationObjects.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000062F5 File Offset: 0x000044F5
		public void AddObject(string name, DiamondClientApplicationObject applicationObject)
		{
			this._clientApplicationObjects.Add(name, applicationObject);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006304 File Offset: 0x00004504
		public void Initialize(ClientApplicationConfiguration applicationConfiguration)
		{
			this._parameters = applicationConfiguration.Parameters;
			foreach (string clientConfiguration in applicationConfiguration.Clients)
			{
				this.CreateClient(clientConfiguration, applicationConfiguration.SessionProviderType);
			}
			foreach (string clientConfiguration2 in applicationConfiguration.SessionlessClients)
			{
				this.CreateSessionlessClient(clientConfiguration2, applicationConfiguration.SessionProviderType);
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000636C File Offset: 0x0000456C
		private void CreateClient(string clientConfiguration, SessionProviderType sessionProviderType)
		{
			Type type = DiamondClientApplication.FindType(clientConfiguration);
			object obj = this.CreateClientSessionProvider(clientConfiguration, type, sessionProviderType, this._parameters);
			IClient value = (IClient)Activator.CreateInstance(type, new object[]
			{
				this,
				obj
			});
			this._clientObjects.Add(clientConfiguration, value);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000063B8 File Offset: 0x000045B8
		private void CreateSessionlessClient(string clientConfiguration, SessionProviderType sessionProviderType)
		{
			Type type = DiamondClientApplication.FindType(clientConfiguration);
			object obj = this.CreateSessionlessClientDriverProvider(clientConfiguration, type, sessionProviderType, this._parameters);
			ISessionlessClient value = (ISessionlessClient)Activator.CreateInstance(type, new object[]
			{
				this,
				obj
			});
			this._sessionlessClientObjects.Add(clientConfiguration, value);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006404 File Offset: 0x00004604
		public object CreateSessionlessClientDriverProvider(string clientName, Type clientType, SessionProviderType sessionProviderType, ParameterContainer parameters)
		{
			if (sessionProviderType == SessionProviderType.Rest || sessionProviderType == SessionProviderType.ThreadedRest)
			{
				Type type = typeof(GenericRestSessionlessClientDriverProvider<>).MakeGenericType(new Type[]
				{
					clientType
				});
				string text;
				parameters.TryGetParameter(clientName + ".Address", out text);
				ushort num;
				parameters.TryGetParameterAsUInt16(clientName + ".Port", out num);
				bool flag;
				parameters.TryGetParameterAsBool(clientName + ".IsSecure", out flag);
				if (ServiceAddress.IsServiceAddress(text))
				{
					string serviceDiscoveryAddress;
					parameters.TryGetParameter(clientName + ".ServiceDiscovery.Address", out serviceDiscoveryAddress);
					ServiceAddressManager.ResolveAddress(serviceDiscoveryAddress, text, ref text, ref num, ref flag);
				}
				string name;
				IHttpDriver httpDriver;
				if (parameters.TryGetParameter(clientName + ".HttpDriver", out name))
				{
					httpDriver = HttpDriverManager.GetHttpDriver(name);
				}
				else
				{
					httpDriver = HttpDriverManager.GetDefaultHttpDriver();
				}
				return Activator.CreateInstance(type, new object[]
				{
					text,
					num,
					flag,
					httpDriver
				});
			}
			throw new NotImplementedException("Other session provider types are not supported yet.");
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00006508 File Offset: 0x00004708
		public object CreateClientSessionProvider(string clientName, Type clientType, SessionProviderType sessionProviderType, ParameterContainer parameters)
		{
			object result;
			if (sessionProviderType == SessionProviderType.Rest || sessionProviderType == SessionProviderType.ThreadedRest)
			{
				Type type = ((sessionProviderType == SessionProviderType.Rest) ? typeof(GenericRestSessionProvider<>) : typeof(GenericThreadedRestSessionProvider<>)).MakeGenericType(new Type[]
				{
					clientType
				});
				string text;
				parameters.TryGetParameter(clientName + ".Address", out text);
				ushort num;
				parameters.TryGetParameterAsUInt16(clientName + ".Port", out num);
				bool flag;
				parameters.TryGetParameterAsBool(clientName + ".IsSecure", out flag);
				if (ServiceAddress.IsServiceAddress(text))
				{
					string serviceDiscoveryAddress;
					parameters.TryGetParameter(clientName + ".ServiceDiscovery.Address", out serviceDiscoveryAddress);
					ServiceAddressManager.ResolveAddress(serviceDiscoveryAddress, text, ref text, ref num, ref flag);
				}
				string text2 = clientName + ".Proxy.";
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (KeyValuePair<string, string> keyValuePair in parameters.Iterator)
				{
					if (keyValuePair.Key.StartsWith(text2) && keyValuePair.Key.Length > text2.Length)
					{
						dictionary[keyValuePair.Key.Substring(text2.Length)] = keyValuePair.Value;
					}
				}
				this.ProxyAddressMap = dictionary;
				string text3;
				if (dictionary.TryGetValue(text, out text3))
				{
					text = text3;
				}
				string name;
				IHttpDriver httpDriver;
				if (parameters.TryGetParameter(clientName + ".HttpDriver", out name))
				{
					httpDriver = HttpDriverManager.GetHttpDriver(name);
				}
				else
				{
					httpDriver = HttpDriverManager.GetDefaultHttpDriver();
				}
				result = Activator.CreateInstance(type, new object[]
				{
					text,
					num,
					flag,
					httpDriver
				});
			}
			else
			{
				if (sessionProviderType != SessionProviderType.InnerProcess)
				{
					throw new NotImplementedException("Other session provider types are not supported yet.");
				}
				InnerProcessManager innerProcessManager = ((InnerProcessManagerClientObject)this.GetObject("InnerProcessManager")).InnerProcessManager;
				Type type2 = typeof(GenericInnerProcessSessionProvider<>).MakeGenericType(new Type[]
				{
					clientType
				});
				ushort num2;
				parameters.TryGetParameterAsUInt16(clientName + ".Port", out num2);
				result = Activator.CreateInstance(type2, new object[]
				{
					innerProcessManager,
					num2
				});
			}
			return result;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00006738 File Offset: 0x00004938
		private static Assembly[] GetDiamondAssemblies()
		{
			List<Assembly> list = new List<Assembly>();
			Assembly assembly = typeof(PeerId).Assembly;
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			list.Add(assembly);
			foreach (Assembly assembly2 in assemblies)
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
			return list.ToArray();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000067C8 File Offset: 0x000049C8
		private static Type FindType(string name)
		{
			Assembly[] diamondAssemblies = DiamondClientApplication.GetDiamondAssemblies();
			Type result = null;
			Assembly[] array = diamondAssemblies;
			for (int i = 0; i < array.Length; i++)
			{
				foreach (Type type in array[i].GetTypesSafe(null))
				{
					if (type.Name == name)
					{
						result = type;
					}
				}
			}
			return result;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006844 File Offset: 0x00004A44
		public T GetClient<T>(string name) where T : class, IClient
		{
			IClient client;
			if (this._clientObjects.TryGetValue(name, out client))
			{
				return client as T;
			}
			return default(T);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006878 File Offset: 0x00004A78
		public T GetSessionlessClient<T>(string name) where T : class, ISessionlessClient
		{
			ISessionlessClient sessionlessClient;
			if (this._sessionlessClientObjects.TryGetValue(name, out sessionlessClient))
			{
				return sessionlessClient as T;
			}
			return default(T);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000068AC File Offset: 0x00004AAC
		public void Update()
		{
			foreach (IClient client in this._clientObjects.Values)
			{
			}
		}

		// Token: 0x040000C9 RID: 201
		private ParameterContainer _parameters;

		// Token: 0x040000CA RID: 202
		private Dictionary<string, DiamondClientApplicationObject> _clientApplicationObjects;

		// Token: 0x040000CB RID: 203
		private Dictionary<string, IClient> _clientObjects;

		// Token: 0x040000CC RID: 204
		private Dictionary<string, ISessionlessClient> _sessionlessClientObjects;
	}
}
