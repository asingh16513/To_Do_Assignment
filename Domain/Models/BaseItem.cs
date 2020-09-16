namespace Domain.Models
{
    /// <summary>
    /// Class to hold item base properties
    /// </summary>
    public class BaseItem : BaseModel
    {
        public int? LabelId { get; set; }
        public int? UserId { get; set; }
    }
}
