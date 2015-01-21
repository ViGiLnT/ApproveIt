
██████╗ ███████╗ █████╗ ██████╗ ███╗   ███╗███████╗    ███╗   ███╗███████╗
██╔══██╗██╔════╝██╔══██╗██╔══██╗████╗ ████║██╔════╝    ████╗ ████║██╔════╝
██████╔╝█████╗  ███████║██║  ██║██╔████╔██║█████╗      ██╔████╔██║█████╗  
██╔══██╗██╔══╝  ██╔══██║██║  ██║██║╚██╔╝██║██╔══╝      ██║╚██╔╝██║██╔══╝  
██║  ██║███████╗██║  ██║██████╔╝██║ ╚═╝ ██║███████╗    ██║ ╚═╝ ██║███████╗
╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═════╝ ╚═╝     ╚═╝╚══════╝    ╚═╝     ╚═╝╚══════╝
                                                                          
Actions to be added to the package:

<Action runat="install" undo="true" alias="addDashboardSection" dashboardAlias="StartupApproveItDashboardSection">
    <section>
    <areas>
        <area>approveIt</area>
    </areas>
    <tab caption="Get Started">
        <control showOnce="true" addPanel="true" panelCaption="">
    views/dashboard/approveIt/approveItdashboardintro.html
    </control>
    </tab>
    </section>
</Action>
<Action runat="install" undo="true" alias="AddLanguageFileKey" language="en" position="end" area="sections" key="approveIt" value="Approve It" />
<Action runat="install" undo="true" alias="AddLanguageFileKey" language="pt" position="end" area="sections" key="approveIt" value="Approve It" />

When the dll is deployed, and Umbraco is first run, Umbraco creates the following content:

Config\trees.config - Appends the following line:
    <add initialize="true" sortOrder="0" alias="approvalTree" application="approveIt" title="Content for Approval" iconClosed="icon-folder" iconOpen="icon-folder-open" type="Create.Plugin.ApproveIt.Trees.ApprovalTreeController, Create.Plugin.ApproveIt" />

Config\applications.config - Appends the following line:
    <add alias="approveIt" name="ApproveIt" icon="icon-people" sortOrder="15" />