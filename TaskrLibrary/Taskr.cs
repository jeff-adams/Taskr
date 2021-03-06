﻿using System.IO;

namespace TaskrLibrary
{
	public class Taskr
	{
		private TaskList taskList;
		private IFileManager fileManager;

		public Taskr()
		{
			fileManager = new XMLFileManager();
			LoadTaskList();
		}

		public Page GetPage(int pageIndex)
		{
			return taskList.Pages[pageIndex];
		}

		public int GetTotalPageCount() => taskList.Pages.Count;

		public void AddTask(string taskTitle)
		{
			var task = new Task(taskTitle);
			InsertTaskOnLastPage(task);
		}

		public void CopyTaskToEndOfList(Task task)
		{
			var newTask = new Task(task.Title, task.TimeStamp);
			InsertTaskOnLastPage(newTask);
		} 

		public bool RemoveCompletelyActionedPages(Page page)
		{
			if(page.IsFull && page.IsFullyActioned)
			{
				taskList.Pages.Remove(page);
				return true;
			}
			else
			{
				return false;
			}
		}

		private void InsertTaskOnLastPage(Task task)
		{
			if (taskList.Pages[taskList.Pages.Count - 1].IsFull)
			{
				var newPage = new Page();
				newPage.Tasks.Add(task);
				taskList.Pages.Add(newPage);
			}
			else
			{
				taskList.Pages[taskList.Pages.Count - 1].Tasks.Add(task);
			}
		}

		private void LoadTaskList()
		{
			try
			{
				taskList = fileManager.Load();
			}
			catch (FileNotFoundException)
			{
				taskList = new TaskList();
				var page = new Page();
				var task = new Task("Populate the task list with various tasks");
				page.Tasks.Add(task);
				taskList.Pages.Add(page);
			}
		}

		public void SaveTaskList()
		{
			fileManager.Save(taskList);
		}
	}
}
