
using SQLI_CrossAR.CrossAR.DataAccess.DB;
using SQLI_CrossAR.CrossAR.Models;
using SQLI_CrossAR.CrossAR.Services;
using SQLI_CrossAR.CrossAR.ViewModels;
using SQLI_CrossAR.Utils;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SQLI_CrossAR.CrossAR.DataAccess.DAO
{
    public class DAOPointInteret : RootViewModel
    {
        private static DAOPointInteret _daoPI;
        DBImpl _dbi;

        public DAOPointInteret()
        {
            _dbi = DBImpl.GetDBImpl();
        }
        //Fonction à tester
        //Fonction permettant d'avoir une instance unique de l'objet
        public static DAOPointInteret GetDAOImplInstance()
        {
            if (_daoPI == null) //Si l'objet est nul on en crée un nouveau
                return _daoPI = new DAOPointInteret();
            else return _daoPI; //Sinon on retourne l'instance déjà créée
        }

        //Fonction à tester
        public string formatQuery (string categorie, Double latitude, Double longitude, int radius)
        {
            string _latitude = latitude.ToString().Replace(",", "."); //Transformer les virgules en point      
            string _longitude = longitude.ToString().Replace(",", "."); //Transformer les virgules en point    
            string _cat = categorie; 
            string _loc = _latitude + "," + _longitude;
            string _rad = radius.ToString();
            string url = String.Format(Constantes.PlacesQueryUrl, _cat, _loc ,_rad, Constantes.ApiKeyServer); //Formate la requete avec les paramètres
            return url;
        }

        public bool InitDB()
        {
            bool result = false;
            bool resultCreateDB = false;
            bool resultCreateTable = false;
            resultCreateDB = _dbi.CreateDBIfNotExist(); //Création de la BDD si elle n'existe pas
            if (resultCreateDB) 
                resultCreateTable = _dbi.CreateTablePIIfNotExist(); //Si la BDD est créé on créé la table si elle n'existe pas
            if (resultCreateTable)
                result = true; //Si la table est créée la BD est correctement initialisée
            return result;
        }

        public bool InsertTablePI(PointInteret pi)
        {
            return _dbi.InsertPI(pi);
        }

        public List<PointInteret> GetTablePI()
        {
            return _daoPI.GetTablePI();
        }
    }


}
