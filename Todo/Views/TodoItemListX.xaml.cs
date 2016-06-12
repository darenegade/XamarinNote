using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Geolocator.Plugin;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Todo
{
    public partial class TodoItemListX : ContentPage
    {
        public TodoItemListX()
        {
            InitializeComponent();

            #region toolbar
            ToolbarItem tbi = null;

			ToolbarItem test = new ToolbarItem("X", "start", ()=> perfTest(), 0, 0);

            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () =>
                {
                    var todoItem = new TodoItem();
                    var todoPage = new TodoItemPageX();
                    todoPage.BindingContext = todoItem;
                    Navigation.PushAsync(todoPage);
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "plus", () =>
                {
                    var todoItem = new TodoItem();
                    var todoPage = new TodoItemPageX();
                    todoPage.BindingContext = todoItem;
                    Navigation.PushAsync(todoPage);
                }, 0, 0);
            }

            ToolbarItems.Add(tbi);
			ToolbarItems.Add(test);
            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtTodoId = -1;
            listView.ItemsSource = App.Database.GetItems();
        }

        void listItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var todoItem = (TodoItem)e.SelectedItem;
            var todoPage = new TodoItemPageX();
            todoPage.BindingContext = todoItem;

            ((App)App.Current).ResumeAtTodoId = todoItem.ID;
            Debug.WriteLine("setting ResumeAtTodoId = " + todoItem.ID);

            Navigation.PushAsync(todoPage);
        }

		async Task perfTest()
		{
			var watch = System.Diagnostics.Stopwatch.StartNew();

			int count = 10000;
			Random randNum = new Random();
			int[] arr = Enumerable
				.Repeat(0, count)
				.Select(i => randNum.Next())
				.ToArray();

			int temp = 0;

			for (int write = 0; write < arr.Length; write++)
			{
				for (int sort = 0; sort < arr.Length - 1; sort++)
				{
					if (arr[sort] > arr[sort + 1])
					{
						temp = arr[sort + 1];
						arr[sort + 1] = arr[sort];
						arr[sort] = temp;
					}
				}
			}

			TodoItem[] items = new TodoItem[count];
			for (int i = 0; i < count; i++)
			{
				items[i] = new TodoItem();
				items[i].Lat = 0;
				items[i].Long = 0;
				items[i].Name = "PerfTest";
				items[i].ID = App.Database.SaveItem(items[i]);
			}

			foreach (TodoItem item in App.Database.GetItemsByName("PerfTest"))
				App.Database.DeleteItem(item.ID);

			watch.Stop();
			await DisplayAlert("Finished", "Done for " + count + " Elements after: " + watch.ElapsedMilliseconds +"ms", "OK");
		}
    }
}
