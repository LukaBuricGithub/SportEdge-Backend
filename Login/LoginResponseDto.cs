namespace SportEdge.API.Login
{
    /// <summary>
    /// Represents the response returned after a successful login.
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// The JWT access token.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
