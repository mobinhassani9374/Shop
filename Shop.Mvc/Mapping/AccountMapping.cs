using Shop.Domain.Dto.Account;
using Shop.Mvc.Models.Account;

namespace Shop.Mvc.Mapping
{
    public static class AccountMapping
    {
        public static RegisterDto ToDto(this RegisterViewModel source)
        {
            return new RegisterDto
            {
                ConfirmPasswprd = source.ConfirmPasswprd,
                FullName = source.FullName,
                Password = source.Password,
                PhoneNumber = source.PhoneNumber
            };
        }
    }
}
