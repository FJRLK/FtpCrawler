using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FtpCrawler.Web.Controllers
{
    public class ServerController : Controller
    {
        private Services.Interfaces.IFtpServerService ServerService;

        public ServerController(Services.Interfaces.IFtpServerService _service)
        {
            ServerService = _service;
        }

        // GET: Server
        public ActionResult Index()
        {
            var mapper = Factories.MappingConfiguration.CreateConfiguration().CreateMapper();

            List<Models.FtpServerModel> servers = ServerService.GetAll().ToList().Select(s => mapper.Map<Data.Models.FtpServer, Models.FtpServerModel>(s)).ToList();

            return View(servers);
        }

        public ActionResult Add()
        {
            return View(new Models.FtpServerModel());
        }

        [HttpPost]
        public ActionResult Add(Models.FtpServerModel model)
        {
            if (ModelState.IsValid)
            {
                var server = ServerService.GetByHostName(model.HostName);

                if (server == null)
                {
                    var mapper = Factories.MappingConfiguration.CreateConfiguration().CreateMapper();
                    ServerService.Create(mapper.Map<Models.FtpServerModel, Data.Models.FtpServer>(model));

                    return RedirectToAction("Index", "Server");
                }
                else
                    ViewBag.ErrorMessage = $"{model.HostName} already exists";
            }

            return View(model);
        }
    }
}