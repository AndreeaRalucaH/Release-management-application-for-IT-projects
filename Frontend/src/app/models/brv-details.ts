import { JiraContentsExcel } from "./Jira/jira-contents-excel";

export class BrvDetails {
    idImpulse: string = '';
    creationDate: string = '';
    author: string = '';
    application: string = '';
    poApp: string = '';
    itApp: string = '';
    poDate: string = '';
    itDate: string = '';
    jiraContents: Array<JiraContentsExcel> = [];
}
