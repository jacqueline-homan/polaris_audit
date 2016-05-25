#r @"/home/jacque/Projects/F-sharp/polaris_audit/Polaris.core/bin/Debug/Polaris.Core.dll"
open Polaris.Core
open Polaris.Core.Types

// printout summary: using the 
// CallSummary.fs file / module.

#load "CallSummary.fs"
open Polaris.CallSummary

// we have some call examples in
// CallsExamples.fsx script file,
// to help us see if our code works.

#load "CallsExamples.fsx"
open CallsExamples

let simpleCall = ``simple call`` ()
let complexCall = ``3 levels call`` ()

// full report on a simple call
createReport simpleCall

// full report on a deeper call
createReport complexCall

// detailed pieces of the report,
// work in progress.
// 1. Survivor or Advocate

callerSummary simpleCall 
|> printfn "%s"

originalNeeds simpleCall 
|> List.iter (printfn "%s")

ngosSummary simpleCall |> printfn "%s"
ngosSummary complexCall |> printfn "%s"

// what needs were addressed in the Call

(*Unsure of the syntax - need to map to a new set 
reflecting the remaining unmetNeedsSummary needs 
after some needs were met
*)

// summary of needs
// 1. what were the original needs
// 2. what needs remain un-addressed: original needs - addressed needs

(*The way I was thinking:
We have to filter the addressed needs out of
the Set<UnmetNeeds> in order to capture that
data for then Survivor's call summary 
and also later on for use in the NGO
database regarding which NGOs helped and how many
survivors got helped vs. not helped *)
let addressedNeedsSummary(Call(caller, callerRequest, callOutcome)) =
    match callerRequest with
    | SurvivorAssistance(needs) ->
        needs
        |> Set.remove (function 
             x.Remove(x.Set(needs)[]) 
        |> Set.filter(needs)    

addressedNeedsSummary simpleCall
 