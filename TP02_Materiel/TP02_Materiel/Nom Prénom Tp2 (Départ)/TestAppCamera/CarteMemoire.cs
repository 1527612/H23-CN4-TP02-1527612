using AppCamera;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppCamera
{
    internal class CarteMemoire : ICarteMemoire
    {
        private List<IPhoto> m_colPhotos;
        private long m_espaceUtilisé;
        private string m_nom;
        private long m_tailleEnOctets;
        public const int TAILLE_MIN = 8; //  8 Mo
        public const int TAILLE_MAX = 8192; // 8192 Mo
        public const string ERR_MSG_ESPACE_INSUFFISANT = "Espace insuffisant";

        public CarteMemoire(string pNom, long pTailleEnMegaOctets)
        {
            m_colPhotos=new List<IPhoto>();
            m_espaceUtilisé=0;
            m_nom = pNom;
            if(pTailleEnMegaOctets >=TAILLE_MIN&&pTailleEnMegaOctets<=TAILLE_MAX)
            {
                m_tailleEnOctets = pTailleEnMegaOctets*1024L*1024;
            }
            else
            {
                throw new ArgumentOutOfRangeException();

            }
        }

        public long EspaceDisponible
        { get {return m_tailleEnOctets-m_espaceUtilisé;} }

        public long EspaceUtilise
        { get {return m_espaceUtilisé;} }

        public int NbPhotos
        { get {return m_colPhotos.Count;} }

        public string Nom
        { get { return m_nom; } }   

        public long TailleEnOctets
        { get { return m_tailleEnOctets; } }

        public void Ajouter(IPhoto pobjPhoto)
        {
            long escpaeR=m_tailleEnOctets-m_espaceUtilisé;
            if(escpaeR >=pobjPhoto.TailleEnOctets)
            {
                m_colPhotos.Add(pobjPhoto);
                m_espaceUtilisé=+pobjPhoto.TailleEnOctets;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void ChargerPhotos()
        {
            StreamReader objfischier = new StreamReader(m_nom + "txt");
            
            while (!objfischier.EndOfStream)
            {
                Photo x = new Photo(objfischier.ReadLine());

                m_colPhotos.Add(x);
                m_espaceUtilisé = +x.TailleEnOctets;
            }
            
          
            objfischier.Close();
        }

        public void EnregistrerPhotos()
        {
            
            StreamWriter FichierPhoto = new StreamWriter(m_nom+"txt");


            for (int index = 0; index < m_colPhotos.Count; index++)
            {

                FichierPhoto.WriteLine(m_colPhotos[index].ToString());

            }
            FichierPhoto.Close();
        }

        public bool PeutAjouter(IPhoto pobjPhoto)
        {
            if(pobjPhoto!=null) 
            {
                long escpaeR = m_tailleEnOctets - m_espaceUtilisé;
                if (escpaeR >= pobjPhoto.TailleEnOctets)
                {
                    return true;
                
                }
                else 
                { 
                return false;
                }
                    
                        
            }
            else { return false; }
        }

        public IPhoto PhotoAt(int pPosition)
        {
            if (pPosition>=0&&pPosition<m_colPhotos.Count)
            {
                IPhoto p = m_colPhotos[pPosition];
                return p;
            }
            else 
            {
                throw new ArgumentOutOfRangeException();
            
            }
        }

        public void SupprimerAt(int pPosition)
        {

            if (pPosition >= 0 && pPosition < m_colPhotos.Count)
            {
                IPhoto p = m_colPhotos[pPosition];
                m_colPhotos.RemoveAt(pPosition);
                m_espaceUtilisé-=p.TailleEnOctets;
            }
            else
            {
                throw new ArgumentOutOfRangeException();

            }
        }

        public void Vider()
        {
            m_colPhotos.Clear();
            m_espaceUtilisé = 0;
        }
    }
}
