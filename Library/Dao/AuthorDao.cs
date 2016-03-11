using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dao
{
    public class AuthorDao :EntityDao
    {
       

        internal  Author FindAuthorByName(string firstName, string lastName, string fullName)
        {
            Author author = new Author();
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                author = Context.Authors.Where(a => a.LastName == lastName && a.FirstName == firstName).FirstOrDefault();
            }
            else
            {
                author = Context.Authors.Where(a => a.FullName==fullName).FirstOrDefault();
            }
            return author;


        }
    }
}
