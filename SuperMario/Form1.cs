namespace SuperMario
{
    public partial class frmGioco : Form
    {
        #region NOTE_AGGIORNAMENTI
        //10:30 17/01 -> sistemato movimento a destra e sinistra, aggiunto shift per correre
        //TODO: aggiungere salto, pensare a cosa fare alla fine del livello (es.animazione automatica)

        //19:30 18/01 -> INTRODOTTO IL SALTO INVIAMO UN MESSAGIO DI CONFERMA SE TI VA BENE ALTRIMENTI TE LO CANCELLO E RIFACCIO COMMIT E PUSH

        //21:23 21/01 -> aggiornamento del salto e vari fix visivi con aggiunta di grafiche migliorate

        //TODO: UNA VOLTA AVER AGGIUNTO I BLOCCHI CONTROLLARE SE POSSO SALTARE
        //+TODO(FACOLTATIVO PER IL MOMENTO): IMPLEMENTARE UN SALTO GRADUALE PER MARIO E CREARE UN MENU INZIALE PER SELEZIONARE IL PERSONAGGIO
        #endregion

        //Variabili di movimento

        //Direzione sguardo
        bool dirDestra = false;
        bool dirSinistra = false;

        //Stato personaggio
        bool salto = false;
        bool inAria = false;
        bool suBlocco = false;

        int velocitaMuovi = 5;  //Velocita di movimento
        int limiteSalto = 15;   //Limite salto (quanto puo saltare)

        int saltoGraduale = 0;  //Contatore che nel salto viene incrementato fino a limiteSalto durante la salita e decrementato durante la discesa

        string direzioneBase = "destra"; //Ultima direzione in cui guardava il personaggio (per sapere che immagine usare quando atterra)

        public frmGioco()
        {
            InitializeComponent();

        }

        private void frmGioco_KeyDown(object sender, KeyEventArgs e)
        {
            // Shift per scattare (non in volo)
            if (e.KeyCode == Keys.ShiftKey && !inAria) velocitaMuovi = 10;

            //Movimento orizzontale (else if per evitare conflitti)

            //Guarda a destra
            if (e.KeyCode == Keys.Right)
            {
                //Cambio immagine in base alla direzione se non è in aria
                if (!inAria)
                    pbxPlayer.Image = Properties.Resources.SuperMario_GuardaDestra; //Immagine Mario che guarda a destra

                //Aggiorna variabili di direzione
                dirSinistra = false;
                dirDestra = true;

                direzioneBase = "destra";
            }
            //Guarda a sinistra
            else if (e.KeyCode == Keys.Left)
            {
                //Cambio immagine in base alla direzione se non è in aria
                if (!inAria)
                    pbxPlayer.Image = Properties.Resources.SuperMario_GuardaSinistra; //Immagine Mario che guarda a sinistra

                //Aggiorna variabili di direzione
                dirDestra = false;
                dirSinistra = true;

                direzioneBase = "sinistra";
            }

            //Salto (solo se non è già in aria)
            if (e.KeyCode == Keys.Space && !inAria)
            {
                //Cambia immagine in base alla direzione
                pbxPlayer.Image = (direzioneBase == "destra") ? Properties.Resources.SuperMario_Salto : Properties.Resources.SuperMario_SaltoSinistra;

                //Aggiorna variabili di salto
                salto = true;
                inAria = true;

                //Inizializza contatore salto
                saltoGraduale = limiteSalto;
            }
        }

        private void frmGioco_KeyUp(object sender, KeyEventArgs e)
        {
            //Rilasciando shift torna velocita normale
            if (e.KeyCode == Keys.ShiftKey) velocitaMuovi = 5;

            //Modifica variabili di direzione del movimento orizzontale
            if (e.KeyCode == Keys.Right) dirDestra = false;
            else if (e.KeyCode == Keys.Left) dirSinistra = false;
        }

        private void tmrGioco_Tick(object sender, EventArgs e)
        {
            //Bounds del giocatore rispetto al form (pbxPlayer)
            Rectangle HitBoxGiocatore = this.RectangleToClient(pbxPlayer.RectangleToScreen(pbxPlayer.ClientRectangle)); //RectangleToScreen calcola le coordinate assolute dello schermo, RectangleToClient le riporta relative al form

            int centroSchermo = this.ClientRectangle.Width / 2; //Centro dello schermo (per lo spostamento degli elementi)

            // Movimento orizzontale
            if (dirDestra && HitBoxGiocatore.Right <= centroSchermo)
                pbxPlayer.Left += velocitaMuovi;
            else if (dirSinistra && HitBoxGiocatore.Left > 0)
                pbxPlayer.Left -= velocitaMuovi;
            else if (dirDestra && HitBoxGiocatore.Right > centroSchermo)
                SpostaElementi();

            //Salto
            if (salto)  
            {
                //pbxPlayer si sposta verso l'alto fino a quando saltoGraduale > 0
                
                pbxPlayer.Top -= saltoGraduale;
                saltoGraduale -= 1; //Decrementa il contatore del salto
                if (saltoGraduale <= 0)
                {
                    salto = false; //Quando il contatore arriva a 0 il salto termina
                    suBlocco = false;
                }
            }

            //Quando durante la discesa pbxPlayer arriva sotto il pavimento, lo fa riscendere gradualmente fino a poggiare sul pavimento
            else if (HitBoxGiocatore.Bottom < pbxPavimento.Top && !suBlocco) 
            {
                pbxPlayer.Top += saltoGraduale;
                saltoGraduale += 1;
            }

            //Altrimenti se si trova sopra il pavimento e non sta saltando, lo posiziona sul pavimento
            else if(HitBoxGiocatore.Bottom >=  pbxPavimento.Top)
            {                             
                //Modifica l'immagine in base alla direzione
                pbxPlayer.Image = (direzioneBase == "destra") ? Properties.Resources.SuperMario_GuardaDestra : Properties.Resources.SuperMario_GuardaSinistra;

                //Posiziona pbxPlayer sul pavimento
                pbxPlayer.Top = pbxPavimento.Top - pbxPlayer.Height;

                //Modifica variabili di stato
                inAria = false;
            }

            //Se entra a contatto con pbxBloccoSpeciale (ovvero il blocco con il punto interrogativo)
            if (pbxPlayer.Bounds.IntersectsWith(pbxBloccoSpeciale.Bounds))
            {
                //Se non si trova sul blocco ed entra in contatto con la parte superiore del blocco
                if(!suBlocco && pbxBloccoSpeciale.Top > pbxPavimento.Bottom)
                {
                    //Resta sopra al mattone
                    pbxPlayer.Image = (direzioneBase == "destra") ? Properties.Resources.SuperMario_GuardaDestra : Properties.Resources.SuperMario_GuardaSinistra;

                    pbxPlayer.Top = pbxBloccoSpeciale.Top - pbxPlayer.Height;
                    inAria = false;
                    salto = false;
                    suBlocco = true;
                    saltoGraduale = 0;
                }

                else if(!suBlocco && pbxBloccoSpeciale.Top + pbxBloccoSpeciale.Height >= pbxPlayer.Top && pbxPlayer.Bottom > pbxBloccoSpeciale.Top && (HitBoxGiocatore.Right > pbxBloccoSpeciale.Left || HitBoxGiocatore.Left < pbxBloccoSpeciale.Right))
                {
                    //Colpisce il mattone da sotto                   
                    pbxPlayer.Top = pbxBloccoSpeciale.Bottom;
                    salto = false;
                    saltoGraduale = 1;
                }
                else if(!suBlocco && pbxPlayer.Right >= pbxBloccoSpeciale.Left)
                {
                    velocitaMuovi = 0;
                }
                velocitaMuovi = 5;
            }

            //Modifica di su blocco
            if (suBlocco && (pbxPlayer.Right < pbxBloccoSpeciale.Left ||  pbxPlayer.Left > pbxBloccoSpeciale.Right))
                suBlocco = false;
        }

        /// <summary>
        /// Metodo che sposta gli elementi di sfondo e pavimento quando il personaggio si muove a destra
        /// </summary>
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
