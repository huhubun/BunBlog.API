using FluentValidation;
using System.Text.RegularExpressions;

namespace BunBlog.API.Validators
{
    public static class CommonValidatorOption
    {
        public static IRuleBuilderOptions<T, string> LinkName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(str =>
                {
                    var regexPattern = @"^[a-zA-Z\-_][a-zA-Z0-9\-_]*$";
                    var regex = new Regex(regexPattern);

                    return regex.IsMatch(str);
                }).WithMessage("'{PropertyName}' 只能包含英文字母、数字、下划线(_)和段杠(-)，并且不能是纯数字。");
        }
    }
}
