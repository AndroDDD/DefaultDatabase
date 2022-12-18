using DefaultDatabase.Models;
using DefaultDatabase.ViewModels;
using DefaultDatabase.DbContexts;

namespace DefaultDatabase.Services
{
    public interface IItemService
    {
        /// <summary>
        /// get list of all items
        /// </summary>
        /// <returns></returns>
        List<Item> GetItemsList();

        /// <summary>
        /// get item details by item id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Item GetItemDetailsById(int itemId);

        /// <summary>
        /// add edit item
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns><returns>
        ResponseModel SaveItem(Item itemModel);

        /// <summary>
        /// delete item
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        ResponseModel DeleteItem(int itemId);

    }

    public class ItemService : IItemService {
        private DefaultContext _context;
        public ItemService(DefaultContext context){
            _context = context;
        }

        /// <summary>
        /// get list of all items
        /// </summary>
        /// <returns></returns>
        public List<Item> GetItemsList() {
            List<Item> itemsList;
            try {
                itemsList = _context.Set<Item>().ToList();
            } catch (Exception){
                throw;
            }
            return itemsList;
        }

        /// <summary>
        /// get item details by item id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Item GetItemDetailsById(int itemId) {
            Item item;
            try {
                item = _context.Find<Item>(itemId);
            } catch {
                throw;
            }
            return item;
        }

        /// <summary>
        /// add edit item
        /// </summary>
        /// <param name="itemModel"></param>
        /// <returns></returns>
        public ResponseModel SaveItem(Item itemModel) {
            ResponseModel model = new ResponseModel();
            try {
                Item _temp = GetItemDetailsById(itemModel.ItemId);
                if(_temp != null){
                    _temp.ItemName = itemModel.ItemName;
                    _temp.ItemPrice = itemModel.ItemPrice;
                    _temp.ItemDescription = itemModel.ItemDescription;
                    _temp.ItemImages = itemModel.ItemImages;
                    _context.Update<Item>(_temp);
                    model.Message = "Item Update Successfully";
                } else {
                    _context.Add<Item>(itemModel);
                    model.Message = "Item Inserted Successfully";
                }
                _context.SaveChanges();
                model.IsSuccess = true;
            } catch (Exception ex) {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        /// <summary>
        /// delete items
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ResponseModel DeleteItem(int itemId) {
            ResponseModel model = new ResponseModel();
            try {
                Item _temp = GetItemDetailsById(itemId);
                if (_temp != null){
                    _context.Remove<Item>(_temp);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Item Deleted Successfully";
                } else {
                    model.IsSuccess = false;
                    model.Message = "Item Not Found";
                }
            } catch (Exception ex) {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }
    }
}