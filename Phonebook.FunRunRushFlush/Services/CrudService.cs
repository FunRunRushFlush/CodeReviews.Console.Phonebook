using Microsoft.Extensions.Logging;
using Phonebook.FunRunRushFlush.Data.DataAccess;
using Phonebook.FunRunRushFlush.Data.Model;
using Phonebook.FunRunRushFlush.Services.Interface;

namespace Phonebook.FunRunRushFlush.Services;

public class CrudService : ICrudService
{
    private ILogger<CrudService> _log;
    private readonly PhonebookDataAccess _phonebook;

    public CrudService(ILogger<CrudService> log, PhonebookDataAccess phonebook)
    {
        _log = log;
        _phonebook = phonebook;
    }

    public async Task<List<PhonebookTable>> ShowAllPhonebookData()
    {
        try
        {
            var res = await _phonebook.GetAllPhonebookData();
            return res;
        }
        catch (Exception ex)
        {
            _log.LogError(ex.Message, ex);
            return new List<PhonebookTable>();
        }
    }
    public async Task AddPhonebookData(PhonebookTable phonebook)
    {
        try
        {
            await _phonebook.InsertPhonebookData(phonebook);
        }
        catch (Exception ex)
        {
            _log.LogError(ex.Message, ex);
        }
    }
    public async Task UpdatePhonebookData(PhonebookTable phonebook)
    {
        try
        {
            await _phonebook.UpdatePhonebookData(phonebook);
        }
        catch (Exception ex)
        {
            _log.LogError(ex.Message, ex);
        }
    }
    public async Task DeletePhonebookData(PhonebookTable phonebook)
    {
        try
        {
            await _phonebook.DeletePhonebookData(phonebook);
        }
        catch (Exception ex)
        {
            _log.LogError(ex.Message, ex);
        }
    }
}