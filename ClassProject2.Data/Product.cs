using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassProject2.Data
{
    public class Product : IValidatableObject
    {
        public Product ( int id )
        {
            Id = id;
        }

        public int Id { get; internal set; }
               
        [Required(AllowEmptyStrings = true)]  
        //[Required()]
        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value; }
        }

        public bool IsDiscontinued { get; set; }

        [Range(0.01, (double)Decimal.MaxValue
            , ErrorMessage = "Price must be > 0")]
        public decimal UnitPrice { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }

        private string _name;
    }
}
