using System.ComponentModel;

/// <summary>
/// Паттерн mvvm
/// </summary>
namespace FarpostAdsWpf.ServicesClasses
{
    public class ObservableObjects : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
