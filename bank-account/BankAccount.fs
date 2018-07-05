module BankAccount
open System

type Account = private { 
    mutable balance: float; 
    isOpen: bool; 
    lock: Object;
}

let mkBankAccount() = { 
    balance = 0.0; 
    isOpen = false; 
    lock = new Object();
}

let openAccount account = { account with isOpen = true }

let closeAccount account = { account with isOpen = false }

let getBalance account = if not account.isOpen then None else Some account.balance

let updateBalance change account =
    lock account.lock (fun () -> account.balance <- account.balance + change)
    account
