using ArxOne.MrAdvice.Advice;
using FluentValidation.Results;

namespace Infrastructure.FluentValidation
{
    public class ValidationBehavior : Attribute, IMethodAdvice
    {
        private readonly Type _validatorType;
        public ValidationBehavior(Type validatorType) => _validatorType = validatorType;
        public void Advise(MethodAdviceContext context)
        {
            if (context.Arguments.Any())
            {
                var requestModel = context.Arguments[0];

                var validateMethod = _validatorType.GetMethod("Validate", new Type[] { requestModel.GetType() });
                var validatorInstance = Activator.CreateInstance(_validatorType);
                if (validateMethod != null)
                {
                    var validationResult = (ValidationResult)validateMethod.Invoke(validatorInstance, new object[] { requestModel });
                    if (!validationResult.IsValid)
                    {
                        foreach (var item in validationResult.Errors)
                        {
                            List<string> strings = new List<string>();

                            strings.Add(item.ErrorMessage);

                            throw new Exception($"{string.Join(" , ", strings)}");
                        }
                    }
                }
            }
            context.Proceed(); //Metod tetikleniyor
        }
    }
}