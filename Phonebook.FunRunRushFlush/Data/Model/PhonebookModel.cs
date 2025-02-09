using System.ComponentModel.DataAnnotations;

namespace Phonebook.FunRunRushFlush.Data.Model;

public class PhonebookTable
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}