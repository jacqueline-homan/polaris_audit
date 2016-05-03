// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.
#load "Component1.fs"

#I "../packages/npgsql.3.0.5/lib/net45/"
#r "Npgsql.dll"


open Npgsql
open Polaris.Postgres


let dbconn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;Database=jacquehammer;User Id=polaris;
Password=polaris;")

let cmd = new Npgsql.NpgsqlCommand()