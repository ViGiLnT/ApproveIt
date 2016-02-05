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
    }
}