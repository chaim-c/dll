using System;

namespace Mono.Cecil
{
	// Token: 0x020000C4 RID: 196
	public abstract class EventReference : MemberReference
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x000199AF File Offset: 0x00017BAF
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x000199B7 File Offset: 0x00017BB7
		public TypeReference EventType
		{
			get
			{
				return this.event_type;
			}
			set
			{
				this.event_type = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x000199C0 File Offset: 0x00017BC0
		public override string FullName
		{
			get
			{
				return this.event_type.FullName + " " + base.MemberFullName();
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000199DD File Offset: 0x00017BDD
		protected EventReference(string name, TypeReference eventType) : base(name)
		{
			if (eventType == null)
			{
				throw new ArgumentNullException("eventType");
			}
			this.event_type = eventType;
		}

		// Token: 0x06000705 RID: 1797
		public abstract EventDefinition Resolve();

		// Token: 0x040004A2 RID: 1186
		private TypeReference event_type;
	}
}
