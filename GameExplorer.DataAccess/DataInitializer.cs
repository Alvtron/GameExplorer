using System;
using System.Collections.ObjectModel;
using GameExplorer.Model;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;

namespace GameExplorer.DataAccess
{
    /// <summary>
    /// Seeds the database in case the model has changed.
    /// </summary>
    /// <seealso cref="System.Data.Entity.CreateDatabaseIfNotExists{GameExplorer.DataAccess.DataContext}" />
    /// <inheritdoc />
    /// <seealso cref="!:System.Data.Entity.DropCreateDatabaseIfModelChanges{QC.DataAccess.DataContext}" />
    public class DataInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        /// <summary>
        /// Seeds the database with specific set data.
        /// </summary>
        /// <param name="context">The context to seed.</param>
        /// <inheritdoc />
        protected override void Seed(DataContext context)
        {

            var kim = new User("kimdrello", "kim@hiof.no", "#Kimdrello23") { FirstName = "Kim-Andre", LastName = "Engebretsen" };
            var vegard = new User("vegster", "vegard@hiof.no", "#Vegard22") { FirstName = "Vegard", LastName = "Strand" };
            var christoffer = new User("rexzore", "christoffer@hiof.no", "#Christoffer89") { FirstName = "Christoffer", LastName = "Øyane" };
            var thomas = new User("alvtron", "thomas@hiof.no", "#Azeroth7") { FirstName = "Thomas", LastName = "Angeland" };

            var admin = new User ("Admin", "thomas.angeland@gmail.com", "#Admin7")
            {
                FirstName = "Admin",
                LastName = "Admin",
                Country = "Norway",
                Gender = "Male",
                Experience = 8000,
                Photo = FileToImageMinimal(@"C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Images\AdminPhoto.jpg"),
                Friends = new ObservableCollection<User> { kim, vegard, thomas, christoffer },
                Bio = "Hello. My name is Thomas and I am the creator of this App.",
                Birthday = new DateTime(1994, 1, 16),
                Website = "https://r3dcraft.net"
            };

            thomas.AddFriend(kim);
            thomas.AddFriend(christoffer);
            thomas.AddFriend(vegard);
            kim.AddFriend(christoffer);
            kim.AddFriend(vegard);
            christoffer.AddFriend(vegard);

            context.Users.Add(kim);
            context.Users.Add(vegard);
            context.Users.Add(christoffer);
            context.Users.Add(thomas);
            context.Users.Add(admin);

            var quest1 = new Quest(admin);

            quest1.Uid = new Guid("6e0ab048-feb1-4c00-8b2c-59488197b939");
            quest1.Title = "Spiderslaying is fun!";
            quest1.About = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}{\\f1\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23 The Elder Scrolls V: Skyrim is an open world action role-playing video game developed by Bethesda Game Studios and published by Bethesda Softworks. It is the fifth main installment in The Elder Scrolls series, following The Elder Scrolls IV: Oblivion, and was released worldwide for Microsoft Windows, PlayStation 3 and Xbox 360 on November 11, 2011.\\par\r\n\\par\r\nThe game's main story revolves around the player character and their quest to defeat Alduin the World-Eater, a dragon who is prophesied to destroy the world. The game is set two hundred years after the events of Oblivion, and takes place in the fictional province of Skyrim. Over the course of the game, the player completes quests and develops the character by improving skills. The game continues the open world tradition of its predecessors by allowing the player to travel anywhere in the game world at any time, and to ignore or postpone the main storyline indefinitely.\\par\r\n\\par\r\nSkyrim was developed using the Creation Engine, rebuilt specifically for the game. The team opted for a unique and more diverse game world than Oblivion's Imperial Province of Cyrodiil, which game director and executive producer Todd Howard considered less interesting by comparison. The game was released to critical acclaim, with reviewers particularly mentioning the character development and setting, and is considered to be one of the greatest video games of all time. However, it nonetheless received some criticism, particularly for its numerous technical issues present at launch. The game shipped over seven million copies to retailers within the first week of its release, and sold over 30 million copies across all platforms.\\par\r\n\\par\r\nThree downloadable content (DLC) add-ons were released\\emdash Dawnguard, Hearthfire and Dragonborn\\emdash which were repackaged into The Elder Scrolls V: Skyrim \\endash  Legendary Edition, which was released in June 2013. The Elder Scrolls V: Skyrim \\endash  Special Edition, a remastered version of the game, was released for Windows, Xbox One and PlayStation 4 in October 2016, which includes all three DLC expansions and a graphical upgrade, along with additional features such as modding capabilities on consoles. Versions for the Nintendo Switch and PlayStation VR were released in November 2017.\\par\r\n\\par\r\n\\f1\\lang1044 (https://en.wikipedia.org/wiki/The_Elder_Scrolls_V:_Skyrim, 25.04.2018)\\f0\\lang1033\r\n}\r\n\0";
            quest1.Game = new Game();

            quest1.SetPhoto(admin, FileToImageMinimal(@"C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Images\image (5).jpg"));

            quest1.Steps.Add(new Step { About = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Go to Deepshroom Cave in Jade forest.\r\n}\r\n\0" });
            quest1.Steps.Add(new Step { About = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Kill 12 \\b Spiders\\b0 .\r\n}\r\n\0" });
            quest1.Steps.Add(new Step { About = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Pick up 24 \\b Webslik\\b0 .\r\n}\r\n\0" });

            quest1.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Haha, this is great! Thanks!\r\n}\r\n\0"));
            quest1.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 You forgot to mention that the spider boss can't be killed unless you drink a potion that \\b increases your damage-output by at least 50\\b0 .\r\n}\r\n\0"));
            quest1.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Thanks! Finally I'll complete this. been stuck for hours!\r\n}\r\n\0"));
            quest1.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Might wan't to update the info. There's so much \\b lore \\b0 about this.\r\n}\r\n\0"));

            quest1.AddScreenshot(admin, FileToImageMinimal(@"C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Images\image (1).jpg"));
            quest1.AddScreenshot(admin, FileToImageMinimal(@"C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Images\image (2).jpg"));
            quest1.AddScreenshot(admin, FileToImageMinimal(@"C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Images\image (3).jpg"));
            quest1.AddScreenshot(admin, FileToImageMinimal(@"C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Images\image (4).jpg"));

            quest1.Rewards.Add(new Reward("Gold", 500));

            var quest2 = new Quest(admin);

            quest2.Uid = new Guid("603025fa-5917-4b2e-a69f-1d3ec0449174");
            quest2.Title = "The Monster in the Sky";
            quest2.About = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}{\\f1\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23 The Elder Scrolls V: Skyrim is an open world action role-playing video game developed by Bethesda Game Studios and published by Bethesda Softworks. It is the fifth main installment in The Elder Scrolls series, following The Elder Scrolls IV: Oblivion, and was released worldwide for Microsoft Windows, PlayStation 3 and Xbox 360 on November 11, 2011.\\par\r\n\\par\r\nThe game's main story revolves around the player character and their quest to defeat Alduin the World-Eater, a dragon who is prophesied to destroy the world. The game is set two hundred years after the events of Oblivion, and takes place in the fictional province of Skyrim. Over the course of the game, the player completes quests and develops the character by improving skills. The game continues the open world tradition of its predecessors by allowing the player to travel anywhere in the game world at any time, and to ignore or postpone the main storyline indefinitely.\\par\r\n\\par\r\nSkyrim was developed using the Creation Engine, rebuilt specifically for the game. The team opted for a unique and more diverse game world than Oblivion's Imperial Province of Cyrodiil, which game director and executive producer Todd Howard considered less interesting by comparison. The game was released to critical acclaim, with reviewers particularly mentioning the character development and setting, and is considered to be one of the greatest video games of all time. However, it nonetheless received some criticism, particularly for its numerous technical issues present at launch. The game shipped over seven million copies to retailers within the first week of its release, and sold over 30 million copies across all platforms.\\par\r\n\\par\r\nThree downloadable content (DLC) add-ons were released\\emdash Dawnguard, Hearthfire and Dragonborn\\emdash which were repackaged into The Elder Scrolls V: Skyrim \\endash  Legendary Edition, which was released in June 2013. The Elder Scrolls V: Skyrim \\endash  Special Edition, a remastered version of the game, was released for Windows, Xbox One and PlayStation 4 in October 2016, which includes all three DLC expansions and a graphical upgrade, along with additional features such as modding capabilities on consoles. Versions for the Nintendo Switch and PlayStation VR were released in November 2017.\\par\r\n\\par\r\n\\f1\\lang1044 (https://en.wikipedia.org/wiki/The_Elder_Scrolls_V:_Skyrim, 25.04.2018)\\f0\\lang1033\r\n}\r\n\0";
            quest2.Game = new Game();

            quest2.SetPhoto(admin, FileToImageMinimal(@"C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Images\image (4).jpg"));

            quest2.Steps.Add(new Step { About = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Go to \\b Highmountain Peak\\b0  in \\b The Forgotten Lands\\b0 .\r\n}\r\n\0" });
            quest2.Steps.Add(new Step { About = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Kill \\b Sky Elementals\\b0  until you have collected 10 \\b Air Totems\\b0 .\r\n}\r\n\0" });
            quest2.Steps.Add(new Step { About = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Place the Air Totems on the ground.\r\n}\r\n\0" });
            quest2.Steps.Add(new Step { About = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Play \\i The Song of Battle\\i0 .\r\n}\r\n\0" });
            quest2.Steps.Add(new Step { About = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 The \\b Silverwing Dragon\\b0  will appear. Slay it.\r\n}\r\n\0" });

            quest2.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 It's late and I have exam tomorrow, but I just can't stop questing!\r\n}\r\n\0"));
            quest2.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\b\\f0\\fs23\\lang1044 LOL!\\b0  He bugged out completely when I did it. Had to restart the game! :D\r\n}\r\n\0"));
            quest2.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 You know, you can actually complete this quest by befriending the dragon instead. You'll even be able to ride it! Just play \\i The Song of Peace\\i0  instead!\r\n}\r\n\0"));
            quest2.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 This took forever!\r\n}\r\n\0"));
            quest2.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Check out my YouTube videos at\\b  www.Iamatotalsellout.com\\b0 !\r\n}\r\n\0"));

            quest2.AddScreenshot(admin, FileToImageMinimal(@"C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Images\image (5).jpg"));
            quest2.AddScreenshot(admin, FileToImageMinimal(@"C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Images\image (6).jpg"));
            quest2.AddScreenshot(admin, FileToImageMinimal(@"C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Images\image (7).jpg"));

            quest2.Rewards.Add(new Reward("Gold", 150));
            quest2.Rewards.Add(new Reward("Dragonsword", 1));

            context.Quests.Add(quest1);
            context.Quests.Add(quest2);

            var wiki = new Wiki(admin)
            {
                Uid = new Guid("6e0ab048-feb1-4c00-8b2c-59488197b777"),
                Title = "Merlin Maelstrom",
                About = "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}{\\f1\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23 Merlin is a wizard.\\f0\\lang1033\r\n}\r\n\0"
            };

            wiki.SetPhoto(admin, FileToImageMinimal(@"C:\Users\thoma\source\repos\Game Explorer\Game Explorer\GameExplorer.Uwp\Images\image (1).jpg"));

            wiki.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Haha, this is great! Thanks!\\par\r\n}\r\n\0"));
            wiki.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 You forgot to mention that the spider boss can't be killed unless you drink a potion that \\b increases your damage-output by at least 50\\b0 .\\par\r\n}\r\n\0"));
            wiki.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Thanks! Finally I'll complete this. been stuck for hours!\\par\r\n}\r\n\0"));
            wiki.AddComment(new Comment(admin, "{\\rtf1\\fbidis\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Segoe UI;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Riched20 10.0.16299}\\viewkind4\\uc1 \r\n\\pard\\tx720\\cf1\\f0\\fs23\\lang1044 Might wan't to update the info. There's so much \\b lore \\b0 about this.\\par\r\n}\r\n\0"));

            context.Wikis.Add(wiki);

            base.Seed(context);
        }

        /// <summary>
        /// Files to byte array.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <returns></returns>
        public static byte[] FileToByteArray(string filepath)
        {
            var stream = File.OpenRead(filepath);
            var fileBytes = new byte[stream.Length];

            stream.Read(fileBytes, 0, fileBytes.Length);
            stream.Close();
            return fileBytes;
        }

        /// <summary>
        /// Files to image minimal.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static Image FileToImageMinimal(string filepath, string description = null)
        {
            var bytearray = FileToByteArray(filepath);
            return new Image(bytearray, description);
        }
    }
}
