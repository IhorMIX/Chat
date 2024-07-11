namespace Chat.BLL.Service.Interface;

public interface IBasicService<TModel> where TModel : class
{
    Task<TModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}