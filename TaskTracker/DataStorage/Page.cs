using System;
using System.Collections.Generic;

namespace TaskrLibrary
{
	[Serializable]
	public class Page
    {
        public List<Task> Tasks {get; private set;}
		public bool IsFull
		{
			get
			{
				return Tasks.Count >= Global.PageSize;
			}
		}

		public Page()
		{
			Tasks = new List<Task>();
		}
    }
}