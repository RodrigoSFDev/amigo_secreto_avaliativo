using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

class Amigo
{
    public string Nome { get; set; }
    public string Email { get; set; }

    public Amigo(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}

public class MainForm : Form
{
    private List<Amigo> amigos = new List<Amigo>();

    public MainForm()
    {
        
        Text = "Desafio do Amigo Secreto";

        Button cadastrarButton = new Button();
        cadastrarButton.Text = "Cadastrar Amigo";
        cadastrarButton.Click += CadastrarAmigo;

        Button listarButton = new Button();
        listarButton.Text = "Listar Amigos";
        listarButton.Click += ListarAmigos;

        Button gerarButton = new Button();
        gerarButton.Text = "Gerar Amigo Secreto";
        gerarButton.Click += GerarAmigoSecreto;

        FlowLayoutPanel panel = new FlowLayoutPanel();
        panel.Controls.Add(cadastrarButton);
        panel.Controls.Add(listarButton);
        panel.Controls.Add(gerarButton);

        Controls.Add(panel);
    }

    private void CadastrarAmigo(object sender, EventArgs e)
    {
        string nome = ShowInputDialog("Informe o nome do amigo:");
        string email = ShowInputDialog("Informe o email do amigo:");

        Amigo novoAmigo = new Amigo(nome, email);
        amigos.Add(novoAmigo);

        using (StreamWriter writer = new StreamWriter("amigos.csv", true))
        {
            writer.WriteLine($"{novoAmigo.Nome};{novoAmigo.Email}");
        }
    }

    private void ListarAmigos(object sender, EventArgs e)
    {
        string listaAmigos = "Lista de Amigos:\n";

        foreach (Amigo amigo in amigos)
        {
            listaAmigos += $"{amigo.Nome} - {amigo.Email}\n";
        }

        MessageBox.Show(listaAmigos, "Listar Amigos");
    }

    private void GerarAmigoSecreto(object sender, EventArgs e)
    {
        Random random = new Random();

        List<Amigo> amigosParaSortear = new List<Amigo>(amigos);

        using (StreamWriter writer = new StreamWriter("secretos.csv"))
        {
            foreach (Amigo amigo in amigos)
            {
                int index = random.Next(amigosParaSortear.Count);
                Amigo amigoSecreto = amigosParaSortear[index];

                amigosParaSortear.RemoveAt(index);

                writer.WriteLine($"{amigo.Nome};{amigo.Email};{amigoSecreto.Nome};{amigoSecreto.Email}");
            }
        }
    }

    private string ShowInputDialog(string prompt)
    {
        Form promptForm = new Form();
        promptForm.Width = 500;
        promptForm.Height = 150;
        promptForm.Text = prompt;

        TextBox textBox = new TextBox() { Left = 50, Top = 25, Width = 400 };
        Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };

        confirmation.Click += (sender, e) => { promptForm.Close(); };
        promptForm.Controls.Add(textBox);
        promptForm.Controls.Add(confirmation);

        promptForm.ShowDialog();

        return textBox.Text;
    }

    static void Main()
    {
        Application.Run(new MainForm());
    }
}
