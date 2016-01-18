namespace Create.Plugin.ApproveIt.Pocos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Umbraco.Core.Persistence;
    using Umbraco.Core.Persistence.DatabaseAnnotations;    
    
    /// <summary>
    /// PetaPoco class that represents the ChangeHistory DB Table
    /// </summary>
    [TableName(Settings.APPROVE_IT_CHANGE_HISTORY_TABLE)]
    [PrimaryKey("Id", autoIncrement = true)]
    [ExplicitColumns]
    public class ChangeHistory
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Column("id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the node identifier.
        /// </summary>
        /// <value>
        /// The node identifier.
        /// </value>
        [Column("nodeId")]
        public int NodeId { get; set; }

        /// <summary>
        /// Gets or sets the property identifier.
        /// </summary>
        /// <value>
        /// The property identifier.
        /// </value>
        [Column("propertyId")]
        public int PropertyId { get; set; }

        /// <summary>
        /// Gets or sets the update date.
        /// </summary>
        /// <value>
        /// The update date.
        /// </value>
        [Column("updated")]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the updated by col.
        /// </summary>
        /// <value>
        /// The updated by col.
        /// </value>
        [Column("updatedBy")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the previous value.
        /// </summary>
        /// <value>
        /// The previous value.
        /// </value>
        [Column("previousValue")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string PreviousValue { get; set; }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        /// <value>
        /// The current value.
        /// </value>
        [Column("currentValue")]
        [SpecialDbType(SpecialDbTypes.NTEXT)]
        public string CurrentValue { get; set; }
    }
}