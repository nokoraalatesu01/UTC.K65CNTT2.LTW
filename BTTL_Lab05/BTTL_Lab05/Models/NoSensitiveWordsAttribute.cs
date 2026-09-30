using System.ComponentModel.DataAnnotations;
namespace BTTL_Lab05.Models
{
    public class NoSensitiveWordsAttribute :ValidationAttribute
    {
        private readonly string[] _sensitiveWords = new string[] { "die", "duma", "wtf" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string input = value.ToString().ToLower();
                foreach (string word in _sensitiveWords)
                {
                    if (input.Contains(word))
                    {
                        return new ValidationResult($"Mô tả {validationContext.DisplayName} là từ ngữ thô tục.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
