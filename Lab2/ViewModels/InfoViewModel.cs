using Lab2.Models;
using Lab2.Navigation;
using Lab2.Tools;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Lab2.ViewModels
{
    public class InfoViewModel : NavigationInProject, INotifyPropertyChanged
    {
        private Person _person;
        public NavigationTypes ViewType => NavigationTypes.Info;

        public event PropertyChangedEventHandler? PropertyChanged;

        public InfoViewModel(Person person)
        {
            _person = person;
        }

        public string Email => _person.Email;
        public string FirstName => _person.FirstName;
        public string LastName => _person.LastName;
        public string IsBirthday => _person.IsBirthday.ToString();
        public string IsAdult => _person.IsAdult.ToString();
        public string ChineseSign => _person.ChineseSign;
        public string SunSign => _person.SunSign;

        public string BirthDate => _person.Birthday.ToShortDateString();
    }
}
