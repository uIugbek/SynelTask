using System.ComponentModel.DataAnnotations;
using SynelTask.Core.Interfaces;

namespace SynelTask.Core.Data.Entities
{
    /// <summary>
    /// Base Model for Entities
    /// </summary>
    public class EntityBase : IEntityBase
    {
        public EntityBase()
        {
        }

        /// <summary>
        /// Id of entity that considered a primary key
        /// </summary>
        [Key]
        public virtual int Id { get; set; }
    }
}
