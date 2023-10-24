using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        string serverIp = "192.168.1.8";  
        int serverPort = 80; 
        TcpListener server = new TcpListener(IPAddress.Parse(serverIp), serverPort);
        server.Start();
        Console.WriteLine("Servidor encendido");

        try
        {
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                NetworkStream stream = client.GetStream();
                byte[] data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string message = Encoding.ASCII.GetString(data, 0, bytesRead);
                Console.WriteLine("Mensaje del cliente: " + message);

                string[] clientData = message.Split('|');
                if (clientData.Length >= 3)
                {
                    string nombre = clientData[0];
                    string apellidos = clientData[1];
                    string edad = clientData[2];
                    Console.WriteLine("Nombre: " + nombre);
                    Console.WriteLine("Apellidos: " + apellidos);
                    Console.WriteLine("Edad: " + edad);
                }

                string responseMessage = "Datos recibidos correctamente.";
                byte[] response = Encoding.ASCII.GetBytes(responseMessage);
                stream.Write(response, 0, response.Length);

                client.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        finally
        {
            server.Stop();
        }
    }
}
