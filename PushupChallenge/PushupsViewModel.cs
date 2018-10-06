using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PushupChallenge
{
    public class PushupsViewModel : INotifyPropertyChanged
    {

        private PushupSetRepository _repository;

        private int _repetitions;
        private int _totalRepetitions;

        public int Repetitions
        {
            get { return _repetitions; }
            set
            {
                if (_repetitions != value)
                {
                    _repetitions = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Repetitions)));
                }
            }
        }

        public int TotalRepetitions
        {
            get { return _totalRepetitions; }
            set
            {
                if (_totalRepetitions != value)
                {
                    _totalRepetitions = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalRepetitions)));
                }
            }
        }


        public ICommand DecreaseRepsCommand { get; }
        public ICommand IncreaseRepsCommand { get; }
        public ICommand SaveCommand { get; }

        public PushupsViewModel()
        {
            DecreaseRepsCommand = new Command(() => Repetitions -= 5);
            IncreaseRepsCommand = new Command(() => Repetitions += 5);
            SaveCommand = new Command(Save);
            _repository = new PushupSetRepository();
            _repository.Initialize().Wait();

            //Doing this synchronously seemed to hang, or maybe it was a coincidence
            Action loadTotals =  async () => TotalRepetitions = await _repository.GetTotalRepetitions();
            loadTotals.Invoke();
        }

        private void Save()
        {
            TotalRepetitions += Repetitions;
            _repository.Save(Repetitions, DateTime.Now).Wait();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
