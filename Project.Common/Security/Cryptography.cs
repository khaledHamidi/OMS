using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace OMS.Common.Security;
public static class Cryptography
{
    public static HashSalt EncryptPassword(string password)
    {
        var salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        var encryptedPass = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: 256 / 8
        ));
        return new HashSalt { Hash = encryptedPass, Salt = salt };
    }

    public static bool VerifyPassword(string enteredPassword, byte[] salt, string storedPassword)
    {
        var encryptedPass = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: enteredPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: 256 / 8
        ));
        return encryptedPass == storedPassword;
    }
}