using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ElVegetarianoFurio.Profile
{
    [INotifyPropertyChanged]
    public partial class ProfileViewModel
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _fullName = string.Empty;

        [ObservableProperty]
        private string _street = string.Empty;

        [ObservableProperty]
        private string _zip = string.Empty;

        [ObservableProperty]
        private string _city = string.Empty;

        [ObservableProperty]
        private string _phone = string.Empty;

        [ObservableProperty]
        private bool _isBusy = false;
       
        private readonly IProfileService _profileService;

        public ProfileViewModel(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task Initialize()
        {
            IsBusy = true;
            try
            {

                var profile = await _profileService.GetAsync();

                FullName = profile.FullName;
                Street = profile.Street;
                Zip = profile.Zip;
                City = profile.City;
                Phone = profile.Phone;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task Save()
        {
            IsBusy = true;

            try
            {
                var profile = new Profile
                {
                    FullName = FullName,
                    Street = Street,
                    Zip = Zip,
                    City = City,
                    Phone = Phone
                };
                await _profileService.SaveAsync(profile);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanSave()
        {
            return !IsBusy
                && !string.IsNullOrWhiteSpace(FullName);
        }

    }
}
