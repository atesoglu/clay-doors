using System.Text.Json;
using Application.Models.Base;
using Domain.Models.CheckPoints;

namespace Application.Models.CheckPoints
{
    /// <summary>
    /// CheckPoint DTO model
    /// </summary>
    public class CheckPointObjectModel : ObjectModelBase<CheckPointModel>
    {
        /// <summary>
        /// Primary key of entity.
        /// </summary>
        public int CheckPointId { get; set; }
        /// <summary>
        /// Address of the checkpoint
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Creates a new instance of CheckPointObjectModel 
        /// </summary>
        public CheckPointObjectModel()
        {
        }

        /// <summary>
        /// Creates a new instance of CheckPointObjectModel
        /// </summary>
        /// <param name="entity">Domain entity to read and assign the values</param>
        public CheckPointObjectModel(CheckPointModel entity)
        {
            AssignFrom(entity);
        }

        /// <summary>
        /// Assign domain entity properties to respective fields.
        /// </summary>
        /// <param name="entity">Domain entity to read and assign the values.</param>
        public sealed override void AssignFrom(CheckPointModel entity)
        {
            CheckPointId = entity.CheckPointId;
            Address = entity.Address;
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}