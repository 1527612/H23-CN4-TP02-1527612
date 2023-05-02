using AppCamera;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        public IPhoto PhotoCourante
        {

            get {
                if (m_posPhotoCourante == -1 || m_objCarteMemoireCourante ==null)
                { return null; }
                
                else
                {
                    return m_objCarteMemoireCourante.PhotoAt(m_posPhotoCourante);
                        
                }
            }

        }

        public enuDimension Dimension
        {
            get { return m_dimension; }

            set { m_dimension = value; } 
        }

        public int PosPhotoCourante 
        {
            get { return m_posPhotoCourante;}
        
        }

        public enuQualite Qualite 
        { 
            get {return m_qualite;}

            set {m_qualite = value;}
        
        }

        public long TailleEnOctetsEstimee 
        {
            get 
            {
                Size size=TailleEnPixelsSelonDimension;
                return (size.Height * size.Width * Photo.TAILLE_PIXEL) / FacteurCompression;
            }
        
        }

        public Size TailleEnPixelsSelonDimension
        {


            get
            {
                int[] larguer = new int[] { 4000, 3840, 3000, 1600, 640 };
                int[] hauteur = new int[] { 3000, 2160, 2000, 1200, 480 };
                return new Size(larguer[(int)m_dimension], hauteur[(int)m_dimension]);
            }
        }
        public enuSensibilite Sensibilite 
        {
            get { return m_sensibilite; } 
            set { m_sensibilite = value;} 
        }

        public void ChargerParametres()
        {
            StreamReader objfischier = new StreamReader(NOM_FICHIER_PARAMETRES);

            while (!objfischier.EndOfStream)
            {
                
                m_dimension=(enuDimension)Enum.Parse(typeof(enuDimension),objfischier.ReadLine());
                m_qualite = (enuQualite)Enum.Parse(typeof(enuQualite), objfischier.ReadLine());
                m_sensibilite= (enuSensibilite)Enum.Parse(typeof(enuSensibilite), objfischier.ReadLine());
                m_flash= (enuFlash)Enum.Parse(typeof(enuFlash), objfischier.ReadLine());
            }


            objfischier.Close();
        }

        public void DimensionSuivante()
        {   if((int)m_dimension == 4)
            { 
                m_dimension = (enuDimension)0; 
            }
            else
            { 
            
                m_dimension++;
            }
                    
        }

        public void EjecterCarteMemoire()
        {
            if (m_objCarteMemoireCourante == null)
            {
                throw new InvalidOperationException(ERR_MSG_CARTE_MANQUANTE);

            }
            else
            {

                m_objCarteMemoireCourante.EnregistrerPhotos();
                m_objCarteMemoireCourante = null;
                m_posPhotoCourante = -1;

            }
        }

        public void EnregistrerParametres()
        {
            StreamWriter obj = new StreamWriter(NOM_FICHIER_PARAMETRES);
            obj.WriteLine(m_dimension);
            obj.WriteLine(m_qualite);
            obj.WriteLine(m_sensibilite);
            obj.WriteLine(m_flash);
            obj.Close(); 
        }

        public void InsererCarteMemoire(ICarteMemoire pCarteMemoire)
        {
            if(pCarteMemoire==null)
            {
                throw new ArgumentNullException(ERR_MSG_CARTE_MANQUANTE);
            }
            if(m_objCarteMemoireCourante!=null)
            {
                throw new InvalidOperationException(ERR_MSG_CARTE_PRÉSENTE);
            }
            else
            {
                 m_objCarteMemoireCourante=pCarteMemoire ;
                m_posPhotoCourante =0;
            }
        }

        public bool PeutPrendreUnePhoto()
        {
            Photo photo = new Photo(TailleEnPixelsSelonDimension,FacteurCompression,Sensibilite,Flash);
            return m_objCarteMemoireCourante.PeutAjouter(photo);
            
        }

        public void PhotoPrecedente()
        {
            if(m_objCarteMemoireCourante!=null)
            {
                if (m_objCarteMemoireCourante.NbPhotos != 0)
                { 
                
                    if(m_posPhotoCourante!=0)
                    {
                    m_posPhotoCourante--;
                    }
                    else
                    {
                    m_posPhotoCourante = m_objCarteMemoireCourante.NbPhotos-1;
                    }
                
                
                }
                else
                {
                    throw new InvalidOperationException(ERR_MSG_CARTE_VIDE);

                }


            }

            else 
            {
                throw new InvalidOperationException(ERR_MSG_CARTE_MANQUANTE);
            
            }
        }

        public void PhotoSuivante()
        {
            if (m_objCarteMemoireCourante != null)
            {
                if (m_objCarteMemoireCourante.NbPhotos != 0)
                {

                    if (m_posPhotoCourante != m_objCarteMemoireCourante.NbPhotos - 1)
                    {
                        m_posPhotoCourante++;
                    }
                    else
                    {
                        m_posPhotoCourante = 0;
                    }


                }
                else
                {
                    throw new InvalidOperationException(ERR_MSG_CARTE_VIDE);

                }


            }

            else
            {
                throw new InvalidOperationException(ERR_MSG_CARTE_MANQUANTE);

            }
        }

        public void PrendrePhoto()
        {
            Photo photo = new Photo(TailleEnPixelsSelonDimension, FacteurCompression, Sensibilite, Flash);
           
            if (m_objCarteMemoireCourante != null)
            {

                if (PeutPrendreUnePhoto())
                {
                    m_objCarteMemoireCourante.Ajouter(photo);
                }


                else
                {
                    throw new InvalidOperationException(CarteMemoire.ERR_MSG_ESPACE_INSUFFISANT);

                }


            }

            else
            {
                throw new InvalidOperationException(ERR_MSG_CARTE_MANQUANTE);

            }
        }

        public void QualiteSuivante()
        {
            if ((int)m_qualite == 3)
            {
                m_qualite = (enuQualite)0;
            }
            else
            {

                m_qualite++;
            }
        }

        public void SensibiliteSuivante()
        {
            if ((int)m_sensibilite == 4)
            {
                m_sensibilite = (enuSensibilite)0;
            }
            else
            {

                m_sensibilite++;
            }
        }

        public void SupprimerPhotoCourante()
        {
            if (m_objCarteMemoireCourante != null)
            {

                if (m_objCarteMemoireCourante.NbPhotos!=0)
                {
                    m_objCarteMemoireCourante.SupprimerAt(PosPhotoCourante);
                    m_posPhotoCourante--;
                }


                else
                {
                    throw new InvalidOperationException(ERR_MSG_CARTE_VIDE);

                }


            }

            else
            {
                throw new InvalidOperationException(ERR_MSG_CARTE_MANQUANTE);

            }
        }

        public void ViderLaCarte()
        {
            if (m_objCarteMemoireCourante != null)
            {

                m_objCarteMemoireCourante.Vider();
                m_posPhotoCourante = 0;


            }

            else
            {
                throw new InvalidOperationException(ERR_MSG_CARTE_MANQUANTE);

            }
        }
    }
}
