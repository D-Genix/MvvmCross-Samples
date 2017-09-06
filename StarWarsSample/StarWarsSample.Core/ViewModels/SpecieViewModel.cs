using System;
using System.Threading.Tasks;
using System.Linq;
using Acr.UserDialogs;
using StarWarsSample.Core.Models;
using StarWarsSample.Core.ViewModelResults;

namespace StarWarsSample.Core.ViewModels
{
    public class SpecieViewModel : BaseViewModel<Specie, DestructionResult<Specie>>
    {
        private readonly IUserDialogs _userDialogs;
                
        public SpecieViewModel(
            IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
        }

        // MvvmCross Lifecycle
        public override Task Initialize(Specie parameter)
        {
            Specie = parameter;            

            return Task.FromResult(0);
        }

        // MVVM Properties
        private Specie _specie;
        public Specie Specie
        {
            get
            {
                return _specie;
            }
            set
            {
                _specie = value;
                RaisePropertyChanged(() => Specie);
            }
        }        
    }
}
