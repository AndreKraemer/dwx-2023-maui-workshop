using CommunityToolkit.Mvvm.ComponentModel;
using ElVegetarianoFurio.Data;
using ElVegetarianoFurio.Menu;
using System.Collections.ObjectModel;


namespace ElVegetarianoFurio
{
    [INotifyPropertyChanged]
    public partial class MainViewModel
    {
        private readonly IDataService _dataService;
       
        [ObservableProperty]
        private bool _isBusy = false;

        public ObservableCollection<Category> Categories { get; set; }
            = new ObservableCollection<Category>();

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
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
    }
}
