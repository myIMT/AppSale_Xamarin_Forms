using System;

using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace Multiselect
{
	/* 
	* based on
	* https://gist.github.com/glennstephens/76e7e347ca6c19d4ef15
	*/

	public class SelectMultipleBasePage<T> : ContentPage
	{
		public class WrappedSelection<T> : INotifyPropertyChanged
		{
			public T Item { get; set; }
			bool isSelected = false;
			public bool IsSelected { 
				get {
					return isSelected;
				}
				set
				{
					if (isSelected != value) {
						isSelected = value;
						PropertyChanged (this, new PropertyChangedEventArgs ("IsSelected"));
//						PropertyChanged (this, new PropertyChangedEventArgs (nameof (IsSelected))); // C# 6
					}
				}
			}
			public event PropertyChangedEventHandler PropertyChanged = delegate {};
		}
		public class WrappedItemSelectionTemplate : ViewCell
		{
			public WrappedItemSelectionTemplate() : base ()
			{
				Label name = new Label();
				name.SetBinding(Label.TextProperty, new Binding("Item.Name"));
				Switch mainSwitch = new Switch();
				mainSwitch.SetBinding(Switch.IsToggledProperty, new Binding("IsSelected"));
                Button myButton1 = new Button();
                myButton1.SetBinding(Button.TextProperty, new Binding("My Button 1"));
                RelativeLayout layout = new RelativeLayout();
				layout.Children.Add (name, 
					Constraint.Constant (5), 
					Constraint.Constant (5),
					Constraint.RelativeToParent (p => p.Width - 60),
					Constraint.RelativeToParent (p => p.Height - 10)
				);
				layout.Children.Add (mainSwitch, 
					Constraint.RelativeToParent (p => p.Width - 55), 
					Constraint.Constant (5),
					Constraint.Constant (50),
					Constraint.RelativeToParent (p => p.Height - 10)
				);
//                layout.Children.Add(myButton1,
//                    Constraint.RelativeToParent(p => p.Width - 55),
//                    Constraint.Constant(5),
//                    Constraint.Constant(50),
//                    Constraint.RelativeToParent(p => p.Height - 10)
//);
                View = layout;

                //Button myButton1 = new Button { Text = "My Button 1" };
                //var layout2 = new StackLayout();
                //layout2.Children.Add(myButton1);
                //myButton1.Clicked += (object sender, EventArgs e) => layout.ForceLayout();
                //View = layout2;
                //layout.Children.Add(button,
                //    Constraint.Constant(10),
                //    Constraint.Constant(10));

            }
        }

		public List<WrappedSelection<T>> WrappedItems = new List<WrappedSelection<T>>();

		public SelectMultipleBasePage(List<T> items,bool init)
		{
            if (init)
            {   
                WrappedItems = items.Select(item => new WrappedSelection<T>() { Item = item, IsSelected = false }).ToList();
            }
            else
            {
                WrappedItems = items.Select(item => new WrappedSelection<T>(){ Item = item, IsSelected = true }).ToList();
                foreach (var wi in WrappedItems)
                {
                    CheckItem it = new CheckItem();
                    //it.Name = (CheckItem)wi;
                    //if (wi.Item.ToString() == "FASHION & BEAUTY")
                    //{
                    //    wi.IsSelected = true;
                    //}
                }
                //DisplayAlert("SetFavouriteValue: " + name, favourites.FashionAndBeauty.ToString(), "OK");

                //return WrappedItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();

                // WrappedItems
                // items.Select(item => new WrappedSelection<T>(){ Item = item, IsSelected = true }).ToList()
                // items.Select().ToList()
                // item => new WrappedSelection<T>(){ Item = item, IsSelected = true }
                // item
                // new WrappedSelection<T>(){ Item = item, IsSelected = true }
                // new WrappedSelection<T>(){}
                // Item = item, IsSelected = true
            }

            ListView mainList = new ListView () {
				ItemsSource = WrappedItems,
				ItemTemplate = new DataTemplate (typeof(WrappedItemSelectionTemplate)),
			};

            //Button button = new Button
            //{
            //    Text = String.Format("Tap for click count!")
            //};
            //button.Clicked += (sender, args) =>
            //{
            //    DisplayAlert("Alert", "Button Clicked", "OK");
            //    //count++;
            //    //button.Text =
            //    //    String.Format("{0} click{1}!", count, count == 1 ? "" : "s");
            //};

            mainList.ItemSelected += (sender, e) => {
				if (e.SelectedItem == null) return;
				var o = (WrappedSelection<T>)e.SelectedItem;
				o.IsSelected = !o.IsSelected;
				((ListView)sender).SelectedItem = null; //de-select
			};
			Content = mainList;
            if (Device.OS == TargetPlatform.Windows)
            {   // fix issue where rows are badly sized (as tall as the screen) on WinPhone8.1
                mainList.RowHeight = 40;
                // also need icons for Windows app bar (other platforms can just use text)
                ToolbarItems.Add(new ToolbarItem("All", "check.png", SelectAll, ToolbarItemOrder.Primary));
                ToolbarItems.Add(new ToolbarItem("None", "cancel.png", SelectNone, ToolbarItemOrder.Primary));
            }
            else
            {
                ToolbarItems.Add(new ToolbarItem("All", null, SelectAll, ToolbarItemOrder.Primary));
                ToolbarItems.Add(new ToolbarItem("None", null, SelectNone, ToolbarItemOrder.Primary));
            }
        }

        //public SetSelectionMultipleBasePage(List<T> items)
        //{
        //    WrappedItems = items.Select(item => new WrappedSelection<T>() { Item = item, IsSelected = true }).ToList();
        //    ListView mainList = new ListView()
        //    {
        //        ItemsSource = WrappedItems,
        //        ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
        //    };

        //    //Button button = new Button
        //    //{
        //    //    Text = String.Format("Tap for click count!")
        //    //};
        //    //button.Clicked += (sender, args) =>
        //    //{
        //    //    DisplayAlert("Alert", "Button Clicked", "OK");
        //    //    //count++;
        //    //    //button.Text =
        //    //    //    String.Format("{0} click{1}!", count, count == 1 ? "" : "s");
        //    //};

        //    mainList.ItemSelected += (sender, e) => {
        //        if (e.SelectedItem == null) return;
        //        var o = (WrappedSelection<T>)e.SelectedItem;
        //        o.IsSelected = !o.IsSelected;
        //        ((ListView)sender).SelectedItem = null; //de-select
        //    };
        //    Content = mainList;
        //    if (Device.OS == TargetPlatform.Windows)
        //    {   // fix issue where rows are badly sized (as tall as the screen) on WinPhone8.1
        //        mainList.RowHeight = 40;
        //        // also need icons for Windows app bar (other platforms can just use text)
        //        ToolbarItems.Add(new ToolbarItem("All", "check.png", SelectAll, ToolbarItemOrder.Primary));
        //        ToolbarItems.Add(new ToolbarItem("None", "cancel.png", SelectNone, ToolbarItemOrder.Primary));
        //    }
        //    else
        //    {
        //        ToolbarItems.Add(new ToolbarItem("All", null, SelectAll, ToolbarItemOrder.Primary));
        //        ToolbarItems.Add(new ToolbarItem("None", null, SelectNone, ToolbarItemOrder.Primary));
        //    }
        //}

        void SelectAll ()
		{
			foreach (var wi in WrappedItems) {
				wi.IsSelected = true;
			}
		}

		void SelectNone ()
		{
			foreach (var wi in WrappedItems) {
				wi.IsSelected = false;
			}
		}

		public List<T> GetSelection() 
		{
			return WrappedItems.Where (item => item.IsSelected).Select (wrappedItem => wrappedItem.Item).ToList ();	
		}

        public void SetSelection()
        {
            foreach (var wi in WrappedItems)
            {
                wi.IsSelected = true;
            }
            //return WrappedItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();
        }
    }
}


