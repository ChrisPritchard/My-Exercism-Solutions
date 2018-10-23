using System;
using bigint = System.Numerics.BigInteger;

public static class DiffieHellman
{
    public static bigint PrivateKey(bigint primeP) 
    {
        var buffer = new byte[primeP.ToByteArray().Length];
        
        var random = new Random();
        random.NextBytes(buffer);
        bigint result;
        while((result = new bigint(buffer, true)) > primeP)
            random.NextBytes(buffer);

        return result;
    }

    public static bigint PublicKey(bigint primeP, bigint primeG, bigint privateKey) 
        => bigint.ModPow(primeG, privateKey, primeP);

    public static bigint Secret(bigint primeP, bigint publicKey, bigint privateKey) 
        => bigint.ModPow(publicKey, privateKey, primeP);
}