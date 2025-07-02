namespace SportEdge.API.Services.Interface
{
    /// <summary>
    /// Defines service operations for password hasher.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Converts user's regular password into hash password.
        /// </summary>
        /// <param name="password">User's regular passwrod.</param>
        /// <returns>Converted password in hash form.</returns>
        public string Hash(string password);


        /// <summary>
        /// Verifies if entered password matches hashed password.
        /// </summary>
        /// <param name="password">Entered password.</param>
        /// <param name="passwordHash">Hashed password.</param>
        /// <returns>True if passwords do match, otherwise false.</returns>
        public bool Verify(string password, string passwordHash);
    }
}
