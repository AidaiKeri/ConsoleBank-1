using ConsoleBank;
using ConsoleBank_1.Interfeces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleBank_1.Models
{
    /// <summary>
    /// Модель истории транзакций
    /// </summary>
    public class HistoryOfTransaction : IEntity<int>
    {
        public int Id { get; set; }
        [Required]
        public string NameOfTransaction { get; set; }
        public string Sender { get; set; }
        public string SenderBank { get; set; }
        public string Recipient { get; set; }
        public string RecipientBank { get; set; }
        [Required]
        public decimal Sum { get; set; }
        public double Commission { get; set; }
        public DateTime TransDateTime { get; set; }

        [ForeignKey("Client")]
        public int ClientId{ get; set; }
        public virtual Client Client { get; set; }
    }
}
