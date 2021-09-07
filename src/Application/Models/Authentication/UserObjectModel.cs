using System.Text.Json;
using Application.Models.Base;
using Domain.Models.Authentication;

namespace Application.Models.Authentication
{
    /// <summary>
    /// User DTO model
    /// </summary>
    public class UserObjectModel : ObjectModelBase<UserModel>
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Email address of user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Creates a new instance of UserObjectModel
        /// </summary>
        public UserObjectModel()
        {
        }

        /// <summary>
        /// Creates a new instance of UserObjectModel
        /// </summary>
        /// <param name="entity">Domain entity to read and assign the values</param>
        public UserObjectModel(UserModel entity)
        {
            AssignFrom(entity);
        }

        /// <summary>
        /// Assign domain entity properties to respective fields.
        /// </summary>
        /// <param name="entity">Domain entity to read and assign the values.</param>
        public sealed override void AssignFrom(UserModel entity)
        {
            UserId = entity.UserId;
            Email = entity.Email;
        }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}