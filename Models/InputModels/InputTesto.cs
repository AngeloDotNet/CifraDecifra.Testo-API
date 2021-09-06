using System.ComponentModel.DataAnnotations;

namespace API_Cifra_Decifra_Testo.Models.InputModels
{
    public class InputTesto
    {
        [Required]
        public string Testo { get; set; }
    }
}