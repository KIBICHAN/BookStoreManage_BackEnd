using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository
{
    public interface IFieldRepository
    {
        Task CreateField(FieldDTO field);
        Task EditField(int fieldID, FieldDTO field);
        Task DeleteField(int idField);
        Task<List<Field>> getAllField();

        Task<Field> getByIDTest(int idField);
        Task<List<FieldBook>> getByID(int idField);
        Task<List<Field>> getByName(string fieldName);
        IEnumerable<Field> getFiveRows();
    }
}
