using SportEdge.API.Services.Interface;
using System.Security.Cryptography;

namespace SportEdge.API.Services.Implementation
{

    /// <summary>
    /// Provides implementation for password hasher interface.
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        //128-bits (16 bytes)
        private const int SaltSize = 16;
        
        //256-bits (32 bytes)
        private const int HashSize = 32;
        
        private const int Iterations = 100000;

        private readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        /// <inheritdoc/>
        public string Hash(string password) 
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password,salt,Iterations,Algorithm,HashSize);
        
            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        
        }

        /// <inheritdoc/>
        public bool Verify(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }

        /*
          /// <inheritdoc/>
        public async Task<GenderDto> UpdateAsync(int id, UpdateGenderRequestDto request)
        {
            var existingGender = await genderRepository.GetAsync(id);
            if (existingGender == null) 
            {
                throw new KeyNotFoundException($"Gender with ID {id} not found.");
            }

            var genderDomain = mapping.ToDomain(request);

            existingGender.Name = genderDomain.Name;

            var updatedGender = await genderRepository.UpdateAsync(existingGender);
            return mapping.ToDto(updatedGender);
        }
         
         */


    }
}
