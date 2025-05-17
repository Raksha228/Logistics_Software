using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Backend.BusinessLogic
{
    public class DealerCustomer
    {
        private string name;
        private string email;
        private string contact;
        public int Id { get; set; }
        public string Type { get; set; }
        
        public string Name
        {
            get => this.name;
            set
            {
                //Проверка имени
                if (value.Length <= 2)
                {
                    throw new ArgumentException(@"Invalid name!");
                }

                this.name = value;
            }
        }
        public string Email
        {
            get => this.email;
            set
            {
                //проверка на emailа
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
        public string Contact
        {
            //Провекра на контакты
            get => this.contact;
            set
            {
                if (value.Length <= 9)
                {
                    throw new ArgumentException(@"Invalid contact!");
                }

                this.contact = value;
            }
        }
        public string Address { get; set; }
        public DateTime AddedDate { get; set; }
        public int AddedBy { get; set; }
        public string AddedByName { get; set; }
    }
}
