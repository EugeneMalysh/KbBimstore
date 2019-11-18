using System.Drawing;
using System.Windows.Forms;

namespace KbBimstore.KBRevitLicensing
{
    /// <summary>SplashScreenForm form class implementation</summary>
    public partial class SplashScreenForm : Form
    {
        #region Delegate Functions
        delegate void StringParameterDelegate(string Text);
        delegate void SplashShowCloseDelegate();
        #endregion

        #region Private Memeber Varibles
        private bool CloseSplashScreenFlag = false;
        #endregion

        #region Public Constructor
        /// <summary>Default constructor</summary>
        public SplashScreenForm()
        {
            InitializeComponent();
            this.lblStatus.Parent = this.pictureBox;
            this.lblStatus.BackColor = Color.Transparent;
            lblStatus.ForeColor = Color.Blue;
            progressBar.Show();
        }
        #endregion

        #region Public Methods
        /// <summary>Displays the splashscreen</summary>
        public void ShowSplashScreen()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(ShowSplashScreen));
                return;
            }
            this.Show();
            Application.Run(this);
        }

        /// <summary>Closes the SplashScreen</summary>
        public void CloseSplashScreen()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(CloseSplashScreen));
                return;
            }
            CloseSplashScreenFlag = true;
            this.Close();
        }

        /// <summary>Update text in default green color of success message </summary>
        /// <param name="Text">Message</param>
        public void UdpateStatusText(string Text)
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new StringParameterDelegate(UdpateStatusText), new object[] { Text });
                return;
            }

            // Must be on the UI thread if we've got this far
            lblStatus.ForeColor = Color.Blue;
            lblStatus.Text = Text;
        }
        #endregion

        #region Form Event Handler
        /// <summary>Prevents the closing of form other than by calling the CloseSplashScreen function </summary>
        /// <param name="sender">object</param>
        /// <param name="e">FormClosingEventArgs</param>
        private void SplashForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseSplashScreenFlag == false)
                e.Cancel = true;
        }
        #endregion
    }
}