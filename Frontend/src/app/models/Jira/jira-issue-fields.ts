import { JiraIssueCreator } from "./jira-issue-creator";
import { JiraIssueStatus } from "./jira-issue-status";
import { JiraIssueTypeFields } from "./jira-issue-type-fields";

export class JiraIssueFields {
    issuetype: JiraIssueTypeFields = new JiraIssueTypeFields();
    summary: string = '';
    status: JiraIssueStatus = new JiraIssueStatus();
    created: string = '';
    updated: string = '';
    creator: JiraIssueCreator = new JiraIssueCreator();
}
