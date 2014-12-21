﻿using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelldusLiveMVC.Helpers;
using TelldusLiveMVC.Models;

namespace TelldusLiveMVC.Controllers
{
    public class TelldusLiveController : Controller
    {
        Uri baseUrl = new Uri("http://api.telldus.com/");

        public ActionResult ListDevices()
        {
            var client = new RestClient(baseUrl);

            client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                ConfigHelper.TelldusLiveConsumerKey, ConfigHelper.TelldusLiveConsumerSecret, TelldusLiveHelper.OAuthToken, TelldusLiveHelper.OAuthTokenSecret
            );

            var request = new RestRequest("json/devices/list", Method.GET);

            var response = client.Execute(request);
            
            string content = response.Content;

            TelldusLiveDevices deviceList = JsonConvert.DeserializeObject<TelldusLiveDevices>(content);

            ViewBag.deviceList = deviceList;

            return View();
        }

        public ActionResult ListSensors()
        {
            var client = new RestClient(baseUrl);

            client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                ConfigHelper.TelldusLiveConsumerKey, ConfigHelper.TelldusLiveConsumerSecret, TelldusLiveHelper.OAuthToken, TelldusLiveHelper.OAuthTokenSecret
            );

            var request = new RestRequest("json/sensors/list", Method.GET);

            var response = client.Execute(request);
            
            string content = response.Content;

            TelldusLiveSensors sensorList = JsonConvert.DeserializeObject<TelldusLiveSensors>(content);

            ViewBag.sensorList = sensorList;

            return View();
        }

        public JsonResult TurnOnAndOffDevice(string id, bool on)
        {
            var client = new RestClient(baseUrl);

            client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                ConfigHelper.TelldusLiveConsumerKey, ConfigHelper.TelldusLiveConsumerSecret, TelldusLiveHelper.OAuthToken, TelldusLiveHelper.OAuthTokenSecret
            );

            string method = "json/device/turnOn";

            if (!on)
            {
                method = "json/device/turnOff";
            }

            var request = new RestRequest(method, Method.GET);
            
            request.AddParameter("id", id);

            var response = client.Execute(request);
            
            string content = response.Content;

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Learn(string id)
        {
            var client = new RestClient(baseUrl);

            client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                ConfigHelper.TelldusLiveConsumerKey, ConfigHelper.TelldusLiveConsumerSecret, TelldusLiveHelper.OAuthToken, TelldusLiveHelper.OAuthTokenSecret
            );

            string method = "json/device/learn";
            
            var request = new RestRequest(method, Method.GET);

            request.AddParameter("id", id);

            var response = client.Execute(request);

            string content = response.Content;

            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}