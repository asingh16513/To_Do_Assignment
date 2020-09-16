using Domain.Models;
using HotChocolate.Types;

namespace Domain.GraphQlModels
{
    /// <summary>
    /// TodoItem model for GraphQL
    /// </summary>
    class ToDoItemType : ObjectType<ToDoItemExt>
    {
        protected override void Configure(IObjectTypeDescriptor<ToDoItemExt> item)
        {
            item.Field(a => a.Id).Type<IdType>();
            item.Field(a => a.Name).Type<StringType>();
            item.Field(a => a.Label).Type<StringType>();

        }
    }
}
