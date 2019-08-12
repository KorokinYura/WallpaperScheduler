using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WallpaperScheduler.Models;
using System.IO;
using System.Linq;

namespace WallpaperScheduler.Views
{
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public List<string> ImagesNames { get; private set; }


        public NewItemPage()
        {
            ImagesNames = Directory.GetFiles(@"/sdcard/WallpaperImages").Select(n => Path.GetFileName(n)).ToList();

            InitializeComponent();

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", new Item
            {
                ImageName = picker.SelectedItem.ToString(),
                Time = timePicker.Time
            });
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}