namespace WordleOnlineServer.Services
{
    public class DictionaryService
    {
        public async Task<bool> IsWordEnableForUsing(string word)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"https://api.dictionaryapi.dev/api/v2/entries/en/{word}");

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (HttpRequestException e)
                {

                    return false;
                }
            }
        }
    }
}
