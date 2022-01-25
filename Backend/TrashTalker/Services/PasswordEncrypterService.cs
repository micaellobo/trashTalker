using Scrypt;

namespace TrashTalker.Services
{
    /// <summary>
    /// This Class represents a Password Encrypter Service that contains all the necessary methods for encrypting use passwords stored in the SQL database.
    /// </summary>
    public static class PasswordEncrypterService
    {
        /// <summary>
        /// Returns a encrypted password.
        /// </summary>
        /// <param name="password">Password to be encrypted</param>
        /// <returns>string</returns>
        public static string encryptPassword(string password)
        {
            var encoder = new ScryptEncoder();
            return encoder.Encode(password);
        }
        
        /// <summary>
        /// Compares a password with a encrypted password.
        /// </summary>
        /// <param name="password">Password not encrypted</param>
        /// <param name="passwordEncrypted">Password encrypted</param>
        /// <returns>"true" when the passwords match</returns>
        /// <returns>"false" when the passwords do not match</returns>
        public static bool comparePasswords(string password, string passwordEncrypted )
        {
            var encoder = new ScryptEncoder();
            return encoder.Compare(password, passwordEncrypted);
        }
        
    }
}