using Microsoft.EntityFrameworkCore;
using Phonebook.FunRunRushFlush.Data.Database;
using Phonebook.FunRunRushFlush.Data.Model;

namespace Phonebook.FunRunRushFlush.Data.DataAccess;

public class PhonebookDataAccess
{
    private AppDbContext _dbContext;

    public PhonebookDataAccess(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PhonebookTable>> GetAllPhonebookData()
    {
        var result = await _dbContext.PhonebookTable.ToListAsync();
        return result;
    }
    public async Task InsertPhonebookData(PhonebookTable phonebook)
    {
        await _dbContext.PhonebookTable.AddAsync(phonebook);
        var result = await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePhonebookData(PhonebookTable phonebook)
    {
        var existingEntity = await _dbContext.PhonebookTable.FindAsync(phonebook.Id);

        if (existingEntity != null)
        {
            existingEntity.Name = phonebook.Name;
            existingEntity.Email = phonebook.Email;
            existingEntity.PhoneNumber = phonebook.PhoneNumber;

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task DeletePhonebookData(PhonebookTable phonebook)
    {
        var entityToDelete = await _dbContext.PhonebookTable.FindAsync(phonebook.Id);
        if (entityToDelete != null)
        {
            _dbContext.PhonebookTable.Remove(entityToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

}