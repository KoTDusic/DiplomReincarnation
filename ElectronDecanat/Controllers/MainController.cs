using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronDecanat.Repozitory;
using Microsoft.AspNetCore.Mvc;

namespace ElectronDecanat.Controllers
{
    public class MainController : BaseController
    {
        public MainController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
        
    }
}