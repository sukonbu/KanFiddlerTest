using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiddlerTest
{
    class Kaihatsu
    {
        private double[] recipe = new double[4];    //開発に用いた資材の量の配列(レシピ)
        private bool isSuccess;     //開発の成否
        private string flagShipName;//旗艦名
        private double flagShipLv;  //旗艦Lv
        private double fleetLv;     //司令部Lv

        private string itemName;    //手に入れた(失敗時は失敗した時の)アイテム名

        public Kaihatsu(double[] recipe, bool isSuccess, string flagShipName, double flagShipLv, double fleetLv, string itemName)
        {
            this.recipe = recipe;
            this.isSuccess = isSuccess;
            this.flagShipName = flagShipName;
            this.flagShipLv = flagShipLv;
            this.fleetLv = fleetLv;
            this.itemName = itemName;
        }

        public double[] getRecipe()
        {
            return recipe;
        }

        public bool getIsSuccess()
        {
            return isSuccess;
        }

        public string getFlagShipName()
        {
            return flagShipName;
        }

        public double getFlagShipLv()
        {
            return flagShipLv;
        }

        public double getFleetLv()
        {
            return fleetLv;
        }

        public string getItemName()
        {
            return itemName;
        }

        public override string ToString()
        {
            return string.Format("レシピ:{0}/{1}/{2}/{3} 開発:{4} 司令部Lv.{5} 旗艦:{6} Lv.{7} 装備:{8}", recipe[0], recipe[1], recipe[2], recipe[3], isSuccess, fleetLv, flagShipName, flagShipLv, itemName);
        }

    }
}
