using Domain.Models;
using HotChocolate.Types;

namespace Domain.GraphQlModels
{
    /// <summary>
    /// Todolist model for  GraphQL
    /// </summary>
    public class ToDoListType : ObjectType<ToDoListExt>
    {
        protected override void Configure(IObjectTypeDescriptor<ToDoListExt> item)
        {
            item.Field(a => a.Id).Type<IdType>();
            item.Field(a => a.Name).Type<StringType>();
            item.Field(a => a.Label).Type<StringType>();

        }
    }
}
