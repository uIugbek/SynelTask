using System;

namespace SynelTask.Core.Interfaces
{
    /// <summary>
    /// An Interface of Base Entity for Entities 
    /// </summary>
    public interface IEntityBase
    {
        /// <summary>
        /// Id of entity
        /// </summary>
        int Id { get; set; }
    }
}
