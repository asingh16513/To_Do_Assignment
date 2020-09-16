using Database;
using Ninject.Modules;
using Persistence;

namespace Application.Helper
{
    /// <summary>
    /// CLass to hold dependencies for Db operations
    /// </summary>
    public class DependencyResolver : NinjectModule
    {
        /// <summary>
        /// Function to load dependency.
        /// </summary>
        public override void Load()
        {
            Bind<IUserDbManager>().To<UserDbManager>().InSingletonScope();
            Bind<ILabelDBManager>().To<LabelDbManager>().InSingletonScope();
            Bind<IToDoItemDbManager>().To<ToDoItemDbManager>().InSingletonScope();
            Bind<IToDoListDbManager>().To<ToDoListDbManager>().InSingletonScope();
        }
    }
}
