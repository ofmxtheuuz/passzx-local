using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PassxzJsonVersion.models;

namespace Passzx.helpers;

public class indexhelper
{
    public static void home() // home method
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine();
        Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Passzx Local Version"));
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("    1. Ver todas as contas");
        Console.WriteLine("    2. Buscar contas pela categoria");
        Console.WriteLine("    3. Inserir uma conta");
        Console.WriteLine("    4. Atualizar uma conta");
        Console.WriteLine("    5. Sair");
        Console.WriteLine();
        Console.Write("    Qual sua opção: ");
        string response = Console.ReadLine();
        switch_response(response);
    }

    private static void switch_response(string response)
    {
        switch (response)
        {
            case "1":
                all_accounts();
                break;
            case "2":
                accounts_category();
                break;
            case "3":
                insert_account();
                break;
            case "4":
                update_account();
                break;
            case "5":
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();

                Environment.Exit(1);
                break;
        }
    }


    private static void all_accounts() // get all accounts
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine();
        Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Todas as contas"));
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;

        if (File.Exists("accounts.json"))
        {
            string json = File.ReadAllText("accounts.json");
            var accounts = JsonConvert.DeserializeObject<List<account>>(json);
            if (accounts == null)
            {
                Console.WriteLine("    Nenhuma conta encontrada");
                Console.WriteLine("    Retornando em 2 segundos");
                Thread.Sleep(2000);
                home();
            }

            foreach (var account in accounts)
            {
                Console.WriteLine();
                Console.WriteLine($"    {account.Title.ToUpper()} -");
                Console.WriteLine(
                    $"    Id: {account.Id}, Email: {account.Email}, Usuário: {account.Username}, Senha: {account.Password}, Categoria: {account.Category}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("    Nenhuma conta encontrada");
            Console.WriteLine("    Retornando em 2 segundos");
            Thread.Sleep(2000);
            home();
        }

        Console.WriteLine();
        Console.Write("     Pressione qualquer tecla para retornar");
        Console.ReadKey();
        home();
    }

    private static void insert_account() // insert an account
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine();
        Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Inserir uma conta"));
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;

        var account_id = 1;
        var accounts = new List<account>();
        if (File.Exists("accounts.json"))
        {
            string json = File.ReadAllText("accounts.json");
            if (json.Length > 0)
            {
                accounts = JsonConvert.DeserializeObject<List<account>>(json);
                if (accounts.Count >= 1)
                {
                    account_id = accounts.FindLast(x => x.Id != null).Id + 1;
                } 
            }
        }

        Console.WriteLine("    (Deixe em branco para cancelar)");
        
        Console.Write("    Titulo: ");
        var title = Console.ReadLine();
        Console.Write("    Email: ");
        var email = Console.ReadLine();
        Console.Write("    Usuario: ");
        var usuario = Console.ReadLine();
        Console.Write("    Senha: ");
        var senha = Console.ReadLine();
        Console.Write("    Categoria: ");
        var categoria = Console.ReadLine();

        if (title == null || senha == null || categoria == null)
        {
            Console.WriteLine();
            Console.WriteLine("    Operação cancelada!");
            Console.WriteLine("    Retornando em 3 segundos.");
            Thread.Sleep(3000);
            home();
        }

        accounts.Add(new()
        {
            Id = account_id,
            Title = title,
            Email = email,
            Username = usuario,
            Password = senha,
            Category = categoria
        });

        var newJson = JsonConvert.SerializeObject(accounts);
        File.WriteAllText("accounts.json", newJson);
        
        Console.WriteLine();
        Console.WriteLine("    Conta adicionada!");
        Console.WriteLine("    Retornando em 3 segundos.");
        Thread.Sleep(3000);
        
        home();
    }

    private static void accounts_category() // get accounts by category
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine();
        Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Buscar por categoria"));
        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;

        Console.Write("    Qual a categoria desejada? ");
        var categoria = Console.ReadLine();
        
        if (File.Exists("accounts.json"))
        {
            string json = File.ReadAllText("accounts.json");
            var accounts = JsonConvert.DeserializeObject<List<account>>(json);
            if (accounts == null)
            {
                Console.WriteLine("    Nenhuma conta encontrada");
                Thread.Sleep(2000);
                home();
            }

            accounts = accounts.Where(acc => acc.Category.ToLower().Contains(categoria.ToLower())).ToList();
            if (accounts != null)
            {
                if (accounts.Count == 0)
                {
                    Console.WriteLine("    Nenhuma conta encontrada");
                    Thread.Sleep(2000);
                    home();
                }
                foreach (var account in accounts)
                {
                    Console.WriteLine();
                    Console.WriteLine($"    {account.Title.ToUpper()} -");
                    Console.WriteLine(
                        $"    Id: {account.Id}, Email: {account.Email}, Usuário: {account.Username}, Senha: {account.Password}, Categoria: {account.Category}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("    Nenhuma conta encontrada");
                Console.WriteLine("    Retornando em 2 segundos");
                Thread.Sleep(2000);
                home();
            }
        }
        else
        {
            Console.WriteLine("    Nenhuma conta encontrada");
            Console.WriteLine("    Retornando em 2 segundos");
            Thread.Sleep(2000);
            home();
        }

        Console.WriteLine();
        Console.Write("     Pressione qualquer tecla para retornar");
        Console.ReadKey();
        home();
    }

    private static void update_account()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine();
        Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Atualizar uma conta"));
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;

        if (File.Exists("accounts.json"))
        {
            var json = File.ReadAllText("accounts.json");
            if (json.Length > 10)
            {
                var accounts = JsonConvert.DeserializeObject<List<account>>(json);
                if (accounts != null)
                {
                    Console.WriteLine("    (Deixe em branco para cancelar)");
        
                    Console.Write("    Id da conta: ");
                    var id = int.Parse(Console.ReadLine());
                    Console.Write("    Titulo: ");
                    var title = Console.ReadLine();
                    Console.Write("    Email: ");
                    var email = Console.ReadLine();
                    Console.Write("    Usuario: ");
                    var usuario = Console.ReadLine();
                    Console.Write("    Senha: ");
                    var senha = Console.ReadLine();
                    Console.Write("    Categoria: ");
                    var categoria = Console.ReadLine();

                    if (title == null || senha == null || categoria == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("    Operação cancelada!");
                        Console.WriteLine("    Retornando em 3 segundos.");
                        Thread.Sleep(3000);
                        home();
                    }

                    var account = accounts.FirstOrDefault(x => x.Id == id);
                    if (account != null)
                    {
                        accounts.Remove(account);
                        accounts.Add(new()
                        {
                            Id = id,
                            Title = title,
                            Email = email,
                            Username = usuario,
                            Password = senha,
                            Category = categoria
                        });

                        var newJson = JsonConvert.SerializeObject(accounts);
                        File.WriteAllText("accounts.json", newJson);
                    }
                    else
                    {
                        Console.WriteLine("    Nenhuma conta encontrada");
                        Console.WriteLine("    Retornando em 2 segundos");
                        Thread.Sleep(2000);
                        home();
                    }
                }
                else
                {
                    Console.WriteLine("    Nenhuma conta encontrada");
                    Console.WriteLine("    Retornando em 2 segundos");
                    Thread.Sleep(2000);
                    home();
                }
            }
            else
            {
                Console.WriteLine("    Nenhuma conta encontrada");
                Console.WriteLine("    Retornando em 2 segundos");
                Thread.Sleep(2000);
                home();
            }
        }
        else
        {
            Console.WriteLine("    Nenhuma conta encontrada");
            Console.WriteLine("    Retornando em 2 segundos");
            Thread.Sleep(2000);
            home();
        }

        Console.WriteLine();
        Console.WriteLine("    Conta atualizada!");
        Console.WriteLine("    Retornando em 3 segundos.");
        Thread.Sleep(3000);
        home();
    }
}
