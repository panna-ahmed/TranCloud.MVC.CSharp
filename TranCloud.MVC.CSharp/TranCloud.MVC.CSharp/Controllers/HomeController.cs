﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranCloud.MVC.CSharp.Models;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.IO;

namespace TranCloud.MVC.CSharp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Complete()
        {
            //print pole display
            var tranCloudAdmin = new TranCloudAdmin();
            tranCloudAdmin.TStream = new TStreamAdmin();
            tranCloudAdmin.TStream.Admin = new Admin();
            tranCloudAdmin.TStream.Admin.SecureDevice = "ONTRAN";
            tranCloudAdmin.TStream.Admin.MerchantID = "NONE";
            tranCloudAdmin.TStream.Admin.TranCode = "PoleDisplay";
            tranCloudAdmin.TStream.Admin.TranDeviceID = ConfigReader.GetDeviceID();
            tranCloudAdmin.TStream.Admin.DisplayData = new DisplayData();
            tranCloudAdmin.TStream.Admin.DisplayData.Line1 = ".WITH MERCURY";
            tranCloudAdmin.TStream.Admin.DisplayData.Line2 = ".SECURITY PAYS!!!!";

            var json = new JavaScriptSerializer().Serialize(tranCloudAdmin);

            var request = (HttpWebRequest)WebRequest.Create(ConfigReader.GetTranCloudURL());
            request.Method = "POST";
            request.ContentType = "application/json";

            var encoded = System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(
                ConfigReader.GetTranCloudUserName() + ":" + ConfigReader.GetTranCloudPassword()));

            request.Headers.Add("Authorization", "Basic " + encoded);
            request.Headers.Add("User-Trace", "testing TranCloud.MVC.CSharp");
            request.ContentLength = json.Length;
            using (var webStream = request.GetRequestStream())
            using (var requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(json);
            }

            var response = string.Empty;

            try
            {
                var webResponse = request.GetResponse();
                using (var webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (var responseReader = new StreamReader(webStream))
                        {
                            response = responseReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response = e.ToString();
            }


            var tranCloudTransaction = new TranCloudTransaction();
            tranCloudTransaction.TStream = new TStream();
            tranCloudTransaction.TStream.Transaction = new Transaction();
            tranCloudTransaction.TStream.Transaction.Account = new Account();
            tranCloudTransaction.TStream.Transaction.Account.AcctNo = "SecureDevice";
            tranCloudTransaction.TStream.Transaction.Amount = new Amount();
            tranCloudTransaction.TStream.Transaction.Amount.Purchase = "1.11";
            tranCloudTransaction.TStream.Transaction.InvoiceNo = "1234";
            tranCloudTransaction.TStream.Transaction.MerchantID = "1234";
            tranCloudTransaction.TStream.Transaction.OperatorID = "test";
            tranCloudTransaction.TStream.Transaction.PartialAuth = "Allow";
            tranCloudTransaction.TStream.Transaction.RefNo = "1234";
            tranCloudTransaction.TStream.Transaction.SecureDevice = "ONTRAN";
            tranCloudTransaction.TStream.Transaction.TranCode = "Sale";
            tranCloudTransaction.TStream.Transaction.TranDeviceID = ConfigReader.GetDeviceID();
            tranCloudTransaction.TStream.Transaction.TranType = "Credit";

            json = new JavaScriptSerializer().Serialize(tranCloudTransaction);

            request = (HttpWebRequest)WebRequest.Create(ConfigReader.GetTranCloudURL());
            request.Method = "POST";
            request.ContentType = "application/json";

            encoded = System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(
                ConfigReader.GetTranCloudUserName() + ":" + ConfigReader.GetTranCloudPassword()));

            request.Headers.Add("Authorization", "Basic " + encoded);
            request.Headers.Add("User-Trace", "testing TranCloud.MVC.CSharp");
            request.ContentLength = json.Length;
            using (var webStream = request.GetRequestStream())
            using (var requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(json);
            }

            response = string.Empty;

            try
            {
                var webResponse = request.GetResponse();
                using (var webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (var responseReader = new StreamReader(webStream))
                        {
                            response = responseReader.ReadToEnd();                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response = e.ToString();
            }

            var tranCloudResponse = new JavaScriptSerializer().Deserialize<dynamic>(response);
            var rstream = tranCloudResponse["RStream"];
            var cmdResponse = rstream["CmdResponse"];
            var tranResponse = rstream["TranResponse"];

            var paymentResponse = new PaymentInfoResponse();
            paymentResponse.AcqRefData = "";
            paymentResponse.Amount = "1.11";
            paymentResponse.CardholderName = "test test";
            paymentResponse.CardType = tranResponse["CardType"];
            paymentResponse.DisplayMessage = cmdResponse["TextResponse"];
            paymentResponse.ExpDate = tranResponse["ExpDate"];
            paymentResponse.MaskedAccount = tranResponse["AcctNo"];
            paymentResponse.Token = tranResponse["RecordNo"];


            //print pole display
            tranCloudAdmin = new TranCloudAdmin();
            tranCloudAdmin.TStream = new TStreamAdmin();
            tranCloudAdmin.TStream.Admin = new Admin();
            tranCloudAdmin.TStream.Admin.SecureDevice = "ONTRAN";
            tranCloudAdmin.TStream.Admin.MerchantID = "NONE";
            tranCloudAdmin.TStream.Admin.TranCode = "DrawerOpen";
            tranCloudAdmin.TStream.Admin.TranDeviceID = ConfigReader.GetDeviceID();

            json = new JavaScriptSerializer().Serialize(tranCloudAdmin);

            request = (HttpWebRequest)WebRequest.Create(ConfigReader.GetTranCloudURL());
            request.Method = "POST";
            request.ContentType = "application/json";

            encoded = System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(
                ConfigReader.GetTranCloudUserName() + ":" + ConfigReader.GetTranCloudPassword()));

            request.Headers.Add("Authorization", "Basic " + encoded);
            request.Headers.Add("User-Trace", "testing TranCloud.MVC.CSharp");
            request.ContentLength = json.Length;
            using (var webStream = request.GetRequestStream())
            using (var requestWriter = new StreamWriter(webStream, System.Text.Encoding.ASCII))
            {
                requestWriter.Write(json);
            }

            response = string.Empty;

            try
            {
                var webResponse = request.GetResponse();
                using (var webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (var responseReader = new StreamReader(webStream))
                        {
                            response = responseReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response = e.ToString();
            }


            return View(paymentResponse);
        }
    }
}