using Microsoft.AspNetCore.Mvc;
using Mcdoliibee.Models;
using Mcdoliibee.Services;
using System.Collections.Generic;

namespace Mcdoliibee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController()
        {
            _menuService = new MenuService(); // Ideally, use dependency injection
        }

        // GET: api/Menu
        [HttpGet]
        public ActionResult<IEnumerable<menu>> GetAllMenus()
        {
            var menus = _menuService.GetAllMenus();
            if (menus == null || menus.Count == 0)
            {
                return NotFound("No menus found.");
            }
            return Ok(menus);
        }

        // GET: api/Menu/{code}
        [HttpGet("{code}")]
        public ActionResult<menu> GetMenu(string code)
        {
            var menus = _menuService.GetAllMenus();
            var menu = menus?.Find(m => m.Code == code);

            if (menu == null)
            {
                return NotFound("Menu item not found.");
            }
            return Ok(menu);
        }

        // POST: api/Menu
        [HttpPost]
        public ActionResult AddMenu([FromBody] menu newMenu)
        {
            if (newMenu == null)
            {
                return BadRequest("Invalid menu data.");
            }

            bool success = _menuService.AddMenu(newMenu);
            if (success)
            {
                return CreatedAtAction(nameof(GetMenu), new { code = newMenu.Code }, newMenu);
            }

            return StatusCode(500, "An error occurred while adding the menu.");
        }

        // PUT: api/Menu/{code}
        [HttpPut("{code}")]
        public ActionResult UpdateMenu(string code, [FromBody] menu updatedMenu)
        {
            if (updatedMenu == null || code != updatedMenu.Code)
            {
                return BadRequest("Invalid menu data or mismatched code.");
            }

            bool success = _menuService.UpdateMenu(updatedMenu);
            if (success)
            {
                return NoContent();
            }

            return StatusCode(500, "An error occurred while updating the menu.");
        }

        // DELETE: api/Menu/{code}
        [HttpDelete("{code}")]
        public ActionResult DeleteMenu(string code)
        {
            bool success = _menuService.DeleteMenu(code);
            if (success)
            {
                return NoContent();
            }

            return StatusCode(500, "An error occurred while deleting the menu.");
        }
    }
}
