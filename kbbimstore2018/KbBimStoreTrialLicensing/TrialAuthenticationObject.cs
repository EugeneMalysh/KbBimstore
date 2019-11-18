using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace KbBimstore.KbBimStoreTrialLicensing
{
    class TrialAuthenticationObject
    {
        private string _motherboardid;
        private string _harddriveid;
        private string _processorid;
        private string _macaddress;
        private string _biosid;
        private string _registrationstate;

        public string motherboardid
        {
            get { return _motherboardid; }
            set { _motherboardid = value; }
        }

        public string harddriveid
        {
            get { return _harddriveid; }
            set { _harddriveid = value; }
        }

        public string processorid
        {
            get { return _processorid; }
            set { _processorid = value; }
        }

        public string macaddress
        {
            get { return _macaddress; }
            set { _macaddress = value; }
        }

        public string biosid
        {
            get { return _biosid; }
            set { _biosid = value; }
        }

        public string registrationstate
        {
            get { return _registrationstate; }
            set { _registrationstate = value; }
        }

        public string toJsonString()
        {
            try
            {
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                String jsonString = jsonSerializer.Serialize(this);
                return jsonString;
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public static TrialAuthenticationObject fromJsonString(string jsonString)
        {
            if (jsonString != null)
            {
                try
                {
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    TrialAuthenticationObject authObj = jsonSerializer.Deserialize<TrialAuthenticationObject>(jsonString);
                    return authObj;
                }
                catch (Exception ex)
                {
                }
            }

            return null;
        }
    }
}
