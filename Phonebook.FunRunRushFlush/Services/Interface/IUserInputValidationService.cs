using Phonebook.FunRunRushFlush.Data.Model;

namespace Phonebook.FunRunRushFlush.Services.Interface
{
    public interface IUserInputValidationService
    {
        PhonebookTable ValidateUserInput(PhonebookTable? existingEntry = null);
    }
}