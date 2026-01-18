namespace SuperMario
{
    public partial class frmGioco : Form
    {
        // AGGIORNATO IL 18/01 19:30
        // INTRODOTTO IL SALTO INVIAMO UN MESSAGIO DI CONFERMA SE TI VA BENE ALTRIMENTI TE LO CANCELLO E RIFACCIO COMMIT E PUSH
        //TODO: UNA VOLTA AVER AGGIUNTO I BLOCCHI CONTROLLARE SE POSSO SALTARE
        //+TODO(FACOLTATIVO PER IL MOMENTO): IMPLEMENTARE UN SALTO GRADUALE PER MARIO E CREARE UN MENU INZIALE PER SELEZIONARE IL PERSONAGGIO
        bool dirDestra = false;
        bool dirSinistra = false;
        bool salto = false;
        bool inAria = false;

        int velocitaMuovi = 5;
        int limiteSalto = 15;       // altezza del salto la modifichiamo in futuro boy
        int velocitaGravita = 5;    // gravita
        int saltoGraduale = 0;       // contatore salto serve per far scendere il bro in modo graduale pardendo ds 15 cioe da quanto puo saltare
        string direzioneBase = "destra"; // sarebbe l'ultima direzzione cosi torno nell'immagine della relativa direzione

        public frmGioco()
        {
            InitializeComponent();
        }

        private void frmGioco_KeyDown(object sender, KeyEventArgs e)
        {
            // Shift per correre
            if (e.KeyCode == Keys.ShiftKey) velocitaMuovi = 10;

            if (e.KeyCode == Keys.Right)
            {
                pbxPlayer.Image = Properties.Resources.SuperMario_GuardaDestra;
                dirDestra = true;
                direzioneBase = "destra";
            }
            else if (e.KeyCode == Keys.Left)
            {
                pbxPlayer.Image = Properties.Resources.SuperMario_GuardaSinistra;
                dirSinistra = true;
                direzioneBase = "sinistra";
            }

            if (e.KeyCode == Keys.Space && !inAria)
            {
                salto = true;
                inAria = true;
                saltoGraduale = limiteSalto;
                pbxPlayer.Image = Properties.Resources.SuperMario_Salto;
            }
        }

        private void frmGioco_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) velocitaMuovi = 5;

            if (e.KeyCode == Keys.Right) dirDestra = false;
            else if (e.KeyCode == Keys.Left) dirSinistra = false;
        }

        private void tmrGioco_Tick(object sender, EventArgs e)
        {
            Rectangle HitBoxGiocatore = this.RectangleToClient(pbxPlayer.RectangleToScreen(pbxPlayer.ClientRectangle));
            int centroSchermo = this.ClientRectangle.Width / 2;

            // Movimento orizzontale
            if (dirDestra && HitBoxGiocatore.Right <= centroSchermo)
                pbxPlayer.Left += velocitaMuovi;
            else if (dirSinistra && HitBoxGiocatore.Left > 0)
                pbxPlayer.Left -= velocitaMuovi;
            else if (dirDestra && HitBoxGiocatore.Right > centroSchermo)
                SpostaElementi();

            if (salto) //salto 
            {
                pbxPlayer.Top -= saltoGraduale;
                saltoGraduale -= 1;
                if (saltoGraduale <= 0) salto = false;
            }
            else if (pbxPlayer.Bottom < pbxPavimento.Top)
            {
                pbxPlayer.Top += velocitaGravita;

                // torna immagine base mentre cade
                if (!salto)
                {
                    pbxPlayer.Image = (direzioneBase == "destra") ? Properties.Resources.SuperMario_GuardaDestra : Properties.Resources.SuperMario_GuardaSinistra;
                }
            }
            else
            {
                // giggy ha toccato il pavimento
                inAria = false;
                pbxPlayer.Top = pbxPavimento.Top - pbxPlayer.Height;

                // ritorna immagine base(quella con cui aveva saltato)
                pbxPlayer.Image = (direzioneBase == "destra") ? Properties.Resources.SuperMario_GuardaDestra : Properties.Resources.SuperMario_GuardaSinistra;
            }
        }

        private void SpostaElementi()
        {
            foreach (Control x in this.Controls)
            {
                if ((x.Tag == "sfondo" || x.Tag == "pavimento" || x.Tag == "blocco_speciale" || x.Tag == "blocco")
                    && pbxSfondo.Right != this.Right)
                {
                    x.Left -= velocitaMuovi;
                }
            }
            if (pbxPlayer.Parent == pbxSfondo)
                pbxPlayer.Left += velocitaMuovi; // Compensa se Mario è figlio dello sfondo
        }
    }
}
