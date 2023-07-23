import { JiraIssueCreator } from './jira-issue-creator';

describe('JiraIssueCreator', () => {
  it('should create an instance', () => {
    expect(new JiraIssueCreator()).toBeTruthy();
  });
});
