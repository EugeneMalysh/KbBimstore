using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace KbBimstore.KbBimStoreTrialLicensing
{
    class TrialRegistration
    {
        private long registrationid = -1;
        private string processorid = "";
        private string harddriveid = "";
        private string macaddress = "";
        private string registrationip = "";
        private short registrationstate = -1;
        private DateTime registrationtime = DateTime.Now;

        public TrialRegistration() { }

        public TrialRegistration(long registrationid, string processorid, string harddriveid, string macaddress, string registrationip, short registrationstate, DateTime registrationtime)
        {
            this.registrationid = registrationid;
            this.processorid = processorid;
            this.harddriveid = harddriveid;
            this.macaddress = macaddress;
            this.registrationip = registrationip;
            this.registrationstate = registrationstate;
            this.registrationtime = registrationtime;
        }

        public long getRegistrationId() { return registrationid; }
        public string getProcessorId() { return processorid; }
        public string getHardDriveId() { return harddriveid; }
        public string getMACAddress() { return macaddress; }
        public string getRegistrationIP() { return registrationip; }
        public short getRegistrationState() { return registrationstate; }
        public DateTime getRegistrationTime() { return registrationtime; }

        public void setRegistrationId(long registrationid) { this.registrationid = registrationid; }
        public void setProcessorId(string processorid) { this.processorid = processorid; }
        public void setHardDriveId(string harddriveid) { this.harddriveid = harddriveid; }
        public void setMACAddress(string macaddress) { this.macaddress = macaddress; }
        public void setRegistrationIP(string registrationip) { this.registrationip = registrationip; }
        public void setRegistrationState(short registrationstate) { this.registrationstate = registrationstate; }
        public void setRegistrationTime(DateTime registrationtime) { this.registrationtime = registrationtime; }

        public string toJSON()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }

        public bool fromJSON(string jsonstr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            TrialRegistration result = null;

            try
            {
                object resultobj = serializer.Deserialize(jsonstr, this.GetType());
                if (resultobj is TrialRegistration)
                {
                    result = resultobj as TrialRegistration;
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            if (result != null)
            {
                this.registrationid = result.registrationid;
                this.processorid = result.processorid;
                this.harddriveid = result.harddriveid;
                this.macaddress = result.macaddress;
                this.registrationip = result.registrationip;
                this.registrationstate = result.registrationstate;
                this.registrationtime = result.registrationtime;
            }

            return false;
        }

        static TrialRegistration getComputerTrialRegistration()
        {
            TrialRegistration result = new TrialRegistration();

            return result;
        }
    }
}
