namespace Create.Plugin.ApproveIt.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Pocos;

    /// <summary>
    /// Approve it property change history.
    /// </summary>
    public class PropertyHistory
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        /// <value>
        /// The alias.
        /// </value>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name of the author.
        /// </summary>
        /// <value>
        /// The name of the author.
        /// </value>
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the author email.
        /// </summary>
        /// <value>
        /// The author email.
        /// </value>
        public string AuthorEmail { get; set; }

        /// <summary>
        /// Gets or sets the change date.
        /// </summary>
        /// <value>
        /// The change date.
        /// </value>
        public string ChangeDate { get; set; }
    }
}