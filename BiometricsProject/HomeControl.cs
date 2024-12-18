using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiometricsProject
{
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
            InitializeHomeContent(); // Add any initialization logic here
        }

        private void InitializeHomeContent()
        {
            Label welcomeLabel = new Label
            {
                Text = "Welcome to SWU-SHS AMS Biometrics System",
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold)
            };

            this.Controls.Add(welcomeLabel);
        }
    }
}
