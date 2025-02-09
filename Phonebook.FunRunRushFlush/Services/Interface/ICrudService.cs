using Phonebook.FunRunRushFlush.Data.Model;

namespace Phonebook.FunRunRushFlush.Services.Interface
{
    public interface ICrudService
    {
        Task AddPhonebookData(PhonebookTable phonebook);
        Task DeletePhonebookData(PhonebookTable phonebook);
        Task<List<PhonebookTable>> ShowAllPhonebookData();
        Task UpdatePhonebookData(PhonebookTable phonebook);
    }
}