using Lab2.Models;
using Lab2.Navigation;
using Lab2.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab2.ViewModels
{

    public class EditPersonViewModel : INotifyPropertyChanged, NavigationInProject
    {
        private Person person;
        private PersonRepository personRepository = new PersonRepository();
        private RelayCommand<object> cancelCommand;
        private RelayCommand<object> gotoInfoCommand;
        private Action gotoList;
        private DateTime changedBirthday = DateTime.Today;
        public event PropertyChangedEventHandler PropertyChanged;

        public NavigationTypes ViewType => NavigationTypes.EditPerson;

        public Person Person
        {
            get { return person; }
        }
        public string ChangedFirstName { get; set; }
        public string ChangedLastName { get; set; }
        public string ChangedEmail { get; set; }
        public DateTime ChangedBirthday
        {
            get { return changedBirthday; }
            set { changedBirthday = value; }
        }
        public string FirstName
        {
            get { return person.FirstName; }
        }
        public string LastName
        {
            get { return person.LastName; }
        }
        public string Email
        {
            get { return person.Email; }
        }
        public DateTime Birthday
        {
            get { return person.Birthday; }
        }
        public bool IsAdult
        {
            get { return person.IsAdult; }
        }
        public bool IsBirthday
        {
            get { return person.IsBirthday; }
        }
        public string ChineseSign
        {
            get { return person.ChineseSign; }

        }
        public string SunSign
        {
            get { return person.SunSign; }
        }
        public string BirthdayInString
        {
            get { return person.Birthday.ToShortDateString(); }
        }
        public EditPersonViewModel(Person person, Action gotoList)
        {
            this.person = person;
            this.gotoList = gotoList;
            ChangedFirstName = person.FirstName;
            ChangedLastName = person.LastName;
            ChangedEmail = person.Email;
        }


        private async Task Proceed()
        {
            Person changedPerson = new Person(ChangedFirstName, ChangedLastName, ChangedEmail, ChangedBirthday);
            try
            {               
                await changedPerson.CalculateData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }

            if (ChangedEmail != Email)
            {
                var existingEmail = await personRepository.GetPersonAsync(ChangedEmail);
                if (existingEmail != null)
                {
                    MessageBox.Show($"Error: This email exists");
                    return;
                } 
                else
                {
                    personRepository.RemoveFromRepository(person);
                }
            }

            await personRepository.AddToRepositoryOrUpdateAsync(changedPerson);
            gotoList.Invoke();
        }
        public RelayCommand<object> CancelCommand
        {
            get
            {
                return cancelCommand ??= new RelayCommand<object>(o => Cancel());
            }
        }
        public RelayCommand<object> GotoInfoCommand
        {
            get
            {
                return gotoInfoCommand ??= new RelayCommand<object>(_ => Proceed(), CanExecute);
            }
        }
        private void Cancel()
        {
            gotoList.Invoke();
        }
        private bool CanExecute(object o)
        {
            return !string.IsNullOrWhiteSpace(ChangedFirstName) && !string.IsNullOrWhiteSpace(ChangedLastName) && !string.IsNullOrWhiteSpace(ChangedEmail);
        }
    }
}
