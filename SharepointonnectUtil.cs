private async Task GetClientContextWithAccessToken1(string targetUrl)
{
    var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { "https://*****.sharepoint.com/Sites.Manage.All" });

    using(ClientContext clientContext = new ClientContext(targetUrl))
    {
        clientContext.ExecutingWebRequest +=
            delegate (object oSender, WebRequestEventArgs webRequestEventArgs)
            {
                webRequestEventArgs.WebRequestExecutor.RequestHeaders["Authorization"] =
                    "Bearer " + accessToken;
            };
        List list = clientContext.Web.Lists.GetByTitle("TestDocumentLibrary");
        clientContext.Load(list);
        clientContext.ExecuteQuery();
    }
}
