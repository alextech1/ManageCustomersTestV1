using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassProject2.Data;

namespace Nile.Windows
{
    public class SelectedCustomer : IValidatableObject
    {
        public int Id { get; internal set; }

        [Required(AllowEmptyStrings = true)]

        public string Customer
        {
            get { return _firstName ?? ""; }
            set { _firstName = value; }
        }

        public bool IsDiscontinued { get; set; }

        [Range(0.01, (double)Decimal.MaxValue
            , ErrorMessage = "Price must be ? 0")]
        public decimal UnitPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }

        private string _firstName;
    }
}
