namespace Create.Plugin.ApproveIt.Controllers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Models;
    using Pocos;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Core.Persistence;
    using Umbraco.Web.Editors;
    using Umbraco.Web.Mvc;

    /// <summary>
    /// Approval Api Controller.
    /// </summary>
    [PluginController("ApproveIt")]
    public class ApprovalApiController : UmbracoAuthorizedJsonController
    {
        #region Private Props

        /// <summary>
        /// Gets or sets the content of the unpublished.
        /// </summary>
        /// <value>
        /// The content of the unpublished.
        /// </value>
        private IList<IContent> UnpublishedContent { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets all content waiting for approval.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// The content nodes waiting for approval and their changeHistory.
        /// </returns>
        public IList<ApproveContent> GetAll(IUser user)
        {
            // Get the Umbraco db
            var db = ApplicationContext.DatabaseContext.Database;

            // Get the nodes waiting approval
            string query = string.Format("SELECT [nodeId] FROM {0} GROUP BY [nodeId]", Settings.APPROVE_IT_CHANGE_HISTORY_TABLE);
            IEnumerable<int> nodesWaitingApproval = db.Query<int>(query);

            // Get the node properties waiting approval
            IList<ChangeHistory> dirtyProps = db.Fetch<ChangeHistory>(
                string.Format("SELECT * FROM {0}", Settings.APPROVE_IT_CHANGE_HISTORY_TABLE));

            // Build the content and dirty properties structure
            IList<ApproveContent> contentForApproval = new List<ApproveContent>();

            // Start the nodes to remove list
            IList<int> nodesToRemove = new List<int>();

            foreach (int nodeId in nodesWaitingApproval)
            {
                IContent content = ApplicationContext.Services.ContentService.GetById(nodeId);

                if (content != null)
                {
                    string[] contentDirtyPropsAliases = dirtyProps
                        .Where(x => x.NodeId == nodeId)
                        .Select(x => x.PropertyAlias)
                        .Distinct()
                        .ToArray();

                    IList<ApproveProperty> contentDirtyProps = new List<ApproveProperty>();

                    foreach(string alias in contentDirtyPropsAliases)
                    {
                        Property prop = content.Properties.Where(x => string.Compare(x.Alias, alias, true) == 0).FirstOrDefault();
                        contentDirtyProps.Add(new ApproveProperty()
                        {
                            Alias = alias,
                            Id = prop.Id,
                            Name = prop.PropertyType.Name
                        });
                    }

                    ApproveContent appContent = new ApproveContent()
                    {
                        Id = content.Id,
                        Name = content.Name,
                        ChangedProperties = contentDirtyProps
                    };

                    contentForApproval.Add(appContent);
                }
                else
                {
                    // It has been removed from the content tree, add it to a list to remove it from the db
                    if (!nodesToRemove.Contains(nodeId))
                    {
                        nodesToRemove.Add(nodeId);
                    }
                }
            }

            // Removes the nodes that are no longer present in the BO
            RemoveNodesFromDB(db, nodesToRemove);

            return contentForApproval;
        }

        /// <summary>
        /// Gets all content waiting for approval.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// The content nodes waiting for approval and their changeHistory.
        /// </returns>
        public IList<ApproveProperty> GetAllForNode(IUser user, string id)
        {
            // Get the Umbraco db
            var db = ApplicationContext.DatabaseContext.Database;

            int nodeId;
            if (!int.TryParse(id, out nodeId))
            {
                return null;
            }

            // Get the node properties waiting approval
            IList<ChangeHistory> dirtyProps = db.Fetch<ChangeHistory>(
                string.Format("SELECT * FROM {0} WHERE [nodeId]=@0", Settings.APPROVE_IT_CHANGE_HISTORY_TABLE),
                id);

            // Start the nodes to remove list
            IList<int> nodesToRemove = new List<int>();
            IList<ApproveProperty> contentDirtyProps = new List<ApproveProperty>();

            if (dirtyProps != null & dirtyProps.Count >= 0)
            {
                IContent content = ApplicationContext.Services.ContentService.GetById(nodeId);

                if (content != null)
                {
                    string[] contentDirtyPropsAliases = dirtyProps
                        .Where(x => x.NodeId == nodeId)
                        .Select(x => x.PropertyAlias)
                        .Distinct()
                        .ToArray();

                    foreach (string alias in contentDirtyPropsAliases)
                    {
                        Property prop = content.Properties.Where(x => string.Compare(x.Alias, alias, true) == 0).FirstOrDefault();
                        contentDirtyProps.Add(new ApproveProperty()
                        {
                            Alias = alias,
                            Id = prop.Id,
                            Name = prop.PropertyType.Name
                        });
                    }
                }
                else
                {
                    nodesToRemove.Add(nodeId);
                }
            }
            else
            {
                // It has been removed from the content tree, add it to a list to remove it from the db
                nodesToRemove.Add(nodeId);
            }

            // Removes the nodes that are no longer present in the BO
            RemoveNodesFromDB(db, nodesToRemove);

            return contentDirtyProps;
        }

        /// <summary>
        /// Gets the node by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The content node.</returns>
        public ContentHistory GetNodeById(int id, string userLocale)
        {
            // Get the Umbraco db
            var db = ApplicationContext.DatabaseContext.Database;

            // Gets the content being visualized from the approveit DB
            IList<ChangeHistory> changeHistoryArray = db.Fetch<ChangeHistory>(
                string.Format("SELECT * FROM {0} WHERE [nodeId]=@0", Settings.APPROVE_IT_CHANGE_HISTORY_TABLE),
                id);

            // Get content being visualized
            IContent content = ApplicationContext.Services.ContentService.GetById(id);

            CultureInfo userCulture = new CultureInfo(userLocale);

            // Start the nodes to remove list
            IList<int> nodesToRemove = new List<int>();

            if (content != null)
            {
                ContentHistory currentContent = new ContentHistory()
                {
                    Name = content.Name,
                    Id = content.Id,
                    Properties = new List<PropertyHistory>()
                };

                foreach (ChangeHistory change in changeHistoryArray)
                {
                    // Get the user that updated the content
                    IUser writer = ApplicationContext.Services.UserService.GetByUsername(change.UpdatedBy);

                    currentContent.Properties.Add(new PropertyHistory()
                    {
                        Alias = change.PropertyAlias,
                        AuthorEmail = writer.Email,
                        AuthorName = writer.Username,
                        ChangeDate = change.UpdateDate.ToString("F", userCulture),
                        Id = change.Id
                    });
                }

                return currentContent;
            }
            else
            {
                // It has been removed from the content tree, add it to a list to remove it from the db
                if (!nodesToRemove.Contains(id))
                {
                    nodesToRemove.Add(id);
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The content node.</returns>
        public ApproveContent GetById(int id, string userLocale)
        {
            // Get the Umbraco db
            var db = ApplicationContext.DatabaseContext.Database;

            // Gets the content being visualized from the approveit DB
            IList<ChangeHistory> changeHistoryArray = db.Fetch<ChangeHistory>(
                string.Format("SELECT * FROM {0} WHERE [nodeId]=@0", Settings.APPROVE_IT_CHANGE_HISTORY_TABLE),
                id);

            // Get content being visualized
            IContent content = ApplicationContext.Services.ContentService.GetById(id);

            // Get the user that updated the content
            IUser writer = ApplicationContext.Services.UserService.GetUserById(content.WriterId);

            CultureInfo userCulture = new CultureInfo(userLocale);

            ApproveContent updatedContent = new ApproveContent()
            {
                Id = content.Id,
                Name = content.Name,
                WriterName = writer.Username,
                WriterEmail = writer.Email,
                UpdateDate = content.UpdateDate.ToString("F", userCulture)
            };

            return updatedContent;
        }

        /// <summary>
        /// Publishes the post.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The content node published.</returns>
        public IContent PostPublish(int id)
        {
            IContent node = ApplicationContext.Services.ContentService.GetById(id);
            ApplicationContext.Services.ContentService.Publish(node);

            // Get the Umbraco db
            var db = ApplicationContext.DatabaseContext.Database;

            // Delete the occurences of this node on the db
            string delQuery = string.Format("DELETE FROM {0} WHERE nodeId=@0", Settings.APPROVE_IT_CHANGE_HISTORY_TABLE);
            db.Execute(delQuery, id);

            return node;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Removes the nodes from database.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="nodesToRemove">The nodes to remove.</param>
        private void RemoveNodesFromDB(UmbracoDatabase db, IList<int> nodesToRemove)
        {
            // It has been removed from the content tree, remove every occurence from the db
            if (nodesToRemove.Count > 0)
            {
                foreach (int nodeToRemove in nodesToRemove)
                {
                    string delQuery = string.Format("DELETE FROM {0} WHERE nodeId=@0", Settings.APPROVE_IT_CHANGE_HISTORY_TABLE);
                    db.Execute(delQuery, nodeToRemove);
                }
            }
        }

        /// <summary>
        /// Recursive method to traverse all content nodes and find the unpublished ones.
        /// </summary>
        /// <param name="node">The node (usually the root node).</param>
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

        #endregion
    }
}
