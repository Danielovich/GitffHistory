using GitffHistory.Experiment.Models;

namespace GitffHistory.Experiment.IO
{
    public interface IReadFiles<T> where T : class
    {
        List<T> LoadCommits();
        T LoadCommit(string filePath);
    }

    public class CommitsReader : IReadFiles<Commit>
    {
        private string filesPath = string.Concat(
            AppDomain.CurrentDomain.BaseDirectory,
            Path.DirectorySeparatorChar,
            "Commits");
        
        /// <summary>
        /// Ordered by filename, descending
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public List<Commit> LoadCommits()
        {
            string[] filePaths = Directory.GetFiles(filesPath);

            List<Commit> commits = new();

            foreach (var filePath in filePaths)
            {
                commits.Add(
                    LoadCommit(filePath)
                );
            }

            // 4, 3, 2, 1....descending order
            return commits.OrderByDescending(f => f.FileName).ToList();
        }

        public Commit LoadCommit(string filePath)
        {
            var commitContent = File.ReadAllText(filePath);

            return new Commit()
            {
                RawFileContents = commitContent,
                FileName = Path.GetFileName(filePath)
            };
        }
    }
}
