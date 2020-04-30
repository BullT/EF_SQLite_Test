using MQTTprism.Data;
using MQTTprism.Data.Entities;
using Prism.Commands;
using Prism.Navigation;

namespace TestApp.ViewModels
{
    public class SensorPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly DataContext _dataContext;
        private DelegateCommand _updateCommand;
        private Sensor _sensor;

        public SensorPageViewModel(
            INavigationService navigationService,
            DataContext dataContext) : base(navigationService)
        {
            _navigationService = navigationService;
            _dataContext = dataContext;
        }

        public DelegateCommand UpdateCommand => _updateCommand ?? (_updateCommand = new DelegateCommand(Update));


        public Sensor Sensor
        {
            get => _sensor;
            set => SetProperty(ref _sensor, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            Sensor = parameters.GetValue<Sensor>("Sensor");
        }

        private async void Update()
        {
            _dataContext.Sensors.Update(Sensor);
            await _dataContext.SaveChangesAsync();

            await _navigationService.GoBackAsync();
        }
    }
}
