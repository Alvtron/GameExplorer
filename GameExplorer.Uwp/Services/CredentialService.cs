using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Security.Credentials;

namespace GameExplorer.Uwp.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class CredentialService
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Response
        {
            /// <summary>
            /// The exist
            /// </summary>
            Exist,
            /// <summary>
            /// The not found
            /// </summary>
            NotFound,
            /// <summary>
            /// The added
            /// </summary>
            Added,
            /// <summary>
            /// The removed
            /// </summary>
            Removed,
            /// <summary>
            /// The not logged in
            /// </summary>
            NotLoggedIn,
            /// <summary>
            /// The logged in
            /// </summary>
            LoggedIn
        }

        /// <summary>
        /// The resource name
        /// </summary>
        private const string ResourceName = "Game Explorer";
        /// <summary>
        /// The vault
        /// </summary>
        private static readonly PasswordVault Vault = new PasswordVault();
        /// <summary>
        /// Gets a value indicating whether this <see cref="CredentialService"/> is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if empty; otherwise, <c>false</c>.
        /// </value>
        public static bool Empty => Vault.RetrieveAll().Count == 0;
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public static int Count => Vault.RetrieveAll().Count;

        /// <summary>
        /// Adds to vault.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static Response AddToVault(string username, string password)
        {
            Debug.WriteLine("Creating new user");
            var credential = new PasswordCredential(ResourceName, username, password);

            if (Contains(credential))
            {
                Debug.WriteLine("CurrentUser exists. No need to add to vault.");
                Settings.SetDefaultUser(credential.UserName);
                return Response.Exist;
            }

            Debug.WriteLine("CurrentUser doesn't exist.");
            Vault.Add(credential);
            Debug.WriteLine("CurrentUser added to Vault.");
            Settings.SetDefaultUser(credential.UserName);
            Debug.WriteLine("CurrentUser set as Default CurrentUser.");
            return Response.Added;
        }

        /// <summary>
        /// Determines whether [contains] [the specified credential].
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified credential]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(PasswordCredential credential)
        {
            if (Empty)
            {
                return false;
            }

            credential.RetrievePassword();

            foreach (var c in Vault.RetrieveAll())
            {
                c.RetrievePassword();
                if (credential.UserName.Equals(c.UserName) && credential.Password.Equals(c.Password))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns></returns>
        public static Response Login()
        {
            var loginCredential = GetCurrent();

            if (loginCredential != null)
            {
                // There is a credential stored in the locker.
                // Populate the Password property of the credential
                // for automatic login.
                loginCredential.RetrievePassword();
            }
            else
            {
                // Display UI to get user credentials.
                return Response.NotLoggedIn;
            }

            // TODO: Log in user to database

            return Response.LoggedIn;
        }

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <returns></returns>
        public static PasswordCredential GetCurrent()
        {
            IReadOnlyList<PasswordCredential> credentialList;

            try
            {
                credentialList = Vault.FindAllByResource(ResourceName);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                return null;
            }

            if (credentialList.Count == 0)
                return null;

            if (credentialList.Count == 1)
                return credentialList[0];

            // There are multiple credentials. Check if there is a default credential.
            if (Settings.DefaultUserExist)
            {
                return Vault.Retrieve(ResourceName, Settings.DefaultUserName);
            }
            else
            {
                // TODO: CurrentUser must select on of the users in the vault
                return null;
            }
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public static List<PasswordCredential> GetAll()
        {
            return Vault.RetrieveAll().ToList();
        }

        /// <summary>
        /// Deletes the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static Response Delete(string username, string password)
        {
            return Delete(new PasswordCredential(ResourceName, username, password));
        }

        /// <summary>
        /// Deletes the specified credential.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <returns></returns>
        public static Response Delete(PasswordCredential credential)
        {
            if (!Contains(credential))
                return Response.NotFound;

            Vault.Remove(credential);

            if (credential.UserName.Equals(Settings.DefaultUserName))
                Settings.RemoveDefaultUser();

            return Response.Removed;
        }

        /// <summary>
        /// Deletes all.
        /// </summary>
        public static void DeleteAll()
        {
            foreach (var credential in Vault.RetrieveAll())
                Vault.Remove(credential);
        }

        /// <summary>
        /// Alls to string.
        /// </summary>
        /// <returns></returns>
        internal static string AllToString()
        {
            var output = "";

            foreach (var credential in Vault.RetrieveAll())
            {
                credential.RetrievePassword();
                output += string.Format("CurrentUser name: {0}\nPassword: {1}\n", credential.UserName, credential.Password);
            }

            return output;
        }
    }
}
