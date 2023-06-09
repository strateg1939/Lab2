﻿using System;
using Lab2.Tools;
using Lab2.Models;
using System.Windows;
using System.ComponentModel;
using Lab2.Navigation;
using System.Threading.Tasks;
using Lab2.AppException;

namespace Lab2.ViewModels
{
    public class NewPersonViewModel : INotifyPropertyChanged, NavigationInProject
    {
        private RelayCommand<object> gotoInfoCommand;
        private RelayCommand<object> cancelCommand;
        private Person newPerson;
        private Action<Person> gotoInfo;
        public event PropertyChangedEventHandler? PropertyChanged;
        public NavigationTypes ViewType => NavigationTypes.NewPerson;
        private PersonRepository personRepository = new PersonRepository();


        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RelayCommand<object> ProceedCommand
        {
            get
            {
                return gotoInfoCommand ??= new RelayCommand<object>(_ => Proceed(), CanExecute);
            }
        }

        public NewPersonViewModel(Action<Person> gotoInfo)
        {
            BirthDate = DateTime.Now;
            this.gotoInfo = gotoInfo;
        }

        private async Task Proceed()
        {
            newPerson = new Person(FirstName, LastName, Email, BirthDate);
            try
            {
                await newPerson.CalculateData();
            } 
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            var existingEmail = await personRepository.GetPersonAsync(Email);
            if (existingEmail != null)
            {
                MessageBox.Show($"Error: This email exists");
                return;
            }
            if (newPerson.IsBirthday)
            {
                MessageBox.Show("Happy birthday to you!");
            }


            await personRepository.AddToRepositoryOrUpdateAsync(newPerson);
            gotoInfo.Invoke(newPerson);
        }
        private bool CanExecute(object o)
        {
            return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(Email);
        }
    }
}
