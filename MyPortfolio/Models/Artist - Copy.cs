using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Configuration;
using Newtonsoft.Json.Serialization;
using System.Xml;
using Newtonsoft.Json;

namespace MyPortfolio.Models
{
    public class Artist
    {
        /// <summary>
        /// Wrapper for GetSimilarArtist method. This returns json from a datatable 
        /// </summary>
        /// <param name="artistname">Name of the artist to search</param>
        /// <returns>Returns XML from last.fm API web request.</returns>
        public string ArtistSearch(string artistName)
        {
            if (String.IsNullOrEmpty(artistName))
            {
                return null;
            }
            else
            {
                var ArtistListJson = GetSimilarArtists(artistName);
                return ArtistListJson;
                //XmlDocument Doc = new XmlDocument();
                //Doc.LoadXml(ArtistList);
                //string jsonText = JsonConvert.SerializeXmlNode(Doc);

                //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                //Dictionary<string, object> row;
               
                //foreach (DataRow dr in Artist.Rows)
                //{
                //    row = new Dictionary<string, object>();
                //    foreach (DataColumn col in Artist.Columns)
                //    {
                //        row.Add(col.ColumnName, dr[col]);
                //    }
                //    rows.Add(row);
                //}
              //  return ArtistListJson;
            }
        }
        /// <summary>
        /// Makes an API call to last.fm 
        /// </summary>
        /// <param name="id">Name of the artist to search</param>
        /// <returns>Returns XML from last.fm API web request.</returns>
        private string GetServiceResponse(string requestUrl)
        {
            string httpResponse = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Timeout = 15000;
            HttpWebResponse response = null;
            StreamReader reader = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                httpResponse = reader.ReadToEnd();
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null) //TODO: Remove this 
                {
                    response.Close();
                }
            }
            return httpResponse;
        }
        /// <summary>
        /// Return artists similar to the one passed.
        /// </summary>
        /// <param name="artistname">The name of the artist to use for the request.</param>
        /// <returns>DataTable with artist names</returns>
        private string GetSimilarArtists(string artistName)
        {
            string requestUrl = GetBaseRequestUrl();
            requestUrl += "&format=json&autocorrect=1&method=artist.getSimilar&artist=" + System.Web.HttpUtility.UrlEncode(artistName.Trim());

            string serviceResponse = GetServiceResponse(requestUrl);

            //var xmlResponse = XElement.Parse(serviceResponse);

            //var artists = from artistsSimilar in xmlResponse.Descendants("artist")
            //              select new
            //              {
            //                  name = artistsSimilar.Element("name").Value,
            //                  page = "http://" + artistsSimilar.Element("url").Value,
            //                  image = ResponseFormatter(artistsSimilar.Element("image").NextNode.NextNode.ToString())
            //              };

            //DataTable similarArtists = new DataTable();
            //similarArtists.Columns.Add("name");
            //similarArtists.Columns.Add("page");
            //similarArtists.Columns.Add("image");

            //if (artists.Count() > 0)
            //{
            //    DataRow artistsRow;
            //    foreach (var artist in artists)
            //    {
            //        artistsRow = similarArtists.NewRow();
            //        artistsRow["name"] = artist.name;
            //        artistsRow["page"] = artist.page;
            //        artistsRow["image"] = artist.image;
            //        similarArtists.Rows.Add(artistsRow);
            //    }
            //}
            return serviceResponse;
        }
        /// <summary>
        /// Builds the URL needed for last.fm api. Gets the value from web.config
        /// </summary>
        /// <param name="">None</param>
        /// <returns>String with the last.fm url and API Key</returns>
        /// 
        private string GetBaseRequestUrl()
        {
            string baseUrl = ConfigurationManager.AppSettings["LastFmURLandApiKey"];
            return baseUrl;
        }
        /// <summary>
        /// Strips HTML tags from the xml node
        /// </summary>
        /// <param name="htmlstring">The XML node data</param>
        /// <returns>Returns a string with no html</returns>
        private static string ResponseFormatter(string htmlString)
        {
            char[] array = new char[htmlString.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < htmlString.Length; i++)
            {
                char let = htmlString[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}