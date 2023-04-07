using ConsoleBank_1.Enum;
using ConsoleBank_1.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleBank
{
    /// <summary>
    /// Модель клиента банкинга
    /// </summary>
    public class Client : Person
    {
        [MinLength(0)]
        public decimal Money { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9)]
        public string PhoneNumber { get; set; }

        public Role Role = Role.User;
        public Bank Bank { get; set; }
        public ICollection<Service> Service { get; set; }
    }
}
