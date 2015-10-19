namespace Create.Plugin.ApproveIt.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;

    /// <summary>
    /// Person Api Controller.
    /// </summary>
    [PluginController("ApproveIt")]
    public class ApprovalApiController : UmbracoAuthorizedJsonController
    {
        /// <summary>
        /// Gets or sets the content of the unpublished.
        /// </summary>
        /// <value>
        /// The content of the unpublished.
        /// </value>
        private IList<IContent> UnpublishedContent { get; set; }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The content nodes waiting for approval.</returns>
        public IEnumerable<IContent> GetAll(IUser user)
        {
            UnpublishedContent = new List<IContent>();

            IList<IContent> root = ApplicationContext.Services.ContentService.GetRootContent().ToList();

            foreach (IContent content in root)
            {
                GetNode(content);
            }

            return UnpublishedContent;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The content node.</returns>
        public IContent GetById(int id)
        {
            IContent content = ApplicationContext.Services.ContentService.GetById(id);
            return content;
        }

        /// <summary>
        /// Posts the publish.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The content node published.</returns>
        public IContent PostPublish(int id)
        {
            IContent node = ApplicationContext.Services.ContentService.GetById(id);
            ApplicationContext.Services.ContentService.Publish(node);

            return node;
        }

        /// <summary>
        /// Recursive method to traverse all content nodes and find the unpublished ones.
        /// </summary>
        /// <param name="node">The node.</param>
        private void GetNode(IContent node)
        {
            if (!node.Published)
            {
                UnpublishedContent.Add(node);
            }

            foreach (IContent child in ApplicationContext.Services.ContentService.GetChildren(node.Id))
            {
                GetNode(child);
            }
        }
    }
}
