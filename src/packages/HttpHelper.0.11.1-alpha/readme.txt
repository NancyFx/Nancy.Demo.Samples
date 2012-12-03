# HttpHelper

This project has been renamed and was formerly called FluentHttp.Core.

## Overview
HttpHelper is a light weight library aimed to ease the development for your rest client with
consistent api throughout different frameworks whether you are using .net 2.0+ or silverlight or
window phone. (It is not aimed to be used directly by the developers, but rather for creating 
a http client wrapper, such as for Facebook, Github, Twitter, Google etc.)

## Supported Frameworks

* .NET 4.5
* .NET 4.0 (client profile supported)
* .NET 3.5 (client profile supported)
* .NET 3.0
* .NET 2.0
* Windows 8 Metro Style Applications - WinRT (Windows 8 Consumer Preview)
* Silverlight 4.0+
* Windows Phone 7+
* Portable Library - `#define HTTPHELPER_PORTABLE_LIBRARY`
* Mono

## NuGet

```
Install-Package HttpHelper
```

## Samples

**Note:**

* **Developer is reponsible for manually disposing request (write) and response (read) streams.**
* **Always create new instance of `HttpHelper` for each new requests.**

### HTTP GET

#### Synchronous Sample

*Synchronous methods are not supported in Silverlight/Windows Phone/WinRT (Windows Metro Apps)*

Make a GET request to https://graph.facebook.com/4 and return the response as string.

```csharp
public static string GetSyncSample()
{
    var httpHelper = new HttpHelper("https://graph.facebook.com/4");

    using (var stream = httpHelper.OpenRead())
    {
        using (var reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
}
```

#### Asynchronous Sample with Async/Await

*Available only in .NET 4.5+, and WinRT (Windows Metro Apps)*

Make an asynchrounous GET request to https://graph.facebook.com/4 and return the response as task of string.

`HTTPHELPER_TPL` conditional compilation symbol must be defined in order to use XTaskAsync methods.

```csharp
public static async Task<string> GetAsyncAwaitSample(CancellationToken cancellationToken = default(CancellationToken))
{
    var httpHelper = new HttpHelper("https://graph.facebook.com/4");

    using (var stream = await httpHelper.OpenReadTaskAsync(cancellationToken))
    {
        using (var reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
}
```

#### Asynchronous Sample with Task Parallel Library (TPL)

*Available only in .NET 4.0+, SL5 and WinRT (Windows Metro Apps)*

Make an asynchrounous GET request to https://graph.facebook.com/4 and return the response as task of string.

`HTTPHELPER_TPL` conditional compilation symbol must be defined in order to use XTaskAsync methods.

```csharp
public static Task<string> GetAsyncTaskSample(CancellationToken cancellationToken = default(CancellationToken))
{
    var httpHelper = new HttpHelper("https://graph.facebook.com/4");

    return httpHelper
        .OpenReadTaskAsync(cancellationToken)
        .ContinueWith(t =>
                          {
                              // propagate previous task exceptions correctly.
                              if (t.IsFaulted || t.IsCanceled) t.Wait();

                              using (var stream = t.Result)
                              {
                                  using (var reader = new StreamReader(stream))
                                  {
                                      return reader.ReadToEnd();
                                  }
                              }
                          });
}
```

#### Asynchronous Sample with Event Programming Model (EPM)

*Available in all .NET plaforms.*

Make an asynchrounous GET request to https://graph.facebook.com/4 and call the callback on completion.
`Action<string, object, bool, Exception>` is mapped to response string, userState, isCancelled and Exception. 

```csharp
public static void GetAsyncSample(Action<string, object, bool, Exception> callback = null, object userState = null)
{
    var httpHelper = new HttpHelper("https://graph.facebook.com/4");

    httpHelper.OpenReadCompleted +=
        (o, e) =>
        {
            if (callback == null)
            {
                // make sure to dispose the response stream
                if(!e.Cancelled && e.Error != null)
                    using (var stream = e.Result) { }
                return;
            }

            if (e.Cancelled)
                callback(null, e.UserState, true, null);
            else if (e.Error != null)
                callback(null, e.UserState, false, e.Error);
            else
            {
                try
                {
                    using (var stream = e.Result)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            callback(reader.ReadToEnd(), e.UserState, false, null);
                        }
                    }
                }
                catch (Exception ex)
                {
                    callback(null, e.UserState, false, ex);
                }
            }
        };

    httpHelper.OpenReadAsync(userState);
}
```

You can cancel the asynchronous requests for EPM using `httpHelper.CancelAsync()` method.

### HTTP POST

#### Synchronous Sample

*Synchronous methods are not supported in Silverlight/Windows Phone/WinRT (Windows Metro Apps)*

Make a POST request to https://graph.facebook.com/me/feed and return the response as string. 
For the below sample you can get the `access_token` from http://developers.facebook.com/tools/explorer/ 
Make sure you have `publish_stream` extended permission which is required to post to your facebook wall.

`HTTPHELPER_URLENCODING` conditional compilation symbol must be defined in order to use 
`HttpHelper.UrlEncode` or `HttpHelper.UrlDecode` methods.

Windows Phone and WinRT (Windows Metro Apps) do not have the property ContentLength for HttpWebRequest. In order to reduce
the `#if (WINDOWS_PHONE || NETFX_CORE)` you can use the `TrySetContentLength` method.

