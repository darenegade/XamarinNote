using System;
using Geolocator.Plugin;
using Xamarin.Forms;

namespace Todo
{
    public partial class TodoItemPageX : ContentPage
    {
        public TodoItemPageX()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);

        }

		async void saveClicked(object sender, EventArgs e)
		{
			var todoItem = (TodoItem)BindingContext;

			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;
			var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

			todoItem.Lat = position.Latitude;
			todoItem.Long = position.Longitude;

			App.Database.SaveItem(todoItem);
			await this.Navigation.PopAsync();
		}

		void deleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            App.Database.DeleteItem(todoItem.ID);
            this.Navigation.PopAsync();
        }

        void cancelClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            this.Navigation.PopAsync();
        }

    }
}
