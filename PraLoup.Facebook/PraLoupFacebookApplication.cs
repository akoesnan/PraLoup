using System;
using Facebook;

namespace PraLoup.Facebook
{
    public class PraLoupFacebookApplication : IFacebookApplication
    {
        // Summary:
        //     Gets the application id.
        public string AppId
        {
            get
            { return "119592781389488"; }
        }
        //
        // Summary:
        //     Gets the application secret.
        public string AppSecret { get { return "3405a077e027f82133b17fb96e0a5591"; } }
        //
        // Summary:
        //     Gets the url to return the user after they cancel authorization.
        public string CancelUrlPath { get { return "http://projectsafari.com"; } }
        //
        // Summary:
        //     Gets the canvas page.
        public string CanvasPage { get { return ""; } }
        //
        // Summary:
        //     Gets the canvas url.
        public string CanvasUrl { get { return ""; } }
        //
        // Summary:
        //     Gets the secure canvas url.
        public string SecureCanvasUrl { get { return ""; } }
        //
        // Summary:
        //     Gets the site url.
        public string SiteUrl { get { return "http://projectsafari.com"; } }
        //
        // Summary:
        //     Gets a value indicating whether it is beta.
        public bool UseFacebookBeta { get { return false; } }
    }
}
