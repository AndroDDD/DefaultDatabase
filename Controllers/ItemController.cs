using Microsoft.AspNetCore.Mvc;
using DefaultDatabase.Services;
using DefaultDatabase.Models;

namespace DefaultDatabase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private IItemService _itemService;

        public ItemController(IItemService service, ILogger<ItemController> logger)
        {
            _logger = logger;
            _itemService = service;
        }

        /// <summary>
        /// get all items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllItems() {
            try {
                var items = _itemService.GetItemsList();
                if (items == null) return NotFound();
                return Ok(items);
            } catch (Exception) {
                return BadRequest();
            }
        }

        /// <summary>
        /// get item details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/id")]
        public IActionResult GetItemById(int id){
            try {
                var item = _itemService.GetItemDetailsById(id);
                if (item == null) return NotFound();
                return Ok(item);
            } catch (Exception) {
                return BadRequest();
            }
        }

        /// <summary>
        /// save employee
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult SaveItem(Item itemModel) {
            try {
                var model = _itemService.SaveItem(itemModel);
                return Ok(model);
            } catch (Exception) {
                return BadRequest();
            }
        }

        /// <summary>
        /// delete item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]")]
        public IActionResult DeleteItem(int id) {
            try {
                var model = _itemService.DeleteItem(id);
                return Ok(model);
            } catch {
                return BadRequest();
            }
        }
    }
}