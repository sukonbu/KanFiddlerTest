using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiddlerTest
{
	class Start2Json
	{
		dynamic Start2 { get; set; } //Jsonをそのまま突っ込んどく用。要構造分けてフィールド化
		public List<dynamic> SlotItemList = new List<dynamic>();

		public string getItemName(int itemId)
		{
			string itemName;
			itemName = SlotItemList[itemId - 1].api_name;

			return itemName;
		}

	}
}
