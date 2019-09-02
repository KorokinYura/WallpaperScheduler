using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WallpaperScheduler.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        private bool isAboutExpanded = false;
        private bool isHowToUseExpanded = false;
        private bool isPerfomanceExpanded = false;

        public AboutPage()
        {
            InitializeComponent();
        }

        protected async void AboutClicked(object sender, EventArgs e)
        {
            if (isAboutExpanded)
            {
                await AboutStack.FadeTo(0);
                AboutStack.IsVisible = !isAboutExpanded;
            }
            else
            {
                AboutStack.IsVisible = !isAboutExpanded;
                await AboutStack.FadeTo(1);
            }

            isAboutExpanded = !isAboutExpanded;
        }

        protected async void HowToUseClicked(object sender, EventArgs e)
        {
            if (isHowToUseExpanded)
            {
                await HowToUseStack.FadeTo(0);
                HowToUseStack.IsVisible = !isHowToUseExpanded;
            }
            else
            {
                HowToUseStack.IsVisible = !isHowToUseExpanded;
                await HowToUseStack.FadeTo(1);
            }

            isHowToUseExpanded = !isHowToUseExpanded;
        }

        protected async void PerfomanceClicked(object sender, EventArgs e)
        {
            if (isPerfomanceExpanded)
            {
                await PerfomanceStack.FadeTo(0);
                PerfomanceStack.IsVisible = !isPerfomanceExpanded;
            }
            else
            {
                PerfomanceStack.IsVisible = !isPerfomanceExpanded;
                await PerfomanceStack.FadeTo(1);
            }

            isPerfomanceExpanded = !isPerfomanceExpanded;
        }
    }
}