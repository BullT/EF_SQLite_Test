using Microsoft.EntityFrameworkCore;
using MQTTprism.Data;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace TestApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly DataContext _dataContext;
        private ObservableCollection<SensorItemViewModel> _sensors;

        public MainPageViewModel(
            INavigationService navigationService,
            DataContext dataContext) : base(navigationService)
        {
            _navigationService = navigationService;
            _dataContext = dataContext;
        }

        public ObservableCollection<SensorItemViewModel> Sensors
        {
            get => _sensors;
            set => SetProperty(ref _sensors, value);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            var sensors = await _dataContext.Sensors.ToListAsync();
            Sensors = new ObservableCollection<SensorItemViewModel>(sensors.Select(s => new SensorItemViewModel(_navigationService)
            {
                Id = s.Id,
                Name = s.Name
            }).ToList());
        }
    }
}
