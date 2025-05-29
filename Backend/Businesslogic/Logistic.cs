using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic
{
    public class Logistic
    {
        private string contact;

        public int Id { get; set; }
        public string Empleyee { get; set; }
        public string FirstNameEmployee { get; set; }
        public string LastNameEmployee { get; set; }
        public string Contact
        {
            get => this.contact;
            set
            {
                //Проверка контакта
                if (value.Length <= 9)
                {
                    throw new ArgumentException(@"Invalid contact!");
                }

                this.contact = value;
            }
        }

        public string Address { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime AddedDate { get; set; }
        public int AddedBy { get; set; }
        public string AddedByName { get; set; }
    }
}
