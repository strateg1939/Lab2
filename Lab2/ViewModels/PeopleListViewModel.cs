using Lab2.Models;
using Lab2.Navigation;
using Lab2.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.ViewModels
{
    public class PeopleListViewModel : NavigationInProject, INotifyPropertyChanged
    {

        private Action gotoLogin;
        private PersonRepository personRepository;
        private ObservableCollection<EditPersonViewModel> people;
        private ObservableCollection<EditPersonViewModel> gridPeople;

        private Action<EditPersonViewModel> gotoPerson;
        private RelayCommand<object> gotoNewPersonCommand;
        private RelayCommand<object> changeSelectedCommand;
        private RelayCommand<object> exitCommand;
        private RelayCommand<object> removePersonCommand;
        private RelayCommand<object> filterAdultsCommand;
        private RelayCommand<object> cancelFilterCommand;

        public NavigationTypes ViewType => NavigationTypes.Info;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<EditPersonViewModel> People
        {
            get
            {
                return people;
            }
            set
            {
                people = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EditPersonViewModel> GridPeople
        {
            get
            {
                return gridPeople;
            }
            set
            {
                gridPeople = value;
                OnPropertyChanged();
            }
        }
        public EditPersonViewModel SelectedPerson
        {
            get;
            set;
        }
        public RelayCommand<object> GotoLoginCommand
        {
            get
            {
                return gotoNewPersonCommand ??= new RelayCommand<object>(_ => GotoLogin());
            }
        }
        public RelayCommand<object> FilterBirthdayCommand
        {
            get
            {
                return new RelayCommand<object>(_ => FilterBirthday());
            }
        }
        public RelayCommand<object> ChangeSelectedCommand
        {
            get
            {
                return changeSelectedCommand ??= new RelayCommand<object>(_ => GoToChangingWindow(), CanExecuteEditOrRemoveSelected);
            }
        }
        public RelayCommand<object> ExitCommand
        {
            get
            {
                return exitCommand ??= new RelayCommand<object>(o => Close());
            }
        }
        public RelayCommand<object> FilterAdultsCommand
        {
            get
            {
                return filterAdultsCommand ??= new RelayCommand<object>(o => FilterAdults());
            }
        }
        public RelayCommand<object> CancelFilterCommand
        {
            get
            {
                return cancelFilterCommand ??= new RelayCommand<object>(o => CancelFilter());
            }
        }
        public RelayCommand<object> RemovePersonCommand
        {
            get
            {
                return removePersonCommand ??= new RelayCommand<object>(o => RemovePerson(), CanExecuteEditOrRemoveSelected);
            }
        }
        public PeopleListViewModel(Action gotoLogin, Action<EditPersonViewModel> gotoPerson, Action gotoInfo)
        {
            this.gotoLogin = gotoLogin;
            this.gotoPerson = gotoPerson;
            personRepository = new PersonRepository();
            people = new ObservableCollection<EditPersonViewModel>(personRepository.GetAllPersons(gotoInfo));
            if (people.Count == 0)
            {
                List<Person> newPeople = new List<Person>();

                newPeople.Add(new Person { FirstName = "John", LastName = "Doe", Email = "johndoe@example.com", Birthday = new DateTime(2021, 1, 1) });
                newPeople.Add(new Person { FirstName = "Jane", LastName = "Doe", Email = "janedoe@example.com", Birthday = new DateTime(1985, 2, 2) });
                newPeople.Add(new Person { FirstName = "Bob", LastName = "Smith", Email = "bobsmith@example.com", Birthday = new DateTime(1990, 3, 3) });
                newPeople.Add(new Person { FirstName = "Alice", LastName = "Johnson", Email = "alicejohnson@example.com", Birthday = new DateTime(1982, 4, 4) });
                newPeople.Add(new Person { FirstName = "David", LastName = "Lee", Email = "davidlee@example.com", Birthday = new DateTime(1979, 5, 5) });
                newPeople.Add(new Person { FirstName = "Emily", LastName = "Wang", Email = "emilywang@example.com", Birthday = new DateTime(2006, 6, 6) });
                newPeople.Add(new Person { FirstName = "Jack", LastName = "Chan", Email = "jackchan@example.com", Birthday = new DateTime(1992, 7, 7) });
                newPeople.Add(new Person { FirstName = "Megan", LastName = "Davis", Email = "megandavis@example.com", Birthday = new DateTime(2001, 8, 8) });
                newPeople.Add(new Person { FirstName = "Michael", LastName = "Johnson", Email = "michaeljohnson@example.com", Birthday = new DateTime(1995, 9, 9) });
                newPeople.Add(new Person { FirstName = "Sophia", LastName = "Brown", Email = "sophiabrown@example.com", Birthday = new DateTime(1984, 10, 10) });
                newPeople.Add(new Person { FirstName = "James", LastName = "Wilson", Email = "jameswilson@example.com", Birthday = new DateTime(1989, 11, 11) });
                newPeople.Add(new Person { FirstName = "Lily", LastName = "Anderson", Email = "lilyanderson@example.com", Birthday = new DateTime(1986, 12, 12) });
                newPeople.Add(new Person { FirstName = "Andrew", LastName = "Thompson", Email = "andrewthompson@example.com", Birthday = new DateTime(2003, 1, 13) });
                newPeople.Add(new Person { FirstName = "Olivia", LastName = "Taylor", Email = "oliviataylor@example.com", Birthday = new DateTime(1981, 2, 14) });
                newPeople.Add(new Person { FirstName = "William", LastName = "Harris", Email = "williamharris@example.com", Birthday = new DateTime(1994, 3, 15) });
                newPeople.Add(new Person { FirstName = "Isabella", LastName = "Martin", Email = "isabellamartin@example.com", Birthday = new DateTime(2000, 4, 16) });
                newPeople.ForEach((person) => 
                {
                    person.CalculateDataSync();
                    personRepository.AddToRepositoryOrUpdateSync(person);
                });
                people = new ObservableCollection<EditPersonViewModel>(personRepository.GetAllPersons(gotoInfo));
            }
            gridPeople = new ObservableCollection<EditPersonViewModel>(personRepository.GetAllPersons(gotoInfo));
        }
        private void FilterAdults()
        {
            GridPeople = new ObservableCollection<EditPersonViewModel>(People.Where(p => p.IsAdult).ToList());
        }
        private void CancelFilter()
        {
            GridPeople = new ObservableCollection<EditPersonViewModel>(People);
        }

        private void FilterBirthday()
        {
            GridPeople = new ObservableCollection<EditPersonViewModel>(People.Where(p => p.IsBirthday).ToList());
        }
        private void GotoLogin()
        {
            gotoLogin.Invoke();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void GoToChangingWindow()
        {
            gotoPerson.Invoke(SelectedPerson);
        }
        private void Close()
        {
            Environment.Exit(0);
        }
        private async Task RemovePerson()
        {
            if (SelectedPerson != null)
            {
                await Task.Run(() => personRepository.RemoveFromRepository(SelectedPerson.Person));
                People.Remove(SelectedPerson);
                OnPropertyChanged(nameof(people));
                GridPeople.Remove(SelectedPerson);
                OnPropertyChanged(nameof(gridPeople));
            }
        }
        private bool CanExecuteEditOrRemoveSelected(object o)
        {
            return SelectedPerson != null;
        }
    }
}
