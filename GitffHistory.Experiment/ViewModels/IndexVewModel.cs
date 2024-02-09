using GitffHistory.Experiment.IO;
using GitffHistory.Experiment.Models;

namespace GitffHistory.Experiment.ViewModels
{
    public class IndexVewModel
    {
        private readonly List<Diff> diffs = new List<Diff>();
        public List<ChangeDetails> Changes { get; set; } = new List<ChangeDetails>();


        public IndexVewModel(List<Diff> diffs)
        {
            this.diffs = diffs;
        }

        public void Prettify()
        {

            foreach (var commit in diffs)
            {
                var fileChanges = new ChangeDetails();

                var result = new List<LineDetail>() { new LineDetail() };

                var linesToExcercise = commit.History.RawFileContents.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                for (int i = 0; i < linesToExcercise.Length; i++)
                {
                    var detail = new LineDetail();
                    detail.Content = linesToExcercise[i];

                    foreach (var deleted in commit.Diffs)
                    {
                        if (deleted.deletedA > 0)
                        {
                            if (deleted.StartA == i)
                            {
                                detail.Deletion = true;
                                detail.HasChange = true;
                            }
                        }
                    }

                    result.Add(detail);
                }

                fileChanges.HistoryFilename = commit.History.FileName;
                fileChanges.History.AddRange(result);


                result = new List<LineDetail>() { new LineDetail() };

                linesToExcercise = commit.Current.RawFileContents.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                for (int i = 0; i < linesToExcercise.Length; i++)
                {
                    var detail = new LineDetail();
                    detail.Content = linesToExcercise[i];

                    foreach (var inserted in commit.Diffs)
                    {
                        if (inserted.insertedB > 0)
                        {
                            if (inserted.StartB == i)
                            {
                                detail.Insertion = true;
                                detail.HasChange = true;
                            }
                        }
                    }

                    result.Add(detail);
                }


                fileChanges.CurrentFilename = commit.Current.FileName;
                fileChanges.Current.AddRange(result);

                Changes.Add(fileChanges);
            }

        }

        public string LeadingWhitespaceToNbsp(string input)
        {
            string result = string.Empty;
            bool firstNonWhitespaceCharFound = false;

            foreach (char c in input)
            {
                if (!firstNonWhitespaceCharFound && char.IsWhiteSpace(c))
                {
                    result += "&nbsp;";
                }
                else
                {
                    firstNonWhitespaceCharFound = true;
                }
            }

            return result;
        }

        public class ChangeDetails
        {
            public string HistoryFilename { get; set; } = default!;
            public string CurrentFilename { get; set; } = default!;
            public List<LineDetail> History { get; set; } = new List<LineDetail> { };
            public List<LineDetail> Current { get; set; } = new List<LineDetail> { };
        }

        public class LineDetail
        {
            public string Content { get; set; } = default!;
            public bool HasChange { get; set; }
            public bool Deletion { get; set; }
            public bool Insertion { get; set; }
            public int LineNumber { get; set; }

        }
    }
}
