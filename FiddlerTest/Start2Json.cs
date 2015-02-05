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

		public List<dynamic> SlotItemList = new List<dynamic>(); //slotitemの全データを格納するリスト
		public List<dynamic> ShipList = new List<dynamic>(); //shipの全データを格納するリスト

		public string getItemName(int itemId)
		{
			return SlotItemList[itemId - 1].api_name;
		}

		public string getShipName(int shipId)
		{
			return ShipList[shipId - 1].api_name;
		}

	}
}
