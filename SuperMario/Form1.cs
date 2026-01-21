namespace SuperMario
{
    public partial class frmGioco : Form
    {
        //10:30 17/01 -> sistemato movimento a destra e sinistra, aggiunto shift per correre
        //TODO: aggiungere salto, pensare a cosa fare alla fine del livello (es.animazione automatica)

        //19:30 18/01 -> INTRODOTTO IL SALTO INVIAMO UN MESSAGIO DI CONFERMA SE TI VA BENE ALTRIMENTI TE LO CANCELLO E RIFACCIO COMMIT E PUSH

        //21:23 21/01 -> aggiornamento del salto e vari fix visivi con aggiunta di grafiche migliorative

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
            if (e.KeyCode == Keys.ShiftKey && !inAria) velocitaMuovi = 10;

            //Movimento orizzontale (else if per evitare conflitti)
            if (e.KeyCode == Keys.Right)
            {
                if (!inAria)
                    pbxPlayer.Image = Properties.Resources.SuperMario_GuardaDestra; //Immagine Mario che guarda a destra

                dirSinistra = false;
                dirDestra = true;

                direzioneBase = "destra";
            }
            else if (e.KeyCode == Keys.Left)
            {
                if(!inAria)
                    pbxPlayer.Image = Properties.Resources.SuperMario_GuardaSinistra; //Immagine Mario che guarda a sinistra

                dirDestra = false;
                dirSinistra = true;

                direzioneBase = "sinistra";
            }

            if (e.KeyCode == Keys.Space && !inAria)
            {
                salto = true;
                inAria = true;

                saltoGraduale = limiteSalto;

                pbxPlayer.Image = (direzioneBase == "destra") ? Properties.Resources.SuperMario_Salto : Properties.Resources.SuperMario_SaltoSinistra;
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
            //Bounds del giocatore rispetto al form (pbxPlayer)
            Rectangle HitBoxGiocatore = this.RectangleToClient(pbxPlayer.RectangleToScreen(pbxPlayer.ClientRectangle)); //RectangleToScreen calcola le coordinate assolute dello schermo, RectangleToClient le riporta relative al form

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
            else if (pbxPlayer.Bottom < pbxPavimento.Top) // salta solo dopo essere tornato a terra + controllo bug sottoterra
            {
                pbxPlayer.Top += saltoGraduale;
                saltoGraduale += 1;
            }           
            else
            {
                // giggy ha toccato il pavimento
                velocitaMuovi = 5;
                inAria = false;
                pbxPlayer.Top = pbxPavimento.Top - pbxPlayer.Height;

                // ritorna immagine base(quella con cui aveva saltato)
                pbxPlayer.Image = (direzioneBase == "destra") ? Properties.Resources.SuperMario_GuardaDestra : Properties.Resources.SuperMario_GuardaSinistra;
            }
        }

        private void SpostaElementi()
        {
            foreach (Control x in this.Controls)            
                if ((x.Tag == "sfondo" || x.Tag == "pavimento" || x.Tag == "blocco_speciale" || x.Tag == "blocco") && pbxSfondo.Right != this.Right)
                    x.Left -= velocitaMuovi;            
            if (pbxPlayer.Parent == pbxSfondo)
                pbxPlayer.Left += velocitaMuovi; // Compensa se Mario è figlio dello sfondo
        }
    }
}
