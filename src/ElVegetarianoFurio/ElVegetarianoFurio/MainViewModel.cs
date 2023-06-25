using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ElVegetarianoFurio.Core;
using ElVegetarianoFurio.Data;
using ElVegetarianoFurio.Menu;
using System.Collections.ObjectModel;


namespace ElVegetarianoFurio
{
    [INotifyPropertyChanged]
    public partial class MainViewModel
    {
        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private bool _isBusy = false;

        public ObservableCollection<Category> Categories { get; set; }
            = new ObservableCollection<Category>();

        public MainViewModel(IDataService dataService, INavigationService navigationService)
        {
            _dataService = dataService;
            _navigationService = navigationService;
        }

        public async Task Initialize()
        {
            IsBusy = true;
            try
            {
                Categories.Clear();
                var categories = await _dataService.GetCategoriesAsync();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async void NavigateToCategory(Category category)
        {
            var navigationParams = new Dictionary<string, object>
            {
                { nameof(CategoryPage.Category), category }
            };
            await _navigationService.GoToAsync($"{nameof(CategoryPage)}", navigationParams);
        }
    }
}
