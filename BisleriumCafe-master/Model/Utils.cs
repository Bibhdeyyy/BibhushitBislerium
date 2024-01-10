using System.Security.Cryptography;

namespace BisleriumCafe.Model
{
    /*public class Utils
    {
        public static string GetBasePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        }
        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetBasePath(), "Users.json");
        }
        public static string GetCoffeeFilePath()
        {
            return Path.Combine(GetBasePath(), "Coffee.json");
        }
        public static string GetAddinsFilePath()
        {
            return Path.Combine(GetBasePath(), "Addin.json");
        }
        public static string GetMemberFilePath()
        {
            return Path.Combine(GetBasePath(), "Member.json");
        }

        public static string GetOrderFilePath()
        {
            return Path.Combine(GetBasePath(), "Order.json");
        }
        public static string GetStaffPassword()
        {
            List<Users> users = UsersService.GetAllUsers();
            Users user = users.FirstOrDefault(x => x.role == "admin");
            return user.password;
        }
        public static string GetAdminPassword()
        {
            List<Users> users = UsersService.GetAllUsers();
            Users user = users.FirstOrDefault(x => x.role == "staff");
            return user.password;
        }
    }*/
    public static class Utils
    {

        private const char _segmentDelimiter = ':';
        // Encryption code
        public static string HashSecret(string input)
        {
            var saltSize = 16;
            var iterations = 100_000;
            var keySize = 32;
            HashAlgorithmName algorithm = HashAlgorithmName.SHA256;
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algorithm, keySize);

            return string.Join(
                _segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                iterations,
                algorithm
            );
        }
        // Yesle k garekko
        public static bool VerifyHash(string input, string hashString)
        {
            string[] segments = hashString.Split(_segmentDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );

            return CryptographicOperations.FixedTimeEquals(inputHash, hash);
        }


        private const string DataFolderPath = "../Data";
        public static string GetAppDirectoryPath()
        {
            //return Path.Combine(Directory.GetCurrentDirectory(), DataFolderPath);

            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "bruhdeep"
            );
        }

        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "users.json");
        }

        public static string GetAppCoffeeFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "coffee.json");
        }

        public static string GetAppAddInsFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "addIns.json");
        }
        public static string GetAppOrdersFilePath()
        {
            return Path.Combine(GetAppDirectoryPath(), "orders.json");
        }
    }
}