namespace BiometricsProject
{
    partial class main
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.sidebarTreeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contentPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer.Panel1.Controls.Add(this.sidebarTreeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.contentPanel);
            this.splitContainer.Size = new System.Drawing.Size(1087, 467);
            this.splitContainer.SplitterDistance = 270;
            this.splitContainer.TabIndex = 0;
            // 
            // sidebarTreeView
            // 
            this.sidebarTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sidebarTreeView.Font = new System.Drawing.Font("Arial", 11F);
            this.sidebarTreeView.FullRowSelect = true;
            this.sidebarTreeView.ImageIndex = 0;
            this.sidebarTreeView.ImageList = this.imageList1;
            this.sidebarTreeView.Indent = 20;
            this.sidebarTreeView.Location = new System.Drawing.Point(0, 0);
            this.sidebarTreeView.Name = "sidebarTreeView";
            this.sidebarTreeView.SelectedImageIndex = 0;
            this.sidebarTreeView.Size = new System.Drawing.Size(270, 467);
            this.sidebarTreeView.TabIndex = 1;
            this.sidebarTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.sidebarTreeView_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.contentPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(813, 467);
            this.contentPanel.TabIndex = 0;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1087, 467);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SWU-SHS AMS Biometrics System";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView sidebarTreeView;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.ImageList imageList1;
    }
}
