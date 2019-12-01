using System;
using ElectronDecanat.Repozitory;
using Microsoft.AspNetCore.Mvc;

namespace ElectronDecanat.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IUnitOfWork UnitOfWork;

        protected BaseController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }
    }
}