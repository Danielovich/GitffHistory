using Microsoft.AspNetCore.Mvc;
using GitffHistory.Experiment.Domain;
using GitffHistory.Experiment.ViewModels;
using GitffHistory.Experiment.IO;
using GitffHistory.Experiment.Models;

namespace GitffHistory.Experiment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDiffFiles commitDiffer;
        private readonly IReadFiles<Commit> readCommits;

        public HomeController(
            ILogger<HomeController> logger, 
            IDiffFiles compareFiles,
            IReadFiles<Commit> readFiles)
        {
            _logger = logger;
            this.commitDiffer = compareFiles;
            this.readCommits = readFiles;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexVewModel(
                commitDiffer.Diff(
                    commitDiffer.BuildParentCurrentHierachi(
                        readCommits.LoadCommits())));

            viewModel.Prettify();

            return View(viewModel);
        }
    }
}
