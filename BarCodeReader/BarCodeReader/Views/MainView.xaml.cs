using BarCodeReader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
	}
}