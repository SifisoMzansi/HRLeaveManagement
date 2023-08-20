using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.MVC.Controllers
{
    [Authorize(Roles = "Adminstrator,Employee")]
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeServiceRepository;
        private readonly ILeaveAllocationService _leaveAllocationService;
        public LeaveTypesController(ILeaveTypeService leaveTypeService, ILeaveAllocationService leaveAllocationService)
        {
            _leaveTypeServiceRepository = leaveTypeService;
            _leaveAllocationService = leaveAllocationService;
        }

        // GET: LeaveTypesController
        public async Task<ActionResult> Index()
        {
           var model = await _leaveTypeServiceRepository.GetLeaveTypes();
            return View(model);
        }

        // GET: LeaveTypesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var leaveType = await _leaveTypeServiceRepository.GetLeaveTypeDetails(id);

            return View(leaveType);
        }

        // GET: LeaveTypesController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateleaveTypeVM leaveType)
        {
            try
            {
                var response =  await _leaveTypeServiceRepository.CreateLeaveType(leaveType);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", response.ValidationErrors);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());

            }
            return View();
        }

        // GET: LeaveTypesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var leaveType = await _leaveTypeServiceRepository.GetLeaveTypeDetails(id);

            return View(leaveType);
         }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LeaveTypeVM leaveType)
        {
            try
            {
                var response = await _leaveTypeServiceRepository.UpdateLeaveType(id, leaveType);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", response.ValidationErrors);
                }
            }
            catch
            {
                return View();
            }
            return View();
        }

        //// GET: LeaveTypesController/Delete/5
        //public async Task<ActionResult> Delete(int id)
        //{
        //    return View();
        //}

        // POST: LeaveTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _leaveTypeServiceRepository.DeleteLeaveType(id);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", response.ValidationErrors);
                }

            }
            catch(Exception ex) 
            {
                ModelState.AddModelError("", ex.ToString());
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Allocate(int id)
        {
            try
            {
                var response = await _leaveAllocationService.CreateLeaveAllocations(id);
                if (response.Success)
                { 
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.ToString());
            }
            return BadRequest();
        }
    }
}
