using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiddlerTest
{
    class Kaihatsu
    {
        public double[] Recipe { get; private set; }    //開発に用いた資材の量の配列(レシピ)
		public bool IsSuccess { get; private set; }     //開発の成否
        public string FlagShipName { get; private set; }//旗艦名
		public double FlagShipLv { get; private set; }  //旗艦Lv
		public double FleetLv { get; private set; }     //司令部Lv

		public string ItemName { get; private set; }    //手に入れた(失敗時は失敗した時の)アイテム名

        public Kaihatsu(double[] recipe, bool isSuccess, string flagShipName, double flagShipLv, double fleetLv, string itemName)
        {
            this.Recipe = recipe;
            this.IsSuccess = isSuccess;
            this.FlagShipName = flagShipName;
            this.FlagShipLv = flagShipLv;
            this.FleetLv = fleetLv;
            this.ItemName = itemName;
        }

        public override string ToString()
        {
            return string.Format("レシピ:{0}/{1}/{2}/{3} 開発:{4} 司令部Lv.{5} 旗艦:{6} Lv.{7} 装備:{8}", Recipe[0], Recipe[1], Recipe[2], Recipe[3], IsSuccess, FleetLv, FlagShipName, FlagShipLv, ItemName);
        }

    }
}
