using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models.HistotyDisplay
{
    public class text
    {
        [NotMapped]
        public string headline { get; set; }
        [NotMapped]
        public string texts {  get; set; }

    }
}
