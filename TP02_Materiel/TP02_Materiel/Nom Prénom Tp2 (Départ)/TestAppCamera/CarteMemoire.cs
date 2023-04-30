using AppCamera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppCamera
{
    internal class CarteMemoire : ICarteMemoire
    {
        private List<IPhoto> m_colPhotos;
        private long m_escpaceUltilise;
        private string m_nom;
        private long m_tailleEnOctets;
        public const int TAILLE_MIN = 8; //  8 Mo
        public const int TAILLE_MAX = 8192; // 8192 Mo
        public const string ERR_MSG_ESPACE_INSUFFISANT = "Espace insuffisant";
        public long EspaceDisponible => throw new NotImplementedException();

        public long EspaceUtilise => throw new NotImplementedException();

        public int NbPhotos => throw new NotImplementedException();

        public string Nom => throw new NotImplementedException();

        public long TailleEnOctets => throw new NotImplementedException();

        public void Ajouter(IPhoto pobjPhoto)
        {
            throw new NotImplementedException();
        }

        public void ChargerPhotos()
        {
            throw new NotImplementedException();
        }

        public void EnregistrerPhotos()
        {
            throw new NotImplementedException();
        }

        public bool PeutAjouter(IPhoto pobjPhoto)
        {
            throw new NotImplementedException();
        }

        public IPhoto PhotoAt(int pPosition)
        {
            throw new NotImplementedException();
        }

        public void SupprimerAt(int pPosition)
        {
            throw new NotImplementedException();
        }

        public void Vider()
        {
            throw new NotImplementedException();
        }
    }
}