```csharp
public static string PostSyncSample(IEnumerable<KeyValuePair<string, string>> parameters)
{
    var httpHelper = new HttpHelper("https://graph.facebook.com/me/feed");
    var request = httpHelper.HttpWebRequest;
    request.Method = "POST";
    request.ContentType = "application/form-url-encoded";

    var bodyString = new StringBuilder();
    foreach (var kvp in parameters)
        bodyString.AppendFormat("{0}={1}&", HttpHelper.UrlEncode(kvp.Key), HttpHelper.UrlEncode(kvp.Value));
    if (bodyString.Length > 0)
        bodyString.Length -= 1;

    var bodyByteArray = Encoding.UTF8.GetBytes(bodyString.ToString());
    request.TrySetContentLength(bodyByteArray.Length);

    // write to request body only if we have data, otherwise directly open the read stream
    if (bodyByteArray.Length > 0)
    {
        using (var stream = httpHelper.OpenWrite())
        {
            stream.Write(bodyByteArray, 0, bodyByteArray.Length);
        }
    }

    using (var stream = httpHelper.OpenRead())
    {
        using (var reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
}

var parameters = new Dictionary<string, string>();
parameters["message"] = "Hi from HttpHelper";
parameters["access_token"] = "";
var result = PostSyncSample(parameters);
Console.WriteLine(result);
```

## Twitter OAuth Sample

HttpHelper includes some basic helper methods for oauth 1.0.

Here is a sample on getting the request token from twitter.

*Note: `GenerateOAuthAuthenticationHeader` will auto generate timestamps/oauth nonce values if you pass it as null. You can either manually pass your own value or use the helper methods such as `HttpHelper.GenerateOAuthNonce()`, `HttpHelper.GenerateOAuthTimestamp`. Since HMAC-SHA1 is not avaialble in the portable library, you will need to add reference to [Portable Library Contrib project](http://pclcontrib.codeplex.com/) and pass the appropriate `HmacSha1Delegate`.*

*Make sure add `#define HTTPHELPER_TPL` if you want to use `OpenReadTaskAsync` method.*

```csharp
public static async Task<IDictionary<string, string>> GetRequestToken(string consumerKey, string consumerSecret, CancellationToken cancellationToken = default (CancellationToken))
{
    var requestTokenUri = new Uri("https://api.twitter.com/oauth/request_token");

    var httHelper = new HttpHelper(requestTokenUri);
    var request = httHelper.HttpWebRequest;
    request.Method = "POST";
    request.ContentType = "application/json";

    request.Headers["Authorization"] = HttpHelper.GenerateOAuthAuthenticationHeader(
        "POST", requestTokenUri, null,
        consumerKey, consumerSecret,
        null, null,
        "HMAC-SHA1", null, null, null, null);

    using (var stream = await httHelper.OpenReadTaskAsync(cancellationToken))
    {
        using (var reader = new StreamReader(stream))
        {
            var response = await reader.ReadToEndAsync();

            var kvp = response.Split('&');
            var dict = new Dictionary<string, string>();
            foreach (var s in kvp)
            {
                var pair = s.Split('=');
                if (pair.Length == 2)
                    dict[pair[0]] = pair[1];
            }

            return dict;
        }
    }
}
```

Then generate the login url and open it in the browser.

```csharp
var requestTokenResult = await GetRequestToken("consumerKey", "consumerSecret");

string token = requestTokenResult["oauth_token"];
string tokenSecret = requestTokenResult["oauth_token_secret"];

Process.Start("https://api.twitter.com/oauth/authorize?oauth_token=" + token);
```

Once you have granted access you will get some random pin number. Then get the access token use the pin.

```csharp
public static async Task<IDictionary<string, string>> GetAccessToken(string consumerKey, string consumerSecret, string token, string tokenSecret, string oauthVerifierPin, CancellationToken cancellationToken = default (CancellationToken))
{
    var requestTokenUri = new Uri("https://api.twitter.com/oauth/access_token");

    var httHelper = new HttpHelper(requestTokenUri);
    var request = httHelper.HttpWebRequest;
    request.Method = "POST";
    request.ContentType = HttpHelper.ApplicationXWWWFormUrlEncodedContentType;
    var data = Encoding.UTF8.GetBytes(string.Concat("oauth_verifier=", oauthVerifierPin));
    request.TrySetContentLength(data.Length);

    var parameters = new Dictionary<string, string>();
    parameters["oauth_verifier"] = oauthVerifierPin;

    request.Headers["Authorization"] = HttpHelper.GenerateOAuthAuthenticationHeader(
        "POST", requestTokenUri, parameters,
        consumerKey, consumerSecret,
        token, tokenSecret,
        "HMAC-SHA1", null, null, null, null);

    using (var stream = await httHelper.OpenWriteTaskAsync(cancellationToken))
    {
        await stream.WriteAsync(data, 0, data.Length, cancellationToken);
    }

    using (var stream = await httHelper.OpenReadTaskAsync(cancellationToken))
    {
        using (var reader = new StreamReader(stream))
        {
            var response = await reader.ReadToEndAsync();

            var kvp = response.Split('&');
            var dict = new Dictionary<string, string>();
            foreach (var s in kvp)
            {
                var pair = s.Split('=');
                if (pair.Length == 2)
                    dict[pair[0]] = pair[1];
            }

            return dict;
        }
    }
}

string pin = "...";

var accessTokenResult = await GetAccessToken(consumerKey, consumerSecret, token, tokenSecret, pin);
var oauthToken = accessTokenResult["oauth_token"];
var oauthTokenSecret = accessTokenResult["oauth_token_secret"];
var userId = accessTokenResult["user_id"];
var screenName = accessTokenResult["screen_name"];

```
