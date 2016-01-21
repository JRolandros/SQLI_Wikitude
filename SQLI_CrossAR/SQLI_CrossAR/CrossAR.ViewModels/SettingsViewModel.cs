using SQLI_CrossAR.CrossAR.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLI_CrossAR.CrossAR.ViewModels
{
    public class SettingsViewModel : RootViewModel
    {
        
        public int Rayon { get; set; }
        public SettingsViewModel()
        {
            //stgModel.Categories.Add("food");
            //stgModel.Categories.Add("restaurant");
            //stgModel.Categories.Add("pharmacy");
            //stgModel.RayonRecherche = 1000;
            //stgModel.CategorieRecherche = "food";

        }

    }
}
