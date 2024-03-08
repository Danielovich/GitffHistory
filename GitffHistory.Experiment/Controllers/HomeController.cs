using Microsoft.AspNetCore.Mvc;
using GitffHistory.Experiment.Domain;
using GitffHistory.Experiment.ViewModels;

namespace GitffHistory.Experiment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDiffFiles commitDiffer;

        public HomeController(ILogger<HomeController> logger, IDiffFiles compareFiles)
        {
            _logger = logger;
            this.commitDiffer = compareFiles;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexVewModel(commitDiffer.Diff());
            viewModel.Prettify();

            return View(viewModel);
        }
    }
}
