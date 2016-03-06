using Library.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.XML;
using System.Xml.Serialization;
using System.IO;
using Library.Model;
using System.Windows;

namespace Library.ImportDb
{
    public class ImportClass
    {
        private static LibraryContext db = new LibraryContext();
        private const string testPath = @"D:\Documents\visual studio 2015\Projects\Library\Library\XML\";
        public static void ImportDoWork()
        {
            foreach (var file in Directory.GetFiles(testPath, "*.xml"))
            {
                rows RowsXml = DeserializeXml(file);
                foreach (var row in RowsXml.row)
                {
                    InsertToDb(row,file);
                }
            }
        }

        private static void InsertToDb(rowsRow row,string file)
        {
            var groupName = int.Parse(Path.GetFileNameWithoutExtension(file));
            Author author = null;
            Book book;
            Box box = db.Box.Where(b => b.Number.Equals(groupName)).FirstOrDefault();
            if (box == null)
            {
                box = new Box() { Number = groupName };
                db.Box.Add(box);
            }

            if (!string.IsNullOrEmpty(row.Autor))
            {
                if (row.Autor.Contains(","))
                {
                    
                    var AuthorName = row.Autor.Replace(" ", "").Split(',');
                    var firstname = AuthorName[1];
                    var lastname = AuthorName[0];
                    author = db.Authors.Where(a => a.LastName == lastname && a.FirstName == firstname).FirstOrDefault();
                    if (author == null)
                    {
                        author = new Author()
                        {
                            LastName = AuthorName[0],
                            FirstName = AuthorName[1],
                            FullName = string.Concat(AuthorName[1], " ", AuthorName[0])
                        };
                        db.Authors.Add(author);
                    }

                }
                else
                {
                    author = db.Authors.Where(a => a.FullName == row.Autor).FirstOrDefault();
                    if (author == null)
                    {
                        author = new Author() { FullName = row.Autor };
                        db.Authors.Add(author);
                    }
                }
            }
            if (author != null)
            {
                book = new Book(row.Tytul, row.Wydawnictwo, ToNullableInt32(row.Rok), author.AuthorId, box.BoxId);
            }
            else
            {
                book = new Book(row.Tytul, row.Wydawnictwo, ToNullableInt32(row.Rok), null, box.BoxId);
            }

            db.Books.Add(book);
            db.SaveChanges();
            //int? fajnytest = author != null ? author.AuthorId : default;
        }

        private static rows DeserializeXml(string path)
        {
            try
            {
                rows rows = null;

                XmlSerializer serializer = new XmlSerializer(typeof(rows));
                StreamReader reader = new StreamReader(path);
               // reader.ReadToEnd();
                rows = (rows)serializer.Deserialize(reader);
                reader.Close();
                return rows;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
                return null;
            }
        }
        public static int? ToNullableInt32( string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }
    }
}
