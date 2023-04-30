using AppCamera;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace TestAppCamera
{
    internal class Camera : ICamera
    {
        private enuDimension m_dimension;
        private enuFlash m_flash;
        private ICarteMemoire m_objCarteMemoireCourante;
        private int m_posPhotoCourante;
        private enuQualite m_qualite;
        private enuSensibilite m_sensibilite;
        public const string ERR_MSG_CARTE_MANQUANTE = "Carte manquante";
        public const string ERR_MSG_CARTE_PRÉSENTE = "Carte déjà présente";
        public const string ERR_MSG_CARTE_VIDE = "Carte vide";
        private const string NOM_FICHIER_PARAMETRES = "parametres.txt";

        public Camera()
        {
            m_posPhotoCourante = -1;
            m_dimension = enuDimension.Maximale;
            m_qualite = enuQualite.Excellente;
            m_sensibilite = enuSensibilite.ISO_64;
            m_flash = enuFlash.Activé;
            m_objCarteMemoireCourante = null;

        }
        public ICarteMemoire CarteMemoireCourante
        {
            get { return m_objCarteMemoireCourante; }
        }

        

        public int FacteurCompression 
        {
            get
            {
                int[] i = new int[] { 4, 12, 20, 50 };

                return i[(int)m_qualite]; 
                
            }
        }

        public enuFlash Flash 
        { 
            get { return m_flash; }

            set { m_flash = value; }
        }

        public IPhoto PhotoCourante => throw new NotImplementedException();

        public enuDimension Dimension
        {
            get { return m_dimension; }

            set { m_dimension = value; } 
        }

        public int PosPhotoCourante => throw new NotImplementedException();

        public enuQualite Qualite 
        { 
            get {return m_qualite;}

            set {m_qualite = value;}
        
        }

        public long TailleEnOctetsEstimee => throw new NotImplementedException();

        public Size TailleEnPixelsSelonDimension => throw new NotImplementedException();

        public enuSensibilite Sensibilite 
        {
            get { return m_sensibilite; } 
            set { m_sensibilite = value;} 
        }

        public void ChargerParametres()
        {
            throw new NotImplementedException();
        }

        public void DimensionSuivante()
        {
            throw new NotImplementedException();
        }

        public void EjecterCarteMemoire()
        {
            throw new NotImplementedException();
        }

        public void EnregistrerParametres()
        {
            throw new NotImplementedException();
        }

        public void InsererCarteMemoire(ICarteMemoire pCarteMemoire)
        {
            throw new NotImplementedException();
        }

        public bool PeutPrendreUnePhoto()
        {
            throw new NotImplementedException();
        }

        public void PhotoPrecedente()
        {
            throw new NotImplementedException();
        }

        public void PhotoSuivante()
        {
            throw new NotImplementedException();
        }

        public void PrendrePhoto()
        {
            throw new NotImplementedException();
        }

        public void QualiteSuivante()
        {
            throw new NotImplementedException();
        }

        public void SensibiliteSuivante()
        {
            throw new NotImplementedException();
        }

        public void SupprimerPhotoCourante()
        {
            throw new NotImplementedException();
        }

        public void ViderLaCarte()
        {
            throw new NotImplementedException();
        }
    }
}
