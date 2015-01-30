using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiddlerTest
{
    class KaihatsuResult
    {
        public Material Recipe { get; set; }    //開発に用いた資材の量の配列(レシピ)
		public bool IsSuccess { get; set; }     //開発の成否
        public string FlagShipName { get; set; }//旗艦名
		public double FlagShipLv { get; set; }  //旗艦Lv
		public double FleetLv { get; set; }     //司令部Lv

		public string ItemName { get; set; }    //手に入れた(失敗時は失敗した時の)アイテム名

        public override string ToString()
        {
            return string.Format("レシピ:{0}/{1}/{2}/{3} 開発:{4} 司令部Lv.{5} 旗艦:{6} Lv.{7} 装備:{8}", Recipe.Fuel, Recipe.Ammunition, Recipe.Steel, Recipe.Bauxite, IsSuccess, FleetLv, FlagShipName, FlagShipLv, ItemName);
        }

    }
}
