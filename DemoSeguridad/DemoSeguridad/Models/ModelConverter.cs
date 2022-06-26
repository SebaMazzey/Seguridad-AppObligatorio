using DemoSeguridad.Data.Entities;
using System;
using System.Collections.Generic;

namespace DemoSeguridad.Models
{
    public class ModelConverter
    {

        public static UserViewModel GetUserViewModel(User user)
        {
            return new UserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = GetRoleViewModel(user.Role)
            };
        }

        public static RoleViewModel GetRoleViewModel(Role role)
        {
            RoleViewModel model = new()
            {
                Name = role.Name,
                Description = role.Description,
            };
            foreach (var rolePermission in role.RolePermissions)
            {
                model.Permissions.Add(GetPermissionViewModel(rolePermission.Permission));
            }

            return model;
        }

        public static PermissionViewModel GetPermissionViewModel(Permission permission)
        {
            return new PermissionViewModel {
                Name = permission.Name,
                Description = permission.Description
            };
        }

        public static BookListViewModel GetBookListViewModel(ICollection<Book> books)
        {
            BookListViewModel model = new();
            foreach (var book in books)
            {
                var bookModel = GetBookViewModel(book);
                model.Books.Add(bookModel);
            }
            return model;
        }

        public static BookViewModel GetBookViewModel(Book book)
        {
            return new BookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Content = book.Content,
                AuthorName = book.Author.FirstName + " " + book.Author.LastName
            };
        }
    }
}
