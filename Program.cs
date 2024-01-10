using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main()
    {
        string apiKey = "8d9230b5708495ac984547aa66d224fa"; //
        string baseUrl = "https://api.themoviedb.org/3";
        string nowPlayingEndpoint = "/movie/now_playing";
        string language = "pt-BR"; // Idioma dos resultados

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = $"{baseUrl}{nowPlayingEndpoint}?api_key={apiKey}&language={language}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JObject.Parse(json);

                    Console.WriteLine("Filmes Lançamentos do Momento:\n");

                    foreach (var movie in data.results)
                    {
                        Console.WriteLine($"{movie.title} - Lançamento: {movie.release_date}");
                        Console.WriteLine($"Resumo: {movie.overview}\n");
                    }
                }
                else
                {
                    Console.WriteLine($"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
        Console.ReadKey();
    }
}
