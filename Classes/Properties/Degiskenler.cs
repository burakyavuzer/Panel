using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yeni_Panel_2015.Classes.Properties
{
    public class Degiskenler
    {
        #region Global

        private string _ID;
        private string _CATID;
        private string _LANGID;
        private string _LANGCODE;
        private string _PAGEURL;
        private string _ORDERNUM;
        private string _STATUS;
        private string _SELECTED;
        private string _LANGNAME;
        private string _NAME;
        private string _FILE;
        private string _URL;
        private string _PAGEID;
        private string _STORE;
        private string _SLIDER;
        private string _EXTERNAL;

        public string EXTERNAL
        {
            get { return _EXTERNAL; }
            set { _EXTERNAL = value; }
        }

        public string SLIDER
        {
            get { return _SLIDER; }
            set { _SLIDER = value; }
        }

        public string STORE
        {
            get { return _STORE; }
            set { _STORE = value; }
        }

        public string PAGEID
        {
            get { return _PAGEID; }
            set { _PAGEID = value; }
        }

        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }

        public string FILE
        {
            get { return _FILE; }
            set { _FILE = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }

        public string LANGNAME
        {
            get { return _LANGNAME; }
            set { _LANGNAME = value; }
        }

        public string SELECTED
        {
            get { return _SELECTED; }
            set { _SELECTED = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string ORDERNUM
        {
            get { return _ORDERNUM; }
            set { _ORDERNUM = value; }
        }

        public string PAGEURL
        {
            get { return _PAGEURL; }
            set { _PAGEURL = value; }
        }

        public string LANGCODE
        {
            get { return _LANGCODE; }
            set { _LANGCODE = value; }
        }

        public string LANGID
        {
            get { return _LANGID; }
            set { _LANGID = value; }
        }

        public string CATID
        {
            get { return _CATID; }
            set { _CATID = value; }
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        #endregion

        #region Sayfalar

        private string _PageName;

        public string PageName
        {
            get { return _PageName; }
            set { _PageName = value; }
        }

        private string _PageContent;

        public string PageContent
        {
            get { return _PageContent; }
            set { _PageContent = value; }
        }

        #endregion

        #region Ortamlar

        private string _MEDIAID;
        private string _MEDIANAME;

        public string MEDIANAME
        {
            get { return _MEDIANAME; }
            set { _MEDIANAME = value; }
        }

        public string MEDIAID
        {
            get { return _MEDIAID; }
            set { _MEDIAID = value; }
        }

        #endregion

        #region Meta Taglar

        private string _metaTitle;
        private string _metaKeywords;
        private string _metaDescription;

        public string MetaDescription
        {
            get { return _metaDescription; }
            set { _metaDescription = value; }
        }

        public string MetaKeywords
        {
            get { return _metaKeywords; }
            set { _metaKeywords = value; }
        }

        public string MetaTitle
        {
            get { return _metaTitle; }
            set { _metaTitle = value; }
        }

        #endregion
    }
}