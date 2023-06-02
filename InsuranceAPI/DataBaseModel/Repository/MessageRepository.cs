using DataBaseModel.Data;
using DataBaseModel.Model;
using DataBaseModel.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataBaseModel.Repository;

public class MessageRepository: IMessageRepository
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
            var returnId = await _context.Messages.AddAsync(Message);
            await _context.SaveChangesAsync();
            return returnId.Entity.Id;
        }
        catch (Exception e)
        {
            throw new Exception("Error on create Movie: " + e.Message);
        }
    }

    public async Task<List<MessageModel>> GetAllAsync()
    {
        return await _context.Messages
            .AsNoTracking()
            .ToListAsync();
    }
}
