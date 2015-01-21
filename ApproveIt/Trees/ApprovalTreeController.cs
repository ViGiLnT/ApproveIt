namespace Create.Plugin.ApproveIt.Trees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web;
    using Create.Plugin.ApproveIt.Controllers;
    using umbraco;
    using umbraco.BusinessLogic.Actions;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.Membership;
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
        protected override TreeNodeCollection GetTreeNodes(string id, FormDataCollection queryStrings)
        {
            var ctrl = new ApprovalApiController();
            var nodes = new TreeNodeCollection();

            IUser user = UmbracoContext.Security.CurrentUser;

            //check if we're rendering the root node's children
            if (id == Constants.System.Root.ToInvariantString())
            {
                foreach (IContent content in ctrl.GetAll(user))
                {
                    var node = CreateTreeNode(
                        content.Id.ToString(),
                        "-1",
                        queryStrings,
                        content.Name,
                        "icon-document",
                        false);

                    nodes.Add(node);
                }
            }

            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, FormDataCollection queryStrings)
        {
            var menu = new MenuItemCollection();

            if (id == Constants.System.Root.ToInvariantString())
            {
                // root actions              
                menu.Items.Add<RefreshNode, ActionRefresh>(ui.Text("actions", ActionRefresh.Instance.Alias), true);
            }

            return menu;
        }
    }
}