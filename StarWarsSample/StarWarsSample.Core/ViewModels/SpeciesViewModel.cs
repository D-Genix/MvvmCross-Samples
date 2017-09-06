using System.Threading.Tasks;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using Nito.AsyncEx;
using StarWarsSample.Core.Models;
using StarWarsSample.Core.ViewModelResults;
using StarWarsSample.Core.Services.Interfaces;

namespace StarWarsSample.Core.ViewModels
{
    public class SpeciesViewModel : BaseViewModel
    {
        private readonly ISpeciesService _speciesService;
        private readonly IMvxNavigationService _navigationService;

        private string _nextPage;

        public SpeciesViewModel(
            ISpeciesService speciesService,
            IMvxNavigationService navigationService)
        {
            _speciesService = speciesService;
            _navigationService = navigationService;

            Species = new MvxObservableCollection<Specie>();

            SpecieSelectedCommand = new MvxAsyncCommand<Specie>(SpecieSelected);
            FetchSpecieCommand = new MvxCommand(
                () =>
            {
                    if (!string.IsNullOrEmpty(_nextPage))
                    {
                        FetchSpeciesTask = NotifyTaskCompletion.Create(LoadSpecies);
                        RaisePropertyChanged(() => FetchSpeciesTask);
                    }
            });
            RefreshSpeciesCommand = new MvxCommand(RefreshSpecies);
        }

        // MvvmCross Lifecycle
        public override Task Initialize()
        {
            LoadSpeciesTask = NotifyTaskCompletion.Create(LoadSpecies);

            return Task.FromResult(0);
        }

        // MVVM Properties
        public INotifyTaskCompletion LoadSpeciesTask { get; private set; }

        public INotifyTaskCompletion FetchSpeciesTask { get; private set; }

        private MvxObservableCollection<Specie> _species;
        public MvxObservableCollection<Specie> Species
        {
            get
            {
                return _species;
            }
            set
            {
                _species = value;
                RaisePropertyChanged(() => Species);
            }
        }

        // MVVM Commands
        public IMvxCommand<Specie> SpecieSelectedCommand { get; private set; }

        public IMvxCommand FetchSpecieCommand { get; private set; }

        public IMvxCommand RefreshSpeciesCommand { get; private set; }

        // Private methods
        private async Task LoadSpecies()
        {
            var result = await _speciesService.GetSpeciesAsync(_nextPage);

            if (string.IsNullOrEmpty(_nextPage))
            {
                Species.Clear();
                Species.AddRange(result.Results);
            }
            else
            {
                Species.AddRange(result.Results);
            }

            _nextPage = result.Next;
        }

        private async Task SpecieSelected(Specie selectedSpecie)
        {
            var result = await _navigationService.Navigate<SpecieViewModel, Specie, DestructionResult<Specie>>(selectedSpecie);
        }

        private void RefreshSpecies()
        {
            _nextPage = null;

            LoadSpeciesTask = NotifyTaskCompletion.Create(LoadSpecies);
            RaisePropertyChanged(() => LoadSpeciesTask);
        }
    }
}
