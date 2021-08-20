package cipher

import "strings"

type Vigenere struct {
	encodeKey []int
	decodeKey []int
}

func encode(input string, key []int) string {
	clean := []rune{}
	for _, v := range input {
		if (v > 'Z' || v < 'A') && (v > 'z' || v < 'a') {
			continue
		}
		if v >= 'A' && v <= 'Z' {
			v = (v - 'A') + 'a'
		}
		clean = append(clean, v)
	}
	result := ""
	for i, v := range clean {
		delta := key[i%len(key)]
		nv := v + rune(delta)
		if nv > 'z' {
			nv = ((nv - 'z') - 1) + 'a'
		} else if nv < 'a' {
			nv = 'z' - (('a' - nv) - 1)
		}
		result += string(nv)
	}
	return result
}

func (c Vigenere) Encode(s string) string {
	return encode(s, c.encodeKey)
}

func (c Vigenere) Decode(s string) string {
	return encode(s, c.decodeKey)
}

func NewCaesar() Cipher {
	return Vigenere{[]int{3}, []int{-3}}
}

func NewShift(distance int) Cipher {
	if distance < -25 || distance > 25 || distance == 0 {
		return nil
	}
	return Vigenere{[]int{distance}, []int{-distance}}
}

func NewVigenere(key string) Cipher {
	if len(strings.ReplaceAll(key, "a", "")) == 0 {
		return nil
	}
	enc, dec := []int{}, []int{}
	for _, v := range key {
		if v < 'a' || v > 'z' {
			return nil
		}
		delta := int(v - 'a')
		enc = append(enc, delta)
		dec = append(dec, -delta)
	}
	return Vigenere{enc, dec}
}
