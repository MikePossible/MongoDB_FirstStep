namespace FirstStep.Models
{
    #region Using

    using InstaSharp;
    using InstaSharp.Models.Responses;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Web;

    #endregion


    public class Instagram
    {
        #region Constructor
        
        public Instagram()
        {
            
        }// Instagram(...)

        #endregion

        #region Properties        

        private readonly string _clientID = ConfigurationManager.AppSettings["ClientID"] as string ?? "";
        private readonly string  _clientSecret = ConfigurationManager.AppSettings["ClientSecret"] as string ?? "";
        private readonly string _redirectUri = ConfigurationManager.AppSettings["CallBackUrl"] as string ?? "";

        private readonly string _apiUrl = "https://api.instagram.com/v1";
        private readonly string _oauthUri = "https://api.instagram.com/oauth";
        private readonly string _callbackUri = ConfigurationManager.AppSettings["CallBackUrl"] as string ?? "";
        private readonly string _accesstokenUri = "https://api.instagram.com/oauth/access_token";
        private readonly string _grantAccess = "authorization_code";

        public InstagramConfig Config
        {
            get
            {

                var config = HttpContext.Current.Session["InstagramConfig"] as InstagramConfig;
                if (config == null)
                {
                    config = new InstagramConfig(this._clientID,
                        this._clientSecret,
                        this._redirectUri,
                        this._callbackUri,
                        this._apiUrl,
                        this._oauthUri,
                        "");
                    
                }// if

                return config;
            }
        }// Config

        //Instagram Info stored in the session
        public OAuthResponse SessionOAuth
        {
            get
            {
                return HttpContext.Current.Session["SessionOAuth"] as OAuthResponse ?? null;
            }// get

            set
            {
                HttpContext.Current.Session["SessionOAuth"] = value;
            }// set
        }// SessionOAuth

        #endregion

        #region Methods       
        
        //Authentication
        public string Authenticate()
        {
            //Create the auth url
            var scopes = new List<OAuth.Scope>();
            scopes.Add(OAuth.Scope.Likes);
            scopes.Add(OAuth.Scope.Comments);

            //var link = OAuth.AuthLink(this.Config.OAuthUri + "/authorize",
            return OAuth.AuthLink(this.Config.OAuthUri + "/authorize",
                this.Config.ClientId,
                this.Config.RedirectUri,
                scopes,
                OAuth.ResponseType.Code);

        }// Authenticate(...)

        //Sets Token and User info
        public void Authorize(string code)
        {
            try
            {
                //Populate the parameters to send
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("client_id", this.Config.ClientId);
                parameters.Add("client_secret", this.Config.ClientSecret);
                parameters.Add("grant_type", _grantAccess);
                parameters.Add("redirect_uri", this.Config.RedirectUri);
                parameters.Add("code", code);

                //WebRequest sent to Instagram
                WebClient client = new WebClient();
                var result = client.UploadValues(_accesstokenUri, parameters);

                //Encoding the result returned
                var response = System.Text.Encoding.Default.GetString(result);

                //Set Session Object
                this.SessionOAuth = JsonConvert.DeserializeObject<OAuthResponse>(response);
            }
            catch (Exception ex)
            {
                //Log error
            }// try..catch

        }// GetToken(...)

        #endregion

    }// class


}// namespace