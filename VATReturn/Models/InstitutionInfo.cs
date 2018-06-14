using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VATReturn.Models
{
    [Validator(typeof(PlaceValidator))]
    public class InstitutionInfo
    {
        public int Id { get; set; }

        [Required]
        [MinLength(11)]
        [Display(Name = "করদাতা সনাক্তকরণ সংখ্যা")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Tax Id must be numeric")]
        public string TaxId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "নাম")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "ঠিকানা")]
        public string Address { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "টেলিফোন নাম্বার")]
        public string PhoneNumber { get; set; }

        [Display(Name = "কার্যক্রম কোড")]
        public string ActivitiesArea { get; set; }

        [Display(Name = "এলাকা কোড")]
        public string AreaCode { get; set; }
    }

    public class PlaceValidator : AbstractValidator<InstitutionInfo>
    {
        public PlaceValidator()
        {
            RuleFor(x => x.TaxId).Must(BeUniqueUrl).WithMessage("Tax Id already exists");
        }

        private bool BeUniqueUrl(string url)
        {
            return new ApplicationDbContext().InstitutionInfos.FirstOrDefault(x => x.TaxId == url) == null;
        }
    }
}