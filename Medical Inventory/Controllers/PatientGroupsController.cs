using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Medical_Inventory.Data;
using Medical_Inventory.Models;
using Medical_Inventory.Data.IRepository;
using Medical_Inventory.Data.IRepository.Repository;
using Medical_Inventory.Exceptions;
using System.Security.Claims;

namespace Medical_Inventory.Controllers
{
    public class PatientGroupsController : Controller
    {
        private readonly ILogger<PatientGroupsController> _logger;
        private readonly IPatientGroupRepository _patientGroupRepository;

        public PatientGroupsController(ILogger<PatientGroupsController> logger, IPatientGroupRepository patientGroupRepository)
        {
            _logger = logger;
            _patientGroupRepository = patientGroupRepository;
        }

        // GET: PatientGroups
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _patientGroupRepository.GetAll()!;
                return View(categories);
            }
            catch (Exception)
            {
                _logger.LogError("failed to load patient groups");
            }

            return View(null);
        }

        // GET: PatientGroups/Details/5
        public async Task<IActionResult> Details(long id)
        {
            try
            {
                var category = await _patientGroupRepository.GetFirstOrDefault(id)!;
                return View(category);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return RedirectToAction("PageNotFound", "Home");
            }
            catch (Exception)
            {
                _logger.LogWarning($"patient group not found with id: {id}");
            }
            return RedirectToAction("PageNotFound", "Home");

        }

        // GET: PatientGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientGroups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PatientGroup patientGroup)
        {
            try
            {
                var existsCategory = await _patientGroupRepository.GetByName(patientGroup.Name)!;

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                patientGroup.CreatedTime = DateTime.Now;
                patientGroup.UpdatedTime = DateTime.Now;
                patientGroup.CreatedById = long.Parse(userId);
                patientGroup.UpdatedById = long.Parse(userId);

                await _patientGroupRepository.Add(patientGroup);
                await _patientGroupRepository.Save();

                _logger.LogInformation(message: $"new category added with name of {patientGroup.Name}");
            }
            catch (DuplicationException ex)
            {
                ModelState.AddModelError(string.Empty, patientGroup.Name + " already exists");
                _logger.LogWarning(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }

            if (!ModelState.IsValid) return View(patientGroup);

            return RedirectToAction(nameof(Index));

        }

        // GET: PatientGroups/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                var category = await _patientGroupRepository.GetFirstOrDefault(id)!;

                return View(category);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning($"patient group not found of id: {id}\n" + ex);
            }
            catch (Exception)
            {

            }
            return RedirectToAction("PageNotFound", "Home");
        }

        // POST: PatientGroups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] PatientGroup patientGroup)
        {
            try
            {
                var existsPatientGroup = await _patientGroupRepository.GetByName(patientGroup.Name, id)!;

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                patientGroup.UpdatedById = long.Parse(userId);

                _patientGroupRepository.Update(patientGroup);
                await _patientGroupRepository.Save();

                _logger.LogWarning($"patient group updated of id: {id}");

                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException)
            {
                _logger.LogWarning($"patient group not found of id: {id}");
                return NotFound();
            }
            catch (DuplicationException ex)
            {
                _logger.LogWarning($"patient group alaready exists of name: {patientGroup.Name}\n" + ex);
                ModelState.AddModelError(string.Empty, patientGroup.Name + " already exists");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Failed to update data of id: {id}");
                _logger.LogWarning(ex.Message);
            }

            return View(patientGroup);
        }

        // GET: PatientGroups/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var patientGroup = await _patientGroupRepository.GetFirstOrDefault(id)!;

            if (patientGroup == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            return View(patientGroup);
        }

        // POST: PatientGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                _patientGroupRepository.Remove(id);
                await _patientGroupRepository.Save();

                _logger.LogWarning($"Deleted data of id: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Failed to delete data of id: {id}\n" + ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
