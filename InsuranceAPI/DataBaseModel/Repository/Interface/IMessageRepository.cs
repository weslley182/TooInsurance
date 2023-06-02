using DataBaseModel.Model;

namespace DataBaseModel.Repository.Interface;

public interface IMessageRepository
{
    Task<List<MessageModel>> GetAllAsync();
    Task Add(MessageModel message);
}
