using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.XML
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = true)]
    public partial class rows
    {

        private rowsRow[] rowField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("row")]
        public rowsRow[] row
        {
            get
            {
                return this.rowField;
            }
            set
            {
                this.rowField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class rowsRow
    {

        private string autorField;

        private string tytulField;

        private string rokField;

        private string wydawnictwoField;

        /// <remarks/>
        public string Autor
        {
            get
            {
                return this.autorField;
            }
            set
            {
                this.autorField = value;
            }
        }

        /// <remarks/>
        public string Tytul
        {
            get
            {
                return this.tytulField;
            }
            set
            {
                this.tytulField = value;
            }
        }

        /// <remarks/>
        public string Rok
        {
            get
            {
                return this.rokField;
            }
            set
            {
                this.rokField = value;
            }
        }

        /// <remarks/>
        public string Wydawnictwo
        {
            get
            {
                return this.wydawnictwoField;
            }
            set
            {
                this.wydawnictwoField = value;
            }
        }
    }




}
