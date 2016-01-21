using Newtonsoft.Json;

using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

using SQLI_CrossAR.CrossAR.Models;
using SQLI_CrossAR.CrossAR.DataAccess.DAO;

namespace SQLI_CrossAR.CrossAR.Services
{
    public class GoogleWebService
    {
        DAOPointInteret _dao;
        public GoogleWebService()
        {
            _dao = DAOPointInteret.GetDAOImplInstance();
        }

        public async Task<List<Place>> GetPlacesForCoordinates(Double latitude, Double longitude)
        {
            App.CurrentQuery = _dao.formatQuery("food", latitude, longitude, 1000);            
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync(App.CurrentQuery);
                    var resp= JsonConvert.DeserializeObject<NearbyQuery>(response);
                    return resp.Places;
                }
                catch (Exception ex)
                {
                    Console.WriteLine( "Http client error :"+ex.StackTrace);
                    return new List<Place>();
                }
            }            
        }
    }
}