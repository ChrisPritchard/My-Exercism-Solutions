package diffiehellman

import (
	"crypto/rand"
	"math/big"
)

func PrivateKey(p *big.Int) *big.Int {
	max := big.NewInt(0).Sub(p, big.NewInt(2))
	v, _ := rand.Int(rand.Reader, max)
	return big.NewInt(0).Add(v, big.NewInt(2))
}

func PublicKey(private, p *big.Int, g int64) *big.Int {
	a := big.NewInt(0).Exp(big.NewInt(g), private, nil)
	return big.NewInt(0).Mod(a, p)
}

func NewPair(p *big.Int, g int64) (private, public *big.Int) {
	private = PrivateKey(p)
	return private, PublicKey(private, p, g)
}

func SecretKey(private1, public2, p *big.Int) *big.Int {
	a := big.NewInt(0).Exp(public2, private1, nil)
	return big.NewInt(0).Mod(a, p)
}
