using BarCodeReader.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
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


        private async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share"
            });
        }


        private async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = "Share"
            });
        }


        private async void ShareBt_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ScanningResultLabel.Text))
            {
                await DisplayAlert("Information !", "No scan result to clear", "OK");
            }
            else
            {
                await ShareText(ScanningResultLabel.Text);
            }
        }


        private async void ClearBt_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ScanningResultLabel.Text))
            {
                await DisplayAlert("Warning !", "No scan result to share", "OK");
            }
            else
            {
                ScanningResultLabel.Text = "";
            }
        }
    }
}