using AppCamera;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppCamera
{
    internal class Photo : IPhoto
    {
        private DateTime m_date;
        private int m_facteurCompression;
        private enuFlash m_flash;
        private enuSensibilite m_sensibilite;
        private Size m_tailleEnPixels;
        public const int TAILLE_PIXEL = 3;
        public const int LARGEUR_MIN = 640;
        public const int LARGEUR_MAX = 8192;
        public const int HAUTEUR_MIN = 480;
        public const int HAUTEUR_MAX = 8192;
        public const int COMPRESSION_MIN = 1;
        public const int COMPRESSION_MAX = 500;

        public Photo( Size pTailleEnPixels,int pFacteurCompression,enuSensibilite pSensibilite, enuFlash pFlash )
        {   if( pTailleEnPixels.Width>=LARGEUR_MIN&&pTailleEnPixels.Width<=LARGEUR_MAX&&
                pTailleEnPixels.Height>=HAUTEUR_MIN&&pTailleEnPixels.Height<=HAUTEUR_MAX)
            {
                m_tailleEnPixels = pTailleEnPixels;

            }
            else
            {
                throw new ArgumentOutOfRangeException();

            }
            if(pFacteurCompression>=COMPRESSION_MIN&&pFacteurCompression<=COMPRESSION_MAX)
            {
                m_facteurCompression = pFacteurCompression;
            }
            else 
            {
                throw new ArgumentOutOfRangeException();
            }
            m_date = DateTime.Now;
            m_flash = pFlash;
            m_sensibilite = pSensibilite;
        }
        public Photo(string pInfosPhoto)
        {
            m_tailleEnPixels=new Size(0,0);
            string[] tableu = pInfosPhoto.Split(',');
            m_tailleEnPixels.Width = int.Parse(tableu[0]);
            m_tailleEnPixels.Height = int.Parse(tableu[1]);
            m_flash = (enuFlash)Enum.Parse(typeof(enuFlash), tableu[2]);
            m_sensibilite=(enuSensibilite)Enum.Parse(typeof(enuSensibilite), tableu[3]);
            m_facteurCompression = int.Parse(tableu[4]);
            m_date = DateTime.Parse(tableu[5]);

        }
        public override string ToString()
        {

            return m_tailleEnPixels.Width+","+m_tailleEnPixels.Height+","+m_flash+","+m_sensibilite+","+m_facteurCompression+","+m_date.ToString("yyyy-MM-dd HH:mm:ss");

        }
        public DateTime Date
        {
            get { return m_date; }

        }

        public int FacteurCompression
        {
            get { return m_facteurCompression; }
        }

        public enuFlash Flash
        {
            get { return m_flash; }
        }

        public enuSensibilite Sensibilite
        {
            get { return m_sensibilite; }
        }
        public int TailleEnOctets
        {

            get
            {
                int i = 0;
                i = (m_tailleEnPixels.Width * m_tailleEnPixels.Height * TAILLE_PIXEL) / m_facteurCompression;
                return i;
            }
        }
        public Size TailleEnPixels 
        {
            get { return m_tailleEnPixels; }

        }

    }
}
