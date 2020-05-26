using WebSCADA.Domain.Models;

namespace WebSCADA.BLL.Interfaces
{
    public interface ISchemaService
    {
        void AddNewSchema(SchemaDomain schemaRequest);

        void SaveSchema(string name, SchemaDomain schema);

        SchemaDomain GetSchema(string name);

        void DeleteSchema(string name);
    }
}
