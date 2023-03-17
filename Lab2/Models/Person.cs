using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab2.Models
{
    public class Person
    {
        private DateTime birthday;
        private string chineseSign;
        private string sunSign;
        private bool isAdult;
        private bool isBirthday;
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday
        {
            get => birthday;

            set => birthday = value;
        }
        public bool IsAdult
        {
            get => isAdult;
        }
        public bool IsBirthday
        {
            get => isBirthday;
        }
        public string ChineseSign
        {
            get => chineseSign;
            
        }
        public string SunSign
        {
            get => sunSign;
        }
        public int Age { get; set; }

        public Person(string firstName, string lastName, string email, DateTime birthday)
        {
            
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Birthday = birthday;
        }
        public Person(string firstName, string lastName, string email)
        {

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public Person(string firstName, string lastName, DateTime birthday)
        {

            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        public async Task CalculateData()
        {
            Task<int> t1 = Task.Run(() => GetAge(Birthday));
            Task<string> t2 = Task.Run(() => GetWesternZodiac(Birthday));
            Task<string> t3 = Task.Run(() => GetChineezeZodiac(Birthday));
            Task<bool> t4 = Task.Run(() => CalculateIsBirthday(Birthday));


            Age = await t1;
            Task<bool> t5 = Task.Run(() => CalculateIsAdult(Age));
            sunSign = await t2;
            chineseSign = await t3;
            isBirthday = await t4;
            isAdult = await t5;       
        }
        private int GetAge(DateTime birthday)
        {
            if (DateTime.Today.Month < birthday.Month || (DateTime.Today.Month == birthday.Month && DateTime.Today.Day < birthday.Day))
            {
                return DateTime.Today.Year - birthday.Year - 1;
            }
            return DateTime.Today.Year - birthday.Year;
        }

        private string GetWesternZodiac(DateTime dt)
        {
            switch (dt.Month)
            {
                case 1:
                    if (dt.Day < 21)
                        return "Capricorn";
                    else
                        return "Aquarius";

                case 2:
                    if (dt.Day < 20)
                        return "Aquarius";
                    else
                        return "Pisces";

                case 3:
                    if (dt.Day < 21)
                        return "Pisces";
                    else
                        return "Aries";

                case 4:
                    if (dt.Day < 21)
                        return "Aries";
                    else
                        return "Taurus";

                case 5:
                    if (dt.Day < 22)
                        return "Taurus";
                    else
                        return "Gemini";

                case 6:
                    if (dt.Day < 22)
                        return "Gemini";
                    else
                        return "Cancer";

                case 7:
                    if (dt.Day < 23)
                        return "Cancer";
                    else
                        return "Leo";

                case 8:
                    if (dt.Day < 22)
                        return "Leo";
                    else
                        return "Virgo";

                case 9:
                    if (dt.Day < 24)
                        return "Virgo";
                    else
                        return "Libra";

                case 10:
                    if (dt.Day < 24)
                        return "Libra";
                    else
                        return "Scorpio";

                case 11:
                    if (dt.Day < 24)
                        return "Scorpio";
                    else
                        return "Sagittarius";     

                case 12:
                    if (dt.Day < 23)
                        return "Sagittarius";
                    else
                        return "Capricorn";
                default:
                    return "Unknown";
            }
        }
        private string GetChineezeZodiac(DateTime dt)
        {
            switch ((dt.Year - 4) % 12)
            {
                case 0:
                    return "Rat";

                case 1:
                    return "Ox";

                case 2:
                    return "Tiger";

                case 3:
                    return "Rabbit";

                case 4:
                    return "Dragon";

                case 5:
                    return "Snake";      

                case 6:
                    return "Horse";      

                case 7:
                    return "Goat";

                case 8:
                    return "Monkey";

                case 9:
                    return "Rooster";
                case 10:
                    return "Dog";
                    
                case 11:
                    return "Pig";
                default:
                    return "Unknown";
            }
        }

        private bool CalculateIsAdult(int age)
        {
            return age >= 18;
        }
        private bool CalculateIsBirthday(DateTime birthday)
        {
            return birthday.Day == DateTime.Today.Day && birthday.Month == DateTime.Today.Month;
        }
    }
}

