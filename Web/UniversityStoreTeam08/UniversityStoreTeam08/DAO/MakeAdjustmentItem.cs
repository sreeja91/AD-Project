using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityStoreTeam08
{
    class MakeAdjustmentItem
    {
        private string comment;
        private string itemNumber;
        private string status;
        private int quantity;

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public string ItemNumber
        {
            get { return itemNumber; }
            set { itemNumber = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
    }
}
