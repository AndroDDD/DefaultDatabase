using System.ComponentModel.DataAnnotations;

namespace DefaultDatabase.Models {
    public class Item {
        [Key]
        public int ItemId {
            get;
            set;
        }

        public string ItemName {
            get;
            set;
        }
        public float ItemPrice {
            get;
            set;
        }
        public string ItemDescription {
            get;
            set;
        }
        public string[] ItemImages {
            get;
            set;
        }

    }
}