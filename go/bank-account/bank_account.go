package account

import (
	"sync"
)

// Account represents a bank account
type Account struct {
	balance  int64
	isClosed bool
	sync.Mutex
}

// Open creates a new account with the initial balance
func Open(initialDeposit int64) *Account {
	if initialDeposit < 0 {
		return nil
	}
	return &Account{initialDeposit, false, sync.Mutex{}}
}

// Close closes an account
func (account *Account) Close() (payout int64, ok bool) {
	account.Lock()
	defer account.Unlock()

	if account.isClosed {
		return
	}

	account.isClosed = true
	return account.balance, true
}

// Balance gets the balance of an open account
func (account *Account) Balance() (balance int64, ok bool) {
	if account.isClosed {
		return
	}
	return account.balance, true
}

// Deposit adds to the balance of an open account
func (account *Account) Deposit(amount int64) (newBalance int64, ok bool) {
	account.Lock()
	defer account.Unlock()

	if account.isClosed || account.balance+amount < 0 {
		return
	}

	account.balance += amount
	return account.balance, true
}
