namespace SuperMario
{
    //10:30 17/01 -> sistemato movimento a destra e sinistra, aggiunto shift per correre
    //TODO: aggiungere salto, pensare a cosa fare alla fine del livello (es.animazione automatica)
    public partial class frmGioco : Form
    {
        bool dirDestra = false;
        bool dirSinistra = false;

        //Velocita' di movimento
        int velocitaMuovi = 5;

        public frmGioco()
        {
            InitializeComponent();
            bool salto = true;
        }

        private void frmGioco_KeyDown(object sender, KeyEventArgs e)
        {
            //Se preme shift, aumento la velocita' di movimento
            if (e.KeyCode == Keys.ShiftKey)
            {
                velocitaMuovi = 10;
            }
           
            //In base alla freccia premuta, imposto la direzione del movimento
            if (e.KeyCode == Keys.Right)
            {
                pbxPlayer.Image = Properties.Resources.SuperMario_GuardaDestra;

                dirDestra = true;
            }
            else if (e.KeyCode == Keys.Left)
            {
                pbxPlayer.Image = Properties.Resources.SuperMario_GuardaSinistra;

                dirSinistra = true;
            }
        }

        private void frmGioco_KeyUp(object sender, KeyEventArgs e)
        {
            //Diminuisco la velocita' di movimento quando rilascio shift
            if (e.KeyCode == Keys.ShiftKey)
            {
                velocitaMuovi = 5;
            }

            //In base alla freccia rilasciata, imposto la direzione del movimento
            if (e.KeyCode == Keys.Right)
            {
                dirDestra = false;
            }
            else if (e.KeyCode == Keys.Left)
            {
                dirSinistra = false;
            }
        }

        private void tmrGioco_Tick(object sender, EventArgs e)
        {
            //Ottengo l'HitBox del giocatore (i due metodi sotto servono per convertire le coordinate locali in coordinate globali e viceversa)
            Rectangle HitBoxGiocatore = this.RectangleToClient(pbxPlayer.RectangleToScreen(pbxPlayer.ClientRectangle));
            int centroSchermo = this.ClientRectangle.Width / 2;

            //Gestione del movimento del giocatore nella prima metà dello schermo

            //Se guarda a destra può raggiungere solo il centro dello schermo
            if (dirDestra && HitBoxGiocatore.Right <= centroSchermo)
            {
                pbxPlayer.Left += velocitaMuovi; 
            }
            //Se guarda a sinistra può raggiungere solo il bordo sinistro dello schermo (else if per evitare bidirezionamenti)
            else if (dirSinistra && HitBoxGiocatore.Left > 0)
            {
                pbxPlayer.Left -= velocitaMuovi;
            }

            //Gestione del movimento dello sfondo

            //Se guarda a destra e supera il centro dello schermo, muovo lo sfondo a sinistra
            if(dirDestra && HitBoxGiocatore.Right > centroSchermo)
            {
                SpostaElementi();
            }
        }

        private void SpostaElementi()
        {
            // Sposto gli elementi di primo livello (es. pbxSfondo)
            foreach (Control x in this.Controls)
                if (pbxSfondo.Right != this.Right && x.Tag == "sfondo" || x.Tag == "pavimento" || x.Tag == "blocco_speciale" || x.Tag == "blocco")
                    x.Left -= velocitaMuovi;                            

            // Se il player è figlio di pbxSfondo, compenso il suo Left locale
            if (pbxPlayer.Parent == pbxSfondo)            
                pbxPlayer.Left += velocitaMuovi;
            
        }
    }
}
