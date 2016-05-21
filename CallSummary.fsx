#r @"/home/jacque/Projects/F-sharp/polaris_audit/Polaris.core/bin/Debug/Polaris.Core.dll"

open Polaris.Core
open Polaris.Core.Types

// We are creating a "fake" call, which
// we can then use to test against our
// code without needed to go through
// the UI first

let caller = Survivor

let needs = 
    set [ 
        UnmetNeeds.Legal
        UnmetNeeds.Medical
        UnmetNeeds.Dental
        UnmetNeeds.IncomeSupport 
        ]

let request = SurvivorAssistance needs

let referredNgo = Ngo(PovertyRelief, "Next NGO")

let result = CallResult(true)

let followUp = NotFollowedUp(Helped(result,request))

let referral = CallerRefToOtherNgo(followUp, referredNgo)

let outcome = CallOutcome.CallerRef(referral,request)

// this is our fake call, which we will use for testing
let call = Call(caller, request, outcome)


// printout summary

#load "CallSummary.fs"
open Polaris.CallSummary

// 1. Survivor or Advocate

callerSummary call 
|> printfn "%s"

(*Unsure of the syntax - need to map to a new set 
reflecting the remaining unmetNeedsSummary needs 
after some needs were met
*)

// summary of needs
// 1. what were the original needs
// 2. what needs remain un-addressed: original needs - addressed needs

originalNeeds call 
|> List.iter (printfn "%s")

// list of NGOs contacted
// this is incomplete: I am getting only the first,
// we should recursively explore the call tree.

let listOfNgosSummary (Call(_, _, callOutcome)) =
    let rec ngoContacted() =
        match outcome with
        | CallerRef (referral,_) -> 
            match referral with
            | CallerRefToOtherNgo (followUp,Ngo(ngoType,ngoName)) ->
                Some(ngoName)
        | _ -> None //This should end the recursion and break out
    ngoContacted()
listOfNgosSummary call

// what needs were addressed in the Call


createReport call

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

addressedNeedsSummary call
 




