using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace KbBimstore.KbBimStoreTrialLicensing
{
    public class TrialWebServiceInterface
    {
        public static int doOperation(string operationURL)
        {
            if (operationURL != null)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    TrialAuthenticationObject authObject = TrialComputerInfo.getAuthenticationObject();

                    string authJson = authObject.toJsonString();
                    //MessageBox.Show("compinfo=" + authJson);
                    StringContent content = new StringContent(authJson, Encoding.UTF8, "application/json");

                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                    System.Threading.Tasks.Task<HttpResponseMessage> task = client.PostAsync((TrialSettings.trialLicenseServicesURL + operationURL), content);


                    HttpResponseMessage response = task.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        HttpContent responseContent = response.Content;                        
                        if (response != null)
                        {
                            string message = responseContent.ReadAsStringAsync().Result;
                            
                            if (message != null)
                            {
                                if (!message.StartsWith("error"))
                                {
                                    try
                                    {
                                        int expDays = Convert.ToInt32(message);

                                        return expDays;
                                    }
                                    catch (Exception ex3)
                                    {                                        
                                        return -70000007;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(message);
                                    return -60000006;
                                }
                            }
                            else
                            {
                                return -50000005;
                            }
                        }
                        else
                        {
                            return -40000004;
                        }
                    }
                    else
                    {
                        return -30000003;
                    }

                }
                catch (Exception ex2)
                {
                    return -20000002;
                }
            }

            return -10000001;
        }

    }
}
