using Microsoft.AspNetCore.Identity;

namespace Admin.Panel.Web.Servises
{
    public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "Необходимаодна минимум одна цифра в пароле."
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "Необходима минимум одна заглавная буква (A-Z)."
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = "Необходима минимум одна строчная буква (a-z)."
            };
        }
    }
}