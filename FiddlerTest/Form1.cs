using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Codeplex.Data;
using System.Diagnostics;

using Fiddler;
using Newtonsoft.Json;


namespace FiddlerTest
{
    public partial class Form1 : Form
    {
        private static double beforeFuel;
        private static double beforeAmmunition;
        private static double beforeSteel;
        private static double beforeBauxite;
        private FleetMaterial material;
        private List<Kaihatsu> kaihatsuResultList = new List<Kaihatsu>();

        public Form1()
        {
            beforeFuel = 0;
            beforeAmmunition = 0;
            beforeSteel = 0;
            beforeBauxite = 0;
            material =  new FleetMaterial(0,0,0,0);


            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = false; //スレッドセーフを無視する最終手段

            Fiddler.FiddlerApplication.AfterSessionComplete
                += new Fiddler.SessionStateHandler(FiddlerApplication_AfterSessionComplete);

            Fiddler.FiddlerApplication.Startup(0,Fiddler.FiddlerCoreStartupFlags.ChainToUpstreamGateway);

           // FiddlerApplication.Startup(0, Fiddler.FiddlerCoreStartupFlags.RegisterAsSystemProxy);

            URLMonInterop.SetProxyInProcess(string.Format("127.0.0.1:{0}", Fiddler.FiddlerApplication.oProxy.ListenPort), "<local>");
            //oSession["x-overrideGateway"] = string.Format("localhost:{0:D}", proxy.UpstreamPort); // 上流プロキシの設定
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Equals("")) textBox1.Text = "http://www.dmm.com/netgame_s/kancolle/";
            webBrowser1.Navigate(textBox1.Text);
        }

        void FiddlerApplication_AfterSessionComplete(Fiddler.Session oSession)
        {



            if (true)//oSession.fullUrl.Contains("125.6.189.247"))  //宿毛湾泊地サーバのIP
            {
                var result = oSession.GetResponseBodyAsString();
                Debug.WriteLine(result);
                if (oSession.fullUrl.Contains("/kcsapi/"))
                {

                    Console.WriteLine(string.Format("Session{0}({3}):HTTP {1} for {2}", oSession.id, oSession.responseCode, oSession.fullUrl, oSession.oResponse.MIMEType));
                    Debug.WriteLine(string.Format("Session{0}({3}):HTTP {1} for {2}", oSession.id, oSession.responseCode, oSession.fullUrl, oSession.oResponse.MIMEType));

                    //string text = (string.Format("Session{0}({3}):HTTP {1} for {2}", oSession.id, oSession.responseCode, oSession.fullUrl, oSession.oResponse.MIMEType));
                    //richTextBox1.AppendText(text+"\n");

                    try
                    {
                        var jsonData = DynamicJson.Parse(result.Replace("svdata=", string.Empty));

                       // string material = null;
                        //double fuel = -1, ammunition = -1, steel = -1, bauxite = -1;

                        if (oSession.fullUrl.Contains("api_req_kousyou/createitem"))//開発時のapiリクエスト
                        {
                            //material = string.Format("資材残量   燃料:{0},弾薬:{1},鋼材:{2},ボーキサイト{3}", jsonData.api_data.api_material[0], jsonData.api_data.api_material[1], jsonData.api_data.api_material[2], jsonData.api_data.api_material[3]);
                            
                            //fuel = jsonData.api_data.api_material[0];
                            //ammunition = jsonData.api_data.api_material[1];
                            //steel = jsonData.api_data.api_material[2];
                            //bauxite = jsonData.api_data.api_material[3];
                            material.setFleetMaterial(jsonData.api_data.api_material[0], jsonData.api_data.api_material[1], jsonData.api_data.api_material[2], jsonData.api_data.api_material[3]);


                            Console.WriteLine(material);
                            //Console.WriteLine(fuel + "," + ammunition + "," + steel + "," + bauxite);
                            var recipeFuel = beforeFuel - material.getFuel();
                            var recipeAmmunition = beforeAmmunition - material.getAmmunition();
                            var recipeSteel = beforeSteel - material.getSteel();
                            var recipeBauxite = beforeBauxite - material.getBauxite();

                            //var kaihatsuResult = "";
                            //var equipName = -0.0;

                            if(jsonData.api_data.api_create_flag == 1)
                            {
                                //kaihatsuResult = "成功";
                                //equipName = jsonData.api_data.api_slot_item.api_slotitem_id;

                                kaihatsuResultList.Add(new Kaihatsu(new double[] { recipeFuel, recipeAmmunition, recipeSteel, recipeBauxite }, true, "", 0, 0, jsonData.api_data.api_slot_item.api_slotitem_id.ToString()));

                            }
                            else
                            {
                                //kaihatsuResult = "失敗";
                                //equipName = jsonData.api_data.api_fdata;
                                kaihatsuResultList.Add(new Kaihatsu(new double[] { recipeFuel, recipeAmmunition, recipeSteel, recipeBauxite }, false, "", 0, 0, jsonData.api_data.api_fdata.ToString()));
                            }
        
                            


                            //Console.WriteLine("レシピ：" +recipeFuel +"/" +recipeAmmunition +"/"+recipeSteel +"/" +recipeBauxite +"\t開発結果:" +kaihatsuResult +"\t装備id (?):" +equipName);
                            Console.WriteLine(kaihatsuResultList[(kaihatsuResultList.Count - 1)]);
                        }
                        else if (oSession.fullUrl.Contains("api_port/port"))//母港開いた時のリクエスト
                        {
                            //material = string.Format("資材残量   燃料:{0}/弾薬:{1}/鋼材:{2}/ボーキサイト:{3}", jsonData.api_data.api_material[0].api_value, jsonData.api_data.api_material[1].api_value, jsonData.api_data.api_material[2].api_value, jsonData.api_data.api_material[3].api_value);
                            //fuel = jsonData.api_data.api_material[0].api_value;
                            //ammunition = jsonData.api_data.api_material[1].api_value;
                            //steel = jsonData.api_data.api_material[2].api_value;
                            //bauxite = jsonData.api_data.api_material[3].api_value;

                            material.setFleetMaterial(jsonData.api_data.api_material[0].api_value, jsonData.api_data.api_material[1].api_value, jsonData.api_data.api_material[2].api_value, jsonData.api_data.api_material[3].api_value);

                            Console.WriteLine(material);
                            //Console.WriteLine(fuel +"," +ammunition +"," +steel +"," +bauxite);
                        }
                        else
                        {
                            //material = "うんこ";
                        }


                        //richTextBox1.AppendText(material+"\n");

                        //dataGridView1.Rows.Clear();
                        //dataGridView1.Rows.Add(fuel,ammunition,steel,bauxite);
                        beforeFuel = material.getFuel();
                        beforeAmmunition = material.getAmmunition();
                        beforeSteel = material.getSteel();
                        beforeBauxite = material.getBauxite();

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                }
            }
        }


        private void Form1_Formclosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                URLMonInterop.ResetProxyInProcessToDefault();
                FiddlerApplication.Shutdown();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                    {
                        m.Result = (IntPtr)0x2;
                        return;
                    }
                    break;

                case 0x112:
                    if ((m.WParam.ToInt32() & 0xF030) == 0xF030)
                    {
                        return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
