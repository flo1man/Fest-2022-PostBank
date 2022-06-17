﻿namespace SoftUniFest.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SoftUniFest.Data.Common.Models;

    public class Trader : BaseDeletableModel<string>
    {
        public Trader()
        {
            this.Id = Guid.NewGuid().ToString();
            this.POSTerminals = new HashSet<POSTerminal>();
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

        public virtual ICollection<POSTerminal> POSTerminals { get; set; }
    }
}
