namespace Create.Plugin.ApproveIt.Trees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web;
    using Create.Plugin.ApproveIt.Controllers;
    using Models;
    using Pocos;
    using umbraco;
    using umbraco.BusinessLogic.Actions;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.Membership;
    using Umbraco.Core.Services;
    using Umbraco.Web.Models.Trees;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.Trees;

    /// <summary>
    /// People tree controller class
    /// </summary>
    [Tree("approveIt", "approvalTree", "Content for Approval")]
    [PluginController("ApproveIt")]
    public class ApprovalTreeController : TreeController
    {
        /// <summary>
        /// Gets the tree nodes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="queryStrings">The query strings.</param>
        /// <returns>The content for approval tree collection.</returns>
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var ctrl = new ApprovalApiController();
            var nodes = new TreeNodeCollection();

            IUser user = UmbracoContext.Security.CurrentUser;

            //check if we're rendering the root node's children
            if (id == Constants.System.Root.ToInvariantString())
            {
                foreach (ApproveContent content in ctrl.GetAll(user))
                {
                    // Add the content nodes to the root of the tree
                    TreeNode node = CreateTreeNode(
                        content.Id.ToString(),
                        "-1",
                        queryStrings,
                        content.Name,
                        "icon-umb-content",
                        true);

                    nodes.Add(node);
                }
            }
            else
            {
                // We're rendering the node properties
                int level = id.Count(c => c == '-');

                if (level == 0)
                {
                    foreach (ApproveProperty prop in ctrl.GetAllForNode(user, id))
                    {
                        TreeNode propNode = CreateTreeNode(
                            prop.Alias,
                            id,
                            queryStrings,
                            prop.Name,
                            "icon-target",
                            false,
                            string.Format("approveIt/approvalTree/history/{0}", prop.Alias));

                        nodes.Add(propNode);
                    }
                }
            }

            return nodes;
        }

        /// <summary>
        /// Gets the menu for node.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="queryStrings">The query strings.</param>
        /// <returns>The menu items.</returns>
        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();
            var ctrl = new ApprovalApiController();

            if (id == Constants.System.Root.ToInvariantString())
            {
                // root actions              
                menu.Items.Add<RefreshNode, ActionRefresh>(ApplicationContext.Services.TextService.Localize(ActionRefresh.Instance.Alias));

                ////MenuItem item = new MenuItem("publishAll", ApplicationContext.Services.TextService.Localize(ActionPublish.Instance.Alias));
                ////item.Icon = "globe";
                ////item.NavigateToRoute("/backoffice/ApproveIt/ApprovalApi/PublishAll");

                ////menu.Items.Add(item);
            }

            return menu;
        }
    }
}