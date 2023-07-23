import { JiraIssueFields } from "./jira-issue-fields";

export class JiraIssue {
    expand: string = '';
    id: number = 0;
    key: string = '';
    fields: JiraIssueFields = new JiraIssueFields();
}
