using System;
using System.Security.Cryptography;
using System.Text;

namespace Hash
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            var salt = "";
            var text = "test" + salt;
            var md5 = GetHashString<MD5CryptoServiceProvider>(text);
            var sha1 = GetHashString<SHA1CryptoServiceProvider>(text);
            var sha256 = GetHashString<SHA256CryptoServiceProvider>(text);
            var sha512 = GetHashString<SHA512CryptoServiceProvider>(text);
            Console.WriteLine($"MD5: {md5}");
            Console.WriteLine($"SHA1: {sha1}");
            Console.WriteLine($"SHA256: {sha256}");
            Console.WriteLine($"SHA512: {sha512}");

            Console.ReadKey();
        }

        public static string GetHashString<T>(string text) where T : HashAlgorithm, new()
        {
            // 文字列をバイト型配列に変換する
            byte[] data = Encoding.UTF8.GetBytes(text);

            // ハッシュアルゴリズム生成
            var algorithm = new T();

            // ハッシュ値を計算する
            byte[] bs = algorithm.ComputeHash(data);

            // リソースを解放する
            algorithm.Clear();

            // バイト型配列を16進数文字列に変換
            var result = new StringBuilder();
            foreach (byte b in bs)
            {
                result.Append(b.ToString("x2"));
            }
            return result.ToString();
        }
    }
}
