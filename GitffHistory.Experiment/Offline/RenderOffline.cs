using GitffHistory.Experiment.Domain;
using GitffHistory.Experiment.IO;
using GitffHistory.Experiment.ViewModels;
using Razor.Templating.Core;

namespace GitffHistory.Experiment.Offline
{
    /// <summary>
    /// This can be invoked from a console or a class application.
    /// For only generation of html based on Razor views in the MVC convention (views folder).
    /// </summary>
    public class RenderOffline
    {
        public async Task<string> Render()
        {
            var commitsReader = new CommitsReader();
            var commitDiffer = new CommitDiffer();

            var diffs = commitDiffer.BuildParentCurrentHierachi(commitsReader.LoadCommits());
            commitDiffer.Diff(diffs);

            var model = new IndexVewModel(diffs);
            model.Prettify();

            var html = await RazorTemplateEngine.RenderAsync("/Views/Home/Index.cshtml", model, null);

            return html;
        }
    }
}
