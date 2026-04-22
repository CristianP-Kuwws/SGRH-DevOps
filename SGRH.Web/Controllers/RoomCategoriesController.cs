using Microsoft.AspNetCore.Mvc;
using SGRH.Web.Models;
using SGRH.Web.Services;

namespace SGRH.Web.Controllers
{
    public class RoomCategoriesController : Controller
    {
        private readonly IRoomCategoryApiService _roomCategoryApiService;

        public RoomCategoriesController(IRoomCategoryApiService roomCategoryApiService)
        {
            _roomCategoryApiService = roomCategoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            var roomCategories = await _roomCategoryApiService.GetAllAsync();
            return View(roomCategories.OrderBy(x => x.Name).ToList());
        }

        public IActionResult Create()
        {
            return View(new RoomCategoryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _roomCategoryApiService.CreateAsync(model);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "No se pudo crear la room category.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Room category creada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var roomCategory = await _roomCategoryApiService.GetByIdAsync(id);
            if (roomCategory is null)
            {
                return NotFound();
            }

            return View(roomCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomCategoryViewModel model)
        {
            if (id != model.CategoryId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _roomCategoryApiService.UpdateAsync(id, model);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "No se pudo actualizar la room category.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Room category actualizada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var roomCategory = await _roomCategoryApiService.GetByIdAsync(id);
            if (roomCategory is null)
            {
                return NotFound();
            }

            return View(roomCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _roomCategoryApiService.DeleteAsync(id);
            if (!result.Success)
            {
                TempData["ErrorMessage"] = result.ErrorMessage ?? "No se pudo eliminar la room category.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Room category eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
