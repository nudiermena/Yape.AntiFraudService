using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace AntiFraudService.Domain.AntiFraud
{
    public class AntiFraud
    {
        public AntiFraud(Guid sourceAccountId, decimal value, DateTime createdAt)
        {
            SourceAccountId = sourceAccountId;            
            Value = value;
            CreatedAt = createdAt;
        }

        [Column("id")]
        [Key]
        public int Id { get;}
        [Column("sourceaccountid")]
        public Guid SourceAccountId { get; set; }
        [Column("value")]
        public decimal Value { get; set; }
        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}