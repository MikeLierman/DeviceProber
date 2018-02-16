/*
 
  This Source Code is subject to the terms of the APACHE
  LICENSE 2.0. You can obtain a copy of the terms at
  https://www.apache.org/licenses/LICENSE-2.0
  Copyright (C) 2018 Invise Labs

  Learn more about Invise Labs and our projects by visiting: http://invi.se/labs

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DeviceProber
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Keep Main clean by calling out to separate function. */
            OnLoad();
        }

        /* Global Variables */
        private static int disabled = 0; /* Devices reported as state 22, ie MISSING. */
        private static int missing = 0; /* Devices reported as state 28 or 1, ie MISSING. */
        private static int zero = 0; /* Devices reported as state 0, 25, or 26, ie OK. */
        private static int error = 0; /*Devices reported as any other states, ie ERROR. */
        private static bool fatalError = false;
        private static bool checkFailed = false;

        private static void OnLoad()
        {
            /* Let's get this done. */
            CheckDevices();

            /* What did we find, boys? */
            string status = zero + " devices OK;";
            if (disabled > 0) { status = disabled + " device(s) in DISABLED state; " + status; checkFailed = false; }
            if (error > 20) { } /* Correction for some types of servers */
            else if (error > 0) { status = error + " device(s) in ERROR state; " + status; checkFailed = true; }
            if (missing > 0) { status = missing + " device(s) with MISSING drivers; " + status; checkFailed = true; }
            Console.WriteLine(status);

            /* Return the exit code. */
            if (checkFailed) { Console.WriteLine("CHECK FAILED;"); Environment.Exit(-3); }
            else { Console.WriteLine("CHECK PASSED;"); Environment.Exit(0); }
        }

        private static void CheckDevices()
        {
            /* Define what we are searching for--EVERYTHING BITCH. */
            ManagementObjectSearcher devMos = new ManagementObjectSearcher(@"SELECT * FROM Win32_PnPEntity");

            if (devMos != null)
            {
                /* Loop through each device in Device Manager. This is done very quickly and w/ minimal CPU resources. */
                foreach (ManagementObject device in devMos.Get())
                {
                    try
                    {
                        /* Record name of device and whether it has OK status or not.*/
                        string ok = "fail";
                        string name = device.GetPropertyValue("Name").ToString();
                        string status = device.GetPropertyValue("Status").ToString().ToLower();
                        string code = device.GetPropertyValue("ConfigManagerErrorCode").ToString().ToLower();

                        if (code == "0" || code == "25" || code == "26") /* Is Windows reporting the device as OK? */
                        { if (status.Contains("degraded") || status.Contains("fail")) { error++; } else { zero++; ok = "ok"; } } /* Double-check the status before giving it the green light. */
                        else if (code == "22") { disabled++; }
                        else if (code == "1" || code == "28") { missing++; }
                        else { error++; }
                    }
                    catch { }
                }
            }
        }
    }
}
