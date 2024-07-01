
using System.Data;
public interface IDatabaseInterface
{
    void Insert(string tableName, Dictionary<string, object> columns);
        
    void Update(string tableName, Dictionary<string, object> columns, string whereClause);
        
    DataTable Select(string tableName, string[] columns, string whereClause);
    DataTable Select(string tableName, string[] columns);

    void Delete(string tableName, string whereClause);
}
