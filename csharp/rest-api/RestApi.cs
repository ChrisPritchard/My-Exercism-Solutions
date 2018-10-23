using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

public class RestApi
{
    private List<User> database;

    public RestApi(string database) => 
        this.database = JsonConvert.DeserializeObject<List<User>>(database);

    public string Get(string url, string payload = null)
    {
        if(url == "/users")
        {
            if(payload == null)
                return JsonConvert.SerializeObject(database.OrderBy(o => o.name));
            var model = JsonConvert.DeserializeObject<GetUsersModel>(payload);
            var users = database.Where(o => model.users.Contains(o.name));
            return JsonConvert.SerializeObject(users.OrderBy(o => o.name));
        }
        else
            throw new InvalidOperationException();
    }

    public string Post(string url, string payload)
    {
        if(url == "/add")
        {
            var model = JsonConvert.DeserializeObject<AddModel>(payload);
            var newUser = new User(model.user);
            database.Add(newUser);
            return JsonConvert.SerializeObject(newUser);
        }
        else if(url == "/iou")
        {
            var model = JsonConvert.DeserializeObject<IOUModel>(payload);
            var lender = database.Single(o => o.name == model.lender);
            var borrower = database.Single(o => o.name == model.borrower);
            LendTo(lender, borrower, model.amount);
            return JsonConvert.SerializeObject(new [] { lender, borrower }.OrderBy(o => o.name));
        }
        else
            throw new InvalidOperationException();
    }

    private void LendTo(User lender, User borrower, double amount)
    {
        var existingDebt = lender.owes.GetValueOrDefault(borrower.name, 0);
        if(existingDebt > amount)
            LendTo(borrower, lender, existingDebt - amount);
        else if (existingDebt > 0)
        {
            lender.owes.Remove(borrower.name);
            borrower.owed_by.Remove(lender.name);
            LendTo(lender, borrower, amount - existingDebt);
        }
        else
        {
            lender.owed_by[borrower.name] = amount;
            borrower.owes[lender.name] = amount;
        }
    }
}

public struct GetUsersModel
{
    public string[] users { get; set; }
}

public struct AddModel
{
    public string user { get; set; }
}

public struct IOUModel
{
    public string lender { get;set; }
    public string borrower { get;set; }
    public double amount { get;set; }
}

public struct User
{
    public string name { get;set; }
    public SortedDictionary<string, double> owes { get;set; }
    public SortedDictionary<string, double> owed_by { get;set; }
    public double balance => owed_by.Sum(o => o.Value) - owes.Sum(o => o.Value);

    public User(string name)
    {
        this.name = name;
        owes = new SortedDictionary<string, double>();
        owed_by = new SortedDictionary<string, double>();
    }
}