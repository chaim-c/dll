using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000B7 RID: 183
	public abstract class PeerComponent : IEntityComponent
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0001F3C8 File Offset: 0x0001D5C8
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x0001F3D0 File Offset: 0x0001D5D0
		public VirtualPlayer Peer
		{
			get
			{
				return this._peer;
			}
			set
			{
				this._peer = value;
			}
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0001F3D9 File Offset: 0x0001D5D9
		public virtual void Initialize()
		{
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001F3DB File Offset: 0x0001D5DB
		public string Name
		{
			get
			{
				return this.Peer.UserName;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000968 RID: 2408 RVA: 0x0001F3E8 File Offset: 0x0001D5E8
		public bool IsMine
		{
			get
			{
				return this.Peer.IsMine;
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001F3F5 File Offset: 0x0001D5F5
		public T GetComponent<T>() where T : PeerComponent
		{
			return this.Peer.GetComponent<T>();
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001F402 File Offset: 0x0001D602
		public virtual void OnInitialize()
		{
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001F404 File Offset: 0x0001D604
		public virtual void OnFinalize()
		{
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0001F406 File Offset: 0x0001D606
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0001F40E File Offset: 0x0001D60E
		public uint TypeId { get; set; }

		// Token: 0x04000570 RID: 1392
		private VirtualPlayer _peer;
	}
}
