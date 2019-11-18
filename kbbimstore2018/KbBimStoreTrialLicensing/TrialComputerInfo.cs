using System;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace KbBimstore.KbBimStoreTrialLicensing
{
    class TrialComputerInfo
    {
        public static SHA256Managed enctyptor = new SHA256Managed();

        public static string geManagementObjectProperty(string className, string propertyName)
        {
            ManagementClass mClass = new System.Management.ManagementClass(className);

            ManagementObjectCollection mObjectCollection = mClass.GetInstances();
            foreach (ManagementObject mObject in mObjectCollection)
            {
                PropertyDataCollection mPropertiesCollection = mObject.Properties;
                foreach (PropertyData mPropertyData in mPropertiesCollection)
                {
                    if (mPropertyData.Name.Equals(propertyName))
                    {
                        object value = mPropertyData.Value;
                        if (value != null)
                        {
                            return mPropertyData.Value.ToString();
                        }
                    }
                }
            }

            return "";
        }

        public static string getHash(string value)
        {
            string hash = "";

            if (value != null && value.Length > 0)
            {
                byte[] valueBytes = Encoding.UTF8.GetBytes(value);
                if (valueBytes != null && valueBytes.Length > 0)
                {
                    byte[] hashBytes = enctyptor.ComputeHash(valueBytes);
                    if (hashBytes != null && hashBytes.Length > 0)
                    {
                        String hashTemp = Encoding.UTF8.GetString(hashBytes);
                        if (hashTemp != null)
                        {
                            if (hashTemp.Length > 128)
                            {
                                hash = hashTemp.Substring(0, 128);
                            }
                            else
                            {
                                hash = hashTemp;
                            }
                        }
                    }
                }
            }

            return hash;
        }

        public static string findMotherboardId()
        {
            string result = geManagementObjectProperty("Win32_BaseBoard", "SerialNumber");

            return result;
        }

        public static string findHardDriveId()
        {

            string result = geManagementObjectProperty("Win32_DiskDrive", "Model");
            result += geManagementObjectProperty("Win32_DiskDrive", "Manufacturer");

            return result;
        }

        public static string findProcessorId()
        {
            string result = geManagementObjectProperty("Win32_Processor", "ProcessorId");
            result += geManagementObjectProperty("Win32_Processor", "UniqueId");

            return result;
        }

        public static string findBiosId()
        {
            string result = geManagementObjectProperty("Win32_BIOS", "SerialNumber");

            return result;
        }

        public static string findMACaddress()
        {
            string result = geManagementObjectProperty("Win32_NetworkAdapterConfiguration", "MACAddress");

            return result;
        }

        public static string ReplaceSpecialCharacters(string str)
        {
            string cleanStr = Regex.Replace(str, "[^0-9a-zA-Z]+", "");
            cleanStr = "KBR" + cleanStr;
            return cleanStr;
        }

        public static TrialAuthenticationObject getAuthenticationObject()
        {
            TrialAuthenticationObject authObj = new TrialAuthenticationObject();

            authObj.motherboardid = findMotherboardId();//getHash(findMotherboardId());
            authObj.harddriveid = findHardDriveId();//getHash(findHardDriveId());
            authObj.processorid = findProcessorId();//getHash(findProcessorId());
            authObj.macaddress = findMACaddress();//getHash(findMACaddress());
            authObj.biosid = findBiosId();//getHash(findBiosId());

            authObj.motherboardid = "unknown";
            authObj.harddriveid = "unknown";
            authObj.processorid = "unknown";
            authObj.macaddress = ReplaceSpecialCharacters(authObj.macaddress);
            authObj.biosid = "unknown";

            authObj.registrationstate = "2018";

            return authObj;
        }
    }
}