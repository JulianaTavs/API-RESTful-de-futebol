using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_RESTful_de_futebol.Models
{
    [Table("Teams")]
    public class Team
    {
        // Usa a propriedade Id para a chave primária
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Impede que o EF Core gere o ID
        public int Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        
        [MaxLength(255)]
        public string Country { get; set; }
    }
}