using BarCodeReader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace BarCodeReader.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : ContentPage
	{
        private MainViewModel viewModel;

		public MainView ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new MainViewModel();
		}

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage
            {
                Title = "Scanning...",
                HasTorch = true,
                BackgroundColor = Color.FromHex("#212223")
            };
            await Navigation.PushAsync(scanPage);
            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop to page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    ScanningResultLabel.Text = result?.Text;
                });
            };
        }
    }
}