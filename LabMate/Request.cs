using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LabMate
{
    public class Request
    {
        [Required]
        public string LabRefNo { get; set; }
        [Required]
        public string HcNo { get; set; }
        public string HospitalNo { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Forename { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public string Dob { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string PostCode { get; set; }
        public string Hospital { get; set; }
        public string ConsGp { get; set; }
        public string Ward { get; set; }
        public string Specialty { get; set; }
        [Required]
        public string SpecimenType { get; set; }
        [Required]
        public string SpecimenDate { get; set; }
        [Required]
        public string RecdDate { get; set; }
        public string FlagCode { get; set; }
        [Required]
        public string Urgency { get; set; }


        // CHECK IF PROPERTY IS REQUIRED //
        public bool IsPropertyRequired(string propertyName)
        {
            var propertyInfo = GetType().GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' does not exist on type '{GetType().Name}'.", nameof(propertyName));
            }

            var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), true).FirstOrDefault() as RequiredAttribute;
            return requiredAttribute != null;
        }
    }
}
