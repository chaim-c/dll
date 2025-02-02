using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.Core
{
	// Token: 0x020000C6 RID: 198
	public class VirtualPlayer
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0002005E File Offset: 0x0001E25E
		public static Dictionary<Type, object> PeerComponents
		{
			get
			{
				return VirtualPlayer._peerComponents;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00020065 File Offset: 0x0001E265
		static VirtualPlayer()
		{
			VirtualPlayer.FindPeerComponents();
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00020078 File Offset: 0x0001E278
		private static void FindPeerComponents()
		{
			Debug.Print("Searching Peer Components", 0, Debug.DebugColor.White, 17592186044416UL);
			VirtualPlayer._peerComponentIds = new Dictionary<Type, uint>();
			VirtualPlayer._peerComponentTypes = new Dictionary<uint, Type>();
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			List<Type> list = new List<Type>();
			foreach (Assembly assembly in assemblies)
			{
				if (VirtualPlayer.CheckAssemblyForPeerComponent(assembly))
				{
					List<Type> typesSafe = assembly.GetTypesSafe(null);
					list.AddRange(from q in typesSafe
					where typeof(PeerComponent).IsAssignableFrom(q) && typeof(PeerComponent) != q
					select q);
				}
			}
			foreach (Type type in list)
			{
				uint djb = (uint)Common.GetDJB2(type.Name);
				VirtualPlayer._peerComponentIds.Add(type, djb);
				VirtualPlayer._peerComponentTypes.Add(djb, type);
			}
			Debug.Print("Found " + list.Count + " peer components", 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000201A0 File Offset: 0x0001E3A0
		private static bool CheckAssemblyForPeerComponent(Assembly assembly)
		{
			Assembly assembly2 = Assembly.GetAssembly(typeof(PeerComponent));
			if (assembly == assembly2)
			{
				return true;
			}
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			for (int i = 0; i < referencedAssemblies.Length; i++)
			{
				if (referencedAssemblies[i].FullName == assembly2.FullName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000201F5 File Offset: 0x0001E3F5
		private static void EnsurePeerTypeList<T>() where T : PeerComponent
		{
			if (!VirtualPlayer._peerComponents.ContainsKey(typeof(T)))
			{
				VirtualPlayer._peerComponents.Add(typeof(T), new List<T>());
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00020228 File Offset: 0x0001E428
		private static void EnsurePeerTypeList(Type type)
		{
			if (!VirtualPlayer._peerComponents.ContainsKey(type))
			{
				IList value = Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[]
				{
					type
				})) as IList;
				VirtualPlayer._peerComponents.Add(type, value);
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00020272 File Offset: 0x0001E472
		public static List<T> Peers<T>() where T : PeerComponent
		{
			VirtualPlayer.EnsurePeerTypeList<T>();
			return VirtualPlayer._peerComponents[typeof(T)] as List<T>;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00020292 File Offset: 0x0001E492
		public static void Reset()
		{
			VirtualPlayer._peerComponents = new Dictionary<Type, object>();
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0002029E File Offset: 0x0001E49E
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x000202B9 File Offset: 0x0001E4B9
		public string BannerCode
		{
			get
			{
				if (this._bannerCode == null)
				{
					this._bannerCode = "11.8.1.4345.4345.770.774.1.0.0.133.7.5.512.512.784.769.1.0.0";
				}
				return this._bannerCode;
			}
			set
			{
				this._bannerCode = value;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x000202C2 File Offset: 0x0001E4C2
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x000202CA File Offset: 0x0001E4CA
		public BodyProperties BodyProperties { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x000202D3 File Offset: 0x0001E4D3
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x000202DB File Offset: 0x0001E4DB
		public int Race { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x000202E4 File Offset: 0x0001E4E4
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x000202EC File Offset: 0x0001E4EC
		public bool IsFemale { get; set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000202F5 File Offset: 0x0001E4F5
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x000202FD File Offset: 0x0001E4FD
		public PlayerId Id { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x00020306 File Offset: 0x0001E506
		// (set) Token: 0x060009C4 RID: 2500 RVA: 0x0002030E File Offset: 0x0001E50E
		public int Index { get; private set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00020317 File Offset: 0x0001E517
		public bool IsMine
		{
			get
			{
				return MBNetwork.MyPeer == this;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00020321 File Offset: 0x0001E521
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x00020329 File Offset: 0x0001E529
		public string UserName { get; private set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00020332 File Offset: 0x0001E532
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x0002033A File Offset: 0x0001E53A
		public int ChosenBadgeIndex { get; set; }

		// Token: 0x060009CA RID: 2506 RVA: 0x00020343 File Offset: 0x0001E543
		public VirtualPlayer(int index, string name, PlayerId playerID, ICommunicator communicator)
		{
			this._peerEntitySystem = new EntitySystem<PeerComponent>();
			this.UserName = name;
			this.Index = index;
			this.Id = playerID;
			this.Communicator = communicator;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00020374 File Offset: 0x0001E574
		public T AddComponent<T>() where T : PeerComponent, new()
		{
			T t = this._peerEntitySystem.AddComponent<T>();
			t.Peer = this;
			t.TypeId = VirtualPlayer._peerComponentIds[typeof(T)];
			VirtualPlayer.EnsurePeerTypeList<T>();
			(VirtualPlayer._peerComponents[typeof(T)] as List<T>).Add(t);
			this.Communicator.OnAddComponent(t);
			t.Initialize();
			return t;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000203FC File Offset: 0x0001E5FC
		public PeerComponent AddComponent(Type peerComponentType)
		{
			PeerComponent peerComponent = this._peerEntitySystem.AddComponent(peerComponentType);
			peerComponent.Peer = this;
			peerComponent.TypeId = VirtualPlayer._peerComponentIds[peerComponentType];
			VirtualPlayer.EnsurePeerTypeList(peerComponentType);
			(VirtualPlayer._peerComponents[peerComponentType] as IList).Add(peerComponent);
			this.Communicator.OnAddComponent(peerComponent);
			peerComponent.Initialize();
			return peerComponent;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0002045E File Offset: 0x0001E65E
		public PeerComponent AddComponent(uint componentId)
		{
			return this.AddComponent(VirtualPlayer._peerComponentTypes[componentId]);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00020471 File Offset: 0x0001E671
		public PeerComponent GetComponent(uint componentId)
		{
			return this.GetComponent(VirtualPlayer._peerComponentTypes[componentId]);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00020484 File Offset: 0x0001E684
		public T GetComponent<T>() where T : PeerComponent
		{
			return this._peerEntitySystem.GetComponent<T>();
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00020491 File Offset: 0x0001E691
		public PeerComponent GetComponent(Type peerComponentType)
		{
			return this._peerEntitySystem.GetComponent(peerComponentType);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000204A0 File Offset: 0x0001E6A0
		public void RemoveComponent<T>(bool synched = true) where T : PeerComponent
		{
			T component = this._peerEntitySystem.GetComponent<T>();
			if (component != null)
			{
				this._peerEntitySystem.RemoveComponent(component);
				(VirtualPlayer._peerComponents[typeof(T)] as List<T>).Remove(component);
				if (synched)
				{
					this.Communicator.OnRemoveComponent(component);
				}
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00020506 File Offset: 0x0001E706
		public void RemoveComponent(PeerComponent component)
		{
			this._peerEntitySystem.RemoveComponent(component);
			(VirtualPlayer._peerComponents[component.GetType()] as IList).Remove(component);
			this.Communicator.OnRemoveComponent(component);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0002053B File Offset: 0x0001E73B
		public void OnDisconnect()
		{
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00020540 File Offset: 0x0001E740
		public void SynchronizeComponentsTo(VirtualPlayer peer)
		{
			foreach (PeerComponent component in this._peerEntitySystem.Components)
			{
				this.Communicator.OnSynchronizeComponentTo(peer, component);
			}
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000205A0 File Offset: 0x0001E7A0
		public void UpdateIndexForReconnectingPlayer(int playerIndex)
		{
			this.Index = playerIndex;
		}

		// Token: 0x040005BD RID: 1469
		private const string DefaultPlayerBannerCode = "11.8.1.4345.4345.770.774.1.0.0.133.7.5.512.512.784.769.1.0.0";

		// Token: 0x040005BE RID: 1470
		private static Dictionary<Type, object> _peerComponents = new Dictionary<Type, object>();

		// Token: 0x040005BF RID: 1471
		private static Dictionary<Type, uint> _peerComponentIds;

		// Token: 0x040005C0 RID: 1472
		private static Dictionary<uint, Type> _peerComponentTypes;

		// Token: 0x040005C1 RID: 1473
		private string _bannerCode;

		// Token: 0x040005C6 RID: 1478
		public readonly ICommunicator Communicator;

		// Token: 0x040005C7 RID: 1479
		private EntitySystem<PeerComponent> _peerEntitySystem;

		// Token: 0x040005CB RID: 1483
		public Dictionary<int, List<int>> UsedCosmetics;
	}
}
