namespace SuperMario
{
    partial class frmGioco
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGioco));
            pbxSfondo = new PictureBox();
            pbxPavimento = new PictureBox();
            pbxPlayer = new PictureBox();
            pbxBloccoSpeciale = new PictureBox();
            tmrGioco = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pbxSfondo).BeginInit();
            pbxSfondo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxPavimento).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxPlayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxBloccoSpeciale).BeginInit();
            SuspendLayout();
            // 
            // pbxSfondo
            // 
            pbxSfondo.BackgroundImage = Properties.Resources.SuperMario_Background;
            pbxSfondo.Controls.Add(pbxPavimento);
            pbxSfondo.Controls.Add(pbxPlayer);
            pbxSfondo.Controls.Add(pbxBloccoSpeciale);
            pbxSfondo.Location = new Point(0, 0);
            pbxSfondo.Name = "pbxSfondo";
            pbxSfondo.Size = new Size(3570, 390);
            pbxSfondo.SizeMode = PictureBoxSizeMode.CenterImage;
            pbxSfondo.TabIndex = 0;
            pbxSfondo.TabStop = false;
            pbxSfondo.Tag = "sfondo";
            // 
            // pbxPavimento
            // 
            pbxPavimento.BackColor = Color.Transparent;
            pbxPavimento.BackgroundImage = Properties.Resources.SuperMario_Pavimento;
            pbxPavimento.Location = new Point(0, 326);
            pbxPavimento.Name = "pbxPavimento";
            pbxPavimento.Size = new Size(3695, 65);
            pbxPavimento.TabIndex = 1;
            pbxPavimento.TabStop = false;
            pbxPavimento.Tag = "pavimento";
            // 
            // pbxPlayer
            // 
            pbxPlayer.BackColor = Color.Transparent;
            pbxPlayer.BackgroundImageLayout = ImageLayout.None;
            pbxPlayer.Image = Properties.Resources.SuperMario_GuardaDestra;
            pbxPlayer.Location = new Point(77, 297);
            pbxPlayer.Margin = new Padding(0);
            pbxPlayer.Name = "pbxPlayer";
            pbxPlayer.Size = new Size(32, 29);
            pbxPlayer.SizeMode = PictureBoxSizeMode.AutoSize;
            pbxPlayer.TabIndex = 2;
            pbxPlayer.TabStop = false;
            pbxPlayer.Tag = "player";
            // 
            // pbxBloccoSpeciale
            // 
            pbxBloccoSpeciale.BackColor = Color.Transparent;
            pbxBloccoSpeciale.Image = Properties.Resources.SuperMario_BloccoSpeciale;
            pbxBloccoSpeciale.Location = new Point(287, 217);
            pbxBloccoSpeciale.Name = "pbxBloccoSpeciale";
            pbxBloccoSpeciale.Size = new Size(32, 32);
            pbxBloccoSpeciale.SizeMode = PictureBoxSizeMode.AutoSize;
            pbxBloccoSpeciale.TabIndex = 1;
            pbxBloccoSpeciale.TabStop = false;
            // 
            // tmrGioco
            // 
            tmrGioco.Enabled = true;
            tmrGioco.Interval = 20;
            tmrGioco.Tick += tmrGioco_Tick;
            // 
            // frmGioco
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(784, 391);
            Controls.Add(pbxSfondo);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmGioco";
            Text = "Super Mario Bros";
            KeyDown += frmGioco_KeyDown;
            KeyUp += frmGioco_KeyUp;
            ((System.ComponentModel.ISupportInitialize)pbxSfondo).EndInit();
            pbxSfondo.ResumeLayout(false);
            pbxSfondo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbxPavimento).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxPlayer).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxBloccoSpeciale).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbxSfondo;
        private PictureBox pbxPavimento;
        private PictureBox pbxPlayer;
        private System.Windows.Forms.Timer tmrGioco;
        private PictureBox pbxBloccoSpeciale;
    }
}
