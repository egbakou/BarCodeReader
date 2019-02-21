using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace BarCodeReader.ViewModels
{
    public class MainMenuViewModel : BaseViewModel
    {
        public MainMenuViewModel()
        {
            Title = "Barcodes Reader";
            ScanCommand = new Command(async () => await Scannning());
            ClearResultCommand = new Command(ClearResult);
            ShareResultCommand = new Command(async () => await ShareResult());
        }



        private async Task ShareText(string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = "Share"
            });
        }


        // Share Scan result
        private async Task ShareResult()
        {
            if (string.IsNullOrEmpty(ScanResult))
            {
                MessagingCenter.Send(this, "NoScanResultToShare");
            }
            else
            {
                await ShareText(ScanResult);
            }
        }


        // Clear Scan result
        private void ClearResult()
        {
            if (string.IsNullOrEmpty(ScanResult))
            {
                MessagingCenter.Send(this, "NoScanResultToClear");
            }
            else
            {
                ScanResult = "";
            }
        }


        // Do scanning
        private async Task Scannning()
        {
            var scanPage = new ZXingScannerPage
            {
                Title = "Scanning...",
                BackgroundColor = Color.FromHex("#212223")
            };
            await App.Current.MainPage.Navigation.PushAsync(scanPage);
            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop to page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.Navigation.PopAsync();
                    ScanResult = result?.Text;
                });
            };
        }

        public ICommand ScanCommand { get; private set; }
        public ICommand ClearResultCommand { get; private set; }
        public ICommand ShareResultCommand { get; private set; }

        private string _scanResult;
        public string ScanResult
        {
            get { return _scanResult; }
            set { SetProperty(ref _scanResult, value); }
        }
    }   
}
