using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models.HistotyDisplay
{
    public class start_date
    {
        [NotMapped]
        public string month {  get; set; }
        [NotMapped]
        public string day { get; set; }
        [NotMapped]
        public string year {  get; set; }
    }
}
