using GameRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameRater.Controllers
{
    public class OnLineGameController : Controller
    {
        private readonly IOnLineGameViewModel _onLineGameViewModel;

        public OnLineGameController(IOnLineGameViewModel onLineGameViewModel)
        {
            _onLineGameViewModel = onLineGameViewModel;
        }

        // GET: OnLineGame
        public ActionResult Index()
        {
            _onLineGameViewModel.HandleRequest();
            return View(_onLineGameViewModel);
        }

        [HttpPost]
       // [Authorize(Roles = "Admin,SuperUser")]
        public ActionResult Index(OnLineGameViewModel onLineGameViewModel)
        {
            onLineGameViewModel.IsValid = ModelState.IsValid;
            onLineGameViewModel.HandleRequest();

            if (onLineGameViewModel.IsValid)
            {
                ModelState.Clear();
            }
            else
            {
                foreach (var item in onLineGameViewModel.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }
            return View(onLineGameViewModel);
        }







    }
}
