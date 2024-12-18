using System;
using System.Windows.Forms;

namespace BiometricsProject
{
    public partial class main : Form
    {
        private DPFP.Template Template;

        public main()
        {
            InitializeComponent();
            InitializeSidebarItems(); // Populate the sidebar
            LoadDefaultContent(); // Load Home content by default
        }

        private void InitializeSidebarItems()
        {
            // Create TreeView nodes with image index
            TreeNode homeNode = new TreeNode("Home", 0, 0); // Folder icon for Home
            TreeNode attendanceNode = new TreeNode("Attendance", 0, 0);
            TreeNode userManagementNode = new TreeNode("User Management", 0, 0);
            TreeNode helpNode = new TreeNode("Help", 0, 0);
            TreeNode exitNode = new TreeNode("Exit", 0, 0);

            // Add child nodes for User Management with file icons
            userManagementNode.Nodes.Add(new TreeNode("Verify", 1, 1)); // Sub-folder/file icon
            userManagementNode.Nodes.Add(new TreeNode("Enroll", 1, 1));

            // Add child nodes for Help
            helpNode.Nodes.Add(new TreeNode("Documentation", 1, 1));
            helpNode.Nodes.Add(new TreeNode("About", 1, 1));

            // Add nodes to the TreeView
            sidebarTreeView.Nodes.Add(homeNode);
            sidebarTreeView.Nodes.Add(attendanceNode);
            sidebarTreeView.Nodes.Add(userManagementNode);
            sidebarTreeView.Nodes.Add(helpNode);
            sidebarTreeView.Nodes.Add(exitNode);

            sidebarTreeView.ExpandAll();

            // Select the Home node by default
            sidebarTreeView.SelectedNode = homeNode;
        }


        private void LoadDefaultContent()
        {
            // Load Home content by default
            LoadContent(new HomeControl()); // Ensure you have a HomeControl implemented
        }

        private void LoadContent(Control control)
        {
            contentPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(control);
        }

        private void sidebarTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedNode = e.Node.Text;

            switch (selectedNode)
            {
                case "Home":
                    LoadContent(new HomeControl()); // Load Home content
                    break;

                case "Attendance":
                    attendance AtFrm = new attendance();
                    AtFrm.ShowDialog(); // Opens Attendance as a modal dialog
                    break;

                case "Verify":
                    var verifyWrapper = new VerifyControlWrapper(Template);
                    LoadContent(verifyWrapper);
                    break;

                case "Enroll":
                    var enrollWrapper = new EnrollControlWrapper();
                    LoadContent(enrollWrapper);
                    break;

                case "Settings":
                    MessageBox.Show("Settings panel goes here!", "Settings");
                    break;

                case "Documentation":
                    MessageBox.Show("Documentation content goes here!", "Help");
                    break;

                case "About":
                    MessageBox.Show("SWU-SHS AMS Biometrics System\nVersion 1.0", "About");
                    break;

                case "Exit":
                    Application.Exit();
                    break;

                default:
                    MessageBox.Show("Option not implemented yet!", "Information");
                    break;
            }

            // Reset the selected node to ensure AfterSelect triggers on re-click
            sidebarTreeView.SelectedNode = null;
        }

    }
}
