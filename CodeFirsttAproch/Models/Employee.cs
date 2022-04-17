using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirsttAproch.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Rollno { get; set; }
        [Required]
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

    }
}
