using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAS.Model.Model.Product;
using DAS.Model.Model.User;

namespace DAS.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var produductCategory = new ProductCategoryEntity();
            var user = new UserEntity();

            System.Console.WriteLine(user.UserRole);
            System.Console.WriteLine(produductCategory.Id);
            System.Console.WriteLine(produductCategory.Name);
            System.Console.WriteLine(produductCategory.Description);
            System.Console.WriteLine(produductCategory.CreatedAt);
            System.Console.WriteLine(produductCategory.DeletedAt);
            System.Console.WriteLine(produductCategory.ModifiedAt);

            System.Console.ReadKey();

        }
    }
}
