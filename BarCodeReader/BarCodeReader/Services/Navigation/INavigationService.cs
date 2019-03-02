using BarCodeReader.ViewModels;
using System.Threading.Tasks;

namespace BarCodeReader.Services.Navigation
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }

        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}
