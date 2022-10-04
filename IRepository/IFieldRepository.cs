using BookStoreManage.DTO;
using BookStoreManage.Entity;

namespace BookStoreManage.IRepository
{
    public interface IFieldRepository
    {
        void CreateField(FieldDTO field);

        void EditField(int fieldID, FieldDTO field);

        void DeleteField(int idField);

        Task<List<Field>> getAllField();

        Task<Field> getByID(int idField);

        Task<List<Field>> getByName(string fieldName);
    }
}
