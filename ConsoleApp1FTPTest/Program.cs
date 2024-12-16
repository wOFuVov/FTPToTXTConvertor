using System;
using System.Net;
using System.IO;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

// string[] mss_text = File.ReadAllLines(@"C:\Users\vovas\Desktop\vova\test.txt");
// for(int i = 0;i< mss_text.Length;i++)
// {
//     File.WriteAllLines(@"C:\Users\vovas\Desktop\vova\test2.txt",mss_text);
// }






internal class Program
{
    private static void Main(string[] args)
    {
        //------------------------
        //string path = @"C:\Users\vovas\Desktop\vova\WinFormClientSmall.png";
        string textPath = @"C:\Users\vovas\Desktop\vova\My projects\ConsoleApp1FTPTest\TestTXTfile.txt";
        string ftpUrl = "ftp://test.rebex.net/pub/example/"; // Адрес FTP-сервера
        string username = "demo";    // Ваш логин
        string password = "password";    // Ваш пароль

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
        //request.Method = WebRequestMethods.Ftp.ListDirectory;
        request.Method = WebRequestMethods.Ftp.ListDirectory;
        request.Credentials = new NetworkCredential(username, password);


        try
        {
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            // using (Stream responseStream = response.GetResponseStream())
            // using (FileStream fileStream = new FileStream(path,FileMode.Create))
            // {
            //      if (responseStream != null)
            //     {
            //         responseStream.CopyTo(fileStream); // Copy data to local file
            //     }
            //     Console.WriteLine($"Download Complete, status: {response.StatusDescription}");
            // }
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                
                string line;
                int count = 0;
                bool test = false;
                string[] lines2 = new string[100];
                while ((line = reader.ReadLine()) != null)
                {
                    
                    string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        for(int i = 0;i<line.Length;i++)
                        {
                            if(line[i] == '.' )//test.txt
                            {
                                test = true;
                                break;
                                
                            }
                        }
                        
                        if(test == true)
                        {
                            File.AppendAllText(textPath,$"\n{currentTime}| Got file: " + line);
                            test = false;
                        }
                        else{
                            lines2[count] = line;
                            File.AppendAllText(textPath,$"\n{currentTime}| Got derectory: " + lines2[count]);
                            count++;
                        }
                    //lines2[count] = line;
                    Console.WriteLine(line);
                    //File.AppendAllText(textPath,$"\n{currentTime}| Got derectory: " + lines2[count]);
                    //File.AppendAllText(textPath,$"\n{currentTime}| Got file: " + line);
                    //File.WriteAllLines(textPath,line);
                    //count++;
                    
                   
                }
                
                
                Console.WriteLine($"Directory list complete, status: {response.StatusDescription}");
                File.AppendAllText(textPath,"\n---------------------------------------------------------------------------------------------\n\n");

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}


