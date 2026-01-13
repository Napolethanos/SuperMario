namespace SuperMario
{
    public partial class frmGioco : Form
    {
        bool dirDestra = false;
        bool dirSinistra = false;

        bool salto = false;

        //Velocita' di movimento
        int velocitaMuovi = 5;


        public frmGioco()
        {
            InitializeComponent();
        }
             // c
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
            if (e.KeyCode == Keys.Left)
            {
                pbxPlayer.Image = Properties.Resources.SuperMario_GuardaSinistra;

                dirSinistra = true;
            }

            //Se premo spazio, inizio il salto
            if (e.KeyCode == Keys.Space && salto == false)
            {
                salto = true;
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
            if (e.KeyCode == Keys.Left)
            {
                dirSinistra = false;
            }
        }

        private void tmrGioco_Tick(object sender, EventArgs e)
        {
            //Gestione del movimento del giocatore

            //Se guarda a destra e raggiunge il centro dello schermo, muovo lo sfondo a sinistra
            if(dirDestra && pbxPlayer.Right < this.ClientSize.Width / 2)
            {
                pbxPlayer.Left += velocitaMuovi;//INSERIRE VELOCITA' DESTRA
            }
            if (dirSinistra && pbxPlayer.Left > 0)
            {
                pbxPlayer.Left -= velocitaMuovi;//INSERIRE VELOCITA' SINISTRA
            }

            //Se guarda a destra e supera il centro dello schermo, muovo lo sfondo a sinistra
            if(dirDestra && pbxPlayer.Right >= this.ClientSize.Width / 2)
            {
                SpostaElementi();
            }
        }

        private void SpostaElementi()
        {
            foreach(Control x in this.Controls)
            {
                if(x.Tag == "sfondo" || x.Tag == "pavimento" || x.Tag == "blocco_speciale" || x.Tag == "blocco")
                {
                    x.Left -= velocitaMuovi;
                }
            }
        }

    }
}
