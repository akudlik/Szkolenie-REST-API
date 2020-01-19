using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using REST.API.SeedWork;

namespace REST.API.Controllers.Model
{
    /// <summary>
    /// Class handle information about user
    /// </summary>
    public class User
    {
        /// <summary>
        /// User id
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        [MaxLength(100)]
        [MinLength(3)]
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        [MaxLength(100)]
        [MinLength(3)]
        public string LastName { get; set; }

        /// <summary>
        /// User date of birthday
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// User email address
        /// </summary>
        [EmailValidation]
        public string Email { get; set; }

        /// <summary>
        /// User login to app
        /// </summary>
        [MaxLength(100)]
        [MinLength(3)]
        public string Login { get; set; }
        
        /// <summary>
        /// User login to app
        /// </summary>
        [ReadOnly(true)]
        public DateTime Created { get; set; }

        public static IEnumerable<User> GetSampleUsers()
        {
            return new List<User>
            {
                new User
                {
                    UserId = 1,
                    Birthday = null,
                    Email = "test@test.pl",
                    Login = "login",
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Created= DateTime.Parse("2020-01-01")
                },
                new User
                {
                    UserId = 2,
                    Birthday = null,
                    Email = "test@test.pl",
                    Login = "login",
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Created= DateTime.Parse("2020-01-02")
                },
                new User
                {
                    UserId = 3,
                    Birthday = null,
                    Email = "test@test.pl",
                    Login = "login",
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Created= DateTime.Parse("2020-01-02")
                },
                new User
                {
                    UserId = 4,
                    Birthday = null,
                    Email = "test@test.pl",
                    Login = "login",
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Created= DateTime.Parse("2020-01-03")
                },
                new User
                {
                    UserId = 5,
                    Birthday = null,
                    Email = "test@test.pl",
                    Login = "login",
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Created= DateTime.Parse("2020-01-04")
                },
                new User
                {
                    UserId = 6,
                    Birthday = null,
                    Email = "test@test.pl",
                    Login = "login",
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Created= DateTime.Parse("2020-01-05")
                }
            };
        }
    }
}