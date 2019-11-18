using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Autodesk.Revit.UI;

namespace KbBimstore
{

    public partial class KBRevitWebBrowser : Page, IDockablePaneProvider
    {
        public KBRevitWebBrowser()
        {
            InitializeComponent();
        }

        #region Data
        private Guid m_targetGuid;
        private DockPosition m_position = DockPosition.Bottom;
        private int m_left = 1;
        private int m_right = 1;
        private int m_top = 1;
        private int m_bottom = 1;
        #endregion

        public void SetupDockablePane(DockablePaneProviderData data)
        {
            data.FrameworkElement = this as FrameworkElement;
            data.InitialState = new DockablePaneState();
            data.InitialState.DockPosition = DockPosition.Tabbed;
            //DockablePaneId targetPane;
            //if (m_targetGuid == Guid.Empty)
            //    targetPane = null;
            //else targetPane = new DockablePaneId(m_targetGuid);
            //if (m_position == DockPosition.Tabbed)
            data.InitialState.TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser;
            //if (m_position == DockPosition.Floating)
            //{
            //data.InitialState.SetFloatingRectangle(new Autodesk.Revit.UI.Rectangle(10, 710, 10, 710));
            //data.InitialState.DockPosition = DockPosition.Tabbed;
            //}
            //Log.Message("***Intial docking parameters***");
            //Log.Message(APIUtility.GetDockStateSummary(data.InitialState));
        }

        private bool almEnabled = false;
        public void setAlmEnabled(bool enable)
        {
            this.txtAddressBar.IsEnabled = enable;
        }

        public void SetInitialDockingParameters(int left, int right, int top, int bottom, DockPosition position,
            Guid targetGuid)
        {
            m_position = position;
            m_left = left;
            m_right = right;
            m_top = top;
            m_bottom = bottom;
            m_targetGuid = targetGuid;
        }

        private void DockableDialogs_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            webBrowser.Navigated += new NavigatedEventHandler(
                WebBrowser_Navigated);

        }

        private void WebBrowser_Navigated(
            object sender,
            NavigationEventArgs e)
        {
            HideJsScriptErrors((KBRevitWebBrowser) sender);

            txtAddressBar.Text = e.Uri.AbsoluteUri;
        }

        public void HideJsScriptErrors(KBRevitWebBrowser wb)
        {
            // IWebBrowser2 interface
            // Exposes methods that are implemented by the 
            // WebBrowser control
            // Searches for the specified field, using the 
            // specified binding constraints.

            FieldInfo fld = typeof (KBRevitWebBrowser).GetField(
                "_axIWebBrowser2",
                BindingFlags.Instance | BindingFlags.NonPublic);

            if (null != fld)
            {
                object obj = fld.GetValue(wb);
                if (null != obj)
                {
                    // Silent: Sets or gets a value that indicates 
                    // whether the object can display dialog boxes.
                    // HRESULT IWebBrowser2::get_Silent(VARIANT_BOOL *pbSilent);
                    // HRESULT IWebBrowser2::put_Silent(VARIANT_BOOL bSilent);

                    obj.GetType().InvokeMember("Silent",
                        BindingFlags.SetProperty, null, obj,
                        new object[] {true});
                }
            }
        }

        public void Connect(int connectionId, object target)
        {            
        }

        private void btnNavigate_Click(object sender, RoutedEventArgs e)
        {
            if (!txtAddressBar.Text.ToLower().StartsWith("http://") &&
                !txtAddressBar.Text.ToLower().StartsWith("https://"))
                txtAddressBar.Text = "http://" + txtAddressBar.Text;

            webBrowser.Navigate(txtAddressBar.Text);
        }

        private void txtAddressBar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnNavigate_Click(sender, null);
        }

        public void ShowLink(string urlstr)
        {
            txtAddressBar.Text = urlstr;
            webBrowser.Navigate(urlstr);
        }
    }
}