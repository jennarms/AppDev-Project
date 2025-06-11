using System;
using System.ComponentModel.DataAnnotations;

namespace MetroLayag.Models
{
    public class Passenger
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string StartingStation { get; set; }

        [Required]
        public string Destination { get; set; }

        public bool IsCanceled { get; set; } = false;


        public bool HasDisembarked { get; set; } = false;

        public DateTime BookingDate { get; set; } = DateTime.Now;

        
    }
}
