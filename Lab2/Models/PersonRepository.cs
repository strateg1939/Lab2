using Lab2.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class PersonRepository
    {
        private static readonly string MainFolder = Path.Combine(Environment.CurrentDirectory, "dataStore");

        public async Task AddToRepositoryOrUpdateAsync(Person person)
        {
            var personInString = JsonSerializer.Serialize(person);
            using (var writer = new StreamWriter(Path.Combine(MainFolder, person.Email), false))
            {
                await writer.WriteAsync(personInString);
            }

        }

        public void AddToRepositoryOrUpdateSync(Person person)
        {
            var personInString = JsonSerializer.Serialize(person);
            using (var writer = new StreamWriter(Path.Combine(MainFolder, person.Email), false))
            {
                writer.Write(personInString);
            }
        }


        public void RemoveFromRepository(Person person)
        {
            File.Delete(Path.Combine(MainFolder, person.Email));
        }
        public List<EditPersonViewModel> GetAllPersons(Action gotoInfo)
        {
            var persons = new List<EditPersonViewModel>();
            foreach (var file in Directory.EnumerateFiles(MainFolder))
            {
                string personInString = null;
                using (var reader = new StreamReader(file))
                {
                    personInString = reader.ReadToEnd();
                }
                if (personInString != null)
                {
                    var person = JsonSerializer.Deserialize<Person>(personInString);
                    persons.Add(new EditPersonViewModel(person, gotoInfo));
                }
            }
            return persons;
        }

        public async Task<Person?> GetPersonAsync(string email)
        {
            string personInString = null;
            string path = Path.Combine(MainFolder, email);
            if (!File.Exists(path))
            {
                return null;
            }
            using (var reader = new StreamReader(path))
            {
                personInString = await reader.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<Person>(personInString);
        }
    }
}
