open System
open System.IO

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

#r "../packages/DotLiquid.1.8.0/lib/NET40/DotLiquid.dll"
#r "../packages/Suave.1.1.2/lib/net40/Suave.dll"
#r "../packages/Suave.DotLiquid.1.1.2/lib/net40/Suave.DotLiquid.dll"
#r "../packages/FSharp.Data.2.2.5/lib/net40/FSharp.Data.dll"
#r "../packages/FSharp.Charting.Gtk.0.90.14/lib/net40/FSharp.Charting.Gtk.dll"

let ctx = FSharp.Data.WorldBankData.GetDataContext()

let data = ctx.Countries.``United States``.Indicators.``Poverty gap at $1.90 a day (2011 PPP) (%)``

open Suave
open Suave.Http.HttpRequest
open Suave.Http.HttpResult
open Suave.Web


startWebServer defaultConfig (Successful.OK(sprintf "Hello world! Here's some data: %A" data.Code))
                                                      
                         


