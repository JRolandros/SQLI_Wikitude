using SQLI_CrossAR.CrossAR.DataAccess.DB.Tables;
using SQLI_CrossAR.CrossAR.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace SQLI_CrossAR.CrossAR.DataAccess.DB
{
    public class DBImpl
    {
        private static DBImpl _dbr;

        public static DBImpl GetDBImpl()
        {
            if (_dbr == null)
                return _dbr = new DBImpl();
            else return _dbr;
        }

        

        // OK //Code pour créer le chemin d'accès à la DB
        // Récupère les chemins spécifiques à chaque plateforme pour placer la BDD (bug windowsphone pour le moment)
        public string GetDBPath()
        {
            var sqliteFilename = "sqli_crossAR_DB.db2";

            var path = "KO";

            #if __IOS__
                string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
                path = Path.Combine(libraryPath, sqliteFilename);
            #else
            #if __ANDROID__
                string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
                path = Path.Combine(documentsPath, sqliteFilename);
                
            //Windows Foireux
            //#else WinPhone
                //path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename); 
            #endif
            #endif

            return path;
        }
        
        // OK //Code pour créer la BDD
        public bool CreateDBIfNotExist()
        {
            var output = "";                                                //Output console
            try
            {
                output += "Creating database if it doesnt exist.";
                string path = GetDBPath();                                  //Récupère le chemin de la BDD
                var db = new SQLiteConnection(path);                        //Connexion à SQLite
                output += " Database created..."; 
                Console.WriteLine(output);                                  //Affichage output
                return true;
            } catch (Exception ex)
            {
                output += ex.Message;
                return false;               
            }
        }
        
        // OK //Code pour créer la Table PI 
        public bool CreateTablePIIfNotExist()
        {
            try
            {
                string path = GetDBPath();                                 //Récupère le chemin de la BDD
                var db = new SQLiteConnection(path);                       //Connexion à SQLite
                db.CreateTable<PI_DB>();                                   //Création de la table PI_DB 
                return true;                                            //Résultat pour les test unitaires
            } catch(Exception ex)
            {
                Console.WriteLine("Error table creation PI: " + ex.Message);
                return false;               
            }
        }
        
        // OK //Code pour insérer un PI dans la Table
        public bool  InsertPI (PointInteret pi)
        {
            try
            {
                string path = GetDBPath();                                 //Récupère le chemin de la BDD
                var db = new SQLiteConnection(path);                        //Connexion à SQLitestring dbPath = DB_GetPath();

                PI_DB item = new PI_DB                                      //Creation d'une nouvelle ligne
                {
                    Nom         = pi.Nom,                                   //Assignation des attributs de la ligne avec les attributs du pi
                    Categorie   = pi.Categorie,
                    Adresse     = pi.Adresse,
                    Longitude   = pi.Longitude,
                    Latitude    = pi.Latitude,
                    ImageUrl    = pi.ImageSource
                };

                db.Insert(item);                                            //Insertion à la table
                db.Commit();                                                //Commit BDD
                Console.WriteLine("PI ajouté à la table");
                return true;                                              //Résultat pour les test unitaires
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // OK //Code pour récupérer tous les points de la Table
        // Fonction pour convertir la table PI_DB en List<> de PI_Model
        public List<PointInteret> GetPITable()
        {
            List<PointInteret> result = null;
            string output = "";                                             //Output console
            try
            {
                string path = GetDBPath();                                 //Récupère le chemin de la BDD
                var db = new SQLiteConnection(path);                        //Connexion à SQLitestring dbPath = DB_GetPath();

                output += "Retrieving all the  data using ORM..";

                var table = db.Table<PI_DB>();                              //Récupération de la table
                foreach (var item in table)                                 //Parcours de la table
                {
                    PointInteret pi = new PointInteret                              //Assignation des attributs dans un PI_Model
                    {
                        Nom = item.Nom,
                        Adresse = item.Adresse,
                        Categorie = item.Categorie,
                        Longitude = item.Longitude,
                        Latitude = item.Latitude,
                        Favoris = true,
                        ImageSource = item.ImageUrl
                    };
                    result.Add(pi);                                         //Insertion du PI_Model à la List<> 
                }
            }
            catch (Exception ex)
            {
                output += ex.Message;
            }
            Console.WriteLine(output);
            return result;
        }

        /*
        // OK //Code pour lire les PI de la Table (Console)
        public string ReadPITable()
        {
            string result = "KO";                                           //Résultat pour les test unitaires
            string output = "";                                             //Output console

            try
            {
                string path = DB_GetPath();                                 //Récupère le chemin de la BDD
                var db = new SQLiteConnection(path);                        //Connexion à SQLitestring dbPath = DB_GetPath();

                output += "Retrieving the data using ORM..";

                var table = db.Table<PI_DB>();                              //On récupère la table PI_DB
                foreach (var item in table)                                  //On parcours la table
                {
                    output += "\nItem : "+item.Id+", Nom : " + item.Nom;    //On ajoute la ligne à l'output console
                }                
                result = "OK";                                              //Résultat pour les test unitaires
            } catch (Exception ex)
            {
                output += ex.Message;                                       //On ajoute le message d'erreur à l'ouput
            }
            Console.WriteLine(output);                                      //On affiche l'ouput
            return result;                                                  //Résultat pour les test unitaires
        }

        

        // OK //Code pour récupérer un PI avec son Id
        // Fonction pour convertir une ligne avec l'id en PI_Model
        //public PI_Model DB_Get_PiById(int id)
        //{
        //    PI_Model result = null;
        //    try
        //    {
        //        string dbPath = DB_GetPath();
        //        var db = new SQLiteConnection(dbPath);
        //        var item = db.Get<PI_DB>(id);

        //        result.Nom          = item.Nom;
        //        result.Categorie    = item.Categorie;
        //        result.Adresse      = item.Adresse;
        //        result.Longitude    = item.Longitude;
        //        result.Latitude     = item.Latitude;
        //        result.Favoris      = item.Favoris;       
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return result;
        //}

        // OK //Code pour mettre à jour un PI
        //public string DB_Update_PiById(int id, PI_Model pi)
        //{
        //    string result = "KO";            
        //    try
        //    {                
        //        string dbPath = DB_GetPath();
        //        var db = new SQLiteConnection(dbPath);
        //        var item = db.Get<PI_DB>(id);
        //        pi.Nom = item.Nom;
        //        pi.Categorie = item.Categorie;
        //        pi.Adresse = item.Adresse;
        //        pi.Longitude = item.Longitude;
        //        pi.Latitude = item.Latitude;
        //        pi.Favoris = item.Favoris;
        //        result = "OK";                
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return result;
        //}

        // OK //Code pour supprimer un point de la Table

        //public string DB_Remove_PI(int id)
        //{
        //    string result = "KO";
        //    try
        //    {
        //        string dbPath = DB_GetPath();
        //        var db = new SQLiteConnection(dbPath);
        //        var item = db.Get<PI_DB>(id);
        //        db.Delete(item);
        //        result = "OK";
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return result;
        //}

        //// OK //Code pour supprimer tous les points de la Table
        //public string DB_Clear_PI_Table()
        //{
        //    string result = "KO";
        //    try
        //    {
        //        string dbPath = DB_GetPath(); 
        //        var db = new SQLiteConnection(dbPath);

        //        foreach (var item in db.Table<PI_DB>())
        //        {
        //           db.Delete(item);
        //        }              
        //        result = "OK";
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return result;
        //}

        // OK //Code pour supprimer la table de PI

        public string DB_Drop_PI_Table()
        {
            string result = "KO";
            
            try
            {
                string dbPath = DB_GetPath();
                var db = new SQLiteConnection(dbPath);
                string sql = "DROP TABLE PI_DB";
                SQLiteCommand cmd = new SQLiteCommand(db);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                db.Commit();
                db.Close();
                result = "OK";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        // OK //Code pour insérer tous les PI Samples dans la BDD
        public string DB_PopulateWithSample_PI_Table()
        {
            string result = "KO";
            var output = "";
            List<PIListItem> list = sampleList();
            try
            {
                string dbPath = DB_GetPath();
                var db = new SQLiteConnection(dbPath);

                var table = db.Table<PI_DB>();
                if (table != null)
                {
                    foreach (var pi in list)
                    {
                        DB_Insert_PI(pi);
                    }
                                 
                } else
                {
                    output += "\n_ _ _ _ _Table deja remplie";
                }
                result = "OK";
            }
            catch (Exception ex)
            {
                output += ex.Message;                
            }
            Console.WriteLine(output);
            return result;
        }

        */


    }
}
