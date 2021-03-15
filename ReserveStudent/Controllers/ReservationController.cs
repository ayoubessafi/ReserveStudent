using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReserveStudent.Models;
using ReserveStudent.Models.contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveStudent.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepo;
        private readonly IReservationTypeRepository _reservationTypeRepo;
        private readonly UserManager<IdentityUser> _userManager;
        public ReservationController(IReservationRepository reservationRepo, IReservationTypeRepository reservationTypeRepo, UserManager<IdentityUser> userManager)
        {
            _reservationRepo = reservationRepo;
            _reservationTypeRepo = reservationTypeRepo;
            _userManager = userManager;
        }

        // GET: ReservationController
        public ActionResult Index()
        {
            var reservation = _reservationRepo.GetAll().OrderBy(x=>x.RequestingStudent.Count);
            return View(reservation);
        }

        // GET: ReservationController/Details/5
        public ActionResult Review(int id)
        {
            var reservation = _reservationRepo.GetById(id);
            return View(reservation);
        }

        public ActionResult ApprouveRequest(int id)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var reservation = _reservationRepo.GetById(id);
                reservation.Status = true;
                reservation.RequestingStudent.Count ++ ;
                _reservationRepo.Update(reservation);
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }

        public ActionResult RejectRequest(int id)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var reservation = _reservationRepo.GetById(id);
                reservation.Status = false;
                _reservationRepo.Update(reservation);
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }
        }

        // GET: ReservationController/Create
            public ActionResult Create()
        {

            var reservationTypes = _reservationTypeRepo.GetAll();
            var absenceTypesItems = reservationTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            var model = new CreateReservation
            {
               ReservationTypes = absenceTypesItems
            };
            return View(model);
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateReservation model)
        {
            try
            {
                var Date = Convert.ToDateTime(model.Date);
                
                var reservationTypes = _reservationTypeRepo.GetAll().ToList();
                var reservationTypesItems = reservationTypes.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
                model.ReservationTypes = reservationTypesItems;
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
               
                var student = _userManager.GetUserAsync(User).Result;
               

                var reservation = new Reservation
                {
                    RequestingStudentId = student.Id,
                    Date = Date,
                    Status=null,
                    ReservationTypeId=model.ReservationTypeId

                };
                /*var reservation = new Reservation
                {
                    Date=reservationVM.Date,
                    RequestingStudentId=reservationVM.RequestingStudentId,
                    ReservationTypeId=reservationVM.ReservationTypeId,
                    Status=reservationVM.Status,
                };*/
                var isSuccuss = _reservationRepo.Create(reservation);
                if (!isSuccuss)
                {
                    ModelState.AddModelError("", "Something went wrong in the submit action");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: ReservationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReservationController/Edit/5
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

        // GET: ReservationController/Delete/5
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: ReservationController/Delete/5
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
