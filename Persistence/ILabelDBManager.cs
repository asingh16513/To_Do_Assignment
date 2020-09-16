using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence
{
    /// <summary>
    /// Interface for db operations for Label
    /// </summary>
    public interface ILabelDBManager
    {
        Task<int> AddLabel(Domain.Models.Label label);
        Task<List<Domain.Models.Label>> GetLabels();
        Task<Domain.Models.Label> GetLabelById(int labelId);

        Task<int> DeleteLabelById(int labelId);
        Task<int> AssignLabelToItem(int labelId, int[] itemId);
        Task<int> AssignLabelToList(int labelId, int[] itemId);
    }
}
