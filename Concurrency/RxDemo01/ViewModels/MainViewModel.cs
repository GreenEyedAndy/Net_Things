using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading;
using DevExpress.Mvvm;
using System.Windows.Input;

namespace RxDemo01.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IDisposable subscription;

        public MainViewModel()
        {
            Logging = new ObservableCollection<string> {"Hallo"};
        }

        private ICommand startBufferCommand;

        public ICommand StartBufferCommand
        {
            get { return startBufferCommand ?? (startBufferCommand = new DelegateCommand(StartBufferExecute)); }
        }

        private void StartBufferExecute()
        {
            Logging.Add("Start Buffer pressed");
            subscription?.Dispose();

            var uiContext = SynchronizationContext.Current;
            subscription = Observable.Interval(TimeSpan.FromSeconds(1))
                .Buffer(2)
                .ObserveOn(uiContext)
                .Subscribe(list => Logging.Add(DateTime.Now.Second + ": Got " + list[0] + " and " + list[1]));
        }

        private ICommand startWindowCommand;

        public ICommand StartWindowCommand
        {
            get { return startWindowCommand ?? (startWindowCommand = new DelegateCommand(StartWindowExecute)); }
        }

        private void StartWindowExecute()
        {
            Logging.Add("Start Window pressed");
            subscription?.Dispose();

            var uiContext = SynchronizationContext.Current;
            subscription = Observable.Interval(TimeSpan.FromSeconds(1))
                .Window(2)
                .ObserveOn(uiContext)
                .Subscribe(observable =>
                {
                    Logging.Add(DateTime.Now.Second + ": Starting new group");
                    observable
                    .ObserveOn(uiContext)
                    .Subscribe(l => Logging.Add(DateTime.Now.Second + ": Saw " + l),
                        () => Logging.Add(DateTime.Now.Second + ": ending group"));
                });
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