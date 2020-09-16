using Domain.Models;

namespace Application.Interface
{
    /// <summary>
    /// Interface used for Mapping
    /// </summary>
    public interface IDTO
    {
        ToDoItem MapItemDTOToAddEntity(BaseToDoItem baseToDo);
        ToDoItem MapItemDTOToUpdateEntity(BaseToDoItem baseToDo);
        ToDoList MapListDTOToAddEntity(BaseToDoList baseToDo);
        ToDoList MapListDTOToUpdateEntity(BaseToDoList baseToDo);
    }
}
