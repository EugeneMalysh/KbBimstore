using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace KbBimstore.KbBimStoreTrialLicensing
{
    class TrialConnection
    {
        private long connectionid = -1;
        private long registrationid = -1;
        private string connectionip = "";
        private short connectionstate = -1;
        private DateTime connectiontime = DateTime.Now;
        private DateTime disconnectiontime = DateTime.Now;

        public TrialConnection() { }

        public TrialConnection(long connectionid, long registrationid, string connectionip, short connectionstate, DateTime connectiontime, DateTime disconnectiontime)
        {
            this.connectionid = connectionid;
            this.registrationid = registrationid;
            this.connectionip = connectionip;
            this.connectionstate = connectionstate;
            this.connectiontime = connectiontime;
            this.disconnectiontime = disconnectiontime;
        }

        public long getConnectionId() { return connectionid; }
        public long getRegistrationId() { return registrationid; }
        public string getConnectionIP() { return connectionip; }
        public short getConnectionState() { return connectionstate; }
        public DateTime getConnectionTime() { return connectiontime; }
        public DateTime getDisconnectionTime() { return disconnectiontime; }

        public void setConnectionId(long connectionid) { this.connectionid = connectionid; }
        public void setRegistrationId(long registrationid) { this.registrationid = registrationid; }
        public void setConnectionIP(string connectionip) { this.connectionip = connectionip; }
        public void setConnectionState(short connectionstate) { this.connectionstate = connectionstate; }
        public void setConnectionTime(DateTime connectiontime) { this.connectiontime = connectiontime; }
        public void setDisconnectionTime(DateTime disconnectiontime) { this.disconnectiontime = disconnectiontime; }

        public string toJSON()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(this);
        }

        public bool fromJSON(string jsonstr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            TrialConnection result = null;

            try
            {
                object resultobj = serializer.Deserialize(jsonstr, this.GetType());
                if (resultobj is TrialConnection)
                {
                    result = resultobj as TrialConnection;
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            if (result != null)
            {
                this.connectionid = result.connectionid;
                this.registrationid = result.registrationid;
                this.connectionip = result.connectionip;
                this.connectionstate = result.connectionstate;
                this.connectiontime = result.connectiontime;
                this.disconnectiontime = result.disconnectiontime;
            }

            return false;
        }

        static TrialConnection getComputerTrialConnection()
        {
            TrialConnection result = new TrialConnection();

            return result;
        }

    }
}
