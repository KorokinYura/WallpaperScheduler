﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

using WallpaperScheduler.Models;
using System.IO;
using System.Linq;

namespace WallpaperScheduler.Views
{
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public string Item { get; set; }

        public List<string> ImagesNames { get; private set; }


        public NewItemPage()
        {
            ImagesNames = Configurator.GetImagesNames().Select(n => Path.GetFileName(n)).ToList();
            Item = ImagesNames[0];

            InitializeComponent();

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", new Item
            {
                Id = Guid.NewGuid().ToString(),
                ImageName = picker.SelectedItem.ToString(),
                Time = timePicker.Time
            });
            await Navigation.PopModalAsync();

            MessagingCenter.Send<Page>(this, "RefreshItemsPage");
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}