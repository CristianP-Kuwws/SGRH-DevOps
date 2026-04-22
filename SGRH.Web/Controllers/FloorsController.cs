using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGRH.Web.Models;
using SGRH.Web.Services;

namespace SGRH.Web.Controllers
{
    public class FloorsController : Controller
    {
        private static readonly List<SelectListItem> StatusOptions =
        [
            new("Activo", "active"),
            new("Inactivo", "inactive"),
            new("Mantenimiento", "maintenance")
        ];

        private readonly IFloorApiService _floorApiService;

        public FloorsController(IFloorApiService floorApiService)
        {
            _floorApiService = floorApiService;
        }

        public async Task<IActionResult> Index()
        {
            var floors = await _floorApiService.GetAllAsync();
            return View(floors.OrderBy(x => x.FloorNumber).ToList());
        }

        public IActionResult Create()
        {
            LoadStatusOptions();
            return View(new FloorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FloorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoadStatusOptions();
                return View(model);
            }

            var result = await _floorApiService.CreateAsync(model);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "No se pudo crear el floor.");
                LoadStatusOptions();
                return View(model);
            }

            TempData["SuccessMessage"] = "Floor creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var floor = await _floorApiService.GetByIdAsync(id);
            if (floor is null)
            {
                return NotFound();
            }

            LoadStatusOptions();
            return View(floor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FloorViewModel model)
        {
            if (id != model.FloorId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                LoadStatusOptions();
                return View(model);
            }

            var result = await _floorApiService.UpdateAsync(id, model);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "No se pudo actualizar el floor.");
                LoadStatusOptions();
                return View(model);
            }

            TempData["SuccessMessage"] = "Floor actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var floor = await _floorApiService.GetByIdAsync(id);
            if (floor is null)
            {
                return NotFound();
            }

            return View(floor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _floorApiService.DeleteAsync(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.ErrorMessage ?? "No se pudo eliminar el floor.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Floor eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        private void LoadStatusOptions()
        {
            ViewBag.StatusOptions = StatusOptions;
        }
    }
}
