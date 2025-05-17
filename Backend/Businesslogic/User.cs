using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backend.BusinesLogic
{
    public class User
    {
        private string firstName;
        private string lastName;
        private string email;
        private string username;
        private string password;

        public int Id { get; set; }
        public string FirstName
        {
            get => this.firstName;
            set
            {
                //Проверка подлинности первого имени
                if (value.Length <= 3)
                {
                    throw new ArgumentException("Invalid first name!");
                }

                this.firstName = value;
            }
        }
        public string LastName
        {
            get => this.lastName;
            set
            {
                //Проверка фамилии
                if (value.Length <= 3)
                {
                    throw new ArgumentException("Invalid last name!");
                }

                this.lastName = value;
            }
        }
        public string Email
        {
            get => this.email;
            set
            {
                //Проверка emaila
                Regex regex = new Regex(@"^([\w-.]+)@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.)|(([\w-]+.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(]?)$");
                Match match = regex.Match(value);
                if (match.Success)
                {
                    this.email = value;
                }
                else
                {
                    throw new ArgumentException(@"Invalid email!");
                }
            }
        }
        public string Username
        {
            get => this.username;
            set
            {
                //Проверка имени пользователя
                if (value.Length < 3)
                {
                    throw new ArgumentException("Invalid username!");
                }

                this.username = value;
            }
        }
        public string Password
        {
            get => this.password;
            set
            {
                //Проверка пароля
                if (value.Length < 5)
                {
                    throw new ArgumentException("Invalid password!");
                }

                this.password = value;
            }
        }

        public string Address { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }
        public string UserType { get; set; }
        public DateTime AddedDate { get; set; }
        public int AddedBy { get; set; }
        public string AddedByName { get; set; }
    }

}
