# ApproveIt
Umbraco Plugin that creates a section that shows all the content that is waiting approval for publishing.

Actions to be added to the package:

```
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
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="cs" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="da" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="en_us" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="es" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="fr" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="he" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="it" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="ja" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="ko" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="nl" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="no" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="pl" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="ru" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="sv" position="end" area="sections" key="approveIt" value="Approve It" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="zh" position="end" area="sections" key="approveIt" value="Approve It" />
    
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="en" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="pt" position="end" area="general" key="approveitupdatedBy" value="Editador por" />
 <Action runat="install" undo="true" alias="AddLanguageFileKey" language="cs" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="da" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="en_us" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="es" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="fr" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="he" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="it" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="ja" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="ko" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="nl" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="no" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="pl" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="ru" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="sv" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
    <Action runat="install" undo="true" alias="AddLanguageFileKey" language="zh" position="end" area="general" key="approveitupdatedBy" value="Updated by" />
```

When the dll is deployed, and Umbraco is first run, Umbraco creates the following content:

Config\trees.config - Appends the following line:
```
<add initialize="true" sortOrder="0" alias="approvalTree" application="approveIt" title="Content for Approval" iconClosed="icon-folder" iconOpen="icon-folder-open" type="Create.Plugin.ApproveIt.Trees.ApprovalTreeController, Create.Plugin.ApproveIt" />
```

Config\applications.config - Appends the following line:
```
<add alias="approveIt" name="ApproveIt" icon="icon-people" sortOrder="15" />
```
