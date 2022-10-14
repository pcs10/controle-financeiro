using CryptSharp;

namespace ControleFinanceiro.Services
{
    public static class Criptografia
    {
        public static string Criptografar(string senha)
        {
            return Crypter.Sha256.Crypt(senha);
        }

        public static bool Comparar(string senha, string hash)
        {
            return Crypter.CheckPassword(senha, hash);
        }
    }
}
