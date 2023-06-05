using DataBaseModel.Data;
using DataBaseModel.Model;
using DataBaseModel.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataBaseModel.Repository;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _context;
    public MessageRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<int> Add(MessageModel Message)
    {
        try
        {
            var returnModel = await _context.Messages.AddAsync(Message);
            await _context.SaveChangesAsync();
            return returnModel.Entity.Id;
        }
        catch (Exception e)
        {
            throw new Exception("Error on create Message: " + e.Message);
        }
    }

    public async Task<List<MessageModel>> GetAllAsync()
    {
        return await _context.Messages
            .AsNoTracking()
            .ToListAsync();
    }
}
