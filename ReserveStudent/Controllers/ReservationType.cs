using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveStudent.Models.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveStudent.Controllers
{
    public class ReservationType : Controller
    {
        private readonly IReservationTypeRepository _repo;
        public ReservationType(IReservationTypeRepository repo)
        {
            _repo = repo;
        }

        // GET: ReservationType
        public ActionResult Index()
        {
            var reservationTypes = _repo.GetAll();
            return View(reservationTypes);
        }

        // GET: ReservationType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReservationType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReservationType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationType/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservationType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReservationType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReservationType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
