using Lab2.Navigation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Lab2.ViewModels
{
    public class NavigationViewModel : INotifyPropertyChanged
    {
        private NavigationInProject viewModel;
        List<NavigationInProject> viewModels = new List<NavigationInProject>();

        public event PropertyChangedEventHandler PropertyChanged;

        public NavigationViewModel()
        {
            Navigate(NavigationTypes.PersonList);
        }

        
        public NavigationInProject ViewModel
        {
            get
            {
                return viewModel;
            }
            set
            {
                viewModel = value;
                OnPropertyChanged(nameof(ViewModel));
            }
        }

        public void Navigate(NavigationTypes type)
        {
            if (ViewModel != null && ViewModel.ViewType == type)
            {
                return;
            }
            NavigationInProject viewModel = GetViewModel(type);
            if (viewModel == null)
            {
                return;
            }
            ViewModel = viewModel;

        }

        private NavigationInProject GetViewModel(NavigationTypes type)
        {
            NavigationInProject viewModel = viewModels.FirstOrDefault(vm => vm.ViewType == type);
            if (viewModel != null)
            {
                return viewModel;
            }
            viewModel = CreateNewViewModel(type);

            viewModels.Add(viewModel);

            return viewModel;
        }

        private void NavigateToInfo(InfoViewModel infoViewModel)
        {
            ViewModel = infoViewModel;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected NavigationInProject CreateNewViewModel(NavigationTypes type)
        {
            switch (type)
            {
                case NavigationTypes.NewPerson:
                    return new NewPersonViewModel((p) => Navigate(NavigationTypes.PersonList));
                case NavigationTypes.PersonList:
                    return new PeopleListViewModel(() => Navigate(NavigationTypes.NewPerson), person => ViewModel = person, () => Navigate(NavigationTypes.PersonList));
                default:
                    return null;
            }
        }

    }
    public enum NavigationTypes
    {
        NewPerson,
        EditPerson,
        PersonList,
        Info,
    }

}
