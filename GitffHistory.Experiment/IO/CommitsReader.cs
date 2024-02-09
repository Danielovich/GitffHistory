using GitffHistory.Experiment.Models;

namespace GitffHistory.Experiment.IO
{
    public interface IReadFiles<T> where T : class
    {
        List<T> ReadFilesFromDirectory();
    }

    public class CommitsReader : IReadFiles<Commit>
    {
        private string filesPath = string.Concat(
            AppDomain.CurrentDomain.BaseDirectory,
            Path.DirectorySeparatorChar,
            "Commits");

        public List<Commit> ReadFilesFromDirectory()
        {
            List<Commit> commits = new List<Commit>();

            if (!Directory.Exists(filesPath))
            {
                throw new DirectoryNotFoundException($"The directory '{filesPath}' was not found.");
            }

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
}
