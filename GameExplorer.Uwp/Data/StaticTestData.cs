using GameExplorer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using GameExplorer.Uwp.Utils;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace GameExplorer.Uwp.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class StaticTestData
    {
        /// <summary>
        /// The admin
        /// </summary>
        public static User Admin = new User
        {
            Username = "Admin",
            FirstName = "Thomas",
            LastName = "Angeland",
            Photo = ImageUtils.FileToImageMinimal("/Images/AdminPhoto.jpg"),
            Country = "Norway",
            Gender = "Male",
            Experience = 8000,
            Bio = "Hello. My name is Thomas and I am the creator of this App.",
            Birthday = new DateTime(1994, 1, 16),
            Email = "thomas.angeland@gmail.com",
            SignedIn = DateTime.Now,
            SignedUp = DateTime.Now,
            Uid = Guid.NewGuid(),
            Website = "https://r3dcraft.net"
        };

        /// <summary>
        /// Gets the games.
        /// </summary>
        /// <value>
        /// The games.
        /// </value>
        public static List<Game> Games
        {
            get
            {
                Debug.WriteLine("Creating games... ");
                var games = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(
                    Windows.ApplicationModel.Package.Current.InstalledLocation.Path + @"/DataSource/gamesdata.json"));
                Debug.WriteLine("Completed!");
                return games;
            }
        }
    }
}