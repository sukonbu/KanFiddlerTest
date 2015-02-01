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
using System.IO;


namespace FiddlerTest
{
	public partial class Form1 : Form
	{
		private Start2Json start2 = new Start2Json();
		private FleetMaterial fleetMaterial = new FleetMaterial();


		private List<KaihatsuResult> kaihatsuResultList = new List<KaihatsuResult>();

		public Form1()
		{

			InitializeComponent();
			//Control.CheckForIllegalCrossThreadCalls = false; //スレッドセーフを無視する最終手段

			Fiddler.FiddlerApplication.AfterSessionComplete
				+= new Fiddler.SessionStateHandler(FiddlerApplication_AfterSessionComplete);

			Fiddler.FiddlerApplication.Startup(8080, Fiddler.FiddlerCoreStartupFlags.ChainToUpstreamGateway); //プロキシの設定(この場合は、ローカルのプロキシで第1引数のポート番号で通信)

			// FiddlerApplication.Startup(0, Fiddler.FiddlerCoreStartupFlags.RegisterAsSystemProxy);  //システムのプロキシの設定全部乗っ取る

			URLMonInterop.SetProxyInProcess(string.Format("127.0.0.1:{0}", Fiddler.FiddlerApplication.oProxy.ListenPort), "<local>");
			//oSession["x-overrideGateway"] = string.Format("localhost:{0:D}", proxy.UpstreamPort); // 上流プロキシの設定?
		}

		private void button1_Click(object sender, EventArgs e)
		{

			if (textBox1.Text.Equals("")) textBox1.Text = "http://www.dmm.com/netgame_s/kancolle/";
			webBrowser1.Navigate(textBox1.Text);
		}

		void FiddlerApplication_AfterSessionComplete(Fiddler.Session oSession)
		{



			if (true) //oSession.fullUrl.Contains("125.6.189.247"))  //宿毛湾泊地サーバのIP
			{
				var responseResult = oSession.GetResponseBodyAsString();
				Debug.WriteLine(responseResult);
				if (oSession.fullUrl.Contains("/kcsapi/"))
				{

					Console.WriteLine(string.Format("Session{0}({3}):HTTP {1} for {2}", oSession.id, oSession.responseCode, oSession.fullUrl, oSession.oResponse.MIMEType));
					Debug.WriteLine(string.Format("Session{0}({3}):HTTP {1} for {2}", oSession.id, oSession.responseCode, oSession.fullUrl, oSession.oResponse.MIMEType));

					try
					{
						var jsonData = DynamicJson.Parse(responseResult.Replace("svdata=", string.Empty));

						//object[] slotitems = jsonData.api_data.api_mst_slotitem;
						//using (var log = new StreamWriter(new FileStream("log.txt", FileMode.Append)))
						//{
						//	foreach (var slotitem in slotitems)
						//	{
						//		log.WriteLine(slotitem);
						//	}
						//}


						


						if (oSession.fullUrl.Contains("api_start2"))
						{
							start2 = jsonData;
							//using (var log = new StreamWriter(new FileStream("start2_log.txt", FileMode.Append)))
							//{
							//	log.WriteLine(start2);

							//}
							object[] slotitems = jsonData.api_data.api_mst_slotitem;
							foreach (var slotitem in slotitems)
							{
								Console.WriteLine(slotitem.ToString());
								start2.SlotItemList.Add(slotitem);
							}
						}

						if (oSession.fullUrl.Contains("api_req_kousyou/createitem"))//開発時のapiリクエスト
						{
							fleetMaterial.NowMaterial.Fuel = jsonData.api_data.api_material[0];
							fleetMaterial.NowMaterial.Ammunition = jsonData.api_data.api_material[1];
							fleetMaterial.NowMaterial.Steel = jsonData.api_data.api_material[2];
							fleetMaterial.NowMaterial.Bauxite = jsonData.api_data.api_material[3];


							//Console.WriteLine(fleetMaterial.BeforeMaterial);
							//Console.WriteLine(fleetMaterial.NowMaterial);

							Material recipeMaterial = new Material();
							recipeMaterial.Fuel = fleetMaterial.BeforeMaterial.Fuel - fleetMaterial.NowMaterial.Fuel;
							recipeMaterial.Ammunition = fleetMaterial.BeforeMaterial.Ammunition - fleetMaterial.NowMaterial.Ammunition;
							recipeMaterial.Steel = fleetMaterial.BeforeMaterial.Steel - fleetMaterial.NowMaterial.Steel;
							recipeMaterial.Bauxite = fleetMaterial.BeforeMaterial.Bauxite - fleetMaterial.NowMaterial.Bauxite;


							KaihatsuResult kaihatsuResult = new KaihatsuResult();
							kaihatsuResult.Recipe = recipeMaterial;
							kaihatsuResult.FlagShipName = "";
							kaihatsuResult.FlagShipLv = 0;
							kaihatsuResult.FleetLv = 0;

							if (jsonData.api_data.api_create_flag == 1)
							{
								//kaihatsuResult = "成功";
								//equipName = jsonData.api_data.api_slot_item.api_slotitem_id;
								kaihatsuResult.IsSuccess = true;
								int itemId = int.Parse(jsonData.api_data.api_slot_item.api_slotitem_id.ToString());
								kaihatsuResult.ItemName = start2.getItemName(itemId);
							}
							else
							{
								//kaihatsuResult = "失敗";
								//equipName = jsonData.api_data.api_fdata;
								kaihatsuResult.IsSuccess = false;
								kaihatsuResult.ItemName = jsonData.api_data.api_fdata;

								int itemId = int.Parse(jsonData.api_data.api_fdata.Split(',')[1]);
								//Console.WriteLine("itemId={0}",itemId);
								kaihatsuResult.ItemName = start2.getItemName(itemId);
							}


							kaihatsuResultList.Add(kaihatsuResult); //開発結果の追加

							using (var log = new StreamWriter(new FileStream("kaihatsuResult_log.txt", FileMode.Append)))
							{
								log.WriteLine(kaihatsuResult); //ログを残す
							}


							//Console.WriteLine("レシピ：" +recipeFuel +"/" +recipeAmmunition +"/"+recipeSteel +"/" +recipeBauxite +"\t開発結果:" +kaihatsuResult +"\t装備id (?):" +equipName);
							Console.WriteLine(kaihatsuResultList[(kaihatsuResultList.Count - 1)]);
						}
						else if (oSession.fullUrl.Contains("api_port/port"))//母港開いた時のリクエスト
						{
							fleetMaterial.NowMaterial.Fuel = jsonData.api_data.api_material[0].api_value;
							fleetMaterial.NowMaterial.Ammunition = jsonData.api_data.api_material[1].api_value;
							fleetMaterial.NowMaterial.Steel = jsonData.api_data.api_material[2].api_value;
							fleetMaterial.NowMaterial.Bauxite = jsonData.api_data.api_material[3].api_value;

							Console.WriteLine(fleetMaterial.NowMaterial);
						}
						else
						{
							//material = "うんこ";
						}

						fleetMaterial.BeforeMaterial = (Material)fleetMaterial.NowMaterial.Clone();

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

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{

		}
	}
}
