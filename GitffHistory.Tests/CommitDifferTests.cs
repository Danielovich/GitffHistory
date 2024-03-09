namespace GitffHistory.Tests;

public class CommitDifferTests
{
    [Theory]
    [DiffsData]
    public void Previous_Commit_Is_Not_The_Same_As_Current_Commit(
        List<Diff> autoDiffs, 
        CommitDiffer sut)
    {
        var diffs = sut.Diff(autoDiffs);

        diffs.ForEach(e => {
            Assert.NotEqual(e.Current.FileName, e.Parent.FileName);
        });
    }

    [Theory]
    [DiffsData]
    public void History_Commits_Are_In_Descending_Order(
        List<Commit> autoCommits, 
        CommitDiffer sut)
    {
        var diffs = sut.BuildParentCurrentHierachi(autoCommits);

        // find the number in the filename; 999_jsjsjjs.txt, should return 999.
        var fileNumbers = diffs.Select(s => 
            Convert.ToInt32(s.Parent.FileName.Substring(0, s.Parent.FileName.IndexOf('_'))))
            .ToList();

        for (int i = 0; i < fileNumbers.Count() - 1; i++)
        {
            if (fileNumbers[i] < fileNumbers[i + 1])
            {
                Assert.Fail("numbers are not descending");
            }
        }
    }
}