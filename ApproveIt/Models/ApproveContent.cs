namespace Create.Plugin.ApproveIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Pocos;

    /// <summary>
    /// Approve it content to approve.
    /// </summary>
    public class ApproveContent
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
        /// Gets or sets the changed properties.
        /// </summary>
        /// <value>
        /// The changed properties.
        /// </value>
        public IList<ApproveProperty> ChangedProperties { get; set; }
    }
}