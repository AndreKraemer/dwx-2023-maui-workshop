using CommunityToolkit.Mvvm.ComponentModel;
using ElVegetarianoFurio.Data;
using System.Collections.ObjectModel;

namespace ElVegetarianoFurio.Menu
{
    [INotifyPropertyChanged]
    public partial class CategoryViewModel
    {
        [ObservableProperty]
        private bool _isBusy = false;

        [ObservableProperty]
        private Category _category;


        public ObservableCollection<Dish> Dishes { get; set; }
            = new ObservableCollection<Dish>();

        private readonly IDataService _dataService;

        public CategoryViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task Initialize(Category category)
        {
            IsBusy = true;
            try
            {
                Dishes.Clear();
                Category = category;
                var dishes = await _dataService.GetDishesAsync(category.Id);

                foreach (var dish in dishes)
                {
                    Dishes.Add(dish);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
