namespace SoftUniFest.Data.Models
{
    using SoftUniFest.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Trader : BaseDeletableModel<string>
    {
        public Trader()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
