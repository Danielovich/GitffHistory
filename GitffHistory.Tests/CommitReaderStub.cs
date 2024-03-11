namespace GitffHistory.Tests;

public class CommitReaderStub : IReadFiles<Commit>
{
    private string filesPath = string.Concat(
        AppDomain.CurrentDomain.BaseDirectory,
        "StubData",
        Path.DirectorySeparatorChar,
        "Commits");

    public Commit LoadCommit(string filePath)
    {
        throw new NotImplementedException();
    }

    public List<Commit> LoadCommits()
    {
        List<Commit> commits = new List<Commit>();

        string[] filePaths = Directory.GetFiles(filesPath);

        foreach (var filePath in filePaths)
        {
            string lines = File.ReadAllText(filePath);

            var commit = new Commit();
            commit.RawFileContents = lines;
            commit.FileName = Path.GetFileName(filePath);
            commits.Add(commit);
        }

        var orderedByFilenames = commits.OrderByDescending(f => f.FileName).ToList();

        return orderedByFilenames;
    }
}