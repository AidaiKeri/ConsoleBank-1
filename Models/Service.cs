using ConsoleBank;
using ConsoleBank_1.Enum;
using ConsoleBank_1.Interfeces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleBank_1.Models
{
    /// <summary>
    /// Модель различных услуг обслуживания клиентов
    /// </summary>
    public class Service : IEntity<int>
    {
        public int Id { get; set; }
        public string ClientLogin { get; set; }
        public Services Services { get; set; }
        public decimal Sum { get; set; }
        public bool IsPaid { get; set; }
        public DateTime ServiceAddDate { get; set; }
        public DateTime PaidDate { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
