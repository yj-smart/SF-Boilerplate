using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SF.Entitys.Abstraction
{
    /// <summary>
    /// 有效的对象
    /// </summary>
    public abstract class ValidatableObject
    {
        public virtual bool IsValid()
        {
            return this.ValidationResults().Count == 0;
        }

        public virtual IList<ValidationResult> ValidationResults()
        {
            IList<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults, true);
            return validationResults;
        }
    }
}
