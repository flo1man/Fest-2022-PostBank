namespace SoftUniFest.Web.ViewModels.Discounts
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SoftUniFest.Common;

    public class DiscountInputModel
    {
        [Required]
        [Range(DataConstants.MinPercentage, DataConstants.MaxPercentage, ErrorMessage = "The percentage should be between 0 and 100")]
        public int Percentage { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}
