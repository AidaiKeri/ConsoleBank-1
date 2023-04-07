using ConsoleBank_1.Interfeces;
using System;
using System.ComponentModel.DataAnnotations;

namespace ConsoleBank
{
    /// <summary>
    /// Абстрактный класс Person для моделей людей
    /// </summary>
    public abstract class Person : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Login { get; set; }

        [MaxLength(10)]
        [Required]
        public string Password { get; set; }

        [MaxLength(10)]
        public string ConfirmPassword { get; set; }

        [MaxLength(15)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(20)]
        [Required]
        public string LastName { get; set; }
        public DateTime DateOfCreate { get; set; }
    }
}
