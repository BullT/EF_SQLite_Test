using MQTTprism.Data.Entities;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.ViewModels
{
    public class SensorItemViewModel : Sensor
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectSensorCommand;

        public SensorItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectSensorCommand => _selectSensorCommand ?? (_selectSensorCommand = new DelegateCommand(SelectSensor));

        private async void SelectSensor()
        {
            var parameters = new NavigationParameters
            {
                {"Sensor", this}
            };

            await _navigationService.NavigateAsync("SensorPage", parameters);
        }
    }
}
