Zoltu.MediaFormatter.Json
=========================
[![Build status](http://img.shields.io/appveyor/ci/Zoltu/zoltu-mediaformatter-json.svg)](https://ci.appveyor.com/project/Zoltu/zoltu-mediaformatter-json)
[![NuGet Status](http://img.shields.io/nuget/v/Zoltu.MediaFormatter.Json.svg)](https://www.nuget.org/packages/Zoltu.MediaFormatter.Json/)

ASP.NET 5.2 / WebApi 2.2 JSON MediaFormatter backed by Newtonsoft Json.NET.

Usage
=====
```
GlobalConfig.Filters.Add(new Zoltu.MediaFormatter.Json.JsonDeserializationExceptionFilterAttribute());
GlobalConfig.Formatters.Clear();
GlobalConfig.Formatters.Add(new Zoltu.MediaFormatter.Json.JsonMediaFormatter());
```
Note: This will remove all other formatters, you may want to add code to only remove the built-in JSON formatter instead.
