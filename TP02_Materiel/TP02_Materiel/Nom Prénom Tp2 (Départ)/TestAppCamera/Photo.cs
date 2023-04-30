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
        public DateTime Date => throw new NotImplementedException();

        public int FacteurCompression => throw new NotImplementedException();

        public enuFlash Flash => throw new NotImplementedException();

        public enuSensibilite Sensibilite => throw new NotImplementedException();

        public int TailleEnOctets => throw new NotImplementedException();

        public Size TailleEnPixels => throw new NotImplementedException();
    }
}
