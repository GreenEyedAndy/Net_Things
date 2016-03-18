using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using DevExpress.Mvvm;
using System.Windows.Input;
using System.Windows.Threading;
using DevExpress.Xpf.Bars;

namespace RxDemo01.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IDisposable subscription;

        public MainViewModel()
        {
            Logging = new GalleryCollection<string> {"Hallo"};
        }

        private ICommand startCommand;

        public ICommand StartCommand
        {
            get { return startCommand ?? (startCommand = new DelegateCommand(StartExecute)); }
        }

        private void StartExecute()
        {
            Logging.Add("Start pressed");
            subscription?.Dispose();

            subscription = Observable.Interval(TimeSpan.FromSeconds(1))
                .Buffer(2)
                .ObserveOn(Dispatcher.CurrentDispatcher)
                .Subscribe(list => Logging.Add(DateTime.Now.Second + ": Got " + list[0] + " and " + list[1]));
        }

        private ICommand stopCommand;

        public ICommand StopCommand
        {
            get { return stopCommand ?? (stopCommand = new DelegateCommand(StopExecute)); }
        }

        private void StopExecute()
        {
            Logging.Add("Stop pressed");
            subscription?.Dispose();
        }

        public ObservableCollection<string> Logging
        {
            get { return GetProperty(() => Logging); }
            set { SetProperty(() => Logging, value); }
        }
    }
}