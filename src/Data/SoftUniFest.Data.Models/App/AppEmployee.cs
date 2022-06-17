namespace SoftUniFest.Data.Models.App
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoftUniFest.Data.Common.Models;

    public class AppEmployee : BaseDeletableModel<string>
    {
        public AppEmployee()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
