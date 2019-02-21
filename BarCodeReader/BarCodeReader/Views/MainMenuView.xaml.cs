using BarCodeReader.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BarCodeReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuView : ContentPage
    {
        private MainMenuViewModel viewModel;
        public MainMenuView()
        {
            InitializeComponent();
            BindingContext = viewModel = new MainMenuViewModel();
            RegisterMesssages();
        }

        private void RegisterMesssages()
        {
            MessagingCenter.Subscribe<MainMenuViewModel>(this, "NoScanResultToClear", (m) =>
            {
                if (m != null)
                {
                    DisplayAlert("Warning !", "No scan result to clear.", "OK");
                }
            });

            MessagingCenter.Subscribe<MainMenuViewModel>(this, "NoScanResultToShare", (m) =>
            {
                if (m != null)
                {
                    DisplayAlert("Warning !", "No scan result to share.", "OK");
                }
            });
        }
    }
}