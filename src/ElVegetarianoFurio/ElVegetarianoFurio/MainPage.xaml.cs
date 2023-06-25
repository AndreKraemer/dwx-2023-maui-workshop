namespace ElVegetarianoFurio;

public partial class MainPage : ContentPage
{
	private readonly MainViewModel _viewModel;
	
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		await _viewModel.Initialize();
    }
}

