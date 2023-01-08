HttpClient _client = new HttpClient
{
    Timeout = TimeSpan.FromSeconds(5)
};
SemaphoreSlim _gate = new SemaphoreSlim(20);



Task.WaitAll(CreateCalls().ToArray());


IEnumerable<Task> CreateCalls()
{
    for (int i = 0; i < 5; i++)
    {
        yield return CallGoogle();
    }
}


async Task CallGoogle()
{
    try
    {
        await _gate.WaitAsync();
        var response = await _client.GetAsync("https://google.com");
        _gate.Release();

        Console.WriteLine(response.StatusCode);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);    
    }
}