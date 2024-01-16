using System;
using BankProjectEfCore.Models;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;


namespace Bank{
    class Bank_Functions{
        private static Ace52024Context db = new Ace52024Context();
        //Transaction ID counter
        public static int Tid = 0;

        //Add Account function
        public static void AddAccount(){
            Console.WriteLine();
            KartikSbaccount sb = new KartikSbaccount();
            Console.WriteLine("Enter account number of account to be added: ");
            int acc;
            bool check = int.TryParse(Console.ReadLine(), out acc);
            if(!check){
                throw new AccountNotValidException("Account number not in correct format, only input integers!");
            }
            Console.WriteLine("Enter customer name: ");
            sb.CustomerName = Console.ReadLine();
            Console.WriteLine("Enter customer address: ");
            sb.CustomerAddress = Console.ReadLine();
            Console.WriteLine("Enter current balance: ");
            sb.CurrentBalance = decimal.Parse(Console.ReadLine());
            sb.AccountNumber = acc;
            db.KartikSbaccounts.Add(sb);
            db.SaveChanges();
            Console.WriteLine("Account added");
        }

        //Deposit Amount function
        public static void DepositAmount(int accNo, decimal amt){
            Console.WriteLine();
            KartikSbaccount sb = null;
            foreach(var item in db.KartikSbaccounts){
                if(item.AccountNumber==accNo){
                    sb = item;
                    break;
                }
            }
            if(sb!=null){
                sb.CurrentBalance += amt;
                Console.WriteLine($"{amt} deposited in {sb.CustomerName} 's account");
                Console.WriteLine($"Available balance now is {sb.CurrentBalance}");
                //Add transaction to transactions table
                Tid++;
                KartikSbtransaction tr = new KartikSbtransaction();
                tr.TransactionId = Tid;
                tr.TransactionDate = DateTime.Now;
                tr.AccountNumber = accNo;
                tr.Amount = amt;
                tr.TransactionType = "Deposit";
                db.KartikSbtransactions.Add(tr);
                db.SaveChanges();
            }
            else{
                throw new AccountNotValidException("Account number not in database");
            }
        }

        //Withdraw function
        public static void WithdrawAmount(int accNo, decimal amt){
            Console.WriteLine();
            KartikSbaccount sb = null;
            foreach(var item in db.KartikSbaccounts){
                if(item.AccountNumber==accNo){
                    sb = item;
                    break;
                }
            }
            if(sb!=null){
                if(amt<=sb.CurrentBalance){
                    sb.CurrentBalance -= amt;
                    Console.WriteLine($"{amt} withdrawed from {sb.CustomerName} 's account");
                    Console.WriteLine($"Available balance now is {sb.CurrentBalance}");
                    Tid++;
                    KartikSbtransaction tr = new KartikSbtransaction();
                    tr.TransactionId = Tid;
                    tr.TransactionDate = DateTime.Now;
                    tr.AccountNumber = accNo;
                    tr.Amount = amt;
                    tr.TransactionType = "Withdraw";
                    db.KartikSbtransactions.Add(tr);
                    db.SaveChanges();
                }
                else{
                    throw new InsufficientBalanceException("Insufficent balance, cannot withdraw!");
                }
            }
            else{
                throw new AccountNotValidException("Account number not in database");
            }
        }

        //Get AccountDetails function
        public static void GetAccountDetails(int accNo){
            Console.WriteLine();
            KartikSbaccount sb = null;
            foreach(var item in db.KartikSbaccounts){
                if(item.AccountNumber == accNo){
                    sb = item;
                    break;
                }
            }
            if(sb==null){
                throw new AccountNotValidException("Account number provided not valid!");;
            }
            Console.WriteLine($"Account details with {accNo} Account Number: ");
            Console.WriteLine(sb.AccountNumber + " " + sb.CustomerName + " " + sb.CustomerAddress + " " + sb.CurrentBalance);
        } 

        //Get All Accounts
        public static void GetAllAccounts(){
            Console.WriteLine();
            Console.WriteLine("All Accounts: ");
            foreach(var item in db.KartikSbaccounts){
                Console.WriteLine($"{item.AccountNumber} | {item.CustomerName} | {item.CustomerAddress} | {item.CurrentBalance}");
            }
        }

        public static void GetAllTransactions(int accNo){
            Console.WriteLine();
            KartikSbaccount sb = null;
            foreach(var item in db.KartikSbaccounts){
                if(item.AccountNumber == accNo){
                    sb = item;
                    break;
                }
            }
            if(sb==null){
                throw new AccountNotValidException("Account number provided not valid!");;
            }
            else{
                Console.WriteLine($"All transactions for given account number {accNo} -");
                foreach(var item in db.KartikSbtransactions){
                    if(item.AccountNumber==accNo){
                        Console.WriteLine($"{item.TransactionId} | {item.TransactionDate} | {item.AccountNumber} | {item.Amount} | {item.TransactionType}");
                    }
                }
            }
        }

        //Main Program
        public static void Main(){
            try{
                AddAccount();
                AddAccount();
                DepositAmount(1, 5000);
                DepositAmount(2, 10000);
                GetAllTransactions(2);
            }
            catch(AccountNotValidException e){
                Console.WriteLine(e.Message);
            }
            catch(InsufficientBalanceException e){
                Console.WriteLine(e.Message);
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}