namespace Create.Plugin.ApproveIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Pocos;

    /// <summary>
    /// Approve it property to approve.
    /// </summary>
    public class ApproveProperty
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>
        /// The alias.
        /// </value>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the property type alias.
        /// </summary>
        /// <value>
        /// The property type alias.
        /// </value>
        public string PropertyTypeAlias { get; set; }

        /// <summary>
        /// Gets or sets the name of the writer.
        /// </summary>
        /// <value>
        /// The name of the writer.
        /// </value>
        public string WriterName { get; set; }

        /// <summary>
        /// Gets or sets the writer email.
        /// </summary>
        /// <value>
        /// The writer email.
        /// </value>
        public string WriterEmail { get; set; }

        /// <summary>
        /// Gets or sets the update date.
        /// </summary>
        /// <value>
        /// The update date.
        /// </value>
        public string UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the Previous Value
        /// </summary>
        /// <value>
        /// The previous value.
        /// </value>
        public string PreviousValue { get; set; }

        /// <summary>
        /// Gets or sets the Current Value
        /// </summary>
        /// <value>
        /// The current value.
        /// </value>
        public string CurrentValue { get; set; }
    }
}