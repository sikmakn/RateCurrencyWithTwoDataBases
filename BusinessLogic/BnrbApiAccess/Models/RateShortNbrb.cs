using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class RateShortNbrb
    {
        public int Cur_ID { get; set; }
        [Key]
        public DateTime Date { get; set; }
        public decimal? Cur_OfficialRate { get; set; }
    }
}
